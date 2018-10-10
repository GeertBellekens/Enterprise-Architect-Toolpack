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

        private List<MP.Mapping> selectedMappings { get; set; } = new List<MP.Mapping>();
        private MP.MappingSet mappingSet { get; set; }
        private List<MappingDetailsControl> mappingDetailControls { get; } = new List<MappingDetailsControl>();


        public MappingControlGUI()
        {
            // The InitializeComponent() call is required for Windows Forms designer
            // support.
            this.InitializeComponent();
            this.setDelegates();
        }


        private void addMappingDetailControl()
        {
            var detailsControl = new MappingDetailsControl();
            detailsControl.Hide();
            this.mappingDetailControls.Add(detailsControl);
            this.mappingPanel.Controls.Add(detailsControl);
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
                    this.addMappingDetailControl();
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

        public event EventHandler exportMappingSet = delegate { };
        void ExportButtonClick(object sender, EventArgs e)
        {
            exportMappingSet(this.mappingSet, e);
        }


        private void sourceTreeView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.showSourceMappings();
        }

        private void showSourceMappings()
        {
            // remove any mappings currently showing
            this.clearMappingDetails();
            if (this.selectedSourceNode != null)
            {
                this.selectedMappings = this.selectedSourceNode.mappings.ToList();
                this.showMappings(this.selectedMappings);
                foreach (var mapping in this.selectedMappings)
                {
                    this.targetTreeView.Reveal(mapping.target, false);
                }

            }
        }

        private void targetTreeView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // remove any mappings currently showing
            this.clearMappingDetails(); ;
            if (this.selectedTargetNode != null)
            {
                this.selectedMappings = this.selectedTargetNode.mappings.ToList();
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

        private void targetTreeView_SubItemChecking(object sender, SubItemCheckingEventArgs e)
        {
            var node = e.RowObject as MappingNode;
            //make sure the childeNodes are set on target nodes and they don't automatically get all child nodes
            if (node != null)
            {
                node.setChildNodes();
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
            var targetNode = e.TargetModel as MP.MappingNode;
            var sourceNode = e.SourceModels.Cast<Object>().FirstOrDefault() as MP.MappingNode;
            if (targetNode != null && sourceNode != null)
            {
                e.Effect = DragDropEffects.Link;
                e.InfoMessage = "Create new Mapping";
            }
        }
        public event EventHandler<BrightIdeasSoftware.ModelDropEventArgs> createNewMapping;
        private void targetTreeView_ModelDropped(object sender, ModelDropEventArgs e)
        {
            createNewMapping?.Invoke(sender, e);
            this.showSourceMappings();
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
    }
}
