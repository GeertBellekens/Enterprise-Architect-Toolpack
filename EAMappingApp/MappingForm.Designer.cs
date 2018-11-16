namespace EAMappingApp
{
    partial class MappingForm
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
            this._mappingControlGUI = new EAMapping.MappingControlGUI();
            this.SuspendLayout();
            // 
            // _mappingControlGUI
            // 
            this._mappingControlGUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mappingControlGUI.Location = new System.Drawing.Point(0, 0);
            this._mappingControlGUI.Name = "_mappingControlGUI";
            this._mappingControlGUI.Size = new System.Drawing.Size(1222, 674);
            this._mappingControlGUI.TabIndex = 0;
            // 
            // MappingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 674);
            this.Controls.Add(this._mappingControlGUI);
            this.Name = "MappingForm";
            this.Text = "MappingForm";
            this.ResumeLayout(false);

        }

        #endregion

        private EAMapping.MappingControlGUI _mappingControlGUI;
    }
}

