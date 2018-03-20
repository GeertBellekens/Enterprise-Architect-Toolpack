/*
 * User: wij
 * Date: 25/02/2011 4:52
 */
namespace EAValidator
{
    partial class AboutWindow
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
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
            this.ApplicationTitle = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.Version = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.AssemblyDate = new System.Windows.Forms.Label();
            this.AuthorName = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ApplicationTitle
            // 
            this.ApplicationTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplicationTitle.Location = new System.Drawing.Point(17, 14);
            this.ApplicationTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ApplicationTitle.Name = "ApplicationTitle";
            this.ApplicationTitle.Size = new System.Drawing.Size(237, 28);
            this.ApplicationTitle.TabIndex = 0;
            this.ApplicationTitle.Text = "EA Validator";
            // 
            // VersionLabel
            // 
            this.VersionLabel.Location = new System.Drawing.Point(17, 95);
            this.VersionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(133, 28);
            this.VersionLabel.TabIndex = 1;
            this.VersionLabel.Text = "Version:";
            // 
            // Version
            // 
            this.Version.Location = new System.Drawing.Point(95, 95);
            this.Version.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(133, 28);
            this.Version.TabIndex = 2;
            this.Version.Text = "assemblyVersion";
            // 
            // DateLabel
            // 
            this.DateLabel.Location = new System.Drawing.Point(17, 111);
            this.DateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(133, 28);
            this.DateLabel.TabIndex = 3;
            this.DateLabel.Text = "Date:";
            // 
            // AssemblyDate
            // 
            this.AssemblyDate.Location = new System.Drawing.Point(95, 111);
            this.AssemblyDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AssemblyDate.Name = "AssemblyDate";
            this.AssemblyDate.Size = new System.Drawing.Size(133, 28);
            this.AssemblyDate.TabIndex = 4;
            this.AssemblyDate.Text = "AssemblyDate";
            // 
            // AuthorName
            // 
            this.AuthorName.Location = new System.Drawing.Point(17, 47);
            this.AuthorName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AuthorName.Name = "AuthorName";
            this.AuthorName.Size = new System.Drawing.Size(245, 28);
            this.AuthorName.TabIndex = 6;
            this.AuthorName.Text = "© 2018 Alain Van Goethem";
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.AutoSize = true;
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(249, 193);
            this.OKButton.Margin = new System.Windows.Forms.Padding(4);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(100, 28);
            this.OKButton.TabIndex = 9;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButtonClick);
            // 
            // AboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 236);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.AuthorName);
            this.Controls.Add(this.AssemblyDate);
            this.Controls.Add(this.DateLabel);
            this.Controls.Add(this.Version);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.ApplicationTitle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "About EA Validator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label AuthorName;
        private System.Windows.Forms.Label AssemblyDate;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.Label Version;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label ApplicationTitle;
    }
}
