using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSF.UmlToolingFramework.Wrappers.EA;

namespace EAValidator
{
    public partial class SettingsForm : EAAddinFramework.Utilities.AddinSettingsFormBase
    {
        private EAValidatorSettings validatorSettings
        {
            get => (EAValidatorSettings)this.settings;
            set => this.settings = value;
        }
        public SettingsForm(EAValidatorSettings settings) : base(settings)
        {
            InitializeComponent();
            this.settings = settings;
            this.allowedRepositoryTypesListBox.DataSource = Enum.GetValues(typeof(RepositoryType));
            this.elementTypesCheckedList.DataSource = this.elementTypes;
            this.diagramTypesCheckedList.DataSource = this.diagramTypes;
            this.loadSettings();
        }
        public override void refreshContents()
        {
            this.loadSettings();
        }
        private void loadSettings()
        {
            this.txtDirectoryValidationChecks.Text = this.validatorSettings.ValidationChecks_Directory;
            this.excludeArchivedPackagesCheckbox.Checked = this.validatorSettings.excludeArchivedPackages;
            this.archivedPackagesQueryTextBox.Text = this.validatorSettings.QueryExcludeArchivedPackages;
            //set allowed RepositoryTypes
            foreach (var repositoryType in this.validatorSettings.AllowedRepositoryTypes)
            {
                for (int i = 0; i < allowedRepositoryTypesListBox.Items.Count; i++)
                {
                    if (repositoryType == (RepositoryType)allowedRepositoryTypesListBox.Items[i])
                    {
                        allowedRepositoryTypesListBox.SetItemChecked(i, true);
                    }
                }
            }
            //set element types
            foreach (var elementType in this.validatorSettings.scopeElementTypes)
            {
                for (int i = 0; i < this.elementTypesCheckedList.Items.Count; i++)
                {
                    if (elementType.Equals((string)elementTypesCheckedList.Items[i],StringComparison.InvariantCultureIgnoreCase))
                    {
                        elementTypesCheckedList.SetItemChecked(i, true);
                    }
                }
            }
            //set diagram types
            foreach (var diagramType in this.validatorSettings.scopeDiagramTypes)
            {
                for (int i = 0; i < this.diagramTypesCheckedList.Items.Count; i++)
                {
                    if (diagramType.Equals((string)diagramTypesCheckedList.Items[i], StringComparison.InvariantCultureIgnoreCase))
                    {
                        diagramTypesCheckedList.SetItemChecked(i, true);
                    }
                }
            }
            this.enableDisable();
        }
        private void unloadSettings()
        {
            this.validatorSettings.ValidationChecks_Directory = this.txtDirectoryValidationChecks.Text;
            this.validatorSettings.excludeArchivedPackages = this.excludeArchivedPackagesCheckbox.Checked;
            this.validatorSettings.QueryExcludeArchivedPackages = this.archivedPackagesQueryTextBox.Text ;
            this.validatorSettings.AllowedRepositoryTypes = this.allowedRepositoryTypesListBox.CheckedItems.Cast<RepositoryType>().ToList();
            this.validatorSettings.scopeElementTypes = this.elementTypesCheckedList.CheckedItems.Cast<string>().ToList();
            this.validatorSettings.scopeDiagramTypes = this.diagramTypesCheckedList.CheckedItems.Cast<string>().ToList();
        }
        private void save()
        {
            this.unloadSettings();
            this.settings.save();
        }
        private void okButton_Click(object sender, EventArgs e)
        {
            this.save();
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectQueryDirectory_Click(object sender, EventArgs e)
        {
            // Change the setting to the selected directory
            this.txtDirectoryValidationChecks.Text = Utils.selectDirectory(this.validatorSettings.ValidationChecks_Directory);
        }

        private void excludeArchivedPackagesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }

        private void enableDisable()
        {
            this.archivedPackagesQueryTextBox.Enabled = this.excludeArchivedPackagesCheckbox.Checked;
        }
        private List<string> diagramTypes = new List<string> {"Activity"
                                                            ,"Analysis"
                                                            ,"Collaboration"
                                                            ,"Component"
                                                            ,"CompositeStructure"
                                                            ,"Custom"
                                                            ,"Deployment"
                                                            ,"InteractionOverview"
                                                            ,"Logical"
                                                            ,"Object"
                                                            ,"Package"
                                                            ,"Sequence"
                                                            ,"Statechart"
                                                            ,"Timing"
                                                            ,"Use Case"};
        private List<string> elementTypes = new List<string> { "Action"
                                                            ,"Activity"
                                                            ,"ActivityPartition"
                                                            ,"ActivityRegion"
                                                            ,"Actor"
                                                            ,"Artifact"
                                                            ,"Association"
                                                            ,"Boundary"
                                                            ,"Change"
                                                            ,"Class"
                                                            ,"Collaboration"
                                                            ,"Component"
                                                            ,"Constraint"
                                                            ,"Decision"
                                                            ,"DeploymentSpecification"
                                                            ,"DiagramFrame"
                                                            ,"EmbeddedElement"
                                                            ,"Entity"
                                                            ,"EntryPoint"
                                                            ,"Event"
                                                            ,"ExceptionHandler"
                                                            ,"ExitPoint"
                                                            ,"ExpansionNode"
                                                            ,"ExpansionRegion"
                                                            ,"Feature"
                                                            ,"GUIElement"
                                                            ,"InteractionFragment"
                                                            ,"InteractionOccurrence"
                                                            ,"InteractionState"
                                                            ,"Interface"
                                                            ,"InterruptibleActivityRegion"
                                                            ,"Issue"
                                                            ,"Node"
                                                            ,"Note"
                                                            ,"Object"
                                                            ,"Package"
                                                            ,"Parameter"
                                                            ,"Part"
                                                            ,"Port"
                                                            ,"ProvidedInterface"
                                                            ,"Report"
                                                            ,"RequiredInterface"
                                                            ,"Requirement"
                                                            ,"Screen"
                                                            ,"Sequence"
                                                            ,"State"
                                                            ,"StateNode"
                                                            ,"Synchronization"
                                                            ,"Text"
                                                            ,"TimeLine"
                                                            ,"UMLDiagram"
                                                            ,"UseCase"};
    }
}
