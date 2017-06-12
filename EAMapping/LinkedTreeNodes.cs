using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using MappingFramework;

namespace EAMapping {
  
  // LinkedTreeNodes represents one instance of two connected LinkedTreeNodes
  // It's the LinkedTreeViews equivalent of a Mapping
  // It managed the two LinkedTreeNodes, the two related LinkedTreeViews and
  // the visualisation between the TreeNodes. It also holds the reference to the
  // EA objects via the Mapping.
	    
  public class LinkedTreeNodes {

    public Mapping Mapping { get; private set; }

    private LinkedTreeViews trees;

    private LinkedTreeNode sourceNode;
    private LinkedTreeNode targetNode;

    private Point start { get { return this.sourceNode.ExternalEndPoint; } }
    private Point end   { get { return this.targetNode.ExternalEndPoint; } }

    private PictureBox icon;

    public  bool IsSelected { get; private set; } = false;

    private int width;

    public LinkedTreeNodes(LinkedTreeViews tree, Mapping mapping ) {
      this.Mapping = mapping;
      this.trees   = tree;

      this.sourceNode = this.trees.SourceTree.AddNode(
        this.Mapping.source.fullMappingPath.Split('.').ToList()
      );
      this.targetNode = this.trees.TargetTree.AddNode(
        this.Mapping.target.fullMappingPath.Split('.').ToList()
      );
    }

    public override string ToString() {
      return this.sourceNode.Text + " - " + this.targetNode.Text;
    }

    // This draws the part of the link between the source and target trees.
    // This is on the LinkedTreeViews central "Panel" part.
    public void Draw(Color color, int width) {
      Graphics g = this.trees.CreateGraphics();
      // if any of the end points is not visible, we don't draw the line/icon
      if( ! this.sourceNode.IsVisible || ! this.targetNode.IsVisible) {
        if(this.icon != null) {
          this.icon.Visible = false;
        }
        return;
      }

      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      this.width = width;

      // draw the line in between the two LinkedTreeView-s
      g.DrawLine(new Pen(color, width),
        this.sourceNode.ExternalEndPoint, this.targetNode.ExternalEndPoint
      );
      // add an icon if mapping logic is available
      if ( this.Mapping != null && this.Mapping.mappingLogic != null) {
        this.drawMappingLogicIcon();
      } else {
        this.removeMappingLogicIcon();
      }

      // draw the "handles" next to the LinkedTreeNodes
      // TODO this redraws every handle for every Mapping, might be optimized
      this.sourceNode.Draw(color, width);
      this.targetNode.Draw(color, width);
    }

    private void drawMappingLogicIcon()  {
      // if we don't have an icon for this mapping yet, create it
      if(this.icon == null) {
        this.icon = new PictureBox();
        ((System.ComponentModel.ISupportInitialize)this.icon).BeginInit();
        this.icon.Image     =
          (Image)this.trees.resources.GetObject("Mapping Logic icon");
        this.icon.BackColor = Color.Transparent;
        this.icon.Size      = new System.Drawing.Size(24, 24);
        ToolTip logicTooltip = new ToolTip();
        logicTooltip.SetToolTip(this.icon,
          this.Mapping.mappingLogic.description
        );
        this.trees.Controls.Add(this.icon);
        ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();

        this.icon.Click       += handleIconClick;
        this.icon.DoubleClick += handleIconDoubleClick;
      }

      // always make sure it's in the right place ;-)
      this.icon.Location = new Point(
        this.start.X + (this.end.X - this.start.X)/2 - this.icon.Size.Width/2,
       (this.end.Y + this.start.Y)/2 - this.icon.Size.Height/2
      );

      // and it is showing
      this.icon.Visible = true;
    }

    private void removeMappingLogicIcon() {
      this.trees.Controls.Remove(this.icon);
      this.icon = null;
    }

    private void handleIconClick(object sender, EventArgs args) {
      this.toggleMappingSelection();
    }

    private void handleIconDoubleClick(object sender, EventArgs args) {
      this.trees.Show(this.Mapping);
    }

    public bool Select() {
      if(! this.IsSelected ) {
        var selected = this.trees.SelectedLink;
        if(selected != null) { selected.Unselect(); }
        this.IsSelected = true;
        this.trees.Invalidate(); // make sure changes are "redrawn"
      }
      return true;
    }

    public bool Unselect() {
      if(this.IsSelected) {
        this.IsSelected = false;
        this.trees.Invalidate(); // make sure changes are "redrawn"
      }
      return false;
    }

    private bool toggleMappingSelection() {
      return this.IsSelected ? this.Unselect() : this.Select();
    }

    public bool Hit(Point hit) {
      // outside of bounding box -> no hit, we are not selected
      if( hit.X < this.start.X || hit.X > this.end.X
       || hit.Y < Math.Min(this.start.Y, this.end.Y)
       || hit.Y > Math.Max(this.start.Y, this.end.Y) )
      {
        return this.Unselect();
      }

      // inside the bounding box -> hit if within a distance of the line
      // TODO ? make distance bigger for easier hitting?
      int dx      = this.end.X - this.start.X;
      int dy      = this.end.Y - this.start.Y;
      int z       = dy * hit.X - dx * hit.Y 
                  + this.start.Y * this.end.X
                  - this.start.X * this.end.Y;
      int n       = dy * dy + dx * dx;
      double dist = Math.Abs(z) / Math.Sqrt(n);

      return dist < this.width ? this.Select() : this.Unselect();
    }
  }
}
