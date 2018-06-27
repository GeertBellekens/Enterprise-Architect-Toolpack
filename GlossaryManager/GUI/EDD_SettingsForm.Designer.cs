namespace GlossaryManager.GUI
{
    partial class EDD_SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EDD_SettingsForm));
            this.browseBusinessItemsPackageButton = new System.Windows.Forms.Button();
            this.businessItemsPackageTextBox = new System.Windows.Forms.TextBox();
            this.BusinessItemsPackageLabel = new System.Windows.Forms.Label();
            this.browseDataItemsPackageButton = new System.Windows.Forms.Button();
            this.dataItemsPackageTextBox = new System.Windows.Forms.TextBox();
            this.dataItemsPackageLabel = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.showWindowCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // browseBusinessItemsPackageButton
            // 
            this.browseBusinessItemsPackageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseBusinessItemsPackageButton.Location = new System.Drawing.Point(323, 14);
            this.browseBusinessItemsPackageButton.Name = "browseBusinessItemsPackageButton";
            this.browseBusinessItemsPackageButton.Size = new System.Drawing.Size(24, 20);
            this.browseBusinessItemsPackageButton.TabIndex = 12;
            this.browseBusinessItemsPackageButton.Text = "...";
            this.browseBusinessItemsPackageButton.UseVisualStyleBackColor = true;
            this.browseBusinessItemsPackageButton.Click += new System.EventHandler(this.browseBusinessItemsPackageButton_Click);
            // 
            // businessItemsPackageTextBox
            // 
            this.businessItemsPackageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.businessItemsPackageTextBox.Location = new System.Drawing.Point(145, 15);
            this.businessItemsPackageTextBox.MinimumSize = new System.Drawing.Size(153, 20);
            this.businessItemsPackageTextBox.Name = "businessItemsPackageTextBox";
            this.businessItemsPackageTextBox.ReadOnly = true;
            this.businessItemsPackageTextBox.Size = new System.Drawing.Size(172, 20);
            this.businessItemsPackageTextBox.TabIndex = 11;
            // 
            // BusinessItemsPackageLabel
            // 
            this.BusinessItemsPackageLabel.Location = new System.Drawing.Point(12, 18);
            this.BusinessItemsPackageLabel.Name = "BusinessItemsPackageLabel";
            this.BusinessItemsPackageLabel.Size = new System.Drawing.Size(141, 23);
            this.BusinessItemsPackageLabel.TabIndex = 13;
            this.BusinessItemsPackageLabel.Text = "Business Items Package";
            // 
            // browseDataItemsPackageButton
            // 
            this.browseDataItemsPackageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseDataItemsPackageButton.Location = new System.Drawing.Point(323, 41);
            this.browseDataItemsPackageButton.Name = "browseDataItemsPackageButton";
            this.browseDataItemsPackageButton.Size = new System.Drawing.Size(24, 20);
            this.browseDataItemsPackageButton.TabIndex = 15;
            this.browseDataItemsPackageButton.Text = "...";
            this.browseDataItemsPackageButton.UseVisualStyleBackColor = true;
            this.browseDataItemsPackageButton.Click += new System.EventHandler(this.browseDataItemsPackageButton_Click);
            // 
            // dataItemsPackageTextBox
            // 
            this.dataItemsPackageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataItemsPackageTextBox.Location = new System.Drawing.Point(145, 42);
            this.dataItemsPackageTextBox.MinimumSize = new System.Drawing.Size(153, 20);
            this.dataItemsPackageTextBox.Name = "dataItemsPackageTextBox";
            this.dataItemsPackageTextBox.ReadOnly = true;
            this.dataItemsPackageTextBox.Size = new System.Drawing.Size(172, 20);
            this.dataItemsPackageTextBox.TabIndex = 14;
            // 
            // dataItemsPackageLabel
            // 
            this.dataItemsPackageLabel.Location = new System.Drawing.Point(12, 45);
            this.dataItemsPackageLabel.Name = "dataItemsPackageLabel";
            this.dataItemsPackageLabel.Size = new System.Drawing.Size(141, 23);
            this.dataItemsPackageLabel.TabIndex = 16;
            this.dataItemsPackageLabel.Text = "Data Items Package";
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(272, 112);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 19;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(191, 112);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 18;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(110, 112);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 17;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // showWindowCheckbox
            // 
            this.showWindowCheckbox.AutoSize = true;
            this.showWindowCheckbox.Checked = true;
            this.showWindowCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showWindowCheckbox.Location = new System.Drawing.Point(15, 72);
            this.showWindowCheckbox.Name = "showWindowCheckbox";
            this.showWindowCheckbox.Size = new System.Drawing.Size(142, 17);
            this.showWindowCheckbox.TabIndex = 20;
            this.showWindowCheckbox.Text = "Show window at start-up";
            this.showWindowCheckbox.UseVisualStyleBackColor = true;
            // 
            // EDD_SettingsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(359, 147);
            this.Controls.Add(this.showWindowCheckbox);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.browseDataItemsPackageButton);
            this.Controls.Add(this.dataItemsPackageTextBox);
            this.Controls.Add(this.dataItemsPackageLabel);
            this.Controls.Add(this.browseBusinessItemsPackageButton);
            this.Controls.Add(this.businessItemsPackageTextBox);
            this.Controls.Add(this.BusinessItemsPackageLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(375, 148);
            this.Name = "EDD_SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EDD Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseBusinessItemsPackageButton;
        private System.Windows.Forms.TextBox businessItemsPackageTextBox;
        private System.Windows.Forms.Label BusinessItemsPackageLabel;
        private System.Windows.Forms.Button browseDataItemsPackageButton;
        private System.Windows.Forms.TextBox dataItemsPackageTextBox;
        private System.Windows.Forms.Label dataItemsPackageLabel;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.CheckBox showWindowCheckbox;
    }
}