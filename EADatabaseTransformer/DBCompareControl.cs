
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
		private DB.Database _originalDatabase;
		private DB.Database _newDatabase;
		public DBCompareControl()
		{
			Application.EnableVisualStyles();	
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//resize columns
			resizeOriginalDBGridColumns();
			resizeNewDBGridColumns();
		}

		public void loadComparison(DB.Compare.DatabaseComparer comparer)
		{
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
				
				var newItem = new ListViewItem(comparedItem.comparisonStatusName);
				newItem = addListViewItem(comparedItem.newDatabaseItem, tableName, newItem);
				this.newDBListView.Items.Add(newItem);
				this.originalDBListView.Items.Add(addListViewItem(comparedItem.existingDatabaseItem,tableName,null));
			}
		}
		private ListViewItem addListViewItem(DB.DatabaseItem item,string tableName,ListViewItem listViewItem)
		{
			if (listViewItem == null) 
			{
				listViewItem = new ListViewItem(tableName);
			}
			else
			{
				listViewItem.SubItems.Add(tableName);
			}
			if (item != null)
			{
				listViewItem.SubItems.Add(item.itemType);
				listViewItem.SubItems.Add(item.name);
				listViewItem.SubItems.Add(item.properties);
			}
			else
			{
				listViewItem.SubItems.Add(string.Empty);
				listViewItem.SubItems.Add(string.Empty);
				listViewItem.SubItems.Add(string.Empty);
			}
			return listViewItem;
		}
		public void loadOriginalDatabase(DB.Database database)
		{
			this._originalDatabase = database;
			loadDatabaseInGrid(_originalDatabase, this.originalDBListView);
		}
		public void loadNewDatabase(DB.Database database)
		{
			this._newDatabase = database;
			loadDatabaseInGrid(_newDatabase, this.newDBListView);
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
		private void resizeOriginalDBGridColumns()
		{
			//set the last column to fill
			this.originalDBListView.Columns[originalDBListView.Columns.Count - 1].Width = -2;
		}
		private void resizeNewDBGridColumns()
		{
			//set the last column to fill
			this.newDBListView.Columns[newDBListView.Columns.Count - 1].Width = -2;
		}
		void NewDBListViewResize(object sender, EventArgs e)
		{
			resizeNewDBGridColumns();
		}
		void OriginalDBListViewResize(object sender, EventArgs e)
		{
			resizeOriginalDBGridColumns();
		}
	}
}
