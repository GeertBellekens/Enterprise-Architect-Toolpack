using System.Collections.Generic;
using System.Linq;
namespace EAMapping
{
	partial class MappingControlGUI
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button exportButton;
		
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MappingControlGUI));
            this.exportButton = new System.Windows.Forms.Button();
            this.MappingContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectInProjectBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectNewMappingRootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newEmptyMappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mappingHotItemStyle = new BrightIdeasSoftware.HotItemStyle();
            this.mappingNodeImageList = new System.Windows.Forms.ImageList(this.components);
            this.leftSplitContainer = new System.Windows.Forms.SplitContainer();
            this.sourceTreeView = new BrightIdeasSoftware.TreeListView();
            this.sourceColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.isMapped = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.sourceExpandColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.mappingPanel = new System.Windows.Forms.TableLayoutPanel();
            this.targetTreeView = new BrightIdeasSoftware.TreeListView();
            this.targetColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.targetMappingsColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.targetExpandedColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.MappingContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).BeginInit();
            this.leftSplitContainer.Panel1.SuspendLayout();
            this.leftSplitContainer.Panel2.SuspendLayout();
            this.leftSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourceTreeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
            this.rightSplitContainer.Panel1.SuspendLayout();
            this.rightSplitContainer.Panel2.SuspendLayout();
            this.rightSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetTreeView)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(1034, 542);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 11;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.ExportButtonClick);
            // 
            // MappingContextMenu
            // 
            this.MappingContextMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MappingContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectInProjectBrowserToolStripMenuItem,
            this.openPropertiesToolStripMenuItem,
            this.selectNewMappingRootToolStripMenuItem,
            this.newEmptyMappingToolStripMenuItem});
            this.MappingContextMenu.Name = "mappingContextMenu";
            this.MappingContextMenu.Size = new System.Drawing.Size(207, 92);
            this.MappingContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.MappingContextMenu_Opening);
            // 
            // selectInProjectBrowserToolStripMenuItem
            // 
            this.selectInProjectBrowserToolStripMenuItem.Image = global::EAMapping.Properties.Resources.SelectInProjectBrowser;
            this.selectInProjectBrowserToolStripMenuItem.Name = "selectInProjectBrowserToolStripMenuItem";
            this.selectInProjectBrowserToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.selectInProjectBrowserToolStripMenuItem.Text = "Select in Project Browser";
            this.selectInProjectBrowserToolStripMenuItem.Click += new System.EventHandler(this.selectInProjectBrowserToolStripMenuItem_Click);
            // 
            // openPropertiesToolStripMenuItem
            // 
            this.openPropertiesToolStripMenuItem.Image = global::EAMapping.Properties.Resources.OpenProperties;
            this.openPropertiesToolStripMenuItem.Name = "openPropertiesToolStripMenuItem";
            this.openPropertiesToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openPropertiesToolStripMenuItem.Text = "Open Properties";
            this.openPropertiesToolStripMenuItem.Click += new System.EventHandler(this.openPropertiesToolStripMenuItem_Click);
            // 
            // selectNewMappingRootToolStripMenuItem
            // 
            this.selectNewMappingRootToolStripMenuItem.Name = "selectNewMappingRootToolStripMenuItem";
            this.selectNewMappingRootToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.selectNewMappingRootToolStripMenuItem.Text = "Select new mapping root";
            this.selectNewMappingRootToolStripMenuItem.Click += new System.EventHandler(this.selectNewMappingRootToolStripMenuItem_Click);
            // 
            // newEmptyMappingToolStripMenuItem
            // 
            this.newEmptyMappingToolStripMenuItem.Name = "newEmptyMappingToolStripMenuItem";
            this.newEmptyMappingToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.newEmptyMappingToolStripMenuItem.Text = "New Empty Mapping";
            this.newEmptyMappingToolStripMenuItem.Click += new System.EventHandler(this.newEmptyMappingToolStripMenuItem_Click);
            // 
            // mappingHotItemStyle
            // 
            this.mappingHotItemStyle.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.mappingHotItemStyle.ForeColor = System.Drawing.Color.White;
            // 
            // mappingNodeImageList
            // 
            this.mappingNodeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mappingNodeImageList.ImageStream")));
            this.mappingNodeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.mappingNodeImageList.Images.SetKeyName(0, "attributeNode");
            this.mappingNodeImageList.Images.SetKeyName(1, "classifierNode");
            this.mappingNodeImageList.Images.SetKeyName(2, "associationNode");
            this.mappingNodeImageList.Images.SetKeyName(3, "packageNode");
            // 
            // leftSplitContainer
            // 
            this.leftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.leftSplitContainer.Name = "leftSplitContainer";
            // 
            // leftSplitContainer.Panel1
            // 
            this.leftSplitContainer.Panel1.Controls.Add(this.sourceTreeView);
            // 
            // leftSplitContainer.Panel2
            // 
            this.leftSplitContainer.Panel2.Controls.Add(this.rightSplitContainer);
            this.leftSplitContainer.Size = new System.Drawing.Size(1106, 533);
            this.leftSplitContainer.SplitterDistance = 430;
            this.leftSplitContainer.TabIndex = 13;
            // 
            // sourceTreeView
            // 
            this.sourceTreeView.AllColumns.Add(this.sourceColumn);
            this.sourceTreeView.AllColumns.Add(this.isMapped);
            this.sourceTreeView.AllColumns.Add(this.sourceExpandColumn);
            this.sourceTreeView.CellEditUseWholeCell = false;
            this.sourceTreeView.CheckedAspectName = "";
            this.sourceTreeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sourceColumn,
            this.isMapped,
            this.sourceExpandColumn});
            this.sourceTreeView.ContextMenuStrip = this.MappingContextMenu;
            this.sourceTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.sourceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceTreeView.FullRowSelect = true;
            this.sourceTreeView.GridLines = true;
            this.sourceTreeView.HideSelection = false;
            this.sourceTreeView.HotItemStyle = this.mappingHotItemStyle;
            this.sourceTreeView.IsSimpleDragSource = true;
            this.sourceTreeView.IsSimpleDropSink = true;
            this.sourceTreeView.Location = new System.Drawing.Point(0, 0);
            this.sourceTreeView.Name = "sourceTreeView";
            this.sourceTreeView.ShowGroups = false;
            this.sourceTreeView.ShowImagesOnSubItems = true;
            this.sourceTreeView.Size = new System.Drawing.Size(430, 533);
            this.sourceTreeView.SmallImageList = this.mappingNodeImageList;
            this.sourceTreeView.TabIndex = 0;
            this.sourceTreeView.UseCellFormatEvents = true;
            this.sourceTreeView.UseCompatibleStateImageBehavior = false;
            this.sourceTreeView.UseFiltering = true;
            this.sourceTreeView.UseHotItem = true;
            this.sourceTreeView.View = System.Windows.Forms.View.Details;
            this.sourceTreeView.VirtualMode = true;
            this.sourceTreeView.SubItemChecking += new System.EventHandler<BrightIdeasSoftware.SubItemCheckingEventArgs>(this.sourceTreeView_SubItemChecking);
            this.sourceTreeView.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.sourceTreeView_FormatCell);
            this.sourceTreeView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.sourceTreeView_FormatRow);
            this.sourceTreeView.HeaderCheckBoxChanging += new System.EventHandler<BrightIdeasSoftware.HeaderCheckBoxChangingEventArgs>(this.sourceTreeView_HeaderCheckBoxChanging);
            this.sourceTreeView.ModelCanDrop += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.sourceTreeView_ModelCanDrop);
            this.sourceTreeView.ModelDropped += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.sourceTreeView_ModelDropped);
            this.sourceTreeView.ItemActivate += new System.EventHandler(this.sourceTreeView_ItemActivate);
            this.sourceTreeView.SelectedIndexChanged += new System.EventHandler(this.sourceTreeView_SelectedIndexChanged);
            // 
            // sourceColumn
            // 
            this.sourceColumn.AspectName = "displayName";
            this.sourceColumn.Hideable = false;
            this.sourceColumn.Text = "Source Model";
            this.sourceColumn.Width = 300;
            // 
            // isMapped
            // 
            this.isMapped.AspectName = "displayMappingCount";
            this.isMapped.Text = "Mappings";
            this.isMapped.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sourceExpandColumn
            // 
            this.sourceExpandColumn.AspectName = "showAll";
            this.sourceExpandColumn.CheckBoxes = true;
            this.sourceExpandColumn.HeaderCheckBox = true;
            this.sourceExpandColumn.HeaderCheckBoxUpdatesRowCheckBoxes = false;
            this.sourceExpandColumn.Text = "Expand";
            this.sourceExpandColumn.Width = 40;
            // 
            // rightSplitContainer
            // 
            this.rightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.rightSplitContainer.Name = "rightSplitContainer";
            // 
            // rightSplitContainer.Panel1
            // 
            this.rightSplitContainer.Panel1.Controls.Add(this.mappingPanel);
            // 
            // rightSplitContainer.Panel2
            // 
            this.rightSplitContainer.Panel2.Controls.Add(this.targetTreeView);
            this.rightSplitContainer.Size = new System.Drawing.Size(672, 533);
            this.rightSplitContainer.SplitterDistance = 265;
            this.rightSplitContainer.TabIndex = 0;
            // 
            // mappingPanel
            // 
            this.mappingPanel.AutoScroll = true;
            this.mappingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mappingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mappingPanel.Location = new System.Drawing.Point(0, 0);
            this.mappingPanel.Name = "mappingPanel";
            this.mappingPanel.RowCount = 3;
            this.mappingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mappingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mappingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mappingPanel.Size = new System.Drawing.Size(265, 533);
            this.mappingPanel.TabIndex = 2;
            // 
            // targetTreeView
            // 
            this.targetTreeView.AllColumns.Add(this.targetColumn);
            this.targetTreeView.AllColumns.Add(this.targetMappingsColumn);
            this.targetTreeView.AllColumns.Add(this.targetExpandedColumn);
            this.targetTreeView.CellEditUseWholeCell = false;
            this.targetTreeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.targetColumn,
            this.targetMappingsColumn,
            this.targetExpandedColumn});
            this.targetTreeView.ContextMenuStrip = this.MappingContextMenu;
            this.targetTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.targetTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.targetTreeView.FullRowSelect = true;
            this.targetTreeView.GridLines = true;
            this.targetTreeView.HideSelection = false;
            this.targetTreeView.HotItemStyle = this.mappingHotItemStyle;
            this.targetTreeView.IsSimpleDragSource = true;
            this.targetTreeView.IsSimpleDropSink = true;
            this.targetTreeView.Location = new System.Drawing.Point(0, 0);
            this.targetTreeView.Name = "targetTreeView";
            this.targetTreeView.ShowGroups = false;
            this.targetTreeView.Size = new System.Drawing.Size(403, 533);
            this.targetTreeView.SmallImageList = this.mappingNodeImageList;
            this.targetTreeView.TabIndex = 1;
            this.targetTreeView.UseCellFormatEvents = true;
            this.targetTreeView.UseCompatibleStateImageBehavior = false;
            this.targetTreeView.UseFiltering = true;
            this.targetTreeView.UseHotItem = true;
            this.targetTreeView.View = System.Windows.Forms.View.Details;
            this.targetTreeView.VirtualMode = true;
            this.targetTreeView.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.targetTreeView_CellRightClick);
            this.targetTreeView.SubItemChecking += new System.EventHandler<BrightIdeasSoftware.SubItemCheckingEventArgs>(this.targetTreeView_SubItemChecking);
            this.targetTreeView.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.targetTreeView_FormatCell);
            this.targetTreeView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.targetTreeView_FormatRow);
            this.targetTreeView.HeaderCheckBoxChanging += new System.EventHandler<BrightIdeasSoftware.HeaderCheckBoxChangingEventArgs>(this.targetTreeView_HeaderCheckBoxChanging);
            this.targetTreeView.ModelCanDrop += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.targetTreeView_ModelCanDrop);
            this.targetTreeView.ModelDropped += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.targetTreeView_ModelDropped);
            this.targetTreeView.ItemActivate += new System.EventHandler(this.targetTreeView_ItemActivate);
            this.targetTreeView.SelectedIndexChanged += new System.EventHandler(this.targetTreeView_SelectedIndexChanged);
            // 
            // targetColumn
            // 
            this.targetColumn.AspectName = "displayName";
            this.targetColumn.Text = "Target Model";
            this.targetColumn.Width = 300;
            // 
            // targetMappingsColumn
            // 
            this.targetMappingsColumn.AspectName = "displayMappingCount";
            this.targetMappingsColumn.Text = "Mappings";
            this.targetMappingsColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // targetExpandedColumn
            // 
            this.targetExpandedColumn.AspectName = "showAll";
            this.targetExpandedColumn.CheckBoxes = true;
            this.targetExpandedColumn.HeaderCheckBox = true;
            this.targetExpandedColumn.HeaderCheckBoxUpdatesRowCheckBoxes = false;
            this.targetExpandedColumn.Text = "Expand";
            this.targetExpandedColumn.Width = 40;
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Controls.Add(this.leftSplitContainer);
            this.mainPanel.Location = new System.Drawing.Point(3, 3);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1106, 533);
            this.mainPanel.TabIndex = 12;
            // 
            // MappingControlGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.exportButton);
            this.Name = "MappingControlGUI";
            this.Size = new System.Drawing.Size(1112, 568);
            this.MappingContextMenu.ResumeLayout(false);
            this.leftSplitContainer.Panel1.ResumeLayout(false);
            this.leftSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).EndInit();
            this.leftSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sourceTreeView)).EndInit();
            this.rightSplitContainer.Panel1.ResumeLayout(false);
            this.rightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.targetTreeView)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}
        private System.Windows.Forms.ImageList mappingNodeImageList;
        private System.Windows.Forms.ContextMenuStrip MappingContextMenu;
        private System.Windows.Forms.ToolStripMenuItem selectInProjectBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectNewMappingRootToolStripMenuItem;
        private BrightIdeasSoftware.HotItemStyle mappingHotItemStyle;
        private System.Windows.Forms.SplitContainer leftSplitContainer;
        private System.Windows.Forms.SplitContainer rightSplitContainer;
        private System.Windows.Forms.Panel mainPanel;
        private BrightIdeasSoftware.TreeListView sourceTreeView;
        public BrightIdeasSoftware.OLVColumn sourceColumn;
        private BrightIdeasSoftware.OLVColumn isMapped;
        private BrightIdeasSoftware.OLVColumn sourceExpandColumn;
        private System.Windows.Forms.TableLayoutPanel mappingPanel;
        private BrightIdeasSoftware.TreeListView targetTreeView;
        private BrightIdeasSoftware.OLVColumn targetColumn;
        private BrightIdeasSoftware.OLVColumn targetMappingsColumn;
        private BrightIdeasSoftware.OLVColumn targetExpandedColumn;
        private System.Windows.Forms.ToolStripMenuItem newEmptyMappingToolStripMenuItem;
    }
}
