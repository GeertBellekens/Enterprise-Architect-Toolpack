/*
 * Created by SharpDevelop.
 * User: LaptopGeert
 * Date: 28/07/2016
 * Time: 13:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace EADatabaseTransformer
{
	partial class EADatabaseTransformerSettingsForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label abbreviationsFileLabel;
		private System.Windows.Forms.TextBox abbreviationsFileTextBox;
		private System.Windows.Forms.Button browseAbbreviationsfileButton;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EADatabaseTransformerSettingsForm));
			this.applyButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.abbreviationsFileLabel = new System.Windows.Forms.Label();
			this.abbreviationsFileTextBox = new System.Windows.Forms.TextBox();
			this.browseAbbreviationsfileButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.applyButton.Location = new System.Drawing.Point(232, 85);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(75, 23);
			this.applyButton.TabIndex = 10;
			this.applyButton.Text = "Apply";
			this.applyButton.UseVisualStyleBackColor = true;
			this.applyButton.Click += new System.EventHandler(this.ApplyButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(151, 85);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 9;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(70, 85);
			this.okButton.MinimumSize = new System.Drawing.Size(75, 23);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 8;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// abbreviationsFileLabel
			// 
			this.abbreviationsFileLabel.Location = new System.Drawing.Point(12, 9);
			this.abbreviationsFileLabel.Name = "abbreviationsFileLabel";
			this.abbreviationsFileLabel.Size = new System.Drawing.Size(100, 23);
			this.abbreviationsFileLabel.TabIndex = 11;
			this.abbreviationsFileLabel.Text = "Abbreviations File";
			// 
			// abbreviationsFileTextBox
			// 
			this.abbreviationsFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.abbreviationsFileTextBox.Location = new System.Drawing.Point(12, 33);
			this.abbreviationsFileTextBox.Name = "abbreviationsFileTextBox";
			this.abbreviationsFileTextBox.Size = new System.Drawing.Size(265, 20);
			this.abbreviationsFileTextBox.TabIndex = 12;
			// 
			// browseAbbreviationsfileButton
			// 
			this.browseAbbreviationsfileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.browseAbbreviationsfileButton.Location = new System.Drawing.Point(283, 33);
			this.browseAbbreviationsfileButton.Name = "browseAbbreviationsfileButton";
			this.browseAbbreviationsfileButton.Size = new System.Drawing.Size(24, 20);
			this.browseAbbreviationsfileButton.TabIndex = 13;
			this.browseAbbreviationsfileButton.Text = "...";
			this.browseAbbreviationsfileButton.UseVisualStyleBackColor = true;
			this.browseAbbreviationsfileButton.Click += new System.EventHandler(this.BrowseAbbreviationsfileButtonClick);
			// 
			// EADatabaseTransformerSettingsForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(319, 120);
			this.Controls.Add(this.browseAbbreviationsfileButton);
			this.Controls.Add(this.abbreviationsFileTextBox);
			this.Controls.Add(this.abbreviationsFileLabel);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EADatabaseTransformerSettingsForm";
			this.Text = "Database Transformer Settings";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
