﻿/*
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
			this.operationsPanel = new System.Windows.Forms.Panel();
			this.functionsCheckBox = new System.Windows.Forms.CheckedListBox();
			this.operationsListBox = new System.Windows.Forms.CheckedListBox();
			this.OkButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.operationsPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// operationsPanel
			// 
			this.operationsPanel.Controls.Add(this.functionsCheckBox);
			this.operationsPanel.Controls.Add(this.operationsListBox);
			this.operationsPanel.Location = new System.Drawing.Point(12, 12);
			this.operationsPanel.Name = "operationsPanel";
			this.operationsPanel.Size = new System.Drawing.Size(526, 388);
			this.operationsPanel.TabIndex = 0;
			// 
			// functionsCheckBox
			// 
			this.functionsCheckBox.FormattingEnabled = true;
			this.functionsCheckBox.Location = new System.Drawing.Point(266, 4);
			this.functionsCheckBox.Name = "functionsCheckBox";
			this.functionsCheckBox.Size = new System.Drawing.Size(257, 379);
			this.functionsCheckBox.TabIndex = 1;
			// 
			// operationsListBox
			// 
			this.operationsListBox.FormattingEnabled = true;
			this.operationsListBox.Location = new System.Drawing.Point(4, 4);
			this.operationsListBox.Name = "operationsListBox";
			this.operationsListBox.Size = new System.Drawing.Size(257, 379);
			this.operationsListBox.TabIndex = 0;
			this.operationsListBox.SelectedIndexChanged += new System.EventHandler(this.OperationsListBoxSelectedIndexChanged);
			// 
			// OkButton
			// 
			this.OkButton.Location = new System.Drawing.Point(462, 417);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 23);
			this.OkButton.TabIndex = 1;
			this.OkButton.Text = "OK";
			this.OkButton.UseVisualStyleBackColor = true;
			this.OkButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(381, 417);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// EAScriptAddinSettingForm
			// 
			this.AcceptButton = this.OkButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(550, 452);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.operationsPanel);
			this.Name = "EAScriptAddinSettingForm";
			this.Text = "Settings";
			this.operationsPanel.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.CheckedListBox operationsListBox;
		private System.Windows.Forms.CheckedListBox functionsCheckBox;
		private System.Windows.Forms.Panel operationsPanel;
	}
}