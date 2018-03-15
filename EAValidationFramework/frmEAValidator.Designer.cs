namespace EAValidationFramework
{
    partial class frmEAValidator
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
            this.ucEAValidator = new EAValidationFramework.ucEAValidator();
            this.SuspendLayout();
            // 
            // ucEAValidator
            // 
            this.ucEAValidator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucEAValidator.Location = new System.Drawing.Point(0, 0);
            this.ucEAValidator.Name = "ucEAValidator";
            this.ucEAValidator.Size = new System.Drawing.Size(1006, 535);
            this.ucEAValidator.TabIndex = 0;
            // 
            // frmEAValidator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1006, 535);
            this.Controls.Add(this.ucEAValidator);
            this.Name = "frmEAValidator";
            this.ShowIcon = false;
            this.Text = "Enterprise Architect Validator";
            this.ResumeLayout(false);

        }

        #endregion

        private ucEAValidator ucEAValidator;
    }
}

