using System.Collections.Generic;
using System.Linq;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EAMapping
{
	/// <summary>
	/// Description of MappingSettingsForm.
	/// </summary>
	public partial class MappingSettingsForm : Form
	{
		EAMappingSettings settings {get;set;}
		public MappingSettingsForm(EAMappingSettings settings)
		{

			InitializeComponent();
			this.settings = settings;
			this.loadData();
			this.enableDisable();
		}
		private void loadData()
		{
			taggedValuesRadio.Checked = settings.useTaggedValues;
			linkToElementFeatureRadio.Checked = ! settings.useTaggedValues;
			mappingLogicElementRadio.Checked = ! settings.useInlineMappingLogic;
			mappingLogicElementType.Text = settings.mappingLogicType;
			inlineMappingLogicRadio.Checked = settings.useInlineMappingLogic;
			attributeTagTextBox.Text = settings.linkedAttributeTagName;
			associationTagTextBox.Text = settings.linkedAssociationTagName;
            elementTagTextBox.Text = settings.linkedElementTagName;
            contextQueryTextBox.Text = settings.contextQuery;
        }
		private void saveChanges()
		{
			settings.useTaggedValues = taggedValuesRadio.Checked ;
			settings.useTaggedValues = ! linkToElementFeatureRadio.Checked ;
			settings.useInlineMappingLogic = ! mappingLogicElementRadio.Checked  ;
			settings.mappingLogicType = mappingLogicElementType.Text ;
			settings.useInlineMappingLogic = inlineMappingLogicRadio.Checked ;
			settings.linkedAttributeTagName = attributeTagTextBox.Text ;
			settings.linkedAssociationTagName = associationTagTextBox.Text ;
            settings.linkedElementTagName = elementTagTextBox.Text;
            settings.contextQuery = contextQueryTextBox.Text;
            this.settings.save();
		}
		private void enableDisable()
		{
			linkTagNamesGroupBox.Enabled = taggedValuesRadio.Checked;
			attributeTagTextBox.Enabled = taggedValuesRadio.Checked;
			associationTagTextBox.Enabled = taggedValuesRadio.Checked;
			inlineMappingLogicRadio.Enabled = taggedValuesRadio.Checked;
		}
		void OkButtonClick(object sender, EventArgs e)
		{
			this.saveChanges();
			this.Close();
		}
		void CancelButtonClick(object sender, EventArgs e)
		{
			this.Close();
		}
		void ApplyButtonClick(object sender, EventArgs e)
		{
			this.saveChanges();
		}
		void TaggedValuesRadioCheckedChanged(object sender, EventArgs e)
		{
			enableDisable();
		}
		void MappingLogicElementRadioCheckedChanged(object sender, EventArgs e)
		{
			enableDisable();
		}

	}
}
