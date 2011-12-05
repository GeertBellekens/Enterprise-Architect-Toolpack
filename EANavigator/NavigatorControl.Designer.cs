/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 30/11/2011
 * Time: 4:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace TSF.UmlToolingFramework.EANavigator
{
	partial class NavigatorControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigatorControl));
			this.NavigatorTree = new System.Windows.Forms.TreeView();
			this.NavigatorImageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// NavigatorTree
			// 
			this.NavigatorTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.NavigatorTree.ImageIndex = 0;
			this.NavigatorTree.ImageList = this.NavigatorImageList;
			this.NavigatorTree.Location = new System.Drawing.Point(4, 4);
			this.NavigatorTree.Name = "NavigatorTree";
			this.NavigatorTree.SelectedImageIndex = 0;
			this.NavigatorTree.ShowNodeToolTips = true;
			this.NavigatorTree.Size = new System.Drawing.Size(400, 220);
			this.NavigatorTree.TabIndex = 0;
			this.NavigatorTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.NavigatorTreeBeforeExpand);
			this.NavigatorTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NavigatorTreeNodeMouseDoubleClick);
			// 
			// NavigatorImageList
			// 
			this.NavigatorImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("NavigatorImageList.ImageStream")));
			this.NavigatorImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.NavigatorImageList.Images.SetKeyName(0, "dummy.png");
			this.NavigatorImageList.Images.SetKeyName(1, "Attribute.png");
			this.NavigatorImageList.Images.SetKeyName(2, "Class.png");
			this.NavigatorImageList.Images.SetKeyName(3, "Operation.png");
			this.NavigatorImageList.Images.SetKeyName(4, "SequenceDiagram.png");
			this.NavigatorImageList.Images.SetKeyName(5, "Folder.png");
			this.NavigatorImageList.Images.SetKeyName(6, "Primitive.png");
			this.NavigatorImageList.Images.SetKeyName(7, "Message.png");
			this.NavigatorImageList.Images.SetKeyName(8, "Action.png");
			// 
			// NavigatorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.NavigatorTree);
			this.Name = "NavigatorControl";
			this.Size = new System.Drawing.Size(407, 227);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ImageList NavigatorImageList;
		private System.Windows.Forms.TreeView NavigatorTree;
	}
}
