using System.Collections.Generic;
using System.Linq;
namespace EAMapping
{
	partial class MappingSettingsForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.GroupBox linkTypeGroupBox;
		private System.Windows.Forms.RadioButton linkToElementFeatureRadio;
		private System.Windows.Forms.RadioButton taggedValuesRadio;
		private System.Windows.Forms.GroupBox mappingLogicGroupBox;
		private System.Windows.Forms.TextBox mappingLogicElementType;
		private System.Windows.Forms.RadioButton inlineMappingLogicRadio;
		private System.Windows.Forms.RadioButton mappingLogicElementRadio;
		private System.Windows.Forms.GroupBox linkTagNamesGroupBox;
		private System.Windows.Forms.Label associationTagLabel;
		private System.Windows.Forms.TextBox associationTagTextBox;
		private System.Windows.Forms.Label attributeTagLabel;
		private System.Windows.Forms.TextBox attributeTagTextBox;
		
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MappingSettingsForm));
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.linkTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.linkToElementFeatureRadio = new System.Windows.Forms.RadioButton();
            this.taggedValuesRadio = new System.Windows.Forms.RadioButton();
            this.mappingLogicGroupBox = new System.Windows.Forms.GroupBox();
            this.mappingLogicElementType = new System.Windows.Forms.TextBox();
            this.inlineMappingLogicRadio = new System.Windows.Forms.RadioButton();
            this.mappingLogicElementRadio = new System.Windows.Forms.RadioButton();
            this.linkTagNamesGroupBox = new System.Windows.Forms.GroupBox();
            this.elementTagLabel = new System.Windows.Forms.Label();
            this.elementTagTextBox = new System.Windows.Forms.TextBox();
            this.associationTagLabel = new System.Windows.Forms.Label();
            this.associationTagTextBox = new System.Windows.Forms.TextBox();
            this.attributeTagLabel = new System.Windows.Forms.Label();
            this.attributeTagTextBox = new System.Windows.Forms.TextBox();
            this.contextGroupBox = new System.Windows.Forms.GroupBox();
            this.contextQueryTextBox = new System.Windows.Forms.TextBox();
            this.linkTypeGroupBox.SuspendLayout();
            this.mappingLogicGroupBox.SuspendLayout();
            this.linkTagNamesGroupBox.SuspendLayout();
            this.contextGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(408, 377);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 12;
            this.applyButton.Text = "Save";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.ApplyButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(327, 377);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(246, 377);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // linkTypeGroupBox
            // 
            this.linkTypeGroupBox.Controls.Add(this.linkToElementFeatureRadio);
            this.linkTypeGroupBox.Controls.Add(this.taggedValuesRadio);
            this.linkTypeGroupBox.Enabled = false;
            this.linkTypeGroupBox.Location = new System.Drawing.Point(12, 12);
            this.linkTypeGroupBox.Name = "linkTypeGroupBox";
            this.linkTypeGroupBox.Size = new System.Drawing.Size(233, 89);
            this.linkTypeGroupBox.TabIndex = 13;
            this.linkTypeGroupBox.TabStop = false;
            this.linkTypeGroupBox.Text = "Link Type";
            // 
            // linkToElementFeatureRadio
            // 
            this.linkToElementFeatureRadio.Enabled = false;
            this.linkToElementFeatureRadio.Location = new System.Drawing.Point(7, 51);
            this.linkToElementFeatureRadio.Name = "linkToElementFeatureRadio";
            this.linkToElementFeatureRadio.Size = new System.Drawing.Size(172, 24);
            this.linkToElementFeatureRadio.TabIndex = 1;
            this.linkToElementFeatureRadio.TabStop = true;
            this.linkToElementFeatureRadio.Text = "Link To Element Feature";
            this.linkToElementFeatureRadio.UseVisualStyleBackColor = true;
            // 
            // taggedValuesRadio
            // 
            this.taggedValuesRadio.Location = new System.Drawing.Point(7, 20);
            this.taggedValuesRadio.Name = "taggedValuesRadio";
            this.taggedValuesRadio.Size = new System.Drawing.Size(154, 24);
            this.taggedValuesRadio.TabIndex = 0;
            this.taggedValuesRadio.TabStop = true;
            this.taggedValuesRadio.Text = "Tagged Values";
            this.taggedValuesRadio.UseVisualStyleBackColor = true;
            this.taggedValuesRadio.CheckedChanged += new System.EventHandler(this.TaggedValuesRadioCheckedChanged);
            // 
            // mappingLogicGroupBox
            // 
            this.mappingLogicGroupBox.Controls.Add(this.mappingLogicElementType);
            this.mappingLogicGroupBox.Controls.Add(this.inlineMappingLogicRadio);
            this.mappingLogicGroupBox.Controls.Add(this.mappingLogicElementRadio);
            this.mappingLogicGroupBox.Enabled = false;
            this.mappingLogicGroupBox.Location = new System.Drawing.Point(12, 107);
            this.mappingLogicGroupBox.Name = "mappingLogicGroupBox";
            this.mappingLogicGroupBox.Size = new System.Drawing.Size(233, 109);
            this.mappingLogicGroupBox.TabIndex = 14;
            this.mappingLogicGroupBox.TabStop = false;
            this.mappingLogicGroupBox.Text = "Mapping Logic Type";
            // 
            // mappingLogicElementType
            // 
            this.mappingLogicElementType.Enabled = false;
            this.mappingLogicElementType.Location = new System.Drawing.Point(22, 50);
            this.mappingLogicElementType.Name = "mappingLogicElementType";
            this.mappingLogicElementType.Size = new System.Drawing.Size(205, 20);
            this.mappingLogicElementType.TabIndex = 2;
            // 
            // inlineMappingLogicRadio
            // 
            this.inlineMappingLogicRadio.Enabled = false;
            this.inlineMappingLogicRadio.Location = new System.Drawing.Point(7, 76);
            this.inlineMappingLogicRadio.Name = "inlineMappingLogicRadio";
            this.inlineMappingLogicRadio.Size = new System.Drawing.Size(104, 24);
            this.inlineMappingLogicRadio.TabIndex = 1;
            this.inlineMappingLogicRadio.TabStop = true;
            this.inlineMappingLogicRadio.Text = "In-line";
            this.inlineMappingLogicRadio.UseVisualStyleBackColor = true;
            // 
            // mappingLogicElementRadio
            // 
            this.mappingLogicElementRadio.Enabled = false;
            this.mappingLogicElementRadio.Location = new System.Drawing.Point(7, 20);
            this.mappingLogicElementRadio.Name = "mappingLogicElementRadio";
            this.mappingLogicElementRadio.Size = new System.Drawing.Size(220, 24);
            this.mappingLogicElementRadio.TabIndex = 0;
            this.mappingLogicElementRadio.TabStop = true;
            this.mappingLogicElementRadio.Text = "Mapping Logic Element Type";
            this.mappingLogicElementRadio.UseVisualStyleBackColor = true;
            this.mappingLogicElementRadio.CheckedChanged += new System.EventHandler(this.MappingLogicElementRadioCheckedChanged);
            // 
            // linkTagNamesGroupBox
            // 
            this.linkTagNamesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkTagNamesGroupBox.Controls.Add(this.elementTagLabel);
            this.linkTagNamesGroupBox.Controls.Add(this.elementTagTextBox);
            this.linkTagNamesGroupBox.Controls.Add(this.associationTagLabel);
            this.linkTagNamesGroupBox.Controls.Add(this.associationTagTextBox);
            this.linkTagNamesGroupBox.Controls.Add(this.attributeTagLabel);
            this.linkTagNamesGroupBox.Controls.Add(this.attributeTagTextBox);
            this.linkTagNamesGroupBox.Location = new System.Drawing.Point(251, 12);
            this.linkTagNamesGroupBox.Name = "linkTagNamesGroupBox";
            this.linkTagNamesGroupBox.Size = new System.Drawing.Size(233, 204);
            this.linkTagNamesGroupBox.TabIndex = 15;
            this.linkTagNamesGroupBox.TabStop = false;
            this.linkTagNamesGroupBox.Text = "Link Tag Names";
            // 
            // elementTagLabel
            // 
            this.elementTagLabel.Location = new System.Drawing.Point(5, 116);
            this.elementTagLabel.Name = "elementTagLabel";
            this.elementTagLabel.Size = new System.Drawing.Size(100, 17);
            this.elementTagLabel.TabIndex = 5;
            this.elementTagLabel.Text = "Element Tag";
            // 
            // elementTagTextBox
            // 
            this.elementTagTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementTagTextBox.Location = new System.Drawing.Point(5, 136);
            this.elementTagTextBox.Name = "elementTagTextBox";
            this.elementTagTextBox.Size = new System.Drawing.Size(222, 20);
            this.elementTagTextBox.TabIndex = 4;
            // 
            // associationTagLabel
            // 
            this.associationTagLabel.Location = new System.Drawing.Point(5, 66);
            this.associationTagLabel.Name = "associationTagLabel";
            this.associationTagLabel.Size = new System.Drawing.Size(100, 17);
            this.associationTagLabel.TabIndex = 3;
            this.associationTagLabel.Text = "Association Tag";
            // 
            // associationTagTextBox
            // 
            this.associationTagTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.associationTagTextBox.Location = new System.Drawing.Point(5, 86);
            this.associationTagTextBox.Name = "associationTagTextBox";
            this.associationTagTextBox.Size = new System.Drawing.Size(222, 20);
            this.associationTagTextBox.TabIndex = 2;
            // 
            // attributeTagLabel
            // 
            this.attributeTagLabel.Location = new System.Drawing.Point(5, 20);
            this.attributeTagLabel.Name = "attributeTagLabel";
            this.attributeTagLabel.Size = new System.Drawing.Size(100, 17);
            this.attributeTagLabel.TabIndex = 1;
            this.attributeTagLabel.Text = "Attribute Tag";
            // 
            // attributeTagTextBox
            // 
            this.attributeTagTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attributeTagTextBox.Location = new System.Drawing.Point(5, 40);
            this.attributeTagTextBox.Name = "attributeTagTextBox";
            this.attributeTagTextBox.Size = new System.Drawing.Size(222, 20);
            this.attributeTagTextBox.TabIndex = 0;
            // 
            // contextGroupBox
            // 
            this.contextGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contextGroupBox.Controls.Add(this.contextQueryTextBox);
            this.contextGroupBox.Location = new System.Drawing.Point(12, 225);
            this.contextGroupBox.Name = "contextGroupBox";
            this.contextGroupBox.Size = new System.Drawing.Size(472, 146);
            this.contextGroupBox.TabIndex = 16;
            this.contextGroupBox.TabStop = false;
            this.contextGroupBox.Text = "Context Query";
            // 
            // contextQueryTextBox
            // 
            this.contextQueryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contextQueryTextBox.Location = new System.Drawing.Point(6, 19);
            this.contextQueryTextBox.Multiline = true;
            this.contextQueryTextBox.Name = "contextQueryTextBox";
            this.contextQueryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.contextQueryTextBox.Size = new System.Drawing.Size(460, 121);
            this.contextQueryTextBox.TabIndex = 0;
            // 
            // MappingSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 412);
            this.Controls.Add(this.contextGroupBox);
            this.Controls.Add(this.linkTagNamesGroupBox);
            this.Controls.Add(this.mappingLogicGroupBox);
            this.Controls.Add(this.linkTypeGroupBox);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(511, 299);
            this.Name = "MappingSettingsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EA Mapping Settings";
            this.linkTypeGroupBox.ResumeLayout(false);
            this.mappingLogicGroupBox.ResumeLayout(false);
            this.mappingLogicGroupBox.PerformLayout();
            this.linkTagNamesGroupBox.ResumeLayout(false);
            this.linkTagNamesGroupBox.PerformLayout();
            this.contextGroupBox.ResumeLayout(false);
            this.contextGroupBox.PerformLayout();
            this.ResumeLayout(false);

		}

        private System.Windows.Forms.Label elementTagLabel;
        private System.Windows.Forms.TextBox elementTagTextBox;
        private System.Windows.Forms.GroupBox contextGroupBox;
        private System.Windows.Forms.TextBox contextQueryTextBox;
    }
}
