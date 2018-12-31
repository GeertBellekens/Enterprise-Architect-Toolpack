namespace EAValidator
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.btnSelectQueryDirectory = new System.Windows.Forms.Button();
            this.txtDirectoryValidationChecks = new System.Windows.Forms.TextBox();
            this.checksPathLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.excludeArchivedPackagesCheckbox = new System.Windows.Forms.CheckBox();
            this.archivedPackagesQueryTextBox = new System.Windows.Forms.TextBox();
            this.archivedPackagesQueryLabel = new System.Windows.Forms.Label();
            this.allowedRepositoryTypesListBox = new System.Windows.Forms.CheckedListBox();
            this.allowedRepositoryTypesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSelectQueryDirectory
            // 
            this.btnSelectQueryDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectQueryDirectory.Location = new System.Drawing.Point(332, 23);
            this.btnSelectQueryDirectory.Name = "btnSelectQueryDirectory";
            this.btnSelectQueryDirectory.Size = new System.Drawing.Size(25, 23);
            this.btnSelectQueryDirectory.TabIndex = 13;
            this.btnSelectQueryDirectory.Text = "...";
            this.btnSelectQueryDirectory.UseVisualStyleBackColor = true;
            this.btnSelectQueryDirectory.Click += new System.EventHandler(this.btnSelectQueryDirectory_Click);
            // 
            // txtDirectoryValidationChecks
            // 
            this.txtDirectoryValidationChecks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirectoryValidationChecks.Location = new System.Drawing.Point(12, 25);
            this.txtDirectoryValidationChecks.Name = "txtDirectoryValidationChecks";
            this.txtDirectoryValidationChecks.Size = new System.Drawing.Size(314, 20);
            this.txtDirectoryValidationChecks.TabIndex = 12;
            // 
            // checksPathLabel
            // 
            this.checksPathLabel.AutoSize = true;
            this.checksPathLabel.Location = new System.Drawing.Point(12, 9);
            this.checksPathLabel.Name = "checksPathLabel";
            this.checksPathLabel.Size = new System.Drawing.Size(86, 13);
            this.checksPathLabel.TabIndex = 14;
            this.checksPathLabel.Text = "Checks directory";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(201, 300);
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
            this.cancelButton.Location = new System.Drawing.Point(282, 300);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // excludeArchivedPackagesCheckbox
            // 
            this.excludeArchivedPackagesCheckbox.AutoSize = true;
            this.excludeArchivedPackagesCheckbox.Location = new System.Drawing.Point(12, 56);
            this.excludeArchivedPackagesCheckbox.Name = "excludeArchivedPackagesCheckbox";
            this.excludeArchivedPackagesCheckbox.Size = new System.Drawing.Size(152, 17);
            this.excludeArchivedPackagesCheckbox.TabIndex = 17;
            this.excludeArchivedPackagesCheckbox.Text = "Exclude archive packages";
            this.excludeArchivedPackagesCheckbox.UseVisualStyleBackColor = true;
            this.excludeArchivedPackagesCheckbox.CheckedChanged += new System.EventHandler(this.excludeArchivedPackagesCheckbox_CheckedChanged);
            // 
            // archivedPackagesQueryTextBox
            // 
            this.archivedPackagesQueryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.archivedPackagesQueryTextBox.Location = new System.Drawing.Point(12, 194);
            this.archivedPackagesQueryTextBox.Multiline = true;
            this.archivedPackagesQueryTextBox.Name = "archivedPackagesQueryTextBox";
            this.archivedPackagesQueryTextBox.Size = new System.Drawing.Size(345, 100);
            this.archivedPackagesQueryTextBox.TabIndex = 18;
            // 
            // archivedPackagesQueryLabel
            // 
            this.archivedPackagesQueryLabel.AutoSize = true;
            this.archivedPackagesQueryLabel.Location = new System.Drawing.Point(12, 178);
            this.archivedPackagesQueryLabel.Name = "archivedPackagesQueryLabel";
            this.archivedPackagesQueryLabel.Size = new System.Drawing.Size(122, 13);
            this.archivedPackagesQueryLabel.TabIndex = 19;
            this.archivedPackagesQueryLabel.Text = "Archive packages query";
            // 
            // allowedRepositoryTypesListBox
            // 
            this.allowedRepositoryTypesListBox.FormattingEnabled = true;
            this.allowedRepositoryTypesListBox.Location = new System.Drawing.Point(12, 100);
            this.allowedRepositoryTypesListBox.Name = "allowedRepositoryTypesListBox";
            this.allowedRepositoryTypesListBox.Size = new System.Drawing.Size(153, 64);
            this.allowedRepositoryTypesListBox.TabIndex = 20;
            // 
            // allowedRepositoryTypesLabel
            // 
            this.allowedRepositoryTypesLabel.AutoSize = true;
            this.allowedRepositoryTypesLabel.Location = new System.Drawing.Point(12, 84);
            this.allowedRepositoryTypesLabel.Name = "allowedRepositoryTypesLabel";
            this.allowedRepositoryTypesLabel.Size = new System.Drawing.Size(120, 13);
            this.allowedRepositoryTypesLabel.TabIndex = 21;
            this.allowedRepositoryTypesLabel.Text = "Allowed repository types";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(369, 335);
            this.Controls.Add(this.allowedRepositoryTypesListBox);
            this.Controls.Add(this.archivedPackagesQueryTextBox);
            this.Controls.Add(this.excludeArchivedPackagesCheckbox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.btnSelectQueryDirectory);
            this.Controls.Add(this.txtDirectoryValidationChecks);
            this.Controls.Add(this.archivedPackagesQueryLabel);
            this.Controls.Add(this.checksPathLabel);
            this.Controls.Add(this.allowedRepositoryTypesLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(370, 281);
            this.Name = "SettingsForm";
            this.Text = "EA Validator Settings";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectQueryDirectory;
        private System.Windows.Forms.TextBox txtDirectoryValidationChecks;
        private System.Windows.Forms.Label checksPathLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox excludeArchivedPackagesCheckbox;
        private System.Windows.Forms.TextBox archivedPackagesQueryTextBox;
        private System.Windows.Forms.Label archivedPackagesQueryLabel;
        private System.Windows.Forms.CheckedListBox allowedRepositoryTypesListBox;
        private System.Windows.Forms.Label allowedRepositoryTypesLabel;
    }
}