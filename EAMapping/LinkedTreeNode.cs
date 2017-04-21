using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MappingFramework;

namespace EAMapping
{
	/// <summary>
	/// Description of LinkedTreeNode.
	/// </summary>
	    
    public class LinkedTreeNode : TreeNode
    {
        public string Path { get; set;  }
        public string Label { get; set; }
        public Mapping mapping {get;set;}
        public LinkedTreeNode(string label, string path) : base(label) 
        { 	
        	Path = path;
        	Label = label;
        }
        public LinkedTreeNode(string label, string path, Mapping mapping) : this(label, path)
        { 	
        	this.mapping = mapping;
        }

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

}
