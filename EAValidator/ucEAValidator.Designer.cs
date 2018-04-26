namespace EAValidator
{
    partial class ucEAValidator
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDoValidation = new System.Windows.Forms.Button();
            this.olvValidations = new BrightIdeasSoftware.ObjectListView();
            this.olvColCheck = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColEAItemType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColItemName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColElementType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColElementStereotype = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColPackageName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColPackageParentLvl1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColPackageParentLvl2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColPackageParentLvl3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColPackageParentLvl4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColPackageParentLvl5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColWarningType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvChecks = new BrightIdeasSoftware.ObjectListView();
            this.olvColCheckDescription = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColCheckId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColCheckStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColCheckNumberOfElementsFound = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColCheckNumberOfValidationResults = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColCheckWarningType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColCheckGroup = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lblChecks = new System.Windows.Forms.Label();
            this.lblResults = new System.Windows.Forms.Label();
            this.txtDirectoryValidationChecks = new System.Windows.Forms.TextBox();
            this.txtElementName = new System.Windows.Forms.TextBox();
            this.btnSelectElement = new System.Windows.Forms.Button();
            this.lblSourceDirectory = new System.Windows.Forms.Label();
            this.btnSelectQueryDirectory = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtElementType = new System.Windows.Forms.TextBox();
            this.lblElementName = new System.Windows.Forms.Label();
            this.lblElementType = new System.Windows.Forms.Label();
            this.chkExcludeArchivePackages = new System.Windows.Forms.CheckBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.grpElement = new System.Windows.Forms.GroupBox();
            this.lblDiagramTypeInfo = new System.Windows.Forms.Label();
            this.lblElementTypeInfo = new System.Windows.Forms.Label();
            this.txtDiagramName = new System.Windows.Forms.TextBox();
            this.txtDiagramType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDiagram = new System.Windows.Forms.Label();
            this.btnSelectDiagram = new System.Windows.Forms.Button();
            this.btnClearScope = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.olvValidations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.olvChecks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpElement.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDoValidation
            // 
            this.btnDoValidation.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDoValidation.Location = new System.Drawing.Point(729, 326);
            this.btnDoValidation.Name = "btnDoValidation";
            this.btnDoValidation.Size = new System.Drawing.Size(166, 48);
            this.btnDoValidation.TabIndex = 0;
            this.btnDoValidation.Text = "Start Validation";
            this.btnDoValidation.UseVisualStyleBackColor = false;
            this.btnDoValidation.Click += new System.EventHandler(this.btnDoValidation_Click);
            // 
            // olvValidations
            // 
            this.olvValidations.AllColumns.Add(this.olvColCheck);
            this.olvValidations.AllColumns.Add(this.olvColEAItemType);
            this.olvValidations.AllColumns.Add(this.olvColItemName);
            this.olvValidations.AllColumns.Add(this.olvColElementType);
            this.olvValidations.AllColumns.Add(this.olvColElementStereotype);
            this.olvValidations.AllColumns.Add(this.olvColPackageName);
            this.olvValidations.AllColumns.Add(this.olvColPackageParentLvl1);
            this.olvValidations.AllColumns.Add(this.olvColPackageParentLvl2);
            this.olvValidations.AllColumns.Add(this.olvColPackageParentLvl3);
            this.olvValidations.AllColumns.Add(this.olvColPackageParentLvl4);
            this.olvValidations.AllColumns.Add(this.olvColPackageParentLvl5);
            this.olvValidations.AllColumns.Add(this.olvColWarningType);
            this.olvValidations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvValidations.CellEditUseWholeCell = false;
            this.olvValidations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColCheck,
            this.olvColEAItemType,
            this.olvColItemName,
            this.olvColElementType,
            this.olvColElementStereotype,
            this.olvColPackageName,
            this.olvColPackageParentLvl1,
            this.olvColPackageParentLvl2,
            this.olvColPackageParentLvl3,
            this.olvColPackageParentLvl4,
            this.olvColPackageParentLvl5,
            this.olvColWarningType});
            this.olvValidations.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvValidations.Location = new System.Drawing.Point(14, 419);
            this.olvValidations.Name = "olvValidations";
            this.olvValidations.Size = new System.Drawing.Size(971, 265);
            this.olvValidations.TabIndex = 2;
            this.olvValidations.UseCompatibleStateImageBehavior = false;
            this.olvValidations.View = System.Windows.Forms.View.Details;
            this.olvValidations.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.olvValidations_CellToolTipShowing);
            this.olvValidations.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.olvValidations_MouseDoubleClick);
            // 
            // olvColCheck
            // 
            this.olvColCheck.AspectName = "CheckDescription";
            this.olvColCheck.IsEditable = false;
            this.olvColCheck.Text = "Check Description";
            this.olvColCheck.Width = 64;
            // 
            // olvColEAItemType
            // 
            this.olvColEAItemType.AspectName = "EAItemType";
            this.olvColEAItemType.Text = "Type";
            this.olvColEAItemType.Width = 102;
            // 
            // olvColItemName
            // 
            this.olvColItemName.AspectName = "ItemName";
            this.olvColItemName.IsEditable = false;
            this.olvColItemName.Text = "Item";
            this.olvColItemName.Width = 339;
            // 
            // olvColElementType
            // 
            this.olvColElementType.AspectName = "ElementType";
            this.olvColElementType.IsEditable = false;
            this.olvColElementType.Text = "Element Type";
            this.olvColElementType.Width = 124;
            // 
            // olvColElementStereotype
            // 
            this.olvColElementStereotype.AspectName = "ElementStereotype";
            this.olvColElementStereotype.IsEditable = false;
            this.olvColElementStereotype.Text = "Element Stereotype";
            this.olvColElementStereotype.Width = 152;
            // 
            // olvColPackageName
            // 
            this.olvColPackageName.AspectName = "PackageName";
            this.olvColPackageName.IsEditable = false;
            this.olvColPackageName.Text = "Package";
            this.olvColPackageName.Width = 161;
            // 
            // olvColPackageParentLvl1
            // 
            this.olvColPackageParentLvl1.AspectName = "PackageParentLevel1";
            this.olvColPackageParentLvl1.Text = "Pckg+1";
            this.olvColPackageParentLvl1.Width = 83;
            // 
            // olvColPackageParentLvl2
            // 
            this.olvColPackageParentLvl2.AspectName = "PackageParentLevel2";
            this.olvColPackageParentLvl2.Text = "Pckg+2";
            this.olvColPackageParentLvl2.Width = 81;
            // 
            // olvColPackageParentLvl3
            // 
            this.olvColPackageParentLvl3.AspectName = "PackageParentLevel3";
            this.olvColPackageParentLvl3.Text = "Pckg+3";
            this.olvColPackageParentLvl3.Width = 79;
            // 
            // olvColPackageParentLvl4
            // 
            this.olvColPackageParentLvl4.AspectName = "PackageParentLevel4";
            this.olvColPackageParentLvl4.Text = "Pckg+4";
            this.olvColPackageParentLvl4.Width = 68;
            // 
            // olvColPackageParentLvl5
            // 
            this.olvColPackageParentLvl5.AspectName = "PackageParentLevel5";
            this.olvColPackageParentLvl5.Text = "Pckg+5";
            this.olvColPackageParentLvl5.Width = 74;
            // 
            // olvColWarningType
            // 
            this.olvColWarningType.AspectName = "CheckWarningType";
            this.olvColWarningType.Text = "Result Type";
            this.olvColWarningType.Width = 100;
            // 
            // olvChecks
            // 
            this.olvChecks.AllColumns.Add(this.olvColCheckDescription);
            this.olvChecks.AllColumns.Add(this.olvColCheckId);
            this.olvChecks.AllColumns.Add(this.olvColCheckStatus);
            this.olvChecks.AllColumns.Add(this.olvColCheckNumberOfElementsFound);
            this.olvChecks.AllColumns.Add(this.olvColCheckNumberOfValidationResults);
            this.olvChecks.AllColumns.Add(this.olvColCheckWarningType);
            this.olvChecks.AllColumns.Add(this.olvColCheckGroup);
            this.olvChecks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvChecks.CellEditUseWholeCell = false;
            this.olvChecks.CheckedAspectName = "";
            this.olvChecks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColCheckDescription,
            this.olvColCheckId,
            this.olvColCheckStatus,
            this.olvColCheckNumberOfElementsFound,
            this.olvColCheckNumberOfValidationResults,
            this.olvColCheckWarningType,
            this.olvColCheckGroup});
            this.olvChecks.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvChecks.Location = new System.Drawing.Point(14, 60);
            this.olvChecks.Name = "olvChecks";
            this.olvChecks.Size = new System.Drawing.Size(971, 173);
            this.olvChecks.TabIndex = 3;
            this.olvChecks.UseCompatibleStateImageBehavior = false;
            this.olvChecks.View = System.Windows.Forms.View.Details;
            this.olvChecks.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.olvChecks_CellToolTipShowing);
            this.olvChecks.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.olvChecks_FormatCell);
            this.olvChecks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.olvChecks_MouseDoubleClick);
            // 
            // olvColCheckDescription
            // 
            this.olvColCheckDescription.AspectName = "CheckDescription";
            this.olvColCheckDescription.HeaderCheckBox = true;
            this.olvColCheckDescription.HeaderCheckState = System.Windows.Forms.CheckState.Checked;
            this.olvColCheckDescription.MaximumWidth = 800;
            this.olvColCheckDescription.MinimumWidth = 50;
            this.olvColCheckDescription.Text = "Description";
            this.olvColCheckDescription.Width = 411;
            // 
            // olvColCheckId
            // 
            this.olvColCheckId.AspectName = "CheckId";
            this.olvColCheckId.AspectToStringFormat = "";
            this.olvColCheckId.MaximumWidth = 200;
            this.olvColCheckId.MinimumWidth = 50;
            this.olvColCheckId.Text = "Id";
            this.olvColCheckId.ToolTipText = "Unique identifier per check";
            this.olvColCheckId.Width = 61;
            // 
            // olvColCheckStatus
            // 
            this.olvColCheckStatus.AspectName = "Status";
            this.olvColCheckStatus.MaximumWidth = 250;
            this.olvColCheckStatus.MinimumWidth = 50;
            this.olvColCheckStatus.Text = "Status";
            this.olvColCheckStatus.ToolTipText = "Check status is not validated, passed or failed";
            this.olvColCheckStatus.Width = 103;
            // 
            // olvColCheckNumberOfElementsFound
            // 
            this.olvColCheckNumberOfElementsFound.AspectName = "NumberOfElementsFound";
            this.olvColCheckNumberOfElementsFound.AspectToStringFormat = "";
            this.olvColCheckNumberOfElementsFound.Text = "# found";
            this.olvColCheckNumberOfElementsFound.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColCheckNumberOfElementsFound.Width = 80;
            // 
            // olvColCheckNumberOfValidationResults
            // 
            this.olvColCheckNumberOfValidationResults.AspectName = "NumberOfValidationResults";
            this.olvColCheckNumberOfValidationResults.AspectToStringFormat = "";
            this.olvColCheckNumberOfValidationResults.Text = "# results";
            this.olvColCheckNumberOfValidationResults.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColCheckNumberOfValidationResults.Width = 86;
            // 
            // olvColCheckWarningType
            // 
            this.olvColCheckWarningType.AspectName = "WarningType";
            this.olvColCheckWarningType.Text = "Result Type";
            this.olvColCheckWarningType.Width = 100;
            // 
            // olvColCheckGroup
            // 
            this.olvColCheckGroup.AspectName = "Group";
            this.olvColCheckGroup.Text = "Group";
            this.olvColCheckGroup.Width = 141;
            // 
            // lblChecks
            // 
            this.lblChecks.AutoSize = true;
            this.lblChecks.Location = new System.Drawing.Point(11, 40);
            this.lblChecks.Name = "lblChecks";
            this.lblChecks.Size = new System.Drawing.Size(101, 13);
            this.lblChecks.TabIndex = 4;
            this.lblChecks.Text = "Checks to validate :";
            // 
            // lblResults
            // 
            this.lblResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(11, 399);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(92, 13);
            this.lblResults.TabIndex = 5;
            this.lblResults.Text = "Validation results :";
            // 
            // txtDirectoryValidationChecks
            // 
            this.txtDirectoryValidationChecks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirectoryValidationChecks.Location = new System.Drawing.Point(158, 15);
            this.txtDirectoryValidationChecks.Name = "txtDirectoryValidationChecks";
            this.txtDirectoryValidationChecks.Size = new System.Drawing.Size(692, 20);
            this.txtDirectoryValidationChecks.TabIndex = 6;
            // 
            // txtElementName
            // 
            this.txtElementName.Location = new System.Drawing.Point(83, 24);
            this.txtElementName.Name = "txtElementName";
            this.txtElementName.Size = new System.Drawing.Size(447, 20);
            this.txtElementName.TabIndex = 8;
            // 
            // btnSelectElement
            // 
            this.btnSelectElement.Location = new System.Drawing.Point(536, 24);
            this.btnSelectElement.Name = "btnSelectElement";
            this.btnSelectElement.Size = new System.Drawing.Size(147, 49);
            this.btnSelectElement.TabIndex = 9;
            this.btnSelectElement.Text = "Select element...";
            this.btnSelectElement.UseVisualStyleBackColor = true;
            this.btnSelectElement.Click += new System.EventHandler(this.btnSelectElement_Click);
            // 
            // lblSourceDirectory
            // 
            this.lblSourceDirectory.AutoSize = true;
            this.lblSourceDirectory.Location = new System.Drawing.Point(11, 15);
            this.lblSourceDirectory.Name = "lblSourceDirectory";
            this.lblSourceDirectory.Size = new System.Drawing.Size(104, 13);
            this.lblSourceDirectory.TabIndex = 10;
            this.lblSourceDirectory.Text = "Location of checks: ";
            // 
            // btnSelectQueryDirectory
            // 
            this.btnSelectQueryDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectQueryDirectory.Location = new System.Drawing.Point(856, 15);
            this.btnSelectQueryDirectory.Name = "btnSelectQueryDirectory";
            this.btnSelectQueryDirectory.Size = new System.Drawing.Size(129, 22);
            this.btnSelectQueryDirectory.TabIndex = 11;
            this.btnSelectQueryDirectory.Text = "Select location...";
            this.btnSelectQueryDirectory.UseVisualStyleBackColor = true;
            this.btnSelectQueryDirectory.Click += new System.EventHandler(this.btnSelectQueryDirectory_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(729, 377);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(166, 15);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(910, 268);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 100);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // txtElementType
            // 
            this.txtElementType.Location = new System.Drawing.Point(83, 49);
            this.txtElementType.Name = "txtElementType";
            this.txtElementType.Size = new System.Drawing.Size(196, 20);
            this.txtElementType.TabIndex = 15;
            // 
            // lblElementName
            // 
            this.lblElementName.AutoSize = true;
            this.lblElementName.Location = new System.Drawing.Point(18, 24);
            this.lblElementName.Name = "lblElementName";
            this.lblElementName.Size = new System.Drawing.Size(45, 13);
            this.lblElementName.TabIndex = 16;
            this.lblElementName.Text = "Element";
            // 
            // lblElementType
            // 
            this.lblElementType.AutoSize = true;
            this.lblElementType.Location = new System.Drawing.Point(37, 49);
            this.lblElementType.Name = "lblElementType";
            this.lblElementType.Size = new System.Drawing.Size(31, 13);
            this.lblElementType.TabIndex = 18;
            this.lblElementType.Text = "Type";
            // 
            // chkExcludeArchivePackages
            // 
            this.chkExcludeArchivePackages.AutoSize = true;
            this.chkExcludeArchivePackages.Location = new System.Drawing.Point(729, 285);
            this.chkExcludeArchivePackages.Name = "chkExcludeArchivePackages";
            this.chkExcludeArchivePackages.Size = new System.Drawing.Size(135, 30);
            this.chkExcludeArchivePackages.TabIndex = 19;
            this.chkExcludeArchivePackages.Text = "Exclude Packages\r\n\"Archive/Deleted/Old\"";
            this.chkExcludeArchivePackages.UseVisualStyleBackColor = true;
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.Location = new System.Drawing.Point(910, 372);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(75, 23);
            this.btnAbout.TabIndex = 20;
            this.btnAbout.Text = "About...";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // grpElement
            // 
            this.grpElement.Controls.Add(this.lblDiagramTypeInfo);
            this.grpElement.Controls.Add(this.lblElementTypeInfo);
            this.grpElement.Controls.Add(this.txtDiagramName);
            this.grpElement.Controls.Add(this.txtDiagramType);
            this.grpElement.Controls.Add(this.label1);
            this.grpElement.Controls.Add(this.lblDiagram);
            this.grpElement.Controls.Add(this.btnSelectDiagram);
            this.grpElement.Controls.Add(this.btnSelectElement);
            this.grpElement.Controls.Add(this.txtElementName);
            this.grpElement.Controls.Add(this.txtElementType);
            this.grpElement.Controls.Add(this.lblElementType);
            this.grpElement.Controls.Add(this.lblElementName);
            this.grpElement.Location = new System.Drawing.Point(14, 239);
            this.grpElement.Name = "grpElement";
            this.grpElement.Size = new System.Drawing.Size(707, 153);
            this.grpElement.TabIndex = 21;
            this.grpElement.TabStop = false;
            this.grpElement.Text = "Scope :";
            // 
            // lblDiagramTypeInfo
            // 
            this.lblDiagramTypeInfo.AutoSize = true;
            this.lblDiagramTypeInfo.Location = new System.Drawing.Point(285, 120);
            this.lblDiagramTypeInfo.Name = "lblDiagramTypeInfo";
            this.lblDiagramTypeInfo.Size = new System.Drawing.Size(99, 13);
            this.lblDiagramTypeInfo.TabIndex = 28;
            this.lblDiagramTypeInfo.Text = "(Use Case diagram)";
            // 
            // lblElementTypeInfo
            // 
            this.lblElementTypeInfo.AutoSize = true;
            this.lblElementTypeInfo.Location = new System.Drawing.Point(285, 52);
            this.lblElementTypeInfo.Name = "lblElementTypeInfo";
            this.lblElementTypeInfo.Size = new System.Drawing.Size(154, 13);
            this.lblElementTypeInfo.TabIndex = 27;
            this.lblElementTypeInfo.Text = "(Change / Release / Package)";
            // 
            // txtDiagramName
            // 
            this.txtDiagramName.Location = new System.Drawing.Point(83, 92);
            this.txtDiagramName.Name = "txtDiagramName";
            this.txtDiagramName.Size = new System.Drawing.Size(447, 20);
            this.txtDiagramName.TabIndex = 23;
            // 
            // txtDiagramType
            // 
            this.txtDiagramType.Location = new System.Drawing.Point(83, 117);
            this.txtDiagramType.Name = "txtDiagramType";
            this.txtDiagramType.Size = new System.Drawing.Size(196, 20);
            this.txtDiagramType.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Type";
            // 
            // lblDiagram
            // 
            this.lblDiagram.AutoSize = true;
            this.lblDiagram.Location = new System.Drawing.Point(18, 92);
            this.lblDiagram.Name = "lblDiagram";
            this.lblDiagram.Size = new System.Drawing.Size(46, 13);
            this.lblDiagram.TabIndex = 25;
            this.lblDiagram.Text = "Diagram";
            // 
            // btnSelectDiagram
            // 
            this.btnSelectDiagram.Location = new System.Drawing.Point(536, 92);
            this.btnSelectDiagram.Name = "btnSelectDiagram";
            this.btnSelectDiagram.Size = new System.Drawing.Size(147, 50);
            this.btnSelectDiagram.TabIndex = 22;
            this.btnSelectDiagram.Text = "Select diagram from Project Browser";
            this.btnSelectDiagram.UseVisualStyleBackColor = true;
            this.btnSelectDiagram.Click += new System.EventHandler(this.btnSelectDiagram_Click);
            // 
            // btnClearScope
            // 
            this.btnClearScope.BackColor = System.Drawing.SystemColors.Control;
            this.btnClearScope.Location = new System.Drawing.Point(729, 245);
            this.btnClearScope.Name = "btnClearScope";
            this.btnClearScope.Size = new System.Drawing.Size(166, 30);
            this.btnClearScope.TabIndex = 29;
            this.btnClearScope.Text = "Clear Scope";
            this.btnClearScope.UseVisualStyleBackColor = false;
            this.btnClearScope.Click += new System.EventHandler(this.btnClearScope_Click);
            // 
            // ucEAValidator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnClearScope);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.chkExcludeArchivePackages);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSelectQueryDirectory);
            this.Controls.Add(this.lblSourceDirectory);
            this.Controls.Add(this.txtDirectoryValidationChecks);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.lblChecks);
            this.Controls.Add(this.olvChecks);
            this.Controls.Add(this.olvValidations);
            this.Controls.Add(this.btnDoValidation);
            this.Controls.Add(this.grpElement);
            this.Name = "ucEAValidator";
            this.Size = new System.Drawing.Size(1000, 700);
            ((System.ComponentModel.ISupportInitialize)(this.olvValidations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.olvChecks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpElement.ResumeLayout(false);
            this.grpElement.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDoValidation;
        private BrightIdeasSoftware.ObjectListView olvValidations;
        private BrightIdeasSoftware.OLVColumn olvColCheck;
        private BrightIdeasSoftware.OLVColumn olvColItemName;
        private BrightIdeasSoftware.OLVColumn olvColElementType;
        private BrightIdeasSoftware.OLVColumn olvColElementStereotype;
        private BrightIdeasSoftware.OLVColumn olvColPackageName;
        private BrightIdeasSoftware.ObjectListView olvChecks;
        private System.Windows.Forms.Label lblChecks;
        private System.Windows.Forms.Label lblResults;
        private BrightIdeasSoftware.OLVColumn olvColCheckDescription;
        private System.Windows.Forms.TextBox txtDirectoryValidationChecks;
        private System.Windows.Forms.TextBox txtElementName;
        private System.Windows.Forms.Button btnSelectElement;
        private BrightIdeasSoftware.OLVColumn olvColCheckStatus;
        private BrightIdeasSoftware.OLVColumn olvColCheckWarningType;
        private System.Windows.Forms.Label lblSourceDirectory;
        private System.Windows.Forms.Button btnSelectQueryDirectory;
        private BrightIdeasSoftware.OLVColumn olvColCheckGroup;
        private BrightIdeasSoftware.OLVColumn olvColPackageParentLvl1;
        private BrightIdeasSoftware.OLVColumn olvColPackageParentLvl2;
        private BrightIdeasSoftware.OLVColumn olvColPackageParentLvl3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtElementType;
        private System.Windows.Forms.Label lblElementName;
        private System.Windows.Forms.Label lblElementType;
        private System.Windows.Forms.CheckBox chkExcludeArchivePackages;
        private BrightIdeasSoftware.OLVColumn olvColEAItemType;
        private BrightIdeasSoftware.OLVColumn olvColCheckNumberOfElementsFound;
        private BrightIdeasSoftware.OLVColumn olvColCheckNumberOfValidationResults;
        private BrightIdeasSoftware.OLVColumn olvColCheckId;
        private BrightIdeasSoftware.OLVColumn olvColPackageParentLvl4;
        private BrightIdeasSoftware.OLVColumn olvColPackageParentLvl5;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.GroupBox grpElement;
        private System.Windows.Forms.Button btnSelectDiagram;
        private System.Windows.Forms.TextBox txtDiagramName;
        private System.Windows.Forms.TextBox txtDiagramType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDiagram;
        private System.Windows.Forms.Label lblElementTypeInfo;
        private System.Windows.Forms.Label lblDiagramTypeInfo;
        private System.Windows.Forms.Button btnClearScope;
        private BrightIdeasSoftware.OLVColumn olvColWarningType;
    }
}
