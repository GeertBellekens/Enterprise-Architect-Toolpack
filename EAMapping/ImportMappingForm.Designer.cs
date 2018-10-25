using System.Collections.Generic;
using System.Linq;
namespace EAMapping
{
	partial class ImportMappingForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button browseMappingFileButton;
		private System.Windows.Forms.TextBox importFileTextBox;
		private System.Windows.Forms.Label importFileLabel;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button importButton;
		private System.Windows.Forms.TextBox statusTextBox;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.Button sourcePathBrowseButton;
		private System.Windows.Forms.TextBox sourcePathTextBox;
		private System.Windows.Forms.Label sourcePathLabel;
		private System.Windows.Forms.Button targetPathBrowseButton;
		private System.Windows.Forms.TextBox targetPathTextBox;
		private System.Windows.Forms.Label targetPathLabel;
		
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportMappingForm));
            this.browseMappingFileButton = new System.Windows.Forms.Button();
            this.importFileTextBox = new System.Windows.Forms.TextBox();
            this.importFileLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.sourcePathBrowseButton = new System.Windows.Forms.Button();
            this.sourcePathTextBox = new System.Windows.Forms.TextBox();
            this.sourcePathLabel = new System.Windows.Forms.Label();
            this.targetPathBrowseButton = new System.Windows.Forms.Button();
            this.targetPathTextBox = new System.Windows.Forms.TextBox();
            this.targetPathLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // browseMappingFileButton
            // 
            this.browseMappingFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseMappingFileButton.Location = new System.Drawing.Point(339, 24);
            this.browseMappingFileButton.Name = "browseMappingFileButton";
            this.browseMappingFileButton.Size = new System.Drawing.Size(24, 20);
            this.browseMappingFileButton.TabIndex = 12;
            this.browseMappingFileButton.Text = "...";
            this.browseMappingFileButton.UseVisualStyleBackColor = true;
            this.browseMappingFileButton.Click += new System.EventHandler(this.BrowseMappingFileButtonClick);
            // 
            // importFileTextBox
            // 
            this.importFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importFileTextBox.Location = new System.Drawing.Point(12, 25);
            this.importFileTextBox.MinimumSize = new System.Drawing.Size(153, 20);
            this.importFileTextBox.Name = "importFileTextBox";
            this.importFileTextBox.Size = new System.Drawing.Size(321, 20);
            this.importFileTextBox.TabIndex = 11;
            this.importFileTextBox.TextChanged += new System.EventHandler(this.ImportFileTextBoxTextChanged);
            // 
            // importFileLabel
            // 
            this.importFileLabel.Location = new System.Drawing.Point(12, 9);
            this.importFileLabel.Name = "importFileLabel";
            this.importFileLabel.Size = new System.Drawing.Size(141, 23);
            this.importFileLabel.TabIndex = 13;
            this.importFileLabel.Text = "Mapping file";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(284, 190);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // importButton
            // 
            this.importButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.importButton.Location = new System.Drawing.Point(203, 190);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(75, 23);
            this.importButton.TabIndex = 14;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.ImportButtonClick);
            // 
            // statusTextBox
            // 
            this.statusTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusTextBox.Location = new System.Drawing.Point(14, 155);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.ReadOnly = true;
            this.statusTextBox.Size = new System.Drawing.Size(345, 20);
            this.statusTextBox.TabIndex = 16;
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.Location = new System.Drawing.Point(14, 138);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(100, 23);
            this.statusLabel.TabIndex = 17;
            this.statusLabel.Text = "Status";
            // 
            // sourcePathBrowseButton
            // 
            this.sourcePathBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sourcePathBrowseButton.Location = new System.Drawing.Point(339, 67);
            this.sourcePathBrowseButton.Name = "sourcePathBrowseButton";
            this.sourcePathBrowseButton.Size = new System.Drawing.Size(24, 20);
            this.sourcePathBrowseButton.TabIndex = 19;
            this.sourcePathBrowseButton.Text = "...";
            this.sourcePathBrowseButton.UseVisualStyleBackColor = true;
            this.sourcePathBrowseButton.Click += new System.EventHandler(this.SourcePathBrowseButtonClick);
            // 
            // sourcePathTextBox
            // 
            this.sourcePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourcePathTextBox.Location = new System.Drawing.Point(12, 68);
            this.sourcePathTextBox.MinimumSize = new System.Drawing.Size(153, 20);
            this.sourcePathTextBox.Name = "sourcePathTextBox";
            this.sourcePathTextBox.ReadOnly = true;
            this.sourcePathTextBox.Size = new System.Drawing.Size(321, 20);
            this.sourcePathTextBox.TabIndex = 18;
            // 
            // sourcePathLabel
            // 
            this.sourcePathLabel.Location = new System.Drawing.Point(12, 52);
            this.sourcePathLabel.Name = "sourcePathLabel";
            this.sourcePathLabel.Size = new System.Drawing.Size(141, 23);
            this.sourcePathLabel.TabIndex = 20;
            this.sourcePathLabel.Text = "Source";
            // 
            // targetPathBrowseButton
            // 
            this.targetPathBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.targetPathBrowseButton.Location = new System.Drawing.Point(339, 109);
            this.targetPathBrowseButton.Name = "targetPathBrowseButton";
            this.targetPathBrowseButton.Size = new System.Drawing.Size(24, 20);
            this.targetPathBrowseButton.TabIndex = 22;
            this.targetPathBrowseButton.Text = "...";
            this.targetPathBrowseButton.UseVisualStyleBackColor = true;
            this.targetPathBrowseButton.Click += new System.EventHandler(this.TargetPathBrowseButtonClick);
            // 
            // targetPathTextBox
            // 
            this.targetPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetPathTextBox.Location = new System.Drawing.Point(12, 110);
            this.targetPathTextBox.MinimumSize = new System.Drawing.Size(153, 20);
            this.targetPathTextBox.Name = "targetPathTextBox";
            this.targetPathTextBox.ReadOnly = true;
            this.targetPathTextBox.Size = new System.Drawing.Size(321, 20);
            this.targetPathTextBox.TabIndex = 21;
            // 
            // targetPathLabel
            // 
            this.targetPathLabel.Location = new System.Drawing.Point(12, 94);
            this.targetPathLabel.Name = "targetPathLabel";
            this.targetPathLabel.Size = new System.Drawing.Size(141, 23);
            this.targetPathLabel.TabIndex = 23;
            this.targetPathLabel.Text = "Target";
            // 
            // ImportMappingForm
            // 
            this.AcceptButton = this.importButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(375, 225);
            this.Controls.Add(this.targetPathBrowseButton);
            this.Controls.Add(this.targetPathTextBox);
            this.Controls.Add(this.targetPathLabel);
            this.Controls.Add(this.sourcePathBrowseButton);
            this.Controls.Add(this.sourcePathTextBox);
            this.Controls.Add(this.sourcePathLabel);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.browseMappingFileButton);
            this.Controls.Add(this.importFileTextBox);
            this.Controls.Add(this.importFileLabel);
            this.Controls.Add(this.statusLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(391, 264);
            this.Name = "ImportMappingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Mapping";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
