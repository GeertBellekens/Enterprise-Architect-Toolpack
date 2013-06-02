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
			this.navigatorToolStrip = new System.Windows.Forms.ToolStrip();
			this.projectBrowserButton = new System.Windows.Forms.ToolStripButton();
			this.propertiesButton = new System.Windows.Forms.ToolStripButton();
			this.addToDiagramButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.fqnButton = new System.Windows.Forms.ToolStripButton();
			this.guidButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.settingsButton = new System.Windows.Forms.ToolStripButton();
			this.aboutButton = new System.Windows.Forms.ToolStripButton();
			this.navigatorToolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.quickSearchComboBox = new TSF.UmlToolingFramework.EANavigator.QuickSearchComboBox();
			this.navigatorContextMenu.SuspendLayout();
			this.navigatorToolStrip.SuspendLayout();
			this.navigatorToolStripContainer.TopToolStripPanel.SuspendLayout();
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
			this.NavigatorTree.Size = new System.Drawing.Size(404, 224);
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
									this.optionsMenuItem});
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
			// navigatorToolStrip
			// 
			this.navigatorToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.navigatorToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.navigatorToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.projectBrowserButton,
									this.propertiesButton,
									this.addToDiagramButton,
									this.toolStripSeparator2,
									this.fqnButton,
									this.guidButton,
									this.toolStripSeparator1,
									this.settingsButton,
									this.aboutButton});
			this.navigatorToolStrip.Location = new System.Drawing.Point(3, 0);
			this.navigatorToolStrip.Name = "navigatorToolStrip";
			this.navigatorToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.navigatorToolStrip.Size = new System.Drawing.Size(176, 25);
			this.navigatorToolStrip.TabIndex = 1;
			// 
			// projectBrowserButton
			// 
			this.projectBrowserButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.projectBrowserButton.Image = ((System.Drawing.Image)(resources.GetObject("projectBrowserButton.Image")));
			this.projectBrowserButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.projectBrowserButton.Name = "projectBrowserButton";
			this.projectBrowserButton.Size = new System.Drawing.Size(23, 22);
			this.projectBrowserButton.ToolTipText = "Select in project Browser";
			this.projectBrowserButton.Click += new System.EventHandler(this.ProjectBrowserButtonClick);
			// 
			// propertiesButton
			// 
			this.propertiesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.propertiesButton.Image = ((System.Drawing.Image)(resources.GetObject("propertiesButton.Image")));
			this.propertiesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.propertiesButton.Name = "propertiesButton";
			this.propertiesButton.Size = new System.Drawing.Size(23, 22);
			this.propertiesButton.ToolTipText = "Open properties";
			this.propertiesButton.Click += new System.EventHandler(this.PropertiesButtonClick);
			// 
			// addToDiagramButton
			// 
			this.addToDiagramButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.addToDiagramButton.Image = ((System.Drawing.Image)(resources.GetObject("addToDiagramButton.Image")));
			this.addToDiagramButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addToDiagramButton.Name = "addToDiagramButton";
			this.addToDiagramButton.Size = new System.Drawing.Size(23, 22);
			this.addToDiagramButton.Text = "To Diagram";
			this.addToDiagramButton.ToolTipText = "Add to diagram";
			this.addToDiagramButton.Click += new System.EventHandler(this.AddToDiagramButtonClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// fqnButton
			// 
			this.fqnButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.fqnButton.Image = ((System.Drawing.Image)(resources.GetObject("fqnButton.Image")));
			this.fqnButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.fqnButton.Name = "fqnButton";
			this.fqnButton.Size = new System.Drawing.Size(23, 22);
			this.fqnButton.ToolTipText = "Navigate to Fully Qualified Name";
			this.fqnButton.Click += new System.EventHandler(this.FqnButtonClick);
			// 
			// guidButton
			// 
			this.guidButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.guidButton.Image = ((System.Drawing.Image)(resources.GetObject("guidButton.Image")));
			this.guidButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.guidButton.Name = "guidButton";
			this.guidButton.Size = new System.Drawing.Size(23, 22);
			this.guidButton.Text = "GUID";
			this.guidButton.ToolTipText = "Select element from GUID";
			this.guidButton.Click += new System.EventHandler(this.GuidButtonClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// settingsButton
			// 
			this.settingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.settingsButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsButton.Image")));
			this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.settingsButton.Name = "settingsButton";
			this.settingsButton.Size = new System.Drawing.Size(23, 22);
			this.settingsButton.ToolTipText = "Settings";
			this.settingsButton.Click += new System.EventHandler(this.SettingsButtonClick);
			// 
			// aboutButton
			// 
			this.aboutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.aboutButton.Image = ((System.Drawing.Image)(resources.GetObject("aboutButton.Image")));
			this.aboutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.aboutButton.Name = "aboutButton";
			this.aboutButton.Size = new System.Drawing.Size(23, 22);
			this.aboutButton.ToolTipText = "About EA Navigator";
			this.aboutButton.Click += new System.EventHandler(this.AboutButtonClick);
			// 
			// navigatorToolStripContainer
			// 
			this.navigatorToolStripContainer.BottomToolStripPanelVisible = false;
			// 
			// navigatorToolStripContainer.ContentPanel
			// 
			this.navigatorToolStripContainer.ContentPanel.Size = new System.Drawing.Size(182, 2);
			this.navigatorToolStripContainer.LeftToolStripPanelVisible = false;
			this.navigatorToolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.navigatorToolStripContainer.Name = "navigatorToolStripContainer";
			this.navigatorToolStripContainer.RightToolStripPanelVisible = false;
			this.navigatorToolStripContainer.Size = new System.Drawing.Size(182, 27);
			this.navigatorToolStripContainer.TabIndex = 1;
			this.navigatorToolStripContainer.Text = "toolStripContainer1";
			// 
			// navigatorToolStripContainer.TopToolStripPanel
			// 
			this.navigatorToolStripContainer.TopToolStripPanel.Controls.Add(this.navigatorToolStrip);
			this.navigatorToolStripContainer.Visible = false;
			// 
			// quickSearchComboBox
			// 
			this.quickSearchComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.quickSearchComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.quickSearchComboBox.FormattingEnabled = true;
			this.quickSearchComboBox.Location = new System.Drawing.Point(189, 4);
			this.quickSearchComboBox.MaxDropDownItems = 10;
			this.quickSearchComboBox.Name = "quickSearchComboBox";
			this.quickSearchComboBox.Size = new System.Drawing.Size(204, 21);
			this.quickSearchComboBox.TabIndex = 2;
			this.quickSearchComboBox.SelectionChangeCommitted += new System.EventHandler(this.QuickSearchComboBoxSelectionChangeCommitted);
			this.quickSearchComboBox.TextUpdate += new System.EventHandler(this.QuickSearchComboBoxTextUpdate);
			this.quickSearchComboBox.TextChanged += new System.EventHandler(this.QuickSearchComboBoxTextChanged);
			// 
			// NavigatorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.quickSearchComboBox);
			this.Controls.Add(this.navigatorToolStripContainer);
			this.Controls.Add(this.NavigatorTree);
			this.Name = "NavigatorControl";
			this.Size = new System.Drawing.Size(407, 227);
			this.navigatorContextMenu.ResumeLayout(false);
			this.navigatorToolStrip.ResumeLayout(false);
			this.navigatorToolStrip.PerformLayout();
			this.navigatorToolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.navigatorToolStripContainer.TopToolStripPanel.PerformLayout();
			this.navigatorToolStripContainer.ResumeLayout(false);
			this.navigatorToolStripContainer.PerformLayout();
			this.ResumeLayout(false);
		}
		private TSF.UmlToolingFramework.EANavigator.QuickSearchComboBox quickSearchComboBox;
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
