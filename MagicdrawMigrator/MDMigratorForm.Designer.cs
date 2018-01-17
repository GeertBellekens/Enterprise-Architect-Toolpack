using System.Collections.Generic;
using System.Linq;
namespace MagicdrawMigrator
{
	partial class MDMigratorForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button startCorrectionButton;
		private System.Windows.Forms.Button browseDefaultPropertiesFileButton;
		private System.Windows.Forms.TextBox mdzipPathTextBox;
		private System.Windows.Forms.Label mdZipPathlabel;
		private System.Windows.Forms.Button browseMDZipFolder;
		
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
            this.startCorrectionButton = new System.Windows.Forms.Button();
            this.browseDefaultPropertiesFileButton = new System.Windows.Forms.Button();
            this.mdzipPathTextBox = new System.Windows.Forms.TextBox();
            this.mdZipPathlabel = new System.Windows.Forms.Label();
            this.browseMDZipFolder = new System.Windows.Forms.Button();
            this.browseImportPackageButton = new System.Windows.Forms.Button();
            this.importPackageTextBox = new System.Windows.Forms.TextBox();
            this.importPackageLabel = new System.Windows.Forms.Label();
            this.correctorsListView = new System.Windows.Forms.ListView();
            this.correctorsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startCorrectionButton
            // 
            this.startCorrectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startCorrectionButton.Location = new System.Drawing.Point(142, 401);
            this.startCorrectionButton.Name = "startCorrectionButton";
            this.startCorrectionButton.Size = new System.Drawing.Size(137, 31);
            this.startCorrectionButton.TabIndex = 0;
            this.startCorrectionButton.Text = "Start Corrections";
            this.startCorrectionButton.UseVisualStyleBackColor = true;
            this.startCorrectionButton.Click += new System.EventHandler(this.StartCorrectionButtonClick);
            // 
            // browseDefaultPropertiesFileButton
            // 
            this.browseDefaultPropertiesFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseDefaultPropertiesFileButton.Location = new System.Drawing.Point(311, 117);
            this.browseDefaultPropertiesFileButton.Name = "browseDefaultPropertiesFileButton";
            this.browseDefaultPropertiesFileButton.Size = new System.Drawing.Size(24, 20);
            this.browseDefaultPropertiesFileButton.TabIndex = 12;
            this.browseDefaultPropertiesFileButton.Text = "...";
            this.browseDefaultPropertiesFileButton.UseVisualStyleBackColor = true;
            // 
            // mdzipPathTextBox
            // 
            this.mdzipPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mdzipPathTextBox.Location = new System.Drawing.Point(12, 23);
            this.mdzipPathTextBox.MinimumSize = new System.Drawing.Size(153, 20);
            this.mdzipPathTextBox.Name = "mdzipPathTextBox";
            this.mdzipPathTextBox.Size = new System.Drawing.Size(237, 20);
            this.mdzipPathTextBox.TabIndex = 11;
            // 
            // mdZipPathlabel
            // 
            this.mdZipPathlabel.Location = new System.Drawing.Point(12, 9);
            this.mdZipPathlabel.Name = "mdZipPathlabel";
            this.mdZipPathlabel.Size = new System.Drawing.Size(141, 23);
            this.mdZipPathlabel.TabIndex = 13;
            this.mdZipPathlabel.Text = "MagicDraw MDzip Folder";
            // 
            // browseMDZipFolder
            // 
            this.browseMDZipFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseMDZipFolder.Location = new System.Drawing.Point(255, 22);
            this.browseMDZipFolder.Name = "browseMDZipFolder";
            this.browseMDZipFolder.Size = new System.Drawing.Size(24, 20);
            this.browseMDZipFolder.TabIndex = 14;
            this.browseMDZipFolder.Text = "...";
            this.browseMDZipFolder.UseVisualStyleBackColor = true;
            this.browseMDZipFolder.Click += new System.EventHandler(this.BrowseMDZipFolderClick);
            // 
            // browseImportPackageButton
            // 
            this.browseImportPackageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseImportPackageButton.Location = new System.Drawing.Point(255, 61);
            this.browseImportPackageButton.Name = "browseImportPackageButton";
            this.browseImportPackageButton.Size = new System.Drawing.Size(24, 20);
            this.browseImportPackageButton.TabIndex = 17;
            this.browseImportPackageButton.Text = "...";
            this.browseImportPackageButton.UseVisualStyleBackColor = true;
            this.browseImportPackageButton.Click += new System.EventHandler(this.browseImportPackageButton_Click);
            // 
            // importPackageTextBox
            // 
            this.importPackageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importPackageTextBox.Location = new System.Drawing.Point(12, 61);
            this.importPackageTextBox.MinimumSize = new System.Drawing.Size(153, 20);
            this.importPackageTextBox.Name = "importPackageTextBox";
            this.importPackageTextBox.ReadOnly = true;
            this.importPackageTextBox.Size = new System.Drawing.Size(237, 20);
            this.importPackageTextBox.TabIndex = 15;
            // 
            // importPackageLabel
            // 
            this.importPackageLabel.Location = new System.Drawing.Point(9, 46);
            this.importPackageLabel.Name = "importPackageLabel";
            this.importPackageLabel.Size = new System.Drawing.Size(141, 23);
            this.importPackageLabel.TabIndex = 16;
            this.importPackageLabel.Text = "EA Import Package";
            // 
            // correctorsListView
            // 
            this.correctorsListView.CheckBoxes = true;
            this.correctorsListView.Location = new System.Drawing.Point(15, 100);
            this.correctorsListView.Name = "correctorsListView";
            this.correctorsListView.Size = new System.Drawing.Size(264, 295);
            this.correctorsListView.TabIndex = 18;
            this.correctorsListView.UseCompatibleStateImageBehavior = false;
            this.correctorsListView.View = System.Windows.Forms.View.List;
            // 
            // correctorsLabel
            // 
            this.correctorsLabel.AutoSize = true;
            this.correctorsLabel.Location = new System.Drawing.Point(9, 84);
            this.correctorsLabel.Name = "correctorsLabel";
            this.correctorsLabel.Size = new System.Drawing.Size(113, 13);
            this.correctorsLabel.TabIndex = 19;
            this.correctorsLabel.Text = "Corrections to execute";
            // 
            // MDMigratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 444);
            this.Controls.Add(this.correctorsLabel);
            this.Controls.Add(this.correctorsListView);
            this.Controls.Add(this.browseImportPackageButton);
            this.Controls.Add(this.importPackageTextBox);
            this.Controls.Add(this.importPackageLabel);
            this.Controls.Add(this.browseMDZipFolder);
            this.Controls.Add(this.browseDefaultPropertiesFileButton);
            this.Controls.Add(this.mdzipPathTextBox);
            this.Controls.Add(this.mdZipPathlabel);
            this.Controls.Add(this.startCorrectionButton);
            this.Name = "MDMigratorForm";
            this.Text = "Magicdraw Migrator";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.Button browseImportPackageButton;
        private System.Windows.Forms.TextBox importPackageTextBox;
        private System.Windows.Forms.Label importPackageLabel;
        private System.Windows.Forms.ListView correctorsListView;
        private System.Windows.Forms.Label correctorsLabel;
    }
}
