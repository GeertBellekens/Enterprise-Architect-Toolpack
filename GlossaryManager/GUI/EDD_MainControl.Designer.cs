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
            this.DI_PrecisionUpDown = new System.Windows.Forms.NumericUpDown();
            this.DI_SizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.DI_FormatTextBox = new System.Windows.Forms.TextBox();
            this.DI_FormatLabel = new System.Windows.Forms.Label();
            this.DI_LabelTextBox = new System.Windows.Forms.TextBox();
            this.DI_LabelLabel = new System.Windows.Forms.Label();
            this.DI_InitialValueTextBox = new System.Windows.Forms.TextBox();
            this.DI_InitialValueLabel = new System.Windows.Forms.Label();
            this.DI_PrecisionLabel = new System.Windows.Forms.Label();
            this.DI_SizeLabel = new System.Windows.Forms.Label();
            this.DI_DatatypeSelectButton = new System.Windows.Forms.Button();
            this.DI_DatatypeTextBox = new System.Windows.Forms.TextBox();
            this.DI_DatatypeLabel = new System.Windows.Forms.Label();
            this.DI_DomainComboBox = new System.Windows.Forms.ComboBox();
            this.DI_DomainLabel = new System.Windows.Forms.Label();
            this.DI_BusinessItemSelectButton = new System.Windows.Forms.Button();
            this.DI_BusinessItemTextBox = new System.Windows.Forms.TextBox();
            this.DI_ModifiedUserTextBox = new System.Windows.Forms.TextBox();
            this.DI_ModifieUserLabel = new System.Windows.Forms.Label();
            this.DI_ModifiedDateTextBox = new System.Windows.Forms.TextBox();
            this.DI_ModifiedDateLabel = new System.Windows.Forms.Label();
            this.DI_CreatedUserTextBox = new System.Windows.Forms.TextBox();
            this.DI_CreatedUserLabel = new System.Windows.Forms.Label();
            this.DI_CreationDateTextBox = new System.Windows.Forms.TextBox();
            this.DI_CreationDateLabel = new System.Windows.Forms.Label();
            this.DI_KeywordsTextBox = new System.Windows.Forms.TextBox();
            this.DI_KeywordsLabel = new System.Windows.Forms.Label();
            this.DI_StatusComboBox = new System.Windows.Forms.ComboBox();
            this.DI_StatusLabel = new System.Windows.Forms.Label();
            this.DI_VersionTextBox = new System.Windows.Forms.TextBox();
            this.DI_VersionLabel = new System.Windows.Forms.Label();
            this.DI_DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.DI_DescriptionLabel = new System.Windows.Forms.Label();
            this.DI_BusinessItemLabel = new System.Windows.Forms.Label();
            this.DI_NameTextBox = new System.Windows.Forms.TextBox();
            this.DI_NameLabel = new System.Windows.Forms.Label();
            this.dataItemsListView = new BrightIdeasSoftware.ObjectListView();
            this.DI_NameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.DI_LabelColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.DI_DatatypeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.DI_DomainColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.DI_BusinessItem = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ColumnsTabPage = new System.Windows.Forms.TabPage();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.columnsListView = new BrightIdeasSoftware.ObjectListView();
            this.C_TableColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.C_PropertiesColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.C_ColumnColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
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
            this.C_DataItem = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.DomainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.domainBreadCrumb)).BeginInit();
            this.DetailsTabControl.SuspendLayout();
            this.BusinessItemsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BusinessItemsListView)).BeginInit();
            this.DataItemsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DI_PrecisionUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DI_SizeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataItemsListView)).BeginInit();
            this.ColumnsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnsListView)).BeginInit();
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
            this.DetailsTabControl.SelectedIndexChanged += new System.EventHandler(this.DetailsTabControl_SelectedIndexChanged);
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
            this.BU_ModifiedByTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_ModifiedByTextBox.Location = new System.Drawing.Point(732, 382);
            this.BU_ModifiedByTextBox.Name = "BU_ModifiedByTextBox";
            this.BU_ModifiedByTextBox.ReadOnly = true;
            this.BU_ModifiedByTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_ModifiedByTextBox.TabIndex = 10;
            // 
            // BU_ModifiedByLabel
            // 
            this.BU_ModifiedByLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_ModifiedByLabel.Location = new System.Drawing.Point(626, 385);
            this.BU_ModifiedByLabel.Name = "BU_ModifiedByLabel";
            this.BU_ModifiedByLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_ModifiedByLabel.TabIndex = 20;
            this.BU_ModifiedByLabel.Text = "Modified by";
            // 
            // BU_ModifiedDateTextBox
            // 
            this.BU_ModifiedDateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_ModifiedDateTextBox.Location = new System.Drawing.Point(732, 356);
            this.BU_ModifiedDateTextBox.Name = "BU_ModifiedDateTextBox";
            this.BU_ModifiedDateTextBox.ReadOnly = true;
            this.BU_ModifiedDateTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_ModifiedDateTextBox.TabIndex = 9;
            // 
            // BU_ModifiedLabel
            // 
            this.BU_ModifiedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_ModifiedLabel.Location = new System.Drawing.Point(626, 359);
            this.BU_ModifiedLabel.Name = "BU_ModifiedLabel";
            this.BU_ModifiedLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_ModifiedLabel.TabIndex = 18;
            this.BU_ModifiedLabel.Text = "Modified date";
            // 
            // BU_CreatedByTextBox
            // 
            this.BU_CreatedByTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_CreatedByTextBox.Location = new System.Drawing.Point(732, 329);
            this.BU_CreatedByTextBox.Name = "BU_CreatedByTextBox";
            this.BU_CreatedByTextBox.ReadOnly = true;
            this.BU_CreatedByTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_CreatedByTextBox.TabIndex = 8;
            // 
            // BU_CreatedByLabel
            // 
            this.BU_CreatedByLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_CreatedByLabel.Location = new System.Drawing.Point(626, 332);
            this.BU_CreatedByLabel.Name = "BU_CreatedByLabel";
            this.BU_CreatedByLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_CreatedByLabel.TabIndex = 16;
            this.BU_CreatedByLabel.Text = "Created by";
            // 
            // BU_CreatedTextBox
            // 
            this.BU_CreatedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_CreatedTextBox.Location = new System.Drawing.Point(732, 303);
            this.BU_CreatedTextBox.Name = "BU_CreatedTextBox";
            this.BU_CreatedTextBox.ReadOnly = true;
            this.BU_CreatedTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_CreatedTextBox.TabIndex = 7;
            // 
            // BU_CreatedLabel
            // 
            this.BU_CreatedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_CreatedLabel.Location = new System.Drawing.Point(626, 306);
            this.BU_CreatedLabel.Name = "BU_CreatedLabel";
            this.BU_CreatedLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_CreatedLabel.TabIndex = 14;
            this.BU_CreatedLabel.Text = "Creation date";
            // 
            // BU_KeywordsTextBox
            // 
            this.BU_KeywordsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_KeywordsTextBox.Location = new System.Drawing.Point(419, 382);
            this.BU_KeywordsTextBox.Name = "BU_KeywordsTextBox";
            this.BU_KeywordsTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_KeywordsTextBox.TabIndex = 6;
            // 
            // BU_KeywordsLabel
            // 
            this.BU_KeywordsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_KeywordsLabel.Location = new System.Drawing.Point(313, 385);
            this.BU_KeywordsLabel.Name = "BU_KeywordsLabel";
            this.BU_KeywordsLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_KeywordsLabel.TabIndex = 12;
            this.BU_KeywordsLabel.Text = "Keywords";
            // 
            // BU_StatusCombobox
            // 
            this.BU_StatusCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_StatusCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BU_StatusCombobox.FormattingEnabled = true;
            this.BU_StatusCombobox.Location = new System.Drawing.Point(419, 355);
            this.BU_StatusCombobox.Name = "BU_StatusCombobox";
            this.BU_StatusCombobox.Size = new System.Drawing.Size(184, 21);
            this.BU_StatusCombobox.TabIndex = 5;
            // 
            // BU_StatusLabel
            // 
            this.BU_StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_StatusLabel.Location = new System.Drawing.Point(313, 358);
            this.BU_StatusLabel.Name = "BU_StatusLabel";
            this.BU_StatusLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_StatusLabel.TabIndex = 10;
            this.BU_StatusLabel.Text = "Status";
            // 
            // BU_VersionTextBox
            // 
            this.BU_VersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_VersionTextBox.Location = new System.Drawing.Point(419, 329);
            this.BU_VersionTextBox.Name = "BU_VersionTextBox";
            this.BU_VersionTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_VersionTextBox.TabIndex = 4;
            // 
            // BU_VersionLabel
            // 
            this.BU_VersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_VersionLabel.Location = new System.Drawing.Point(313, 332);
            this.BU_VersionLabel.Name = "BU_VersionLabel";
            this.BU_VersionLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_VersionLabel.TabIndex = 8;
            this.BU_VersionLabel.Text = "Version";
            // 
            // BU_DomainComboBox
            // 
            this.BU_DomainComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_DomainComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BU_DomainComboBox.FormattingEnabled = true;
            this.BU_DomainComboBox.Location = new System.Drawing.Point(419, 303);
            this.BU_DomainComboBox.Name = "BU_DomainComboBox";
            this.BU_DomainComboBox.Size = new System.Drawing.Size(184, 21);
            this.BU_DomainComboBox.TabIndex = 3;
            // 
            // BU_DescriptionTextBox
            // 
            this.BU_DescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_DescriptionTextBox.Location = new System.Drawing.Point(112, 329);
            this.BU_DescriptionTextBox.Multiline = true;
            this.BU_DescriptionTextBox.Name = "BU_DescriptionTextBox";
            this.BU_DescriptionTextBox.Size = new System.Drawing.Size(184, 96);
            this.BU_DescriptionTextBox.TabIndex = 2;
            // 
            // BU_DescriptionLabel
            // 
            this.BU_DescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_DescriptionLabel.Location = new System.Drawing.Point(6, 332);
            this.BU_DescriptionLabel.Name = "BU_DescriptionLabel";
            this.BU_DescriptionLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_DescriptionLabel.TabIndex = 5;
            this.BU_DescriptionLabel.Text = "Description";
            // 
            // BU_DomainLabel
            // 
            this.BU_DomainLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_DomainLabel.Location = new System.Drawing.Point(313, 306);
            this.BU_DomainLabel.Name = "BU_DomainLabel";
            this.BU_DomainLabel.Size = new System.Drawing.Size(100, 23);
            this.BU_DomainLabel.TabIndex = 3;
            this.BU_DomainLabel.Text = "Domain";
            // 
            // BU_NameTextBox
            // 
            this.BU_NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BU_NameTextBox.Location = new System.Drawing.Point(112, 303);
            this.BU_NameTextBox.Name = "BU_NameTextBox";
            this.BU_NameTextBox.Size = new System.Drawing.Size(184, 20);
            this.BU_NameTextBox.TabIndex = 1;
            // 
            // BU_NameLabel
            // 
            this.BU_NameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.BusinessItemsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
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
            this.DataItemsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.DataItemsTabPage.Controls.Add(this.DI_PrecisionUpDown);
            this.DataItemsTabPage.Controls.Add(this.DI_SizeNumericUpDown);
            this.DataItemsTabPage.Controls.Add(this.DI_FormatTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_FormatLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_LabelTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_LabelLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_InitialValueTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_InitialValueLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_PrecisionLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_SizeLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_DatatypeSelectButton);
            this.DataItemsTabPage.Controls.Add(this.DI_DatatypeTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_DatatypeLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_DomainComboBox);
            this.DataItemsTabPage.Controls.Add(this.DI_DomainLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_BusinessItemSelectButton);
            this.DataItemsTabPage.Controls.Add(this.DI_BusinessItemTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_ModifiedUserTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_ModifieUserLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_ModifiedDateTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_ModifiedDateLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_CreatedUserTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_CreatedUserLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_CreationDateTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_CreationDateLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_KeywordsTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_KeywordsLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_StatusComboBox);
            this.DataItemsTabPage.Controls.Add(this.DI_StatusLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_VersionTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_VersionLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_DescriptionTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_DescriptionLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_BusinessItemLabel);
            this.DataItemsTabPage.Controls.Add(this.DI_NameTextBox);
            this.DataItemsTabPage.Controls.Add(this.DI_NameLabel);
            this.DataItemsTabPage.Controls.Add(this.dataItemsListView);
            this.DataItemsTabPage.Location = new System.Drawing.Point(4, 22);
            this.DataItemsTabPage.Name = "DataItemsTabPage";
            this.DataItemsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DataItemsTabPage.Size = new System.Drawing.Size(923, 551);
            this.DataItemsTabPage.TabIndex = 1;
            this.DataItemsTabPage.Text = "Data Items";
            // 
            // DI_PrecisionUpDown
            // 
            this.DI_PrecisionUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_PrecisionUpDown.Location = new System.Drawing.Point(422, 382);
            this.DI_PrecisionUpDown.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.DI_PrecisionUpDown.Name = "DI_PrecisionUpDown";
            this.DI_PrecisionUpDown.Size = new System.Drawing.Size(120, 20);
            this.DI_PrecisionUpDown.TabIndex = 10;
            this.DI_PrecisionUpDown.ThousandsSeparator = true;
            // 
            // DI_SizeNumericUpDown
            // 
            this.DI_SizeNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_SizeNumericUpDown.Location = new System.Drawing.Point(422, 355);
            this.DI_SizeNumericUpDown.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.DI_SizeNumericUpDown.Name = "DI_SizeNumericUpDown";
            this.DI_SizeNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.DI_SizeNumericUpDown.TabIndex = 9;
            this.DI_SizeNumericUpDown.ThousandsSeparator = true;
            // 
            // DI_FormatTextBox
            // 
            this.DI_FormatTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_FormatTextBox.Location = new System.Drawing.Point(422, 407);
            this.DI_FormatTextBox.Name = "DI_FormatTextBox";
            this.DI_FormatTextBox.Size = new System.Drawing.Size(184, 20);
            this.DI_FormatTextBox.TabIndex = 11;
            // 
            // DI_FormatLabel
            // 
            this.DI_FormatLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_FormatLabel.Location = new System.Drawing.Point(316, 410);
            this.DI_FormatLabel.Name = "DI_FormatLabel";
            this.DI_FormatLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_FormatLabel.TabIndex = 58;
            this.DI_FormatLabel.Text = "Format";
            // 
            // DI_LabelTextBox
            // 
            this.DI_LabelTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_LabelTextBox.Location = new System.Drawing.Point(422, 302);
            this.DI_LabelTextBox.Name = "DI_LabelTextBox";
            this.DI_LabelTextBox.Size = new System.Drawing.Size(184, 20);
            this.DI_LabelTextBox.TabIndex = 6;
            // 
            // DI_LabelLabel
            // 
            this.DI_LabelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_LabelLabel.Location = new System.Drawing.Point(316, 305);
            this.DI_LabelLabel.Name = "DI_LabelLabel";
            this.DI_LabelLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_LabelLabel.TabIndex = 56;
            this.DI_LabelLabel.Text = "Label";
            // 
            // DI_InitialValueTextBox
            // 
            this.DI_InitialValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_InitialValueTextBox.Location = new System.Drawing.Point(422, 434);
            this.DI_InitialValueTextBox.Name = "DI_InitialValueTextBox";
            this.DI_InitialValueTextBox.Size = new System.Drawing.Size(184, 20);
            this.DI_InitialValueTextBox.TabIndex = 12;
            // 
            // DI_InitialValueLabel
            // 
            this.DI_InitialValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_InitialValueLabel.Location = new System.Drawing.Point(316, 437);
            this.DI_InitialValueLabel.Name = "DI_InitialValueLabel";
            this.DI_InitialValueLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_InitialValueLabel.TabIndex = 54;
            this.DI_InitialValueLabel.Text = "Initial Value";
            // 
            // DI_PrecisionLabel
            // 
            this.DI_PrecisionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_PrecisionLabel.Location = new System.Drawing.Point(316, 384);
            this.DI_PrecisionLabel.Name = "DI_PrecisionLabel";
            this.DI_PrecisionLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_PrecisionLabel.TabIndex = 52;
            this.DI_PrecisionLabel.Text = "Precision";
            // 
            // DI_SizeLabel
            // 
            this.DI_SizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_SizeLabel.Location = new System.Drawing.Point(316, 357);
            this.DI_SizeLabel.Name = "DI_SizeLabel";
            this.DI_SizeLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_SizeLabel.TabIndex = 50;
            this.DI_SizeLabel.Text = "Size";
            // 
            // DI_DatatypeSelectButton
            // 
            this.DI_DatatypeSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_DatatypeSelectButton.Location = new System.Drawing.Point(580, 326);
            this.DI_DatatypeSelectButton.Name = "DI_DatatypeSelectButton";
            this.DI_DatatypeSelectButton.Size = new System.Drawing.Size(26, 23);
            this.DI_DatatypeSelectButton.TabIndex = 8;
            this.DI_DatatypeSelectButton.Text = "...";
            this.DI_DatatypeSelectButton.UseVisualStyleBackColor = true;
            this.DI_DatatypeSelectButton.Click += new System.EventHandler(this.DI_DatatypeSelectButton_Click);
            // 
            // DI_DatatypeTextBox
            // 
            this.DI_DatatypeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_DatatypeTextBox.Location = new System.Drawing.Point(422, 328);
            this.DI_DatatypeTextBox.Name = "DI_DatatypeTextBox";
            this.DI_DatatypeTextBox.ReadOnly = true;
            this.DI_DatatypeTextBox.Size = new System.Drawing.Size(152, 20);
            this.DI_DatatypeTextBox.TabIndex = 7;
            // 
            // DI_DatatypeLabel
            // 
            this.DI_DatatypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_DatatypeLabel.Location = new System.Drawing.Point(316, 331);
            this.DI_DatatypeLabel.Name = "DI_DatatypeLabel";
            this.DI_DatatypeLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_DatatypeLabel.TabIndex = 47;
            this.DI_DatatypeLabel.Text = "Datatype";
            // 
            // DI_DomainComboBox
            // 
            this.DI_DomainComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_DomainComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DI_DomainComboBox.FormattingEnabled = true;
            this.DI_DomainComboBox.Location = new System.Drawing.Point(112, 456);
            this.DI_DomainComboBox.Name = "DI_DomainComboBox";
            this.DI_DomainComboBox.Size = new System.Drawing.Size(184, 21);
            this.DI_DomainComboBox.TabIndex = 5;
            // 
            // DI_DomainLabel
            // 
            this.DI_DomainLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_DomainLabel.Location = new System.Drawing.Point(6, 459);
            this.DI_DomainLabel.Name = "DI_DomainLabel";
            this.DI_DomainLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_DomainLabel.TabIndex = 45;
            this.DI_DomainLabel.Text = "Domain";
            // 
            // DI_BusinessItemSelectButton
            // 
            this.DI_BusinessItemSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_BusinessItemSelectButton.Location = new System.Drawing.Point(270, 428);
            this.DI_BusinessItemSelectButton.Name = "DI_BusinessItemSelectButton";
            this.DI_BusinessItemSelectButton.Size = new System.Drawing.Size(26, 23);
            this.DI_BusinessItemSelectButton.TabIndex = 4;
            this.DI_BusinessItemSelectButton.Text = "...";
            this.DI_BusinessItemSelectButton.UseVisualStyleBackColor = true;
            this.DI_BusinessItemSelectButton.Click += new System.EventHandler(this.DI_BusinessItemSelectButton_Click);
            // 
            // DI_BusinessItemTextBox
            // 
            this.DI_BusinessItemTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_BusinessItemTextBox.Location = new System.Drawing.Point(112, 430);
            this.DI_BusinessItemTextBox.Name = "DI_BusinessItemTextBox";
            this.DI_BusinessItemTextBox.ReadOnly = true;
            this.DI_BusinessItemTextBox.Size = new System.Drawing.Size(152, 20);
            this.DI_BusinessItemTextBox.TabIndex = 3;
            // 
            // DI_ModifiedUserTextBox
            // 
            this.DI_ModifiedUserTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_ModifiedUserTextBox.Location = new System.Drawing.Point(728, 460);
            this.DI_ModifiedUserTextBox.Name = "DI_ModifiedUserTextBox";
            this.DI_ModifiedUserTextBox.ReadOnly = true;
            this.DI_ModifiedUserTextBox.Size = new System.Drawing.Size(184, 20);
            this.DI_ModifiedUserTextBox.TabIndex = 19;
            // 
            // DI_ModifieUserLabel
            // 
            this.DI_ModifieUserLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_ModifieUserLabel.Location = new System.Drawing.Point(622, 463);
            this.DI_ModifieUserLabel.Name = "DI_ModifieUserLabel";
            this.DI_ModifieUserLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_ModifieUserLabel.TabIndex = 41;
            this.DI_ModifieUserLabel.Text = "Modified by";
            // 
            // DI_ModifiedDateTextBox
            // 
            this.DI_ModifiedDateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_ModifiedDateTextBox.Location = new System.Drawing.Point(728, 434);
            this.DI_ModifiedDateTextBox.Name = "DI_ModifiedDateTextBox";
            this.DI_ModifiedDateTextBox.ReadOnly = true;
            this.DI_ModifiedDateTextBox.Size = new System.Drawing.Size(184, 20);
            this.DI_ModifiedDateTextBox.TabIndex = 18;
            // 
            // DI_ModifiedDateLabel
            // 
            this.DI_ModifiedDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_ModifiedDateLabel.Location = new System.Drawing.Point(622, 437);
            this.DI_ModifiedDateLabel.Name = "DI_ModifiedDateLabel";
            this.DI_ModifiedDateLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_ModifiedDateLabel.TabIndex = 39;
            this.DI_ModifiedDateLabel.Text = "Modified date";
            // 
            // DI_CreatedUserTextBox
            // 
            this.DI_CreatedUserTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_CreatedUserTextBox.Location = new System.Drawing.Point(728, 407);
            this.DI_CreatedUserTextBox.Name = "DI_CreatedUserTextBox";
            this.DI_CreatedUserTextBox.ReadOnly = true;
            this.DI_CreatedUserTextBox.Size = new System.Drawing.Size(184, 20);
            this.DI_CreatedUserTextBox.TabIndex = 17;
            // 
            // DI_CreatedUserLabel
            // 
            this.DI_CreatedUserLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_CreatedUserLabel.Location = new System.Drawing.Point(622, 410);
            this.DI_CreatedUserLabel.Name = "DI_CreatedUserLabel";
            this.DI_CreatedUserLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_CreatedUserLabel.TabIndex = 37;
            this.DI_CreatedUserLabel.Text = "Created by";
            // 
            // DI_CreationDateTextBox
            // 
            this.DI_CreationDateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_CreationDateTextBox.Location = new System.Drawing.Point(728, 381);
            this.DI_CreationDateTextBox.Name = "DI_CreationDateTextBox";
            this.DI_CreationDateTextBox.ReadOnly = true;
            this.DI_CreationDateTextBox.Size = new System.Drawing.Size(184, 20);
            this.DI_CreationDateTextBox.TabIndex = 16;
            // 
            // DI_CreationDateLabel
            // 
            this.DI_CreationDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_CreationDateLabel.Location = new System.Drawing.Point(622, 384);
            this.DI_CreationDateLabel.Name = "DI_CreationDateLabel";
            this.DI_CreationDateLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_CreationDateLabel.TabIndex = 35;
            this.DI_CreationDateLabel.Text = "Creation date";
            // 
            // DI_KeywordsTextBox
            // 
            this.DI_KeywordsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_KeywordsTextBox.Location = new System.Drawing.Point(728, 355);
            this.DI_KeywordsTextBox.Name = "DI_KeywordsTextBox";
            this.DI_KeywordsTextBox.Size = new System.Drawing.Size(184, 20);
            this.DI_KeywordsTextBox.TabIndex = 15;
            // 
            // DI_KeywordsLabel
            // 
            this.DI_KeywordsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_KeywordsLabel.Location = new System.Drawing.Point(622, 358);
            this.DI_KeywordsLabel.Name = "DI_KeywordsLabel";
            this.DI_KeywordsLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_KeywordsLabel.TabIndex = 33;
            this.DI_KeywordsLabel.Text = "Keywords";
            // 
            // DI_StatusComboBox
            // 
            this.DI_StatusComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_StatusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DI_StatusComboBox.FormattingEnabled = true;
            this.DI_StatusComboBox.Location = new System.Drawing.Point(728, 328);
            this.DI_StatusComboBox.Name = "DI_StatusComboBox";
            this.DI_StatusComboBox.Size = new System.Drawing.Size(184, 21);
            this.DI_StatusComboBox.TabIndex = 14;
            // 
            // DI_StatusLabel
            // 
            this.DI_StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_StatusLabel.Location = new System.Drawing.Point(622, 331);
            this.DI_StatusLabel.Name = "DI_StatusLabel";
            this.DI_StatusLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_StatusLabel.TabIndex = 31;
            this.DI_StatusLabel.Text = "Status";
            // 
            // DI_VersionTextBox
            // 
            this.DI_VersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_VersionTextBox.Location = new System.Drawing.Point(728, 302);
            this.DI_VersionTextBox.Name = "DI_VersionTextBox";
            this.DI_VersionTextBox.Size = new System.Drawing.Size(184, 20);
            this.DI_VersionTextBox.TabIndex = 13;
            // 
            // DI_VersionLabel
            // 
            this.DI_VersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_VersionLabel.Location = new System.Drawing.Point(622, 305);
            this.DI_VersionLabel.Name = "DI_VersionLabel";
            this.DI_VersionLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_VersionLabel.TabIndex = 29;
            this.DI_VersionLabel.Text = "Version";
            // 
            // DI_DescriptionTextBox
            // 
            this.DI_DescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_DescriptionTextBox.Location = new System.Drawing.Point(112, 328);
            this.DI_DescriptionTextBox.Multiline = true;
            this.DI_DescriptionTextBox.Name = "DI_DescriptionTextBox";
            this.DI_DescriptionTextBox.Size = new System.Drawing.Size(184, 96);
            this.DI_DescriptionTextBox.TabIndex = 2;
            // 
            // DI_DescriptionLabel
            // 
            this.DI_DescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_DescriptionLabel.Location = new System.Drawing.Point(6, 331);
            this.DI_DescriptionLabel.Name = "DI_DescriptionLabel";
            this.DI_DescriptionLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_DescriptionLabel.TabIndex = 26;
            this.DI_DescriptionLabel.Text = "Description";
            // 
            // DI_BusinessItemLabel
            // 
            this.DI_BusinessItemLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_BusinessItemLabel.Location = new System.Drawing.Point(6, 433);
            this.DI_BusinessItemLabel.Name = "DI_BusinessItemLabel";
            this.DI_BusinessItemLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_BusinessItemLabel.TabIndex = 25;
            this.DI_BusinessItemLabel.Text = "BusinessItem";
            // 
            // DI_NameTextBox
            // 
            this.DI_NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_NameTextBox.Location = new System.Drawing.Point(112, 302);
            this.DI_NameTextBox.Name = "DI_NameTextBox";
            this.DI_NameTextBox.Size = new System.Drawing.Size(184, 20);
            this.DI_NameTextBox.TabIndex = 1;
            // 
            // DI_NameLabel
            // 
            this.DI_NameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DI_NameLabel.Location = new System.Drawing.Point(6, 305);
            this.DI_NameLabel.Name = "DI_NameLabel";
            this.DI_NameLabel.Size = new System.Drawing.Size(100, 23);
            this.DI_NameLabel.TabIndex = 23;
            this.DI_NameLabel.Text = "Name";
            // 
            // dataItemsListView
            // 
            this.dataItemsListView.AllColumns.Add(this.DI_NameColumn);
            this.dataItemsListView.AllColumns.Add(this.DI_LabelColumn);
            this.dataItemsListView.AllColumns.Add(this.DI_DatatypeColumn);
            this.dataItemsListView.AllColumns.Add(this.DI_DomainColumn);
            this.dataItemsListView.AllColumns.Add(this.DI_BusinessItem);
            this.dataItemsListView.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.dataItemsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataItemsListView.CellEditUseWholeCell = false;
            this.dataItemsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DI_NameColumn,
            this.DI_LabelColumn,
            this.DI_DatatypeColumn,
            this.DI_DomainColumn,
            this.DI_BusinessItem});
            this.dataItemsListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataItemsListView.FullRowSelect = true;
            this.dataItemsListView.GridLines = true;
            this.dataItemsListView.HideSelection = false;
            this.dataItemsListView.Location = new System.Drawing.Point(3, 3);
            this.dataItemsListView.MultiSelect = false;
            this.dataItemsListView.Name = "dataItemsListView";
            this.dataItemsListView.ShowCommandMenuOnRightClick = true;
            this.dataItemsListView.ShowGroups = false;
            this.dataItemsListView.Size = new System.Drawing.Size(914, 290);
            this.dataItemsListView.TabIndex = 0;
            this.dataItemsListView.TintSortColumn = true;
            this.dataItemsListView.UseCompatibleStateImageBehavior = false;
            this.dataItemsListView.UseFilterIndicator = true;
            this.dataItemsListView.UseFiltering = true;
            this.dataItemsListView.UseHotItem = true;
            this.dataItemsListView.View = System.Windows.Forms.View.Details;
            this.dataItemsListView.SelectedIndexChanged += new System.EventHandler(this.dataItemsListView_SelectedIndexChanged);
            this.dataItemsListView.DoubleClick += new System.EventHandler(this.dataItemsListView_DoubleClick);
            // 
            // DI_NameColumn
            // 
            this.DI_NameColumn.AspectName = "Name";
            this.DI_NameColumn.Text = "Name";
            this.DI_NameColumn.ToolTipText = "Name of the Data Item";
            this.DI_NameColumn.Width = 200;
            // 
            // DI_LabelColumn
            // 
            this.DI_LabelColumn.AspectName = "Label";
            this.DI_LabelColumn.Text = "Label";
            this.DI_LabelColumn.ToolTipText = "Label of the Data Item";
            this.DI_LabelColumn.Width = 150;
            // 
            // DI_DatatypeColumn
            // 
            this.DI_DatatypeColumn.AspectName = "datatypeDisplayName";
            this.DI_DatatypeColumn.DisplayIndex = 3;
            this.DI_DatatypeColumn.Text = "Datatype";
            this.DI_DatatypeColumn.ToolTipText = "Logical Datatype";
            this.DI_DatatypeColumn.Width = 150;
            // 
            // DI_DomainColumn
            // 
            this.DI_DomainColumn.AspectName = "domainPath";
            this.DI_DomainColumn.DisplayIndex = 2;
            this.DI_DomainColumn.Text = "Domain";
            this.DI_DomainColumn.ToolTipText = "Domain of the Data Item";
            this.DI_DomainColumn.Width = 150;
            // 
            // DI_BusinessItem
            // 
            this.DI_BusinessItem.AspectName = "businessItem.Name";
            this.DI_BusinessItem.Text = "Business Item";
            this.DI_BusinessItem.ToolTipText = "Business Item related to this Data Item";
            this.DI_BusinessItem.Width = 150;
            // 
            // ColumnsTabPage
            // 
            this.ColumnsTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.ColumnsTabPage.Controls.Add(this.numericUpDown1);
            this.ColumnsTabPage.Controls.Add(this.numericUpDown2);
            this.ColumnsTabPage.Controls.Add(this.textBox1);
            this.ColumnsTabPage.Controls.Add(this.label1);
            this.ColumnsTabPage.Controls.Add(this.textBox2);
            this.ColumnsTabPage.Controls.Add(this.label2);
            this.ColumnsTabPage.Controls.Add(this.textBox3);
            this.ColumnsTabPage.Controls.Add(this.label3);
            this.ColumnsTabPage.Controls.Add(this.label4);
            this.ColumnsTabPage.Controls.Add(this.label5);
            this.ColumnsTabPage.Controls.Add(this.button1);
            this.ColumnsTabPage.Controls.Add(this.textBox4);
            this.ColumnsTabPage.Controls.Add(this.label6);
            this.ColumnsTabPage.Controls.Add(this.comboBox1);
            this.ColumnsTabPage.Controls.Add(this.label7);
            this.ColumnsTabPage.Controls.Add(this.button2);
            this.ColumnsTabPage.Controls.Add(this.textBox5);
            this.ColumnsTabPage.Controls.Add(this.textBox6);
            this.ColumnsTabPage.Controls.Add(this.label8);
            this.ColumnsTabPage.Controls.Add(this.textBox7);
            this.ColumnsTabPage.Controls.Add(this.label9);
            this.ColumnsTabPage.Controls.Add(this.textBox8);
            this.ColumnsTabPage.Controls.Add(this.label10);
            this.ColumnsTabPage.Controls.Add(this.textBox9);
            this.ColumnsTabPage.Controls.Add(this.label11);
            this.ColumnsTabPage.Controls.Add(this.textBox10);
            this.ColumnsTabPage.Controls.Add(this.label12);
            this.ColumnsTabPage.Controls.Add(this.comboBox2);
            this.ColumnsTabPage.Controls.Add(this.label13);
            this.ColumnsTabPage.Controls.Add(this.textBox11);
            this.ColumnsTabPage.Controls.Add(this.label14);
            this.ColumnsTabPage.Controls.Add(this.textBox12);
            this.ColumnsTabPage.Controls.Add(this.label15);
            this.ColumnsTabPage.Controls.Add(this.label16);
            this.ColumnsTabPage.Controls.Add(this.textBox13);
            this.ColumnsTabPage.Controls.Add(this.label17);
            this.ColumnsTabPage.Controls.Add(this.columnsListView);
            this.ColumnsTabPage.Location = new System.Drawing.Point(4, 22);
            this.ColumnsTabPage.Name = "ColumnsTabPage";
            this.ColumnsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ColumnsTabPage.Size = new System.Drawing.Size(923, 551);
            this.ColumnsTabPage.TabIndex = 2;
            this.ColumnsTabPage.Text = "Columns";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.Location = new System.Drawing.Point(418, 379);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 68;
            this.numericUpDown1.ThousandsSeparator = true;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown2.Location = new System.Drawing.Point(418, 352);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 67;
            this.numericUpDown2.ThousandsSeparator = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(418, 404);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(184, 20);
            this.textBox1.TabIndex = 69;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(312, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 94;
            this.label1.Text = "Format";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.Location = new System.Drawing.Point(418, 299);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(184, 20);
            this.textBox2.TabIndex = 64;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(312, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 93;
            this.label2.Text = "Label";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox3.Location = new System.Drawing.Point(418, 431);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(184, 20);
            this.textBox3.TabIndex = 70;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(312, 434);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 92;
            this.label3.Text = "Initial Value";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Location = new System.Drawing.Point(312, 381);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 91;
            this.label4.Text = "Precision";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(312, 354);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 90;
            this.label5.Text = "Size";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(576, 323);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 23);
            this.button1.TabIndex = 66;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox4.Location = new System.Drawing.Point(418, 325);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(152, 20);
            this.textBox4.TabIndex = 65;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Location = new System.Drawing.Point(312, 328);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 89;
            this.label6.Text = "Datatype";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(108, 453);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(184, 21);
            this.comboBox1.TabIndex = 63;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.Location = new System.Drawing.Point(2, 456);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 88;
            this.label7.Text = "Domain";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(266, 425);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 23);
            this.button2.TabIndex = 62;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox5.Location = new System.Drawing.Point(108, 427);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(152, 20);
            this.textBox5.TabIndex = 61;
            // 
            // textBox6
            // 
            this.textBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox6.Location = new System.Drawing.Point(724, 457);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(184, 20);
            this.textBox6.TabIndex = 77;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.Location = new System.Drawing.Point(618, 460);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 87;
            this.label8.Text = "Modified by";
            // 
            // textBox7
            // 
            this.textBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox7.Location = new System.Drawing.Point(724, 431);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(184, 20);
            this.textBox7.TabIndex = 76;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.Location = new System.Drawing.Point(618, 434);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 23);
            this.label9.TabIndex = 86;
            this.label9.Text = "Modified date";
            // 
            // textBox8
            // 
            this.textBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox8.Location = new System.Drawing.Point(724, 404);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(184, 20);
            this.textBox8.TabIndex = 75;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.Location = new System.Drawing.Point(618, 407);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 85;
            this.label10.Text = "Created by";
            // 
            // textBox9
            // 
            this.textBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox9.Location = new System.Drawing.Point(724, 378);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(184, 20);
            this.textBox9.TabIndex = 74;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.Location = new System.Drawing.Point(618, 381);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 23);
            this.label11.TabIndex = 84;
            this.label11.Text = "Creation date";
            // 
            // textBox10
            // 
            this.textBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox10.Location = new System.Drawing.Point(724, 352);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(184, 20);
            this.textBox10.TabIndex = 73;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.Location = new System.Drawing.Point(618, 355);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 23);
            this.label12.TabIndex = 83;
            this.label12.Text = "Keywords";
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(724, 325);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(184, 21);
            this.comboBox2.TabIndex = 72;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.Location = new System.Drawing.Point(618, 328);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 23);
            this.label13.TabIndex = 82;
            this.label13.Text = "Status";
            // 
            // textBox11
            // 
            this.textBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox11.Location = new System.Drawing.Point(724, 299);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(184, 20);
            this.textBox11.TabIndex = 71;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.Location = new System.Drawing.Point(618, 302);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 23);
            this.label14.TabIndex = 81;
            this.label14.Text = "Version";
            // 
            // textBox12
            // 
            this.textBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox12.Location = new System.Drawing.Point(108, 325);
            this.textBox12.Multiline = true;
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(184, 96);
            this.textBox12.TabIndex = 60;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.Location = new System.Drawing.Point(2, 328);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 23);
            this.label15.TabIndex = 80;
            this.label15.Text = "Description";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.Location = new System.Drawing.Point(2, 430);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 23);
            this.label16.TabIndex = 79;
            this.label16.Text = "BusinessItem";
            // 
            // textBox13
            // 
            this.textBox13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox13.Location = new System.Drawing.Point(108, 299);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(184, 20);
            this.textBox13.TabIndex = 59;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.Location = new System.Drawing.Point(2, 302);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(100, 23);
            this.label17.TabIndex = 78;
            this.label17.Text = "Name";
            // 
            // columnsListView
            // 
            this.columnsListView.AllColumns.Add(this.C_TableColumn);
            this.columnsListView.AllColumns.Add(this.C_ColumnColumn);
            this.columnsListView.AllColumns.Add(this.C_PropertiesColumn);
            this.columnsListView.AllColumns.Add(this.C_DataItem);
            this.columnsListView.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.columnsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.columnsListView.CellEditUseWholeCell = false;
            this.columnsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.C_TableColumn,
            this.C_ColumnColumn,
            this.C_PropertiesColumn,
            this.C_DataItem});
            this.columnsListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.columnsListView.FullRowSelect = true;
            this.columnsListView.GridLines = true;
            this.columnsListView.HideSelection = false;
            this.columnsListView.Location = new System.Drawing.Point(3, 3);
            this.columnsListView.MultiSelect = false;
            this.columnsListView.Name = "columnsListView";
            this.columnsListView.ShowCommandMenuOnRightClick = true;
            this.columnsListView.ShowGroups = false;
            this.columnsListView.ShowItemCountOnGroups = true;
            this.columnsListView.Size = new System.Drawing.Size(914, 290);
            this.columnsListView.TabIndex = 1;
            this.columnsListView.TintSortColumn = true;
            this.columnsListView.UseCompatibleStateImageBehavior = false;
            this.columnsListView.UseFilterIndicator = true;
            this.columnsListView.UseFiltering = true;
            this.columnsListView.UseHotItem = true;
            this.columnsListView.View = System.Windows.Forms.View.Details;
            // 
            // C_TableColumn
            // 
            this.C_TableColumn.AspectName = "tableName";
            this.C_TableColumn.GroupWithItemCountFormat = "{0} ({1} columns)";
            this.C_TableColumn.GroupWithItemCountSingularFormat = "{0} (1 column)";
            this.C_TableColumn.Text = "Table";
            this.C_TableColumn.ToolTipText = "Name of the database and table";
            this.C_TableColumn.Width = 150;
            // 
            // C_PropertiesColumn
            // 
            this.C_PropertiesColumn.AspectName = "properties";
            this.C_PropertiesColumn.Text = "Datatype";
            this.C_PropertiesColumn.ToolTipText = "Column Properties";
            this.C_PropertiesColumn.Width = 150;
            // 
            // C_ColumnColumn
            // 
            this.C_ColumnColumn.AspectName = "name";
            this.C_ColumnColumn.Text = "Column";
            this.C_ColumnColumn.ToolTipText = "Label of the Data Item";
            this.C_ColumnColumn.Width = 150;
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
            this.deleteButton.TabIndex = 3;
            this.myToolTip.SetToolTip(this.deleteButton, "Delete Element");
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // newButton
            // 
            this.newButton.Image = ((System.Drawing.Image)(resources.GetObject("newButton.Image")));
            this.newButton.Location = new System.Drawing.Point(93, 7);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(29, 28);
            this.newButton.TabIndex = 2;
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
            this.openPropertiesButton.TabIndex = 1;
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
            this.navigateProjectBrowserButton.TabIndex = 0;
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
            this.cancelButton.TabIndex = 5;
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
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // C_DataItem
            // 
            this.C_DataItem.AspectName = "dataItem.Name";
            this.C_DataItem.Text = "DataItem";
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
            this.DataItemsTabPage.ResumeLayout(false);
            this.DataItemsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DI_PrecisionUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DI_SizeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataItemsListView)).EndInit();
            this.ColumnsTabPage.ResumeLayout(false);
            this.ColumnsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnsListView)).EndInit();
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
        private System.Windows.Forms.Button DI_BusinessItemSelectButton;
        private System.Windows.Forms.TextBox DI_BusinessItemTextBox;
        private System.Windows.Forms.TextBox DI_ModifiedUserTextBox;
        private System.Windows.Forms.Label DI_ModifieUserLabel;
        private System.Windows.Forms.TextBox DI_ModifiedDateTextBox;
        private System.Windows.Forms.Label DI_ModifiedDateLabel;
        private System.Windows.Forms.TextBox DI_CreatedUserTextBox;
        private System.Windows.Forms.Label DI_CreatedUserLabel;
        private System.Windows.Forms.TextBox DI_CreationDateTextBox;
        private System.Windows.Forms.Label DI_CreationDateLabel;
        private System.Windows.Forms.TextBox DI_KeywordsTextBox;
        private System.Windows.Forms.Label DI_KeywordsLabel;
        private System.Windows.Forms.ComboBox DI_StatusComboBox;
        private System.Windows.Forms.Label DI_StatusLabel;
        private System.Windows.Forms.TextBox DI_VersionTextBox;
        private System.Windows.Forms.Label DI_VersionLabel;
        private System.Windows.Forms.TextBox DI_DescriptionTextBox;
        private System.Windows.Forms.Label DI_DescriptionLabel;
        private System.Windows.Forms.Label DI_BusinessItemLabel;
        private System.Windows.Forms.TextBox DI_NameTextBox;
        private System.Windows.Forms.Label DI_NameLabel;
        private ObjectListView dataItemsListView;
        private OLVColumn DI_NameColumn;
        private OLVColumn DI_DomainColumn;
        private System.Windows.Forms.ComboBox DI_DomainComboBox;
        private System.Windows.Forms.Label DI_DomainLabel;
        private System.Windows.Forms.Button DI_DatatypeSelectButton;
        private System.Windows.Forms.TextBox DI_DatatypeTextBox;
        private System.Windows.Forms.Label DI_DatatypeLabel;
        private System.Windows.Forms.TextBox DI_LabelTextBox;
        private System.Windows.Forms.Label DI_LabelLabel;
        private System.Windows.Forms.TextBox DI_InitialValueTextBox;
        private System.Windows.Forms.Label DI_InitialValueLabel;
        private System.Windows.Forms.Label DI_PrecisionLabel;
        private System.Windows.Forms.Label DI_SizeLabel;
        private System.Windows.Forms.TextBox DI_FormatTextBox;
        private System.Windows.Forms.Label DI_FormatLabel;
        private System.Windows.Forms.NumericUpDown DI_SizeNumericUpDown;
        private System.Windows.Forms.NumericUpDown DI_PrecisionUpDown;
        private OLVColumn DI_LabelColumn;
        private OLVColumn DI_DatatypeColumn;
        private OLVColumn DI_BusinessItem;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label17;
        private ObjectListView columnsListView;
        private OLVColumn C_TableColumn;
        private OLVColumn C_PropertiesColumn;
        private OLVColumn C_ColumnColumn;
        private OLVColumn C_DataItem;
    }
}
