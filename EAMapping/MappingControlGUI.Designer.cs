using System.Collections.Generic;
using System.Linq;
namespace EAMapping
{
	partial class MappingControlGUI
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private EAMapping.LinkedTreeViews trees;
		
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
			this.trees = new EAMapping.LinkedTreeViews();
			this.SuspendLayout();
			// 
			// trees
			// 
			this.trees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.trees.BackColor = System.Drawing.Color.LightGray;
			this.trees.Location = new System.Drawing.Point(0, 0);
			this.trees.Name = "trees";
			this.trees.Size = new System.Drawing.Size(994, 541);
			this.trees.TabIndex = 0;
			// 
			// MappingControlGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.trees);
			this.Name = "MappingControlGUI";
			this.Size = new System.Drawing.Size(994, 584);
			this.ResumeLayout(false);

		}
	}
}
