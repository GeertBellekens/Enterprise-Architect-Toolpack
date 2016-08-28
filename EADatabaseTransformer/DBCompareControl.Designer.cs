
namespace EADatabaseTransformer
{
	partial class DBCompareControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListView compareDBListView;
		private System.Windows.Forms.ColumnHeader tableColumn;
		private System.Windows.Forms.ColumnHeader itemTypeColum;
		private System.Windows.Forms.ColumnHeader newPropertiesColumn;
		private System.Windows.Forms.ColumnHeader newNameColumn;
		private System.Windows.Forms.ColumnHeader compareStatusColumn;
		private System.Windows.Forms.ColumnHeader existingNameColumn;
		private System.Windows.Forms.ColumnHeader existingPropertiesColumn;
		
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
			this.compareDBListView = new System.Windows.Forms.ListView();
			this.compareStatusColumn = new System.Windows.Forms.ColumnHeader();
			this.itemTypeColum = new System.Windows.Forms.ColumnHeader();
			this.tableColumn = new System.Windows.Forms.ColumnHeader();
			this.newNameColumn = new System.Windows.Forms.ColumnHeader();
			this.newPropertiesColumn = new System.Windows.Forms.ColumnHeader();
			this.existingNameColumn = new System.Windows.Forms.ColumnHeader();
			this.existingPropertiesColumn = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// compareDBListView
			// 
			this.compareDBListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.compareDBListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.compareStatusColumn,
			this.itemTypeColum,
			this.tableColumn,
			this.newNameColumn,
			this.newPropertiesColumn,
			this.existingNameColumn,
			this.existingPropertiesColumn});
			this.compareDBListView.FullRowSelect = true;
			this.compareDBListView.GridLines = true;
			this.compareDBListView.Location = new System.Drawing.Point(0, 0);
			this.compareDBListView.Name = "compareDBListView";
			this.compareDBListView.Size = new System.Drawing.Size(902, 354);
			this.compareDBListView.TabIndex = 1;
			this.compareDBListView.UseCompatibleStateImageBehavior = false;
			this.compareDBListView.View = System.Windows.Forms.View.Details;
			this.compareDBListView.Resize += new System.EventHandler(this.CompareListViewResize);
			// 
			// compareStatusColumn
			// 
			this.compareStatusColumn.Text = "Status";
			// 
			// itemTypeColum
			// 
			this.itemTypeColum.Text = "Item Type";
			this.itemTypeColum.Width = 80;
			// 
			// tableColumn
			// 
			this.tableColumn.Text = "Table";
			this.tableColumn.Width = 80;
			// 
			// newNameColumn
			// 
			this.newNameColumn.Text = "New Name";
			this.newNameColumn.Width = 120;
			// 
			// newPropertiesColumn
			// 
			this.newPropertiesColumn.Text = "New Properties";
			this.newPropertiesColumn.Width = 190;
			// 
			// existingNameColumn
			// 
			this.existingNameColumn.Text = "Existing Name";
			this.existingNameColumn.Width = 123;
			// 
			// existingPropertiesColumn
			// 
			this.existingPropertiesColumn.Text = "Existing Properties";
			this.existingPropertiesColumn.Width = 193;
			// 
			// DBCompareControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.compareDBListView);
			this.Name = "DBCompareControl";
			this.Size = new System.Drawing.Size(902, 420);
			this.ResumeLayout(false);

		}
	}
}
