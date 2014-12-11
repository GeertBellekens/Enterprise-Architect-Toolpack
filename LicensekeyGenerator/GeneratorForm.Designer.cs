/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 5/12/2014
 * Time: 4:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace LicensekeyGenerator
{
	partial class GeneratorForm
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
			this.keyTextBox = new System.Windows.Forms.TextBox();
			this.applicationDropDown = new System.Windows.Forms.ComboBox();
			this.applicationLabel = new System.Windows.Forms.Label();
			this.validUntilDatePicker = new System.Windows.Forms.DateTimePicker();
			this.validLabel = new System.Windows.Forms.Label();
			this.clientTextBox = new System.Windows.Forms.TextBox();
			this.clientLabel = new System.Windows.Forms.Label();
			this.numberOfKeysLabel = new System.Windows.Forms.Label();
			this.generateButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.copyToClipboardButton = new System.Windows.Forms.Button();
			this.keySizeDropDown = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.generateKeyPairButton = new System.Windows.Forms.Button();
			this.floatingCheckbox = new System.Windows.Forms.CheckBox();
			this.numberOfLicensesUpDown = new System.Windows.Forms.NumericUpDown();
			this.validateButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numberOfLicensesUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// keyTextBox
			// 
			this.keyTextBox.Location = new System.Drawing.Point(13, 74);
			this.keyTextBox.Multiline = true;
			this.keyTextBox.Name = "keyTextBox";
			this.keyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.keyTextBox.Size = new System.Drawing.Size(690, 319);
			this.keyTextBox.TabIndex = 0;
			this.keyTextBox.WordWrap = false;
			// 
			// applicationDropDown
			// 
			this.applicationDropDown.FormattingEnabled = true;
			this.applicationDropDown.Items.AddRange(new object[] {
									"EAScriptAddin"});
			this.applicationDropDown.Location = new System.Drawing.Point(110, 12);
			this.applicationDropDown.Name = "applicationDropDown";
			this.applicationDropDown.Size = new System.Drawing.Size(181, 21);
			this.applicationDropDown.TabIndex = 1;
			this.applicationDropDown.Text = "EAScriptAddin";
			// 
			// applicationLabel
			// 
			this.applicationLabel.Location = new System.Drawing.Point(12, 15);
			this.applicationLabel.Name = "applicationLabel";
			this.applicationLabel.Size = new System.Drawing.Size(59, 23);
			this.applicationLabel.TabIndex = 2;
			this.applicationLabel.Text = "Application";
			// 
			// validUntilDatePicker
			// 
			this.validUntilDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.validUntilDatePicker.Location = new System.Drawing.Point(110, 43);
			this.validUntilDatePicker.Name = "validUntilDatePicker";
			this.validUntilDatePicker.ShowCheckBox = true;
			this.validUntilDatePicker.Size = new System.Drawing.Size(100, 20);
			this.validUntilDatePicker.TabIndex = 3;
			this.validUntilDatePicker.Value = new System.DateTime(2014, 12, 5, 0, 0, 0, 0);
			// 
			// validLabel
			// 
			this.validLabel.Location = new System.Drawing.Point(13, 47);
			this.validLabel.Name = "validLabel";
			this.validLabel.Size = new System.Drawing.Size(100, 23);
			this.validLabel.TabIndex = 4;
			this.validLabel.Text = "Valid Until";
			// 
			// clientTextBox
			// 
			this.clientTextBox.Location = new System.Drawing.Point(379, 13);
			this.clientTextBox.Name = "clientTextBox";
			this.clientTextBox.Size = new System.Drawing.Size(205, 20);
			this.clientTextBox.TabIndex = 5;
			// 
			// clientLabel
			// 
			this.clientLabel.Location = new System.Drawing.Point(310, 12);
			this.clientLabel.Name = "clientLabel";
			this.clientLabel.Size = new System.Drawing.Size(63, 23);
			this.clientLabel.TabIndex = 6;
			this.clientLabel.Text = "Client";
			// 
			// numberOfKeysLabel
			// 
			this.numberOfKeysLabel.Location = new System.Drawing.Point(310, 47);
			this.numberOfKeysLabel.Name = "numberOfKeysLabel";
			this.numberOfKeysLabel.Size = new System.Drawing.Size(52, 23);
			this.numberOfKeysLabel.TabIndex = 8;
			this.numberOfKeysLabel.Text = "# Keys";
			// 
			// generateButton
			// 
			this.generateButton.Location = new System.Drawing.Point(628, 44);
			this.generateButton.Name = "generateButton";
			this.generateButton.Size = new System.Drawing.Size(75, 23);
			this.generateButton.TabIndex = 9;
			this.generateButton.Text = "Generate";
			this.generateButton.UseVisualStyleBackColor = true;
			this.generateButton.Click += new System.EventHandler(this.GenerateButtonClick);
			// 
			// closeButton
			// 
			this.closeButton.Location = new System.Drawing.Point(628, 408);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 10;
			this.closeButton.Text = "Close";
			this.closeButton.UseVisualStyleBackColor = true;
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(547, 408);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 11;
			this.saveButton.Text = "Save to File";
			this.saveButton.UseVisualStyleBackColor = true;
			// 
			// copyToClipboardButton
			// 
			this.copyToClipboardButton.Location = new System.Drawing.Point(432, 408);
			this.copyToClipboardButton.Name = "copyToClipboardButton";
			this.copyToClipboardButton.Size = new System.Drawing.Size(109, 23);
			this.copyToClipboardButton.TabIndex = 12;
			this.copyToClipboardButton.Text = "Copy to Clipboard";
			this.copyToClipboardButton.UseVisualStyleBackColor = true;
			// 
			// keySizeDropDown
			// 
			this.keySizeDropDown.FormattingEnabled = true;
			this.keySizeDropDown.Location = new System.Drawing.Point(70, 408);
			this.keySizeDropDown.Name = "keySizeDropDown";
			this.keySizeDropDown.Size = new System.Drawing.Size(67, 21);
			this.keySizeDropDown.TabIndex = 13;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 408);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 23);
			this.label1.TabIndex = 14;
			this.label1.Text = "Key Size";
			// 
			// generateKeyPairButton
			// 
			this.generateKeyPairButton.Location = new System.Drawing.Point(143, 406);
			this.generateKeyPairButton.Name = "generateKeyPairButton";
			this.generateKeyPairButton.Size = new System.Drawing.Size(115, 23);
			this.generateKeyPairButton.TabIndex = 15;
			this.generateKeyPairButton.Text = "Generate key pair";
			this.generateKeyPairButton.UseVisualStyleBackColor = true;
			this.generateKeyPairButton.Click += new System.EventHandler(this.generatKeyPairButtonClick);
			// 
			// floatingCheckbox
			// 
			this.floatingCheckbox.Location = new System.Drawing.Point(478, 44);
			this.floatingCheckbox.Name = "floatingCheckbox";
			this.floatingCheckbox.Size = new System.Drawing.Size(106, 24);
			this.floatingCheckbox.TabIndex = 16;
			this.floatingCheckbox.Text = "Floating";
			this.floatingCheckbox.UseVisualStyleBackColor = true;
			// 
			// numberOfLicensesUpDown
			// 
			this.numberOfLicensesUpDown.Location = new System.Drawing.Point(379, 44);
			this.numberOfLicensesUpDown.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.numberOfLicensesUpDown.Name = "numberOfLicensesUpDown";
			this.numberOfLicensesUpDown.Size = new System.Drawing.Size(60, 20);
			this.numberOfLicensesUpDown.TabIndex = 17;
			this.numberOfLicensesUpDown.Value = new decimal(new int[] {
									1,
									0,
									0,
									0});
			// 
			// validateButton
			// 
			this.validateButton.Location = new System.Drawing.Point(265, 405);
			this.validateButton.Name = "validateButton";
			this.validateButton.Size = new System.Drawing.Size(75, 23);
			this.validateButton.TabIndex = 18;
			this.validateButton.Text = "Validate";
			this.validateButton.UseVisualStyleBackColor = true;
			this.validateButton.Click += new System.EventHandler(this.ValidateButtonClick);
			// 
			// GeneratorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(719, 443);
			this.Controls.Add(this.validateButton);
			this.Controls.Add(this.numberOfLicensesUpDown);
			this.Controls.Add(this.floatingCheckbox);
			this.Controls.Add(this.generateKeyPairButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.keySizeDropDown);
			this.Controls.Add(this.copyToClipboardButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.generateButton);
			this.Controls.Add(this.numberOfKeysLabel);
			this.Controls.Add(this.clientLabel);
			this.Controls.Add(this.clientTextBox);
			this.Controls.Add(this.validLabel);
			this.Controls.Add(this.validUntilDatePicker);
			this.Controls.Add(this.applicationLabel);
			this.Controls.Add(this.applicationDropDown);
			this.Controls.Add(this.keyTextBox);
			this.Name = "GeneratorForm";
			this.Text = "LicensekeyGenerator";
			((System.ComponentModel.ISupportInitialize)(this.numberOfLicensesUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button validateButton;
		private System.Windows.Forms.NumericUpDown numberOfLicensesUpDown;
		private System.Windows.Forms.CheckBox floatingCheckbox;
		private System.Windows.Forms.Button generateKeyPairButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox keySizeDropDown;
		private System.Windows.Forms.Button copyToClipboardButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Button generateButton;
		private System.Windows.Forms.Label numberOfKeysLabel;
		private System.Windows.Forms.Label clientLabel;
		private System.Windows.Forms.TextBox clientTextBox;
		private System.Windows.Forms.Label validLabel;
		private System.Windows.Forms.DateTimePicker validUntilDatePicker;
		private System.Windows.Forms.Label applicationLabel;
		private System.Windows.Forms.ComboBox applicationDropDown;
		private System.Windows.Forms.TextBox keyTextBox;
	}
}
