namespace EAValidator
{
    partial class CheckEditorForm
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
            this.idLabel = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.rationaleTextBox = new System.Windows.Forms.TextBox();
            this.rationaleLabel = new System.Windows.Forms.Label();
            this.proposedSolutionTextBox = new System.Windows.Forms.TextBox();
            this.solutionLabel = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.helpUrlTextBox = new System.Windows.Forms.TextBox();
            this.helpURLLabel = new System.Windows.Forms.Label();
            this.checkTabControl = new System.Windows.Forms.TabControl();
            this.mainTab = new System.Windows.Forms.TabPage();
            this.invalidElementsLabel = new System.Windows.Forms.Label();
            this.invalidElementsTextBox = new System.Windows.Forms.TextBox();
            this.selectElementsQueryLabel = new System.Windows.Forms.Label();
            this.elementsToCheckTextBox = new System.Windows.Forms.TextBox();
            this.filtersTab = new System.Windows.Forms.TabPage();
            this.diagramFilterLabel = new System.Windows.Forms.Label();
            this.diagramFilterTextBox = new System.Windows.Forms.TextBox();
            this.releaseFilterLabel = new System.Windows.Forms.Label();
            this.releaseFilterTextBox = new System.Windows.Forms.TextBox();
            this.changeFilterLabel = new System.Windows.Forms.Label();
            this.changeFilterTextBox = new System.Windows.Forms.TextBox();
            this.packageFilterLabel = new System.Windows.Forms.Label();
            this.packageFilterTextBox = new System.Windows.Forms.TextBox();
            this.resolveTab = new System.Windows.Forms.TabPage();
            this.resolveCodeTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.languageLabel = new System.Windows.Forms.Label();
            this.checkTabControl.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.filtersTab.SuspendLayout();
            this.resolveTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idLabel.Location = new System.Drawing.Point(16, 17);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(18, 13);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "ID";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(16, 34);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 1;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(16, 80);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(572, 20);
            this.descriptionTextBox.TabIndex = 3;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(16, 63);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.descriptionLabel.TabIndex = 2;
            this.descriptionLabel.Text = "Description";
            // 
            // rationaleTextBox
            // 
            this.rationaleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rationaleTextBox.Location = new System.Drawing.Point(16, 126);
            this.rationaleTextBox.Name = "rationaleTextBox";
            this.rationaleTextBox.Size = new System.Drawing.Size(572, 20);
            this.rationaleTextBox.TabIndex = 5;
            // 
            // rationaleLabel
            // 
            this.rationaleLabel.AutoSize = true;
            this.rationaleLabel.Location = new System.Drawing.Point(16, 109);
            this.rationaleLabel.Name = "rationaleLabel";
            this.rationaleLabel.Size = new System.Drawing.Size(52, 13);
            this.rationaleLabel.TabIndex = 4;
            this.rationaleLabel.Text = "Rationale";
            // 
            // proposedSolutionTextBox
            // 
            this.proposedSolutionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.proposedSolutionTextBox.Location = new System.Drawing.Point(16, 171);
            this.proposedSolutionTextBox.Name = "proposedSolutionTextBox";
            this.proposedSolutionTextBox.Size = new System.Drawing.Size(572, 20);
            this.proposedSolutionTextBox.TabIndex = 7;
            // 
            // solutionLabel
            // 
            this.solutionLabel.AutoSize = true;
            this.solutionLabel.Location = new System.Drawing.Point(16, 154);
            this.solutionLabel.Name = "solutionLabel";
            this.solutionLabel.Size = new System.Drawing.Size(91, 13);
            this.solutionLabel.TabIndex = 6;
            this.solutionLabel.Text = "Proposed solution";
            // 
            // typeComboBox
            // 
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "Error",
            "Warning"});
            this.typeComboBox.Location = new System.Drawing.Point(122, 34);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(121, 21);
            this.typeComboBox.TabIndex = 8;
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeLabel.Location = new System.Drawing.Point(119, 17);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(31, 13);
            this.typeLabel.TabIndex = 9;
            this.typeLabel.Text = "Type";
            // 
            // helpUrlTextBox
            // 
            this.helpUrlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.helpUrlTextBox.Location = new System.Drawing.Point(16, 213);
            this.helpUrlTextBox.Name = "helpUrlTextBox";
            this.helpUrlTextBox.Size = new System.Drawing.Size(572, 20);
            this.helpUrlTextBox.TabIndex = 11;
            // 
            // helpURLLabel
            // 
            this.helpURLLabel.AutoSize = true;
            this.helpURLLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpURLLabel.Location = new System.Drawing.Point(16, 196);
            this.helpURLLabel.Name = "helpURLLabel";
            this.helpURLLabel.Size = new System.Drawing.Size(54, 13);
            this.helpURLLabel.TabIndex = 10;
            this.helpURLLabel.Text = "Help URL";
            // 
            // checkTabControl
            // 
            this.checkTabControl.Controls.Add(this.mainTab);
            this.checkTabControl.Controls.Add(this.filtersTab);
            this.checkTabControl.Controls.Add(this.resolveTab);
            this.checkTabControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkTabControl.Location = new System.Drawing.Point(0, 0);
            this.checkTabControl.Name = "checkTabControl";
            this.checkTabControl.SelectedIndex = 0;
            this.checkTabControl.Size = new System.Drawing.Size(608, 976);
            this.checkTabControl.TabIndex = 14;
            // 
            // mainTab
            // 
            this.mainTab.BackColor = System.Drawing.SystemColors.Control;
            this.mainTab.Controls.Add(this.invalidElementsLabel);
            this.mainTab.Controls.Add(this.invalidElementsTextBox);
            this.mainTab.Controls.Add(this.selectElementsQueryLabel);
            this.mainTab.Controls.Add(this.elementsToCheckTextBox);
            this.mainTab.Controls.Add(this.helpURLLabel);
            this.mainTab.Controls.Add(this.idLabel);
            this.mainTab.Controls.Add(this.idTextBox);
            this.mainTab.Controls.Add(this.helpUrlTextBox);
            this.mainTab.Controls.Add(this.descriptionLabel);
            this.mainTab.Controls.Add(this.descriptionTextBox);
            this.mainTab.Controls.Add(this.typeLabel);
            this.mainTab.Controls.Add(this.rationaleLabel);
            this.mainTab.Controls.Add(this.typeComboBox);
            this.mainTab.Controls.Add(this.rationaleTextBox);
            this.mainTab.Controls.Add(this.proposedSolutionTextBox);
            this.mainTab.Controls.Add(this.solutionLabel);
            this.mainTab.Location = new System.Drawing.Point(4, 22);
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Windows.Forms.Padding(3);
            this.mainTab.Size = new System.Drawing.Size(600, 950);
            this.mainTab.TabIndex = 0;
            this.mainTab.Text = "Main";
            // 
            // invalidElementsLabel
            // 
            this.invalidElementsLabel.AutoSize = true;
            this.invalidElementsLabel.Location = new System.Drawing.Point(16, 565);
            this.invalidElementsLabel.Name = "invalidElementsLabel";
            this.invalidElementsLabel.Size = new System.Drawing.Size(112, 13);
            this.invalidElementsLabel.TabIndex = 14;
            this.invalidElementsLabel.Text = "Invalid elements query";
            // 
            // invalidElementsTextBox
            // 
            this.invalidElementsTextBox.AcceptsReturn = true;
            this.invalidElementsTextBox.AcceptsTab = true;
            this.invalidElementsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.invalidElementsTextBox.Location = new System.Drawing.Point(16, 582);
            this.invalidElementsTextBox.Multiline = true;
            this.invalidElementsTextBox.Name = "invalidElementsTextBox";
            this.invalidElementsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.invalidElementsTextBox.Size = new System.Drawing.Size(572, 350);
            this.invalidElementsTextBox.TabIndex = 15;
            this.invalidElementsTextBox.TextChanged += new System.EventHandler(this.invalidElementsTextBox_TextChanged);
            // 
            // selectElementsQueryLabel
            // 
            this.selectElementsQueryLabel.AutoSize = true;
            this.selectElementsQueryLabel.Location = new System.Drawing.Point(16, 241);
            this.selectElementsQueryLabel.Name = "selectElementsQueryLabel";
            this.selectElementsQueryLabel.Size = new System.Drawing.Size(124, 13);
            this.selectElementsQueryLabel.TabIndex = 12;
            this.selectElementsQueryLabel.Text = "Elements to check query";
            // 
            // elementsToCheckTextBox
            // 
            this.elementsToCheckTextBox.AcceptsReturn = true;
            this.elementsToCheckTextBox.AcceptsTab = true;
            this.elementsToCheckTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementsToCheckTextBox.Location = new System.Drawing.Point(16, 258);
            this.elementsToCheckTextBox.Multiline = true;
            this.elementsToCheckTextBox.Name = "elementsToCheckTextBox";
            this.elementsToCheckTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.elementsToCheckTextBox.Size = new System.Drawing.Size(572, 297);
            this.elementsToCheckTextBox.TabIndex = 13;
            this.elementsToCheckTextBox.TextChanged += new System.EventHandler(this.elementsToCheckTextBox_TextChanged);
            // 
            // filtersTab
            // 
            this.filtersTab.BackColor = System.Drawing.SystemColors.Control;
            this.filtersTab.Controls.Add(this.diagramFilterLabel);
            this.filtersTab.Controls.Add(this.diagramFilterTextBox);
            this.filtersTab.Controls.Add(this.releaseFilterLabel);
            this.filtersTab.Controls.Add(this.releaseFilterTextBox);
            this.filtersTab.Controls.Add(this.changeFilterLabel);
            this.filtersTab.Controls.Add(this.changeFilterTextBox);
            this.filtersTab.Controls.Add(this.packageFilterLabel);
            this.filtersTab.Controls.Add(this.packageFilterTextBox);
            this.filtersTab.Location = new System.Drawing.Point(4, 22);
            this.filtersTab.Name = "filtersTab";
            this.filtersTab.Padding = new System.Windows.Forms.Padding(3);
            this.filtersTab.Size = new System.Drawing.Size(600, 950);
            this.filtersTab.TabIndex = 1;
            this.filtersTab.Text = "Filters";
            // 
            // diagramFilterLabel
            // 
            this.diagramFilterLabel.AutoSize = true;
            this.diagramFilterLabel.Location = new System.Drawing.Point(5, 594);
            this.diagramFilterLabel.Name = "diagramFilterLabel";
            this.diagramFilterLabel.Size = new System.Drawing.Size(71, 13);
            this.diagramFilterLabel.TabIndex = 20;
            this.diagramFilterLabel.Text = "Diagram Filter";
            // 
            // diagramFilterTextBox
            // 
            this.diagramFilterTextBox.AcceptsReturn = true;
            this.diagramFilterTextBox.AcceptsTab = true;
            this.diagramFilterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.diagramFilterTextBox.Location = new System.Drawing.Point(8, 611);
            this.diagramFilterTextBox.Multiline = true;
            this.diagramFilterTextBox.Name = "diagramFilterTextBox";
            this.diagramFilterTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.diagramFilterTextBox.Size = new System.Drawing.Size(572, 200);
            this.diagramFilterTextBox.TabIndex = 21;
            this.diagramFilterTextBox.TextChanged += new System.EventHandler(this.diagramFilterTextBox_TextChanged);
            // 
            // releaseFilterLabel
            // 
            this.releaseFilterLabel.AutoSize = true;
            this.releaseFilterLabel.Location = new System.Drawing.Point(5, 363);
            this.releaseFilterLabel.Name = "releaseFilterLabel";
            this.releaseFilterLabel.Size = new System.Drawing.Size(71, 13);
            this.releaseFilterLabel.TabIndex = 18;
            this.releaseFilterLabel.Text = "Release Filter";
            // 
            // releaseFilterTextBox
            // 
            this.releaseFilterTextBox.AcceptsReturn = true;
            this.releaseFilterTextBox.AcceptsTab = true;
            this.releaseFilterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.releaseFilterTextBox.Location = new System.Drawing.Point(8, 380);
            this.releaseFilterTextBox.Multiline = true;
            this.releaseFilterTextBox.Name = "releaseFilterTextBox";
            this.releaseFilterTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.releaseFilterTextBox.Size = new System.Drawing.Size(572, 200);
            this.releaseFilterTextBox.TabIndex = 19;
            this.releaseFilterTextBox.TextChanged += new System.EventHandler(this.releaseFilterTextBox_TextChanged);
            // 
            // changeFilterLabel
            // 
            this.changeFilterLabel.AutoSize = true;
            this.changeFilterLabel.Location = new System.Drawing.Point(5, 133);
            this.changeFilterLabel.Name = "changeFilterLabel";
            this.changeFilterLabel.Size = new System.Drawing.Size(69, 13);
            this.changeFilterLabel.TabIndex = 16;
            this.changeFilterLabel.Text = "Change Filter";
            // 
            // changeFilterTextBox
            // 
            this.changeFilterTextBox.AcceptsReturn = true;
            this.changeFilterTextBox.AcceptsTab = true;
            this.changeFilterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.changeFilterTextBox.Location = new System.Drawing.Point(8, 150);
            this.changeFilterTextBox.Multiline = true;
            this.changeFilterTextBox.Name = "changeFilterTextBox";
            this.changeFilterTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.changeFilterTextBox.Size = new System.Drawing.Size(572, 200);
            this.changeFilterTextBox.TabIndex = 17;
            this.changeFilterTextBox.TextChanged += new System.EventHandler(this.changeFilterTextBox_TextChanged);
            // 
            // packageFilterLabel
            // 
            this.packageFilterLabel.AutoSize = true;
            this.packageFilterLabel.Location = new System.Drawing.Point(5, 16);
            this.packageFilterLabel.Name = "packageFilterLabel";
            this.packageFilterLabel.Size = new System.Drawing.Size(75, 13);
            this.packageFilterLabel.TabIndex = 14;
            this.packageFilterLabel.Text = "Package Filter";
            // 
            // packageFilterTextBox
            // 
            this.packageFilterTextBox.AcceptsReturn = true;
            this.packageFilterTextBox.AcceptsTab = true;
            this.packageFilterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.packageFilterTextBox.Location = new System.Drawing.Point(8, 33);
            this.packageFilterTextBox.Multiline = true;
            this.packageFilterTextBox.Name = "packageFilterTextBox";
            this.packageFilterTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.packageFilterTextBox.Size = new System.Drawing.Size(572, 88);
            this.packageFilterTextBox.TabIndex = 15;
            this.packageFilterTextBox.TextChanged += new System.EventHandler(this.packageFilterTextBox_TextChanged);
            // 
            // resolveTab
            // 
            this.resolveTab.BackColor = System.Drawing.SystemColors.Control;
            this.resolveTab.Controls.Add(this.languageLabel);
            this.resolveTab.Controls.Add(this.languageComboBox);
            this.resolveTab.Controls.Add(this.resolveCodeTextBox);
            this.resolveTab.Location = new System.Drawing.Point(4, 22);
            this.resolveTab.Name = "resolveTab";
            this.resolveTab.Padding = new System.Windows.Forms.Padding(3);
            this.resolveTab.Size = new System.Drawing.Size(600, 950);
            this.resolveTab.TabIndex = 2;
            this.resolveTab.Text = "Resolve Code";
            // 
            // resolveCodeTextBox
            // 
            this.resolveCodeTextBox.AcceptsReturn = true;
            this.resolveCodeTextBox.AcceptsTab = true;
            this.resolveCodeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resolveCodeTextBox.Location = new System.Drawing.Point(3, 33);
            this.resolveCodeTextBox.Multiline = true;
            this.resolveCodeTextBox.Name = "resolveCodeTextBox";
            this.resolveCodeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.resolveCodeTextBox.Size = new System.Drawing.Size(594, 914);
            this.resolveCodeTextBox.TabIndex = 23;
            this.resolveCodeTextBox.TextChanged += new System.EventHandler(this.resolveCodeTextBox_TextChanged);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(440, 982);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 15;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(521, 982);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // languageComboBox
            // 
            this.languageComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.languageComboBox.FormattingEnabled = true;
            this.languageComboBox.Items.AddRange(new object[] {
            "VBScript",
            "JavaScript",
            "JScript"});
            this.languageComboBox.Location = new System.Drawing.Point(471, 6);
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(121, 21);
            this.languageComboBox.TabIndex = 24;
            // 
            // languageLabel
            // 
            this.languageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.languageLabel.AutoSize = true;
            this.languageLabel.Location = new System.Drawing.Point(401, 9);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(55, 13);
            this.languageLabel.TabIndex = 25;
            this.languageLabel.Text = "Language";
            // 
            // CheckEditorForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(608, 1017);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.checkTabControl);
            this.MinimumSize = new System.Drawing.Size(250, 1056);
            this.Name = "CheckEditorForm";
            this.Text = "Check Editor";
            this.checkTabControl.ResumeLayout(false);
            this.mainTab.ResumeLayout(false);
            this.mainTab.PerformLayout();
            this.filtersTab.ResumeLayout(false);
            this.filtersTab.PerformLayout();
            this.resolveTab.ResumeLayout(false);
            this.resolveTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TextBox rationaleTextBox;
        private System.Windows.Forms.Label rationaleLabel;
        private System.Windows.Forms.TextBox proposedSolutionTextBox;
        private System.Windows.Forms.Label solutionLabel;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.TextBox helpUrlTextBox;
        private System.Windows.Forms.Label helpURLLabel;
        private System.Windows.Forms.TabControl checkTabControl;
        private System.Windows.Forms.TabPage mainTab;
        private System.Windows.Forms.Label selectElementsQueryLabel;
        private System.Windows.Forms.TextBox elementsToCheckTextBox;
        private System.Windows.Forms.TabPage filtersTab;
        private System.Windows.Forms.TabPage resolveTab;
        private System.Windows.Forms.Label invalidElementsLabel;
        private System.Windows.Forms.TextBox invalidElementsTextBox;
        private System.Windows.Forms.Label diagramFilterLabel;
        private System.Windows.Forms.TextBox diagramFilterTextBox;
        private System.Windows.Forms.Label releaseFilterLabel;
        private System.Windows.Forms.TextBox releaseFilterTextBox;
        private System.Windows.Forms.Label changeFilterLabel;
        private System.Windows.Forms.TextBox changeFilterTextBox;
        private System.Windows.Forms.Label packageFilterLabel;
        private System.Windows.Forms.TextBox packageFilterTextBox;
        private System.Windows.Forms.TextBox resolveCodeTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.ComboBox languageComboBox;
    }
}