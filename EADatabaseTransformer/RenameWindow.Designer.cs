
namespace EADatabaseTransformer
{
	partial class RenameWindow
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox newNameTextBox;
		private System.Windows.Forms.Label newNameLable;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenameWindow));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.newNameTextBox = new System.Windows.Forms.TextBox();
			this.newNameLable = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(184, 60);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(103, 60);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// newNameTextBox
			// 
			this.newNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.newNameTextBox.Location = new System.Drawing.Point(12, 31);
			this.newNameTextBox.Name = "newNameTextBox";
			this.newNameTextBox.Size = new System.Drawing.Size(247, 20);
			this.newNameTextBox.TabIndex = 2;
			// 
			// newNameLable
			// 
			this.newNameLable.Location = new System.Drawing.Point(12, 9);
			this.newNameLable.Name = "newNameLable";
			this.newNameLable.Size = new System.Drawing.Size(100, 19);
			this.newNameLable.TabIndex = 3;
			this.newNameLable.Text = "New Name";
			// 
			// Rename
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(271, 95);
			this.Controls.Add(this.newNameLable);
			this.Controls.Add(this.newNameTextBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(287, 134);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(287, 134);
			this.Name = "Rename";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "RenameWindow";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
