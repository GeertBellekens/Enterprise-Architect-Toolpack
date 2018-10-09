namespace EAMapping
{
    partial class MappingDetailsControl
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
            this.fromLabel = new System.Windows.Forms.Label();
            this.fromTextBox = new System.Windows.Forms.TextBox();
            this.toTextBox = new System.Windows.Forms.TextBox();
            this.toLabel = new System.Windows.Forms.Label();
            this.mappingLogicTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Location = new System.Drawing.Point(3, 8);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(30, 13);
            this.fromLabel.TabIndex = 0;
            this.fromLabel.Text = "From";
            // 
            // fromTextBox
            // 
            this.fromTextBox.Location = new System.Drawing.Point(39, 5);
            this.fromTextBox.Name = "fromTextBox";
            this.fromTextBox.ReadOnly = true;
            this.fromTextBox.Size = new System.Drawing.Size(176, 20);
            this.fromTextBox.TabIndex = 1;
            // 
            // toTextBox
            // 
            this.toTextBox.Location = new System.Drawing.Point(38, 31);
            this.toTextBox.Name = "toTextBox";
            this.toTextBox.ReadOnly = true;
            this.toTextBox.Size = new System.Drawing.Size(177, 20);
            this.toTextBox.TabIndex = 3;
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(3, 34);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(20, 13);
            this.toLabel.TabIndex = 2;
            this.toLabel.Text = "To";
            // 
            // mappingLogicTextBox
            // 
            this.mappingLogicTextBox.Location = new System.Drawing.Point(3, 57);
            this.mappingLogicTextBox.Multiline = true;
            this.mappingLogicTextBox.Name = "mappingLogicTextBox";
            this.mappingLogicTextBox.Size = new System.Drawing.Size(212, 91);
            this.mappingLogicTextBox.TabIndex = 4;
            // 
            // MappingDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mappingLogicTextBox);
            this.Controls.Add(this.toTextBox);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.fromTextBox);
            this.Controls.Add(this.fromLabel);
            this.Name = "MappingDetailsControl";
            this.Size = new System.Drawing.Size(219, 151);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.TextBox fromTextBox;
        private System.Windows.Forms.TextBox toTextBox;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.TextBox mappingLogicTextBox;
    }
}
