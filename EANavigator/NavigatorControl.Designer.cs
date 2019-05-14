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
            this.navigatorContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openPropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectBrowserMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToDiagramMenuOption = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyGUIDMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigatorToolStrip = new System.Windows.Forms.ToolStrip();
            this.openInNavigatorButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.projectBrowserButton = new System.Windows.Forms.ToolStripButton();
            this.propertiesButton = new System.Windows.Forms.ToolStripButton();
            this.addToDiagramButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.fqnButton = new System.Windows.Forms.ToolStripButton();
            this.guidButton = new System.Windows.Forms.ToolStripButton();
            this.copyGUIDButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsButton = new System.Windows.Forms.ToolStripButton();
            this.aboutButton = new System.Windows.Forms.ToolStripButton();
            this.navigatorToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.quickSearchBox = new TSF.UmlToolingFramework.EANavigator.QuickSearchBox();
            this.navigatorContextMenu.SuspendLayout();
            this.navigatorToolStrip.SuspendLayout();
            this.navigatorToolStripContainer.ContentPanel.SuspendLayout();
            this.navigatorToolStripContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // NavigatorTree
            // 
            this.NavigatorTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NavigatorTree.Location = new System.Drawing.Point(0, 0);
            this.NavigatorTree.Name = "NavigatorTree";
            this.NavigatorTree.ShowNodeToolTips = true;
            this.NavigatorTree.Size = new System.Drawing.Size(407, 224);
            this.NavigatorTree.TabIndex = 0;
            this.NavigatorTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.NavigatorTreeBeforeExpand);
            this.NavigatorTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NavigatorTreeNodeMouseClick);
            this.NavigatorTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NavigatorTreeNodeMouseDoubleClick);
            // 
            // navigatorContextMenu
            // 
            this.navigatorContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPropertiesMenuItem,
            this.selectBrowserMenuItem,
            this.addToDiagramMenuOption,
            this.optionsMenuItem,
            this.copyGUIDMenuItem});
            this.navigatorContextMenu.Name = "navigatorContextMenu";
            this.navigatorContextMenu.Size = new System.Drawing.Size(204, 114);
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
            // addToDiagramMenuOption
            // 
            this.addToDiagramMenuOption.Image = ((System.Drawing.Image)(resources.GetObject("addToDiagramMenuOption.Image")));
            this.addToDiagramMenuOption.Name = "addToDiagramMenuOption";
            this.addToDiagramMenuOption.Size = new System.Drawing.Size(203, 22);
            this.addToDiagramMenuOption.Text = "Add to Diagram";
            this.addToDiagramMenuOption.Click += new System.EventHandler(this.AddToDiagramMenuOptionClick);
            // 
            // optionsMenuItem
            // 
            this.optionsMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("optionsMenuItem.Image")));
            this.optionsMenuItem.Name = "optionsMenuItem";
            this.optionsMenuItem.Size = new System.Drawing.Size(203, 22);
            this.optionsMenuItem.Text = "Options";
            this.optionsMenuItem.Click += new System.EventHandler(this.OptionsMenuItemClick);
            // 
            // copyGUIDMenuItem
            // 
            this.copyGUIDMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyGUIDMenuItem.Image")));
            this.copyGUIDMenuItem.Name = "copyGUIDMenuItem";
            this.copyGUIDMenuItem.Size = new System.Drawing.Size(203, 22);
            this.copyGUIDMenuItem.Text = "Copy GUID";
            this.copyGUIDMenuItem.ToolTipText = "Copy the GUID of the selected element to the clipboard.";
            this.copyGUIDMenuItem.Click += new System.EventHandler(this.CopyGUIDMenuItemClick);
            // 
            // navigatorToolStrip
            // 
            this.navigatorToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigatorToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.navigatorToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInNavigatorButton,
            this.toolStripSeparator3,
            this.projectBrowserButton,
            this.propertiesButton,
            this.addToDiagramButton,
            this.toolStripSeparator2,
            this.fqnButton,
            this.guidButton,
            this.copyGUIDButton,
            this.toolStripSeparator1,
            this.settingsButton,
            this.aboutButton});
            this.navigatorToolStrip.Location = new System.Drawing.Point(0, 0);
            this.navigatorToolStrip.Name = "navigatorToolStrip";
            this.navigatorToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.navigatorToolStrip.Size = new System.Drawing.Size(234, 27);
            this.navigatorToolStrip.TabIndex = 1;
            // 
            // openInNavigatorButton
            // 
            this.openInNavigatorButton.AutoToolTip = false;
            this.openInNavigatorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openInNavigatorButton.Image = ((System.Drawing.Image)(resources.GetObject("openInNavigatorButton.Image")));
            this.openInNavigatorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openInNavigatorButton.Name = "openInNavigatorButton";
            this.openInNavigatorButton.Size = new System.Drawing.Size(23, 24);
            this.openInNavigatorButton.Text = "Open";
            this.openInNavigatorButton.ToolTipText = "Show selected item in EA Navigator";
            this.openInNavigatorButton.Click += new System.EventHandler(this.OpenInNavigatorButtonClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // projectBrowserButton
            // 
            this.projectBrowserButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.projectBrowserButton.Image = ((System.Drawing.Image)(resources.GetObject("projectBrowserButton.Image")));
            this.projectBrowserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.projectBrowserButton.Name = "projectBrowserButton";
            this.projectBrowserButton.Size = new System.Drawing.Size(23, 24);
            this.projectBrowserButton.ToolTipText = "Select in project Browser";
            this.projectBrowserButton.Click += new System.EventHandler(this.ProjectBrowserButtonClick);
            // 
            // propertiesButton
            // 
            this.propertiesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.propertiesButton.Image = ((System.Drawing.Image)(resources.GetObject("propertiesButton.Image")));
            this.propertiesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(23, 24);
            this.propertiesButton.ToolTipText = "Open properties";
            this.propertiesButton.Click += new System.EventHandler(this.PropertiesButtonClick);
            // 
            // addToDiagramButton
            // 
            this.addToDiagramButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addToDiagramButton.Image = ((System.Drawing.Image)(resources.GetObject("addToDiagramButton.Image")));
            this.addToDiagramButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addToDiagramButton.Name = "addToDiagramButton";
            this.addToDiagramButton.Size = new System.Drawing.Size(23, 24);
            this.addToDiagramButton.Text = "To Diagram";
            this.addToDiagramButton.ToolTipText = "Add to diagram";
            this.addToDiagramButton.Click += new System.EventHandler(this.AddToDiagramButtonClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // fqnButton
            // 
            this.fqnButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fqnButton.Image = ((System.Drawing.Image)(resources.GetObject("fqnButton.Image")));
            this.fqnButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fqnButton.Name = "fqnButton";
            this.fqnButton.Size = new System.Drawing.Size(23, 24);
            this.fqnButton.ToolTipText = "Navigate to Fully Qualified Name";
            this.fqnButton.Click += new System.EventHandler(this.FqnButtonClick);
            // 
            // guidButton
            // 
            this.guidButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.guidButton.Image = ((System.Drawing.Image)(resources.GetObject("guidButton.Image")));
            this.guidButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.guidButton.Name = "guidButton";
            this.guidButton.Size = new System.Drawing.Size(23, 24);
            this.guidButton.Text = "GUID";
            this.guidButton.ToolTipText = "Select element from GUID";
            this.guidButton.Click += new System.EventHandler(this.GuidButtonClick);
            // 
            // copyGUIDButton
            // 
            this.copyGUIDButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyGUIDButton.Image = ((System.Drawing.Image)(resources.GetObject("copyGUIDButton.Image")));
            this.copyGUIDButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyGUIDButton.Name = "copyGUIDButton";
            this.copyGUIDButton.Size = new System.Drawing.Size(23, 24);
            this.copyGUIDButton.Text = "Copy GUID";
            this.copyGUIDButton.Click += new System.EventHandler(this.CopyGUIDButtonClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // settingsButton
            // 
            this.settingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsButton.Image")));
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(23, 24);
            this.settingsButton.ToolTipText = "Settings";
            this.settingsButton.Click += new System.EventHandler(this.SettingsButtonClick);
            // 
            // aboutButton
            // 
            this.aboutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aboutButton.Image = ((System.Drawing.Image)(resources.GetObject("aboutButton.Image")));
            this.aboutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(23, 24);
            this.aboutButton.ToolTipText = "About EA Navigator";
            this.aboutButton.Click += new System.EventHandler(this.AboutButtonClick);
            // 
            // navigatorToolStripContainer
            // 
            this.navigatorToolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // navigatorToolStripContainer.ContentPanel
            // 
            this.navigatorToolStripContainer.ContentPanel.Controls.Add(this.navigatorToolStrip);
            this.navigatorToolStripContainer.ContentPanel.Size = new System.Drawing.Size(234, 27);
            this.navigatorToolStripContainer.LeftToolStripPanelVisible = false;
            this.navigatorToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.navigatorToolStripContainer.Name = "navigatorToolStripContainer";
            this.navigatorToolStripContainer.RightToolStripPanelVisible = false;
            this.navigatorToolStripContainer.Size = new System.Drawing.Size(234, 27);
            this.navigatorToolStripContainer.TabIndex = 1;
            this.navigatorToolStripContainer.Text = "toolStripContainer1";
            this.navigatorToolStripContainer.Visible = false;
            // 
            // quickSearchBox
            // 
            this.quickSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.quickSearchBox.AutoSize = true;
            this.quickSearchBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.quickSearchBox.DroppedDown = false;
            this.quickSearchBox.FormattingEnabled = true;
            this.quickSearchBox.Location = new System.Drawing.Point(235, 4);
            this.quickSearchBox.MaxDropDownItems = 10;
            this.quickSearchBox.Name = "quickSearchBox";
            this.quickSearchBox.SelectedIndex = -1;
            this.quickSearchBox.SelectedItem = null;
            this.quickSearchBox.Size = new System.Drawing.Size(169, 23);
            this.quickSearchBox.TabIndex = 2;
            this.quickSearchBox.Visible = false;
            this.quickSearchBox.TextUpdate += new System.EventHandler(this.QuickSearchComboBoxTextUpdate);
            this.quickSearchBox.SelectionChangeCommitted += new System.EventHandler(this.QuickSearchComboBoxSelectionChangeCommitted);
            this.quickSearchBox.TextChanged += new System.EventHandler(this.QuickSearchComboBoxTextChanged);
            this.quickSearchBox.Enter += new System.EventHandler(this.QuickSearchComboBoxEnter);
            this.quickSearchBox.Leave += new System.EventHandler(this.QuickSearchComboBoxLeave);
            // 
            // NavigatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.quickSearchBox);
            this.Controls.Add(this.navigatorToolStripContainer);
            this.Controls.Add(this.NavigatorTree);
            this.Name = "NavigatorControl";
            this.Size = new System.Drawing.Size(407, 227);
            this.navigatorContextMenu.ResumeLayout(false);
            this.navigatorToolStrip.ResumeLayout(false);
            this.navigatorToolStrip.PerformLayout();
            this.navigatorToolStripContainer.ContentPanel.ResumeLayout(false);
            this.navigatorToolStripContainer.ContentPanel.PerformLayout();
            this.navigatorToolStripContainer.ResumeLayout(false);
            this.navigatorToolStripContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ToolStripButton copyGUIDButton;
		private System.Windows.Forms.ToolStripMenuItem copyGUIDMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton openInNavigatorButton;
		private TSF.UmlToolingFramework.EANavigator.QuickSearchBox quickSearchBox;
		private System.Windows.Forms.ToolStripMenuItem addToDiagramMenuOption;
		private System.Windows.Forms.ToolStripButton addToDiagramButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton guidButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton fqnButton;
		private System.Windows.Forms.ToolStripContainer navigatorToolStripContainer;
		private System.Windows.Forms.ToolStripButton aboutButton;
		private System.Windows.Forms.ToolStripButton settingsButton;
		private System.Windows.Forms.ToolStripButton propertiesButton;
		private System.Windows.Forms.ToolStripButton projectBrowserButton;
		private System.Windows.Forms.ToolStrip navigatorToolStrip;
		private System.Windows.Forms.ToolStripMenuItem optionsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectBrowserMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openPropertiesMenuItem;
		private System.Windows.Forms.ContextMenuStrip navigatorContextMenu;
		private System.Windows.Forms.TreeView NavigatorTree;
		

		

		

	}
}
