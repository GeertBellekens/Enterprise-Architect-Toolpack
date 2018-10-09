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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.mappingPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.targetTreeView = new BrightIdeasSoftware.TreeListView();
            this.targetColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.targetMappingsColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.targetExpandedColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.MappingContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectInProjectBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectNewMappingRootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mappingHotItemStyle = new BrightIdeasSoftware.HotItemStyle();
            this.mappingNodeImageList = new System.Windows.Forms.ImageList(this.components);
            this.sourceTreeView = new BrightIdeasSoftware.TreeListView();
            this.sourceColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.isMapped = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.sourceExpandColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetTreeView)).BeginInit();
            this.MappingContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourceTreeView)).BeginInit();
            this.SuspendLayout();
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(916, 558);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 11;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.ExportButtonClick);
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Controls.Add(this.mappingPanel);
            this.mainPanel.Controls.Add(this.targetTreeView);
            this.mainPanel.Controls.Add(this.sourceTreeView);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(991, 552);
            this.mainPanel.TabIndex = 12;
            // 
            // mappingPanel
            // 
            this.mappingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mappingPanel.AutoScroll = true;
            this.mappingPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.mappingPanel.Location = new System.Drawing.Point(365, 4);
            this.mappingPanel.MinimumSize = new System.Drawing.Size(224, 0);
            this.mappingPanel.Name = "mappingPanel";
            this.mappingPanel.Size = new System.Drawing.Size(234, 545);
            this.mappingPanel.TabIndex = 2;
            this.mappingPanel.WrapContents = false;
            // 
            // targetTreeView
            // 
            this.targetTreeView.AllColumns.Add(this.targetColumn);
            this.targetTreeView.AllColumns.Add(this.targetMappingsColumn);
            this.targetTreeView.AllColumns.Add(this.targetExpandedColumn);
            this.targetTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetTreeView.CellEditUseWholeCell = false;
            this.targetTreeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.targetColumn,
            this.targetMappingsColumn,
            this.targetExpandedColumn});
            this.targetTreeView.ContextMenuStrip = this.MappingContextMenu;
            this.targetTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.targetTreeView.FullRowSelect = true;
            this.targetTreeView.GridLines = true;
            this.targetTreeView.HideSelection = false;
            this.targetTreeView.HotItemStyle = this.mappingHotItemStyle;
            this.targetTreeView.IsSimpleDropSink = true;
            this.targetTreeView.Location = new System.Drawing.Point(603, 3);
            this.targetTreeView.Name = "targetTreeView";
            this.targetTreeView.ShowGroups = false;
            this.targetTreeView.Size = new System.Drawing.Size(385, 546);
            this.targetTreeView.SmallImageList = this.mappingNodeImageList;
            this.targetTreeView.TabIndex = 1;
            this.targetTreeView.UseCellFormatEvents = true;
            this.targetTreeView.UseCompatibleStateImageBehavior = false;
            this.targetTreeView.UseHotItem = true;
            this.targetTreeView.View = System.Windows.Forms.View.Details;
            this.targetTreeView.VirtualMode = true;
            this.targetTreeView.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.targetTreeView_CellRightClick);
            this.targetTreeView.SubItemChecking += new System.EventHandler<BrightIdeasSoftware.SubItemCheckingEventArgs>(this.targetTreeView_SubItemChecking);
            this.targetTreeView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.targetTreeView_FormatRow);
            this.targetTreeView.ModelCanDrop += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.targetTreeView_ModelCanDrop);
            this.targetTreeView.ModelDropped += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.targetTreeView_ModelDropped);
            this.targetTreeView.ItemActivate += new System.EventHandler(this.targetTreeView_ItemActivate);
            this.targetTreeView.SelectedIndexChanged += new System.EventHandler(this.targetTreeView_SelectedIndexChanged);
            // 
            // targetColumn
            // 
            this.targetColumn.AspectName = "name";
            this.targetColumn.FillsFreeSpace = true;
            this.targetColumn.Text = "Target";
            this.targetColumn.Width = 290;
            // 
            // targetMappingsColumn
            // 
            this.targetMappingsColumn.AspectName = "mappings.Count";
            this.targetMappingsColumn.Text = "Mappings";
            // 
            // targetExpandedColumn
            // 
            this.targetExpandedColumn.AspectName = "showAll";
            this.targetExpandedColumn.CheckBoxes = true;
            this.targetExpandedColumn.Text = "Expanded";
            // 
            // MappingContextMenu
            // 
            this.MappingContextMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MappingContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectInProjectBrowserToolStripMenuItem,
            this.openPropertiesToolStripMenuItem,
            this.selectNewMappingRootToolStripMenuItem});
            this.MappingContextMenu.Name = "mappingContextMenu";
            this.MappingContextMenu.Size = new System.Drawing.Size(207, 70);
            // 
            // selectInProjectBrowserToolStripMenuItem
            // 
            this.selectInProjectBrowserToolStripMenuItem.Name = "selectInProjectBrowserToolStripMenuItem";
            this.selectInProjectBrowserToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.selectInProjectBrowserToolStripMenuItem.Text = "Select in Project Browser";
            this.selectInProjectBrowserToolStripMenuItem.Click += new System.EventHandler(this.selectInProjectBrowserToolStripMenuItem_Click);
            // 
            // openPropertiesToolStripMenuItem
            // 
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
            // sourceTreeView
            // 
            this.sourceTreeView.AllColumns.Add(this.sourceColumn);
            this.sourceTreeView.AllColumns.Add(this.isMapped);
            this.sourceTreeView.AllColumns.Add(this.sourceExpandColumn);
            this.sourceTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceTreeView.CellEditUseWholeCell = false;
            this.sourceTreeView.CheckedAspectName = "";
            this.sourceTreeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sourceColumn,
            this.isMapped,
            this.sourceExpandColumn});
            this.sourceTreeView.ContextMenuStrip = this.MappingContextMenu;
            this.sourceTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.sourceTreeView.FullRowSelect = true;
            this.sourceTreeView.GridLines = true;
            this.sourceTreeView.HideSelection = false;
            this.sourceTreeView.HotItemStyle = this.mappingHotItemStyle;
            this.sourceTreeView.IsSimpleDragSource = true;
            this.sourceTreeView.Location = new System.Drawing.Point(3, 3);
            this.sourceTreeView.Name = "sourceTreeView";
            this.sourceTreeView.ShowGroups = false;
            this.sourceTreeView.ShowImagesOnSubItems = true;
            this.sourceTreeView.Size = new System.Drawing.Size(356, 546);
            this.sourceTreeView.SmallImageList = this.mappingNodeImageList;
            this.sourceTreeView.TabIndex = 0;
            this.sourceTreeView.UseCellFormatEvents = true;
            this.sourceTreeView.UseCompatibleStateImageBehavior = false;
            this.sourceTreeView.UseHotItem = true;
            this.sourceTreeView.View = System.Windows.Forms.View.Details;
            this.sourceTreeView.VirtualMode = true;
            this.sourceTreeView.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.sourceTreeView_FormatRow);
            this.sourceTreeView.ItemActivate += new System.EventHandler(this.sourceTreeView_ItemActivate);
            this.sourceTreeView.SelectedIndexChanged += new System.EventHandler(this.sourceTreeView_SelectedIndexChanged);
            // 
            // sourceColumn
            // 
            this.sourceColumn.AspectName = "name";
            this.sourceColumn.FillsFreeSpace = true;
            this.sourceColumn.Text = "Source";
            this.sourceColumn.Width = 260;
            // 
            // isMapped
            // 
            this.isMapped.AspectName = "mappings.Count";
            this.isMapped.Text = "Mappings";
            // 
            // sourceExpandColumn
            // 
            this.sourceExpandColumn.AspectName = "showAll";
            this.sourceExpandColumn.CheckBoxes = true;
            this.sourceExpandColumn.Text = "Expanded";
            // 
            // MappingControlGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.exportButton);
            this.Name = "MappingControlGUI";
            this.Size = new System.Drawing.Size(994, 584);
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.targetTreeView)).EndInit();
            this.MappingContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sourceTreeView)).EndInit();
            this.ResumeLayout(false);

		}

        private System.Windows.Forms.Panel mainPanel;
        private BrightIdeasSoftware.TreeListView targetTreeView;
        private BrightIdeasSoftware.TreeListView sourceTreeView;
        public BrightIdeasSoftware.OLVColumn sourceColumn;
        private BrightIdeasSoftware.OLVColumn targetColumn;
        private BrightIdeasSoftware.OLVColumn isMapped;
        private System.Windows.Forms.ImageList mappingNodeImageList;
        private BrightIdeasSoftware.OLVColumn sourceExpandColumn;
        private BrightIdeasSoftware.OLVColumn targetMappingsColumn;
        private BrightIdeasSoftware.OLVColumn targetExpandedColumn;
        private System.Windows.Forms.ContextMenuStrip MappingContextMenu;
        private System.Windows.Forms.ToolStripMenuItem selectInProjectBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectNewMappingRootToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel mappingPanel;
        private BrightIdeasSoftware.HotItemStyle mappingHotItemStyle;
    }
}
