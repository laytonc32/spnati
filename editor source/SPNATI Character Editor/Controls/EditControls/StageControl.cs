﻿using Desktop;
using Desktop.CommonControls;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace SPNATI_Character_Editor
{
	public partial class StageControl : PropertyEditControl
	{
		private bool _filterStagesToTarget;
		private string _sourceProperty;
		private MemberInfo _sourceMember;

		public StageControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			StageSelectAttribute attr = parameters as StageSelectAttribute;
			_filterStagesToTarget = attr.FilterStagesToTarget;
			if (parameters.BoundProperties != null)
			{
				_sourceProperty = parameters.BoundProperties[0];
			}
		}

		protected override void OnBoundData()
		{
			if (_sourceProperty != null)
			{
				_sourceMember = Data.GetType().GetMember(_sourceProperty)[0];
			}

			FillItems();

			string min = GetValue()?.ToString();
			string max = null;
			if (!string.IsNullOrEmpty(min))
			{
				string[] pieces = min.Split('-');
				min = pieces[0];
				if (pieces.Length > 1)
					max = pieces[1];
			}

			SetCombo(cboFrom, min);
			SetCombo(cboTo, max);

			cboFrom.SelectedIndexChanged += cboFrom_SelectedIndexChanged;
			cboTo.SelectedIndexChanged += cboTo_SelectedIndexChanged;
		}

		private void SetCombo(ComboBox box, string stage)
		{
			for (int i = 0; i < box.Items.Count; i++)
			{
				StageName stageName = box.Items[i] as StageName;
				if (stageName != null && stageName.Id == stage)
				{
					box.SelectedIndex = i;
					return;
				}
			}
			box.Text = stage; //If couldn't set an object, just set the text
		}

		protected override void OnBindingUpdated(string property)
		{
			FillItems();
		}

		private void FillItems()
		{
			string from = cboFrom.Text;
			string to = cboTo.Text;

			Case selectedCase = Data as Case;

			string key = _sourceMember.GetValue(Data)?.ToString();
			Character character = CharacterDatabase.Get(key);
			
			string tag = selectedCase?.Tag;
			string filterType = null;
			bool removing = false;
			bool removed = false;
			bool lookForward = false;
			bool filterStages = _filterStagesToTarget;
			if (tag != null && filterStages)
			{
				removing = tag.Contains("removing_");
				removed = tag.Contains("removed_");
				lookForward = removing;
				if (removing || removed)
				{
					int index = tag.LastIndexOf('_');
					if (index >= 0 && index < tag.Length - 1)
					{
						filterType = tag.Substring(index + 1);
						if (filterType == "accessory")
							filterType = "extra";
					}
				}
			}

			List<object> data = new List<object>();
			cboFrom.BindingContext = new BindingContext();
			cboTo.BindingContext = new BindingContext();
			if (character == null)
			{
				//If the character is not valid, still allow something but there's no way to give a useful name to it
				for (int i = 0; i < 8 + Clothing.ExtraStages; i++)
				{
					data.Add(i);
				}
				cboFrom.DataSource = data;
				cboTo.DataSource = data;
			}
			else
			{
				for (int i = 0; i < character.Layers + Clothing.ExtraStages; i++)
				{
					if (filterStages)
					{
						if (filterType != null)
						{
							//Filter out stages that will never be valid
							if (i >= 0 && i <= character.Layers)
							{
								int layer = removed ? i - 1 : i;
								if (layer < 0 || layer >= character.Layers)
									continue;

								Clothing clothing = character.Wardrobe[character.Layers - layer - 1];
								string realType = clothing.Type;
								if (filterType != realType.ToLower())
									continue;
							}
							else continue;
						}
					}
					data.Add(character.LayerToStageName(i, lookForward));
				}
				cboFrom.DataSource = data;
				cboTo.DataSource = data;
				if (!string.IsNullOrEmpty(from))
				{
					cboFrom.Text = from;
				}
				if (!string.IsNullOrEmpty(to))
				{
					cboTo.Text = to;
				}
			}
		}

		public override void Clear()
		{
			cboFrom.Text = "";
			cboTo.Text = "";
		}

		public override void Save()
		{
			string min = ReadStage(cboFrom);
			string max = ReadStage(cboTo);

			if (string.IsNullOrEmpty(min))
			{
				SetValue(null);
				return;
			}
			string value = min;
			if (!string.IsNullOrEmpty(max) && min != max)
			{
				value += "-" + max;
			}
			SetValue(value);
		}

		private string ReadStage(ComboBox box)
		{
			StageName stage = box.SelectedItem as StageName;
			if (stage == null)
			{
				//Must be a generic stage
				return box.Text;
			}

			return stage.Id;
		}

		private void cboFrom_SelectedIndexChanged(object sender, EventArgs e)
		{
			Save();
		}

		private void cboTo_SelectedIndexChanged(object sender, EventArgs e)
		{
			Save();
		}
	}

	public class StageSelectAttribute : EditControlAttribute
	{
		public override Type EditControlType
		{
			get { return typeof(StageControl); }
		}

		public bool FilterStagesToTarget { get; set; }
	}
}