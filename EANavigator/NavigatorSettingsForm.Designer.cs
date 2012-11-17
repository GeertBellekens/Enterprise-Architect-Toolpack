/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 31/07/2012
 * Time: 5:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace TSF.UmlToolingFramework.EANavigator
{
	partial class NavigatorSettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigatorSettingsForm));
			this.defaultActionGroupBox = new System.Windows.Forms.GroupBox();
			this.propertiesRadioButton = new System.Windows.Forms.RadioButton();
			this.projectBrowserRadioButton = new System.Windows.Forms.RadioButton();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.showToolbarCheckBox = new System.Windows.Forms.CheckBox();
			this.useContextMenuCheckBox = new System.Windows.Forms.CheckBox();
			this.trackSelectedElementCheckBox = new System.Windows.Forms.CheckBox();
			this.defaultActionGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// defaultActionGroupBox
			// 
			this.defaultActionGroupBox.Controls.Add(this.propertiesRadioButton);
			this.defaultActionGroupBox.Controls.Add(this.projectBrowserRadioButton);
			this.defaultActionGroupBox.Location = new System.Drawing.Point(12, 12);
			this.defaultActionGroupBox.Name = "defaultActionGroupBox";
			this.defaultActionGroupBox.Size = new System.Drawing.Size(202, 81);
			this.defaultActionGroupBox.TabIndex = 0;
			this.defaultActionGroupBox.TabStop = false;
			this.defaultActionGroupBox.Text = "Doubleclick action";
			// 
			// propertiesRadioButton
			// 
			this.propertiesRadioButton.Location = new System.Drawing.Point(6, 49);
			this.propertiesRadioButton.Name = "propertiesRadioButton";
			this.propertiesRadioButton.Size = new System.Drawing.Size(173, 24);
			this.propertiesRadioButton.TabIndex = 1;
			this.propertiesRadioButton.TabStop = true;
			this.propertiesRadioButton.Text = "Open properties";
			this.propertiesRadioButton.UseVisualStyleBackColor = true;
			this.propertiesRadioButton.CheckedChanged += new System.EventHandler(this.PropertiesRadioButtonCheckedChanged);
			// 
			// projectBrowserRadioButton
			// 
			this.projectBrowserRadioButton.Location = new System.Drawing.Point(6, 19);
			this.projectBrowserRadioButton.Name = "projectBrowserRadioButton";
			this.projectBrowserRadioButton.Size = new System.Drawing.Size(185, 24);
			this.projectBrowserRadioButton.TabIndex = 0;
			this.projectBrowserRadioButton.TabStop = true;
			this.projectBrowserRadioButton.Text = "Select in project browser";
			this.projectBrowserRadioButton.UseVisualStyleBackColor = true;
			this.projectBrowserRadioButton.CheckedChanged += new System.EventHandler(this.ProjectBrowserRadioButtonCheckedChanged);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(116, 227);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(197, 227);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
			// 
			// showToolbarCheckBox
			// 
			this.showToolbarCheckBox.Location = new System.Drawing.Point(18, 99);
			this.showToolbarCheckBox.Name = "showToolbarCheckBox";
			this.showToolbarCheckBox.Size = new System.Drawing.Size(196, 24);
			this.showToolbarCheckBox.TabIndex = 3;
			this.showToolbarCheckBox.Text = "Show toolbar";
			this.showToolbarCheckBox.UseVisualStyleBackColor = true;
			this.showToolbarCheckBox.CheckedChanged += new System.EventHandler(this.ShowToolbarCheckBoxCheckedChanged);
			// 
			// useContextMenuCheckBox
			// 
			this.useContextMenuCheckBox.Location = new System.Drawing.Point(18, 130);
			this.useContextMenuCheckBox.Name = "useContextMenuCheckBox";
			this.useContextMenuCheckBox.Size = new System.Drawing.Size(261, 24);
			this.useContextMenuCheckBox.TabIndex = 4;
			this.useContextMenuCheckBox.Text = "Show Context Menus";
			this.useContextMenuCheckBox.UseVisualStyleBackColor = true;
			this.useContextMenuCheckBox.CheckedChanged += new System.EventHandler(this.UseContextMenuCheckBoxCheckedChanged);
			// 
			// trackSelectedElementCheckBox
			// 
			this.trackSelectedElementCheckBox.Location = new System.Drawing.Point(18, 161);
			this.trackSelectedElementCheckBox.Name = "trackSelectedElementCheckBox";
			this.trackSelectedElementCheckBox.Size = new System.Drawing.Size(261, 24);
			this.trackSelectedElementCheckBox.TabIndex = 5;
			this.trackSelectedElementCheckBox.Text = "Track Selected Element";
			this.trackSelectedElementCheckBox.UseVisualStyleBackColor = true;
			this.trackSelectedElementCheckBox.CheckedChanged += new System.EventHandler(this.TrackSelectedElementCheckBoxCheckedChanged);
			// 
			// NavigatorSettingsForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.trackSelectedElementCheckBox);
			this.Controls.Add(this.useContextMenuCheckBox);
			this.Controls.Add(this.showToolbarCheckBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.defaultActionGroupBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "NavigatorSettingsForm";
			this.Text = "EA Navigator Settings";
			this.defaultActionGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox trackSelectedElementCheckBox;
		private System.Windows.Forms.CheckBox useContextMenuCheckBox;
		private System.Windows.Forms.CheckBox showToolbarCheckBox;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.RadioButton propertiesRadioButton;
		private System.Windows.Forms.RadioButton projectBrowserRadioButton;
		private System.Windows.Forms.GroupBox defaultActionGroupBox;
	}
}
