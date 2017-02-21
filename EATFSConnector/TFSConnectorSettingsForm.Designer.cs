
namespace EATFSConnector
{
	partial class TFSConnectorSettingsForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label TFSUrlLabel;
		private System.Windows.Forms.TextBox tfsUrlTextBox;
		private System.Windows.Forms.GroupBox workitemMappingBox;
		private System.Windows.Forms.Button deleteMappingButton;
		private System.Windows.Forms.DataGridView workitemMappingsgrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn EAType;
		private System.Windows.Forms.DataGridViewTextBoxColumn TFSType;
		private System.Windows.Forms.TextBox defaultUserTextBox;
		private System.Windows.Forms.Label defaultUserLabel;
		private System.Windows.Forms.TextBox defaultWorkitemTypeTextBox;
		private System.Windows.Forms.Label defaultWorkitemLabel;
		private System.Windows.Forms.TextBox defaultStatusTextBox;
		private System.Windows.Forms.Label defaultStatusLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox defaultProjectTextBox;
		private System.Windows.Forms.Label defaultProjectLabel;
		private System.Windows.Forms.TextBox tfsFilterTagTextBox;
		private System.Windows.Forms.Label tfsFilterTagLabel;
		private System.Windows.Forms.TextBox defaultCollectionTextBox;
		private System.Windows.Forms.Label defaultCollectionLabel;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TFSConnectorSettingsForm));
			this.TFSUrlLabel = new System.Windows.Forms.Label();
			this.tfsUrlTextBox = new System.Windows.Forms.TextBox();
			this.workitemMappingBox = new System.Windows.Forms.GroupBox();
			this.deleteMappingButton = new System.Windows.Forms.Button();
			this.workitemMappingsgrid = new System.Windows.Forms.DataGridView();
			this.EAType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TFSType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.defaultUserTextBox = new System.Windows.Forms.TextBox();
			this.defaultUserLabel = new System.Windows.Forms.Label();
			this.defaultWorkitemTypeTextBox = new System.Windows.Forms.TextBox();
			this.defaultWorkitemLabel = new System.Windows.Forms.Label();
			this.defaultStatusTextBox = new System.Windows.Forms.TextBox();
			this.defaultStatusLabel = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.defaultProjectTextBox = new System.Windows.Forms.TextBox();
			this.defaultProjectLabel = new System.Windows.Forms.Label();
			this.tfsFilterTagTextBox = new System.Windows.Forms.TextBox();
			this.tfsFilterTagLabel = new System.Windows.Forms.Label();
			this.defaultCollectionTextBox = new System.Windows.Forms.TextBox();
			this.defaultCollectionLabel = new System.Windows.Forms.Label();
			this.workitemMappingBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.workitemMappingsgrid)).BeginInit();
			this.SuspendLayout();
			// 
			// TFSUrlLabel
			// 
			this.TFSUrlLabel.Location = new System.Drawing.Point(12, 9);
			this.TFSUrlLabel.Name = "TFSUrlLabel";
			this.TFSUrlLabel.Size = new System.Drawing.Size(100, 23);
			this.TFSUrlLabel.TabIndex = 0;
			this.TFSUrlLabel.Text = "TFS URL";
			// 
			// tfsUrlTextBox
			// 
			this.tfsUrlTextBox.Location = new System.Drawing.Point(12, 24);
			this.tfsUrlTextBox.Name = "tfsUrlTextBox";
			this.tfsUrlTextBox.Size = new System.Drawing.Size(185, 20);
			this.tfsUrlTextBox.TabIndex = 1;
			// 
			// workitemMappingBox
			// 
			this.workitemMappingBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.workitemMappingBox.Controls.Add(this.deleteMappingButton);
			this.workitemMappingBox.Controls.Add(this.workitemMappingsgrid);
			this.workitemMappingBox.Location = new System.Drawing.Point(239, 12);
			this.workitemMappingBox.Name = "workitemMappingBox";
			this.workitemMappingBox.Size = new System.Drawing.Size(325, 247);
			this.workitemMappingBox.TabIndex = 2;
			this.workitemMappingBox.TabStop = false;
			this.workitemMappingBox.Text = "Workitem Mappings";
			// 
			// deleteMappingButton
			// 
			this.deleteMappingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.deleteMappingButton.Location = new System.Drawing.Point(244, 218);
			this.deleteMappingButton.Name = "deleteMappingButton";
			this.deleteMappingButton.Size = new System.Drawing.Size(75, 23);
			this.deleteMappingButton.TabIndex = 1;
			this.deleteMappingButton.Text = "Delete";
			this.deleteMappingButton.UseVisualStyleBackColor = true;
			this.deleteMappingButton.Click += new System.EventHandler(this.DeleteMappingButtonClick);
			// 
			// workitemMappingsgrid
			// 
			this.workitemMappingsgrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.workitemMappingsgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.workitemMappingsgrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.EAType,
			this.TFSType});
			this.workitemMappingsgrid.Location = new System.Drawing.Point(6, 19);
			this.workitemMappingsgrid.MultiSelect = false;
			this.workitemMappingsgrid.Name = "workitemMappingsgrid";
			this.workitemMappingsgrid.Size = new System.Drawing.Size(313, 190);
			this.workitemMappingsgrid.TabIndex = 0;
			// 
			// EAType
			// 
			this.EAType.HeaderText = "EA Type/Stereotype";
			this.EAType.Name = "EAType";
			this.EAType.Width = 150;
			// 
			// TFSType
			// 
			this.TFSType.HeaderText = "TFS Type";
			this.TFSType.Name = "TFSType";
			this.TFSType.Width = 120;
			// 
			// defaultUserTextBox
			// 
			this.defaultUserTextBox.Location = new System.Drawing.Point(12, 105);
			this.defaultUserTextBox.Name = "defaultUserTextBox";
			this.defaultUserTextBox.Size = new System.Drawing.Size(185, 20);
			this.defaultUserTextBox.TabIndex = 4;
			// 
			// defaultUserLabel
			// 
			this.defaultUserLabel.Location = new System.Drawing.Point(12, 90);
			this.defaultUserLabel.Name = "defaultUserLabel";
			this.defaultUserLabel.Size = new System.Drawing.Size(100, 23);
			this.defaultUserLabel.TabIndex = 3;
			this.defaultUserLabel.Text = "Default Username";
			// 
			// defaultWorkitemTypeTextBox
			// 
			this.defaultWorkitemTypeTextBox.Location = new System.Drawing.Point(12, 148);
			this.defaultWorkitemTypeTextBox.Name = "defaultWorkitemTypeTextBox";
			this.defaultWorkitemTypeTextBox.Size = new System.Drawing.Size(185, 20);
			this.defaultWorkitemTypeTextBox.TabIndex = 6;
			// 
			// defaultWorkitemLabel
			// 
			this.defaultWorkitemLabel.Location = new System.Drawing.Point(12, 133);
			this.defaultWorkitemLabel.Name = "defaultWorkitemLabel";
			this.defaultWorkitemLabel.Size = new System.Drawing.Size(100, 23);
			this.defaultWorkitemLabel.TabIndex = 5;
			this.defaultWorkitemLabel.Text = "Default Workitem Type";
			// 
			// defaultStatusTextBox
			// 
			this.defaultStatusTextBox.Location = new System.Drawing.Point(12, 191);
			this.defaultStatusTextBox.Name = "defaultStatusTextBox";
			this.defaultStatusTextBox.Size = new System.Drawing.Size(185, 20);
			this.defaultStatusTextBox.TabIndex = 8;
			// 
			// defaultStatusLabel
			// 
			this.defaultStatusLabel.Location = new System.Drawing.Point(12, 176);
			this.defaultStatusLabel.Name = "defaultStatusLabel";
			this.defaultStatusLabel.Size = new System.Drawing.Size(100, 23);
			this.defaultStatusLabel.TabIndex = 7;
			this.defaultStatusLabel.Text = "Default Status";
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(409, 277);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 9;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(490, 277);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 10;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
			// 
			// defaultProjectTextBox
			// 
			this.defaultProjectTextBox.Location = new System.Drawing.Point(12, 236);
			this.defaultProjectTextBox.Name = "defaultProjectTextBox";
			this.defaultProjectTextBox.Size = new System.Drawing.Size(185, 20);
			this.defaultProjectTextBox.TabIndex = 12;
			// 
			// defaultProjectLabel
			// 
			this.defaultProjectLabel.Location = new System.Drawing.Point(12, 221);
			this.defaultProjectLabel.Name = "defaultProjectLabel";
			this.defaultProjectLabel.Size = new System.Drawing.Size(100, 23);
			this.defaultProjectLabel.TabIndex = 11;
			this.defaultProjectLabel.Text = "Default Project";
			// 
			// tfsFilterTagTextBox
			// 
			this.tfsFilterTagTextBox.Location = new System.Drawing.Point(12, 279);
			this.tfsFilterTagTextBox.Name = "tfsFilterTagTextBox";
			this.tfsFilterTagTextBox.Size = new System.Drawing.Size(185, 20);
			this.tfsFilterTagTextBox.TabIndex = 14;
			// 
			// tfsFilterTagLabel
			// 
			this.tfsFilterTagLabel.Location = new System.Drawing.Point(12, 264);
			this.tfsFilterTagLabel.Name = "tfsFilterTagLabel";
			this.tfsFilterTagLabel.Size = new System.Drawing.Size(100, 23);
			this.tfsFilterTagLabel.TabIndex = 13;
			this.tfsFilterTagLabel.Text = "TFS Filter Tag";
			// 
			// defaultCollectionTextBox
			// 
			this.defaultCollectionTextBox.Location = new System.Drawing.Point(12, 62);
			this.defaultCollectionTextBox.Name = "defaultCollectionTextBox";
			this.defaultCollectionTextBox.Size = new System.Drawing.Size(185, 20);
			this.defaultCollectionTextBox.TabIndex = 16;
			// 
			// defaultCollectionLabel
			// 
			this.defaultCollectionLabel.Location = new System.Drawing.Point(12, 47);
			this.defaultCollectionLabel.Name = "defaultCollectionLabel";
			this.defaultCollectionLabel.Size = new System.Drawing.Size(100, 23);
			this.defaultCollectionLabel.TabIndex = 15;
			this.defaultCollectionLabel.Text = "Default Collection";
			// 
			// TFSConnectorSettingsForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(577, 312);
			this.Controls.Add(this.defaultCollectionTextBox);
			this.Controls.Add(this.defaultCollectionLabel);
			this.Controls.Add(this.tfsFilterTagTextBox);
			this.Controls.Add(this.tfsFilterTagLabel);
			this.Controls.Add(this.defaultProjectTextBox);
			this.Controls.Add(this.defaultProjectLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.defaultStatusTextBox);
			this.Controls.Add(this.defaultStatusLabel);
			this.Controls.Add(this.defaultWorkitemTypeTextBox);
			this.Controls.Add(this.defaultWorkitemLabel);
			this.Controls.Add(this.defaultUserTextBox);
			this.Controls.Add(this.defaultUserLabel);
			this.Controls.Add(this.workitemMappingBox);
			this.Controls.Add(this.tfsUrlTextBox);
			this.Controls.Add(this.TFSUrlLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(593, 319);
			this.Name = "TFSConnectorSettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "TFS Connector Settings";
			this.workitemMappingBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.workitemMappingsgrid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
