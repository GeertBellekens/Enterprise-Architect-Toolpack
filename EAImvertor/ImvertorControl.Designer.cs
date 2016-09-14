/*
 * Created by SharpDevelop.
 * User: LaptopGeert
 * Date: 2/07/2016
 * Time: 7:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace EAImvertor
{
	partial class ImvertorControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListView imvertorJobGrid;
		private System.Windows.Forms.ColumnHeader packageHeader;
		private System.Windows.Forms.ColumnHeader statusHeader;
		private System.Windows.Forms.Button retryButton;
		private System.Windows.Forms.Button resultsButton;
		private System.Windows.Forms.GroupBox jobDetailsBox;
		private System.Windows.Forms.TextBox jobIDTextBox;
		private System.Windows.Forms.Button viewWarningsButton;
		private System.Windows.Forms.Label jobIDLabel;
		private System.Windows.Forms.TextBox propertiesTextBox;
		private System.Windows.Forms.Label propertiesLabel;
		private System.Windows.Forms.TextBox processTextBox;
		private System.Windows.Forms.Label processLabel;
		private System.Windows.Forms.TextBox historyFileTextBox;
		private System.Windows.Forms.Label historyFileLabel;
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.ToolTip refreshToolTip;
		private System.Windows.Forms.Button reportButton;
		private System.Windows.Forms.Button publishButton;
		private System.Windows.Forms.Label selectedPackageLabel;
		
		/// <summary>
		/// Disposes resources used by the control.
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImvertorControl));
			this.imvertorJobGrid = new System.Windows.Forms.ListView();
			this.packageHeader = new System.Windows.Forms.ColumnHeader();
			this.statusHeader = new System.Windows.Forms.ColumnHeader();
			this.retryButton = new System.Windows.Forms.Button();
			this.jobDetailsBox = new System.Windows.Forms.GroupBox();
			this.reportButton = new System.Windows.Forms.Button();
			this.refreshButton = new System.Windows.Forms.Button();
			this.historyFileTextBox = new System.Windows.Forms.TextBox();
			this.historyFileLabel = new System.Windows.Forms.Label();
			this.propertiesTextBox = new System.Windows.Forms.TextBox();
			this.propertiesLabel = new System.Windows.Forms.Label();
			this.processTextBox = new System.Windows.Forms.TextBox();
			this.processLabel = new System.Windows.Forms.Label();
			this.jobIDTextBox = new System.Windows.Forms.TextBox();
			this.viewWarningsButton = new System.Windows.Forms.Button();
			this.resultsButton = new System.Windows.Forms.Button();
			this.jobIDLabel = new System.Windows.Forms.Label();
			this.refreshToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.publishButton = new System.Windows.Forms.Button();
			this.selectedPackageLabel = new System.Windows.Forms.Label();
			this.jobDetailsBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// imvertorJobGrid
			// 
			this.imvertorJobGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.imvertorJobGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.packageHeader,
			this.statusHeader});
			this.imvertorJobGrid.FullRowSelect = true;
			this.imvertorJobGrid.GridLines = true;
			this.imvertorJobGrid.Location = new System.Drawing.Point(3, 3);
			this.imvertorJobGrid.Name = "imvertorJobGrid";
			this.imvertorJobGrid.Size = new System.Drawing.Size(366, 75);
			this.imvertorJobGrid.TabIndex = 0;
			this.imvertorJobGrid.UseCompatibleStateImageBehavior = false;
			this.imvertorJobGrid.View = System.Windows.Forms.View.Details;
			this.imvertorJobGrid.SelectedIndexChanged += new System.EventHandler(this.ImvertorJobGridSelectedIndexChanged);
			this.imvertorJobGrid.Resize += new System.EventHandler(this.ImvertorJobGridResize);
			// 
			// packageHeader
			// 
			this.packageHeader.Text = "Package";
			this.packageHeader.Width = 263;
			// 
			// statusHeader
			// 
			this.statusHeader.Text = "Status";
			this.statusHeader.Width = 99;
			// 
			// retryButton
			// 
			this.retryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.retryButton.Enabled = false;
			this.retryButton.Location = new System.Drawing.Point(41, 163);
			this.retryButton.Name = "retryButton";
			this.retryButton.Size = new System.Drawing.Size(75, 23);
			this.retryButton.TabIndex = 1;
			this.retryButton.Text = "Retry";
			this.retryButton.UseVisualStyleBackColor = true;
			this.retryButton.Click += new System.EventHandler(this.RetryButtonClick);
			// 
			// jobDetailsBox
			// 
			this.jobDetailsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.jobDetailsBox.Controls.Add(this.reportButton);
			this.jobDetailsBox.Controls.Add(this.refreshButton);
			this.jobDetailsBox.Controls.Add(this.historyFileTextBox);
			this.jobDetailsBox.Controls.Add(this.historyFileLabel);
			this.jobDetailsBox.Controls.Add(this.propertiesTextBox);
			this.jobDetailsBox.Controls.Add(this.propertiesLabel);
			this.jobDetailsBox.Controls.Add(this.processTextBox);
			this.jobDetailsBox.Controls.Add(this.processLabel);
			this.jobDetailsBox.Controls.Add(this.jobIDTextBox);
			this.jobDetailsBox.Controls.Add(this.viewWarningsButton);
			this.jobDetailsBox.Controls.Add(this.retryButton);
			this.jobDetailsBox.Controls.Add(this.resultsButton);
			this.jobDetailsBox.Controls.Add(this.jobIDLabel);
			this.jobDetailsBox.Location = new System.Drawing.Point(7, 115);
			this.jobDetailsBox.Name = "jobDetailsBox";
			this.jobDetailsBox.Size = new System.Drawing.Size(365, 192);
			this.jobDetailsBox.TabIndex = 3;
			this.jobDetailsBox.TabStop = false;
			this.jobDetailsBox.Text = "Job details";
			// 
			// reportButton
			// 
			this.reportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.reportButton.Location = new System.Drawing.Point(203, 163);
			this.reportButton.Name = "reportButton";
			this.reportButton.Size = new System.Drawing.Size(75, 23);
			this.reportButton.TabIndex = 13;
			this.reportButton.Text = "Report";
			this.reportButton.UseVisualStyleBackColor = true;
			this.reportButton.Click += new System.EventHandler(this.ReportButtonClick);
			// 
			// refreshButton
			// 
			this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
			this.refreshButton.Location = new System.Drawing.Point(6, 163);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(25, 23);
			this.refreshButton.TabIndex = 12;
			this.refreshButton.UseVisualStyleBackColor = true;
			this.refreshButton.Click += new System.EventHandler(this.RefreshButtonClick);
			// 
			// historyFileTextBox
			// 
			this.historyFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.historyFileTextBox.Location = new System.Drawing.Point(7, 120);
			this.historyFileTextBox.Name = "historyFileTextBox";
			this.historyFileTextBox.ReadOnly = true;
			this.historyFileTextBox.Size = new System.Drawing.Size(351, 20);
			this.historyFileTextBox.TabIndex = 11;
			// 
			// historyFileLabel
			// 
			this.historyFileLabel.Location = new System.Drawing.Point(7, 102);
			this.historyFileLabel.Name = "historyFileLabel";
			this.historyFileLabel.Size = new System.Drawing.Size(100, 23);
			this.historyFileLabel.TabIndex = 10;
			this.historyFileLabel.Text = "History File";
			// 
			// propertiesTextBox
			// 
			this.propertiesTextBox.Location = new System.Drawing.Point(7, 79);
			this.propertiesTextBox.Name = "propertiesTextBox";
			this.propertiesTextBox.ReadOnly = true;
			this.propertiesTextBox.Size = new System.Drawing.Size(170, 20);
			this.propertiesTextBox.TabIndex = 7;
			// 
			// propertiesLabel
			// 
			this.propertiesLabel.Location = new System.Drawing.Point(6, 62);
			this.propertiesLabel.Name = "propertiesLabel";
			this.propertiesLabel.Size = new System.Drawing.Size(100, 23);
			this.propertiesLabel.TabIndex = 6;
			this.propertiesLabel.Text = "Processing Mode";
			// 
			// processTextBox
			// 
			this.processTextBox.Location = new System.Drawing.Point(189, 79);
			this.processTextBox.Name = "processTextBox";
			this.processTextBox.ReadOnly = true;
			this.processTextBox.Size = new System.Drawing.Size(170, 20);
			this.processTextBox.TabIndex = 5;
			// 
			// processLabel
			// 
			this.processLabel.Location = new System.Drawing.Point(188, 62);
			this.processLabel.Name = "processLabel";
			this.processLabel.Size = new System.Drawing.Size(146, 23);
			this.processLabel.TabIndex = 4;
			this.processLabel.Text = "Process";
			// 
			// jobIDTextBox
			// 
			this.jobIDTextBox.Location = new System.Drawing.Point(7, 33);
			this.jobIDTextBox.Name = "jobIDTextBox";
			this.jobIDTextBox.ReadOnly = true;
			this.jobIDTextBox.Size = new System.Drawing.Size(170, 20);
			this.jobIDTextBox.TabIndex = 3;
			// 
			// viewWarningsButton
			// 
			this.viewWarningsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.viewWarningsButton.Location = new System.Drawing.Point(122, 163);
			this.viewWarningsButton.Name = "viewWarningsButton";
			this.viewWarningsButton.Size = new System.Drawing.Size(75, 23);
			this.viewWarningsButton.TabIndex = 1;
			this.viewWarningsButton.Text = "Messages";
			this.viewWarningsButton.UseVisualStyleBackColor = true;
			this.viewWarningsButton.Click += new System.EventHandler(this.ViewWarningsButtonClick);
			// 
			// resultsButton
			// 
			this.resultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.resultsButton.Location = new System.Drawing.Point(284, 163);
			this.resultsButton.Name = "resultsButton";
			this.resultsButton.Size = new System.Drawing.Size(75, 23);
			this.resultsButton.TabIndex = 2;
			this.resultsButton.Text = "Zipfile";
			this.resultsButton.UseVisualStyleBackColor = true;
			this.resultsButton.Click += new System.EventHandler(this.ResultsButtonClick);
			// 
			// jobIDLabel
			// 
			this.jobIDLabel.Location = new System.Drawing.Point(6, 16);
			this.jobIDLabel.Name = "jobIDLabel";
			this.jobIDLabel.Size = new System.Drawing.Size(100, 23);
			this.jobIDLabel.TabIndex = 0;
			this.jobIDLabel.Text = "Job ID";
			// 
			// publishButton
			// 
			this.publishButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.publishButton.Location = new System.Drawing.Point(7, 85);
			this.publishButton.Name = "publishButton";
			this.publishButton.Size = new System.Drawing.Size(75, 23);
			this.publishButton.TabIndex = 4;
			this.publishButton.Text = "Publish";
			this.publishButton.UseVisualStyleBackColor = true;
			this.publishButton.Click += new System.EventHandler(this.PublishButtonClick);
			// 
			// selectedPackageLabel
			// 
			this.selectedPackageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.selectedPackageLabel.Location = new System.Drawing.Point(88, 90);
			this.selectedPackageLabel.Name = "selectedPackageLabel";
			this.selectedPackageLabel.Size = new System.Drawing.Size(276, 23);
			this.selectedPackageLabel.TabIndex = 5;
			// 
			// ImvertorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.selectedPackageLabel);
			this.Controls.Add(this.publishButton);
			this.Controls.Add(this.jobDetailsBox);
			this.Controls.Add(this.imvertorJobGrid);
			this.Name = "ImvertorControl";
			this.Size = new System.Drawing.Size(372, 310);
			this.jobDetailsBox.ResumeLayout(false);
			this.jobDetailsBox.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
