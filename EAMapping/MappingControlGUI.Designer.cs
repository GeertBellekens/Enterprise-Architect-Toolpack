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
		private System.Windows.Forms.Button goToTargetButton;
		private System.Windows.Forms.Button goToSourceButton;
		private System.Windows.Forms.Button createMappingButton;
		private System.Windows.Forms.Button deleteMappingButton;
		private System.Windows.Forms.Button editMappingLogicButton;
		private System.Windows.Forms.Button deleteMappingLogicButton;
		
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
            this.goToTargetButton = new System.Windows.Forms.Button();
            this.goToSourceButton = new System.Windows.Forms.Button();
            this.createMappingButton = new System.Windows.Forms.Button();
            this.deleteMappingButton = new System.Windows.Forms.Button();
            this.editMappingLogicButton = new System.Windows.Forms.Button();
            this.deleteMappingLogicButton = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.targetTreeView = new BrightIdeasSoftware.TreeListView();
            this.targetColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.mappingNodeImageList = new System.Windows.Forms.ImageList(this.components);
            this.sourceTreeView = new BrightIdeasSoftware.TreeListView();
            this.sourceColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.isMapped = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.expandTargetButton = new System.Windows.Forms.Button();
            this.expandSourceButton = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetTreeView)).BeginInit();
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
            // goToTargetButton
            // 
            this.goToTargetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.goToTargetButton.Image = ((System.Drawing.Image)(resources.GetObject("goToTargetButton.Image")));
            this.goToTargetButton.Location = new System.Drawing.Point(33, 558);
            this.goToTargetButton.Name = "goToTargetButton";
            this.goToTargetButton.Size = new System.Drawing.Size(24, 23);
            this.goToTargetButton.TabIndex = 7;
            this.goToTargetButton.UseVisualStyleBackColor = true;
            // 
            // goToSourceButton
            // 
            this.goToSourceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.goToSourceButton.Image = ((System.Drawing.Image)(resources.GetObject("goToSourceButton.Image")));
            this.goToSourceButton.Location = new System.Drawing.Point(3, 558);
            this.goToSourceButton.Name = "goToSourceButton";
            this.goToSourceButton.Size = new System.Drawing.Size(24, 23);
            this.goToSourceButton.TabIndex = 6;
            this.goToSourceButton.UseVisualStyleBackColor = true;
            // 
            // createMappingButton
            // 
            this.createMappingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.createMappingButton.Image = ((System.Drawing.Image)(resources.GetObject("createMappingButton.Image")));
            this.createMappingButton.Location = new System.Drawing.Point(63, 558);
            this.createMappingButton.Name = "createMappingButton";
            this.createMappingButton.Size = new System.Drawing.Size(24, 23);
            this.createMappingButton.TabIndex = 8;
            this.createMappingButton.UseVisualStyleBackColor = true;
            this.createMappingButton.Click += new System.EventHandler(this.CreateMappingButtonClick);
            // 
            // deleteMappingButton
            // 
            this.deleteMappingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteMappingButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteMappingButton.Image")));
            this.deleteMappingButton.Location = new System.Drawing.Point(93, 558);
            this.deleteMappingButton.Name = "deleteMappingButton";
            this.deleteMappingButton.Size = new System.Drawing.Size(24, 23);
            this.deleteMappingButton.TabIndex = 9;
            this.deleteMappingButton.UseVisualStyleBackColor = true;
            this.deleteMappingButton.Click += new System.EventHandler(this.DeleteMappingButtonClick);
            // 
            // editMappingLogicButton
            // 
            this.editMappingLogicButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editMappingLogicButton.Image = ((System.Drawing.Image)(resources.GetObject("editMappingLogicButton.Image")));
            this.editMappingLogicButton.Location = new System.Drawing.Point(123, 558);
            this.editMappingLogicButton.Name = "editMappingLogicButton";
            this.editMappingLogicButton.Size = new System.Drawing.Size(24, 23);
            this.editMappingLogicButton.TabIndex = 8;
            this.editMappingLogicButton.UseVisualStyleBackColor = true;
            this.editMappingLogicButton.Click += new System.EventHandler(this.EditMappingLogicButtonClick);
            // 
            // deleteMappingLogicButton
            // 
            this.deleteMappingLogicButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteMappingLogicButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteMappingLogicButton.Image")));
            this.deleteMappingLogicButton.Location = new System.Drawing.Point(153, 558);
            this.deleteMappingLogicButton.Name = "deleteMappingLogicButton";
            this.deleteMappingLogicButton.Size = new System.Drawing.Size(24, 23);
            this.deleteMappingLogicButton.TabIndex = 9;
            this.deleteMappingLogicButton.UseVisualStyleBackColor = true;
            this.deleteMappingLogicButton.Click += new System.EventHandler(this.DeleteMappingLogicButtonClick);
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Controls.Add(this.targetTreeView);
            this.mainPanel.Controls.Add(this.sourceTreeView);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(991, 552);
            this.mainPanel.TabIndex = 12;
            // 
            // targetTreeView
            // 
            this.targetTreeView.AllColumns.Add(this.targetColumn);
            this.targetTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetTreeView.CellEditUseWholeCell = false;
            this.targetTreeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.targetColumn});
            this.targetTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.targetTreeView.FullRowSelect = true;
            this.targetTreeView.GridLines = true;
            this.targetTreeView.HideSelection = false;
            this.targetTreeView.Location = new System.Drawing.Point(579, 3);
            this.targetTreeView.Name = "targetTreeView";
            this.targetTreeView.ShowGroups = false;
            this.targetTreeView.Size = new System.Drawing.Size(409, 546);
            this.targetTreeView.SmallImageList = this.mappingNodeImageList;
            this.targetTreeView.TabIndex = 1;
            this.targetTreeView.UseCompatibleStateImageBehavior = false;
            this.targetTreeView.View = System.Windows.Forms.View.Details;
            this.targetTreeView.VirtualMode = true;
            // 
            // targetColumn
            // 
            this.targetColumn.AspectName = "name";
            this.targetColumn.Text = "Target";
            this.targetColumn.Width = 290;
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
            this.sourceTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sourceTreeView.CellEditUseWholeCell = false;
            this.sourceTreeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sourceColumn,
            this.isMapped});
            this.sourceTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.sourceTreeView.FullRowSelect = true;
            this.sourceTreeView.GridLines = true;
            this.sourceTreeView.HideSelection = false;
            this.sourceTreeView.Location = new System.Drawing.Point(3, 3);
            this.sourceTreeView.Name = "sourceTreeView";
            this.sourceTreeView.ShowGroups = false;
            this.sourceTreeView.Size = new System.Drawing.Size(409, 546);
            this.sourceTreeView.SmallImageList = this.mappingNodeImageList;
            this.sourceTreeView.TabIndex = 0;
            this.sourceTreeView.UseCompatibleStateImageBehavior = false;
            this.sourceTreeView.View = System.Windows.Forms.View.Details;
            this.sourceTreeView.VirtualMode = true;
            this.sourceTreeView.SelectedIndexChanged += new System.EventHandler(this.sourceTreeView_SelectedIndexChanged);
            // 
            // sourceColumn
            // 
            this.sourceColumn.AspectName = "name";
            this.sourceColumn.Text = "Source";
            this.sourceColumn.Width = 260;
            // 
            // isMapped
            // 
            this.isMapped.AspectName = "mappings.Count";
            this.isMapped.Text = "MappingCount";
            // 
            // expandTargetButton
            // 
            this.expandTargetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.expandTargetButton.Image = ((System.Drawing.Image)(resources.GetObject("expandTargetButton.Image")));
            this.expandTargetButton.Location = new System.Drawing.Point(579, 555);
            this.expandTargetButton.Name = "expandTargetButton";
            this.expandTargetButton.Size = new System.Drawing.Size(24, 23);
            this.expandTargetButton.TabIndex = 13;
            this.expandTargetButton.UseVisualStyleBackColor = true;
            this.expandTargetButton.Click += new System.EventHandler(this.expandTargetButton_Click);
            // 
            // expandSourceButton
            // 
            this.expandSourceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.expandSourceButton.Image = ((System.Drawing.Image)(resources.GetObject("expandSourceButton.Image")));
            this.expandSourceButton.Location = new System.Drawing.Point(183, 558);
            this.expandSourceButton.Name = "expandSourceButton";
            this.expandSourceButton.Size = new System.Drawing.Size(24, 23);
            this.expandSourceButton.TabIndex = 14;
            this.expandSourceButton.UseVisualStyleBackColor = true;
            this.expandSourceButton.Click += new System.EventHandler(this.expandSourceButton_Click);
            // 
            // MappingControlGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.expandSourceButton);
            this.Controls.Add(this.expandTargetButton);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.goToTargetButton);
            this.Controls.Add(this.goToSourceButton);
            this.Controls.Add(this.createMappingButton);
            this.Controls.Add(this.deleteMappingButton);
            this.Controls.Add(this.editMappingLogicButton);
            this.Controls.Add(this.deleteMappingLogicButton);
            this.Name = "MappingControlGUI";
            this.Size = new System.Drawing.Size(994, 584);
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.targetTreeView)).EndInit();
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
        private System.Windows.Forms.Button expandTargetButton;
        private System.Windows.Forms.Button expandSourceButton;
    }
}
