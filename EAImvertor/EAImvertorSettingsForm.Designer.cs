/*
 * Created by SharpDevelop.
 * User: LaptopGeert
 * Date: 9/07/2016
 * Time: 6:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace EAImvertor
{
	partial class EAImvertorSettingsForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label ImvertorURLLabel;
		private System.Windows.Forms.TextBox ImvertorURLTextbox;
		private System.Windows.Forms.TextBox defaultPinTextBox;
		private System.Windows.Forms.Label defaultPinLabel;
		private System.Windows.Forms.Button applyButton;
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
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EAImvertorSettingsForm));
			this.ImvertorURLLabel = new System.Windows.Forms.Label();
			this.ImvertorURLTextbox = new System.Windows.Forms.TextBox();
			this.defaultPinTextBox = new System.Windows.Forms.TextBox();
			this.defaultPinLabel = new System.Windows.Forms.Label();
			this.applyButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ImvertorURLLabel
			// 
			this.ImvertorURLLabel.Location = new System.Drawing.Point(12, 15);
			this.ImvertorURLLabel.Name = "ImvertorURLLabel";
			this.ImvertorURLLabel.Size = new System.Drawing.Size(100, 23);
			this.ImvertorURLLabel.TabIndex = 0;
			this.ImvertorURLLabel.Text = "Imvertor URL";
			// 
			// ImvertorURLTextbox
			// 
			this.ImvertorURLTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.ImvertorURLTextbox.Location = new System.Drawing.Point(121, 12);
			this.ImvertorURLTextbox.Name = "ImvertorURLTextbox";
			this.ImvertorURLTextbox.Size = new System.Drawing.Size(153, 20);
			this.ImvertorURLTextbox.TabIndex = 1;
			// 
			// defaultPinTextBox
			// 
			this.defaultPinTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.defaultPinTextBox.Location = new System.Drawing.Point(121, 36);
			this.defaultPinTextBox.MinimumSize = new System.Drawing.Size(153, 20);
			this.defaultPinTextBox.Name = "defaultPinTextBox";
			this.defaultPinTextBox.Size = new System.Drawing.Size(153, 20);
			this.defaultPinTextBox.TabIndex = 3;
			// 
			// defaultPinLabel
			// 
			this.defaultPinLabel.Location = new System.Drawing.Point(12, 39);
			this.defaultPinLabel.Name = "defaultPinLabel";
			this.defaultPinLabel.Size = new System.Drawing.Size(100, 23);
			this.defaultPinLabel.TabIndex = 2;
			this.defaultPinLabel.Text = "Default PIN";
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.applyButton.Location = new System.Drawing.Point(199, 91);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(75, 23);
			this.applyButton.TabIndex = 7;
			this.applyButton.Text = "Apply";
			this.applyButton.UseVisualStyleBackColor = true;
			this.applyButton.Click += new System.EventHandler(this.ApplyButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(118, 91);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 6;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(37, 91);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 5;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// EAImvertorSettingsForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(283, 126);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.defaultPinTextBox);
			this.Controls.Add(this.defaultPinLabel);
			this.Controls.Add(this.ImvertorURLTextbox);
			this.Controls.Add(this.ImvertorURLLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EAImvertorSettingsForm";
			this.Text = "EA Imvertor settings";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
