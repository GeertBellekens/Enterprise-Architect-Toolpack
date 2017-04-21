using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System;
using System.Windows.Forms;

namespace EAMapping
{
	/// <summary>
	/// Description of LinkedTreeViews.
	/// </summary>
	public class LinkedTreeViews : Panel
    {
        public LinkedTreeView LeftTree { get; private set; }
        public LinkedTreeView RightTree { get; private set; }
        
        public LinkedTreeViews()
        {
            // create and link the two TreeViews
            this.BackColor = Color.LightGray;
            this.LeftTree = this.createLeftTree();

            this.RightTree = this.createRightTree().LinkTo(this.LeftTree);


            // we're a Panel, stretch us to our parents size

            this.Dock = DockStyle.Fill;
           
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
            tree.BackColor = Color.White;
            return tree;

        }



        private LinkedTreeView createRightTree()
        {

            var tree = this.createDefaultTree();

            tree.Left = 300;
            tree.Scrollable = false;

            tree.IsLeft = false;

            tree.Dock = DockStyle.Right;

            tree.BackColor = Color.White;
            return tree;

        }



        private LinkedTreeView createDefaultTree()
        {

            var tree = new LinkedTreeView();
            tree.BorderStyle = BorderStyle.None;

            int w = Screen.PrimaryScreen.Bounds.Width;
 //           Screen.PrimaryScreen.Bounds.Height;
            tree.Width = w/7;

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
