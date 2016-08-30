
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DB=DatabaseFramework;
using DB_EA = EAAddinFramework.Databases;

namespace EADatabaseTransformer
{
	/// <summary>
	/// Description of DBCompareControl.
	/// </summary>
	public partial class DBCompareControl : UserControl
	{
		DB.Compare.DatabaseComparer _comparer = null;
		public DBCompareControl()
		{
			Application.EnableVisualStyles();	
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//resize columns
			resizeCompareGridColumns();
		}
		public void clear()
		{
			this.compareDBListView.Items.Clear();
		}
		public void loadComparison(DB.Compare.DatabaseComparer comparer)
		{
			this._comparer = comparer;
			this.clear();
			string tableName = string.Empty;
			foreach (var comparedItem in comparer.comparedItems) 
			{
				if (comparedItem.newDatabaseItem != null 
				    && comparedItem.newDatabaseItem is DB.Table) 
				{
					tableName = comparedItem.newDatabaseItem.name;
				} else if (comparedItem.existingDatabaseItem != null 
				           && comparedItem.existingDatabaseItem is DB.Table)
				{
					tableName = comparedItem.existingDatabaseItem.name;
				}
				
				var newItem = addListViewItem(comparedItem,tableName);
				this.setStatusColor(newItem);
				this.compareDBListView.Items.Add(newItem);

			}
		}
		private void setStatusColor(ListViewItem item)
		{
			var comparedItem = (DB.Compare.DatabaseItemComparison)item.Tag;
			switch (comparedItem.comparisonStatus) 
			{
				case DB.Compare.DatabaseComparisonStatusEnum.changed:
					item.BackColor = Color.FromArgb(204,204,255);//purple-bleu
					break;
				case DB.Compare.DatabaseComparisonStatusEnum.newItem:
					item.BackColor = Color.FromArgb(204,255,204);//pale green
					break;
				case DB.Compare.DatabaseComparisonStatusEnum.deletedItem:
					item.BackColor = Color.FromArgb(255,216,216);//pale red
					break;		
				case DB.Compare.DatabaseComparisonStatusEnum.dboverride:
					item.Font = new Font(item.Font, FontStyle.Italic); //overridden items get italic font
					break;								
			}
		}
		private ListViewItem addListViewItem(DB.Compare.DatabaseItemComparison comparison,string tableName)
		{

			var listViewItem = new ListViewItem(comparison.comparisonStatusName);
			listViewItem.SubItems.Add(comparison.itemType);
			listViewItem.SubItems.Add(tableName);
			
			addDatabaseItemSpecifics(listViewItem,comparison.newDatabaseItem );
			addDatabaseItemSpecifics(listViewItem,comparison.existingDatabaseItem);
			
			listViewItem.Tag = comparison;
			return listViewItem;
		}
		
		private void addDatabaseItemSpecifics(ListViewItem listViewItem,DB.DatabaseItem databaseItem )
		{
			if (databaseItem != null)
			{
				listViewItem.SubItems.Add(databaseItem.name);
				listViewItem.SubItems.Add(databaseItem.properties);
			}
			else
			{
				listViewItem.SubItems.Add(string.Empty);
				listViewItem.SubItems.Add(string.Empty);
			}
		}
		
		private void loadDatabaseInGrid(DB.Database database, ListView grid)
		{
			foreach (var table  in database.tables) 
			{
				//columns
				foreach (var column in table.columns) 
				{
					addDatabaseItem(column,table.name,grid);
				}
				//primary key
				if (table.primaryKey != null)
				{
					addDatabaseItem(table.primaryKey,table.name,grid);
				}
				//foreignkeys
				foreach (var foreignkey in table.foreignKeys) 
				{
					addDatabaseItem(foreignkey,table.name,grid);
				}
			}
		}
		private void addDatabaseItem(DB.DatabaseItem item,string tableName, ListView grid)
		{
			var listViewItem = new ListViewItem(tableName);
			listViewItem.SubItems.Add(item.itemType);
			listViewItem.SubItems.Add(item.name);
			listViewItem.SubItems.Add(item.properties);
			grid.Items.Add(listViewItem);
		}

		private void resizeCompareGridColumns()
		{
			//set the last column to fill
			this.compareDBListView.Columns[compareDBListView.Columns.Count - 1].Width = -2;
		}
		void CompareListViewResize(object sender, EventArgs e)
		{
			resizeCompareGridColumns();
		}
		public event EventHandler saveDatabaseButtonClick;
		void SaveDatabaseButtonClick(object sender, EventArgs e)
		{
			if (this.saveDatabaseButtonClick != null)
			{
				saveDatabaseButtonClick(sender, e);
			}
		}
		public event EventHandler refreshButtonClicked;
		void RefreshButtonClick(object sender, EventArgs e)
		{
			if (this.refreshButtonClicked != null)
			{
				refreshButtonClicked(sender, e);
			}
		}

	}
}
