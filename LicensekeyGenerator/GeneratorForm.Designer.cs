/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 5/12/2014
 * Time: 4:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace LicensekeyGenerator
{
	partial class GeneratorForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
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
			this.keyTextBox = new System.Windows.Forms.TextBox();
			this.clientTextBox = new System.Windows.Forms.TextBox();
			this.clientLabel = new System.Windows.Forms.Label();
			this.closeButton = new System.Windows.Forms.Button();
			this.floatingCheckbox = new System.Windows.Forms.CheckBox();
			this.validateButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// keyTextBox
			// 
			this.keyTextBox.Location = new System.Drawing.Point(12, 35);
			this.keyTextBox.Multiline = true;
			this.keyTextBox.Name = "keyTextBox";
			this.keyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.keyTextBox.Size = new System.Drawing.Size(694, 88);
			this.keyTextBox.TabIndex = 0;
			this.keyTextBox.WordWrap = false;
			// 
			// clientTextBox
			// 
			this.clientTextBox.Location = new System.Drawing.Point(81, 9);
			this.clientTextBox.Name = "clientTextBox";
			this.clientTextBox.Size = new System.Drawing.Size(205, 20);
			this.clientTextBox.TabIndex = 5;
			// 
			// clientLabel
			// 
			this.clientLabel.Location = new System.Drawing.Point(12, 9);
			this.clientLabel.Name = "clientLabel";
			this.clientLabel.Size = new System.Drawing.Size(63, 23);
			this.clientLabel.TabIndex = 6;
			this.clientLabel.Text = "Client";
			// 
			// closeButton
			// 
			this.closeButton.Location = new System.Drawing.Point(631, 144);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 10;
			this.closeButton.Text = "Close";
			this.closeButton.UseVisualStyleBackColor = true;
			// 
			// floatingCheckbox
			// 
			this.floatingCheckbox.Location = new System.Drawing.Point(302, 4);
			this.floatingCheckbox.Name = "floatingCheckbox";
			this.floatingCheckbox.Size = new System.Drawing.Size(106, 24);
			this.floatingCheckbox.TabIndex = 16;
			this.floatingCheckbox.Text = "Floating";
			this.floatingCheckbox.UseVisualStyleBackColor = true;
			// 
			// validateButton
			// 
			this.validateButton.Location = new System.Drawing.Point(550, 144);
			this.validateButton.Name = "validateButton";
			this.validateButton.Size = new System.Drawing.Size(75, 23);
			this.validateButton.TabIndex = 18;
			this.validateButton.Text = "Validate";
			this.validateButton.UseVisualStyleBackColor = true;
			this.validateButton.Click += new System.EventHandler(this.ValidateButtonClick);
			// 
			// GeneratorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(715, 208);
			this.Controls.Add(this.validateButton);
			this.Controls.Add(this.floatingCheckbox);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.clientLabel);
			this.Controls.Add(this.clientTextBox);
			this.Controls.Add(this.keyTextBox);
			this.Name = "GeneratorForm";
			this.Text = "LicensekeyGenerator";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button validateButton;
		private System.Windows.Forms.CheckBox floatingCheckbox;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Label clientLabel;
		private System.Windows.Forms.TextBox clientTextBox;
		private System.Windows.Forms.TextBox keyTextBox;
	}
}
