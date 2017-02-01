
using MappingFramework;
using EAMapping.Custom_User_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EAMapping
{
	/// <summary>
	/// Description of MappingControl.
	/// </summary>
	public partial class MappingControl : UserControl
	{
        
        ModelSetContainer modelSetSource;
        ModelSetContainer modelSetTarget;
        Point p1 = Point.Empty;
        Point p2 = Point.Empty;
        System.Drawing.Graphics graphics;
        public MappingControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //

            
            
		}
		private MappingFramework.MappingSet _mappingSet;

		public void loadMappingSet(MappingFramework.MappingSet mappingSet)
		{
            List<Mapping> mappingList = mappingSet.mappings;
            List<TreeItem> sourceItems = new List<TreeItem>();
            List<TreeItem> targetItems = new List<TreeItem>();
            graphics = this.CreateGraphics();
            foreach(Mapping map in mappingList)
            {
                sourceItems.Add(new TreeItem(map.source.fullMappingPath));
                targetItems.Add(new TreeItem(map.target.fullMappingPath));
            }

            modelSetSource = new ModelSetContainer("Source", sourceItems);
            modelSetTarget = new ModelSetContainer("Target", targetItems);
            modelSetSource.Location = new Point(0, 0);
            modelSetTarget.Location = new Point(800, 0);
            this.Controls.Add(modelSetSource);
            this.Controls.Add(modelSetTarget);
            modelSetSource.tv.IsLeft = true;
            modelSetTarget.tv.IsLeft = false;
            modelSetSource.tv.NodeMouseClick += new TreeNodeMouseClickEventHandler(modelSetSource_NodeMouseClick);
            this.Paint += new PaintEventHandler(panel1_Paint);
            this.Show();
            
        }


        private void modelSetSource_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode n1 = e.Node;
            //TreeNode[] col =  modelSetTarget.tv.Nodes.Find("Test",true);
            TreeNode n2 = modelSetTarget.tv.Nodes[0];
            modelSetTarget.tv.SelectedNode = n2;

            p1 = new Point(
                  modelSetSource.tv.Left + n2.Bounds.Left + n1.Bounds.Width + modelSetSource.tv.BorderWidth,
                  modelSetSource.tv.Top + n1.Bounds.Top + n1.Bounds.Height / 2 + modelSetSource.tv.BorderWidth);
            p2 = new Point(
                  modelSetTarget.tv.Left + n2.Bounds.Left + modelSetTarget.tv.BorderWidth,
                  modelSetTarget.tv.Top + n2.Bounds.Top + n2.Bounds.Height / 2 + modelSetTarget.tv.BorderWidth);

            float slope = -1f * (p2.Y - p1.Y) / (p2.X - p1.X);
            modelSetSource.tv.markNode(n1, slope);
            modelSetTarget.tv.markNode(n2, slope);

            this.Invalidate();
            modelSetSource.tv.Invalidate();
            modelSetTarget.tv.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawLine(Pens.Coral, p1, p2);
            graphics.DrawLine(Pens.Coral, p1, p2);
            graphics.DrawRectangle(Pens.Coral, 5, 5, 200, 200);

            //DrawLine(Pens.Coral, p1, p2);




        }
        internal const int WM_PAINT = 0xF;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                Graphics G = this.CreateGraphics();
                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                

                G.DrawLine(Pens.Coral, p1, p2);
            }
        }
    }
}
