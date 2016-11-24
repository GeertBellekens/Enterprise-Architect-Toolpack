
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DB=DatabaseFramework;
using DB_EA = EAAddinFramework.Databases;
using UML = TSF.UmlToolingFramework.UML;
using System.Linq;

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
			//enable and Disable buttons
			enableDisable();
		}
		private void enableDisable()
		{
			//if there's a comparer we can refresh
			this.refreshButton.Enabled = (_comparer != null);
			//we can save if both new and existing database exist and are valid
			this.saveDatabaseButton.Enabled = (_comparer != null 
			                                   && _comparer.existingDatabase != null
			                                   && _comparer.newDatabase != null 
			                                   && _comparer.newDatabase.tables.Any(x => x.isValid));
			this.renameButton.Enabled = (this.selectedComparison != null
			                             && this.selectedComparison.newDatabaseItem != null);
			this.overrideButton.Enabled = (this.selectedComparison != null
											&& (((this.selectedComparison.existingDatabaseItem != null
												&& (this.selectedComparison.comparisonStatus == DB.Compare.DatabaseComparisonStatusEnum.changed
			                                       || this.selectedComparison.comparisonStatus == DB.Compare.DatabaseComparisonStatusEnum.deletedItem))
			                                   ||this.selectedComparison.comparisonStatus ==  DB.Compare.DatabaseComparisonStatusEnum.newItem )
			                                   || this.selectedComparison.comparisonStatus == DB.Compare.DatabaseComparisonStatusEnum.dboverride));
			//downbutton should not be enabled is this is the last item, of if the next item is a table
			this.downButton.Enabled = this.selectedComparison != null
				&& this.selectedComparison.itemType.Equals("Column",StringComparison.InvariantCultureIgnoreCase)
				&& this.compareDBListView.SelectedIndices[0]< this.compareDBListView.Items.Count -1 //not last item
				&& ! this.compareDBListView.Items[compareDBListView.SelectedIndices[0] +1 ].SubItems[1].Text.Equals("Table", StringComparison.InvariantCultureIgnoreCase); //next item not table
			//up button should not be enbled if this is the first item, and if the previous item is a table
			this.upButton.Enabled = this.selectedComparison != null
				&& this.selectedComparison.itemType.Equals("Column",StringComparison.InvariantCultureIgnoreCase)
				&& this.compareDBListView.SelectedIndices[0] > 0 //not last item
				&& ! this.compareDBListView.Items[compareDBListView.SelectedIndices[0] -1 ].SubItems[1].Text.Equals("Table", StringComparison.InvariantCultureIgnoreCase); //previous item not table
		}
		public void clear()
		{
			this.compareDBListView.Items.Clear();
			enableDisable();
		}
		public void loadComparison(DB.Compare.DatabaseComparer comparer)
		{
			this._comparer = comparer;
			this.clear();
			string tableName = string.Empty;
			foreach (var comparedItem in comparer.comparedItems) 
			{
				if (comparedItem.newDatabaseItem is DB.Table) 
				{
					tableName = comparedItem.newDatabaseItem.name;
				} else if (comparedItem.existingDatabaseItem is DB.Table)
				{
					tableName = comparedItem.existingDatabaseItem.name;
				}
				
				var newItem = addListViewItem(comparedItem,tableName);
				this.setStatusColor(newItem);
				this.compareDBListView.Items.Add(newItem);
			}
			enableDisable();
		}
		private void setItemBackColor(ListViewItem item, Color color)
		{
			foreach (ListViewItem.ListViewSubItem subItem in item.SubItems) 
			{
				subItem.BackColor = color;
			}
		}
		private void setItemFont(ListViewItem item, Font font)
		{
			foreach (ListViewItem.ListViewSubItem subItem in item.SubItems) 
			{
				subItem.Font = font;
			}
		}
		private void setStatusColor(ListViewItem item)
		{
			item.UseItemStyleForSubItems = false;
			var comparedItem = (DB.Compare.DatabaseItemComparison)item.Tag;
			switch (comparedItem.comparisonStatus) 
			{
				case DB.Compare.DatabaseComparisonStatusEnum.changed:
					setItemBackColor(item,Color.FromArgb(204,204,255));//purple-bleu
					break;
				case DB.Compare.DatabaseComparisonStatusEnum.newItem:
					setItemBackColor(item,Color.FromArgb(204,255,204));//pale green
					break;
				case DB.Compare.DatabaseComparisonStatusEnum.deletedItem:
					setItemBackColor(item,Color.FromArgb(255,216,216));//pale red
					break;		
				case DB.Compare.DatabaseComparisonStatusEnum.dboverride:
					setItemFont(item,new Font(item.Font, FontStyle.Italic)); //overridden items get italic font
					break;								
			}
			//table should be bold
			if (comparedItem.itemType == "Table") 
			{
				if (comparedItem.comparisonStatus == DB.Compare.DatabaseComparisonStatusEnum.dboverride)
				{
					setItemFont(item,new Font(item.Font,FontStyle.Bold|FontStyle.Italic));
				}
				else
				{
					setItemFont(item,new Font(item.Font,FontStyle.Bold));
				}		
			}
			//if an item is not valid then it should be in Red
			if (comparedItem.newDatabaseItem != null
				&& ! comparedItem.newDatabaseItem.isValid)
			{
				//add "!" before the name
				item.SubItems[3].Text = "(!) " + item.SubItems[3].Text;
				//name and properties should be red
				item.SubItems[3].ForeColor = Color.Red;
				item.SubItems[4].ForeColor = Color.Red;
			}
			if (comparedItem.existingDatabaseItem != null 
			    && ! comparedItem.existingDatabaseItem.isValid)
			{
				//add "!" before the name
				item.SubItems[5].Text = "(!) " + item.SubItems[5].Text;
				//name and properties should be red
				item.SubItems[5].ForeColor = Color.Red;
				item.SubItems[6].ForeColor = Color.Red;
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
		public event EventHandler selectLogicalItem = delegate { }; 
		public event EventHandler selectDatabaseItem = delegate { }; 
		void CompareDBListViewMouseDoubleClick(object sender, MouseEventArgs e)
		{
			ListViewHitTestInfo info = ((ListView)sender).HitTest(e.X, e.Y);
			if (info.Item != null)
			{
				var comparedItem = (DB.Compare.DatabaseItemComparison) info.Item.Tag;
				if (info.SubItem != null)
				{
					int columnIndex = info.Item.SubItems.IndexOf(info.SubItem);
					DB.DatabaseItem clickedItem = null;
					if (columnIndex <= 4 )
					{
						clickedItem = comparedItem.newDatabaseItem;
						selectLogicalItem(clickedItem, null);
					}
					else
					{
						clickedItem = comparedItem.existingDatabaseItem;
						selectDatabaseItem(clickedItem, null);
					}
				}
			}
			
		}
		public DB.Compare.DatabaseItemComparison selectedComparison
		{
			get
			{
				if (this.compareDBListView.SelectedItems.Count > 0)
				{
					return this.compareDBListView.SelectedItems[0].Tag as DB.Compare.DatabaseItemComparison;
				}
				return null;
			}
		}
		public DB.DatabaseItem selectedDatabaseItemWithLogical
		{
			get
			{
				if (this.selectedComparison != null)
				{
				  if (this.selectedComparison.existingDatabaseItem != null)
				  {
				  	if (this.selectedComparison.existingDatabaseItem.logicalElement != null) 
				  		return this.selectedComparison.existingDatabaseItem;
				  }
				  return this.selectedComparison.newDatabaseItem;
				}
				return null;
			}
		}

		void ToLogicalButtonClick(object sender, EventArgs e)
		{
			selectLogicalItem(selectedDatabaseItemWithLogical, null);
		}
		void ToDatabaseButtonClick(object sender, EventArgs e)
		{
			if (selectedComparison != null)
			{
				selectDatabaseItem(selectedComparison.existingDatabaseItem, null);
			}
		}
		public event EventHandler renameButtonClick = delegate { };
		void RenameButtonClick(object sender, EventArgs e)
		{
			renameButtonClick(this.selectedComparison,e);
		}
		public event EventHandler overrideButtonClick = delegate { };		
		void OverrideButtonClick(object sender, EventArgs e)
		{
			overrideButtonClick(this.selectedComparison,e);
		}
		void CompareDBListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			this.enableDisable();
		}
		void UpButtonClick(object sender, EventArgs e)
		{
			var currentComparison = this.selectedComparison;
			
			if (currentComparison != null)
			{
				var currentIndex = this._comparer.comparedItems.IndexOf(currentComparison);
				if (currentIndex > 0 )
				{
					if (currentComparison.itemType.Equals("Table",StringComparison.InvariantCultureIgnoreCase))
				    {
				    	//this is a table. we have to move all the underlying items as well.
				    	//TODO: currently not implemented until further notice
				    }
					else //not a table, move single item
					{					
						//move up by removing it from the current index and adding it to index -1
						this._comparer.comparedItems.RemoveAt(currentIndex);
						this._comparer.comparedItems.Insert(currentIndex -1,currentComparison);
						//reload
						loadComparison(this._comparer);
						this.compareDBListView.Items[currentIndex -1].Focused = true;
						this.compareDBListView.Items[currentIndex -1].Selected = true;
					}
				}
			}
		}
		void DownButtonClick(object sender, EventArgs e)
		{
			var currentComparison = this.selectedComparison;
			
			if (currentComparison != null)
			{
				var currentIndex = this._comparer.comparedItems.IndexOf(currentComparison);
				if (currentIndex <  this.compareDBListView.Items.Count -1)
				{
					if (currentComparison.itemType.Equals("Table",StringComparison.InvariantCultureIgnoreCase))
				    {
				    	//this is a table. we have to move all the underlying items as well.
				    	//TODO: currently not implemented until further notice
				    }
					else //not a table, move single item
					{					
						//move up by removing it from the current index and adding it to index -1
						this._comparer.comparedItems.RemoveAt(currentIndex);
						this._comparer.comparedItems.Insert(currentIndex +1,currentComparison);
						//reload
						loadComparison(this._comparer);
						this.compareDBListView.Items[currentIndex +1].Focused = true;
						this.compareDBListView.Items[currentIndex +1].Selected = true;
					}
				}
			}
		}	

	}
}
