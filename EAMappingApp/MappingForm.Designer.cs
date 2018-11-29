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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.eAMappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mappingControlGUI
            // 
            this._mappingControlGUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mappingControlGUI.Location = new System.Drawing.Point(0, 24);
            this._mappingControlGUI.Name = "_mappingControlGUI";
            this._mappingControlGUI.Size = new System.Drawing.Size(1222, 650);
            this._mappingControlGUI.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eAMappingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1222, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // eAMappingToolStripMenuItem
            // 
            this.eAMappingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importMappingToolStripMenuItem});
            this.eAMappingToolStripMenuItem.Name = "eAMappingToolStripMenuItem";
            this.eAMappingToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.eAMappingToolStripMenuItem.Text = "EA Mapping";
            // 
            // importMappingToolStripMenuItem
            // 
            this.importMappingToolStripMenuItem.Name = "importMappingToolStripMenuItem";
            this.importMappingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importMappingToolStripMenuItem.Text = "Import Mapping";
            this.importMappingToolStripMenuItem.Click += new System.EventHandler(this.importMappingToolStripMenuItem_Click);
            // 
            // MappingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 674);
            this.Controls.Add(this._mappingControlGUI);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MappingForm";
            this.Text = "MappingForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EAMapping.MappingControlGUI _mappingControlGUI;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eAMappingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMappingToolStripMenuItem;
    }
}

