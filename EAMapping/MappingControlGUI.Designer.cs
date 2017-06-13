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
		private EAMapping.LinkedTreeViews trees;
		private System.Windows.Forms.Button exportButton;
		private System.Windows.Forms.Button goToTargetButton;
		private System.Windows.Forms.Button goToSourceButton;
		private System.Windows.Forms.Button createMappingButton;
		private System.Windows.Forms.Button deleteMappingButton;
		private System.Windows.Forms.Button editMappingLogicButton;
		private System.Windows.Forms.Button deleteMappingLogicButton;
		private System.Windows.Forms.Button addNodeButton;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MappingControlGUI));
			this.trees = new EAMapping.LinkedTreeViews();
			this.exportButton = new System.Windows.Forms.Button();
			this.goToTargetButton = new System.Windows.Forms.Button();
			this.goToSourceButton = new System.Windows.Forms.Button();
			this.createMappingButton = new System.Windows.Forms.Button();
			this.deleteMappingButton = new System.Windows.Forms.Button();
			this.editMappingLogicButton = new System.Windows.Forms.Button();
			this.deleteMappingLogicButton = new System.Windows.Forms.Button();
			this.addNodeButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// trees
			// 
			this.trees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.trees.BackColor = System.Drawing.Color.LightGray;
			this.trees.Location = new System.Drawing.Point(0, 0);
			this.trees.Name = "trees";
			this.trees.Size = new System.Drawing.Size(994, 552);
			this.trees.TabIndex = 0;
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
			// createMappingButton
			// 
			this.createMappingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.createMappingButton.Image = ((System.Drawing.Image)(resources.GetObject("createMapping.Image")));
			this.createMappingButton.Location = new System.Drawing.Point(63, 558);
			this.createMappingButton.Name = "createMapping";
			this.createMappingButton.Size = new System.Drawing.Size(24, 23);
			this.createMappingButton.TabIndex = 8;
			this.createMappingButton.UseVisualStyleBackColor = true;
			this.createMappingButton.Click += new System.EventHandler(this.CreateMappingButtonClick);
			// 
			// deleteMappingButton
			// 
			this.deleteMappingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.deleteMappingButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteMapping.Image")));
			this.deleteMappingButton.Location = new System.Drawing.Point(93, 558);
			this.deleteMappingButton.Name = "deleteMapping";
			this.deleteMappingButton.Size = new System.Drawing.Size(24, 23);
			this.deleteMappingButton.TabIndex = 9;
			this.deleteMappingButton.UseVisualStyleBackColor = true;
			this.deleteMappingButton.Click += new System.EventHandler(this.DeleteMappingButtonClick);
			// 
			// editMappingLogicButton
			// 
			this.editMappingLogicButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.editMappingLogicButton.Image = ((System.Drawing.Image)(resources.GetObject("editMappingLogic.Image")));
			this.editMappingLogicButton.Location = new System.Drawing.Point(123, 558);
			this.editMappingLogicButton.Name = "editMappingLogic";
			this.editMappingLogicButton.Size = new System.Drawing.Size(24, 23);
			this.editMappingLogicButton.TabIndex = 8;
			this.editMappingLogicButton.UseVisualStyleBackColor = true;
			this.editMappingLogicButton.Click += new System.EventHandler(this.EditMappingLogicButtonClick);
			// 
			// deleteMappingLogicButton
			// 
			this.deleteMappingLogicButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.deleteMappingLogicButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteMappingLogic.Image")));
			this.deleteMappingLogicButton.Location = new System.Drawing.Point(153, 558);
			this.deleteMappingLogicButton.Name = "deleteMappingLogic";
			this.deleteMappingLogicButton.Size = new System.Drawing.Size(24, 23);
			this.deleteMappingLogicButton.TabIndex = 9;
			this.deleteMappingLogicButton.UseVisualStyleBackColor = true;
			this.deleteMappingLogicButton.Click += new System.EventHandler(this.DeleteMappingLogicButtonClick);
			// 
			// addNodeButton
			// 
			this.addNodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.addNodeButton.Image = ((System.Drawing.Image)(resources.GetObject("addNode.Image")));
			this.addNodeButton.Location = new System.Drawing.Point(183, 558);
			this.addNodeButton.Name = "addNode";
			this.addNodeButton.Size = new System.Drawing.Size(24, 23);
			this.addNodeButton.TabIndex = 10;
			this.addNodeButton.UseVisualStyleBackColor = true;
			this.addNodeButton.Click += new System.EventHandler(this.AddNodeButtonClick);
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
			// MappingControlGUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.exportButton);
			this.Controls.Add(this.goToTargetButton);
			this.Controls.Add(this.goToSourceButton);
			this.Controls.Add(this.createMappingButton);
			this.Controls.Add(this.deleteMappingButton);
			this.Controls.Add(this.editMappingLogicButton);
			this.Controls.Add(this.deleteMappingLogicButton);
			this.Controls.Add(this.addNodeButton);
			this.Controls.Add(this.trees);
			this.Name = "MappingControlGUI";
			this.Size = new System.Drawing.Size(994, 584);
			this.ResumeLayout(false);

		}
	}
}
