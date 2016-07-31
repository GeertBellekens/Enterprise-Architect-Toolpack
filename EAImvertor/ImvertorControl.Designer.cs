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
			this.imvertorJobGrid = new System.Windows.Forms.ListView();
			this.packageHeader = new System.Windows.Forms.ColumnHeader();
			this.statusHeader = new System.Windows.Forms.ColumnHeader();
			this.retryButton = new System.Windows.Forms.Button();
			this.jobDetailsBox = new System.Windows.Forms.GroupBox();
			this.jobIDTextBox = new System.Windows.Forms.TextBox();
			this.viewWarningsButton = new System.Windows.Forms.Button();
			this.resultsButton = new System.Windows.Forms.Button();
			this.jobIDLabel = new System.Windows.Forms.Label();
			this.jobDetailsBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// imvertorJobGrid
			// 
			this.imvertorJobGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.imvertorJobGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.packageHeader,
			this.statusHeader});
			this.imvertorJobGrid.FullRowSelect = true;
			this.imvertorJobGrid.GridLines = true;
			this.imvertorJobGrid.HoverSelection = true;
			this.imvertorJobGrid.Location = new System.Drawing.Point(3, 3);
			this.imvertorJobGrid.Name = "imvertorJobGrid";
			this.imvertorJobGrid.Size = new System.Drawing.Size(366, 97);
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
			this.retryButton.Location = new System.Drawing.Point(122, 127);
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
			this.jobDetailsBox.Controls.Add(this.jobIDTextBox);
			this.jobDetailsBox.Controls.Add(this.viewWarningsButton);
			this.jobDetailsBox.Controls.Add(this.retryButton);
			this.jobDetailsBox.Controls.Add(this.resultsButton);
			this.jobDetailsBox.Controls.Add(this.jobIDLabel);
			this.jobDetailsBox.Location = new System.Drawing.Point(4, 106);
			this.jobDetailsBox.Name = "jobDetailsBox";
			this.jobDetailsBox.Size = new System.Drawing.Size(365, 156);
			this.jobDetailsBox.TabIndex = 3;
			this.jobDetailsBox.TabStop = false;
			this.jobDetailsBox.Text = "Job details";
			// 
			// jobIDTextBox
			// 
			this.jobIDTextBox.Enabled = false;
			this.jobIDTextBox.Location = new System.Drawing.Point(7, 33);
			this.jobIDTextBox.Name = "jobIDTextBox";
			this.jobIDTextBox.Size = new System.Drawing.Size(146, 20);
			this.jobIDTextBox.TabIndex = 3;
			// 
			// viewWarningsButton
			// 
			this.viewWarningsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.viewWarningsButton.Location = new System.Drawing.Point(203, 127);
			this.viewWarningsButton.Name = "viewWarningsButton";
			this.viewWarningsButton.Size = new System.Drawing.Size(75, 23);
			this.viewWarningsButton.TabIndex = 1;
			this.viewWarningsButton.Text = "Warnings";
			this.viewWarningsButton.UseVisualStyleBackColor = true;
			this.viewWarningsButton.Click += new System.EventHandler(this.ViewWarningsButtonClick);
			// 
			// resultsButton
			// 
			this.resultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.resultsButton.Location = new System.Drawing.Point(284, 127);
			this.resultsButton.Name = "resultsButton";
			this.resultsButton.Size = new System.Drawing.Size(75, 23);
			this.resultsButton.TabIndex = 2;
			this.resultsButton.Text = "Results";
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
			// ImvertorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.jobDetailsBox);
			this.Controls.Add(this.imvertorJobGrid);
			this.Name = "ImvertorControl";
			this.Size = new System.Drawing.Size(372, 265);
			this.jobDetailsBox.ResumeLayout(false);
			this.jobDetailsBox.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
