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
    public partial class SettingsForm : Form
    {
        EAValidatorSettings settings { get; set; }
        public SettingsForm(EAValidatorSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            this.allowedRepositoryTypesListBox.DataSource = Enum.GetValues(typeof(RepositoryType));
            this.elementTypesCheckedList.DataSource = this.elementTypes;
            this.diagramTypesCheckedList.DataSource = this.diagramTypes;
            this.loadSettings();
        }
        private void loadSettings()
        {
            this.txtDirectoryValidationChecks.Text = this.settings.ValidationChecks_Directory;
            this.excludeArchivedPackagesCheckbox.Checked = this.settings.excludeArchivedPackages;
            this.archivedPackagesQueryTextBox.Text = this.settings.QueryExcludeArchivedPackages;
            //set allowed RepositoryTypes
            foreach (var repositoryType in this.settings.AllowedRepositoryTypes)
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
            foreach (var elementType in this.settings.scopeElementTypes)
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
            foreach (var diagramType in this.settings.scopeDiagramTypes)
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
            this.settings.ValidationChecks_Directory = this.txtDirectoryValidationChecks.Text;
            this.settings.excludeArchivedPackages = this.excludeArchivedPackagesCheckbox.Checked;
            this.settings.QueryExcludeArchivedPackages = this.archivedPackagesQueryTextBox.Text ;
            this.settings.AllowedRepositoryTypes = this.allowedRepositoryTypesListBox.CheckedItems.Cast<RepositoryType>().ToList();
            this.settings.scopeElementTypes = this.elementTypesCheckedList.CheckedItems.Cast<string>().ToList();
            this.settings.scopeDiagramTypes = this.diagramTypesCheckedList.CheckedItems.Cast<string>().ToList();
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
            this.txtDirectoryValidationChecks.Text = Utils.selectDirectory(this.settings.ValidationChecks_Directory);
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
