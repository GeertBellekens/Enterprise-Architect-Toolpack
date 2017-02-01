
using System.Windows.Forms;

namespace EAMapping
{
	partial class MappingControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TextBox tempTextBox;
		
		/// <summary>
		/// Disposes resources used by the control.
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
			//this.tempTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// tempTextBox
			// 
			/*this.tempTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tempTextBox.Location = new System.Drawing.Point(3, 3);
			this.tempTextBox.Multiline = true;
			this.tempTextBox.Name = "tempTextBox";
			this.tempTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tempTextBox.Size = new System.Drawing.Size(496, 479);
			this.tempTextBox.TabIndex = 0;*/
			// 
			// MappingControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			//this.Controls.Add(this.tempTextBox);
			this.Name = "MappingControl";
			this.Size = new System.Drawing.Size(502, 485);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
        /*
        private void modelSetSource_AfterSelect(System.Object sender, System.Windows.Forms.TreeViewEventArgs e)
        {

            // Vary the response depending on which TreeViewAction
            // triggered the event. 
            switch ((e.Action))
            {
                case TreeViewAction.ByKeyboard:
                    MessageBox.Show("You like the keyboard!");
                    break;
                case TreeViewAction.ByMouse:
                    MessageBox.Show("You like the mouse!");
                    break;
            }
        }*/
    }
}
