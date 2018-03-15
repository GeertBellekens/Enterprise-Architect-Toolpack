namespace GlossaryManager.GUI
{
    partial class EDD_TestForm
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
            this.edD_MainControl1 = new GlossaryManager.GUI.EDD_MainControl();
            this.SuspendLayout();
            // 
            // edD_MainControl1
            // 
            this.edD_MainControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edD_MainControl1.domains = null;
            this.edD_MainControl1.Location = new System.Drawing.Point(0, 0);
            this.edD_MainControl1.Name = "edD_MainControl1";
            this.edD_MainControl1.Size = new System.Drawing.Size(941, 511);
            this.edD_MainControl1.statusses = null;
            this.edD_MainControl1.TabIndex = 0;
            // 
            // EDD_TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 511);
            this.Controls.Add(this.edD_MainControl1);
            this.Name = "EDD_TestForm";
            this.Text = "EDD_TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private EDD_MainControl edD_MainControl1;
    }
}