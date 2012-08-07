/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 4/08/2012
 * Time: 7:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace TSF.UmlToolingFramework.EANavigator
{
	partial class FQNInputForm
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
			this.fqnTextBox = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// fqnTextBox
			// 
			this.fqnTextBox.Location = new System.Drawing.Point(13, 13);
			this.fqnTextBox.Name = "fqnTextBox";
			this.fqnTextBox.Size = new System.Drawing.Size(309, 20);
			this.fqnTextBox.TabIndex = 0;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(337, 10);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// FQNInputForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(424, 46);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.fqnTextBox);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FQNInputForm";
			this.Text = "Fill in the Fully Qualified Name";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.TextBox fqnTextBox;
	}
}
