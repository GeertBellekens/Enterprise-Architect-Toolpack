
namespace EATFSConnector
{
	partial class SetProjectForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TextBox projectNameTextBox;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;

		
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
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetProjectForm));
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.projectNameTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(202, 63);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(121, 63);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// projectNameTextBox
			// 
			this.projectNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.projectNameTextBox.Location = new System.Drawing.Point(12, 30);
			this.projectNameTextBox.Name = "projectNameTextBox";
			this.projectNameTextBox.Size = new System.Drawing.Size(265, 20);
			this.projectNameTextBox.TabIndex = 9;
			// 
			// SetProjectForm
			// 
			this.AcceptButton = this.okButton;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(289, 98);
			this.Controls.Add(this.projectNameTextBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(305, 137);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(305, 137);
			this.Name = "SetProjectForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Set Project Name";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
