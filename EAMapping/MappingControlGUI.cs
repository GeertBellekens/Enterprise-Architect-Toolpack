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
	public partial class MappingControlGUI : UserControl {

		public MappingSet mappingSet           { get; set; }
    public List<LinkedTreeNode> leftNodes  { get; set; }
    public List<LinkedTreeNode> rightNodes { get; set; }

		public Mapping SelectedMapping {
			get	{
        LinkedTreeNodes link = this.trees.SelectedLink;
        if(link == null) { return null; }
        return link.Mapping;
			}
		}
        
		public MappingControlGUI() {
			// The InitializeComponent() call is required for Windows Forms designer
			// support.
			InitializeComponent();

      // bubble LinkedTreeViews events via our own events
      this.trees.CreateMapping    += this.handleCreateMapping;
      this.trees.EditMappingLogic += this.handleEditMappingLogic;
		}

		public void loadMappingSet(MappingSet mappingSet) {
      this.trees.Mappings = mappingSet.mappings;
      this.trees.ExpandAll();
    }

    // EVENTS

    // navigate to source/target

		public event EventHandler selectSource = delegate { }; 
		void GoToSourceButtonClick(object sender, EventArgs e) {
			if(this.SelectedMapping != null) {
        selectSource(this.SelectedMapping,e);
      }
		}

		public event EventHandler selectTarget = delegate { }; 
		void GoToTargetButtonClick(object sender, EventArgs e) {
			if(this.SelectedMapping != null) {
        selectTarget(this.SelectedMapping,e);
      }
		}

    // create/delete mapping

		public event Action<Mapping> CreateMapping = delegate { }; 
		private void handleCreateMapping(Mapping mapping) {
			this.CreateMapping(mapping);
		}

    public void CreateMappingButtonClick(object sender, EventArgs e) {
      var source = this.trees.SourceTree.SelectedNode as LinkedTreeNode;
      var target = this.trees.TargetTree.SelectedNode as LinkedTreeNode;
      if(source != null && target != null) {
        this.trees.Link(source, target);
      } else {
        MessageBox.Show("Please select a source and target to map.");
      }
    }

    public event Action<Mapping> DeleteMapping = delegate {};
    public void DeleteMappingButtonClick(object sender, EventArgs e) {
      if (this.SelectedMapping != null) {
        Mapping mapping = this.SelectedMapping;
        this.trees.DeleteMapping(mapping);
        this.DeleteMapping(mapping);
      }
    }

    // edit / delete mapping logic

		public event Action<Mapping> EditMappingLogic = delegate { };
		private void handleEditMappingLogic(Mapping mapping) {
			this.EditMappingLogic(mapping);
		}

    public void EditMappingLogicButtonClick(object sender, EventArgs e) {
      if(this.SelectedMapping != null) {
        this.EditMappingLogic(this.SelectedMapping);
      }
    }

		public event Action<Mapping> DeleteMappingLogic = delegate { };

    public void DeleteMappingLogicButtonClick(object sender, EventArgs e) {
      if(this.trees.SelectedLink != null) {
        this.DeleteMappingLogic(this.SelectedMapping);
        this.trees.Invalidate();
      }
    }

    // export

		public event EventHandler exportMappingSet = delegate { }; 
		void ExportButtonClick(object sender, EventArgs e) {
			exportMappingSet(this.mappingSet,e);
		}

	}
}
