
namespace EADatabaseTransformer
{
	partial class DBCompareControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListView compareDBListView;
		private System.Windows.Forms.Button saveDatabaseButton;
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.Button toLogicalButton;
		private System.Windows.Forms.Button toDatabaseButton;
		private System.Windows.Forms.ToolTip buttonsTooltip;
		private System.Windows.Forms.Button renameButton;
		private System.Windows.Forms.Button overrideButton;
		private System.Windows.Forms.Button upButton;
		private System.Windows.Forms.Button downButton;

		
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
		/// 
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ColumnHeader compareStatusColumn;
			System.Windows.Forms.ColumnHeader itemTypeColum;
			System.Windows.Forms.ColumnHeader tableColumn;
			System.Windows.Forms.ColumnHeader logicalColumn;
			System.Windows.Forms.ColumnHeader newNameColumn;
			System.Windows.Forms.ColumnHeader newPropertiesColumn;
			System.Windows.Forms.ColumnHeader existingNameColumn;
			System.Windows.Forms.ColumnHeader existingPropertiesColumn;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBCompareControl));
			this.compareDBListView = new System.Windows.Forms.ListView();
			this.saveDatabaseButton = new System.Windows.Forms.Button();
			this.refreshButton = new System.Windows.Forms.Button();
			this.toLogicalButton = new System.Windows.Forms.Button();
			this.toDatabaseButton = new System.Windows.Forms.Button();
			this.buttonsTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.upButton = new System.Windows.Forms.Button();
			this.downButton = new System.Windows.Forms.Button();
			this.renameButton = new System.Windows.Forms.Button();
			this.overrideButton = new System.Windows.Forms.Button();
			compareStatusColumn = new System.Windows.Forms.ColumnHeader();
			itemTypeColum = new System.Windows.Forms.ColumnHeader();
			tableColumn = new System.Windows.Forms.ColumnHeader();
			logicalColumn = new System.Windows.Forms.ColumnHeader();
			newNameColumn = new System.Windows.Forms.ColumnHeader();
			newPropertiesColumn = new System.Windows.Forms.ColumnHeader();
			existingNameColumn = new System.Windows.Forms.ColumnHeader();
			existingPropertiesColumn = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// compareDBListView
			// 
			this.compareDBListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.compareDBListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			compareStatusColumn,
			itemTypeColum,
			tableColumn,
			logicalColumn,
			newNameColumn,
			newPropertiesColumn,
			existingNameColumn,
			existingPropertiesColumn});
			this.compareDBListView.FullRowSelect = true;
			this.compareDBListView.GridLines = true;
			this.compareDBListView.Location = new System.Drawing.Point(0, 0);
			this.compareDBListView.MultiSelect = false;
			this.compareDBListView.Name = "compareDBListView";
			this.compareDBListView.Size = new System.Drawing.Size(902, 379);
			this.compareDBListView.TabIndex = 1;
			this.compareDBListView.UseCompatibleStateImageBehavior = false;
			this.compareDBListView.View = System.Windows.Forms.View.Details;
			this.compareDBListView.SelectedIndexChanged += new System.EventHandler(this.CompareDBListViewSelectedIndexChanged);
			this.compareDBListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.CompareDBListViewMouseDoubleClick);
			this.compareDBListView.Resize += new System.EventHandler(this.CompareListViewResize);
			// 
			// compareStatusColumn
			// 
			compareStatusColumn.Text = "Status";
			// 
			// itemTypeColum
			// 
			itemTypeColum.Text = "Item Type";
			itemTypeColum.Width = 80;
			// 
			// tableColumn
			// 
			tableColumn.Text = "Table";
			tableColumn.Width = 80;
			// 
			// logicalColumn
			// 
			logicalColumn.Text = "Logical Name";
			logicalColumn.Width = 150;
			// 
			// newNameColumn
			// 
			newNameColumn.Text = "New Name";
			newNameColumn.Width = 160;
			// 
			// newPropertiesColumn
			// 
			newPropertiesColumn.Text = "New Properties";
			newPropertiesColumn.Width = 190;
			// 
			// existingNameColumn
			// 
			existingNameColumn.Text = "Existing Name";
			existingNameColumn.Width = 160;
			// 
			// existingPropertiesColumn
			// 
			existingPropertiesColumn.Text = "Existing Properties";
			existingPropertiesColumn.Width = 193;
			// 
			// saveDatabaseButton
			// 
			this.saveDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.saveDatabaseButton.Location = new System.Drawing.Point(846, 385);
			this.saveDatabaseButton.Name = "saveDatabaseButton";
			this.saveDatabaseButton.Size = new System.Drawing.Size(53, 23);
			this.saveDatabaseButton.TabIndex = 2;
			this.saveDatabaseButton.Text = "Save";
			this.buttonsTooltip.SetToolTip(this.saveDatabaseButton, "Save changes to the Database");
			this.saveDatabaseButton.UseVisualStyleBackColor = true;
			this.saveDatabaseButton.Click += new System.EventHandler(this.SaveDatabaseButtonClick);
			// 
			// refreshButton
			// 
			this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
			this.refreshButton.Location = new System.Drawing.Point(3, 385);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(25, 23);
			this.refreshButton.TabIndex = 13;
			this.buttonsTooltip.SetToolTip(this.refreshButton, "Refresh");
			this.refreshButton.UseVisualStyleBackColor = true;
			this.refreshButton.Click += new System.EventHandler(this.RefreshButtonClick);
			// 
			// toLogicalButton
			// 
			this.toLogicalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.toLogicalButton.Image = ((System.Drawing.Image)(resources.GetObject("toLogicalButton.Image")));
			this.toLogicalButton.Location = new System.Drawing.Point(34, 385);
			this.toLogicalButton.Name = "toLogicalButton";
			this.toLogicalButton.Size = new System.Drawing.Size(25, 23);
			this.toLogicalButton.TabIndex = 14;
			this.toLogicalButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonsTooltip.SetToolTip(this.toLogicalButton, "Select Logical Item");
			this.toLogicalButton.UseVisualStyleBackColor = true;
			this.toLogicalButton.Click += new System.EventHandler(this.ToLogicalButtonClick);
			// 
			// toDatabaseButton
			// 
			this.toDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.toDatabaseButton.Image = ((System.Drawing.Image)(resources.GetObject("toDatabaseButton.Image")));
			this.toDatabaseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toDatabaseButton.Location = new System.Drawing.Point(65, 385);
			this.toDatabaseButton.Name = "toDatabaseButton";
			this.toDatabaseButton.Size = new System.Drawing.Size(25, 23);
			this.toDatabaseButton.TabIndex = 15;
			this.toDatabaseButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonsTooltip.SetToolTip(this.toDatabaseButton, "Select Database Item");
			this.toDatabaseButton.UseVisualStyleBackColor = true;
			this.toDatabaseButton.Click += new System.EventHandler(this.ToDatabaseButtonClick);
			// 
			// upButton
			// 
			this.upButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.upButton.Image = ((System.Drawing.Image)(resources.GetObject("upButton.Image")));
			this.upButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.upButton.Location = new System.Drawing.Point(111, 385);
			this.upButton.Name = "upButton";
			this.upButton.Size = new System.Drawing.Size(25, 23);
			this.upButton.TabIndex = 18;
			this.upButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonsTooltip.SetToolTip(this.upButton, "Select Database Item");
			this.upButton.UseVisualStyleBackColor = true;
			this.upButton.Click += new System.EventHandler(this.UpButtonClick);
			// 
			// downButton
			// 
			this.downButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.downButton.Image = ((System.Drawing.Image)(resources.GetObject("downButton.Image")));
			this.downButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.downButton.Location = new System.Drawing.Point(142, 385);
			this.downButton.Name = "downButton";
			this.downButton.Size = new System.Drawing.Size(25, 23);
			this.downButton.TabIndex = 19;
			this.downButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonsTooltip.SetToolTip(this.downButton, "Select Database Item");
			this.downButton.UseVisualStyleBackColor = true;
			this.downButton.Click += new System.EventHandler(this.DownButtonClick);
			// 
			// renameButton
			// 
			this.renameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.renameButton.Location = new System.Drawing.Point(198, 385);
			this.renameButton.Name = "renameButton";
			this.renameButton.Size = new System.Drawing.Size(75, 23);
			this.renameButton.TabIndex = 16;
			this.renameButton.Text = "Rename";
			this.renameButton.UseVisualStyleBackColor = true;
			this.renameButton.Click += new System.EventHandler(this.RenameButtonClick);
			// 
			// overrideButton
			// 
			this.overrideButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.overrideButton.Location = new System.Drawing.Point(280, 385);
			this.overrideButton.Name = "overrideButton";
			this.overrideButton.Size = new System.Drawing.Size(75, 23);
			this.overrideButton.TabIndex = 17;
			this.overrideButton.Text = "Override";
			this.overrideButton.UseVisualStyleBackColor = true;
			this.overrideButton.Click += new System.EventHandler(this.OverrideButtonClick);
			// 
			// DBCompareControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.downButton);
			this.Controls.Add(this.upButton);
			this.Controls.Add(this.overrideButton);
			this.Controls.Add(this.renameButton);
			this.Controls.Add(this.toDatabaseButton);
			this.Controls.Add(this.toLogicalButton);
			this.Controls.Add(this.refreshButton);
			this.Controls.Add(this.saveDatabaseButton);
			this.Controls.Add(this.compareDBListView);
			this.Name = "DBCompareControl";
			this.Size = new System.Drawing.Size(902, 411);
			this.ResumeLayout(false);

		}
	}
}
