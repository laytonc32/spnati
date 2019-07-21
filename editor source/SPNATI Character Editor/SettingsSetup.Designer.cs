﻿namespace SPNATI_Character_Editor
{
	partial class SettingsSetup
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.txtApplicationDirectory = new Desktop.Skinning.SkinnedTextBox();
			this.label1 = new Desktop.Skinning.SkinnedLabel();
			this.cmdOk = new Desktop.Skinning.SkinnedButton();
			this.cmdBrowse = new Desktop.Skinning.SkinnedButton();
			this.cmdCancel = new Desktop.Skinning.SkinnedButton();
			this.label2 = new Desktop.Skinning.SkinnedLabel();
			this.txtUserName = new Desktop.Skinning.SkinnedTextBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.label3 = new Desktop.Skinning.SkinnedLabel();
			this.valAutoSave = new Desktop.Skinning.SkinnedNumericUpDown();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.chkHidePrefixlessImages = new Desktop.Skinning.SkinnedCheckBox();
			this.helpAutoSave = new Desktop.Skinning.SkinnedIcon();
			this.helpIntellisense = new Desktop.Skinning.SkinnedIcon();
			this.button1 = new Desktop.Skinning.SkinnedIcon();
			this.chkDefaults = new Desktop.Skinning.SkinnedCheckBox();
			this.chkIntellisense = new Desktop.Skinning.SkinnedCheckBox();
			this.label4 = new Desktop.Skinning.SkinnedLabel();
			this.txtFilter = new Desktop.Skinning.SkinnedTextBox();
			this.tabsSections = new Desktop.Skinning.SkinnedTabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.chkHideImages = new Desktop.Skinning.SkinnedCheckBox();
			this.cmdBrowseKisekae = new Desktop.Skinning.SkinnedButton();
			this.txtKisekae = new Desktop.Skinning.SkinnedTextBox();
			this.label5 = new Desktop.Skinning.SkinnedLabel();
			this.tabDialogue = new System.Windows.Forms.TabPage();
			this.chkColorTargets = new Desktop.Skinning.SkinnedCheckBox();
			this.chkCaseTree = new Desktop.Skinning.SkinnedCheckBox();
			this.chkInitialAdd = new Desktop.Skinning.SkinnedCheckBox();
			this.tabBanter = new System.Windows.Forms.TabPage();
			this.chkAutoBanter = new Desktop.Skinning.SkinnedCheckBox();
			this.tabEpilogues = new System.Windows.Forms.TabPage();
			this.label6 = new Desktop.Skinning.SkinnedLabel();
			this.lstPauses = new Desktop.Skinning.SkinnedCheckedListBox();
			this.stripSections = new Desktop.Skinning.SkinnedTabStrip();
			this.skinnedPanel1 = new Desktop.Skinning.SkinnedPanel();
			this.tabTroubleshoot = new System.Windows.Forms.TabPage();
			this.chkAutoBackup = new Desktop.Skinning.SkinnedCheckBox();
			this.chkWorkflowTracer = new Desktop.Skinning.SkinnedCheckBox();
			((System.ComponentModel.ISupportInitialize)(this.valAutoSave)).BeginInit();
			this.tabsSections.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.tabDialogue.SuspendLayout();
			this.tabBanter.SuspendLayout();
			this.tabEpilogues.SuspendLayout();
			this.skinnedPanel1.SuspendLayout();
			this.tabTroubleshoot.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtApplicationDirectory
			// 
			this.txtApplicationDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtApplicationDirectory.BackColor = System.Drawing.Color.White;
			this.txtApplicationDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtApplicationDirectory.ForeColor = System.Drawing.Color.Black;
			this.txtApplicationDirectory.Location = new System.Drawing.Point(133, 8);
			this.txtApplicationDirectory.Name = "txtApplicationDirectory";
			this.txtApplicationDirectory.Size = new System.Drawing.Size(238, 20);
			this.txtApplicationDirectory.TabIndex = 0;
			this.txtApplicationDirectory.Validating += new System.ComponentModel.CancelEventHandler(this.txtApplicationDirectory_Validating);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label1.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label1.Location = new System.Drawing.Point(6, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "SPNATI repository:";
			// 
			// cmdOk
			// 
			this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOk.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdOk.FieldType = Desktop.Skinning.SkinnedFieldType.Secondary;
			this.cmdOk.Flat = false;
			this.cmdOk.ForeColor = System.Drawing.Color.White;
			this.cmdOk.Location = new System.Drawing.Point(498, 4);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 23);
			this.cmdOk.TabIndex = 4;
			this.cmdOk.Text = "OK";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowse.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdBrowse.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdBrowse.Flat = false;
			this.cmdBrowse.Location = new System.Drawing.Point(377, 6);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(32, 23);
			this.cmdBrowse.TabIndex = 1;
			this.cmdBrowse.Text = "...";
			this.cmdBrowse.UseVisualStyleBackColor = true;
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdCancel.Flat = true;
			this.cmdCancel.ForeColor = System.Drawing.Color.White;
			this.cmdCancel.Location = new System.Drawing.Point(579, 4);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 5;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label2.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label2.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label2.Location = new System.Drawing.Point(7, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Username:";
			// 
			// txtUserName
			// 
			this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUserName.BackColor = System.Drawing.Color.White;
			this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtUserName.ForeColor = System.Drawing.Color.Black;
			this.txtUserName.Location = new System.Drawing.Point(133, 60);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(276, 20);
			this.txtUserName.TabIndex = 4;
			this.toolTip1.SetToolTip(this.txtUserName, "This is used for auto-saving. Only characters written by this user will be auto-s" +
        "aved.");
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Filter = "Exe files|*.exe";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label3.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label3.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label3.Location = new System.Drawing.Point(7, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(103, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Auto-save (minutes):";
			// 
			// valAutoSave
			// 
			this.valAutoSave.BackColor = System.Drawing.Color.White;
			this.valAutoSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.valAutoSave.ForeColor = System.Drawing.Color.Black;
			this.valAutoSave.Location = new System.Drawing.Point(133, 85);
			this.valAutoSave.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.valAutoSave.Name = "valAutoSave";
			this.valAutoSave.Size = new System.Drawing.Size(45, 20);
			this.valAutoSave.TabIndex = 5;
			this.toolTip1.SetToolTip(this.valAutoSave, "Number of minutes to auto-save characters you\'ve written. Use 0 to disable auto-s" +
        "ave.");
			// 
			// chkHidePrefixlessImages
			// 
			this.chkHidePrefixlessImages.AutoSize = true;
			this.chkHidePrefixlessImages.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkHidePrefixlessImages.Location = new System.Drawing.Point(6, 29);
			this.chkHidePrefixlessImages.Name = "chkHidePrefixlessImages";
			this.chkHidePrefixlessImages.Size = new System.Drawing.Size(143, 17);
			this.chkHidePrefixlessImages.TabIndex = 22;
			this.chkHidePrefixlessImages.Text = "Include prefixless images";
			this.toolTip1.SetToolTip(this.chkHidePrefixlessImages, "If unchecked, images with no prefix (ex. 0-*.png) will not appear for use in dial" +
        "ogue lines.");
			this.chkHidePrefixlessImages.UseVisualStyleBackColor = true;
			this.chkHidePrefixlessImages.CheckedChanged += new System.EventHandler(this.chkHideImages_CheckedChanged);
			// 
			// helpAutoSave
			// 
			this.helpAutoSave.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.helpAutoSave.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.helpAutoSave.Flat = false;
			this.helpAutoSave.FlatAppearance.BorderSize = 0;
			this.helpAutoSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.helpAutoSave.Image = global::SPNATI_Character_Editor.Properties.Resources.Help;
			this.helpAutoSave.Location = new System.Drawing.Point(182, 83);
			this.helpAutoSave.Name = "helpAutoSave";
			this.helpAutoSave.Size = new System.Drawing.Size(21, 23);
			this.helpAutoSave.TabIndex = 6;
			this.toolTip1.SetToolTip(this.helpAutoSave, "Only characters with your username as the writer can be auto-saved");
			this.helpAutoSave.UseVisualStyleBackColor = true;
			// 
			// helpIntellisense
			// 
			this.helpIntellisense.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.helpIntellisense.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.helpIntellisense.Flat = false;
			this.helpIntellisense.FlatAppearance.BorderSize = 0;
			this.helpIntellisense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.helpIntellisense.Image = global::SPNATI_Character_Editor.Properties.Resources.Help;
			this.helpIntellisense.Location = new System.Drawing.Point(145, 2);
			this.helpIntellisense.Name = "helpIntellisense";
			this.helpIntellisense.Size = new System.Drawing.Size(26, 23);
			this.helpIntellisense.TabIndex = 21;
			this.toolTip1.SetToolTip(this.helpIntellisense, "Typing ~ in a dialogue line will bring up a popup containing available variables," +
        " their meanings, parameters, and so on.");
			this.helpIntellisense.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.button1.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.button1.Flat = false;
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Image = global::SPNATI_Character_Editor.Properties.Resources.Help;
			this.button1.Location = new System.Drawing.Point(145, 25);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(26, 23);
			this.button1.TabIndex = 23;
			this.toolTip1.SetToolTip(this.button1, "When checked, every image in the character\'s folder with no stage prefix (ex. 2-h" +
        "appy.png) will be available for poses in every stage.");
			this.button1.UseVisualStyleBackColor = true;
			// 
			// chkDefaults
			// 
			this.chkDefaults.AutoSize = true;
			this.chkDefaults.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkDefaults.Location = new System.Drawing.Point(6, 97);
			this.chkDefaults.Name = "chkDefaults";
			this.chkDefaults.Size = new System.Drawing.Size(179, 17);
			this.chkDefaults.TabIndex = 26;
			this.chkDefaults.Text = "Ensure cases have generic lines";
			this.toolTip1.SetToolTip(this.chkDefaults, "If unchecked, images with no prefix (ex. 0-*.png) will not appear for use in dial" +
        "ogue lines.");
			this.chkDefaults.UseVisualStyleBackColor = true;
			// 
			// chkIntellisense
			// 
			this.chkIntellisense.AutoSize = true;
			this.chkIntellisense.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkIntellisense.Location = new System.Drawing.Point(6, 6);
			this.chkIntellisense.Name = "chkIntellisense";
			this.chkIntellisense.Size = new System.Drawing.Size(139, 17);
			this.chkIntellisense.TabIndex = 20;
			this.chkIntellisense.Text = "Use variable intellisense";
			this.chkIntellisense.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label4.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label4.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label4.Location = new System.Drawing.Point(3, 53);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(143, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Exclude images starting with:";
			// 
			// txtFilter
			// 
			this.txtFilter.BackColor = System.Drawing.Color.White;
			this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtFilter.ForeColor = System.Drawing.Color.Black;
			this.txtFilter.Location = new System.Drawing.Point(145, 50);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(92, 20);
			this.txtFilter.TabIndex = 24;
			// 
			// tabsSections
			// 
			this.tabsSections.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.tabsSections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabsSections.Controls.Add(this.tabGeneral);
			this.tabsSections.Controls.Add(this.tabDialogue);
			this.tabsSections.Controls.Add(this.tabBanter);
			this.tabsSections.Controls.Add(this.tabEpilogues);
			this.tabsSections.Controls.Add(this.tabTroubleshoot);
			this.tabsSections.ItemSize = new System.Drawing.Size(25, 100);
			this.tabsSections.Location = new System.Drawing.Point(101, 27);
			this.tabsSections.Margin = new System.Windows.Forms.Padding(0);
			this.tabsSections.Multiline = true;
			this.tabsSections.Name = "tabsSections";
			this.tabsSections.SelectedIndex = 0;
			this.tabsSections.Size = new System.Drawing.Size(555, 181);
			this.tabsSections.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabsSections.TabIndex = 12;
			// 
			// tabGeneral
			// 
			this.tabGeneral.BackColor = System.Drawing.Color.White;
			this.tabGeneral.Controls.Add(this.chkHideImages);
			this.tabGeneral.Controls.Add(this.cmdBrowseKisekae);
			this.tabGeneral.Controls.Add(this.txtKisekae);
			this.tabGeneral.Controls.Add(this.label5);
			this.tabGeneral.Controls.Add(this.helpAutoSave);
			this.tabGeneral.Controls.Add(this.cmdBrowse);
			this.tabGeneral.Controls.Add(this.txtApplicationDirectory);
			this.tabGeneral.Controls.Add(this.label1);
			this.tabGeneral.Controls.Add(this.txtUserName);
			this.tabGeneral.Controls.Add(this.valAutoSave);
			this.tabGeneral.Controls.Add(this.label2);
			this.tabGeneral.Controls.Add(this.label3);
			this.tabGeneral.ForeColor = System.Drawing.Color.Black;
			this.tabGeneral.Location = new System.Drawing.Point(104, 4);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabGeneral.Size = new System.Drawing.Size(447, 173);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			// 
			// chkHideImages
			// 
			this.chkHideImages.AutoSize = true;
			this.chkHideImages.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkHideImages.Location = new System.Drawing.Point(10, 111);
			this.chkHideImages.Name = "chkHideImages";
			this.chkHideImages.Size = new System.Drawing.Size(126, 17);
			this.chkHideImages.TabIndex = 9;
			this.chkHideImages.Text = "Hide Preview Images";
			this.chkHideImages.UseVisualStyleBackColor = true;
			this.chkHideImages.CheckedChanged += new System.EventHandler(this.chkHideImages_CheckedChanged_1);
			// 
			// cmdBrowseKisekae
			// 
			this.cmdBrowseKisekae.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowseKisekae.Background = Desktop.Skinning.SkinnedBackgroundType.Surface;
			this.cmdBrowseKisekae.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.cmdBrowseKisekae.Flat = false;
			this.cmdBrowseKisekae.Location = new System.Drawing.Point(377, 32);
			this.cmdBrowseKisekae.Name = "cmdBrowseKisekae";
			this.cmdBrowseKisekae.Size = new System.Drawing.Size(32, 23);
			this.cmdBrowseKisekae.TabIndex = 3;
			this.cmdBrowseKisekae.Text = "...";
			this.cmdBrowseKisekae.UseVisualStyleBackColor = true;
			this.cmdBrowseKisekae.Click += new System.EventHandler(this.cmdBrowseKisekae_Click);
			// 
			// txtKisekae
			// 
			this.txtKisekae.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtKisekae.BackColor = System.Drawing.Color.White;
			this.txtKisekae.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.txtKisekae.ForeColor = System.Drawing.Color.Black;
			this.txtKisekae.Location = new System.Drawing.Point(133, 34);
			this.txtKisekae.Name = "txtKisekae";
			this.txtKisekae.Size = new System.Drawing.Size(238, 20);
			this.txtKisekae.TabIndex = 2;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label5.Highlight = Desktop.Skinning.SkinnedHighlight.Label;
			this.label5.Level = Desktop.Skinning.SkinnedLabelLevel.Normal;
			this.label5.Location = new System.Drawing.Point(7, 37);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(90, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "KKL.exe location:";
			// 
			// tabDialogue
			// 
			this.tabDialogue.BackColor = System.Drawing.Color.White;
			this.tabDialogue.Controls.Add(this.chkColorTargets);
			this.tabDialogue.Controls.Add(this.chkCaseTree);
			this.tabDialogue.Controls.Add(this.chkDefaults);
			this.tabDialogue.Controls.Add(this.chkInitialAdd);
			this.tabDialogue.Controls.Add(this.button1);
			this.tabDialogue.Controls.Add(this.helpIntellisense);
			this.tabDialogue.Controls.Add(this.chkIntellisense);
			this.tabDialogue.Controls.Add(this.txtFilter);
			this.tabDialogue.Controls.Add(this.chkHidePrefixlessImages);
			this.tabDialogue.Controls.Add(this.label4);
			this.tabDialogue.ForeColor = System.Drawing.Color.Black;
			this.tabDialogue.Location = new System.Drawing.Point(104, 4);
			this.tabDialogue.Name = "tabDialogue";
			this.tabDialogue.Padding = new System.Windows.Forms.Padding(3);
			this.tabDialogue.Size = new System.Drawing.Size(447, 173);
			this.tabDialogue.TabIndex = 1;
			this.tabDialogue.Text = "Dialogue";
			// 
			// chkColorTargets
			// 
			this.chkColorTargets.AutoSize = true;
			this.chkColorTargets.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkColorTargets.Location = new System.Drawing.Point(6, 143);
			this.chkColorTargets.Name = "chkColorTargets";
			this.chkColorTargets.Size = new System.Drawing.Size(239, 17);
			this.chkColorTargets.TabIndex = 28;
			this.chkColorTargets.Text = "Color code lines that target another character";
			this.chkColorTargets.UseVisualStyleBackColor = true;
			// 
			// chkCaseTree
			// 
			this.chkCaseTree.AutoSize = true;
			this.chkCaseTree.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkCaseTree.Location = new System.Drawing.Point(6, 120);
			this.chkCaseTree.Name = "chkCaseTree";
			this.chkCaseTree.Size = new System.Drawing.Size(230, 17);
			this.chkCaseTree.TabIndex = 27;
			this.chkCaseTree.Text = "Use multi-column case tree (restart needed)";
			this.chkCaseTree.UseVisualStyleBackColor = true;
			// 
			// chkInitialAdd
			// 
			this.chkInitialAdd.AutoSize = true;
			this.chkInitialAdd.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkInitialAdd.Location = new System.Drawing.Point(6, 74);
			this.chkInitialAdd.Name = "chkInitialAdd";
			this.chkInitialAdd.Size = new System.Drawing.Size(258, 17);
			this.chkInitialAdd.TabIndex = 25;
			this.chkInitialAdd.Text = "Auto-open selection form when adding conditions";
			this.chkInitialAdd.UseVisualStyleBackColor = true;
			// 
			// tabBanter
			// 
			this.tabBanter.BackColor = System.Drawing.Color.White;
			this.tabBanter.Controls.Add(this.chkAutoBanter);
			this.tabBanter.ForeColor = System.Drawing.Color.Black;
			this.tabBanter.Location = new System.Drawing.Point(104, 4);
			this.tabBanter.Name = "tabBanter";
			this.tabBanter.Padding = new System.Windows.Forms.Padding(3);
			this.tabBanter.Size = new System.Drawing.Size(447, 173);
			this.tabBanter.TabIndex = 2;
			this.tabBanter.Text = "Banter Wizard";
			// 
			// chkAutoBanter
			// 
			this.chkAutoBanter.AutoSize = true;
			this.chkAutoBanter.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkAutoBanter.Location = new System.Drawing.Point(6, 6);
			this.chkAutoBanter.Name = "chkAutoBanter";
			this.chkAutoBanter.Size = new System.Drawing.Size(383, 17);
			this.chkAutoBanter.TabIndex = 0;
			this.chkAutoBanter.Text = "Always filter list to characters who actually target yours (very slow initial loa" +
    "d)";
			this.chkAutoBanter.UseVisualStyleBackColor = true;
			// 
			// tabEpilogues
			// 
			this.tabEpilogues.BackColor = System.Drawing.Color.White;
			this.tabEpilogues.Controls.Add(this.label6);
			this.tabEpilogues.Controls.Add(this.lstPauses);
			this.tabEpilogues.ForeColor = System.Drawing.Color.Black;
			this.tabEpilogues.Location = new System.Drawing.Point(104, 4);
			this.tabEpilogues.Name = "tabEpilogues";
			this.tabEpilogues.Padding = new System.Windows.Forms.Padding(3);
			this.tabEpilogues.Size = new System.Drawing.Size(447, 173);
			this.tabEpilogues.TabIndex = 3;
			this.tabEpilogues.Text = "Epilogues";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label6.Highlight = Desktop.Skinning.SkinnedHighlight.Normal;
			this.label6.Level = Desktop.Skinning.SkinnedLabelLevel.Label;
			this.label6.Location = new System.Drawing.Point(6, 7);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(237, 13);
			this.label6.TabIndex = 1;
			this.label6.Text = "Auto-add \"Wait for Input\" directive when adding:";
			// 
			// lstPauses
			// 
			this.lstPauses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstPauses.BackColor = System.Drawing.Color.White;
			this.lstPauses.CheckOnClick = true;
			this.lstPauses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lstPauses.ForeColor = System.Drawing.Color.Black;
			this.lstPauses.FormattingEnabled = true;
			this.lstPauses.Location = new System.Drawing.Point(6, 23);
			this.lstPauses.Name = "lstPauses";
			this.lstPauses.Size = new System.Drawing.Size(120, 124);
			this.lstPauses.TabIndex = 0;
			// 
			// stripSections
			// 
			this.stripSections.AddCaption = null;
			this.stripSections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.stripSections.Location = new System.Drawing.Point(1, 27);
			this.stripSections.Margin = new System.Windows.Forms.Padding(0);
			this.stripSections.Name = "stripSections";
			this.stripSections.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryLight;
			this.stripSections.ShowAddButton = false;
			this.stripSections.ShowCloseButton = false;
			this.stripSections.Size = new System.Drawing.Size(100, 181);
			this.stripSections.StartMargin = 5;
			this.stripSections.TabControl = this.tabsSections;
			this.stripSections.TabIndex = 13;
			this.stripSections.TabMargin = 5;
			this.stripSections.TabPadding = 20;
			this.stripSections.TabSize = 25;
			this.stripSections.TabType = Desktop.Skinning.SkinnedBackgroundType.Background;
			this.stripSections.Vertical = true;
			// 
			// skinnedPanel1
			// 
			this.skinnedPanel1.Controls.Add(this.cmdCancel);
			this.skinnedPanel1.Controls.Add(this.cmdOk);
			this.skinnedPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.skinnedPanel1.Location = new System.Drawing.Point(0, 208);
			this.skinnedPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.skinnedPanel1.Name = "skinnedPanel1";
			this.skinnedPanel1.PanelType = Desktop.Skinning.SkinnedBackgroundType.PrimaryDark;
			this.skinnedPanel1.Size = new System.Drawing.Size(657, 30);
			this.skinnedPanel1.TabIndex = 14;
			this.skinnedPanel1.TabSide = Desktop.Skinning.TabSide.None;
			// 
			// tabTroubleshoot
			// 
			this.tabTroubleshoot.BackColor = System.Drawing.Color.White;
			this.tabTroubleshoot.Controls.Add(this.chkWorkflowTracer);
			this.tabTroubleshoot.Controls.Add(this.chkAutoBackup);
			this.tabTroubleshoot.ForeColor = System.Drawing.Color.Black;
			this.tabTroubleshoot.Location = new System.Drawing.Point(104, 4);
			this.tabTroubleshoot.Name = "tabTroubleshoot";
			this.tabTroubleshoot.Padding = new System.Windows.Forms.Padding(3);
			this.tabTroubleshoot.Size = new System.Drawing.Size(447, 173);
			this.tabTroubleshoot.TabIndex = 4;
			this.tabTroubleshoot.Text = "Diagnostics";
			// 
			// chkAutoBackup
			// 
			this.chkAutoBackup.AutoSize = true;
			this.chkAutoBackup.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkAutoBackup.Location = new System.Drawing.Point(6, 6);
			this.chkAutoBackup.Name = "chkAutoBackup";
			this.chkAutoBackup.Size = new System.Drawing.Size(176, 17);
			this.chkAutoBackup.TabIndex = 8;
			this.chkAutoBackup.Text = "Create data recovery snapshots";
			this.chkAutoBackup.UseVisualStyleBackColor = true;
			// 
			// chkWorkflowTracer
			// 
			this.chkWorkflowTracer.AutoSize = true;
			this.chkWorkflowTracer.FieldType = Desktop.Skinning.SkinnedFieldType.Primary;
			this.chkWorkflowTracer.Location = new System.Drawing.Point(6, 29);
			this.chkWorkflowTracer.Name = "chkWorkflowTracer";
			this.chkWorkflowTracer.Size = new System.Drawing.Size(245, 17);
			this.chkWorkflowTracer.TabIndex = 9;
			this.chkWorkflowTracer.Text = "Record workflow screenshots for crash reports";
			this.chkWorkflowTracer.UseVisualStyleBackColor = true;
			// 
			// SettingsSetup
			// 
			this.AcceptButton = this.cmdOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(657, 238);
			this.Controls.Add(this.skinnedPanel1);
			this.Controls.Add(this.stripSections);
			this.Controls.Add(this.tabsSections);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsSetup";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Sizable = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Settings";
			((System.ComponentModel.ISupportInitialize)(this.valAutoSave)).EndInit();
			this.tabsSections.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.tabGeneral.PerformLayout();
			this.tabDialogue.ResumeLayout(false);
			this.tabDialogue.PerformLayout();
			this.tabBanter.ResumeLayout(false);
			this.tabBanter.PerformLayout();
			this.tabEpilogues.ResumeLayout(false);
			this.tabEpilogues.PerformLayout();
			this.skinnedPanel1.ResumeLayout(false);
			this.tabTroubleshoot.ResumeLayout(false);
			this.tabTroubleshoot.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private Desktop.Skinning.SkinnedTextBox txtApplicationDirectory;
		private Desktop.Skinning.SkinnedLabel label1;
		private Desktop.Skinning.SkinnedButton cmdOk;
		private Desktop.Skinning.SkinnedButton cmdBrowse;
		private Desktop.Skinning.SkinnedButton cmdCancel;
		private Desktop.Skinning.SkinnedLabel label2;
		private Desktop.Skinning.SkinnedTextBox txtUserName;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private Desktop.Skinning.SkinnedLabel label3;
		private Desktop.Skinning.SkinnedNumericUpDown valAutoSave;
		private System.Windows.Forms.ToolTip toolTip1;
		private Desktop.Skinning.SkinnedCheckBox chkIntellisense;
		private Desktop.Skinning.SkinnedCheckBox chkHidePrefixlessImages;
		private Desktop.Skinning.SkinnedLabel label4;
		private Desktop.Skinning.SkinnedTextBox txtFilter;
		private Desktop.Skinning.SkinnedTabControl tabsSections;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabDialogue;
		private Desktop.Skinning.SkinnedIcon helpAutoSave;
		private Desktop.Skinning.SkinnedIcon button1;
		private Desktop.Skinning.SkinnedIcon helpIntellisense;
		private System.Windows.Forms.TabPage tabBanter;
		private Desktop.Skinning.SkinnedCheckBox chkAutoBanter;
		private Desktop.Skinning.SkinnedCheckBox chkInitialAdd;
		private Desktop.Skinning.SkinnedButton cmdBrowseKisekae;
		private Desktop.Skinning.SkinnedTextBox txtKisekae;
		private Desktop.Skinning.SkinnedLabel label5;
		private Desktop.Skinning.SkinnedCheckBox chkDefaults;
		private System.Windows.Forms.TabPage tabEpilogues;
		private Desktop.Skinning.SkinnedLabel label6;
		private Desktop.Skinning.SkinnedCheckedListBox lstPauses;
		private Desktop.Skinning.SkinnedCheckBox chkCaseTree;
		private Desktop.Skinning.SkinnedCheckBox chkHideImages;
		private Desktop.Skinning.SkinnedTabStrip stripSections;
		private Desktop.Skinning.SkinnedPanel skinnedPanel1;
		private Desktop.Skinning.SkinnedCheckBox chkColorTargets;
		private System.Windows.Forms.TabPage tabTroubleshoot;
		private Desktop.Skinning.SkinnedCheckBox chkWorkflowTracer;
		private Desktop.Skinning.SkinnedCheckBox chkAutoBackup;
	}
}