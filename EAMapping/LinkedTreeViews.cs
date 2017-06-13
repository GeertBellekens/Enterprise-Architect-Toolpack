using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System;
using System.Windows.Forms;

using MappingFramework;
using AM = EAAddinFramework.Mapping;

namespace EAMapping {

  // The LinkedTreeViews Control is a Panel with two LinkedTreeViews
  // It visualizes a set/list of Mappings and exposes events that require
  // changes to these mappings.

	public class LinkedTreeViews : Panel {
    public LinkedTreeView SourceTree { get; private set; }
    public LinkedTreeView TargetTree { get; private set; }

    public ComponentResourceManager resources { get; private set; }

    // a mapping between Mapping and its realisation as LinkedTreeNodes
    private Dictionary<Mapping, LinkedTreeNodes> links =
      new Dictionary<Mapping,LinkedTreeNodes>();

    public  List<Mapping> Mappings {
      set {
        this.Clear();
        foreach(Mapping mapping in value) {
          // TODO ?
          if (mapping.source.fullMappingPath.Contains("Target")) {
            MessageBox.Show("target spotted");
          } else {
            this.addMapping(mapping);
          }
        }
      }
    }

    public LinkedTreeNodes SelectedLink {
      get {
        return this.links.Values.FirstOrDefault(link => link.IsSelected);
      }
    }

    public LinkedTreeViews() {
    	this.resources   = new ComponentResourceManager(typeof(LinkedTreeViews));

      this.SourceTree  = this.createSourceTree();
      this.TargetTree  = this.createTargetTree().LinkTo(this.SourceTree);

      this.Dock        = DockStyle.Fill;
      this.BackColor   = Color.LightGray;

      this.Resize     += new EventHandler(this.updateTrees);
      this.MouseClick += this.hitLinks;

      // this panel renders the lines connecting the two LinkedTreeViews
      this.Paint      += new PaintEventHandler(this.drawAllVisibleLinks);
    }

    // EVENTS
    // delete events can't be triggered using other than using the toolbar
    // creation can be triggered by drag and drop, edit by double click
    public event Action<Mapping> CreateMapping    = delegate { }; 
		public event Action<Mapping> EditMappingLogic = delegate { };

    public LinkedTreeNodes Link(LinkedTreeNode source, LinkedTreeNode target) {
      // create new mapping from link information
      var mapping = new AM.TaggedValueMapping(
        (AM.MappingEnd)source.MappedEnd, (AM.MappingEnd)target.MappedEnd
      );
      this.addMapping(mapping);

      // raise Event
      this.CreateMapping(mapping);

      // TODO? invalidated to re-render new links ?
      this.Invalidate();
      return null;
    }

    public void EditMapping(Mapping mapping) {
      this.EditMappingLogic(mapping);
    }

    private LinkedTreeNodes addMapping(Mapping mapping) {
      LinkedTreeNodes link = new LinkedTreeNodes(this, mapping);
      this.links.Add(mapping, link);
      return link;
    }

    public void DeleteMapping(Mapping mapping) {
      if(! this.links.ContainsKey(mapping)) { return; }
      this.links[mapping].Delete();
      this.links.Remove(mapping);
      this.Invalidate();
      this.SourceTree.Invalidate();
      this.TargetTree.Invalidate();
    }

    public void Clear() {
      this.SourceTree.Clear();
      this.TargetTree.Clear();
    }

    public void ExpandAll() {
			this.TargetTree.ExpandAll();
			this.SourceTree.ExpandAll();
    }

    private Color defaultLineColor  = Color.FromArgb(255, 128, 128, 128);
    private Color selectedLineColor = Color.DarkBlue;
    private int   lineWidth         = 3;
    
    // request all links to draw themselves, passing our own graphicscontext
    private void drawAllVisibleLinks(object sender, PaintEventArgs e) {
      // first draw all links
      foreach(var link in this.links.Values) {
        if( ! link.IsSelected) {
          link.Draw(defaultLineColor, lineWidth);
        }
      }
      // finally (re)draw the selected link (ensures the handles are selected)
      if(this.SelectedLink != null) {
        this.SelectedLink.Draw(selectedLineColor, lineWidth);
      }
    }

    private LinkedTreeView createSourceTree() {
      var tree = this.createBaseTree();
      tree.IsLeft      = true;
      tree.Dock        = DockStyle.Left;
      tree.Scrollable  = false;
      tree.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      tree.BackColor   = Color.White;
      return tree;
    }

    private LinkedTreeView createTargetTree() {
      var tree = this.createBaseTree();
      tree.Left       = 300;
      tree.Scrollable = false;
      tree.IsLeft     = false;
      tree.Dock       = DockStyle.Right;
      tree.BackColor  = Color.White;
      return tree;
    }

    private LinkedTreeView createBaseTree() {
      var tree = new LinkedTreeView(this);
      tree.BorderStyle    = BorderStyle.None;
      tree.Width          = Screen.PrimaryScreen.Bounds.Width / 7;
      tree.AfterCollapse += new TreeViewEventHandler(this.updateTrees);
      tree.AfterExpand   += new TreeViewEventHandler(this.updateTrees);
      tree.HideSelection  = false;
      this.Controls.Add(tree);
      return tree;
    }

    private void updateTrees(object sender, EventArgs e) {
      this.SourceTree.Invalidate();
      this.TargetTree.Invalidate();
      this.Invalidate();
    }

    private void hitLinks(object sender, EventArgs e) {
      Point location = this.PointToClient(Cursor.Position);
      foreach(var link in this.links.Values) {
        link.Hit(location);
      }
    }
  }
}
