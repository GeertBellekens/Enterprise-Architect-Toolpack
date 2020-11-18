using System.Collections.Generic;
using System.Linq;
using System;
using System.Drawing;
using System.Windows.Forms;
using EAAddinFramework.Utilities;

namespace EAMapping
{
    /// <summary>
    /// Description of MappingSettingsForm.
    /// </summary>
    public partial class MappingSettingsForm : AddinSettingsFormBase
    {
        EAMappingSettings mappingSettings
        {
            get => (EAMappingSettings)this.settings;
            set => this.settings = value;
        }
        public MappingSettingsForm(EAMappingSettings settings) : base(settings)
        {
            InitializeComponent();
            this.loadData();
        }
        public override void refreshContents()
        {
            this.loadData();
        }
        private void loadData()
        {
            taggedValuesRadio.Checked = mappingSettings.useTaggedValues;
            linkToElementFeatureRadio.Checked = !mappingSettings.useTaggedValues;
            mappingLogicElementRadio.Checked = !mappingSettings.useInlineMappingLogic;
            mappingLogicElementType.Text = mappingSettings.mappingLogicType;
            inlineMappingLogicRadio.Checked = mappingSettings.useInlineMappingLogic;
            attributeTagTextBox.Text = mappingSettings.linkedAttributeTagName;
            associationTagTextBox.Text = mappingSettings.linkedAssociationTagName;
            elementTagTextBox.Text = mappingSettings.linkedElementTagName;
            contextQueryTextBox.Text = mappingSettings.contextQuery;
            this.enableDisable();
        }
        private void saveChanges()
        {
            mappingSettings.useTaggedValues = taggedValuesRadio.Checked;
            mappingSettings.useTaggedValues = !linkToElementFeatureRadio.Checked;
            mappingSettings.useInlineMappingLogic = !mappingLogicElementRadio.Checked;
            mappingSettings.mappingLogicType = mappingLogicElementType.Text;
            mappingSettings.useInlineMappingLogic = inlineMappingLogicRadio.Checked;
            mappingSettings.linkedAttributeTagName = attributeTagTextBox.Text;
            mappingSettings.linkedAssociationTagName = associationTagTextBox.Text;
            mappingSettings.linkedElementTagName = elementTagTextBox.Text;
            mappingSettings.contextQuery = contextQueryTextBox.Text;
            this.mappingSettings.save();
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
