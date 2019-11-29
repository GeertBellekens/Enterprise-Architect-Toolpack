namespace ERXImporter
{
    partial class ErxImporterForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.browseImportFileButton = new System.Windows.Forms.Button();
            this.importFileTextBox = new System.Windows.Forms.TextBox();
            this.importFileLabel = new System.Windows.Forms.Label();
            this.importButton = new System.Windows.Forms.Button();
            this.errorTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.synchFKsButton = new System.Windows.Forms.Button();
            this.importERXGroup = new System.Windows.Forms.GroupBox();
            this.createForeignKeyGroupBox = new System.Windows.Forms.GroupBox();
            this.exportFileTextBox = new System.Windows.Forms.TextBox();
            this.exportFileLabel = new System.Windows.Forms.Label();
            this.browseExportFileButton = new System.Windows.Forms.Button();
            this.createRelationsGroupBox = new System.Windows.Forms.GroupBox();
            this.tdmPackageTextBox = new System.Windows.Forms.TextBox();
            this.createRelationsButton = new System.Windows.Forms.Button();
            this.TDMPackageLabel = new System.Windows.Forms.Label();
            this.browseTDMPackage = new System.Windows.Forms.Button();
            this.importERXGroup.SuspendLayout();
            this.createForeignKeyGroupBox.SuspendLayout();
            this.createRelationsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // browseImportFileButton
            // 
            this.browseImportFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseImportFileButton.Location = new System.Drawing.Point(381, 34);
            this.browseImportFileButton.Name = "browseImportFileButton";
            this.browseImportFileButton.Size = new System.Drawing.Size(24, 20);
            this.browseImportFileButton.TabIndex = 15;
            this.browseImportFileButton.Text = "...";
            this.browseImportFileButton.UseVisualStyleBackColor = true;
            this.browseImportFileButton.Click += new System.EventHandler(this.browseImportFileButton_Click);
            // 
            // importFileTextBox
            // 
            this.importFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importFileTextBox.Location = new System.Drawing.Point(6, 35);
            this.importFileTextBox.MinimumSize = new System.Drawing.Size(153, 20);
            this.importFileTextBox.Name = "importFileTextBox";
            this.importFileTextBox.Size = new System.Drawing.Size(369, 20);
            this.importFileTextBox.TabIndex = 14;
            // 
            // importFileLabel
            // 
            this.importFileLabel.Location = new System.Drawing.Point(3, 19);
            this.importFileLabel.Name = "importFileLabel";
            this.importFileLabel.Size = new System.Drawing.Size(141, 23);
            this.importFileLabel.TabIndex = 16;
            this.importFileLabel.Text = "Import File";
            // 
            // importButton
            // 
            this.importButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.importButton.Location = new System.Drawing.Point(335, 66);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(75, 23);
            this.importButton.TabIndex = 17;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // errorTextBox
            // 
            this.errorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorTextBox.Location = new System.Drawing.Point(12, 343);
            this.errorTextBox.Name = "errorTextBox";
            this.errorTextBox.ReadOnly = true;
            this.errorTextBox.Size = new System.Drawing.Size(416, 36);
            this.errorTextBox.TabIndex = 18;
            this.errorTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 327);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Info";
            // 
            // synchFKsButton
            // 
            this.synchFKsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.synchFKsButton.Location = new System.Drawing.Point(298, 19);
            this.synchFKsButton.Name = "synchFKsButton";
            this.synchFKsButton.Size = new System.Drawing.Size(112, 23);
            this.synchFKsButton.TabIndex = 20;
            this.synchFKsButton.Text = "Create Foreign Keys";
            this.synchFKsButton.UseVisualStyleBackColor = true;
            this.synchFKsButton.Click += new System.EventHandler(this.synchFKsButton_Click);
            // 
            // importERXGroup
            // 
            this.importERXGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importERXGroup.Controls.Add(this.importFileTextBox);
            this.importERXGroup.Controls.Add(this.importFileLabel);
            this.importERXGroup.Controls.Add(this.browseImportFileButton);
            this.importERXGroup.Controls.Add(this.importButton);
            this.importERXGroup.Location = new System.Drawing.Point(12, 12);
            this.importERXGroup.Name = "importERXGroup";
            this.importERXGroup.Size = new System.Drawing.Size(416, 95);
            this.importERXGroup.TabIndex = 21;
            this.importERXGroup.TabStop = false;
            this.importERXGroup.Text = "Import ERX";
            // 
            // createForeignKeyGroupBox
            // 
            this.createForeignKeyGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createForeignKeyGroupBox.Controls.Add(this.synchFKsButton);
            this.createForeignKeyGroupBox.Location = new System.Drawing.Point(12, 170);
            this.createForeignKeyGroupBox.Name = "createForeignKeyGroupBox";
            this.createForeignKeyGroupBox.Size = new System.Drawing.Size(416, 53);
            this.createForeignKeyGroupBox.TabIndex = 22;
            this.createForeignKeyGroupBox.TabStop = false;
            this.createForeignKeyGroupBox.Text = "Create Foreign Keys";
            // 
            // exportFileTextBox
            // 
            this.exportFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exportFileTextBox.Location = new System.Drawing.Point(12, 132);
            this.exportFileTextBox.MinimumSize = new System.Drawing.Size(153, 20);
            this.exportFileTextBox.Name = "exportFileTextBox";
            this.exportFileTextBox.Size = new System.Drawing.Size(369, 20);
            this.exportFileTextBox.TabIndex = 14;
            this.exportFileTextBox.TextChanged += new System.EventHandler(this.exportFileTextBox_TextChanged);
            // 
            // exportFileLabel
            // 
            this.exportFileLabel.Location = new System.Drawing.Point(9, 116);
            this.exportFileLabel.Name = "exportFileLabel";
            this.exportFileLabel.Size = new System.Drawing.Size(141, 23);
            this.exportFileLabel.TabIndex = 16;
            this.exportFileLabel.Text = "Output File";
            // 
            // browseExportFileButton
            // 
            this.browseExportFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseExportFileButton.Location = new System.Drawing.Point(387, 131);
            this.browseExportFileButton.Name = "browseExportFileButton";
            this.browseExportFileButton.Size = new System.Drawing.Size(24, 20);
            this.browseExportFileButton.TabIndex = 15;
            this.browseExportFileButton.Text = "...";
            this.browseExportFileButton.UseVisualStyleBackColor = true;
            this.browseExportFileButton.Click += new System.EventHandler(this.browseExportFileButton_Click);
            // 
            // createRelationsGroupBox
            // 
            this.createRelationsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createRelationsGroupBox.Controls.Add(this.tdmPackageTextBox);
            this.createRelationsGroupBox.Controls.Add(this.createRelationsButton);
            this.createRelationsGroupBox.Controls.Add(this.TDMPackageLabel);
            this.createRelationsGroupBox.Controls.Add(this.browseTDMPackage);
            this.createRelationsGroupBox.Location = new System.Drawing.Point(12, 229);
            this.createRelationsGroupBox.Name = "createRelationsGroupBox";
            this.createRelationsGroupBox.Size = new System.Drawing.Size(416, 95);
            this.createRelationsGroupBox.TabIndex = 23;
            this.createRelationsGroupBox.TabStop = false;
            this.createRelationsGroupBox.Text = "Create Relations in EA";
            // 
            // tdmPackageTextBox
            // 
            this.tdmPackageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tdmPackageTextBox.Location = new System.Drawing.Point(6, 35);
            this.tdmPackageTextBox.MinimumSize = new System.Drawing.Size(153, 20);
            this.tdmPackageTextBox.Name = "tdmPackageTextBox";
            this.tdmPackageTextBox.ReadOnly = true;
            this.tdmPackageTextBox.Size = new System.Drawing.Size(369, 20);
            this.tdmPackageTextBox.TabIndex = 14;
            // 
            // createRelationsButton
            // 
            this.createRelationsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.createRelationsButton.Location = new System.Drawing.Point(313, 66);
            this.createRelationsButton.Name = "createRelationsButton";
            this.createRelationsButton.Size = new System.Drawing.Size(97, 23);
            this.createRelationsButton.TabIndex = 20;
            this.createRelationsButton.Text = "Create Relations";
            this.createRelationsButton.UseVisualStyleBackColor = true;
            this.createRelationsButton.Click += new System.EventHandler(this.createRelationsButton_Click);
            // 
            // TDMPackageLabel
            // 
            this.TDMPackageLabel.Location = new System.Drawing.Point(3, 19);
            this.TDMPackageLabel.Name = "TDMPackageLabel";
            this.TDMPackageLabel.Size = new System.Drawing.Size(141, 23);
            this.TDMPackageLabel.TabIndex = 16;
            this.TDMPackageLabel.Text = "TDM Package";
            // 
            // browseTDMPackage
            // 
            this.browseTDMPackage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseTDMPackage.Location = new System.Drawing.Point(381, 34);
            this.browseTDMPackage.Name = "browseTDMPackage";
            this.browseTDMPackage.Size = new System.Drawing.Size(24, 20);
            this.browseTDMPackage.TabIndex = 15;
            this.browseTDMPackage.Text = "...";
            this.browseTDMPackage.UseVisualStyleBackColor = true;
            this.browseTDMPackage.Click += new System.EventHandler(this.browseTDMPackage_Click);
            // 
            // ErxImporterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 391);
            this.Controls.Add(this.exportFileTextBox);
            this.Controls.Add(this.createRelationsGroupBox);
            this.Controls.Add(this.exportFileLabel);
            this.Controls.Add(this.createForeignKeyGroupBox);
            this.Controls.Add(this.browseExportFileButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.errorTextBox);
            this.Controls.Add(this.importERXGroup);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(456, 430);
            this.Name = "ErxImporterForm";
            this.Text = "ERX Importer";
            this.importERXGroup.ResumeLayout(false);
            this.importERXGroup.PerformLayout();
            this.createForeignKeyGroupBox.ResumeLayout(false);
            this.createRelationsGroupBox.ResumeLayout(false);
            this.createRelationsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseImportFileButton;
        private System.Windows.Forms.TextBox importFileTextBox;
        private System.Windows.Forms.Label importFileLabel;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.RichTextBox errorTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button synchFKsButton;
        private System.Windows.Forms.GroupBox importERXGroup;
        private System.Windows.Forms.GroupBox createForeignKeyGroupBox;
        private System.Windows.Forms.TextBox exportFileTextBox;
        private System.Windows.Forms.Label exportFileLabel;
        private System.Windows.Forms.Button browseExportFileButton;
        private System.Windows.Forms.GroupBox createRelationsGroupBox;
        private System.Windows.Forms.TextBox tdmPackageTextBox;
        private System.Windows.Forms.Button createRelationsButton;
        private System.Windows.Forms.Label TDMPackageLabel;
        private System.Windows.Forms.Button browseTDMPackage;
    }
}

