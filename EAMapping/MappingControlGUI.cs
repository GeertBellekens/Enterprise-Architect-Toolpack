using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MP = MappingFramework;
using BrightIdeasSoftware;
using EAAddinFramework.Mapping;
using UML = TSF.UmlToolingFramework.UML;

namespace EAMapping
{
	/// <summary>
	/// Description of MappingControlGUI.
	/// </summary>
	public partial class MappingControlGUI : UserControl 
	{

		public MP.MappingSet mappingSet           { get; set; }
    	public List<LinkedTreeNode> leftNodes  { get; set; }
   		public List<LinkedTreeNode> rightNodes { get; set; }


        
		public MappingControlGUI() 
		{
			// The InitializeComponent() call is required for Windows Forms designer
			// support.
			InitializeComponent();
            setDelegates();
            // bubble LinkedTreeViews events via our own events
            //this.trees.CreateMapping    += this.handleCreateMapping;
            //this.trees.EditMappingLogic += this.handleEditMappingLogic;
        }
        private void setDelegates()
        {
            //tell the control who can expand 
            TreeListView.CanExpandGetterDelegate canExpandGetter = delegate (object x)
            {
                var mappingNode = (MappingNode)x;
                return mappingNode.childNodes.Any();
            };
            this.sourceTreeView.CanExpandGetter = canExpandGetter;
            this.targetTreeView.CanExpandGetter = canExpandGetter;
            //tell the control how to expand
            TreeListView.ChildrenGetterDelegate childrenGetter = delegate (object x)
            {
                var mappingNode = (MappingNode)x;
                return mappingNode.childNodes;
            };
            this.sourceTreeView.ChildrenGetter = childrenGetter;
            this.targetTreeView.ChildrenGetter = childrenGetter;
            //tell the control which image to show
            ImageGetterDelegate imageGetter = delegate (object rowObject)
            {
                if (rowObject is ClassifierMappingNode)
                {
                    if (((ClassifierMappingNode)rowObject).source is UML.Classes.Kernel.Package)
                        return "packageNode";
                    else
                        return "classifierNode";
                }
                if (rowObject is AttributeMappingNode) return "attributeNode";
                if (rowObject is AssociationMappingNode) return "associationNode";
                else return string.Empty;
            };
            this.sourceColumn.ImageGetter = imageGetter;
            this.targetColumn.ImageGetter = imageGetter;
        }

        public Mapping SelectedMapping
        {
            get
            {
                //LinkedTreeNodes link = this.trees.SelectedLink;
                //if(link == null) { return null; }
                //return link.Mapping;
                return null;
            }
        }
        private MP.MappingNode selectedSourceNode
        {
            get { return this.sourceTreeView.SelectedObject as MP.MappingNode; }
        }
        private MP.MappingNode selectedTargetNode
        {
            get { return this.targetTreeView.SelectedObject as MP.MappingNode; }
        }

        public void loadMappingSet(MP.MappingSet mappingSet) 
		{
			//first clear the existing mappings
			this.clear();
            //then add the new mappingSet
            this.mappingSet = mappingSet;
            this.sourceTreeView.Objects = new List<MP.MappingNode>() { mappingSet.source };
            this.targetTreeView.Objects = new List<MP.MappingNode>() { mappingSet.target };
            //expand the treeviews
            this.sourceTreeView.ExpandAll();
            this.targetTreeView.ExpandAll();
        }

		public void addNode(MappingNode mappedEnd, bool source)
		{
			//var path =  mappedEnd.fullMappingPath.Split('.').ToList();
			if (source) 
			{
				//this.trees.SourceTree.addNode(mappedEnd,path);
			}
			else
			{
				//this.trees.TargetTree.addNode(mappedEnd,path);
			}
			//make sure the new node is visible?
			//this.trees.ExpandAll();
		}
		void clear()
		{
			//this.trees.Clear();
		}
    	// EVENTS

    	// navigate to source/target

		public event EventHandler selectSource = delegate { }; 
		void GoToSourceButtonClick(object sender, EventArgs e) 
		{
			if(this.SelectedMapping != null)  selectSource(this.SelectedMapping,e);
		}

		public event EventHandler selectTarget = delegate { }; 
		void GoToTargetButtonClick(object sender, EventArgs e) 
		{
			if(this.SelectedMapping != null) selectTarget(this.SelectedMapping,e);
		}

    	// create/delete mapping

		public event Action<Mapping> CreateMapping = delegate { }; 
		private void handleCreateMapping(Mapping mapping) 
		{
			this.CreateMapping(mapping);
		}

		public void CreateMappingButtonClick(object sender, EventArgs e) 
		{
			//var source = this.trees.SourceTree.SelectedNode as LinkedTreeNode;
			//var target = this.trees.TargetTree.SelectedNode as LinkedTreeNode;
			//if(source != null && target != null) 
			//{
			//	this.trees.Link(source, target);
			//}
			//else
			//{
			//	MessageBox.Show("Please select a source and target to map.");
			//}
		}
		
		public event Action<Mapping> DeleteMapping = delegate {};
		public void DeleteMappingButtonClick(object sender, EventArgs e) 
		{
			if (this.SelectedMapping != null) 
			{
				Mapping mapping = this.SelectedMapping;
				//this.trees.DeleteMapping(mapping);
				this.DeleteMapping(mapping);
			}
		}

    	// edit / delete mapping logic

		public event Action<Mapping> EditMappingLogic = delegate { };
		private void handleEditMappingLogic(Mapping mapping) {
			this.EditMappingLogic(mapping);
		}

		public void EditMappingLogicButtonClick(object sender, EventArgs e) 
		{
			if(this.SelectedMapping != null) 
			{
				this.EditMappingLogic(this.SelectedMapping);
			}
		}
		
		public event Action<Mapping> DeleteMappingLogic = delegate { };
		
		public void DeleteMappingLogicButtonClick(object sender, EventArgs e) 
		{
			//if(this.trees.SelectedLink != null) 
			//{
			//	this.DeleteMappingLogic(this.SelectedMapping);
			//	this.trees.Invalidate();
			//}
		}
		
    	// export

		public event EventHandler exportMappingSet = delegate { }; 
		void ExportButtonClick(object sender, EventArgs e) 
		{
			exportMappingSet(this.mappingSet,e);
		}

        private void expandSourceButton_Click(object sender, EventArgs e)
        {
            var mappingNode = this.selectedSourceNode;
            if (mappingNode != null)
            {
                //toggle showAll
                mappingNode.showAll = !mappingNode.showAll;
                this.sourceTreeView.RefreshObject(mappingNode);
                this.sourceTreeView.Expand(mappingNode);
            }

        }

        private void expandTargetButton_Click(object sender, EventArgs e)
        {

        }


        private void sourceTreeView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.selectedSourceNode != null)
            {
                foreach (var mapping in this.selectedSourceNode.mappings)
                {
                    targetTreeView.SelectObject(mapping.target);
                }
            }

        }
    }
}
