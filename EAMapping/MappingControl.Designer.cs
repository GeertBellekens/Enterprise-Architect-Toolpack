using System.Collections.Generic;
using System.Linq;
namespace EAMapping
{
	partial class MappingControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListView mappingListView;
		private System.Windows.Forms.ColumnHeader sourceHeader;
		private System.Windows.Forms.ColumnHeader sourceTypeHeader;
		private System.Windows.Forms.ColumnHeader sourcePathHeader;
		private System.Windows.Forms.ColumnHeader logicHeader;
		private System.Windows.Forms.ColumnHeader targetHeader;
		private System.Windows.Forms.ColumnHeader targetTypeHeader;
		private System.Windows.Forms.ColumnHeader targetPathHeader;
		
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
			this.mappingListView = new System.Windows.Forms.ListView();
			this.sourceHeader = new System.Windows.Forms.ColumnHeader();
			this.sourceTypeHeader = new System.Windows.Forms.ColumnHeader();
			this.sourcePathHeader = new System.Windows.Forms.ColumnHeader();
			this.logicHeader = new System.Windows.Forms.ColumnHeader();
			this.targetHeader = new System.Windows.Forms.ColumnHeader();
			this.targetTypeHeader = new System.Windows.Forms.ColumnHeader();
			this.targetPathHeader = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// mappingListView
			// 
			this.mappingListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.mappingListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.sourceHeader,
			this.sourceTypeHeader,
			this.sourcePathHeader,
			this.logicHeader,
			this.targetHeader,
			this.targetTypeHeader,
			this.targetPathHeader});
			this.mappingListView.FullRowSelect = true;
			this.mappingListView.GridLines = true;
			this.mappingListView.Location = new System.Drawing.Point(0, 0);
			this.mappingListView.MultiSelect = false;
			this.mappingListView.Name = "mappingListView";
			this.mappingListView.Size = new System.Drawing.Size(955, 483);
			this.mappingListView.TabIndex = 2;
			this.mappingListView.UseCompatibleStateImageBehavior = false;
			this.mappingListView.View = System.Windows.Forms.View.Details;
			this.mappingListView.Resize += new System.EventHandler(this.MappingListViewResize);
			// 
			// sourceHeader
			// 
			this.sourceHeader.Text = "Source";
			this.sourceHeader.Width = 200;
			// 
			// sourceTypeHeader
			// 
			this.sourceTypeHeader.Text = "Source Type";
			this.sourceTypeHeader.Width = 80;
			// 
			// sourcePathHeader
			// 
			this.sourcePathHeader.Text = "Source Path";
			this.sourcePathHeader.Width = 80;
			// 
			// logicHeader
			// 
			this.logicHeader.Text = "Mapping Logic";
			this.logicHeader.Width = 200;
			// 
			// targetHeader
			// 
			this.targetHeader.Text = "Target";
			this.targetHeader.Width = 200;
			// 
			// targetTypeHeader
			// 
			this.targetTypeHeader.Text = "Target Type";
			this.targetTypeHeader.Width = 80;
			// 
			// targetPathHeader
			// 
			this.targetPathHeader.Text = "Target Path";
			this.targetPathHeader.Width = 80;
			// 
			// MappingControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mappingListView);
			this.Name = "MappingControl";
			this.Size = new System.Drawing.Size(955, 486);
			this.ResumeLayout(false);

		}
	}
}
