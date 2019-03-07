namespace EAAddinRegisterer
{
    partial class AddinRegisterForm
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
            this.browseAddinFile = new System.Windows.Forms.Button();
            this.addinFileTextBox = new System.Windows.Forms.TextBox();
            this.addinFileLabel = new System.Windows.Forms.Label();
            this.registerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // browseAddinFile
            // 
            this.browseAddinFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseAddinFile.Location = new System.Drawing.Point(215, 30);
            this.browseAddinFile.Name = "browseAddinFile";
            this.browseAddinFile.Size = new System.Drawing.Size(24, 20);
            this.browseAddinFile.TabIndex = 16;
            this.browseAddinFile.Text = "...";
            this.browseAddinFile.UseVisualStyleBackColor = true;
            this.browseAddinFile.Click += new System.EventHandler(this.browseAddinFolder_Click);
            // 
            // addinFileTextBox
            // 
            this.addinFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addinFileTextBox.Location = new System.Drawing.Point(12, 31);
            this.addinFileTextBox.MinimumSize = new System.Drawing.Size(153, 20);
            this.addinFileTextBox.Name = "addinFileTextBox";
            this.addinFileTextBox.Size = new System.Drawing.Size(197, 20);
            this.addinFileTextBox.TabIndex = 15;
            this.addinFileTextBox.TextChanged += new System.EventHandler(this.addinFileTextBox_TextChanged);
            // 
            // addinFileLabel
            // 
            this.addinFileLabel.AutoSize = true;
            this.addinFileLabel.Location = new System.Drawing.Point(9, 9);
            this.addinFileLabel.Name = "addinFileLabel";
            this.addinFileLabel.Size = new System.Drawing.Size(53, 13);
            this.addinFileLabel.TabIndex = 17;
            this.addinFileLabel.Text = "Addin File";
            // 
            // registerButton
            // 
            this.registerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.registerButton.Enabled = false;
            this.registerButton.Location = new System.Drawing.Point(164, 88);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(75, 23);
            this.registerButton.TabIndex = 18;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // AddinRegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 123);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.addinFileLabel);
            this.Controls.Add(this.browseAddinFile);
            this.Controls.Add(this.addinFileTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddinRegisterForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Register add-in classes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseAddinFile;
        private System.Windows.Forms.TextBox addinFileTextBox;
        private System.Windows.Forms.Label addinFileLabel;
        private System.Windows.Forms.Button registerButton;
    }
}

