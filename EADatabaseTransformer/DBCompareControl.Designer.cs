
namespace EADatabaseTransformer
{
	partial class DBCompareControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListView originalDBListView;
		private System.Windows.Forms.ColumnHeader typeColumn;
		private System.Windows.Forms.ColumnHeader propertiesColumn;
		private System.Windows.Forms.ListView newDBListView;
		private System.Windows.Forms.ColumnHeader newTableColumn;
		private System.Windows.Forms.ColumnHeader newItemTypeColum;
		private System.Windows.Forms.ColumnHeader newPropertiesColumn;
		private System.Windows.Forms.ColumnHeader tableHeader;
		private System.Windows.Forms.ColumnHeader nameHeader;
		private System.Windows.Forms.ColumnHeader nameColumn;
		
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.newDBListView = new System.Windows.Forms.ListView();
			this.newTableColumn = new System.Windows.Forms.ColumnHeader();
			this.newItemTypeColum = new System.Windows.Forms.ColumnHeader();
			this.newPropertiesColumn = new System.Windows.Forms.ColumnHeader();
			this.originalDBListView = new System.Windows.Forms.ListView();
			this.tableHeader = new System.Windows.Forms.ColumnHeader();
			this.typeColumn = new System.Windows.Forms.ColumnHeader();
			this.nameHeader = new System.Windows.Forms.ColumnHeader();
			this.propertiesColumn = new System.Windows.Forms.ColumnHeader();
			this.nameColumn = new System.Windows.Forms.ColumnHeader();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.newDBListView);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.originalDBListView);
			this.splitContainer1.Size = new System.Drawing.Size(902, 354);
			this.splitContainer1.SplitterDistance = 431;
			this.splitContainer1.TabIndex = 0;
			// 
			// newDBListView
			// 
			this.newDBListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.newTableColumn,
			this.newItemTypeColum,
			this.nameColumn,
			this.newPropertiesColumn});
			this.newDBListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.newDBListView.FullRowSelect = true;
			this.newDBListView.GridLines = true;
			this.newDBListView.Location = new System.Drawing.Point(0, 0);
			this.newDBListView.Name = "newDBListView";
			this.newDBListView.Size = new System.Drawing.Size(431, 354);
			this.newDBListView.TabIndex = 1;
			this.newDBListView.UseCompatibleStateImageBehavior = false;
			this.newDBListView.View = System.Windows.Forms.View.Details;
			this.newDBListView.Resize += new System.EventHandler(this.NewDBListViewResize);
			// 
			// newTableColumn
			// 
			this.newTableColumn.Text = "Table";
			this.newTableColumn.Width = 80;
			// 
			// newItemTypeColum
			// 
			this.newItemTypeColum.Text = "Item Type";
			this.newItemTypeColum.Width = 80;
			// 
			// newPropertiesColumn
			// 
			this.newPropertiesColumn.Text = "Properties";
			// 
			// originalDBListView
			// 
			this.originalDBListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.tableHeader,
			this.typeColumn,
			this.nameHeader,
			this.propertiesColumn});
			this.originalDBListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.originalDBListView.FullRowSelect = true;
			this.originalDBListView.GridLines = true;
			this.originalDBListView.Location = new System.Drawing.Point(0, 0);
			this.originalDBListView.Name = "originalDBListView";
			this.originalDBListView.Size = new System.Drawing.Size(467, 354);
			this.originalDBListView.TabIndex = 0;
			this.originalDBListView.UseCompatibleStateImageBehavior = false;
			this.originalDBListView.View = System.Windows.Forms.View.Details;
			this.originalDBListView.Resize += new System.EventHandler(this.OriginalDBListViewResize);
			// 
			// tableHeader
			// 
			this.tableHeader.Text = "Table";
			this.tableHeader.Width = 80;
			// 
			// typeColumn
			// 
			this.typeColumn.Text = "Item Type";
			this.typeColumn.Width = 80;
			// 
			// nameHeader
			// 
			this.nameHeader.Text = "Name";
			this.nameHeader.Width = 120;
			// 
			// propertiesColumn
			// 
			this.propertiesColumn.Text = "Properties";
			// 
			// nameColumn
			// 
			this.nameColumn.Text = "Name";
			this.nameColumn.Width = 120;
			// 
			// DBCompareControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "DBCompareControl";
			this.Size = new System.Drawing.Size(902, 423);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
