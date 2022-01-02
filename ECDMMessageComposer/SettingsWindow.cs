/*
 * Created by SharpDevelop.
 * User: Geert
 * Date: 27/01/2016
 * Time: 5:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ECDMMessageComposer
{
    /// <summary>
    /// Description of SettingsWindow.
    /// </summary>
    public partial class SettingsWindow : EAAddinFramework.Utilities.AddinSettingsFormBase
    {
        private ECDMMessageComposerSettings messageComposerSettings
        {        
            get => (ECDMMessageComposerSettings)this.settings;
            set => this.settings = value;   
        }
        public SettingsWindow(ECDMMessageComposerSettings settings):base(settings)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            this.InitializeComponent();
            this.loadData();
            this.enableDisable();

        }
        private void loadData()
        {
            // get the ignored stereotypes
            this.loadGridData(this.ignoredStereoTypesGrid, this.messageComposerSettings.ignoredStereotypes);
            //get the ignored tagged values
            this.loadGridData(this.ignoredTaggedValuesGrid, this.messageComposerSettings.ignoredTaggedValues);
            //get the ignored constraints
            this.loadGridData(this.ignoredConstraintsGrid, this.messageComposerSettings.ignoredConstraintTypes);
            //get the datatypes to copy
            this.loadGridData(this.dataTypesGridView, this.messageComposerSettings.dataTypesToCopy);
            //get the hidden element types
            this.loadGridData(this.hiddenElementGrid, this.messageComposerSettings.hiddenElementTypes);
            //get the synchronized tagged values
            this.loadGridData(this.synchronizedTagsGridView, this.messageComposerSettings.synchronizedTaggedValues);
            //copy generalizations
            this.generalCopyGeneralizationsCheckbox.Checked = this.messageComposerSettings.copyExternalGeneralizations;
            //ignore inheritance checkbox
            this.copyAllGeneralizationsCheckBox.Checked = this.messageComposerSettings.copyAllGeneralizations;
            //Generate to artifact package
            this.generateToArtifactPackageCheckBox.Checked = this.messageComposerSettings.generateToArtifactPackage;
            //use alias for redefined elements
            this.useAliasForRedefinedElementsCheckBox.Checked = this.messageComposerSettings.useAliasForRedefinedElements;
            //generate diagram checkbox
            this.generateDiagramCheckbox.Checked = this.messageComposerSettings.generateDiagram;
            //addDataTypes checkbox
            this.addDataTypesCheckBox.Checked = this.messageComposerSettings.addDataTypes;
            //addSourceElements checkbox
            this.addSourceElementCheckBox.Checked = this.messageComposerSettings.addSourceElements;
            //copySourceElements checkbox
            this.copyDatatypesCheckbox.Checked = this.messageComposerSettings.copyDataTypes;
            //limit datatypes checkbox
            this.limitDatatypesCheckBox.Checked = this.messageComposerSettings.limitDataTypes;
            //copy Generalizations checkbox
            this.copyDataTypeGeneralizationsCheckBox.Checked = this.messageComposerSettings.copyDataTypeGeneralizations;
            //Attribute options
            this.keepAttributeOrderRadio.Checked = this.messageComposerSettings.keepOriginalAttributeOrder;
            this.setAttributesOrderZeroRadio.Checked = this.messageComposerSettings.setAttributeOrderZero;
            this.addNewAttributesLastRadio.Checked = !this.messageComposerSettings.setAttributeOrderZero && !this.messageComposerSettings.keepOriginalAttributeOrder;
            //sourceAttributeTag
            this.attributeTagTextBox.Text = this.messageComposerSettings.sourceAttributeTagName;
            //sourceAssociationTag
            this.associationTagTextBox.Text = this.messageComposerSettings.sourceAssociationTagName;
            //redirectGeneralizationsToSubset
            this.RedirectGeneralizationsCheckBox.Checked = this.messageComposerSettings.redirectGeneralizationsToSubset;
            this.checkSecurityCheckBox.Checked = this.messageComposerSettings.checkSecurity;
            this.deleteUnusedElementsCheckBox.Checked = this.messageComposerSettings.deleteUnusedSchemaElements;
            this.usePackageSubsetsOnlyCheckBox.Checked = this.messageComposerSettings.usePackageSchemasOnly;
            //notes options
            this.prefixNotesCheckBox.Checked = this.messageComposerSettings.prefixNotes;
            this.notesPrefixTextBox.Text = this.messageComposerSettings.prefixNotesText;
            this.keepNotesInSyncCheckBox.Checked = this.messageComposerSettings.keepNotesInSync;
            //xml schema options
            this.noAttributeDependenciesCheckbox.Checked = this.messageComposerSettings.dontCreateAttributeDependencies;
            this.orderAssociationsCheckbox.Checked = this.messageComposerSettings.orderAssociationsAlphabetically;
            this.orderAssociationsAmongstAttributesCheckbox.Checked = this.messageComposerSettings.orderAssociationsAmongstAttributes;
            this.choiceBeforeAttributesCheckbox.Checked = this.messageComposerSettings.orderXmlChoiceBeforeAttributes;
            this.customOrderTagTextBox.Text = this.messageComposerSettings.customPositionTag;
            this.tvInsteadOfTraceCheckBox.Checked = this.messageComposerSettings.tvInsteadOfTrace;
            this.elementTagTextBox.Text = this.messageComposerSettings.elementTagName;
            
        }
        private void enableDisable()
        {
            this.limitDatatypesCheckBox.Enabled = this.copyDatatypesCheckbox.Checked;
            this.dataTypesGridView.Enabled = this.limitDatatypesCheckBox.Checked;
            this.deleteDataTypeButton.Enabled = this.dataTypesGridView.Enabled;
            this.copyDataTypeGeneralizationsCheckBox.Enabled = this.copyDatatypesCheckbox.Checked;
            this.notesPrefixTextBox.Enabled = this.prefixNotesCheckBox.Checked && !this.keepNotesInSyncCheckBox.Checked; ;
            this.prefixNotesCheckBox.Enabled = !this.keepNotesInSyncCheckBox.Checked;
            this.orderAssociationsAmongstAttributesCheckbox.Enabled = this.orderAssociationsCheckbox.Checked;
            this.elementTagTextBox.Enabled = this.tvInsteadOfTraceCheckBox.Checked;
            this.prefixNotesCheckBox.Enabled = !this.keepNotesInSyncCheckBox.Checked;
        }
        private void saveChanges()
        {
            //get the stereotypes from the grid
            this.messageComposerSettings.ignoredStereotypes = this.getListFromDataGrid(this.ignoredStereoTypesGrid);
            //get the tagged values from the grid
            this.messageComposerSettings.ignoredTaggedValues = this.getListFromDataGrid(this.ignoredTaggedValuesGrid);
            //get the constraint types from the grid
            this.messageComposerSettings.ignoredConstraintTypes = this.getListFromDataGrid(this.ignoredConstraintsGrid);
            //get the datatypes from the grid
            this.messageComposerSettings.dataTypesToCopy = this.getListFromDataGrid(this.dataTypesGridView);
            //get the hidden 
            this.messageComposerSettings.hiddenElementTypes = this.getListFromDataGrid(this.hiddenElementGrid);
            //get the synchronized tags 
            this.messageComposerSettings.synchronizedTaggedValues = this.getListFromDataGrid(this.synchronizedTagsGridView);
            //general options
            this.messageComposerSettings.copyExternalGeneralizations = this.generalCopyGeneralizationsCheckbox.Checked;
            this.messageComposerSettings.copyAllGeneralizations = this.copyAllGeneralizationsCheckBox.Checked;
            this.messageComposerSettings.redirectGeneralizationsToSubset = this.RedirectGeneralizationsCheckBox.Checked;

            this.messageComposerSettings.checkSecurity = this.checkSecurityCheckBox.Checked;
            this.messageComposerSettings.deleteUnusedSchemaElements = this.deleteUnusedElementsCheckBox.Checked;
            this.messageComposerSettings.usePackageSchemasOnly = this.usePackageSubsetsOnlyCheckBox.Checked;
            this.messageComposerSettings.generateToArtifactPackage = this.generateToArtifactPackageCheckBox.Checked;
            this.messageComposerSettings.useAliasForRedefinedElements = this.useAliasForRedefinedElementsCheckBox.Checked;
            //notes options
            this.messageComposerSettings.prefixNotes = this.prefixNotesCheckBox.Checked;
            this.messageComposerSettings.prefixNotesText = this.notesPrefixTextBox.Text;
            this.messageComposerSettings.keepNotesInSync = this.keepNotesInSyncCheckBox.Checked;
            //diagram options
            this.messageComposerSettings.generateDiagram = this.generateDiagramCheckbox.Checked;
            this.messageComposerSettings.addDataTypes = this.addDataTypesCheckBox.Checked;
            this.messageComposerSettings.addSourceElements = this.addSourceElementCheckBox.Checked;
            //datatype options
            this.messageComposerSettings.copyDataTypes = this.copyDatatypesCheckbox.Checked;
            this.messageComposerSettings.limitDataTypes = this.limitDatatypesCheckBox.Checked;
            this.messageComposerSettings.copyDataTypeGeneralizations = this.copyDataTypeGeneralizationsCheckBox.Checked;
            //Attribute options
            this.messageComposerSettings.keepOriginalAttributeOrder = this.keepAttributeOrderRadio.Checked;
            this.messageComposerSettings.setAttributeOrderZero = this.setAttributesOrderZeroRadio.Checked;
            //tracebility tag names
            this.messageComposerSettings.sourceAttributeTagName = this.attributeTagTextBox.Text;
            this.messageComposerSettings.sourceAssociationTagName = this.associationTagTextBox.Text;
            //xml schema settings
            this.messageComposerSettings.dontCreateAttributeDependencies = this.noAttributeDependenciesCheckbox.Checked;
            this.messageComposerSettings.orderAssociationsAlphabetically = this.orderAssociationsCheckbox.Checked;
            this.messageComposerSettings.orderAssociationsAmongstAttributes = this.orderAssociationsAmongstAttributesCheckbox.Checked;
            this.messageComposerSettings.orderXmlChoiceBeforeAttributes = this.choiceBeforeAttributesCheckbox.Checked;
            this.messageComposerSettings.customPositionTag = this.customOrderTagTextBox.Text;
            this.messageComposerSettings.tvInsteadOfTrace = this.tvInsteadOfTraceCheckBox.Checked;
            this.messageComposerSettings.elementTagName = this.elementTagTextBox.Text;
            //save changes
            this.messageComposerSettings.save();
        }
        private void loadGridData(DataGridView datagrid, List<string> data)
        {
            datagrid.Rows.Clear();
            foreach (var rowvalue in data)
            {
                datagrid.Rows.Add(rowvalue);
            }
        }
        private List<string> getListFromDataGrid(DataGridView datagrid)
        {
            //make new tagged values list
            var returnedList = new List<string>();
            //get ignored tagged values from grid
            foreach (DataGridViewRow row in datagrid.Rows)
            {
                string rowValue = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : string.Empty;
                if (rowValue != string.Empty)
                {
                    returnedList.Add(rowValue);
                }
            }
            return returnedList;
        }

        void DeleteStereotypeButtonClick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.ignoredStereoTypesGrid.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.ignoredStereoTypesGrid.Rows.Remove(row);
                }
            }
        }
        void OkButtonClick(object sender, EventArgs e)
        {
            this.saveChanges();
            this.Close();
        }

        void ApplyButtonClick(object sender, EventArgs e)
        {
            this.saveChanges();
        }
        void CancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
        void DeleteTaggedValueButtonClick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.ignoredTaggedValuesGrid.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.ignoredTaggedValuesGrid.Rows.Remove(row);
                }
            }
        }
        private void deleteConstraintTypeButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.ignoredConstraintsGrid.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.ignoredConstraintsGrid.Rows.Remove(row);
                }
            }
        }
        void DeleteDataTypeButtonClick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataTypesGridView.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.dataTypesGridView.Rows.Remove(row);
                }
            }
        }
        private void deleteHiddenElementButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.hiddenElementGrid.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.hiddenElementGrid.Rows.Remove(row);
                }
            }
        }
        void CopyDatatypesCheckboxCheckedChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }
        void LimitDatatypesCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }
        void PrefixNotesCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }
        void OrderAssociationsCheckboxCheckedChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }
        void TtvInsteadOfTraceCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }

        private void keepNotesInSyncCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }

        private void DeleteSynchronizedTagButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.synchronizedTagsGridView.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.synchronizedTagsGridView.Rows.Remove(row);
                }
            }
        }

        public override void refreshContents()
        {
            this.loadData();
        }
    }
}
