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
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public void loadMappingSet(MappingSet mappingSet )
		{
			//clear trees
			this.trees.LeftTree.Nodes.Clear();
			this.trees.RightTree.Nodes.Clear();
			this.mappingSet = mappingSet;
			// populate left tree with some nodes
			List<Mapping> mappingList = mappingSet.mappings;
			List<LinkedTreeNode> sourceItems = new List<LinkedTreeNode>();
			List<LinkedTreeNode> targetItems = new List<LinkedTreeNode>();
			foreach (Mapping map in mappingList) {
				//TODO When target getMapping is fixed delete the if
				if (map.source.fullMappingPath.Contains("Target")) {
					MessageBox.Show("target spotted");
				} else {
					sourceItems.Add(new LinkedTreeNode(map.source.mappedEnd.name, map.source.fullMappingPath,map));
					targetItems.Add(new LinkedTreeNode(map.target.mappedEnd.name, map.target.fullMappingPath,map));
				}
			}
			this.leftNodes = new List<LinkedTreeNode>();
			this.rightNodes = new List<LinkedTreeNode>();
			//Create the left tree nodes
			PopulateTree(trees.LeftTree, sourceItems, '.');
			//Create the right tree nodes
			PopulateTree(trees.RightTree, targetItems, '.');
			//Get all the already excisting mappings and paint them.
			paintAllExistingMappings(sourceItems, targetItems, mappingSet, trees);
			//expand both trees. 
			trees.RightTree.ExpandAll();
			trees.LeftTree.ExpandAll();
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
		public Mapping selectedMapping
		{
			get
			{
				//TODO: fix code to get the selected mapping
				//check if the left node is selected
				var leftnode = trees.LeftTree.SelectedNode as LinkedTreeNode;
				if (leftnode != null) return leftnode.mapping;
				//check if right node is selected
				var rightnode = trees.RightTree.SelectedNode as LinkedTreeNode;
				if (leftnode != null) return rightnode.mapping;
				//if none is selected return null
				return null;
			}
		}		
        public void PopulateTree(LinkedTreeView treeView, ICollection<LinkedTreeNode> items, char pathSeparator)
        {
            List<string> stringlist = new List<string>();
            foreach (LinkedTreeNode l in items)
            {
                stringlist.Add(l.Path + "");
            }

            PopulateTreeList(treeView, stringlist, '.');
        }


        private void PopulateTreeList(LinkedTreeView treeView, List<string> items, char pathSeparator)
        {
            LinkedTreeNode lastNode = null;
            string subPathAgg;
            foreach (string path in items)
            {
                subPathAgg = string.Empty;
                foreach (string subPath in path.Split(pathSeparator))
                {
                    if(subPathAgg.Length == 0)
                    {
                        subPathAgg = subPath;
                    } else 
                         subPathAgg += pathSeparator + subPath; 
                    //label, path
                    List<LinkedTreeNode> nodes = new List<LinkedTreeNode> ();
                    nodes = Find(subPath, subPathAgg, treeView);
                    if (nodes.Count == 0)
                        if (lastNode == null)
                            lastNode = addNode(subPath, subPathAgg,treeView);
                        else
                        {
                            Boolean left = treeView.IsLeft;
                            lastNode = addNode(subPath, subPathAgg, left,lastNode);

                        }
                    else
                        lastNode = nodes[0];
                }
                lastNode = null; 
            }
        }

        private LinkedTreeNode addNode(string label, string path, LinkedTreeView tree = null)
        {
            var node = new LinkedTreeNode(label, path);
            if (tree.IsLeft)
            {
                leftNodes.Add(node);
            }else
            {
                rightNodes.Add(node);
            }
            if (tree != null)
            {
                tree.Nodes.Add(node);
            }
            return node;
        }

        private LinkedTreeNode addNode(string label, string path, Boolean left, TreeNode parent = null)
        {
            var node = new LinkedTreeNode(label, path);
            if (left)
            {
                leftNodes.Add(node);
            }
            else
            {
                rightNodes.Add(node);
            }
            if (parent != null) { parent.Nodes.Add(node); }

            return node;

        }

        private List<LinkedTreeNode> Find(string label, string path, LinkedTreeView tree = null)
        {
            List<LinkedTreeNode> LTN = new List<LinkedTreeNode>();
            
            if (tree.IsLeft)
            {

                foreach (LinkedTreeNode l in this.leftNodes)
                {
                    if(l.Label.Equals(label) && l.Path.Equals(path))
                    {
                        LTN.Add(l);
                    }
                }
            }
            else
            {
                foreach (LinkedTreeNode l in rightNodes)
                {
                    if (l.Label.Equals(label) && l.Path.Equals(path))
                    {
                        LTN.Add(l);                  
                    }
                }
            }

            return LTN;
        }

        //Better use the Find with path in parameters.
        private List<LinkedTreeNode> Find(string label, LinkedTreeView tree = null)
        {
            List<LinkedTreeNode> LTN = new List<LinkedTreeNode>();

            if (tree.IsLeft)
            {
                foreach (LinkedTreeNode l in this.leftNodes)
                {
                    if (l.Label.Equals(label))
                    {
                        LTN.Add(l);
                    }
                }
            }
            else
            {
                
                foreach (LinkedTreeNode l in rightNodes)
                {
                    if (l.Label.Equals(label))
                    {
                        LTN.Add(l);

                    }
                }
            }

            return LTN;
        }

        private void paintAllExistingMappings(List<LinkedTreeNode> sourceItems, List<LinkedTreeNode> targetItems, MappingSet MS, LinkedTreeViews linkedTV)
        {
            List<Mapping> mappings = MS.mappings;
            LinkedTreeView leftTreeView = linkedTV.LeftTree;
            LinkedTreeView rightTreeView = linkedTV.RightTree;
          
            foreach (Mapping m in mappings)
            {               
                string sn = m.source.mappedEnd.name;
                string tn = m.target.mappedEnd.name;

                string sp = m.source.mappingPath + '.' + sn;
                string tp = m.target.mappingPath + '.' + tn;

                List<LinkedTreeNode> LTN = Find(sn,sp, linkedTV.LeftTree);
                List<LinkedTreeNode> RTN = Find(tn,tp, linkedTV.RightTree);

                if(LTN.Count == 1 && RTN.Count == 1)
                {
                    LinkedTreeNode l = LTN[0];
                    LinkedTreeNode r = RTN[0];
                    l.LinkTo(r);
                }
        }

            
        }
	}
}
