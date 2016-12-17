
namespace EATFSConnector
{
	partial class SelectImportTypesForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ComboBox TFSTypeComboBox;
		private System.Windows.Forms.Label TFSTypeLabel;
		private System.Windows.Forms.ComboBox SparxTypesComboBox;
		private System.Windows.Forms.Button importButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label SparxTypeLabel;
		private System.Windows.Forms.CheckBox allTypesCheckBox;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectImportTypesForm));
			this.TFSTypeComboBox = new System.Windows.Forms.ComboBox();
			this.TFSTypeLabel = new System.Windows.Forms.Label();
			this.SparxTypesComboBox = new System.Windows.Forms.ComboBox();
			this.importButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SparxTypeLabel = new System.Windows.Forms.Label();
			this.allTypesCheckBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// TFSTypeComboBox
			// 
			this.TFSTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.TFSTypeComboBox.FormattingEnabled = true;
			this.TFSTypeComboBox.Location = new System.Drawing.Point(12, 35);
			this.TFSTypeComboBox.Name = "TFSTypeComboBox";
			this.TFSTypeComboBox.Size = new System.Drawing.Size(188, 21);
			this.TFSTypeComboBox.TabIndex = 0;
			this.TFSTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.TFSTypeComboBoxSelectedIndexChanged);
			// 
			// TFSTypeLabel
			// 
			this.TFSTypeLabel.Location = new System.Drawing.Point(12, 14);
			this.TFSTypeLabel.Name = "TFSTypeLabel";
			this.TFSTypeLabel.Size = new System.Drawing.Size(120, 18);
			this.TFSTypeLabel.TabIndex = 1;
			this.TFSTypeLabel.Text = "TFS Workitem Type";
			this.TFSTypeLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// SparxTypesComboBox
			// 
			this.SparxTypesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.SparxTypesComboBox.FormattingEnabled = true;
			this.SparxTypesComboBox.Location = new System.Drawing.Point(12, 89);
			this.SparxTypesComboBox.Name = "SparxTypesComboBox";
			this.SparxTypesComboBox.Size = new System.Drawing.Size(188, 21);
			this.SparxTypesComboBox.TabIndex = 2;
			// 
			// importButton
			// 
			this.importButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.importButton.Location = new System.Drawing.Point(89, 118);
			this.importButton.Name = "importButton";
			this.importButton.Size = new System.Drawing.Size(75, 23);
			this.importButton.TabIndex = 4;
			this.importButton.Text = "Import";
			this.importButton.UseVisualStyleBackColor = true;
			this.importButton.Click += new System.EventHandler(this.ImportButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(170, 118);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
			// 
			// SparxTypeLabel
			// 
			this.SparxTypeLabel.Location = new System.Drawing.Point(12, 68);
			this.SparxTypeLabel.Name = "SparxTypeLabel";
			this.SparxTypeLabel.Size = new System.Drawing.Size(120, 18);
			this.SparxTypeLabel.TabIndex = 6;
			this.SparxTypeLabel.Text = "Sparx Element Type";
			this.SparxTypeLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// allTypesCheckBox
			// 
			this.allTypesCheckBox.Location = new System.Drawing.Point(206, 32);
			this.allTypesCheckBox.Name = "allTypesCheckBox";
			this.allTypesCheckBox.Size = new System.Drawing.Size(39, 24);
			this.allTypesCheckBox.TabIndex = 7;
			this.allTypesCheckBox.Text = "All";
			this.allTypesCheckBox.UseVisualStyleBackColor = true;
			this.allTypesCheckBox.CheckedChanged += new System.EventHandler(this.AllTypesCheckBoxCheckedChanged);
			// 
			// SelectImportTypesForm
			// 
			this.AcceptButton = this.importButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(257, 153);
			this.Controls.Add(this.allTypesCheckBox);
			this.Controls.Add(this.SparxTypeLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.importButton);
			this.Controls.Add(this.SparxTypesComboBox);
			this.Controls.Add(this.TFSTypeLabel);
			this.Controls.Add(this.TFSTypeComboBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(256, 192);
			this.Name = "SelectImportTypesForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Import Types";
			this.ResumeLayout(false);

		}
	}
}
