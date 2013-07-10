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
			this.quickSearchBox = new System.Windows.Forms.GroupBox();
			this.quickSearchAddToDiagramCheck = new System.Windows.Forms.CheckBox();
			this.quickSearchDiagramsCheck = new System.Windows.Forms.CheckBox();
			this.quickSearchAttributesCheck = new System.Windows.Forms.CheckBox();
			this.quickSearchOperationsCheck = new System.Windows.Forms.CheckBox();
			this.quickSearchElementsCheck = new System.Windows.Forms.CheckBox();
			this.quickSearchSelectCheckBox = new System.Windows.Forms.CheckBox();
			this.ActionsGroupBox = new System.Windows.Forms.GroupBox();
			this.defaultActionGroupBox.SuspendLayout();
			this.quickSearchBox.SuspendLayout();
			this.ActionsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// defaultActionGroupBox
			// 
			this.defaultActionGroupBox.Controls.Add(this.propertiesRadioButton);
			this.defaultActionGroupBox.Controls.Add(this.projectBrowserRadioButton);
			this.defaultActionGroupBox.Location = new System.Drawing.Point(12, 12);
			this.defaultActionGroupBox.Name = "defaultActionGroupBox";
			this.defaultActionGroupBox.Size = new System.Drawing.Size(166, 81);
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
			this.okButton.Location = new System.Drawing.Point(213, 240);
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
			this.cancelButton.Location = new System.Drawing.Point(294, 240);
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
			// quickSearchBox
			// 
			this.quickSearchBox.Controls.Add(this.ActionsGroupBox);
			this.quickSearchBox.Controls.Add(this.quickSearchDiagramsCheck);
			this.quickSearchBox.Controls.Add(this.quickSearchAttributesCheck);
			this.quickSearchBox.Controls.Add(this.quickSearchOperationsCheck);
			this.quickSearchBox.Controls.Add(this.quickSearchElementsCheck);
			this.quickSearchBox.Location = new System.Drawing.Point(197, 12);
			this.quickSearchBox.Name = "quickSearchBox";
			this.quickSearchBox.Size = new System.Drawing.Size(173, 212);
			this.quickSearchBox.TabIndex = 6;
			this.quickSearchBox.TabStop = false;
			this.quickSearchBox.Text = "Quick Search";
			// 
			// quickSearchAddToDiagramCheck
			// 
			this.quickSearchAddToDiagramCheck.Location = new System.Drawing.Point(7, 16);
			this.quickSearchAddToDiagramCheck.Name = "quickSearchAddToDiagramCheck";
			this.quickSearchAddToDiagramCheck.Size = new System.Drawing.Size(104, 24);
			this.quickSearchAddToDiagramCheck.TabIndex = 4;
			this.quickSearchAddToDiagramCheck.Text = "Add to diagram";
			this.quickSearchAddToDiagramCheck.UseVisualStyleBackColor = true;
			this.quickSearchAddToDiagramCheck.CheckedChanged += new System.EventHandler(this.QuickSearchAddToDiagramCheckCheckedChanged);
			// 
			// quickSearchDiagramsCheck
			// 
			this.quickSearchDiagramsCheck.Location = new System.Drawing.Point(11, 108);
			this.quickSearchDiagramsCheck.Name = "quickSearchDiagramsCheck";
			this.quickSearchDiagramsCheck.Size = new System.Drawing.Size(105, 24);
			this.quickSearchDiagramsCheck.TabIndex = 3;
			this.quickSearchDiagramsCheck.Text = "Diagrams";
			this.quickSearchDiagramsCheck.UseVisualStyleBackColor = true;
			this.quickSearchDiagramsCheck.CheckedChanged += new System.EventHandler(this.QuickSearchDiagramsCheckCheckedChanged);
			// 
			// quickSearchAttributesCheck
			// 
			this.quickSearchAttributesCheck.Location = new System.Drawing.Point(11, 78);
			this.quickSearchAttributesCheck.Name = "quickSearchAttributesCheck";
			this.quickSearchAttributesCheck.Size = new System.Drawing.Size(105, 24);
			this.quickSearchAttributesCheck.TabIndex = 2;
			this.quickSearchAttributesCheck.Text = "Attributes";
			this.quickSearchAttributesCheck.UseVisualStyleBackColor = true;
			this.quickSearchAttributesCheck.CheckedChanged += new System.EventHandler(this.QuickSearchAttributesCheckCheckedChanged);
			// 
			// quickSearchOperationsCheck
			// 
			this.quickSearchOperationsCheck.Location = new System.Drawing.Point(11, 48);
			this.quickSearchOperationsCheck.Name = "quickSearchOperationsCheck";
			this.quickSearchOperationsCheck.Size = new System.Drawing.Size(105, 24);
			this.quickSearchOperationsCheck.TabIndex = 1;
			this.quickSearchOperationsCheck.Text = "Operations";
			this.quickSearchOperationsCheck.UseVisualStyleBackColor = true;
			this.quickSearchOperationsCheck.CheckedChanged += new System.EventHandler(this.QuickSearchOperationsCheckCheckedChanged);
			// 
			// quickSearchElementsCheck
			// 
			this.quickSearchElementsCheck.Location = new System.Drawing.Point(11, 20);
			this.quickSearchElementsCheck.Name = "quickSearchElementsCheck";
			this.quickSearchElementsCheck.Size = new System.Drawing.Size(105, 24);
			this.quickSearchElementsCheck.TabIndex = 0;
			this.quickSearchElementsCheck.Text = "Elements";
			this.quickSearchElementsCheck.UseVisualStyleBackColor = true;
			this.quickSearchElementsCheck.CheckedChanged += new System.EventHandler(this.QuickSearchElementsCheckCheckedChanged);
			// 
			// quickSearchSelectCheckBox
			// 
			this.quickSearchSelectCheckBox.Location = new System.Drawing.Point(7, 42);
			this.quickSearchSelectCheckBox.Name = "quickSearchSelectCheckBox";
			this.quickSearchSelectCheckBox.Size = new System.Drawing.Size(152, 24);
			this.quickSearchSelectCheckBox.TabIndex = 5;
			this.quickSearchSelectCheckBox.Text = "Select in Project Browser";
			this.quickSearchSelectCheckBox.UseVisualStyleBackColor = true;
			this.quickSearchSelectCheckBox.CheckedChanged += new System.EventHandler(this.QuickSearchSelectCheckBoxCheckedChanged);
			// 
			// ActionsGroupBox
			// 
			this.ActionsGroupBox.Controls.Add(this.quickSearchSelectCheckBox);
			this.ActionsGroupBox.Controls.Add(this.quickSearchAddToDiagramCheck);
			this.ActionsGroupBox.Location = new System.Drawing.Point(4, 136);
			this.ActionsGroupBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.ActionsGroupBox.Name = "ActionsGroupBox";
			this.ActionsGroupBox.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.ActionsGroupBox.Size = new System.Drawing.Size(165, 69);
			this.ActionsGroupBox.TabIndex = 7;
			this.ActionsGroupBox.TabStop = false;
			this.ActionsGroupBox.Text = "Actions";
			// 
			// NavigatorSettingsForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(381, 275);
			this.Controls.Add(this.quickSearchBox);
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
			this.quickSearchBox.ResumeLayout(false);
			this.ActionsGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox quickSearchSelectCheckBox;
		private System.Windows.Forms.GroupBox ActionsGroupBox;
		private System.Windows.Forms.CheckBox quickSearchAddToDiagramCheck;
		private System.Windows.Forms.CheckBox quickSearchElementsCheck;
		private System.Windows.Forms.CheckBox quickSearchOperationsCheck;
		private System.Windows.Forms.CheckBox quickSearchAttributesCheck;
		private System.Windows.Forms.CheckBox quickSearchDiagramsCheck;
		private System.Windows.Forms.GroupBox quickSearchBox;
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
