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
            this.components = new System.ComponentModel.Container();
            this.contextDropdown = new System.Windows.Forms.ComboBox();
            this.mappingLogicTextBox = new System.Windows.Forms.TextBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // contextDropdown
            // 
            this.contextDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contextDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.contextDropdown.Location = new System.Drawing.Point(3, 3);
            this.contextDropdown.Name = "contextDropdown";
            this.contextDropdown.Size = new System.Drawing.Size(159, 21);
            this.contextDropdown.TabIndex = 0;
            this.toolTip1.SetToolTip(this.contextDropdown, "Default");
            this.contextDropdown.SelectionChangeCommitted += new System.EventHandler(this.contextDropdown_SelectionChangeCommitted);
            this.contextDropdown.SelectedValueChanged += new System.EventHandler(this.contextDropdown_SelectedValueChanged);
            this.contextDropdown.Resize += new System.EventHandler(this.contextDropdown_Resize);
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
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Image = global::EAMapping.Properties.Resources.delete_cross;
            this.deleteButton.Location = new System.Drawing.Point(168, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(21, 21);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // MappingLogicControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.deleteButton);
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
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
