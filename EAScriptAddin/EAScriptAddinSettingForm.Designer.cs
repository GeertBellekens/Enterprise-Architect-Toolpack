/*
 * Created by SharpDevelop.
 * User: Geert
 * Date: 26/10/2014
 * Time: 6:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace EAScriptAddin
{
	partial class EAScriptAddinSettingForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EAScriptAddinSettingForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.addFunctionButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.developerModeCheckBox = new System.Windows.Forms.CheckBox();
            this.scriptPathLabel = new System.Windows.Forms.Label();
            this.scriptPathTextBox = new System.Windows.Forms.TextBox();
            this.scriptPathSelectButton = new System.Windows.Forms.Button();
            this.scriptTreeView = new BrightIdeasSoftware.TreeListView();
            this.nameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.scriptTreeImages = new System.Windows.Forms.ImageList(this.components);
            this.functionDropdown = new System.Windows.Forms.ComboBox();
            this.functionsLabel = new System.Windows.Forms.Label();
            this.environmentsGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConnectionString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteEnvironmentButton = new System.Windows.Forms.Button();
            this.environmentsGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.scriptTreeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.environmentsGridView)).BeginInit();
            this.environmentsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // addFunctionButton
            // 
            this.addFunctionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addFunctionButton.Location = new System.Drawing.Point(269, 474);
            this.addFunctionButton.Name = "addFunctionButton";
            this.addFunctionButton.Size = new System.Drawing.Size(86, 23);
            this.addFunctionButton.TabIndex = 5;
            this.addFunctionButton.Text = "Add Function";
            this.addFunctionButton.UseVisualStyleBackColor = true;
            this.addFunctionButton.Click += new System.EventHandler(this.AddFunctionButtonClick);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(280, 745);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(197, 745);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // developerModeCheckBox
            // 
            this.developerModeCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.developerModeCheckBox.AutoSize = true;
            this.developerModeCheckBox.Location = new System.Drawing.Point(12, 503);
            this.developerModeCheckBox.Name = "developerModeCheckBox";
            this.developerModeCheckBox.Size = new System.Drawing.Size(105, 17);
            this.developerModeCheckBox.TabIndex = 3;
            this.developerModeCheckBox.Text = "Developer Mode";
            this.developerModeCheckBox.UseVisualStyleBackColor = true;
            this.developerModeCheckBox.CheckedChanged += new System.EventHandler(this.developerModeCheckBox_CheckedChanged);
            // 
            // scriptPathLabel
            // 
            this.scriptPathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.scriptPathLabel.AutoSize = true;
            this.scriptPathLabel.Location = new System.Drawing.Point(12, 523);
            this.scriptPathLabel.Name = "scriptPathLabel";
            this.scriptPathLabel.Size = new System.Drawing.Size(59, 13);
            this.scriptPathLabel.TabIndex = 4;
            this.scriptPathLabel.Text = "Script Path";
            // 
            // scriptPathTextBox
            // 
            this.scriptPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptPathTextBox.Location = new System.Drawing.Point(12, 539);
            this.scriptPathTextBox.Name = "scriptPathTextBox";
            this.scriptPathTextBox.Size = new System.Drawing.Size(307, 20);
            this.scriptPathTextBox.TabIndex = 5;
            // 
            // scriptPathSelectButton
            // 
            this.scriptPathSelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptPathSelectButton.Location = new System.Drawing.Point(325, 536);
            this.scriptPathSelectButton.Name = "scriptPathSelectButton";
            this.scriptPathSelectButton.Size = new System.Drawing.Size(30, 23);
            this.scriptPathSelectButton.TabIndex = 6;
            this.scriptPathSelectButton.Text = "...";
            this.scriptPathSelectButton.UseVisualStyleBackColor = true;
            this.scriptPathSelectButton.Click += new System.EventHandler(this.scriptPathSelectButton_Click);
            // 
            // scriptTreeView
            // 
            this.scriptTreeView.AllColumns.Add(this.nameColumn);
            this.scriptTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptTreeView.CellEditUseWholeCell = false;
            this.scriptTreeView.CheckedAspectName = "";
            this.scriptTreeView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn});
            this.scriptTreeView.Cursor = System.Windows.Forms.Cursors.Default;
            this.scriptTreeView.HideSelection = false;
            this.scriptTreeView.Location = new System.Drawing.Point(12, 12);
            this.scriptTreeView.Name = "scriptTreeView";
            this.scriptTreeView.ShowGroups = false;
            this.scriptTreeView.ShowImagesOnSubItems = true;
            this.scriptTreeView.Size = new System.Drawing.Size(343, 439);
            this.scriptTreeView.SmallImageList = this.scriptTreeImages;
            this.scriptTreeView.TabIndex = 7;
            this.scriptTreeView.UseCompatibleStateImageBehavior = false;
            this.scriptTreeView.View = System.Windows.Forms.View.Details;
            this.scriptTreeView.VirtualMode = true;
            this.scriptTreeView.SelectedIndexChanged += new System.EventHandler(this.scriptTreeView_SelectedIndexChanged);
            // 
            // nameColumn
            // 
            this.nameColumn.AspectName = "name";
            this.nameColumn.FillsFreeSpace = true;
            this.nameColumn.Text = "Scripts";
            // 
            // scriptTreeImages
            // 
            this.scriptTreeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("scriptTreeImages.ImageStream")));
            this.scriptTreeImages.TransparentColor = System.Drawing.Color.Transparent;
            this.scriptTreeImages.Images.SetKeyName(0, "ScriptGroup");
            this.scriptTreeImages.Images.SetKeyName(1, "Script");
            this.scriptTreeImages.Images.SetKeyName(2, "Operation");
            // 
            // functionDropdown
            // 
            this.functionDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.functionDropdown.DisplayMember = "Name";
            this.functionDropdown.DropDownHeight = 107;
            this.functionDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.functionDropdown.FormattingEnabled = true;
            this.functionDropdown.IntegralHeight = false;
            this.functionDropdown.Location = new System.Drawing.Point(12, 476);
            this.functionDropdown.Name = "functionDropdown";
            this.functionDropdown.Size = new System.Drawing.Size(251, 21);
            this.functionDropdown.TabIndex = 6;
            this.functionDropdown.ValueMember = "null";
            // 
            // functionsLabel
            // 
            this.functionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.functionsLabel.Location = new System.Drawing.Point(12, 457);
            this.functionsLabel.Name = "functionsLabel";
            this.functionsLabel.Size = new System.Drawing.Size(100, 16);
            this.functionsLabel.TabIndex = 7;
            this.functionsLabel.Text = "Function";
            this.functionsLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // environmentsGridView
            // 
            this.environmentsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.environmentsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.environmentsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.environmentsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.ConnectionString});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.environmentsGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.environmentsGridView.Location = new System.Drawing.Point(6, 19);
            this.environmentsGridView.Name = "environmentsGridView";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.environmentsGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.environmentsGridView.RowHeadersVisible = false;
            this.environmentsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.environmentsGridView.Size = new System.Drawing.Size(323, 108);
            this.environmentsGridView.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "Environment";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ToolTipText = "Environment";
            // 
            // ConnectionString
            // 
            this.ConnectionString.HeaderText = "ConnectionString";
            this.ConnectionString.Name = "ConnectionString";
            this.ConnectionString.Width = 250;
            // 
            // deleteEnvironmentButton
            // 
            this.deleteEnvironmentButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteEnvironmentButton.Location = new System.Drawing.Point(254, 133);
            this.deleteEnvironmentButton.Name = "deleteEnvironmentButton";
            this.deleteEnvironmentButton.Size = new System.Drawing.Size(75, 23);
            this.deleteEnvironmentButton.TabIndex = 8;
            this.deleteEnvironmentButton.Text = "Delete";
            this.deleteEnvironmentButton.UseVisualStyleBackColor = true;
            this.deleteEnvironmentButton.Click += new System.EventHandler(this.deleteEnvironmentButton_Click);
            // 
            // environmentsGroupBox
            // 
            this.environmentsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.environmentsGroupBox.Controls.Add(this.environmentsGridView);
            this.environmentsGroupBox.Controls.Add(this.deleteEnvironmentButton);
            this.environmentsGroupBox.Location = new System.Drawing.Point(12, 575);
            this.environmentsGroupBox.Name = "environmentsGroupBox";
            this.environmentsGroupBox.Size = new System.Drawing.Size(341, 164);
            this.environmentsGroupBox.TabIndex = 17;
            this.environmentsGroupBox.TabStop = false;
            this.environmentsGroupBox.Text = "Environments";
            // 
            // EAScriptAddinSettingForm
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(365, 780);
            this.Controls.Add(this.environmentsGroupBox);
            this.Controls.Add(this.addFunctionButton);
            this.Controls.Add(this.functionsLabel);
            this.Controls.Add(this.scriptTreeView);
            this.Controls.Add(this.functionDropdown);
            this.Controls.Add(this.scriptPathSelectButton);
            this.Controls.Add(this.scriptPathTextBox);
            this.Controls.Add(this.scriptPathLabel);
            this.Controls.Add(this.developerModeCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OkButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EAScriptAddinSettingForm";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.scriptTreeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.environmentsGridView)).EndInit();
            this.environmentsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Button addFunctionButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.CheckBox developerModeCheckBox;
        private System.Windows.Forms.Label scriptPathLabel;
        private System.Windows.Forms.TextBox scriptPathTextBox;
        private System.Windows.Forms.Button scriptPathSelectButton;
        private BrightIdeasSoftware.TreeListView scriptTreeView;
        private System.Windows.Forms.ImageList scriptTreeImages;
        private BrightIdeasSoftware.OLVColumn nameColumn;
        private System.Windows.Forms.ComboBox functionDropdown;
        private System.Windows.Forms.Label functionsLabel;
        private System.Windows.Forms.DataGridView environmentsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConnectionString;
        private System.Windows.Forms.Button deleteEnvironmentButton;
        private System.Windows.Forms.GroupBox environmentsGroupBox;
    }
}
