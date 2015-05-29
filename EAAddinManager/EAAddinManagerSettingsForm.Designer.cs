/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 13/05/2015
 * Time: 4:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace EAAddinManager
{
	partial class EAAddinManagerSettingsForm
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
			this.addinsListView = new System.Windows.Forms.ListView();
			this.addinNameHeader = new System.Windows.Forms.ColumnHeader();
			this.loadHeader = new System.Windows.Forms.ColumnHeader();
			this.addinsGrupBox = new System.Windows.Forms.GroupBox();
			this.browseFileButton = new System.Windows.Forms.Button();
			this.fileTextBox = new System.Windows.Forms.TextBox();
			this.fileLabel = new System.Windows.Forms.Label();
			this.deleteAddinButton = new System.Windows.Forms.Button();
			this.addAddinButton = new System.Windows.Forms.Button();
			this.browseNameButton = new System.Windows.Forms.Button();
			this.loadCheckBox = new System.Windows.Forms.CheckBox();
			this.versionTextBox = new System.Windows.Forms.TextBox();
			this.versionLabel = new System.Windows.Forms.Label();
			this.nameTextBox = new System.Windows.Forms.TextBox();
			this.nameLabel = new System.Windows.Forms.Label();
			this.locationsGroupBox = new System.Windows.Forms.GroupBox();
			this.deleteLocationButton = new System.Windows.Forms.Button();
			this.addLocationButton = new System.Windows.Forms.Button();
			this.locationsListBox = new System.Windows.Forms.ListBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.applyButton = new System.Windows.Forms.Button();
			this.addinsGrupBox.SuspendLayout();
			this.locationsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// addinsListView
			// 
			this.addinsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.addinNameHeader,
									this.loadHeader});
			this.addinsListView.FullRowSelect = true;
			this.addinsListView.GridLines = true;
			this.addinsListView.Location = new System.Drawing.Point(6, 19);
			this.addinsListView.MultiSelect = false;
			this.addinsListView.Name = "addinsListView";
			this.addinsListView.Size = new System.Drawing.Size(255, 152);
			this.addinsListView.TabIndex = 0;
			this.addinsListView.UseCompatibleStateImageBehavior = false;
			this.addinsListView.View = System.Windows.Forms.View.Details;
			this.addinsListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.AddinsListViewItemSelectionChanged);
			// 
			// addinNameHeader
			// 
			this.addinNameHeader.Text = "Name";
			this.addinNameHeader.Width = 200;
			// 
			// loadHeader
			// 
			this.loadHeader.Text = "Load";
			this.loadHeader.Width = 50;
			// 
			// addinsGrupBox
			// 
			this.addinsGrupBox.Controls.Add(this.browseFileButton);
			this.addinsGrupBox.Controls.Add(this.fileTextBox);
			this.addinsGrupBox.Controls.Add(this.fileLabel);
			this.addinsGrupBox.Controls.Add(this.deleteAddinButton);
			this.addinsGrupBox.Controls.Add(this.addAddinButton);
			this.addinsGrupBox.Controls.Add(this.browseNameButton);
			this.addinsGrupBox.Controls.Add(this.loadCheckBox);
			this.addinsGrupBox.Controls.Add(this.versionTextBox);
			this.addinsGrupBox.Controls.Add(this.versionLabel);
			this.addinsGrupBox.Controls.Add(this.nameTextBox);
			this.addinsGrupBox.Controls.Add(this.nameLabel);
			this.addinsGrupBox.Controls.Add(this.addinsListView);
			this.addinsGrupBox.Location = new System.Drawing.Point(12, 12);
			this.addinsGrupBox.Name = "addinsGrupBox";
			this.addinsGrupBox.Size = new System.Drawing.Size(483, 212);
			this.addinsGrupBox.TabIndex = 1;
			this.addinsGrupBox.TabStop = false;
			this.addinsGrupBox.Text = "Add-ins";
			// 
			// browseFileButton
			// 
			this.browseFileButton.Location = new System.Drawing.Point(438, 75);
			this.browseFileButton.Name = "browseFileButton";
			this.browseFileButton.Size = new System.Drawing.Size(29, 23);
			this.browseFileButton.TabIndex = 12;
			this.browseFileButton.Text = "...";
			this.browseFileButton.UseVisualStyleBackColor = true;
			this.browseFileButton.Click += new System.EventHandler(this.BrowseFileButtonClick);
			// 
			// fileTextBox
			// 
			this.fileTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.fileTextBox.Location = new System.Drawing.Point(267, 77);
			this.fileTextBox.Name = "fileTextBox";
			this.fileTextBox.ReadOnly = true;
			this.fileTextBox.Size = new System.Drawing.Size(165, 20);
			this.fileTextBox.TabIndex = 11;
			// 
			// fileLabel
			// 
			this.fileLabel.Location = new System.Drawing.Point(267, 62);
			this.fileLabel.Name = "fileLabel";
			this.fileLabel.Size = new System.Drawing.Size(100, 23);
			this.fileLabel.TabIndex = 10;
			this.fileLabel.Text = "File";
			// 
			// deleteAddinButton
			// 
			this.deleteAddinButton.Location = new System.Drawing.Point(186, 177);
			this.deleteAddinButton.Name = "deleteAddinButton";
			this.deleteAddinButton.Size = new System.Drawing.Size(75, 23);
			this.deleteAddinButton.TabIndex = 8;
			this.deleteAddinButton.Text = "Delete";
			this.deleteAddinButton.UseVisualStyleBackColor = true;
			this.deleteAddinButton.Click += new System.EventHandler(this.DeleteAddinButtonClick);
			// 
			// addAddinButton
			// 
			this.addAddinButton.Location = new System.Drawing.Point(105, 177);
			this.addAddinButton.Name = "addAddinButton";
			this.addAddinButton.Size = new System.Drawing.Size(75, 23);
			this.addAddinButton.TabIndex = 7;
			this.addAddinButton.Text = "Add";
			this.addAddinButton.UseVisualStyleBackColor = true;
			this.addAddinButton.Click += new System.EventHandler(this.AddAddinButtonClick);
			// 
			// browseNameButton
			// 
			this.browseNameButton.Location = new System.Drawing.Point(438, 32);
			this.browseNameButton.Name = "browseNameButton";
			this.browseNameButton.Size = new System.Drawing.Size(29, 23);
			this.browseNameButton.TabIndex = 6;
			this.browseNameButton.Text = "...";
			this.browseNameButton.UseVisualStyleBackColor = true;
			this.browseNameButton.Click += new System.EventHandler(this.BrowseNameButtonClick);
			// 
			// loadCheckBox
			// 
			this.loadCheckBox.Location = new System.Drawing.Point(267, 147);
			this.loadCheckBox.Name = "loadCheckBox";
			this.loadCheckBox.Size = new System.Drawing.Size(104, 24);
			this.loadCheckBox.TabIndex = 5;
			this.loadCheckBox.Text = "Load Add-in";
			this.loadCheckBox.UseVisualStyleBackColor = true;
			this.loadCheckBox.CheckedChanged += new System.EventHandler(this.LoadCheckBoxCheckedChanged);
			// 
			// versionTextBox
			// 
			this.versionTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.versionTextBox.Location = new System.Drawing.Point(267, 121);
			this.versionTextBox.Name = "versionTextBox";
			this.versionTextBox.ReadOnly = true;
			this.versionTextBox.Size = new System.Drawing.Size(165, 20);
			this.versionTextBox.TabIndex = 4;
			// 
			// versionLabel
			// 
			this.versionLabel.Location = new System.Drawing.Point(267, 105);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(100, 23);
			this.versionLabel.TabIndex = 3;
			this.versionLabel.Text = "Version";
			// 
			// nameTextBox
			// 
			this.nameTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.nameTextBox.Location = new System.Drawing.Point(267, 34);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.ReadOnly = true;
			this.nameTextBox.Size = new System.Drawing.Size(165, 20);
			this.nameTextBox.TabIndex = 2;
			// 
			// nameLabel
			// 
			this.nameLabel.Location = new System.Drawing.Point(267, 19);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(100, 23);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "Name";
			// 
			// locationsGroupBox
			// 
			this.locationsGroupBox.Controls.Add(this.deleteLocationButton);
			this.locationsGroupBox.Controls.Add(this.addLocationButton);
			this.locationsGroupBox.Controls.Add(this.locationsListBox);
			this.locationsGroupBox.Location = new System.Drawing.Point(15, 230);
			this.locationsGroupBox.Name = "locationsGroupBox";
			this.locationsGroupBox.Size = new System.Drawing.Size(480, 137);
			this.locationsGroupBox.TabIndex = 2;
			this.locationsGroupBox.TabStop = false;
			this.locationsGroupBox.Text = "Add-in Locations";
			// 
			// deleteLocationButton
			// 
			this.deleteLocationButton.Location = new System.Drawing.Point(399, 108);
			this.deleteLocationButton.Name = "deleteLocationButton";
			this.deleteLocationButton.Size = new System.Drawing.Size(75, 23);
			this.deleteLocationButton.TabIndex = 10;
			this.deleteLocationButton.Text = "Delete";
			this.deleteLocationButton.UseVisualStyleBackColor = true;
			this.deleteLocationButton.Click += new System.EventHandler(this.DeleteLocationButtonClick);
			// 
			// addLocationButton
			// 
			this.addLocationButton.Location = new System.Drawing.Point(318, 108);
			this.addLocationButton.Name = "addLocationButton";
			this.addLocationButton.Size = new System.Drawing.Size(75, 23);
			this.addLocationButton.TabIndex = 9;
			this.addLocationButton.Text = "Add";
			this.addLocationButton.UseVisualStyleBackColor = true;
			this.addLocationButton.Click += new System.EventHandler(this.AddLocationButtonClick);
			// 
			// locationsListBox
			// 
			this.locationsListBox.BackColor = System.Drawing.SystemColors.Window;
			this.locationsListBox.FormattingEnabled = true;
			this.locationsListBox.Location = new System.Drawing.Point(7, 20);
			this.locationsListBox.Name = "locationsListBox";
			this.locationsListBox.Size = new System.Drawing.Size(467, 82);
			this.locationsListBox.TabIndex = 0;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(256, 383);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(337, 383);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// applyButton
			// 
			this.applyButton.Location = new System.Drawing.Point(418, 383);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(75, 23);
			this.applyButton.TabIndex = 5;
			this.applyButton.Text = "Apply";
			this.applyButton.UseVisualStyleBackColor = true;
			this.applyButton.Click += new System.EventHandler(this.ApplyButtonClick);
			// 
			// EAAddinManagerSettingsForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(505, 418);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.locationsGroupBox);
			this.Controls.Add(this.addinsGrupBox);
			this.Name = "EAAddinManagerSettingsForm";
			this.Text = "EA AddinManager Settings";
			this.addinsGrupBox.ResumeLayout(false);
			this.addinsGrupBox.PerformLayout();
			this.locationsGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.Button browseFileButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.ListBox locationsListBox;
		private System.Windows.Forms.Button addLocationButton;
		private System.Windows.Forms.Button deleteLocationButton;
		private System.Windows.Forms.GroupBox locationsGroupBox;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.TextBox nameTextBox;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.TextBox versionTextBox;
		private System.Windows.Forms.CheckBox loadCheckBox;
		private System.Windows.Forms.Button browseNameButton;
		private System.Windows.Forms.Button addAddinButton;
		private System.Windows.Forms.Button deleteAddinButton;
		private System.Windows.Forms.Label fileLabel;
		private System.Windows.Forms.TextBox fileTextBox;
		private System.Windows.Forms.GroupBox addinsGrupBox;
		private System.Windows.Forms.ColumnHeader loadHeader;
		private System.Windows.Forms.ColumnHeader addinNameHeader;
		private System.Windows.Forms.ListView addinsListView;
		

	}
}
