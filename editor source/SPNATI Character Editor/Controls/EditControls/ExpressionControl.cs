﻿using Desktop;
using Desktop.CommonControls;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SPNATI_Character_Editor
{
	public partial class ExpressionControl : PropertyEditControl
	{
		private ExpressionTest _expression;
		private string _currentVariable;

		public ExpressionControl()
		{
			InitializeComponent();

			cboExpression.Items.AddRange(new string[] {
				"~background~",
				"~background.location~",
				"~cards~",
				"~clothing~",
				"~clothing.plural~",
				"~clothing.position~",
				"~player~",
				"~self.costume~",
				"~self.slot~",
				"~self.tag~",
				"~target.costume~",
				"~target.gender~",
				"~target.position~",
				"~target.size~",
				"~target.slot~",
				"~target.tag~",
				"~weekday~",
			});
			cboExpression.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			cboExpression.AutoCompleteSource = AutoCompleteSource.ListItems;
		}

		public override void ApplyMacro(List<string> values)
		{
			if (values.Count > 2)
			{
				cboExpression.Text = values[0];
				cboOperator.SelectedItem = values[1];
				cboValue.Text = values[2];
			}
		}

		public override void BuildMacro(List<string> values)
		{
			values.Add(cboExpression.Text);
			values.Add(cboOperator.Text);
			values.Add(cboValue.Text);
		}

		public override void OnInitialAdd()
		{
			if (!string.IsNullOrEmpty(cboExpression.Text))
			{
				cboValue.Focus();
			}
		}

		protected override void OnBoundData()
		{
			_expression = GetValue() as ExpressionTest;
			cboExpression.Text = _expression.Expression;
			try
			{
				cboOperator.SelectedItem = _expression.Operator ?? "==";
			}
			catch
			{
				cboOperator.SelectedItem = "==";
			}
			cboValue.Text = _expression.Value;

			if (Bindings.Contains("AlsoPlaying"))
			{
				FillInCharacter();
			}

			UpdateAutoComplete(true);

			cboExpression.TextChanged += TextValueChanged;
			cboOperator.SelectedValueChanged += TextValueChanged;
			cboValue.TextChanged += TextValueChanged;
		}

		protected override void OnBindingUpdated(string property)
		{
			if (property == "AlsoPlaying")
			{
				FillInCharacter();
			}
			if (property == "Target" || property == "AlsoPlaying")
			{
				UpdateAutoComplete(true);
			}
		}

		private void FillInCharacter()
		{
			if (!cboExpression.Text.StartsWith("~_."))
			{
				return;
			}
			Case data = Data as Case;
			if (data != null)
			{
				string id = data.AlsoPlaying;
				if (!string.IsNullOrEmpty(id))
				{
					string key = CharacterDatabase.GetId(id);
					string variable = cboExpression.Text;
					variable = $"~{key}.{variable.Substring(3)}";
					cboExpression.Text = variable;
				}
			}
		}

		private void TextValueChanged(object sender, EventArgs e)
		{
			UpdateAutoComplete(false);
			Save();
		}

		public override void Clear()
		{
			cboExpression.Text = "";
			cboExpression.SelectedItem = "==";
			cboValue.Text = "";
			Save();
		}

		public override void Save()
		{
			_expression.Expression = cboExpression.Text;
			_expression.Operator = cboOperator.Text;
			_expression.Value = cboValue.Text;
		}

		private void UpdateAutoComplete(bool force)
		{
			Character character = Context as Character;
			string variable = cboExpression.Text;
			string key = "";
			string func = "";
			if (!force && variable == _currentVariable)
			{
				return;
			}
			_currentVariable = variable;

			Regex regex = new Regex(@"~(.*)\.(.*)~");
			Match m = regex.Match(variable);
			if (m.Groups.Count > 2)
			{
				key = m.Groups[1].Value;
				func = m.Groups[2].Value;
			}

			cboValue.Items.Clear();
			switch (_currentVariable)
			{
				case "~cards~":
					cboValue.Items.AddRange(new string[] {
						"0",
						"1",
						"2",
						"3",
						"4",
						"5",
					});
					break;
				case "~clothing.position~":
					cboValue.Items.AddRange(new string[] {
						"upper",
						"lower",
						"both",
						"head",
						"neck",
						"hands",
						"arms",
						"feet",
						"legs",
						"waist",
						"other",
					});
					break;
				case "~clothing.plural~":
					cboValue.Items.AddRange(new string[] {
						"plural",
						"single",
					});
					break;
				case "~background.location~":
					cboValue.Items.AddRange(new string[] {
						"indoors",
						"outdoors",
					});
					break;
				case "~background~":
					cboValue.Items.AddRange(new string[] {
						"inventory",
						"beach",
						"classroom",
						"brick",
						"night",
						"roof",
						"seasonal",
						"library",
						"bathhouse",
						"poolside",
						"hot spring",
						"mansion",
						"purple room",
						"showers",
						"street",
						"green screen",
						"arcade",
						"club",
						"bedroom",
						"hall",
						"locker room",
						"haunted forest",
						"romantic",
						"classic",
					});
					break;
				case "~weekday~":
					cboValue.Items.AddRange(new string[] {
						"Sunday",
						"Monday",
						"Tuesday",
						"Wednesday",
						"Thursday",
						"Friday",
						"Saturday",
					});
					break;
				case "~self.costume~":
					cboValue.Items.Add("default");
					if (character != null)
					{
						foreach (AlternateSkin alt in character.Metadata.AlternateSkins)
						{
							foreach (SkinLink skin in alt.Skins)
							{
								cboValue.Items.Add(skin.Costume.Id);
							}
						}
					}
					break;
				case "~self.position~":
					cboValue.Items.AddRange(new string[] {
						"self",
					});
					break;
				case "~self.slot~":
					cboValue.Items.AddRange(new string[] {
						"1",
						"2",
						"3",
						"4",
					});
					break;
				case "~self.tag~":
					cboValue.Items.AddRange(new string[] {
						"true",
						"false",
					});
					break;
				case "~target.costume~":
					Case data = Data as Case;
					cboValue.Items.Add("default");
					if (!string.IsNullOrEmpty(data.Target))
					{
						Character target = CharacterDatabase.Get(data.Target);
						if (target != null)
						{
							foreach (AlternateSkin alt in target.Metadata.AlternateSkins)
							{
								foreach (SkinLink skin in alt.Skins)
								{
									cboValue.Items.Add(skin.Costume.Id);
								}
							}
						}
					}
					break;
				case "~target.gender~":
					cboValue.Items.AddRange(new string[] {
						"female",
						"male",
					});
					break;
				case "~target.position~":
					cboValue.Items.AddRange(new string[] {
						"left",
						"right",
						"self",
					});
					break;
				case "~target.size~":
					cboValue.Items.AddRange(new string[] {
						"small",
						"medium",
						"large",
					});
					break;
				case "~target.slot~":
					cboValue.Items.AddRange(new string[] {
						"1",
						"2",
						"3",
						"4",
					});
					break;
				case "~target.tag~":
					cboValue.Items.AddRange(new string[] {
						"true",
						"false",
					});
					break;
			}
			Character characterVar = CharacterDatabase.GetById(key);
			if (characterVar != null || key == "_")
			{
				switch (func)
				{
					case "costume":
						cboValue.Items.Add("default");
						if (characterVar != null)
						{
							foreach (AlternateSkin alt in characterVar.Metadata.AlternateSkins)
							{
								foreach (SkinLink skin in alt.Skins)
								{
									cboValue.Items.Add(skin.Costume.Id);
								}
							}
						}
						break;
					case "position":
						cboValue.Items.AddRange(new string[] {
							"left",
							"right",
							"self",
						});
						break;
					case "slot":
						cboValue.Items.AddRange(new string[] {
							"1",
							"2",
							"3",
							"4",
						});
						break;
					case "tag":
						cboValue.Items.AddRange(new string[] {
							"true",
							"false",
						});
						break;
				}
			}
			cboValue.Sorted = true;
			cboValue.AutoCompleteSource = AutoCompleteSource.ListItems;
		}
	}

	public class ExpressionAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(ExpressionControl); }
		}
	}
}
