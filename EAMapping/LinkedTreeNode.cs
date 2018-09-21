using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MappingFramework;

namespace EAMapping {
  
  // A LinkedTreeNode is a node in a LinkedTreeView
  // It can be linked to multiple (other) nodes in the other LinkedTreeView
  // It provides properties with geometric information for the LinkedTreeView
  // to render additional links
	    
  public class LinkedTreeNode : TreeNode {

    public LinkedTreeNode(string label) : base(label) {}

    public MappingNode MappedEnd { get; set; }

    // the EndPoint at the outside of the TreeView
    public Point ExternalEndPoint {
      get {
        LinkedTreeView tree = this.TreeView as LinkedTreeView;
        return new Point(
          tree.IsLeft ? tree.Right : tree.Left,
          this.TreeView.Top + this.Bounds.Top + this.Bounds.Height / 2
        );
      }
    }

    public void Draw(Color color, int width) {
      Graphics g = this.TreeView.CreateGraphics();
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      g.DrawLine(
        new Pen(color, width),
        this.InternalLabelPoint,
        this.InternalEndPoint
      );
    }

    // the starting Point next to the Label of the TreeNode
    public Point InternalLabelPoint {
      get {
        LinkedTreeView tree = this.TreeView as LinkedTreeView;
        return new Point(
          tree.IsLeft ? this.Bounds.Right : this.Bounds.Left,
          this.Bounds.Top + this.Bounds.Height / 2
        );
      }
    }

    // the EndPoint at the inside of the TreeView
    public Point InternalEndPoint {
      get {
        LinkedTreeView tree = this.TreeView as LinkedTreeView;
        return new Point(
          tree.IsLeft ? tree.ClientRectangle.Width : 0,
          this.Bounds.Top + this.Bounds.Height / 2
        );
      }
    }

  }
}
