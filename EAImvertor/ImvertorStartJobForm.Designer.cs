
namespace EAImvertor
{
	partial class ImvertorStartJobForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button browseDefaultHistoryFileButton;
		private System.Windows.Forms.TextBox defaultHistoryFileTextBox;
		private System.Windows.Forms.Label HistoryFileLabel;
		private System.Windows.Forms.Button browseDefaultPropertiesFileButton;
		private System.Windows.Forms.TextBox defaultPropertiesPathTextBox;
		private System.Windows.Forms.Label PropertiesPathLabel;
		private System.Windows.Forms.ComboBox defaultPropertiesTextBox;
		private System.Windows.Forms.Label PropertiesLabel;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImvertorStartJobForm));
			this.browseDefaultHistoryFileButton = new System.Windows.Forms.Button();
			this.defaultHistoryFileTextBox = new System.Windows.Forms.TextBox();
			this.HistoryFileLabel = new System.Windows.Forms.Label();
			this.browseDefaultPropertiesFileButton = new System.Windows.Forms.Button();
			this.defaultPropertiesPathTextBox = new System.Windows.Forms.TextBox();
			this.PropertiesPathLabel = new System.Windows.Forms.Label();
			this.defaultPropertiesTextBox = new System.Windows.Forms.ComboBox();
			this.PropertiesLabel = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// browseDefaultHistoryFileButton
			// 
			this.browseDefaultHistoryFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.browseDefaultHistoryFileButton.Location = new System.Drawing.Point(327, 66);
			this.browseDefaultHistoryFileButton.Name = "browseDefaultHistoryFileButton";
			this.browseDefaultHistoryFileButton.Size = new System.Drawing.Size(24, 20);
			this.browseDefaultHistoryFileButton.TabIndex = 25;
			this.browseDefaultHistoryFileButton.Text = "...";
			this.browseDefaultHistoryFileButton.UseVisualStyleBackColor = true;
			this.browseDefaultHistoryFileButton.Click += new System.EventHandler(this.BrowseDefaultHistoryFileButtonClick);
			// 
			// defaultHistoryFileTextBox
			// 
			this.defaultHistoryFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.defaultHistoryFileTextBox.Location = new System.Drawing.Point(110, 67);
			this.defaultHistoryFileTextBox.MinimumSize = new System.Drawing.Size(153, 20);
			this.defaultHistoryFileTextBox.Name = "defaultHistoryFileTextBox";
			this.defaultHistoryFileTextBox.Size = new System.Drawing.Size(211, 20);
			this.defaultHistoryFileTextBox.TabIndex = 24;
			// 
			// HistoryFileLabel
			// 
			this.HistoryFileLabel.Location = new System.Drawing.Point(4, 70);
			this.HistoryFileLabel.Name = "HistoryFileLabel";
			this.HistoryFileLabel.Size = new System.Drawing.Size(141, 23);
			this.HistoryFileLabel.TabIndex = 26;
			this.HistoryFileLabel.Text = "History File";
			// 
			// browseDefaultPropertiesFileButton
			// 
			this.browseDefaultPropertiesFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.browseDefaultPropertiesFileButton.Location = new System.Drawing.Point(327, 37);
			this.browseDefaultPropertiesFileButton.Name = "browseDefaultPropertiesFileButton";
			this.browseDefaultPropertiesFileButton.Size = new System.Drawing.Size(24, 20);
			this.browseDefaultPropertiesFileButton.TabIndex = 21;
			this.browseDefaultPropertiesFileButton.Text = "...";
			this.browseDefaultPropertiesFileButton.UseVisualStyleBackColor = true;
			this.browseDefaultPropertiesFileButton.Click += new System.EventHandler(this.BrowseDefaultPropertiesFileButtonClick);
			// 
			// defaultPropertiesPathTextBox
			// 
			this.defaultPropertiesPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.defaultPropertiesPathTextBox.Location = new System.Drawing.Point(110, 38);
			this.defaultPropertiesPathTextBox.MinimumSize = new System.Drawing.Size(153, 20);
			this.defaultPropertiesPathTextBox.Name = "defaultPropertiesPathTextBox";
			this.defaultPropertiesPathTextBox.Size = new System.Drawing.Size(211, 20);
			this.defaultPropertiesPathTextBox.TabIndex = 20;
			// 
			// PropertiesPathLabel
			// 
			this.PropertiesPathLabel.Location = new System.Drawing.Point(4, 41);
			this.PropertiesPathLabel.Name = "PropertiesPathLabel";
			this.PropertiesPathLabel.Size = new System.Drawing.Size(141, 23);
			this.PropertiesPathLabel.TabIndex = 23;
			this.PropertiesPathLabel.Text = "Properties File";
			// 
			// defaultPropertiesTextBox
			// 
			this.defaultPropertiesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.defaultPropertiesTextBox.Location = new System.Drawing.Point(110, 12);
			this.defaultPropertiesTextBox.Name = "defaultPropertiesTextBox";
			this.defaultPropertiesTextBox.Size = new System.Drawing.Size(241, 21);
			this.defaultPropertiesTextBox.TabIndex = 19;
			// 
			// PropertiesLabel
			// 
			this.PropertiesLabel.Location = new System.Drawing.Point(4, 15);
			this.PropertiesLabel.Name = "PropertiesLabel";
			this.PropertiesLabel.Size = new System.Drawing.Size(141, 23);
			this.PropertiesLabel.TabIndex = 22;
			this.PropertiesLabel.Text = "Processing Mode";
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(276, 102);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 28;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(195, 102);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 27;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// ImvertorStartJobForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(363, 137);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.browseDefaultHistoryFileButton);
			this.Controls.Add(this.defaultHistoryFileTextBox);
			this.Controls.Add(this.HistoryFileLabel);
			this.Controls.Add(this.browseDefaultPropertiesFileButton);
			this.Controls.Add(this.defaultPropertiesPathTextBox);
			this.Controls.Add(this.PropertiesPathLabel);
			this.Controls.Add(this.defaultPropertiesTextBox);
			this.Controls.Add(this.PropertiesLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImvertorStartJobForm";
			this.Text = "Publish to Imvertor";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
