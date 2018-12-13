﻿using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Controls
{
	public partial class DialogueTree : UserControl
	{
		private string LastViewSetting = "TreeView";

		/// <summary>
		/// Raised when a new case (or stage) is about to be selected
		/// </summary>
		public event EventHandler<CaseSelectionEventArgs> SelectedNodeChanging;
		/// <summary>
		/// Raised when a new case (or stage) is selected
		/// </summary>
		public event EventHandler<CaseSelectionEventArgs> SelectedNodeChanged;
		/// <summary>
		/// Raised when a case is being created, to update its properties before it gets added to the tree
		/// </summary>
		public event EventHandler<CaseCreationEventArgs> CreatingCase;
		/// <summary>
		/// Raised when a case has been created
		/// </summary>
		public event EventHandler<CaseCreationEventArgs> CreatedCase;

		private Character _character;
		private DialogueNode _selectedNode;
		private bool _changingViews;
		private IDialogueTreeView _view;
		private Queue<TreeNode> _pendingDeletion = new Queue<TreeNode>();

		public DialogueTree()
		{
			InitializeComponent();

			recTreeTarget.RecordType = typeof(Character);
			cboTreeFilter.SelectedIndex = 0;
		}

		public void SetData(Character character)
		{
			_character = character;
			cboView.SelectedIndexChanged += cboView_SelectedIndexChanged;
			int view = Config.GetInt(LastViewSetting);
			cboView.SelectedIndex = view;
			if (_view == null)
			{
				_view = new StageView();
				_view.DeleteNode += _view_DeleteNode;
				_view.SaveNode += _view_SaveNode;
				_view.Initialize(treeDialogue, character);
			}
			tsbtnSplit.DropDown = _view.GetCopyMenu();
			_view.SetFilter(TreeFilterMode.All, "");
			_character.Behavior.CaseAdded += Behavior_CaseAdded;
			_character.Behavior.CaseRemoved += Behavior_CaseRemoved;
			_character.Behavior.CaseModified += Behavior_CaseModified;
			PopulateTriggerMenu();
			_view.BuildTree();

			treeDialogue.BeforeSelect += TreeDialogue_BeforeSelect;
			treeDialogue.AfterSelect += TreeDialogue_AfterSelect;
		}

		private void PopulateTriggerMenu()
		{
			List<Trigger> triggers = TriggerDatabase.Triggers;
			triggers.Sort((a, b) => a.Group == b.Group ? a.GroupOrder - b.GroupOrder : a.Group - b.Group);
			int curGroup = -1;
			ContextMenuStrip curGroupMenu = null;

			foreach (Trigger t in triggers)
			{
				if (t.StartStage < 0) continue;
				if (t.Group != curGroup)
				{
					curGroup = t.Group;
					ToolStripMenuItem groupMenuItem = new ToolStripMenuItem();
					groupMenuItem.Text = TriggerDatabase.GetGroupName(curGroup);
					curGroupMenu = new ContextMenuStrip();
					curGroupMenu.ShowImageMargin = false;
					groupMenuItem.DropDown = curGroupMenu;
					triggerMenu.Items.Add(groupMenuItem);
				}
				curGroupMenu.Items.Add(new ToolStripMenuItem(t.Label, null, triggerAddItem_Click, t.Tag));
			}
		}

		/// <summary>
		/// Rebuilds the tree from scratch. Useful when the stages have changed 
		/// </summary>
		public void RegenerateTree()
		{
			_view.BuildTree();
		}

		/// <summary>
		/// Adds a new case and selects it in the tree
		/// </summary>
		/// <param name="tag">Tag of case to add</param>
		/// <param name="singleStage">If true, initial stages will only be the current stage. If false, all possible stages will be checked.</param>
		private void AddAndSelectNewCase(string tag, bool singleStage)
		{
			Case newCase = new Case(tag);
			int startStage;
			Trigger trigger = TriggerDatabase.GetTrigger(tag);
			startStage = trigger.StartStage;
			int currentStage = _selectedNode?.Stage?.Id ?? startStage;

			//Add a default line
			Tuple<string, string> template = DialogueDatabase.GetTemplate(tag);
			DialogueLine line = new DialogueLine(template.Item1, template.Item2);
			newCase.Lines.Add(line);

			if (singleStage)
			{
				newCase.Stages.Add(currentStage);
			}
			else
			{
				for (int stageIndex = 0; stageIndex < _character.Layers + Clothing.ExtraStages; stageIndex++)
				{
					if (TriggerDatabase.UsedInStage(tag, _character, stageIndex))
					{
						newCase.Stages.Add(stageIndex);
					}
				}
			}

			//Give the host control a chance to setup some properties
			CreatingCase?.Invoke(this, new CaseCreationEventArgs(newCase));

			//Add to the collection, which will trigger the CaseAdded event which will add it to the tree
			_character.Behavior.AddWorkingCase(newCase);

			//Raise an event saying the case has been created
			CreatedCase?.Invoke(this, new CaseCreationEventArgs(newCase));

			//Select it
			SelectNode(currentStage, newCase);
		}

		/// <summary>
		/// Selects a node based on a stage and case
		/// </summary>
		/// <param name="stage">Which stage within the case to select</param>
		/// <param name="stageCase">Case to select</param>
		public void SelectNode(int stage, Case stageCase)
		{
			_view.SelectNode(stage, stageCase);
		}

		/// <summary>
		/// Unselects the current node to force a save prior to splitting, duplicating, etc.
		/// </summary>
		private void LeaveNode()
		{
			if (_character == null || _selectedNode?.Case == null)
				return;
			Case selectedCase = _selectedNode.Case;
			CaseSelectionEventArgs args = new CaseSelectionEventArgs(_selectedNode.Stage, selectedCase);
			SelectedNodeChanging?.Invoke(this, args);
			_selectedNode = null;
			treeDialogue.SelectedNode = null;
		}
		
		/// <summary>
		/// Removes a case from the tree
		/// </summary>
		private void DeleteSelectedCase()
		{
			if (_character == null || _selectedNode?.Case == null)
				return;

			if (MessageBox.Show("Are you sure you want to permanently delete this case from all applicable stages?", "Delete Case", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				Case selectedCase = _selectedNode.Case;
				_character.Behavior.RemoveWorkingCase(selectedCase);
			}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				CleanupView();
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Cleans up anything related to the current view
		/// </summary>
		private void CleanupView()
		{
			if (_view == null) { return; }
			_view.DeleteNode -= _view_DeleteNode;
			_view.SaveNode -= _view_SaveNode;
		}

		#region Event handlers
		private void cboTreeFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_character == null) { return; }
			TreeFilterMode mode = (TreeFilterMode)cboTreeFilter.SelectedIndex;
			LeaveNode();
			_view.SetFilter(mode, recTreeTarget.RecordKey);
		}

		private void recTreeTarget_RecordChanged(object sender, IRecord record)
		{
			if (_character == null) { return; }
			TreeFilterMode mode = (TreeFilterMode)cboTreeFilter.SelectedIndex;
			LeaveNode();
			_view.SetFilter(mode, recTreeTarget.RecordKey);
			treeDialogue.ExpandAll();
		}

		private void TreeDialogue_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			if (_changingViews) { return; }
			if (_selectedNode != null)
			{
				CaseSelectionEventArgs args = new CaseSelectionEventArgs();
				args.Stage = _selectedNode.Stage;
				args.Case = _selectedNode.Case;
				SelectedNodeChanging?.Invoke(this, args);
			}
		}

		private void TreeDialogue_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (_changingViews) { return; }
			TreeNode node = treeDialogue.SelectedNode;
			DialogueNode wrapper = node?.Tag as DialogueNode;
			_selectedNode = wrapper;

			CaseSelectionEventArgs args = new CaseSelectionEventArgs();
			if (wrapper != null)
			{
				args.Stage = wrapper.Stage;
				args.Case = wrapper.Case;
			}
			SelectedNodeChanged?.Invoke(this, args);

			if (wrapper == null)
			{
				tsbtnRemoveDialogue.Enabled = false;
				tsbtnSplit.Enabled = false;
				return;
			}

			switch (wrapper.NodeType)
			{
				case NodeType.Case:
					tsbtnRemoveDialogue.Enabled = true;
					tsbtnSplit.Enabled = true;
					break;
				case NodeType.Stage:
					tsbtnRemoveDialogue.Enabled = false;
					tsbtnSplit.Enabled = false;
					break;
			}
		}

		private void Behavior_CaseModified(object sender, Case modifiedCase)
		{
			_view.ModifyCase(modifiedCase);
		}

		private void Behavior_CaseRemoved(object sender, Case removedCase)
		{
			_view.RemoveCase(removedCase);
		}

		private void Behavior_CaseAdded(object sender, Case newCase)
		{
			_view.AddCase(newCase);
		}

		/// <summary>
		/// Adds a new case whose tag matches the currently selected case
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbtnAddDialogue_ButtonClick(object sender, EventArgs e)
		{
			string tag = _view.AddingCase();
			if (tag == null)
			{
				return;
			}
			else if (tag == "")
			{
				tsbtnAddDialogue.ShowDropDown();
			}
			else
			{
				AddAndSelectNewCase(tag, true);
			}
		}

		private void triggerAddItem_Click(object sender, EventArgs e)
		{
			if (_character == null)
				return;

			string tag = ((ToolStripMenuItem)sender).Name;
			AddAndSelectNewCase(tag, false);
		}

		private void tsmiRemove_Click(object sender, EventArgs e)
		{
			DeleteSelectedCase();
		}

		private void treeDialogue_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				DeleteSelectedCase();
			}
		}

		private void tmrDelete_Tick(object sender, EventArgs e)
		{
			tmrDelete.Enabled = false;
			while (_pendingDeletion.Count > 0)
			{
				TreeNode node = _pendingDeletion.Dequeue();
				node.Remove();
			}
		}

		private void _view_DeleteNode(object sender, TreeNode node)
		{
			_pendingDeletion.Enqueue(node);
			tmrDelete.Enabled = true;
		}

		private void _view_SaveNode(object sender, EventArgs e)
		{
			LeaveNode();
		}

		private void cboView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_character == null) { return; }
			_changingViews = true;
			LeaveNode();
			int index = cboView.SelectedIndex;
			Config.Set(LastViewSetting, index);
			CleanupView();
			if (index == 0)
			{
				_view = new StageView();
			}
			else
			{
				_view = new CaseView();
			}
			_view.DeleteNode += _view_DeleteNode;
			_view.SaveNode += _view_SaveNode;
			_view.Initialize(treeDialogue, _character);
			tsbtnSplit.DropDown = _view.GetCopyMenu();
			TreeFilterMode mode = (TreeFilterMode)cboTreeFilter.SelectedIndex;
			_view.SetFilter(mode, recTreeTarget.RecordKey);
			_view.BuildTree();
			_changingViews = false;
		}
		#endregion
	}

	public class CaseSelectionEventArgs : EventArgs
	{
		public Stage Stage;
		public Case Case;

		public CaseSelectionEventArgs() { }

		public CaseSelectionEventArgs(Stage stage, Case theCase)
		{
			Stage = stage;
			Case = theCase;
		}
	}

	public class CaseCreationEventArgs : EventArgs
	{
		/// <summary>
		/// Case being created
		/// </summary>
		public Case Case;

		public CaseCreationEventArgs(Case c)
		{
			Case = c;
		}
	}
}