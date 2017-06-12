using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System;
using System.Windows.Forms;

namespace EAMapping {

  // A LinkedTreeView extends the basic TreeView with functionality supporting
  // the concept of two LinkedTreeViews. A "leaf-"item in the Tree is a 
  // LinkedTreeNode, all other/non-leaf nodes are regular TreeNodes.

  public class LinkedTreeView : TreeView {

    // boolean to indicate left/right position vs other LinkedTreeView
    public bool IsLeft { get; set; }
    
    // the other treeview (to check if other end has a selected node)
    private LinkedTreeView otherTree;

    private LinkedTreeViews trees;

    public LinkedTreeView LinkTo(LinkedTreeView otherTree) {
      this.otherTree = otherTree;
      this.otherTree.otherTree = this;
      return this;
    }

    public LinkedTreeView(LinkedTreeViews trees) {
      this.trees        = trees;
      this.AllowDrop    = true;
      this.ItemDrag    += new ItemDragEventHandler(this.handleItemDrag);
      this.DragDrop    += new DragEventHandler(this.handleDragDrop);
      this.DragOver    += new DragEventHandler(this.handleDragOver);
      this.AfterSelect += new TreeViewEventHandler(this.handleAfterSelect);
    }

    // mapping between path name and corresponding node
    private Dictionary<string,TreeNode> nodes =
      new Dictionary<string,TreeNode>();

    public void Clear() {
      this.nodes.Clear();
    }

    public LinkedTreeNode AddNode(List<string> path) {
      string fqn  = string.Join(".", path);
      string name = path[path.Count -1];
      path.RemoveAt(path.Count-1);
      if( ! this.nodes.ContainsKey(fqn) ) {
        this.nodes.Add( fqn,
          this.createLinkedTreeNode( name, this.createPath(path) )
        );
      }
      return this.nodes[fqn] as LinkedTreeNode;
    }
    
    // creates a path of TreeNodes, reusing existing subpaths, building parent-
    // paths in a recursive way
    private TreeNode createPath(List<string> path) {
      if(path.Count == 0) { return null; } // terminate recursion
      var fqp = string.Join(".", path);
      if( ! this.nodes.ContainsKey(fqp) ) {
        // full path doesn't exist yet, try popping of the leaf and create the
        // parent-path up to there
        var name   = path[path.Count-1];
        var parent = this.createPath(path.Take(path.Count - 1).ToList());
        var node   = this.createTreeNode(name, parent);
        this.nodes.Add(fqp, node);
      }
      return this.nodes[fqp];
    }

    private TreeNode createTreeNode(string name, TreeNode parent) {
      var node = new TreeNode(name);
      if(parent != null) {
        parent.Nodes.Add(node);
      } else {
        this.Nodes.Add(node);
      }
      return node;
    }
    
    private LinkedTreeNode createLinkedTreeNode(string name, TreeNode parent) {
      var node = new LinkedTreeNode(name);
      if(parent != null) {
        parent.Nodes.Add(node);
      } else {
        this.Nodes.Add( node);
      }
      return node;
    }

    private void handleItemDrag(object sender, ItemDragEventArgs e) {
      this.SelectedNode = e.Item as LinkedTreeNode;
      this.DoDragDrop(e.Item, DragDropEffects.All);
    }

    private void handleDragOver(object sender, DragEventArgs e) {
      e.Effect = DragDropEffects.Copy;
    }

    private void handleDragDrop(object sender, DragEventArgs e) {
      var source = e.Data.GetData(typeof(LinkedTreeNode)) as LinkedTreeNode;
      if (source != null) {
        Point location = this.PointToClient(Cursor.Position);
        var target = this.HitTest(location).Node as LinkedTreeNode;
        this.SelectedNode = target;
        if(target != null && source.TreeView != this) {
          this.trees.Link(source, target);
        }
      }
    }

    private void handleAfterSelect(object sender, TreeViewEventArgs e) {
      this.Capture = false;
      this.otherTree.Capture = false;
      this.trees.Invalidate();
    }

    // fake transparancy by inheriting parent background color

    protected override void OnParentChanged(EventArgs e) {
      if (this.Parent != null) { this.BackColor = this.Parent.BackColor; }
      base.OnParentChanged(e);
    }

    protected override void OnParentBackColorChanged(EventArgs e) {
      this.BackColor = this.Parent.BackColor;
      base.OnParentBackColorChanged(e);
    }

  }
}
