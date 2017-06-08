using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System;
using System.Windows.Forms;
using MappingFramework;

namespace EAMapping
{
	/// <summary>
	/// Description of LinkedTreeViews.
	/// </summary>
	public class LinkedTreeViews : Panel
    {
        public LinkedTreeView LeftTree { get; private set; }
        public LinkedTreeView RightTree { get; private set; }
        private System.ComponentModel.ComponentResourceManager resources;
        List<MappingLine> mappingLines {get;set;}
        public Mapping selectedMapping {get;private set;}
        
        private Color defaultLineColor = Color.FromArgb(255, 128, 128, 128);
        private Color selectedLineColor = Color.DarkBlue;
        
        public LinkedTreeViews()
        {
        	//use the resources for images
        	resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkedTreeViews));
            // create and link the two TreeViews
            this.BackColor = Color.LightGray;
            this.LeftTree = this.createLeftTree();
            this.RightTree = this.createRightTree().LinkTo(this.LeftTree);

            // we're a Panel, stretch us to our parents size

            this.Dock = DockStyle.Fill;
            //events
            this.Resize += new EventHandler(this.UpdateTrees);
            this.MouseClick += this.mappingLineClicked;


            // make sure links are redrawn

            this.Paint += new PaintEventHandler(this.drawAllVisibleLinks);

        }



        // draw a link between the two TreeViews to link the linked Nodes
        // start by going thgough the links list from one side, e.g. LeftTree

        private void drawAllVisibleLinks(object sender, PaintEventArgs e)

        {
        	this.mappingLines = new List<MappingLine>();

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

            } else {
              if(node.Tag != null) {
                this.Controls.Remove((PictureBox)node.Tag);
                node.Tag = null;
              }
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
    	MappingLine line = new MappingLine(defaultLineColor,3,node.ExternalEndPoint, node.OtherNode.ExternalEndPoint,node.mapping);
    	this.mappingLines.Add(line);
    	line.Draw(g);
      if (node.mapping != null && node.mapping.mappingLogic != null) {
        if(node.Tag != null) {
          // MappingLogicIcon is already showing, move it
          this.moveMappingLogicIcon(
            (PictureBox)node.Tag,
            node.ExternalEndPoint,
            node.OtherNode.ExternalEndPoint
          );
        } else {
          // create a new marker
          node.Tag = this.drawMappingLogicIcon(
            node.ExternalEndPoint,
            node.OtherNode.ExternalEndPoint,
            node.mapping
          );
        }
      }
    }

		void mappingLineClicked(object sender, MouseEventArgs e)
		{
			Graphics g = this.CreateGraphics();
        	g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			foreach(var mappingLine in this.mappingLines)
			{
				if (mappingLine.HitTest(e.Location))
				{
					mappingLine.LineColor = selectedLineColor;
					this.selectedMapping = mappingLine.mapping;
				}
				else
				{
					mappingLine.LineColor = defaultLineColor;
				}
				mappingLine.Draw(g);
			}	
		}

		public event EventHandler showMapping = delegate { }; 
		void showMappingClick(object sender, EventArgs e) {
			showMapping((Mapping)((PictureBox)sender).Tag, e);
		}
		
		PictureBox drawMappingLogicIcon(Point start, Point end, MappingFramework.Mapping mapping)
		{
			var logicIcon = ((System.Drawing.Image)(resources.GetObject("Mapping Logic icon")));
			var pictureBox = new PictureBox();
			((System.ComponentModel.ISupportInitialize)(pictureBox)).BeginInit();
			pictureBox.Image = logicIcon;
			pictureBox.BackColor = Color.Transparent;
			pictureBox.Size = new System.Drawing.Size(24, 24);
      this.moveMappingLogicIcon(pictureBox, start, end);
			pictureBox.Tag = mapping;
			ToolTip logicTooltip = new ToolTip();
			logicTooltip.SetToolTip(pictureBox,mapping.mappingLogic.description);
			this.Controls.Add(pictureBox);
			((System.ComponentModel.ISupportInitialize)(pictureBox)).EndInit();
      pictureBox.DoubleClick += showMappingClick;
      return pictureBox;
		}
    
    void moveMappingLogicIcon(PictureBox box, Point start, Point end) {
      Point location = new System.Drawing.Point(
         end.X - start.X,
        (end.Y + start.Y)/2 - box.Size.Height / 2
      );
      box.Location = location;
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

            tree.AfterCollapse += new TreeViewEventHandler(this.UpdateTrees);
            tree.AfterExpand   += new TreeViewEventHandler(this.UpdateTrees);

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
