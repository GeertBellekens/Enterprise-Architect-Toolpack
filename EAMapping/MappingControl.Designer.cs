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
		private System.Windows.Forms.Button goToSourceButton;
		private System.Windows.Forms.Button goToTargetButton;
		private System.Windows.Forms.Button exportButton;
		private System.Windows.Forms.ColumnHeader sourcePathHeader;
		private System.Windows.Forms.ColumnHeader sourceTypeHeader;
		private System.Windows.Forms.ColumnHeader souceHeader;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MappingControl));
			this.mappingListView = new System.Windows.Forms.ListView();
			this.sourcePathHeader = new System.Windows.Forms.ColumnHeader();
			this.sourceTypeHeader = new System.Windows.Forms.ColumnHeader();
			this.souceHeader = new System.Windows.Forms.ColumnHeader();
			this.logicHeader = new System.Windows.Forms.ColumnHeader();
			this.targetHeader = new System.Windows.Forms.ColumnHeader();
			this.targetTypeHeader = new System.Windows.Forms.ColumnHeader();
			this.targetPathHeader = new System.Windows.Forms.ColumnHeader();
			this.goToSourceButton = new System.Windows.Forms.Button();
			this.goToTargetButton = new System.Windows.Forms.Button();
			this.exportButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// mappingListView
			// 
			this.mappingListView.AllowDrop = true;
			this.mappingListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.mappingListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.sourcePathHeader,
			this.sourceTypeHeader,
			this.souceHeader,
			this.logicHeader,
			this.targetHeader,
			this.targetTypeHeader,
			this.targetPathHeader});
			this.mappingListView.FullRowSelect = true;
			this.mappingListView.GridLines = true;
			this.mappingListView.Location = new System.Drawing.Point(0, 0);
			this.mappingListView.MultiSelect = false;
			this.mappingListView.Name = "mappingListView";
			this.mappingListView.Size = new System.Drawing.Size(955, 450);
			this.mappingListView.TabIndex = 2;
			this.mappingListView.UseCompatibleStateImageBehavior = false;
			this.mappingListView.View = System.Windows.Forms.View.Details;
			this.mappingListView.SelectedIndexChanged += new System.EventHandler(this.MappingListViewSelectedIndexChanged);
			this.mappingListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MappingListViewMouseDoubleClick);
			this.mappingListView.Resize += new System.EventHandler(this.MappingListViewResize);
			// 
			// sourcePathHeader
			// 
			this.sourcePathHeader.Text = "Source Path";
			this.sourcePathHeader.Width = 120;
			// 
			// sourceTypeHeader
			// 
			this.sourceTypeHeader.Text = "Source Type";
			this.sourceTypeHeader.Width = 80;
			// 
			// souceHeader
			// 
			this.souceHeader.Text = "Source";
			this.souceHeader.Width = 200;
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
			this.targetPathHeader.Text = "Target path";
			this.targetPathHeader.Width = 120;
			// 
			// goToSourceButton
			// 
			this.goToSourceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.goToSourceButton.Image = ((System.Drawing.Image)(resources.GetObject("goToSourceButton.Image")));
			this.goToSourceButton.Location = new System.Drawing.Point(4, 457);
			this.goToSourceButton.Name = "goToSourceButton";
			this.goToSourceButton.Size = new System.Drawing.Size(24, 23);
			this.goToSourceButton.TabIndex = 3;
			this.goToSourceButton.UseVisualStyleBackColor = true;
			this.goToSourceButton.Click += new System.EventHandler(this.GoToSourceButtonClick);
			// 
			// goToTargetButton
			// 
			this.goToTargetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.goToTargetButton.Image = ((System.Drawing.Image)(resources.GetObject("goToTargetButton.Image")));
			this.goToTargetButton.Location = new System.Drawing.Point(34, 457);
			this.goToTargetButton.Name = "goToTargetButton";
			this.goToTargetButton.Size = new System.Drawing.Size(24, 23);
			this.goToTargetButton.TabIndex = 4;
			this.goToTargetButton.UseVisualStyleBackColor = true;
			this.goToTargetButton.Click += new System.EventHandler(this.GoToTargetButtonClick);
			// 
			// exportButton
			// 
			this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.exportButton.Location = new System.Drawing.Point(877, 457);
			this.exportButton.Name = "exportButton";
			this.exportButton.Size = new System.Drawing.Size(75, 23);
			this.exportButton.TabIndex = 5;
			this.exportButton.Text = "Export";
			this.exportButton.UseVisualStyleBackColor = true;
			this.exportButton.Click += new System.EventHandler(this.ExportButtonClick);
			// 
			// MappingControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.exportButton);
			this.Controls.Add(this.goToTargetButton);
			this.Controls.Add(this.goToSourceButton);
			this.Controls.Add(this.mappingListView);
			this.Name = "MappingControl";
			this.Size = new System.Drawing.Size(955, 486);
			this.ResumeLayout(false);

		}
	}
}
