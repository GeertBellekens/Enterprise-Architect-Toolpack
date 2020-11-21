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
            this.components = new System.ComponentModel.Container();
            this.fromLabel = new System.Windows.Forms.Label();
            this.fromTextBox = new System.Windows.Forms.TextBox();
            this.toTextBox = new System.Windows.Forms.TextBox();
            this.toLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.addLogicButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.mappingLogicPanel = new System.Windows.Forms.TableLayoutPanel();
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
            this.fromTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fromTextBox.Location = new System.Drawing.Point(39, 5);
            this.fromTextBox.Name = "fromTextBox";
            this.fromTextBox.ReadOnly = true;
            this.fromTextBox.Size = new System.Drawing.Size(180, 20);
            this.fromTextBox.TabIndex = 1;
            // 
            // toTextBox
            // 
            this.toTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toTextBox.Location = new System.Drawing.Point(38, 31);
            this.toTextBox.Name = "toTextBox";
            this.toTextBox.ReadOnly = true;
            this.toTextBox.Size = new System.Drawing.Size(181, 20);
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
            // addLogicButton
            // 
            this.addLogicButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addLogicButton.Image = global::EAMapping.Properties.Resources.Add_plus;
            this.addLogicButton.Location = new System.Drawing.Point(6, 97);
            this.addLogicButton.Name = "addLogicButton";
            this.addLogicButton.Size = new System.Drawing.Size(27, 32);
            this.addLogicButton.TabIndex = 6;
            this.toolTip.SetToolTip(this.addLogicButton, "Add New Mapping Logic");
            this.addLogicButton.UseVisualStyleBackColor = true;
            this.addLogicButton.Click += new System.EventHandler(this.addLogicButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.Image = global::EAMapping.Properties.Resources.delete;
            this.deleteButton.Location = new System.Drawing.Point(6, 132);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(27, 32);
            this.deleteButton.TabIndex = 5;
            this.toolTip.SetToolTip(this.deleteButton, "Delete Mapping");
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // mappingLogicPanel
            // 
            this.mappingLogicPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mappingLogicPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mappingLogicPanel.ColumnCount = 1;
            this.mappingLogicPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mappingLogicPanel.Location = new System.Drawing.Point(38, 54);
            this.mappingLogicPanel.Name = "mappingLogicPanel";
            this.mappingLogicPanel.RowCount = 3;
            this.mappingLogicPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mappingLogicPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mappingLogicPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mappingLogicPanel.Size = new System.Drawing.Size(184, 110);
            this.mappingLogicPanel.TabIndex = 7;
            // 
            // MappingDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.mappingLogicPanel);
            this.Controls.Add(this.addLogicButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.toTextBox);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.fromTextBox);
            this.Controls.Add(this.fromLabel);
            this.MinimumSize = new System.Drawing.Size(0, 169);
            this.Name = "MappingDetailsControl";
            this.Size = new System.Drawing.Size(225, 169);
            this.Enter += new System.EventHandler(this.MappingDetailsControl_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.TextBox fromTextBox;
        private System.Windows.Forms.TextBox toTextBox;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button addLogicButton;
        private System.Windows.Forms.TableLayoutPanel mappingLogicPanel;
    }
}
