using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMapping.Custom_User_Controls
{
    public class PTreeView : TreeView
    {
        public bool IsLeft { get; set; }
        public int BorderWidth { get; private set; }
        private float slope { get; set; }
        private Point Pt { get; set; }

        public PTreeView() { }

        public void markNode(TreeNode node, float slope_)
        {
            if (this.IsLeft) Pt =
            new Point(node.Bounds.Right, node.Bounds.Top + node.Bounds.Height / 2);
            else Pt = new Point(node.Bounds.Left, node.Bounds.Top + node.Bounds.Height / 2);
            slope = slope_;
            BorderWidth = (this.Width - this.ClientRectangle.Width) / 2;
        }

        internal const int WM_PAINT = 0xF;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                Graphics G = this.CreateGraphics();
                G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                int px = IsLeft ? this.ClientRectangle.Width : 0;
                int py = (int)(Pt.Y + slope * (Pt.X - px));
                Point p0 = new Point(px, py);

                G.DrawLine(Pens.Coral, Pt, p0);
            }
        }
    }
}
