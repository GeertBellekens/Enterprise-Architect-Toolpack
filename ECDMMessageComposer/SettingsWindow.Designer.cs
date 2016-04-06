/*
 * Created by SharpDevelop.
 * User: Geert
 * Date: 27/01/2016
 * Time: 5:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ECDMMessageComposer
{
	partial class SettingsWindow
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataGridView ignoredStereoTypesGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn StereotypeColumn;
		private System.Windows.Forms.Button deleteStereotypeButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.DataGridView ignoredTaggedValuesGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.Button deleteTaggedValueButton;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox diagramOptionsGroupBox;
		private System.Windows.Forms.CheckBox addSourceElementCheckBox;
		private System.Windows.Forms.CheckBox addDataTypesCheckBox;
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
			this.ignoredStereoTypesGrid = new System.Windows.Forms.DataGridView();
			this.StereotypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.deleteStereotypeButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.applyButton = new System.Windows.Forms.Button();
			this.ignoredTaggedValuesGrid = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.deleteTaggedValueButton = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.diagramOptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.addSourceElementCheckBox = new System.Windows.Forms.CheckBox();
			this.addDataTypesCheckBox = new System.Windows.Forms.CheckBox();
			this.copyDatatypesCheckbox = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.ignoredStereoTypesGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ignoredTaggedValuesGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.diagramOptionsGroupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ignoredStereoTypesGrid
			// 
			this.ignoredStereoTypesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.ignoredStereoTypesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ignoredStereoTypesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.StereotypeColumn});
			this.ignoredStereoTypesGrid.Location = new System.Drawing.Point(2, 3);
			this.ignoredStereoTypesGrid.Name = "ignoredStereoTypesGrid";
			this.ignoredStereoTypesGrid.RowHeadersVisible = false;
			this.ignoredStereoTypesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.ignoredStereoTypesGrid.Size = new System.Drawing.Size(166, 186);
			this.ignoredStereoTypesGrid.TabIndex = 0;
			// 
			// StereotypeColumn
			// 
			this.StereotypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.StereotypeColumn.HeaderText = "Ignored Stereotypes";
			this.StereotypeColumn.Name = "StereotypeColumn";
			this.StereotypeColumn.ToolTipText = "Classes with these stereotypes will not be deleted when updating a subset model";
			// 
			// deleteStereotypeButton
			// 
			this.deleteStereotypeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteStereotypeButton.Location = new System.Drawing.Point(93, 195);
			this.deleteStereotypeButton.Name = "deleteStereotypeButton";
			this.deleteStereotypeButton.Size = new System.Drawing.Size(75, 23);
			this.deleteStereotypeButton.TabIndex = 1;
			this.deleteStereotypeButton.Text = "Delete";
			this.deleteStereotypeButton.UseVisualStyleBackColor = true;
			this.deleteStereotypeButton.Click += new System.EventHandler(this.DeleteStereotypeButtonClick);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(115, 380);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(196, 380);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.applyButton.Location = new System.Drawing.Point(277, 380);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(75, 23);
			this.applyButton.TabIndex = 4;
			this.applyButton.Text = "Apply";
			this.applyButton.UseVisualStyleBackColor = true;
			this.applyButton.Click += new System.EventHandler(this.ApplyButtonClick);
			// 
			// ignoredTaggedValuesGrid
			// 
			this.ignoredTaggedValuesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.ignoredTaggedValuesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ignoredTaggedValuesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.dataGridViewTextBoxColumn1});
			this.ignoredTaggedValuesGrid.Location = new System.Drawing.Point(3, 3);
			this.ignoredTaggedValuesGrid.Name = "ignoredTaggedValuesGrid";
			this.ignoredTaggedValuesGrid.RowHeadersVisible = false;
			this.ignoredTaggedValuesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.ignoredTaggedValuesGrid.Size = new System.Drawing.Size(159, 186);
			this.ignoredTaggedValuesGrid.TabIndex = 5;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn1.HeaderText = "Ignored Tagged Values";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ToolTipText = "These tagged values will be left untouched when they have a value in the subset m" +
	"odel";
			// 
			// deleteTaggedValueButton
			// 
			this.deleteTaggedValueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteTaggedValueButton.Location = new System.Drawing.Point(84, 195);
			this.deleteTaggedValueButton.Name = "deleteTaggedValueButton";
			this.deleteTaggedValueButton.Size = new System.Drawing.Size(75, 23);
			this.deleteTaggedValueButton.TabIndex = 6;
			this.deleteTaggedValueButton.Text = "Delete";
			this.deleteTaggedValueButton.UseVisualStyleBackColor = true;
			this.deleteTaggedValueButton.Click += new System.EventHandler(this.DeleteTaggedValueButtonClick);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(12, 15);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.ignoredStereoTypesGrid);
			this.splitContainer1.Panel1.Controls.Add(this.deleteStereotypeButton);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.deleteTaggedValueButton);
			this.splitContainer1.Panel2.Controls.Add(this.ignoredTaggedValuesGrid);
			this.splitContainer1.Size = new System.Drawing.Size(340, 226);
			this.splitContainer1.SplitterDistance = 171;
			this.splitContainer1.TabIndex = 7;
			// 
			// diagramOptionsGroupBox
			// 
			this.diagramOptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.diagramOptionsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.diagramOptionsGroupBox.Controls.Add(this.addSourceElementCheckBox);
			this.diagramOptionsGroupBox.Controls.Add(this.addDataTypesCheckBox);
			this.diagramOptionsGroupBox.Location = new System.Drawing.Point(12, 298);
			this.diagramOptionsGroupBox.MinimumSize = new System.Drawing.Size(0, 75);
			this.diagramOptionsGroupBox.Name = "diagramOptionsGroupBox";
			this.diagramOptionsGroupBox.Size = new System.Drawing.Size(340, 76);
			this.diagramOptionsGroupBox.TabIndex = 8;
			this.diagramOptionsGroupBox.TabStop = false;
			this.diagramOptionsGroupBox.Text = "New Diagram Options";
			// 
			// addSourceElementCheckBox
			// 
			this.addSourceElementCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.addSourceElementCheckBox.Location = new System.Drawing.Point(6, 45);
			this.addSourceElementCheckBox.Name = "addSourceElementCheckBox";
			this.addSourceElementCheckBox.Size = new System.Drawing.Size(143, 24);
			this.addSourceElementCheckBox.TabIndex = 1;
			this.addSourceElementCheckBox.Text = "Add Source Elements";
			this.addSourceElementCheckBox.UseVisualStyleBackColor = true;
			// 
			// addDataTypesCheckBox
			// 
			this.addDataTypesCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.addDataTypesCheckBox.Location = new System.Drawing.Point(6, 19);
			this.addDataTypesCheckBox.Name = "addDataTypesCheckBox";
			this.addDataTypesCheckBox.Size = new System.Drawing.Size(104, 24);
			this.addDataTypesCheckBox.TabIndex = 0;
			this.addDataTypesCheckBox.Text = "Add Datatypes";
			this.addDataTypesCheckBox.UseVisualStyleBackColor = true;
			// 
			// copyDatatypesCheckbox
			// 
			this.copyDatatypesCheckbox.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.copyDatatypesCheckbox.Location = new System.Drawing.Point(6, 16);
			this.copyDatatypesCheckbox.Name = "copyDatatypesCheckbox";
			this.copyDatatypesCheckbox.Size = new System.Drawing.Size(162, 24);
			this.copyDatatypesCheckbox.TabIndex = 2;
			this.copyDatatypesCheckbox.Text = "Copy Datatypes to subset";
			this.copyDatatypesCheckbox.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.copyDatatypesCheckbox);
			this.groupBox1.Location = new System.Drawing.Point(12, 247);
			this.groupBox1.MinimumSize = new System.Drawing.Size(0, 45);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(340, 46);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "General Options";
			// 
			// button1
			// 
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(259, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Limit to";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// SettingsWindow
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(364, 411);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.diagramOptionsGroupBox);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.splitContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(366, 450);
			this.Name = "SettingsWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ECDM Message Composer settings";
			((System.ComponentModel.ISupportInitialize)(this.ignoredStereoTypesGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ignoredTaggedValuesGrid)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.diagramOptionsGroupBox.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

        private System.Windows.Forms.CheckBox copyDatatypesCheckbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
    }
}
