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
			//enableDisable
			enableDisable();
			
		}
		public Mapping selectedMapping
		{
			get
			{
				if (this.mappingListView.SelectedItems.Count > 0)
				{
					return this.mappingListView.SelectedItems[0].Tag as Mapping;
				}
				return null;
			}
		}
		void enableDisable()
		{
			this.goToSourceButton.Enabled = this.selectedMapping != null;
			this.goToTargetButton.Enabled = this.selectedMapping != null;
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
				//sourcePath
				ListViewItem listViewItem = new ListViewItem(mapping.source.mappingPath);
				//source type
				listViewItem.SubItems.Add(mapping.source.mappedEnd.GetType().Name);
				//source
				listViewItem.SubItems.Add(mapping.source.mappedEnd.name);
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
		
		public event EventHandler selectTarget = delegate { }; 
		void GoToSourceButtonClick(object sender, EventArgs e)
		{
			selectSource(this.selectedMapping,e);
		}
		public event EventHandler selectSource = delegate { }; 
		void GoToTargetButtonClick(object sender, EventArgs e)
		{
			selectTarget(this.selectedMapping,e);
		}
		public event EventHandler exportMappingSet = delegate { }; 
		void ExportButtonClick(object sender, EventArgs e)
		{
			exportMappingSet(this.mappingSet,e);
		}
		void MappingListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			this.enableDisable();
		}
		void MappingListViewMouseDoubleClick(object sender, MouseEventArgs e)
		{
			ListViewHitTestInfo info = ((ListView)sender).HitTest(e.X, e.Y);
			if (info.Item != null)
			{
				var mapping = (Mapping) info.Item.Tag;
				if (info.SubItem != null)
				{
					int columnIndex = info.Item.SubItems.IndexOf(info.SubItem);
					if (columnIndex <= 3 )
					{
						selectSource(mapping,e);
					}
					else
					{
						selectTarget(mapping,e);
					}
				}
			}
	
		}
	}
}
