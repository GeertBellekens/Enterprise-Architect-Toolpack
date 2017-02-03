using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MappingFramework;

namespace EAMapping
{
	/// <summary>
	/// Description of MappingControl.
	/// </summary>
	public partial class MappingControl : UserControl
	{
		MappingSet mappingSet {get;set;}
		public MappingControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//set the grid columns to the right size
			resizeCompareGridColumns();
			
		}
		public void loadMappingSet(MappingSet mappingSet)
		{
			//set current mapping Set
			this.mappingSet = mappingSet;
			//clear the list
			clear();
			//add the mappings
			foreach (var mapping in mappingSet.mappings) 
			{
				//source
				ListViewItem listViewItem = new ListViewItem(mapping.source.mappedEnd.name);
				//source type
				listViewItem.SubItems.Add(mapping.source.mappedEnd.GetType().Name);
				//source path
				listViewItem.SubItems.Add(mapping.source.mappingPath);
				//mapping logic
				listViewItem.SubItems.Add(mapping.mappingLogic != null ? mapping.mappingLogic.description : string.Empty);
				//target
				listViewItem.SubItems.Add(mapping.target.mappedEnd.name);
				//target type
				listViewItem.SubItems.Add(mapping.target.mappedEnd.GetType().Name);
				//target path
				listViewItem.SubItems.Add(mapping.target.mappingPath);
				//set tag
				listViewItem.Tag = mapping;
				//add the item to the grid
				this.mappingListView.Items.Add(listViewItem);
			}
		}
		private void resizeCompareGridColumns()
		{
			//set the last column to fill
			this.mappingListView.Columns[mappingListView.Columns.Count - 1].Width = -2;
		}
		void MappingListViewResize(object sender, EventArgs e)
		{
			resizeCompareGridColumns();
		}
		public void clear()
		{
			//clear the listview
			this.mappingListView.Items.Clear();
		}
	}
}
