
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
