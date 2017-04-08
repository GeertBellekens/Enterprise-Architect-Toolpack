
using MappingFramework;
using EAMapping.Custom_User_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EAMapping;

namespace EAMapping
{
    /// <summary>
    /// Description of MappingControl.
    /// </summary>
    public partial class MappingControlGUI : Form
    {
        MappingSet mappingSet { get; set; }
        public List<LinkedTreeNode> leftNodes { get; set; }
        public List<LinkedTreeNode> rightNodes { get; set; }
        
        public MappingControlGUI(MappingSet mappingSet)
        {
            this.MinimumSize = new Size(1200, 400);
            this.Dock = DockStyle.None;
            //this.Width = 600;

            var trees = new LinkedTreeViews();
            this.Controls.Add(trees);

            this.mappingSet = mappingSet;
            // populate left tree with some nodes
            List<Mapping> mappingList = mappingSet.mappings;
            List<LinkedTreeNode> sourceItems = new List<LinkedTreeNode>();
            List<LinkedTreeNode> targetItems = new List<LinkedTreeNode>();


            foreach (Mapping map in mappingList)
            {
                //TODO When target getMapping is fixed delete the if
                if (map.source.fullMappingPath.Contains("Target"))
                {
                    MessageBox.Show("target spotted");
                }
                else
                {
                    sourceItems.Add(new LinkedTreeNode(map.source.mappedEnd.name, map.source.fullMappingPath));
                    targetItems.Add(new LinkedTreeNode(map.target.mappedEnd.name, map.target.fullMappingPath));
                }
            }

            this.leftNodes = new List<LinkedTreeNode>();
            this.rightNodes = new List<LinkedTreeNode>();

            //Create the left tree nodes
            PopulateTree(trees.LeftTree, sourceItems, '.');

            //Create the right tree nodes
            PopulateTree(trees.RightTree, targetItems, '.');

            //Get all the already excisting mappings and paint them.
            paintAllExistingMappings(sourceItems,targetItems, mappingSet, trees);

            //expand both trees. 
            trees.RightTree.ExpandAll();
            trees.LeftTree.ExpandAll();
        }

        /*public void PopulateTree(LinkedTreeView treeView, ICollection<LinkedTreeNode> items, char pathSeparator)
        {
            TreeNode lastNode = null;
            LinkedTreeNode father = null;
            string subPathAgg;
            foreach (LinkedTreeNode path in items)
            {

                subPathAgg = string.Empty;
                foreach (string subPath in path.Path.Split(pathSeparator))
                {
                    subPathAgg = subPath + pathSeparator;
                    TreeNode[] nodes = treeView.Nodes.Find(father.Name, true);
                    if (nodes.Length == 0)
                        if (father == null)
                            father = addNode(subPath, subPath, treeView);
                        else
                            father = addNode(subPath, subPath, lastNode);    
                    else
                        lastNode = nodes[0];
                    subPathAgg = subPath;
                }
                lastNode = null; // This is the place code was changed

            }
            /*            tree.Nodes.Clear();
                        List<TreeNode> roots = new List<TreeNode>();
                        roots.Add(tree.Nodes.Add("Items"));
                        foreach (TreeItem item in items)
                        {
                            if (item.Level == roots.Count) roots.Add(roots[roots.Count - 1].LastNode);
                            roots[item.Level].Nodes.Add(item.Name);
                        }*/
        /*}*/
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
                //  node.LinkTo();
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
           // int indexL = 0;
           // int indexR = 0;
            
            if (tree.IsLeft)
            {
              /*  if (this.leftNodes == null)
                {
                    return LTN;
                }*/
                foreach (LinkedTreeNode l in this.leftNodes)
                {
                    if(l.Label.Equals(label) && l.Path.Equals(path))
                    {
                        LTN.Add(l);
                       // LTN[indexL] = l;
                       // indexL++;
                    }
                }
            }
            else
            {
            /*    if(rightNodes == null)
                {
                    return LTN;
                }*/
                foreach (LinkedTreeNode l in rightNodes)
                {
                    if (l.Label.Equals(label) && l.Path.Equals(path))
                    {
                        LTN.Add(l);
                        //LTN[indexR] = l;
                        //indexR++;
                    }
                }
            }

            return LTN;
        }

        //Better use the Find with path in parameters.
        private List<LinkedTreeNode> Find(string label, LinkedTreeView tree = null)
        {
            List<LinkedTreeNode> LTN = new List<LinkedTreeNode>();
            // int indexL = 0;
            // int indexR = 0;

            if (tree.IsLeft)
            {
                /*  if (this.leftNodes == null)
                  {
                      return LTN;
                  }*/
                foreach (LinkedTreeNode l in this.leftNodes)
                {
                    if (l.Label.Equals(label))
                    {
                        LTN.Add(l);
                        // LTN[indexL] = l;
                        // indexL++;
                    }
                }
            }
            else
            {
                /*    if(rightNodes == null)
                    {
                        return LTN;
                    }*/
                foreach (LinkedTreeNode l in rightNodes)
                {
                    if (l.Label.Equals(label))
                    {
                        LTN.Add(l);
                        //LTN[indexR] = l;
                        //indexR++;
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
                //sourceName en path
                //targetName en path 
                //Mijn find function gebruiken om beide te zoeken in de 2 lijsten 
                //source.LinkTo(target)

                
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

            /* int length = sourceItems.Count;
             for (int index = 0; index < length; index++)
             {
                 sourceItems[0].LinkTo(targetItems[0]);
                 MessageBox.Show("link between source: " + sourceItems[0].Name + " - Target: " + targetItems[0].Name);
             }
             linkedTV.Refresh();
             */
            /* int length = sourceItems.Count;

             for(int index = 0; index < length; index++)
             {
                 string leftNameLookup = sourceItems[index].Name;
                 string rightNameLookup = targetItems[index].Name; 
                 TreeNode[] leftN = leftTreeView.Nodes.Find(leftNameLookup, true);
                 TreeNode[] rightN = rightTreeView.Nodes.Find(rightNameLookup, true);

                 if(leftN.Length == 1 && rightN.Length == 1)
                 {
                     leftN[0].
                     //LinkedTreeNode LTN = TreeNode[0];
                 }

                 sourceItems[0].LinkTo(targetItems[0]);
                 MessageBox.Show("link between source: " + sourceItems[0].Name + " - Target: " + targetItems[0].Name);


             }*/

            //            linkedTV.drawAllVisibleLinks()

            /* for
             string sourceName = mappings[0].source.mappedEnd.name;
             string targetName = mappings[0].target.mappedEnd.name;

             TreeNode[] nodes = linkedTV.LeftTree.Nodes.Find(sourceName,true);
             TreeNode[] nodesRight = linkedTV.RightTree.Nodes.Find(targetName, true);


             if(nodes.Length == 1 && nodesRight.Length == 1)
             {
                 LinkedTreeNode left = (LinkedTreeNode)nodes[0];
                 LinkedTreeNode right = (LinkedTreeNode)nodesRight[0];

                 left.LinkTo(right);

                 //paint line
                     //get location of both nodes
                     //paint line*/

            // }

            //linkedTV.Refresh();
        }

    }
    
    // TreeNode with a link to another TreeNode
    
    public class LinkedTreeNode : TreeNode
    {
        public string Path { get; set;  }
        public string Label { get; set; }
        public LinkedTreeNode(string label, string path) : base(label) { Path = path; Label = label; }

        public LinkedTreeNode OtherNode { get; private set; }

        public bool IsLinked { get { return this.OtherNode != null; } }


        public bool HasVisibleLink
        {
            get
            {
                return this.IsLinked && this.IsVisible && this.OtherNode.IsVisible;
            }
        }

        // utility function to manage links

        public LinkedTreeNode LinkTo(LinkedTreeNode otherNode)
        {
            this.Unlink();

            otherNode.Unlink();
            
            if (otherNode != null)
            {
                this.OtherNode = otherNode;
                this.OtherNode.OtherNode = this;
                //TODO: backendcode to connect
            }
            else
            {
                //TODO Unlink code between this and this.OtherNode 
                //Delete the mapping.
            }

            return this;

        }

        public void Unlink()
        {
            if (this.OtherNode == null) { return; }
            this.OtherNode.OtherNode = null;
            this.OtherNode = null;
        }

        // the EndPoint at the outside of the TreeView

        public Point ExternalEndPoint
        {

            get
            {

                LinkedTreeView tree = this.TreeView as LinkedTreeView;

                return new Point(

                  tree.IsLeft ? tree.Right : tree.Left,

                  this.TreeView.Top + this.Bounds.Top + this.Bounds.Height / 2

                );

            }

        }

        // the starting Point next to the Label of the TreeNode

        public Point InternalLabelPoint
        {

            get
            {

                LinkedTreeView tree = this.TreeView as LinkedTreeView;

                return new Point(

                  tree.IsLeft ? this.Bounds.Right : this.Bounds.Left,

                  this.Bounds.Top + this.Bounds.Height / 2

                );

            }

        }

        // the internal counterpart of ExternalEndPoint

        public Point InternalEndPoint
        {

            get
            {

                LinkedTreeView tree = this.TreeView as LinkedTreeView;

                return new Point(

                  tree.IsLeft ? tree.ClientRectangle.Width : 0,

                  this.Bounds.Top + this.Bounds.Height / 2

                );

            }

        }

    }



    // A TreeView with links to another TreeView



    public class LinkedTreeView : TreeView
    {

        // boolean to indicate left/right position vs other LinkedTreeView
        public bool IsLeft { get; set; }
        
        // the other treeview (to check if other end has a selected node)
        private LinkedTreeView otherTree;



        public LinkedTreeView LinkTo(LinkedTreeView otherTree)
        {
            this.otherTree = otherTree;

            this.otherTree.otherTree = this;

            return this;

        }



        internal const int WM_PAINT = 0xF;

        internal const int WM_VSCROLL = 0x0115;



        protected override void WndProc(ref Message m)
        {

            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {

                // draw links for all linked tree nodes

                foreach (LinkedTreeNode node in this.Nodes)
                {
                    this.drawAllVisibles(node);
                }

                // invalidate parent to allow it to re-render the middle part of the link

                this.Parent.Invalidate();

            }

            // if we scroll, we might have made our selected node invisible, make sure

            // the other party is also notified of this

            if (m.Msg == WM_VSCROLL)
            {

                if (this.otherTree != null) { this.otherTree.Invalidate(); }

            }

        }



        // render the end point for this node, and its subnodes

        private void drawAllVisibles(LinkedTreeNode node)
        {

            // only draw when we AND the other end are visible

            if (node.HasVisibleLink)
            {

                this.drawLink(node);

            }

            // recurse down

            foreach (LinkedTreeNode subnode in node.Nodes)
            {

                this.drawAllVisibles(subnode);

            }



        }

        public LinkedTreeView()
        {

            // drag-drop support

            this.AllowDrop = true;

            this.DragDrop += new DragEventHandler(this.handleDragDrop);

            this.DragOver += new DragEventHandler(this.handleDragOver);

            this.MouseDown += new MouseEventHandler(this.handleMouseDown);

            this.AfterSelect += new TreeViewEventHandler(this.handleAfterSelect);

        }



        private void handleDragOver(object sender, DragEventArgs e)
        {

            e.Effect = DragDropEffects.Copy;

        }



        private void handleAfterSelect(object sender, TreeViewEventArgs e)
        {

            this.Capture = false;

            this.otherTree.Capture = false;

        }



        private void handleMouseDown(object sender, MouseEventArgs e)
        {

            LinkedTreeNode node = this.HitTest(e.Location).Node as LinkedTreeNode;

           // if (node != null)
           // {

                this.DoDragDrop(node, DragDropEffects.All);

           // }

        }



        private void handleDragDrop(object sender, DragEventArgs e)
        {

            var source = e.Data.GetData(typeof(LinkedTreeNode)) as LinkedTreeNode;

            if (source != null)
            {

                Point location = this.PointToClient(Cursor.Position);

                LinkedTreeNode target = this.HitTest(location).Node as LinkedTreeNode;

                if (target == null)
                {
                    // seems we're trying to unlink ?
                    source.Unlink();
                }
                else
                {
                    if (source.TreeView == this)
                    {
                        // dropping a node on same treeview could mean: "change this side of 
                        // the link ;-)
                        target.LinkTo(source.OtherNode);
                        //Code to link  in backend code;
                    }
                    else
                    {
                        target.LinkTo(source);
                    }

                }

                this.Invalidate();

                this.otherTree.Invalidate();

            }

        }



        private void drawLink(LinkedTreeNode node)
        {

            Graphics g = this.CreateGraphics();

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen pen = new Pen(Color.FromArgb(255, 128, 128, 128), 3);

            g.DrawLine(pen, node.InternalLabelPoint, node.InternalEndPoint);

        }



        // fake transparancy by inheriting parent background color

        protected override void OnParentChanged(EventArgs e)
        {

            if (this.Parent != null) { this.BackColor = this.Parent.BackColor; }

            base.OnParentChanged(e);

        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {

            this.BackColor = this.Parent.BackColor;

            base.OnParentBackColorChanged(e);

        }

    }



    // two LinkedTreeViews together on a Panel

    public class LinkedTreeViews : Panel
    {
        public LinkedTreeView LeftTree { get; private set; }
        public LinkedTreeView RightTree { get; private set; }
        
        public LinkedTreeViews()
        {

            // create and link the two TreeViews

            this.LeftTree = this.createLeftTree();
            //this.LeftTree.IsLeft = true;

            this.RightTree = this.createRightTree().LinkTo(this.LeftTree);
           // this.RightTree.IsLeft = false;


            // we're a Panel, stretch us to our parents size

            this.Dock = DockStyle.Fill;
            this.AutoSize = true;
            /*
            SuspendLayout();

            Width = 700;
            Height = 500;

            this.Size = new Size(Width, Height);

            this.Location = new Point(ClientSize.Width / 2 - this.Width / 2, ClientSize.Height / 2 - this.Height / 2);
            this.Anchor = AnchorStyles.None;
            this.Dock = DockStyle.None;

            ResumeLayout();*/

            //size of Panel big enough
            //int newWidth = 300;
            //this.MaximumSize = new Size(newWidth, this.Height);
            //this.Size = new Size(newWidth, this.Height);

            // resizing might cause scroll bars to appear and make nodes invisible

            this.Resize += new EventHandler(this.UpdateTrees);



            // make sure links are redrawn

            this.Paint += new PaintEventHandler(this.drawAllVisibleLinks);

        }



        // draw a link between the two TreeViews to link the linked Nodes
        // start by going thgough the links list from one side, e.g. LeftTree

        private void drawAllVisibleLinks(object sender, PaintEventArgs e)

        {

            foreach (LinkedTreeNode node in LeftTree.Nodes)
            {

                this.drawAllVisibleLinks(node);

            }

        }



        private void drawAllVisibleLinks(LinkedTreeNode node)
        {

            if (node.HasVisibleLink)
            {

                this.drawLink((LinkedTreeNode)node);

            }

            // recurse down

            foreach (LinkedTreeNode subnode in node.Nodes)
            {

                this.drawAllVisibleLinks(subnode);

            }

        }



        private void drawLink(LinkedTreeNode node)
        {

            Graphics g = this.CreateGraphics();

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen pen = new Pen(Color.FromArgb(255, 128, 128, 128), 3);

            g.DrawLine(pen, node.ExternalEndPoint, node.OtherNode.ExternalEndPoint);

        }



        private LinkedTreeView createLeftTree()
        {

            var tree = this.createDefaultTree();

            tree.IsLeft = true;

            tree.Dock = DockStyle.Left;

            tree.Scrollable = false;

            tree.RightToLeft = System.Windows.Forms.RightToLeft.Yes;

            return tree;

        }



        private LinkedTreeView createRightTree()
        {

            var tree = this.createDefaultTree();

            tree.Left = 300;
            tree.Scrollable = false;

            tree.IsLeft = false;

            tree.Dock = DockStyle.Right;

            return tree;

        }



        private LinkedTreeView createDefaultTree()
        {

            var tree = new LinkedTreeView();

            tree.BorderStyle = BorderStyle.None;

            tree.Width = 450;

            tree.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.UpdateTrees);
             this.Controls.Add(tree);

            return tree;

        }



        // make sure all three parts of the link are redrawn

        private void UpdateTrees(object sender, EventArgs e)
        {
            this.LeftTree.Invalidate();
            this.RightTree.Invalidate();
            this.Invalidate();

        }
    }
}
