﻿namespace SPNATI_Character_Editor.Activities
{
	partial class SkinEditor
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cboBaseStage = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.gridLabels = new SPNATI_Character_Editor.Controls.StageSpecificGrid();
			this.label22 = new System.Windows.Forms.Label();
			this.cboDefaultPic = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(53, 3);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(156, 20);
			this.txtName.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(169, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Use base images starting at stage:";
			// 
			// cboBaseStage
			// 
			this.cboBaseStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboBaseStage.FormattingEnabled = true;
			this.cboBaseStage.Location = new System.Drawing.Point(178, 61);
			this.cboBaseStage.Name = "cboBaseStage";
			this.cboBaseStage.Size = new System.Drawing.Size(129, 21);
			this.cboBaseStage.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 97);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Labels:";
			// 
			// gridLabels
			// 
			this.gridLabels.Label = "Display Name";
			this.gridLabels.Location = new System.Drawing.Point(6, 113);
			this.gridLabels.Name = "gridLabels";
			this.gridLabels.Size = new System.Drawing.Size(195, 151);
			this.gridLabels.TabIndex = 5;
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(3, 32);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(43, 13);
			this.label22.TabIndex = 96;
			this.label22.Text = "Portrait:";
			// 
			// cboDefaultPic
			// 
			this.cboDefaultPic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDefaultPic.FormattingEnabled = true;
			this.cboDefaultPic.Location = new System.Drawing.Point(53, 29);
			this.cboDefaultPic.Name = "cboDefaultPic";
			this.cboDefaultPic.Size = new System.Drawing.Size(156, 21);
			this.cboDefaultPic.TabIndex = 2;
			this.cboDefaultPic.SelectedIndexChanged += new System.EventHandler(this.cboDefaultPic_SelectedIndexChanged);
			// 
			// SkinEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label22);
			this.Controls.Add(this.cboDefaultPic);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.gridLabels);
			this.Controls.Add(this.cboBaseStage);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label1);
			this.Name = "SkinEditor";
			this.Size = new System.Drawing.Size(562, 538);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboBaseStage;
		private Controls.StageSpecificGrid gridLabels;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.ComboBox cboDefaultPic;
	}
}