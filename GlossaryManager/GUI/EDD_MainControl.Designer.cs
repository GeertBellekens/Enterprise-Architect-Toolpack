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
            this.BU_DomainComboBox = new System.Windows.Forms.ComboBox();
            this.BU_DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.BU_DescriptionLabel = new System.Windows.Forms.Label();
            this.BU_DomainLabel = new System.Windows.Forms.Label();
            this.BU_NameTextBox = new System.Windows.Forms.TextBox();
            this.BU_NameLabel = new System.Windows.Forms.Label();
            this.BusinessItemsListView = new BrightIdeasSoftware.ObjectListView();
            this.BU_NameCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.BU_DomainCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.BU_DescriptionCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.DataItemsTabPage = new System.Windows.Forms.TabPage();
            this.ColumnsTabPage = new System.Windows.Forms.TabPage();
            this.BU_Name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.BU_Domain = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.DetailsTabControl.SuspendLayout();
            this.BusinessItemsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BusinessItemsListView)).BeginInit();
            this.ButtonPanel.SuspendLayout();
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
            this.BusinessItemsTabPage.Controls.Add(this.BU_DomainComboBox);
            this.BusinessItemsTabPage.Controls.Add(this.BU_DescriptionTextBox);
            this.BusinessItemsTabPage.Controls.Add(this.BU_DescriptionLabel);
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
            // BU_DomainComboBox
            // 
            this.BU_DomainComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BU_DomainComboBox.FormattingEnabled = true;
            this.BU_DomainComboBox.Location = new System.Drawing.Point(113, 427);
            this.BU_DomainComboBox.Name = "BU_DomainComboBox";
            this.BU_DomainComboBox.Size = new System.Drawing.Size(184, 21);
            this.BU_DomainComboBox.TabIndex = 7;
            // 
            // BU_DescriptionTextBox
            // 
            this.BU_DescriptionTextBox.Location = new System.Drawing.Point(113, 324);
            this.BU_DescriptionTextBox.Multiline = true;
            this.BU_DescriptionTextBox.Name = "BU_DescriptionTextBox";
            this.BU_DescriptionTextBox.Size = new System.Drawing.Size(184, 96);
            this.BU_DescriptionTextBox.TabIndex = 6;
            // 
            // BU_DescriptionLabel
            // 
            this.BU_DescriptionLabel.Location = new System.Drawing.Point(7, 327);
            this.BU_DescriptionLabel.Name = "BU_DescriptionLabel";
            this.BU_DescriptionLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_DescriptionLabel.TabIndex = 5;
            this.BU_DescriptionLabel.Text = "Description";
            // 
            // BU_DomainLabel
            // 
            this.BU_DomainLabel.Location = new System.Drawing.Point(7, 430);
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
            this.BusinessItemsListView.AllColumns.Add(this.BU_NameCol);
            this.BusinessItemsListView.AllColumns.Add(this.BU_DomainCol);
            this.BusinessItemsListView.AllColumns.Add(this.BU_DescriptionCol);
            this.BusinessItemsListView.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.BusinessItemsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BusinessItemsListView.BackColor = System.Drawing.SystemColors.Window;
            this.BusinessItemsListView.CellEditUseWholeCell = false;
            this.BusinessItemsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.BU_NameCol,
            this.BU_DomainCol,
            this.BU_DescriptionCol});
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
            this.BusinessItemsListView.SelectedIndexChanged += new System.EventHandler(this.BusinessItemsListView_SelectedIndexChanged);
            // 
            // BU_NameCol
            // 
            this.BU_NameCol.AspectName = "Name";
            this.BU_NameCol.Text = "Name";
            this.BU_NameCol.ToolTipText = "Name of the Business Item";
            this.BU_NameCol.Width = 200;
            // 
            // BU_DomainCol
            // 
            this.BU_DomainCol.AspectName = "domainPath";
            this.BU_DomainCol.Text = "Domain";
            this.BU_DomainCol.ToolTipText = "The domain of the Business Item";
            this.BU_DomainCol.Width = 200;
            // 
            // BU_DescriptionCol
            // 
            this.BU_DescriptionCol.AspectName = "Description";
            this.BU_DescriptionCol.FillsFreeSpace = true;
            this.BU_DescriptionCol.Text = "Description";
            this.BU_DescriptionCol.ToolTipText = "Description of this Business Item";
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
            // BU_Name
            // 
            this.BU_Name.AspectName = "Name";
            this.BU_Name.DisplayIndex = 0;
            this.BU_Name.Text = "Name";
            this.BU_Name.Width = 150;
            // 
            // BU_Domain
            // 
            this.BU_Domain.AspectName = "Domain";
            this.BU_Domain.DisplayIndex = 1;
            this.BU_Domain.Text = "Domain";
            this.BU_Domain.Width = 100;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonPanel.Controls.Add(this.cancelButton);
            this.ButtonPanel.Controls.Add(this.saveButton);
            this.ButtonPanel.Location = new System.Drawing.Point(0, 610);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(937, 43);
            this.ButtonPanel.TabIndex = 2;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(849, 11);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 20);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(768, 11);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 20);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
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
            this.ButtonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        private OLVColumn BU_NameCol;
        private OLVColumn BU_DomainCol;
        private System.Windows.Forms.TextBox BU_DescriptionTextBox;
        private System.Windows.Forms.Label BU_DescriptionLabel;
        private OLVColumn BU_DescriptionCol;
        private System.Windows.Forms.ComboBox BU_DomainComboBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
    }
}
