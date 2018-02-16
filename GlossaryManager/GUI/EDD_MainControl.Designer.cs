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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EDD_MainControl));
            this.DomainPanel = new System.Windows.Forms.Panel();
            this.domainBreadCrumb = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumb();
            this.DetailsTabControl = new System.Windows.Forms.TabControl();
            this.BusinessItemsTabPage = new System.Windows.Forms.TabPage();
            this.BU_ModifiedByTextBox = new System.Windows.Forms.TextBox();
            this.BU_ModifiedByLabel = new System.Windows.Forms.Label();
            this.BU_ModifiedDateTextBox = new System.Windows.Forms.TextBox();
            this.BU_ModifiedLabel = new System.Windows.Forms.Label();
            this.BU_CreatedByTextBox = new System.Windows.Forms.TextBox();
            this.BU_CreatedByLabel = new System.Windows.Forms.Label();
            this.BU_CreatedTextBox = new System.Windows.Forms.TextBox();
            this.BU_CreatedLabel = new System.Windows.Forms.Label();
            this.BU_KeywordsTextBox = new System.Windows.Forms.TextBox();
            this.BU_KeywordsLabel = new System.Windows.Forms.Label();
            this.BU_StatusCombobox = new System.Windows.Forms.ComboBox();
            this.BU_StatusLabel = new System.Windows.Forms.Label();
            this.BU_VersionTextBox = new System.Windows.Forms.TextBox();
            this.BU_VersionLabel = new System.Windows.Forms.Label();
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
            this.deleteButton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.openPropertiesButton = new System.Windows.Forms.Button();
            this.navigateProjectBrowserButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.DomainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.domainBreadCrumb)).BeginInit();
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
            this.DomainPanel.Controls.Add(this.domainBreadCrumb);
            this.DomainPanel.Location = new System.Drawing.Point(0, 0);
            this.DomainPanel.Name = "DomainPanel";
            this.DomainPanel.Size = new System.Drawing.Size(937, 28);
            this.DomainPanel.TabIndex = 0;
            // 
            // domainBreadCrumb
            // 
            this.domainBreadCrumb.AutoSize = false;
            this.domainBreadCrumb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.domainBreadCrumb.Location = new System.Drawing.Point(0, 0);
            this.domainBreadCrumb.Name = "domainBreadCrumb";
            this.domainBreadCrumb.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            // 
            // 
            // 
            this.domainBreadCrumb.RootItem.ShortText = "Domains";
            this.domainBreadCrumb.SelectedItem = this.domainBreadCrumb.RootItem;
            this.domainBreadCrumb.Size = new System.Drawing.Size(937, 28);
            this.domainBreadCrumb.TabIndex = 0;
            this.domainBreadCrumb.SelectedItemChanged += new System.EventHandler(this.domainBreadCrumb_SelectedItemChanged);
            // 
            // DetailsTabControl
            // 
            this.DetailsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailsTabControl.Controls.Add(this.BusinessItemsTabPage);
            this.DetailsTabControl.Controls.Add(this.DataItemsTabPage);
            this.DetailsTabControl.Controls.Add(this.ColumnsTabPage);
            this.DetailsTabControl.Location = new System.Drawing.Point(0, 34);
            this.DetailsTabControl.Name = "DetailsTabControl";
            this.DetailsTabControl.SelectedIndex = 0;
            this.DetailsTabControl.Size = new System.Drawing.Size(931, 577);
            this.DetailsTabControl.TabIndex = 1;
            // 
            // BusinessItemsTabPage
            // 
            this.BusinessItemsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.BusinessItemsTabPage.Controls.Add(this.BU_ModifiedByTextBox);
            this.BusinessItemsTabPage.Controls.Add(this.BU_ModifiedByLabel);
            this.BusinessItemsTabPage.Controls.Add(this.BU_ModifiedDateTextBox);
            this.BusinessItemsTabPage.Controls.Add(this.BU_ModifiedLabel);
            this.BusinessItemsTabPage.Controls.Add(this.BU_CreatedByTextBox);
            this.BusinessItemsTabPage.Controls.Add(this.BU_CreatedByLabel);
            this.BusinessItemsTabPage.Controls.Add(this.BU_CreatedTextBox);
            this.BusinessItemsTabPage.Controls.Add(this.BU_CreatedLabel);
            this.BusinessItemsTabPage.Controls.Add(this.BU_KeywordsTextBox);
            this.BusinessItemsTabPage.Controls.Add(this.BU_KeywordsLabel);
            this.BusinessItemsTabPage.Controls.Add(this.BU_StatusCombobox);
            this.BusinessItemsTabPage.Controls.Add(this.BU_StatusLabel);
            this.BusinessItemsTabPage.Controls.Add(this.BU_VersionTextBox);
            this.BusinessItemsTabPage.Controls.Add(this.BU_VersionLabel);
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
            this.BusinessItemsTabPage.Size = new System.Drawing.Size(923, 551);
            this.BusinessItemsTabPage.TabIndex = 0;
            this.BusinessItemsTabPage.Text = "Business Items";
            // 
            // BU_ModifiedByTextBox
            // 
            this.BU_ModifiedByTextBox.Location = new System.Drawing.Point(732, 382);
            this.BU_ModifiedByTextBox.Name = "BU_ModifiedByTextBox";
            this.BU_ModifiedByTextBox.ReadOnly = true;
            this.BU_ModifiedByTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_ModifiedByTextBox.TabIndex = 21;
            // 
            // BU_ModifiedByLabel
            // 
            this.BU_ModifiedByLabel.Location = new System.Drawing.Point(626, 385);
            this.BU_ModifiedByLabel.Name = "BU_ModifiedByLabel";
            this.BU_ModifiedByLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_ModifiedByLabel.TabIndex = 20;
            this.BU_ModifiedByLabel.Text = "Modified by";
            // 
            // BU_ModifiedDateTextBox
            // 
            this.BU_ModifiedDateTextBox.Location = new System.Drawing.Point(732, 356);
            this.BU_ModifiedDateTextBox.Name = "BU_ModifiedDateTextBox";
            this.BU_ModifiedDateTextBox.ReadOnly = true;
            this.BU_ModifiedDateTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_ModifiedDateTextBox.TabIndex = 19;
            // 
            // BU_ModifiedLabel
            // 
            this.BU_ModifiedLabel.Location = new System.Drawing.Point(626, 359);
            this.BU_ModifiedLabel.Name = "BU_ModifiedLabel";
            this.BU_ModifiedLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_ModifiedLabel.TabIndex = 18;
            this.BU_ModifiedLabel.Text = "Modified date";
            // 
            // BU_CreatedByTextBox
            // 
            this.BU_CreatedByTextBox.Location = new System.Drawing.Point(732, 329);
            this.BU_CreatedByTextBox.Name = "BU_CreatedByTextBox";
            this.BU_CreatedByTextBox.ReadOnly = true;
            this.BU_CreatedByTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_CreatedByTextBox.TabIndex = 17;
            // 
            // BU_CreatedByLabel
            // 
            this.BU_CreatedByLabel.Location = new System.Drawing.Point(626, 332);
            this.BU_CreatedByLabel.Name = "BU_CreatedByLabel";
            this.BU_CreatedByLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_CreatedByLabel.TabIndex = 16;
            this.BU_CreatedByLabel.Text = "Created by";
            // 
            // BU_CreatedTextBox
            // 
            this.BU_CreatedTextBox.Location = new System.Drawing.Point(732, 303);
            this.BU_CreatedTextBox.Name = "BU_CreatedTextBox";
            this.BU_CreatedTextBox.ReadOnly = true;
            this.BU_CreatedTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_CreatedTextBox.TabIndex = 15;
            // 
            // BU_CreatedLabel
            // 
            this.BU_CreatedLabel.Location = new System.Drawing.Point(626, 306);
            this.BU_CreatedLabel.Name = "BU_CreatedLabel";
            this.BU_CreatedLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_CreatedLabel.TabIndex = 14;
            this.BU_CreatedLabel.Text = "Creation date";
            // 
            // BU_KeywordsTextBox
            // 
            this.BU_KeywordsTextBox.Location = new System.Drawing.Point(419, 382);
            this.BU_KeywordsTextBox.Name = "BU_KeywordsTextBox";
            this.BU_KeywordsTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_KeywordsTextBox.TabIndex = 13;
            // 
            // BU_KeywordsLabel
            // 
            this.BU_KeywordsLabel.Location = new System.Drawing.Point(313, 385);
            this.BU_KeywordsLabel.Name = "BU_KeywordsLabel";
            this.BU_KeywordsLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_KeywordsLabel.TabIndex = 12;
            this.BU_KeywordsLabel.Text = "Keywords";
            // 
            // BU_StatusCombobox
            // 
            this.BU_StatusCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BU_StatusCombobox.FormattingEnabled = true;
            this.BU_StatusCombobox.Location = new System.Drawing.Point(419, 355);
            this.BU_StatusCombobox.Name = "BU_StatusCombobox";
            this.BU_StatusCombobox.Size = new System.Drawing.Size(184, 21);
            this.BU_StatusCombobox.TabIndex = 11;
            // 
            // BU_StatusLabel
            // 
            this.BU_StatusLabel.Location = new System.Drawing.Point(313, 358);
            this.BU_StatusLabel.Name = "BU_StatusLabel";
            this.BU_StatusLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_StatusLabel.TabIndex = 10;
            this.BU_StatusLabel.Text = "Status";
            // 
            // BU_VersionTextBox
            // 
            this.BU_VersionTextBox.Location = new System.Drawing.Point(419, 329);
            this.BU_VersionTextBox.Name = "BU_VersionTextBox";
            this.BU_VersionTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_VersionTextBox.TabIndex = 9;
            // 
            // BU_VersionLabel
            // 
            this.BU_VersionLabel.Location = new System.Drawing.Point(313, 332);
            this.BU_VersionLabel.Name = "BU_VersionLabel";
            this.BU_VersionLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_VersionLabel.TabIndex = 8;
            this.BU_VersionLabel.Text = "Version";
            // 
            // BU_DomainComboBox
            // 
            this.BU_DomainComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BU_DomainComboBox.FormattingEnabled = true;
            this.BU_DomainComboBox.Location = new System.Drawing.Point(419, 303);
            this.BU_DomainComboBox.Name = "BU_DomainComboBox";
            this.BU_DomainComboBox.Size = new System.Drawing.Size(184, 21);
            this.BU_DomainComboBox.TabIndex = 7;
            // 
            // BU_DescriptionTextBox
            // 
            this.BU_DescriptionTextBox.Location = new System.Drawing.Point(112, 329);
            this.BU_DescriptionTextBox.Multiline = true;
            this.BU_DescriptionTextBox.Name = "BU_DescriptionTextBox";
            this.BU_DescriptionTextBox.Size = new System.Drawing.Size(184, 96);
            this.BU_DescriptionTextBox.TabIndex = 6;
            // 
            // BU_DescriptionLabel
            // 
            this.BU_DescriptionLabel.Location = new System.Drawing.Point(6, 332);
            this.BU_DescriptionLabel.Name = "BU_DescriptionLabel";
            this.BU_DescriptionLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_DescriptionLabel.TabIndex = 5;
            this.BU_DescriptionLabel.Text = "Description";
            // 
            // BU_DomainLabel
            // 
            this.BU_DomainLabel.Location = new System.Drawing.Point(313, 306);
            this.BU_DomainLabel.Name = "BU_DomainLabel";
            this.BU_DomainLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_DomainLabel.TabIndex = 3;
            this.BU_DomainLabel.Text = "Domain";
            // 
            // BU_NameTextBox
            // 
            this.BU_NameTextBox.Location = new System.Drawing.Point(112, 303);
            this.BU_NameTextBox.Name = "BU_NameTextBox";
            this.BU_NameTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_NameTextBox.TabIndex = 2;
            // 
            // BU_NameLabel
            // 
            this.BU_NameLabel.Location = new System.Drawing.Point(6, 306);
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
            this.BusinessItemsListView.CellEditUseWholeCell = false;
            this.BusinessItemsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.BU_NameCol,
            this.BU_DomainCol,
            this.BU_DescriptionCol});
            this.BusinessItemsListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.BusinessItemsListView.FullRowSelect = true;
            this.BusinessItemsListView.GridLines = true;
            this.BusinessItemsListView.HideSelection = false;
            this.BusinessItemsListView.Location = new System.Drawing.Point(3, 4);
            this.BusinessItemsListView.MultiSelect = false;
            this.BusinessItemsListView.Name = "BusinessItemsListView";
            this.BusinessItemsListView.ShowCommandMenuOnRightClick = true;
            this.BusinessItemsListView.ShowGroups = false;
            this.BusinessItemsListView.Size = new System.Drawing.Size(914, 290);
            this.BusinessItemsListView.TabIndex = 0;
            this.BusinessItemsListView.TintSortColumn = true;
            this.BusinessItemsListView.UseCompatibleStateImageBehavior = false;
            this.BusinessItemsListView.UseFilterIndicator = true;
            this.BusinessItemsListView.UseFiltering = true;
            this.BusinessItemsListView.UseHotItem = true;
            this.BusinessItemsListView.View = System.Windows.Forms.View.Details;
            this.BusinessItemsListView.SelectedIndexChanged += new System.EventHandler(this.BusinessItemsListView_SelectedIndexChanged);
            this.BusinessItemsListView.DoubleClick += new System.EventHandler(this.BusinessItemsListView_DoubleClick);
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
            this.DataItemsTabPage.Size = new System.Drawing.Size(923, 551);
            this.DataItemsTabPage.TabIndex = 1;
            this.DataItemsTabPage.Text = "Data Items";
            this.DataItemsTabPage.UseVisualStyleBackColor = true;
            // 
            // ColumnsTabPage
            // 
            this.ColumnsTabPage.Location = new System.Drawing.Point(4, 22);
            this.ColumnsTabPage.Name = "ColumnsTabPage";
            this.ColumnsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ColumnsTabPage.Size = new System.Drawing.Size(923, 551);
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
            this.ButtonPanel.Controls.Add(this.deleteButton);
            this.ButtonPanel.Controls.Add(this.newButton);
            this.ButtonPanel.Controls.Add(this.openPropertiesButton);
            this.ButtonPanel.Controls.Add(this.navigateProjectBrowserButton);
            this.ButtonPanel.Controls.Add(this.cancelButton);
            this.ButtonPanel.Controls.Add(this.saveButton);
            this.ButtonPanel.Location = new System.Drawing.Point(0, 610);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(937, 43);
            this.ButtonPanel.TabIndex = 2;
            // 
            // deleteButton
            // 
            this.deleteButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteButton.Image")));
            this.deleteButton.Location = new System.Drawing.Point(128, 7);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(29, 28);
            this.deleteButton.TabIndex = 5;
            this.myToolTip.SetToolTip(this.deleteButton, "Delete Element");
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // newButton
            // 
            this.newButton.Image = ((System.Drawing.Image)(resources.GetObject("newButton.Image")));
            this.newButton.Location = new System.Drawing.Point(93, 7);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(29, 28);
            this.newButton.TabIndex = 4;
            this.myToolTip.SetToolTip(this.newButton, "Select in Project Browser");
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // openPropertiesButton
            // 
            this.openPropertiesButton.Image = ((System.Drawing.Image)(resources.GetObject("openPropertiesButton.Image")));
            this.openPropertiesButton.Location = new System.Drawing.Point(39, 7);
            this.openPropertiesButton.Name = "openPropertiesButton";
            this.openPropertiesButton.Size = new System.Drawing.Size(29, 28);
            this.openPropertiesButton.TabIndex = 3;
            this.myToolTip.SetToolTip(this.openPropertiesButton, "Open Properties");
            this.openPropertiesButton.UseVisualStyleBackColor = true;
            this.openPropertiesButton.Click += new System.EventHandler(this.openPropertiesButton_Click);
            // 
            // navigateProjectBrowserButton
            // 
            this.navigateProjectBrowserButton.Image = ((System.Drawing.Image)(resources.GetObject("navigateProjectBrowserButton.Image")));
            this.navigateProjectBrowserButton.Location = new System.Drawing.Point(4, 7);
            this.navigateProjectBrowserButton.Name = "navigateProjectBrowserButton";
            this.navigateProjectBrowserButton.Size = new System.Drawing.Size(29, 28);
            this.navigateProjectBrowserButton.TabIndex = 2;
            this.myToolTip.SetToolTip(this.navigateProjectBrowserButton, "Select in Project Browser");
            this.navigateProjectBrowserButton.UseVisualStyleBackColor = true;
            this.navigateProjectBrowserButton.Click += new System.EventHandler(this.navigateProjectBrowserButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(849, 7);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 28);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(768, 7);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 28);
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
            this.DomainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.domainBreadCrumb)).EndInit();
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
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumb domainBreadCrumb;
        private System.Windows.Forms.TextBox BU_CreatedByTextBox;
        private System.Windows.Forms.Label BU_CreatedByLabel;
        private System.Windows.Forms.TextBox BU_CreatedTextBox;
        private System.Windows.Forms.Label BU_CreatedLabel;
        private System.Windows.Forms.TextBox BU_KeywordsTextBox;
        private System.Windows.Forms.Label BU_KeywordsLabel;
        private System.Windows.Forms.ComboBox BU_StatusCombobox;
        private System.Windows.Forms.Label BU_StatusLabel;
        private System.Windows.Forms.TextBox BU_VersionTextBox;
        private System.Windows.Forms.Label BU_VersionLabel;
        private System.Windows.Forms.TextBox BU_ModifiedByTextBox;
        private System.Windows.Forms.Label BU_ModifiedByLabel;
        private System.Windows.Forms.TextBox BU_ModifiedDateTextBox;
        private System.Windows.Forms.Label BU_ModifiedLabel;
        private System.Windows.Forms.Button openPropertiesButton;
        private System.Windows.Forms.ToolTip myToolTip;
        private System.Windows.Forms.Button navigateProjectBrowserButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button newButton;
    }
}
