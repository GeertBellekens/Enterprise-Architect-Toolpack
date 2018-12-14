namespace EAMapping
{
    partial class MappingLogicControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.contextDropdown = new System.Windows.Forms.ComboBox();
            this.mappingLogicTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // contextDropdown
            // 
            this.contextDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contextDropdown.Location = new System.Drawing.Point(3, 3);
            this.contextDropdown.Name = "contextDropdown";
            this.contextDropdown.Size = new System.Drawing.Size(186, 21);
            this.contextDropdown.TabIndex = 0;
            // 
            // mappingLogicTextBox
            // 
            this.mappingLogicTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mappingLogicTextBox.Location = new System.Drawing.Point(3, 30);
            this.mappingLogicTextBox.Multiline = true;
            this.mappingLogicTextBox.Name = "mappingLogicTextBox";
            this.mappingLogicTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mappingLogicTextBox.Size = new System.Drawing.Size(186, 74);
            this.mappingLogicTextBox.TabIndex = 5;
            this.mappingLogicTextBox.TextChanged += new System.EventHandler(this.mappingLogicTextBox_TextChanged);
            // 
            // MappingLogicControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mappingLogicTextBox);
            this.Controls.Add(this.contextDropdown);
            this.Name = "MappingLogicControl";
            this.Size = new System.Drawing.Size(192, 107);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox contextDropdown;
        private System.Windows.Forms.TextBox mappingLogicTextBox;
    }
}
