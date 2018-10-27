using BrightIdeasSoftware;
using EAAddinFramework.Mapping;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MP = MappingFramework;
using UML = TSF.UmlToolingFramework.UML;

namespace EAMapping
{
    /// <summary>
    /// Description of MappingControlGUI.
    /// </summary>
    public partial class MappingControlGUI : UserControl
    {

        private List<MP.Mapping> selectedMappings => this.selectedNode != null ?
                                                    this.selectedNode?.mappings.ToList() : 
                                                    new List<MP.Mapping>();
        private MP.MappingSet mappingSet { get; set; }
        private List<MappingDetailsControl> mappingDetailControls { get; } = new List<MappingDetailsControl>();


        public MappingControlGUI()
        {
            // The InitializeComponent() call is required for Windows Forms designer
            // support.
            this.InitializeComponent();
            this.setDelegates();
        }


        private void addMappingDetailControl(int i)
        {
            var detailsControl = new MappingDetailsControl();
            detailsControl.mappingDeleted += this.mappingDeleted;
            detailsControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            detailsControl.BorderStyle = BorderStyle.FixedSingle;
            detailsControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            detailsControl.TabIndex = i;
            detailsControl.Hide();
            this.mappingDetailControls.Add(detailsControl);
            this.mappingPanel.Controls.Add(detailsControl,0,i );
        }

        private void mappingDeleted(object sender, EventArgs e)
        {
            var detailsControl = sender as MappingDetailsControl;
            var mapping = detailsControl.mapping;
            //get source and target
            var source = mapping?.source;
            var target = mapping?.target;
            //delete the mapping
            detailsControl.mapping?.delete();
            //clear the GUI
            this.clearMappingDetails();
            //show the mappings again
            this.showMappings(this.selectedMappings);
            //refresh to show the updated data
            this.sourceTreeView.RefreshObject(this.sourceTreeView.Objects.Cast<MappingNode>().FirstOrDefault());
            this.targetTreeView.RefreshObject(this.sourceTreeView.Objects.Cast<MappingNode>().FirstOrDefault());
        }



        private void clearMappingDetails()
        {
            foreach (var detailsControl in this.mappingDetailControls)
            {
                detailsControl.mapping = null;
                detailsControl.Hide();
            }
        }
        private void showMappings(List<MP.Mapping> mappings)
        {
            for (int i = 0; i < mappings.Count(); i++)
            {
                //make sure there are enough mappingDetailControls
                while (i >= this.mappingDetailControls.Count)
                {
                    this.addMappingDetailControl(i);
                }
                //set details and show
                this.mappingDetailControls[i].mapping = mappings[i];
                this.mappingDetailControls[i].Show();
            }
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
                    {
                        return "packageNode";
                    }
                    else
                    {
                        return "classifierNode";
                    }
                }
                if (rowObject is AttributeMappingNode)
                {
                    return "attributeNode";
                }

                if (rowObject is AssociationMappingNode)
                {
                    return "associationNode";
                }
                else
                {
                    return string.Empty;
                }
            };
            this.sourceColumn.ImageGetter = imageGetter;
            this.targetColumn.ImageGetter = imageGetter;
        }

        public Mapping SelectedMapping =>
                //LinkedTreeNodes link = this.trees.SelectedLink;
                //if(link == null) { return null; }
                //return link.Mapping;
                null;
        private MP.MappingNode selectedSourceNode => this.sourceTreeView.SelectedObject as MP.MappingNode;
        private MP.MappingNode selectedTargetNode => this.targetTreeView.SelectedObject as MP.MappingNode;
        private MP.MappingNode selectedNode { get; set; }

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
        private void clear()
        {
            this.clearMappingDetails();
        }


        // export

        public event EventHandler exportMappingSet;
        void ExportButtonClick(object sender, EventArgs e)
        {
           this.exportMappingSet?.Invoke(this.mappingSet, e);
        }


        private void sourceTreeView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the selected node
            this.selectedNode = this.selectedSourceNode;
            //show the mappings
            this.showSourceMappings();
        }

        private void showSourceMappings()
        {
            // remove any mappings currently showing
            this.clearMappingDetails();
            if (this.selectedSourceNode != null)
            {
                this.showMappings(this.selectedMappings);
                foreach (var mapping in this.selectedMappings)
                {
                    this.targetTreeView.Reveal(mapping.target, false);
                }

            }
        }

        private void targetTreeView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the selected node
            this.selectedNode = this.selectedTargetNode;
            //show the mappings
            showTargetMappings();

        }
        private void showTargetMappings()
        {
            // remove any mappings currently showing
            this.clearMappingDetails(); ;
            if (this.selectedTargetNode != null)
            {
                this.showMappings(this.selectedMappings);
                foreach (var mapping in this.selectedMappings)
                {
                    this.sourceTreeView.Reveal(mapping.source, false);
                }
            }
        }

        private void targetTreeView_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            //TODO: enableDisable context menu options
        }

        private void selectInProjectBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var parent = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (parent == this.sourceTreeView)
            {
                this.selectMappingNode(this.selectedSourceNode);
            }
            else
            {
                this.selectMappingNode(this.selectedTargetNode);
            }
        }
        private void selectMappingNode(MP.MappingNode node)
        {
            if (node?.source != null)
            {
                node.source.select();
            }
        }

        private void openPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var parent = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (parent == this.sourceTreeView)
            {
                this.openMappingNodeProperties(this.selectedSourceNode);
            }
            else
            {
                this.openMappingNodeProperties(this.selectedTargetNode);
            }
        }
        private void openMappingNodeProperties(MP.MappingNode node)
        {
            if (node?.source != null)
            {
                node.source.openProperties();
            }
        }

        public event EventHandler selectNewMappingTarget;
        public event EventHandler selectNewMappingSource;
        private void selectNewMappingRootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var parent = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (parent == this.targetTreeView)
            {
                selectNewMappingTarget?.Invoke(this.mappingSet, e);
                //reload the target view
                this.targetTreeView.Objects = new List<MP.MappingNode>() { this.mappingSet.target };
                this.targetTreeView.RefreshObject(this.mappingSet.target);
                this.targetTreeView.ExpandAll();
            }
            else
            {
                selectNewMappingSource?.Invoke(this.mappingSet, e);
            }
        }
        private void newEmptyMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var parent = ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
            if (parent == this.sourceTreeView)
            {
                this.selectedSourceNode.createEmptyMapping(this.targetTreeView.Objects.Cast<MappingNode>().FirstOrDefault());
            }
        }

        private void targetTreeView_SubItemChecking(object sender, SubItemCheckingEventArgs e)
        {
            var node = e.RowObject as MappingNode;
            //make sure the childeNodes are set on target nodes as they don't automatically get all child nodes
            if (node != null)
            {
                node.setChildNodes();
                this.targetTreeView.Expand(node);
            }
            
        }

        private void sourceTreeView_ItemActivate(object sender, EventArgs e)
        {
            this.selectMappingNode(this.selectedSourceNode);
        }

        private void targetTreeView_ItemActivate(object sender, EventArgs e)
        {
            this.selectMappingNode(this.selectedTargetNode);
        }

        private void targetTreeView_ModelCanDrop(object sender, ModelDropEventArgs e)
        {
            this.canDrop(e, targetTreeView);
        }
        public event EventHandler<BrightIdeasSoftware.ModelDropEventArgs> sourceToTargetDropped;
        private void targetTreeView_ModelDropped(object sender, ModelDropEventArgs e)
        {
            sourceToTargetDropped?.Invoke(sender, e);
            this.showSourceMappings();
        }

        private void sourceTreeView_ModelCanDrop(object sender, ModelDropEventArgs e)
        {
            this.canDrop(e, sourceTreeView);
        }
        private void canDrop(ModelDropEventArgs e, TreeListView treeListView)
        {
            var targetNode = e.TargetModel as MP.MappingNode;
            var sourceNode = e.SourceModels.Cast<MP.MappingNode>().FirstOrDefault();
            var topNode = treeListView.Objects.Cast<MP.MappingNode>().FirstOrDefault();
            //
            if (targetNode != null && sourceNode != null
                //do not allow mapping to self
                && targetNode != sourceNode
                //make sure the nodes are not empty
                && targetNode.source != null && sourceNode.source != null
                //do not allow to drop on same treeView
                && sourceNode != topNode
                && !sourceNode.isChildOf(topNode))
            {
                //make sure that they are not already mapped
                if  ( sourceNode.mappings.Any (x => x.target == targetNode && x.source == sourceNode
                                                || x.target == sourceNode && x.source == targetNode))
                {
                    e.Effect = DragDropEffects.None;
                    e.InfoMessage = "Items are already mapped!";
                }
                else
                {
                    //OK we can create the mapping
                    e.Effect = DragDropEffects.Link;
                    e.InfoMessage = "Create new Mapping";
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        public event EventHandler<BrightIdeasSoftware.ModelDropEventArgs> targetToSourceDropped;
        private void sourceTreeView_ModelDropped(object sender, ModelDropEventArgs e)
        {
            targetToSourceDropped?.Invoke(sender, e);
            this.showTargetMappings();
        }

        private void targetTreeView_FormatRow(object sender, FormatRowEventArgs e)
        {
            if (this.selectedMappings.Any(x => x.target == e.Model))
            {
                e.Item.BackColor = Color.LightYellow;
                //e.Item.Font = new Font(e.Item.Font, FontStyle.Bold);
            }
        }

        private void sourceTreeView_FormatRow(object sender, FormatRowEventArgs e)
        {
            if (this.selectedMappings.Any(x => x.source == e.Model))
            {
                e.Item.BackColor = Color.LightYellow;
                //e.Item.Font = new Font(e.Item.Font, FontStyle.Bold);
            }
        }

        private void sourceTreeView_HeaderCheckBoxChanging(object sender, HeaderCheckBoxChangingEventArgs e)
        {
            setAllExpanded(this.sourceTreeView, e.NewCheckState == CheckState.Checked, false);
        }
        private void targetTreeView_HeaderCheckBoxChanging(object sender, HeaderCheckBoxChangingEventArgs e)
        {
            setAllExpanded(this.targetTreeView, e.NewCheckState == CheckState.Checked, true);
        }
        private void setAllExpanded(TreeListView treeView, bool expand, bool isTarget)
        {
            //set expanded property on all nodes
            var root = treeView.Objects.Cast<MappingNode>().FirstOrDefault();
            setExpanded(root,expand, isTarget);
            treeView.RefreshObject(root);
            //expand all
            treeView.ExpandAll();
        }
        private void setExpanded(MappingNode node, bool expand, bool isTarget)
        {
            //don't do anything if null
            if (node == null) return;
            //set expanded property
            if (!node.showAll && isTarget)
                node.setChildNodes();
            node.showAll = expand;
            //do the same for the childnodes
            foreach (MappingNode subNode in node.childNodes)
            {
                setExpanded(subNode, expand, isTarget);
            }
        }


    }
}
