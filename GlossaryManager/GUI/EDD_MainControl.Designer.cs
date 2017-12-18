using System.Collections.Generic;
using System.Linq;
using BrightIdeasSoftware;
namespace GlossaryManager.GUI
{
	partial class EDD_MainControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel DomainPanel;
		private System.Windows.Forms.TabControl DetailsTabControl;
		private System.Windows.Forms.TabPage BusinessItemsTabPage;
		private System.Windows.Forms.TabPage DataItemsTabPage;
		private System.Windows.Forms.TabPage ColumnsTabPage;
		private System.Windows.Forms.Panel ButtonPanel;
		private ObjectListView BusinessItemsListView;
		private OLVColumn BU_Name;
		private OLVColumn BU_Domain;
		private System.Windows.Forms.TextBox BU_DomainTextBox;
		private System.Windows.Forms.Label BU_DomainLabel;
		private System.Windows.Forms.TextBox BU_NameTextBox;
		private System.Windows.Forms.Label BU_NameLabel;
		
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
			this.DomainPanel = new System.Windows.Forms.Panel();
			this.DetailsTabControl = new System.Windows.Forms.TabControl();
			this.BusinessItemsTabPage = new System.Windows.Forms.TabPage();
			this.BU_DomainTextBox = new System.Windows.Forms.TextBox();
			this.BU_DomainLabel = new System.Windows.Forms.Label();
			this.BU_NameTextBox = new System.Windows.Forms.TextBox();
			this.BU_NameLabel = new System.Windows.Forms.Label();
			this.BusinessItemsListView = new BrightIdeasSoftware.ObjectListView();
			this.BU_Name = new BrightIdeasSoftware.OLVColumn();
			this.BU_Domain = new BrightIdeasSoftware.OLVColumn();
			this.DataItemsTabPage = new System.Windows.Forms.TabPage();
			this.ColumnsTabPage = new System.Windows.Forms.TabPage();
			this.ButtonPanel = new System.Windows.Forms.Panel();
			this.DetailsTabControl.SuspendLayout();
			this.BusinessItemsTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BusinessItemsListView)).BeginInit();
			this.SuspendLayout();
			// 
			// DomainPanel
			// 
			this.DomainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.DomainPanel.Location = new System.Drawing.Point(0, 0);
			this.DomainPanel.Name = "DomainPanel";
			this.DomainPanel.Size = new System.Drawing.Size(937, 47);
			this.DomainPanel.TabIndex = 0;
			// 
			// DetailsTabControl
			// 
			this.DetailsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.DetailsTabControl.Controls.Add(this.BusinessItemsTabPage);
			this.DetailsTabControl.Controls.Add(this.DataItemsTabPage);
			this.DetailsTabControl.Controls.Add(this.ColumnsTabPage);
			this.DetailsTabControl.Location = new System.Drawing.Point(3, 53);
			this.DetailsTabControl.Name = "DetailsTabControl";
			this.DetailsTabControl.SelectedIndex = 0;
			this.DetailsTabControl.Size = new System.Drawing.Size(931, 555);
			this.DetailsTabControl.TabIndex = 1;
			// 
			// BusinessItemsTabPage
			// 
			this.BusinessItemsTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.BusinessItemsTabPage.Controls.Add(this.BU_DomainTextBox);
			this.BusinessItemsTabPage.Controls.Add(this.BU_DomainLabel);
			this.BusinessItemsTabPage.Controls.Add(this.BU_NameTextBox);
			this.BusinessItemsTabPage.Controls.Add(this.BU_NameLabel);
			this.BusinessItemsTabPage.Controls.Add(this.BusinessItemsListView);
			this.BusinessItemsTabPage.Location = new System.Drawing.Point(4, 22);
			this.BusinessItemsTabPage.Name = "BusinessItemsTabPage";
			this.BusinessItemsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.BusinessItemsTabPage.Size = new System.Drawing.Size(923, 529);
			this.BusinessItemsTabPage.TabIndex = 0;
			this.BusinessItemsTabPage.Text = "Business Items";
			// 
			// BU_DomainTextBox
			// 
			this.BU_DomainTextBox.Location = new System.Drawing.Point(113, 321);
			this.BU_DomainTextBox.Name = "BU_DomainTextBox";
			this.BU_DomainTextBox.Size = new System.Drawing.Size(184, 20);
			this.BU_DomainTextBox.TabIndex = 4;
			// 
			// BU_DomainLabel
			// 
			this.BU_DomainLabel.Location = new System.Drawing.Point(7, 324);
			this.BU_DomainLabel.Name = "BU_DomainLabel";
			this.BU_DomainLabel.Size = new System.Drawing.Size(100, 23);
			this.BU_DomainLabel.TabIndex = 3;
			this.BU_DomainLabel.Text = "Domain";
			// 
			// BU_NameTextBox
			// 
			this.BU_NameTextBox.Location = new System.Drawing.Point(113, 298);
			this.BU_NameTextBox.Name = "BU_NameTextBox";
			this.BU_NameTextBox.Size = new System.Drawing.Size(184, 20);
			this.BU_NameTextBox.TabIndex = 2;
			// 
			// BU_NameLabel
			// 
			this.BU_NameLabel.Location = new System.Drawing.Point(7, 301);
			this.BU_NameLabel.Name = "BU_NameLabel";
			this.BU_NameLabel.Size = new System.Drawing.Size(100, 23);
			this.BU_NameLabel.TabIndex = 1;
			this.BU_NameLabel.Text = "Name";
			// 
			// BusinessItemsListView
			// 
			this.BusinessItemsListView.BackColor = System.Drawing.SystemColors.Window;
			this.BusinessItemsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.BU_Name,
			this.BU_Domain});
			this.BusinessItemsListView.Cursor = System.Windows.Forms.Cursors.Default;
			this.BusinessItemsListView.FullRowSelect = true;
			this.BusinessItemsListView.HideSelection = false;
			this.BusinessItemsListView.Location = new System.Drawing.Point(3, 4);
			this.BusinessItemsListView.MultiSelect = false;
			this.BusinessItemsListView.Name = "BusinessItemsListView";
			this.BusinessItemsListView.ShowGroups = false;
			this.BusinessItemsListView.Size = new System.Drawing.Size(914, 290);
			this.BusinessItemsListView.TabIndex = 0;
			this.BusinessItemsListView.TintSortColumn = true;
			this.BusinessItemsListView.UseAlternatingBackColors = true;
			this.BusinessItemsListView.UseCompatibleStateImageBehavior = false;
			this.BusinessItemsListView.UseFilterIndicator = true;
			this.BusinessItemsListView.UseFiltering = true;
			this.BusinessItemsListView.UseHotItem = true;
			this.BusinessItemsListView.View = System.Windows.Forms.View.Details;
			this.BusinessItemsListView.SelectedIndexChanged += BusinessItemsListViewSelectedIndexChanged;
			// 
			// BU_Name
			// 
			this.BU_Name.AspectName = "Name";
			this.BU_Name.Text = "Name";
			this.BU_Name.Width = 150;
			// 
			// BU_Domain
			// 
			this.BU_Domain.AspectName = "Domain";
			this.BU_Domain.Text = "Domain";
			this.BU_Domain.Width = 100;
			// 
			// DataItemsTabPage
			// 
			this.DataItemsTabPage.Location = new System.Drawing.Point(4, 22);
			this.DataItemsTabPage.Name = "DataItemsTabPage";
			this.DataItemsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.DataItemsTabPage.Size = new System.Drawing.Size(923, 529);
			this.DataItemsTabPage.TabIndex = 1;
			this.DataItemsTabPage.Text = "Data Items";
			this.DataItemsTabPage.UseVisualStyleBackColor = true;
			// 
			// ColumnsTabPage
			// 
			this.ColumnsTabPage.Location = new System.Drawing.Point(4, 22);
			this.ColumnsTabPage.Name = "ColumnsTabPage";
			this.ColumnsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.ColumnsTabPage.Size = new System.Drawing.Size(923, 529);
			this.ColumnsTabPage.TabIndex = 2;
			this.ColumnsTabPage.Text = "Columns";
			this.ColumnsTabPage.UseVisualStyleBackColor = true;
			// 
			// ButtonPanel
			// 
			this.ButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonPanel.Location = new System.Drawing.Point(0, 614);
			this.ButtonPanel.Name = "ButtonPanel";
			this.ButtonPanel.Size = new System.Drawing.Size(937, 39);
			this.ButtonPanel.TabIndex = 2;
			// 
			// EDD_MainControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ButtonPanel);
			this.Controls.Add(this.DetailsTabControl);
			this.Controls.Add(this.DomainPanel);
			this.Name = "EDD_MainControl";
			this.Size = new System.Drawing.Size(937, 653);
			this.DetailsTabControl.ResumeLayout(false);
			this.BusinessItemsTabPage.ResumeLayout(false);
			this.BusinessItemsTabPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.BusinessItemsListView)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
