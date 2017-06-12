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
	/// Description of MappingControlGUI.
	/// </summary>
	public partial class MappingControlGUI : UserControl
	{
		MappingSet mappingSet { get; set; }
        public List<LinkedTreeNode> leftNodes { get; set; }
        public List<LinkedTreeNode> rightNodes { get; set; }
        
		public MappingControlGUI()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

      // bubble LinkedTreeViews events via our own events
      this.trees.ShowMapping   += this.handleShowMapping;
      this.trees.CreateMapping += this.handleCreateMapping;
		}

		public void loadMappingSet(MappingSet mappingSet) {
      this.trees.Mappings = mappingSet.mappings;
      this.trees.ExpandAll();
    }

    // EVENTS

		public event Action<Mapping> ShowMapping = delegate { }; 
		private void handleShowMapping(Mapping mapping) {
			this.ShowMapping(mapping);
		}

		public event Action<Mapping> CreateMapping = delegate { }; 
		private void handleCreateMapping(Mapping mapping) {
			this.CreateMapping(mapping);
		}

		public event EventHandler selectTarget = delegate { }; 
		void GoToSourceButtonClick(object sender, EventArgs e)
		{
			if (this.selectedMapping != null) selectSource(this.selectedMapping,e);
		}

		public event EventHandler selectSource = delegate { }; 
		void GoToTargetButtonClick(object sender, EventArgs e)
		{
			if (this.selectedMapping != null) selectTarget(this.selectedMapping,e);
		}

		public event EventHandler exportMappingSet = delegate { }; 
		void ExportButtonClick(object sender, EventArgs e)
		{
			exportMappingSet(this.mappingSet,e);
		}

    // PROPERTIES

		public Mapping selectedMapping {
			get	{
        LinkedTreeNodes link = this.trees.SelectedLink;
        if(link == null) { return null; }
        return link.Mapping;
			}
		}
	}
}
