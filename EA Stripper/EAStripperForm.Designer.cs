namespace EA_Stripper
{
    partial class EAStripperForm
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
            this.startStrippingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startStrippingButton
            // 
            this.startStrippingButton.Location = new System.Drawing.Point(12, 12);
            this.startStrippingButton.Name = "startStrippingButton";
            this.startStrippingButton.Size = new System.Drawing.Size(111, 23);
            this.startStrippingButton.TabIndex = 0;
            this.startStrippingButton.Text = "Start Stripping";
            this.startStrippingButton.UseVisualStyleBackColor = true;
            this.startStrippingButton.Click += new System.EventHandler(this.startStrippingButton_Click);
            // 
            // EAStripperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(211, 91);
            this.Controls.Add(this.startStrippingButton);
            this.Name = "EAStripperForm";
            this.Text = "EA Stripper";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startStrippingButton;
    }
}

