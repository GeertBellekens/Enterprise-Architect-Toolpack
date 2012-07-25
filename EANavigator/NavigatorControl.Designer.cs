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
			this.navigatorContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openPropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.navigatorContextMenu.SuspendLayout();
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
			this.NavigatorTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NavigatorTreeNodeMouseClick);
			this.NavigatorTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NavigatorTreeNodeMouseDoubleClick);
			// 
			// NavigatorImageList
			// 
			this.NavigatorImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("NavigatorImageList.ImageStream")));
			this.NavigatorImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.NavigatorImageList.Images.SetKeyName(0, "dummy.png");
			this.NavigatorImageList.Images.SetKeyName(1, "Attribute.png");
			this.NavigatorImageList.Images.SetKeyName(2, "Element.png");
			this.NavigatorImageList.Images.SetKeyName(3, "Operation.png");
			this.NavigatorImageList.Images.SetKeyName(4, "Diagram.png");
			this.NavigatorImageList.Images.SetKeyName(5, "Package_element.png");
			this.NavigatorImageList.Images.SetKeyName(6, "Primitive.png");
			this.NavigatorImageList.Images.SetKeyName(7, "Message.png");
			this.NavigatorImageList.Images.SetKeyName(8, "Action.png");
			this.NavigatorImageList.Images.SetKeyName(9, "SequenceDiagram.png");
			this.NavigatorImageList.Images.SetKeyName(10, "Class.png");
			this.NavigatorImageList.Images.SetKeyName(11, "StateMachine.png");
			this.NavigatorImageList.Images.SetKeyName(12, "Interaction.png");
			this.NavigatorImageList.Images.SetKeyName(13, "Activity.png");
			this.NavigatorImageList.Images.SetKeyName(14, "TaggedValue.png");
			this.NavigatorImageList.Images.SetKeyName(15, "AttributeTag.png");
			this.NavigatorImageList.Images.SetKeyName(16, "ElementTag.png");
			this.NavigatorImageList.Images.SetKeyName(17, "OperationTag.png");
			this.NavigatorImageList.Images.SetKeyName(18, "RelationTag.png");
			this.NavigatorImageList.Images.SetKeyName(19, "Parameter.png");
			this.NavigatorImageList.Images.SetKeyName(20, "Package.png");
			this.NavigatorImageList.Images.SetKeyName(21, "Package_action.png");
			this.NavigatorImageList.Images.SetKeyName(22, "Package_attribute.png");
			this.NavigatorImageList.Images.SetKeyName(23, "Package_operation.png");
			this.NavigatorImageList.Images.SetKeyName(24, "Package_parameter.png");
			this.NavigatorImageList.Images.SetKeyName(25, "Package_sequenceDiagram.png");
			this.NavigatorImageList.Images.SetKeyName(26, "Package_taggedValue.png");
			this.NavigatorImageList.Images.SetKeyName(27, "ParameterTag.png");
			this.NavigatorImageList.Images.SetKeyName(28, "Rootpackage.png");
			this.NavigatorImageList.Images.SetKeyName(29, "CommunicationDiagram.png");
			this.NavigatorImageList.Images.SetKeyName(30, "Enumeration.png");
			this.NavigatorImageList.Images.SetKeyName(31, "DataType.png");
			// 
			// navigatorContextMenu
			// 
			this.navigatorContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.openPropertiesMenuItem,
									this.selectBrowserMenuItem});
			this.navigatorContextMenu.Name = "navigatorContextMenu";
			this.navigatorContextMenu.Size = new System.Drawing.Size(204, 70);
			// 
			// openPropertiesMenuItem
			// 
			this.openPropertiesMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openPropertiesMenuItem.Image")));
			this.openPropertiesMenuItem.Name = "openPropertiesMenuItem";
			this.openPropertiesMenuItem.Size = new System.Drawing.Size(203, 22);
			this.openPropertiesMenuItem.Text = "Open Properties";
			this.openPropertiesMenuItem.Click += new System.EventHandler(this.OpenPropertiesMenuItemClick);
			// 
			// selectBrowserMenuItem
			// 
			this.selectBrowserMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("selectBrowserMenuItem.Image")));
			this.selectBrowserMenuItem.Name = "selectBrowserMenuItem";
			this.selectBrowserMenuItem.Size = new System.Drawing.Size(203, 22);
			this.selectBrowserMenuItem.Text = "Select in Project Browser";
			this.selectBrowserMenuItem.Click += new System.EventHandler(this.SelectBrowserMenuItemClick);
			// 
			// NavigatorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.NavigatorTree);
			this.Name = "NavigatorControl";
			this.Size = new System.Drawing.Size(407, 227);
			this.navigatorContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ToolStripMenuItem selectBrowserMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openPropertiesMenuItem;
		private System.Windows.Forms.ContextMenuStrip navigatorContextMenu;
		private System.Windows.Forms.ImageList NavigatorImageList;
		private System.Windows.Forms.TreeView NavigatorTree;
		

	}
}
