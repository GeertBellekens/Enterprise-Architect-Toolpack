namespace EAMapping
{
    partial class SelectTargetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectTargetForm));
            this.elementsListView = new BrightIdeasSoftware.ObjectListView();
            this.nameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ownerColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fqnColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.elementsListView)).BeginInit();
            this.SuspendLayout();
            // 
            // elementsListView
            // 
            this.elementsListView.AllColumns.Add(this.nameColumn);
            this.elementsListView.AllColumns.Add(this.ownerColumn);
            this.elementsListView.AllColumns.Add(this.fqnColumn);
            this.elementsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementsListView.CellEditUseWholeCell = false;
            this.elementsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.ownerColumn,
            this.fqnColumn});
            this.elementsListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.elementsListView.FullRowSelect = true;
            this.elementsListView.Location = new System.Drawing.Point(12, 12);
            this.elementsListView.MultiSelect = false;
            this.elementsListView.Name = "elementsListView";
            this.elementsListView.ShowGroups = false;
            this.elementsListView.Size = new System.Drawing.Size(486, 246);
            this.elementsListView.TabIndex = 0;
            this.elementsListView.UseCompatibleStateImageBehavior = false;
            this.elementsListView.View = System.Windows.Forms.View.Details;
            this.elementsListView.DoubleClick += new System.EventHandler(this.elementsListView_DoubleClick);
            // 
            // nameColumn
            // 
            this.nameColumn.AspectName = "name";
            this.nameColumn.Text = "Name";
            this.nameColumn.Width = 200;
            // 
            // ownerColumn
            // 
            this.ownerColumn.AspectName = "owner.name";
            this.ownerColumn.Text = "Owner";
            this.ownerColumn.Width = 200;
            // 
            // fqnColumn
            // 
            this.fqnColumn.AspectName = "fqn";
            this.fqnColumn.FillsFreeSpace = true;
            this.fqnColumn.Text = "FQN";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.okButton.Location = new System.Drawing.Point(342, 264);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(423, 264);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // SelectTargetForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(510, 299);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.elementsListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(526, 338);
            this.Name = "SelectTargetForm";
            this.Text = "Select Target Element";
            ((System.ComponentModel.ISupportInitialize)(this.elementsListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView elementsListView;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private BrightIdeasSoftware.OLVColumn nameColumn;
        private BrightIdeasSoftware.OLVColumn ownerColumn;
        private BrightIdeasSoftware.OLVColumn fqnColumn;
    }
}