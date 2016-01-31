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
			this.deleteStereotypeButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.applyButton = new System.Windows.Forms.Button();
			this.ignoredTaggedValuesGrid = new System.Windows.Forms.DataGridView();
			this.deleteTaggedValueButton = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.StereotypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.ignoredStereoTypesGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ignoredTaggedValuesGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
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
			this.ignoredStereoTypesGrid.Size = new System.Drawing.Size(154, 150);
			this.ignoredStereoTypesGrid.TabIndex = 0;
			// 
			// deleteStereotypeButton
			// 
			this.deleteStereotypeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteStereotypeButton.Location = new System.Drawing.Point(81, 159);
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
			this.okButton.Location = new System.Drawing.Point(101, 222);
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
			this.cancelButton.Location = new System.Drawing.Point(182, 222);
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
			this.applyButton.Location = new System.Drawing.Point(263, 222);
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
			this.ignoredTaggedValuesGrid.Size = new System.Drawing.Size(154, 150);
			this.ignoredTaggedValuesGrid.TabIndex = 5;
			// 
			// deleteTaggedValueButton
			// 
			this.deleteTaggedValueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteTaggedValueButton.Location = new System.Drawing.Point(82, 159);
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
			this.splitContainer1.Location = new System.Drawing.Point(12, 12);
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
			this.splitContainer1.Size = new System.Drawing.Size(326, 204);
			this.splitContainer1.SplitterDistance = 159;
			this.splitContainer1.TabIndex = 7;
			// 
			// StereotypeColumn
			// 
			this.StereotypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.StereotypeColumn.HeaderText = "Ignored Stereotypes";
			this.StereotypeColumn.Name = "StereotypeColumn";
			this.StereotypeColumn.ToolTipText = "Classes with these stereotypes will not be deleted when updating a subset model";
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn1.HeaderText = "Ignored Tagged Values";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ToolTipText = "These tagged values will be left untouched when they have a value in the subset m" +
	"odel";
			// 
			// SettingsWindow
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(350, 257);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.splitContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(366, 295);
			this.Name = "SettingsWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ECDM Message Composer settings";
			((System.ComponentModel.ISupportInitialize)(this.ignoredStereoTypesGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ignoredTaggedValuesGrid)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
