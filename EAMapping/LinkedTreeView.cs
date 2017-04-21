using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System;
using System.Windows.Forms;

namespace EAMapping
{
	/// <summary>
	/// Description of LinkedTreeView.
	/// </summary>
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

                var target = this.HitTest(location).Node as LinkedTreeNode;

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

}
