/*
 * Created by SharpDevelop.
 * User: Geert
 * Date: 27/01/2016
 * Time: 5:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ECDMMessageComposer
{
	partial class SettingsWindow
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DataGridView ignoredStereoTypesGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn StereotypeColumn;
		private System.Windows.Forms.Button deleteStereotypeButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.DataGridView ignoredTaggedValuesGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.Button deleteTaggedValueButton;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox diagramOptionsGroupBox;
		private System.Windows.Forms.CheckBox addSourceElementCheckBox;
		private System.Windows.Forms.CheckBox addDataTypesCheckBox;
        private System.Windows.Forms.CheckBox copyDatatypesCheckbox;
        private System.Windows.Forms.GroupBox dataTypeOptionsGroupBox;
        private System.Windows.Forms.Button deleteDataTypeButton;
        private System.Windows.Forms.CheckBox limitDatatypesCheckBox;
        private System.Windows.Forms.DataGridView dataTypesGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.CheckBox copyDataTypeGeneralizationsCheckBox;
        private System.Windows.Forms.GroupBox traceabilityGroupBox;
        private System.Windows.Forms.Label associationTagLabel;
        private System.Windows.Forms.TextBox associationTagTextBox;
        private System.Windows.Forms.Label attributeTagLabel;
        private System.Windows.Forms.TextBox attributeTagTextBox;
        private System.Windows.Forms.GroupBox GeneralGroupBox;
        private System.Windows.Forms.CheckBox RedirectGeneralizationsCheckBox;
        private System.Windows.Forms.TextBox notesPrefixTextBox;
        private System.Windows.Forms.CheckBox prefixNotesCheckBox;
        private System.Windows.Forms.CheckBox checkSecurityCheckBox;
        private System.Windows.Forms.CheckBox generalCopyGeneralizationsCheckbox;
        private System.Windows.Forms.CheckBox deleteUnusedElementsCheckBox;
        private System.Windows.Forms.CheckBox usePackageSubsetsOnlyCheckBox;
        private System.Windows.Forms.Button deleteHiddenElementButton;
        private System.Windows.Forms.DataGridView hiddenElementGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.GroupBox xmlSchemaGroup;
        private System.Windows.Forms.CheckBox orderAssociationsCheckbox;
        private System.Windows.Forms.CheckBox noAttributeDependenciesCheckbox;
        private System.Windows.Forms.CheckBox orderAssociationsAmongstAttributesCheckbox;
        private System.Windows.Forms.Label elementTagLabel;
        private System.Windows.Forms.TextBox elementTagTextBox;
        private System.Windows.Forms.CheckBox tvInsteadOfTraceCheckBox;
		/// <summary>
		/// Disposes resources used by the form.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.ignoredStereoTypesGrid = new System.Windows.Forms.DataGridView();
            this.StereotypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteStereotypeButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.ignoredTaggedValuesGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteTaggedValueButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.diagramOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.generateDiagramCheckbox = new System.Windows.Forms.CheckBox();
            this.deleteHiddenElementButton = new System.Windows.Forms.Button();
            this.hiddenElementGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addSourceElementCheckBox = new System.Windows.Forms.CheckBox();
            this.addDataTypesCheckBox = new System.Windows.Forms.CheckBox();
            this.copyDatatypesCheckbox = new System.Windows.Forms.CheckBox();
            this.dataTypeOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.copyDataTypeGeneralizationsCheckBox = new System.Windows.Forms.CheckBox();
            this.limitDatatypesCheckBox = new System.Windows.Forms.CheckBox();
            this.deleteDataTypeButton = new System.Windows.Forms.Button();
            this.dataTypesGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.traceabilityGroupBox = new System.Windows.Forms.GroupBox();
            this.associationTagLabel = new System.Windows.Forms.Label();
            this.associationTagTextBox = new System.Windows.Forms.TextBox();
            this.attributeTagLabel = new System.Windows.Forms.Label();
            this.attributeTagTextBox = new System.Windows.Forms.TextBox();
            this.GeneralGroupBox = new System.Windows.Forms.GroupBox();
            this.generateToArtifactPackageCheckBox = new System.Windows.Forms.CheckBox();
            this.usePackageSubsetsOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.deleteUnusedElementsCheckBox = new System.Windows.Forms.CheckBox();
            this.generalCopyGeneralizationsCheckbox = new System.Windows.Forms.CheckBox();
            this.checkSecurityCheckBox = new System.Windows.Forms.CheckBox();
            this.notesPrefixTextBox = new System.Windows.Forms.TextBox();
            this.prefixNotesCheckBox = new System.Windows.Forms.CheckBox();
            this.RedirectGeneralizationsCheckBox = new System.Windows.Forms.CheckBox();
            this.keepAttributeOrderRadio = new System.Windows.Forms.RadioButton();
            this.xmlSchemaGroup = new System.Windows.Forms.GroupBox();
            this.choiceBeforeAttributesCheckbox = new System.Windows.Forms.CheckBox();
            this.elementTagLabel = new System.Windows.Forms.Label();
            this.elementTagTextBox = new System.Windows.Forms.TextBox();
            this.tvInsteadOfTraceCheckBox = new System.Windows.Forms.CheckBox();
            this.orderAssociationsAmongstAttributesCheckbox = new System.Windows.Forms.CheckBox();
            this.orderAssociationsCheckbox = new System.Windows.Forms.CheckBox();
            this.noAttributeDependenciesCheckbox = new System.Windows.Forms.CheckBox();
            this.attributeOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.setAttributesOrderZeroRadio = new System.Windows.Forms.RadioButton();
            this.addNewAttributesLastRadio = new System.Windows.Forms.RadioButton();
            this.notesOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.keepNotesInSyncCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ignoredStereoTypesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ignoredTaggedValuesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.diagramOptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hiddenElementGrid)).BeginInit();
            this.dataTypeOptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTypesGridView)).BeginInit();
            this.traceabilityGroupBox.SuspendLayout();
            this.GeneralGroupBox.SuspendLayout();
            this.xmlSchemaGroup.SuspendLayout();
            this.attributeOptionsGroupBox.SuspendLayout();
            this.notesOptionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ignoredStereoTypesGrid
            // 
            this.ignoredStereoTypesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ignoredStereoTypesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StereotypeColumn});
            this.ignoredStereoTypesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ignoredStereoTypesGrid.Location = new System.Drawing.Point(0, 0);
            this.ignoredStereoTypesGrid.Name = "ignoredStereoTypesGrid";
            this.ignoredStereoTypesGrid.RowHeadersVisible = false;
            this.ignoredStereoTypesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ignoredStereoTypesGrid.Size = new System.Drawing.Size(177, 151);
            this.ignoredStereoTypesGrid.TabIndex = 0;
            // 
            // StereotypeColumn
            // 
            this.StereotypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StereotypeColumn.HeaderText = "Ignored Stereotypes";
            this.StereotypeColumn.Name = "StereotypeColumn";
            this.StereotypeColumn.ToolTipText = "Classes with these stereotypes will not be deleted when updating a subset model";
            // 
            // deleteStereotypeButton
            // 
            this.deleteStereotypeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteStereotypeButton.Location = new System.Drawing.Point(478, 172);
            this.deleteStereotypeButton.Name = "deleteStereotypeButton";
            this.deleteStereotypeButton.Size = new System.Drawing.Size(75, 23);
            this.deleteStereotypeButton.TabIndex = 1;
            this.deleteStereotypeButton.Text = "Delete";
            this.deleteStereotypeButton.UseVisualStyleBackColor = true;
            this.deleteStereotypeButton.Click += new System.EventHandler(this.DeleteStereotypeButtonClick);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(496, 628);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(577, 628);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(658, 628);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 4;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.ApplyButtonClick);
            // 
            // ignoredTaggedValuesGrid
            // 
            this.ignoredTaggedValuesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ignoredTaggedValuesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.ignoredTaggedValuesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ignoredTaggedValuesGrid.Location = new System.Drawing.Point(0, 0);
            this.ignoredTaggedValuesGrid.Name = "ignoredTaggedValuesGrid";
            this.ignoredTaggedValuesGrid.RowHeadersVisible = false;
            this.ignoredTaggedValuesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ignoredTaggedValuesGrid.Size = new System.Drawing.Size(176, 151);
            this.ignoredTaggedValuesGrid.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Ignored Tagged Values";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ToolTipText = "These tagged values will be left untouched when they have a value in the subset m" +
    "odel";
            // 
            // deleteTaggedValueButton
            // 
            this.deleteTaggedValueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteTaggedValueButton.Location = new System.Drawing.Point(660, 172);
            this.deleteTaggedValueButton.Name = "deleteTaggedValueButton";
            this.deleteTaggedValueButton.Size = new System.Drawing.Size(75, 23);
            this.deleteTaggedValueButton.TabIndex = 6;
            this.deleteTaggedValueButton.Text = "Delete";
            this.deleteTaggedValueButton.UseVisualStyleBackColor = true;
            this.deleteTaggedValueButton.Click += new System.EventHandler(this.DeleteTaggedValueButtonClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(376, 15);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ignoredStereoTypesGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ignoredTaggedValuesGrid);
            this.splitContainer1.Size = new System.Drawing.Size(357, 151);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.TabIndex = 7;
            // 
            // diagramOptionsGroupBox
            // 
            this.diagramOptionsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.diagramOptionsGroupBox.Controls.Add(this.generateDiagramCheckbox);
            this.diagramOptionsGroupBox.Controls.Add(this.deleteHiddenElementButton);
            this.diagramOptionsGroupBox.Controls.Add(this.hiddenElementGrid);
            this.diagramOptionsGroupBox.Controls.Add(this.addSourceElementCheckBox);
            this.diagramOptionsGroupBox.Controls.Add(this.addDataTypesCheckBox);
            this.diagramOptionsGroupBox.Location = new System.Drawing.Point(12, 489);
            this.diagramOptionsGroupBox.MinimumSize = new System.Drawing.Size(0, 75);
            this.diagramOptionsGroupBox.Name = "diagramOptionsGroupBox";
            this.diagramOptionsGroupBox.Size = new System.Drawing.Size(351, 158);
            this.diagramOptionsGroupBox.TabIndex = 8;
            this.diagramOptionsGroupBox.TabStop = false;
            this.diagramOptionsGroupBox.Text = "Diagram Options";
            // 
            // generateDiagramCheckbox
            // 
            this.generateDiagramCheckbox.Location = new System.Drawing.Point(6, 19);
            this.generateDiagramCheckbox.Name = "generateDiagramCheckbox";
            this.generateDiagramCheckbox.Size = new System.Drawing.Size(143, 24);
            this.generateDiagramCheckbox.TabIndex = 10;
            this.generateDiagramCheckbox.Text = "Generate Diagram";
            this.generateDiagramCheckbox.UseVisualStyleBackColor = true;
            // 
            // deleteHiddenElementButton
            // 
            this.deleteHiddenElementButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteHiddenElementButton.Location = new System.Drawing.Point(270, 130);
            this.deleteHiddenElementButton.Name = "deleteHiddenElementButton";
            this.deleteHiddenElementButton.Size = new System.Drawing.Size(75, 23);
            this.deleteHiddenElementButton.TabIndex = 9;
            this.deleteHiddenElementButton.Text = "Delete";
            this.deleteHiddenElementButton.UseVisualStyleBackColor = true;
            // 
            // hiddenElementGrid
            // 
            this.hiddenElementGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hiddenElementGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.hiddenElementGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3});
            this.hiddenElementGrid.Location = new System.Drawing.Point(169, 19);
            this.hiddenElementGrid.Name = "hiddenElementGrid";
            this.hiddenElementGrid.RowHeadersVisible = false;
            this.hiddenElementGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.hiddenElementGrid.Size = new System.Drawing.Size(176, 105);
            this.hiddenElementGrid.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Hidden Element Types";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ToolTipText = "List of Element types and sterereotypes that should not be shown on diagrams";
            // 
            // addSourceElementCheckBox
            // 
            this.addSourceElementCheckBox.Location = new System.Drawing.Point(6, 79);
            this.addSourceElementCheckBox.Name = "addSourceElementCheckBox";
            this.addSourceElementCheckBox.Size = new System.Drawing.Size(143, 24);
            this.addSourceElementCheckBox.TabIndex = 1;
            this.addSourceElementCheckBox.Text = "Add Source Elements";
            this.addSourceElementCheckBox.UseVisualStyleBackColor = true;
            // 
            // addDataTypesCheckBox
            // 
            this.addDataTypesCheckBox.Location = new System.Drawing.Point(6, 49);
            this.addDataTypesCheckBox.Name = "addDataTypesCheckBox";
            this.addDataTypesCheckBox.Size = new System.Drawing.Size(104, 24);
            this.addDataTypesCheckBox.TabIndex = 0;
            this.addDataTypesCheckBox.Text = "Add Datatypes";
            this.addDataTypesCheckBox.UseVisualStyleBackColor = true;
            // 
            // copyDatatypesCheckbox
            // 
            this.copyDatatypesCheckbox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.copyDatatypesCheckbox.Location = new System.Drawing.Point(6, 19);
            this.copyDatatypesCheckbox.Name = "copyDatatypesCheckbox";
            this.copyDatatypesCheckbox.Size = new System.Drawing.Size(162, 24);
            this.copyDatatypesCheckbox.TabIndex = 2;
            this.copyDatatypesCheckbox.Text = "Copy Datatypes to subset";
            this.copyDatatypesCheckbox.UseVisualStyleBackColor = true;
            this.copyDatatypesCheckbox.CheckedChanged += new System.EventHandler(this.CopyDatatypesCheckboxCheckedChanged);
            // 
            // dataTypeOptionsGroupBox
            // 
            this.dataTypeOptionsGroupBox.Controls.Add(this.copyDataTypeGeneralizationsCheckBox);
            this.dataTypeOptionsGroupBox.Controls.Add(this.limitDatatypesCheckBox);
            this.dataTypeOptionsGroupBox.Controls.Add(this.deleteDataTypeButton);
            this.dataTypeOptionsGroupBox.Controls.Add(this.dataTypesGridView);
            this.dataTypeOptionsGroupBox.Controls.Add(this.copyDatatypesCheckbox);
            this.dataTypeOptionsGroupBox.Location = new System.Drawing.Point(12, 321);
            this.dataTypeOptionsGroupBox.MinimumSize = new System.Drawing.Size(0, 45);
            this.dataTypeOptionsGroupBox.Name = "dataTypeOptionsGroupBox";
            this.dataTypeOptionsGroupBox.Size = new System.Drawing.Size(351, 162);
            this.dataTypeOptionsGroupBox.TabIndex = 9;
            this.dataTypeOptionsGroupBox.TabStop = false;
            this.dataTypeOptionsGroupBox.Text = "Datatype Options";
            // 
            // copyDataTypeGeneralizationsCheckBox
            // 
            this.copyDataTypeGeneralizationsCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.copyDataTypeGeneralizationsCheckBox.Location = new System.Drawing.Point(6, 79);
            this.copyDataTypeGeneralizationsCheckBox.Name = "copyDataTypeGeneralizationsCheckBox";
            this.copyDataTypeGeneralizationsCheckBox.Size = new System.Drawing.Size(162, 24);
            this.copyDataTypeGeneralizationsCheckBox.TabIndex = 9;
            this.copyDataTypeGeneralizationsCheckBox.Text = "Copy Generalizations";
            this.copyDataTypeGeneralizationsCheckBox.UseVisualStyleBackColor = true;
            // 
            // limitDatatypesCheckBox
            // 
            this.limitDatatypesCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.limitDatatypesCheckBox.Location = new System.Drawing.Point(6, 49);
            this.limitDatatypesCheckBox.Name = "limitDatatypesCheckBox";
            this.limitDatatypesCheckBox.Size = new System.Drawing.Size(162, 24);
            this.limitDatatypesCheckBox.TabIndex = 8;
            this.limitDatatypesCheckBox.Text = "Limit Datatypes to copy";
            this.limitDatatypesCheckBox.UseVisualStyleBackColor = true;
            this.limitDatatypesCheckBox.CheckedChanged += new System.EventHandler(this.LimitDatatypesCheckBoxCheckedChanged);
            // 
            // deleteDataTypeButton
            // 
            this.deleteDataTypeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteDataTypeButton.Location = new System.Drawing.Point(269, 130);
            this.deleteDataTypeButton.Name = "deleteDataTypeButton";
            this.deleteDataTypeButton.Size = new System.Drawing.Size(75, 23);
            this.deleteDataTypeButton.TabIndex = 7;
            this.deleteDataTypeButton.Text = "Delete";
            this.deleteDataTypeButton.UseVisualStyleBackColor = true;
            this.deleteDataTypeButton.Click += new System.EventHandler(this.DeleteDataTypeButtonClick);
            // 
            // dataTypesGridView
            // 
            this.dataTypesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataTypesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTypesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2});
            this.dataTypesGridView.Location = new System.Drawing.Point(174, 19);
            this.dataTypesGridView.Name = "dataTypesGridView";
            this.dataTypesGridView.RowHeadersVisible = false;
            this.dataTypesGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataTypesGridView.Size = new System.Drawing.Size(170, 105);
            this.dataTypesGridView.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Datatypes to copy";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ToolTipText = "Only copy these datatypes to the subset model";
            // 
            // traceabilityGroupBox
            // 
            this.traceabilityGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.traceabilityGroupBox.Controls.Add(this.associationTagLabel);
            this.traceabilityGroupBox.Controls.Add(this.associationTagTextBox);
            this.traceabilityGroupBox.Controls.Add(this.attributeTagLabel);
            this.traceabilityGroupBox.Controls.Add(this.attributeTagTextBox);
            this.traceabilityGroupBox.Location = new System.Drawing.Point(378, 312);
            this.traceabilityGroupBox.Name = "traceabilityGroupBox";
            this.traceabilityGroupBox.Size = new System.Drawing.Size(357, 76);
            this.traceabilityGroupBox.TabIndex = 10;
            this.traceabilityGroupBox.TabStop = false;
            this.traceabilityGroupBox.Text = "Traceability tags";
            // 
            // associationTagLabel
            // 
            this.associationTagLabel.Location = new System.Drawing.Point(185, 20);
            this.associationTagLabel.Name = "associationTagLabel";
            this.associationTagLabel.Size = new System.Drawing.Size(100, 17);
            this.associationTagLabel.TabIndex = 3;
            this.associationTagLabel.Text = "AssociationTag";
            // 
            // associationTagTextBox
            // 
            this.associationTagTextBox.Location = new System.Drawing.Point(185, 40);
            this.associationTagTextBox.Name = "associationTagTextBox";
            this.associationTagTextBox.Size = new System.Drawing.Size(164, 20);
            this.associationTagTextBox.TabIndex = 2;
            // 
            // attributeTagLabel
            // 
            this.attributeTagLabel.Location = new System.Drawing.Point(5, 20);
            this.attributeTagLabel.Name = "attributeTagLabel";
            this.attributeTagLabel.Size = new System.Drawing.Size(100, 17);
            this.attributeTagLabel.TabIndex = 1;
            this.attributeTagLabel.Text = "AttributeTag";
            // 
            // attributeTagTextBox
            // 
            this.attributeTagTextBox.Location = new System.Drawing.Point(5, 40);
            this.attributeTagTextBox.Name = "attributeTagTextBox";
            this.attributeTagTextBox.Size = new System.Drawing.Size(164, 20);
            this.attributeTagTextBox.TabIndex = 0;
            // 
            // GeneralGroupBox
            // 
            this.GeneralGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GeneralGroupBox.Controls.Add(this.generateToArtifactPackageCheckBox);
            this.GeneralGroupBox.Controls.Add(this.usePackageSubsetsOnlyCheckBox);
            this.GeneralGroupBox.Controls.Add(this.deleteUnusedElementsCheckBox);
            this.GeneralGroupBox.Controls.Add(this.generalCopyGeneralizationsCheckbox);
            this.GeneralGroupBox.Controls.Add(this.checkSecurityCheckBox);
            this.GeneralGroupBox.Controls.Add(this.RedirectGeneralizationsCheckBox);
            this.GeneralGroupBox.Location = new System.Drawing.Point(12, 12);
            this.GeneralGroupBox.MinimumSize = new System.Drawing.Size(0, 20);
            this.GeneralGroupBox.Name = "GeneralGroupBox";
            this.GeneralGroupBox.Size = new System.Drawing.Size(351, 201);
            this.GeneralGroupBox.TabIndex = 11;
            this.GeneralGroupBox.TabStop = false;
            this.GeneralGroupBox.Text = "General Options";
            // 
            // generateToArtifactPackageCheckBox
            // 
            this.generateToArtifactPackageCheckBox.Location = new System.Drawing.Point(6, 169);
            this.generateToArtifactPackageCheckBox.Name = "generateToArtifactPackageCheckBox";
            this.generateToArtifactPackageCheckBox.Size = new System.Drawing.Size(276, 24);
            this.generateToArtifactPackageCheckBox.TabIndex = 9;
            this.generateToArtifactPackageCheckBox.Text = "Generate to profile package only";
            this.generateToArtifactPackageCheckBox.UseVisualStyleBackColor = true;
            // 
            // usePackageSubsetsOnlyCheckBox
            // 
            this.usePackageSubsetsOnlyCheckBox.Location = new System.Drawing.Point(6, 139);
            this.usePackageSubsetsOnlyCheckBox.Name = "usePackageSubsetsOnlyCheckBox";
            this.usePackageSubsetsOnlyCheckBox.Size = new System.Drawing.Size(276, 24);
            this.usePackageSubsetsOnlyCheckBox.TabIndex = 7;
            this.usePackageSubsetsOnlyCheckBox.Text = "Use Package Structure for subset determination";
            this.usePackageSubsetsOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // deleteUnusedElementsCheckBox
            // 
            this.deleteUnusedElementsCheckBox.Location = new System.Drawing.Point(6, 109);
            this.deleteUnusedElementsCheckBox.Name = "deleteUnusedElementsCheckBox";
            this.deleteUnusedElementsCheckBox.Size = new System.Drawing.Size(248, 24);
            this.deleteUnusedElementsCheckBox.TabIndex = 6;
            this.deleteUnusedElementsCheckBox.Text = "Delete unused subset elements";
            this.deleteUnusedElementsCheckBox.UseVisualStyleBackColor = true;
            // 
            // generalCopyGeneralizationsCheckbox
            // 
            this.generalCopyGeneralizationsCheckbox.Location = new System.Drawing.Point(6, 19);
            this.generalCopyGeneralizationsCheckbox.Name = "generalCopyGeneralizationsCheckbox";
            this.generalCopyGeneralizationsCheckbox.Size = new System.Drawing.Size(247, 24);
            this.generalCopyGeneralizationsCheckbox.TabIndex = 5;
            this.generalCopyGeneralizationsCheckbox.Text = "Copy Generalizations";
            this.generalCopyGeneralizationsCheckbox.UseVisualStyleBackColor = true;
            // 
            // checkSecurityCheckBox
            // 
            this.checkSecurityCheckBox.Location = new System.Drawing.Point(6, 79);
            this.checkSecurityCheckBox.Name = "checkSecurityCheckBox";
            this.checkSecurityCheckBox.Size = new System.Drawing.Size(248, 24);
            this.checkSecurityCheckBox.TabIndex = 4;
            this.checkSecurityCheckBox.Text = "Check Security locks";
            this.checkSecurityCheckBox.UseVisualStyleBackColor = true;
            // 
            // notesPrefixTextBox
            // 
            this.notesPrefixTextBox.Location = new System.Drawing.Point(103, 51);
            this.notesPrefixTextBox.Name = "notesPrefixTextBox";
            this.notesPrefixTextBox.Size = new System.Drawing.Size(242, 20);
            this.notesPrefixTextBox.TabIndex = 3;
            // 
            // prefixNotesCheckBox
            // 
            this.prefixNotesCheckBox.Location = new System.Drawing.Point(6, 49);
            this.prefixNotesCheckBox.Name = "prefixNotesCheckBox";
            this.prefixNotesCheckBox.Size = new System.Drawing.Size(102, 24);
            this.prefixNotesCheckBox.TabIndex = 1;
            this.prefixNotesCheckBox.Text = "Prefix Notes";
            this.prefixNotesCheckBox.UseVisualStyleBackColor = true;
            this.prefixNotesCheckBox.CheckedChanged += new System.EventHandler(this.PrefixNotesCheckBoxCheckedChanged);
            // 
            // RedirectGeneralizationsCheckBox
            // 
            this.RedirectGeneralizationsCheckBox.Location = new System.Drawing.Point(6, 49);
            this.RedirectGeneralizationsCheckBox.Name = "RedirectGeneralizationsCheckBox";
            this.RedirectGeneralizationsCheckBox.Size = new System.Drawing.Size(247, 24);
            this.RedirectGeneralizationsCheckBox.TabIndex = 0;
            this.RedirectGeneralizationsCheckBox.Text = "Redirect Generalizations to subset";
            this.RedirectGeneralizationsCheckBox.UseVisualStyleBackColor = true;
            // 
            // keepAttributeOrderRadio
            // 
            this.keepAttributeOrderRadio.Location = new System.Drawing.Point(6, 42);
            this.keepAttributeOrderRadio.Name = "keepAttributeOrderRadio";
            this.keepAttributeOrderRadio.Size = new System.Drawing.Size(276, 24);
            this.keepAttributeOrderRadio.TabIndex = 8;
            this.keepAttributeOrderRadio.Text = "Keep original attribute order";
            this.keepAttributeOrderRadio.UseVisualStyleBackColor = true;
            // 
            // xmlSchemaGroup
            // 
            this.xmlSchemaGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xmlSchemaGroup.Controls.Add(this.choiceBeforeAttributesCheckbox);
            this.xmlSchemaGroup.Controls.Add(this.elementTagLabel);
            this.xmlSchemaGroup.Controls.Add(this.elementTagTextBox);
            this.xmlSchemaGroup.Controls.Add(this.tvInsteadOfTraceCheckBox);
            this.xmlSchemaGroup.Controls.Add(this.orderAssociationsAmongstAttributesCheckbox);
            this.xmlSchemaGroup.Controls.Add(this.orderAssociationsCheckbox);
            this.xmlSchemaGroup.Controls.Add(this.noAttributeDependenciesCheckbox);
            this.xmlSchemaGroup.Location = new System.Drawing.Point(378, 396);
            this.xmlSchemaGroup.Name = "xmlSchemaGroup";
            this.xmlSchemaGroup.Size = new System.Drawing.Size(357, 226);
            this.xmlSchemaGroup.TabIndex = 12;
            this.xmlSchemaGroup.TabStop = false;
            this.xmlSchemaGroup.Text = "XML Schema Options";
            // 
            // choiceBeforeAttributesCheckbox
            // 
            this.choiceBeforeAttributesCheckbox.Location = new System.Drawing.Point(6, 112);
            this.choiceBeforeAttributesCheckbox.Name = "choiceBeforeAttributesCheckbox";
            this.choiceBeforeAttributesCheckbox.Size = new System.Drawing.Size(276, 24);
            this.choiceBeforeAttributesCheckbox.TabIndex = 14;
            this.choiceBeforeAttributesCheckbox.Text = "Order XmlChoice before Attributes";
            this.choiceBeforeAttributesCheckbox.UseVisualStyleBackColor = true;
            // 
            // elementTagLabel
            // 
            this.elementTagLabel.Location = new System.Drawing.Point(5, 169);
            this.elementTagLabel.Name = "elementTagLabel";
            this.elementTagLabel.Size = new System.Drawing.Size(100, 17);
            this.elementTagLabel.TabIndex = 13;
            this.elementTagLabel.Text = "Element Tag";
            // 
            // elementTagTextBox
            // 
            this.elementTagTextBox.Location = new System.Drawing.Point(5, 189);
            this.elementTagTextBox.Name = "elementTagTextBox";
            this.elementTagTextBox.Size = new System.Drawing.Size(164, 20);
            this.elementTagTextBox.TabIndex = 12;
            // 
            // tvInsteadOfTraceCheckBox
            // 
            this.tvInsteadOfTraceCheckBox.Location = new System.Drawing.Point(6, 142);
            this.tvInsteadOfTraceCheckBox.Name = "tvInsteadOfTraceCheckBox";
            this.tvInsteadOfTraceCheckBox.Size = new System.Drawing.Size(276, 24);
            this.tvInsteadOfTraceCheckBox.TabIndex = 11;
            this.tvInsteadOfTraceCheckBox.Text = "Use Tagged Values instead of Trace";
            this.tvInsteadOfTraceCheckBox.UseVisualStyleBackColor = true;
            this.tvInsteadOfTraceCheckBox.CheckedChanged += new System.EventHandler(this.TtvInsteadOfTraceCheckBoxCheckedChanged);
            // 
            // orderAssociationsAmongstAttributesCheckbox
            // 
            this.orderAssociationsAmongstAttributesCheckbox.Location = new System.Drawing.Point(6, 82);
            this.orderAssociationsAmongstAttributesCheckbox.Name = "orderAssociationsAmongstAttributesCheckbox";
            this.orderAssociationsAmongstAttributesCheckbox.Size = new System.Drawing.Size(276, 24);
            this.orderAssociationsAmongstAttributesCheckbox.TabIndex = 10;
            this.orderAssociationsAmongstAttributesCheckbox.Text = "Order Associations between Attributes";
            this.orderAssociationsAmongstAttributesCheckbox.UseVisualStyleBackColor = true;
            // 
            // orderAssociationsCheckbox
            // 
            this.orderAssociationsCheckbox.Location = new System.Drawing.Point(6, 52);
            this.orderAssociationsCheckbox.Name = "orderAssociationsCheckbox";
            this.orderAssociationsCheckbox.Size = new System.Drawing.Size(276, 24);
            this.orderAssociationsCheckbox.TabIndex = 9;
            this.orderAssociationsCheckbox.Text = "Order Associations Alphabetically";
            this.orderAssociationsCheckbox.UseVisualStyleBackColor = true;
            this.orderAssociationsCheckbox.CheckedChanged += new System.EventHandler(this.OrderAssociationsCheckboxCheckedChanged);
            // 
            // noAttributeDependenciesCheckbox
            // 
            this.noAttributeDependenciesCheckbox.Location = new System.Drawing.Point(6, 22);
            this.noAttributeDependenciesCheckbox.Name = "noAttributeDependenciesCheckbox";
            this.noAttributeDependenciesCheckbox.Size = new System.Drawing.Size(248, 24);
            this.noAttributeDependenciesCheckbox.TabIndex = 8;
            this.noAttributeDependenciesCheckbox.Text = "Do not create Attribute dependencies";
            this.noAttributeDependenciesCheckbox.UseVisualStyleBackColor = true;
            // 
            // attributeOptionsGroupBox
            // 
            this.attributeOptionsGroupBox.Controls.Add(this.setAttributesOrderZeroRadio);
            this.attributeOptionsGroupBox.Controls.Add(this.addNewAttributesLastRadio);
            this.attributeOptionsGroupBox.Controls.Add(this.keepAttributeOrderRadio);
            this.attributeOptionsGroupBox.Location = new System.Drawing.Point(378, 201);
            this.attributeOptionsGroupBox.Name = "attributeOptionsGroupBox";
            this.attributeOptionsGroupBox.Size = new System.Drawing.Size(355, 97);
            this.attributeOptionsGroupBox.TabIndex = 13;
            this.attributeOptionsGroupBox.TabStop = false;
            this.attributeOptionsGroupBox.Text = "Attribute options";
            // 
            // setAttributesOrderZeroRadio
            // 
            this.setAttributesOrderZeroRadio.AutoSize = true;
            this.setAttributesOrderZeroRadio.Location = new System.Drawing.Point(6, 72);
            this.setAttributesOrderZeroRadio.Name = "setAttributesOrderZeroRadio";
            this.setAttributesOrderZeroRadio.Size = new System.Drawing.Size(130, 17);
            this.setAttributesOrderZeroRadio.TabIndex = 9;
            this.setAttributesOrderZeroRadio.TabStop = true;
            this.setAttributesOrderZeroRadio.Text = "Set attribute order to 0";
            this.setAttributesOrderZeroRadio.UseVisualStyleBackColor = true;
            // 
            // addNewAttributesLastRadio
            // 
            this.addNewAttributesLastRadio.AutoSize = true;
            this.addNewAttributesLastRadio.Location = new System.Drawing.Point(6, 19);
            this.addNewAttributesLastRadio.Name = "addNewAttributesLastRadio";
            this.addNewAttributesLastRadio.Size = new System.Drawing.Size(164, 17);
            this.addNewAttributesLastRadio.TabIndex = 0;
            this.addNewAttributesLastRadio.TabStop = true;
            this.addNewAttributesLastRadio.Text = "Add new attributes at the end";
            this.addNewAttributesLastRadio.UseVisualStyleBackColor = true;
            // 
            // notesOptionsGroupBox
            // 
            this.notesOptionsGroupBox.Controls.Add(this.keepNotesInSyncCheckBox);
            this.notesOptionsGroupBox.Controls.Add(this.notesPrefixTextBox);
            this.notesOptionsGroupBox.Controls.Add(this.prefixNotesCheckBox);
            this.notesOptionsGroupBox.Location = new System.Drawing.Point(12, 220);
            this.notesOptionsGroupBox.Name = "notesOptionsGroupBox";
            this.notesOptionsGroupBox.Size = new System.Drawing.Size(351, 95);
            this.notesOptionsGroupBox.TabIndex = 14;
            this.notesOptionsGroupBox.TabStop = false;
            this.notesOptionsGroupBox.Text = "Notes Options";
            // 
            // keepNotesInSyncCheckBox
            // 
            this.keepNotesInSyncCheckBox.Location = new System.Drawing.Point(6, 19);
            this.keepNotesInSyncCheckBox.Name = "keepNotesInSyncCheckBox";
            this.keepNotesInSyncCheckBox.Size = new System.Drawing.Size(174, 24);
            this.keepNotesInSyncCheckBox.TabIndex = 4;
            this.keepNotesInSyncCheckBox.Text = "Keep Notes in Sync";
            this.keepNotesInSyncCheckBox.UseVisualStyleBackColor = true;
            this.keepNotesInSyncCheckBox.CheckedChanged += new System.EventHandler(this.keepNotesInSyncCheckBox_CheckedChanged);
            // 
            // SettingsWindow
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(745, 659);
            this.Controls.Add(this.notesOptionsGroupBox);
            this.Controls.Add(this.attributeOptionsGroupBox);
            this.Controls.Add(this.xmlSchemaGroup);
            this.Controls.Add(this.deleteTaggedValueButton);
            this.Controls.Add(this.deleteStereotypeButton);
            this.Controls.Add(this.GeneralGroupBox);
            this.Controls.Add(this.traceabilityGroupBox);
            this.Controls.Add(this.dataTypeOptionsGroupBox);
            this.Controls.Add(this.diagramOptionsGroupBox);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EA Message Composer settings";
            ((System.ComponentModel.ISupportInitialize)(this.ignoredStereoTypesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ignoredTaggedValuesGrid)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.diagramOptionsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hiddenElementGrid)).EndInit();
            this.dataTypeOptionsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataTypesGridView)).EndInit();
            this.traceabilityGroupBox.ResumeLayout(false);
            this.traceabilityGroupBox.PerformLayout();
            this.GeneralGroupBox.ResumeLayout(false);
            this.xmlSchemaGroup.ResumeLayout(false);
            this.xmlSchemaGroup.PerformLayout();
            this.attributeOptionsGroupBox.ResumeLayout(false);
            this.attributeOptionsGroupBox.PerformLayout();
            this.notesOptionsGroupBox.ResumeLayout(false);
            this.notesOptionsGroupBox.PerformLayout();
            this.ResumeLayout(false);

		}

        private System.Windows.Forms.RadioButton keepAttributeOrderRadio;
        private System.Windows.Forms.CheckBox generateToArtifactPackageCheckBox;
        private System.Windows.Forms.CheckBox generateDiagramCheckbox;
        private System.Windows.Forms.GroupBox attributeOptionsGroupBox;
        private System.Windows.Forms.RadioButton setAttributesOrderZeroRadio;
        private System.Windows.Forms.RadioButton addNewAttributesLastRadio;
        private System.Windows.Forms.CheckBox choiceBeforeAttributesCheckbox;
        private System.Windows.Forms.GroupBox notesOptionsGroupBox;
        private System.Windows.Forms.CheckBox keepNotesInSyncCheckBox;
        //this.ResumeLayout(false);
    }
}
