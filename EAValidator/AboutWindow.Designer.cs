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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
            this.ApplicationTitle = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.Version = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.AssemblyDate = new System.Windows.Forms.Label();
            this.AuthorName = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.HomePage = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.AuthorEmail = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ApplicationTitle
            // 
            this.ApplicationTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplicationTitle.Location = new System.Drawing.Point(13, 11);
            this.ApplicationTitle.Name = "ApplicationTitle";
            this.ApplicationTitle.Size = new System.Drawing.Size(178, 23);
            this.ApplicationTitle.TabIndex = 0;
            this.ApplicationTitle.Text = "EA Validator";
            // 
            // VersionLabel
            // 
            this.VersionLabel.Location = new System.Drawing.Point(13, 101);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(100, 23);
            this.VersionLabel.TabIndex = 1;
            this.VersionLabel.Text = "Version:";
            // 
            // Version
            // 
            this.Version.Location = new System.Drawing.Point(71, 101);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(100, 23);
            this.Version.TabIndex = 2;
            this.Version.Text = "assemblyVersion";
            // 
            // DateLabel
            // 
            this.DateLabel.Location = new System.Drawing.Point(13, 114);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(100, 23);
            this.DateLabel.TabIndex = 3;
            this.DateLabel.Text = "Date:";
            // 
            // AssemblyDate
            // 
            this.AssemblyDate.Location = new System.Drawing.Point(71, 114);
            this.AssemblyDate.Name = "AssemblyDate";
            this.AssemblyDate.Size = new System.Drawing.Size(100, 23);
            this.AssemblyDate.TabIndex = 4;
            this.AssemblyDate.Text = "AssemblyDate";
            // 
            // AuthorName
            // 
            this.AuthorName.Location = new System.Drawing.Point(13, 38);
            this.AuthorName.Name = "AuthorName";
            this.AuthorName.Size = new System.Drawing.Size(137, 17);
            this.AuthorName.TabIndex = 6;
            this.AuthorName.Text = "© 2018 Alain Van Goethem";
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.AutoSize = true;
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(213, 144);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 9;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButtonClick);
            // 
            // logoBox
            // 
            this.logoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logoBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.logoBox.ErrorImage = global::EAValidator.Properties.Resources.EA_Validator_Logo;
            this.logoBox.Image = global::EAValidator.Properties.Resources.EA_Validator_Logo;
            this.logoBox.InitialImage = global::EAValidator.Properties.Resources.EA_Validator_Logo;
            this.logoBox.Location = new System.Drawing.Point(191, 24);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(97, 89);
            this.logoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoBox.TabIndex = 14;
            this.logoBox.TabStop = false;
            // 
            // HomePage
            // 
            this.HomePage.Location = new System.Drawing.Point(14, 140);
            this.HomePage.Name = "HomePage";
            this.HomePage.Size = new System.Drawing.Size(157, 23);
            this.HomePage.TabIndex = 15;
            this.HomePage.TabStop = true;
            this.HomePage.Text = "EA Validator homepage";
            this.HomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HomePage_LinkClicked);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "© 2018 Bellekens";
            // 
            // AuthorEmail
            // 
            this.AuthorEmail.Location = new System.Drawing.Point(14, 72);
            this.AuthorEmail.Name = "AuthorEmail";
            this.AuthorEmail.Size = new System.Drawing.Size(127, 23);
            this.AuthorEmail.TabIndex = 17;
            this.AuthorEmail.TabStop = true;
            this.AuthorEmail.Text = "geert@bellekens.com";
            this.AuthorEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AuthorEmail_LinkClicked);
            // 
            // AboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 179);
            this.Controls.Add(this.AuthorEmail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HomePage);
            this.Controls.Add(this.logoBox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.AuthorName);
            this.Controls.Add(this.AssemblyDate);
            this.Controls.Add(this.DateLabel);
            this.Controls.Add(this.Version);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.ApplicationTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "About EA Validator";
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
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
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.LinkLabel HomePage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel AuthorEmail;
    }
}
