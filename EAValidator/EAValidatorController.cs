using EAAddinFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TSF.UmlToolingFramework.UML.Extended;
using TSF.UmlToolingFramework.Wrappers.EA;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAValidator
{
    /// <summary>
    /// EAValidatorController organizes the validations
    /// </summary>
    public class EAValidatorController
    {
        public TSF_EA.Model model { get; private set; }
        public string outputName { get; private set; }
        public List<Validation> validations { get; set; }
        public EAValidatorSettings settings { get; private set; }
        public CheckGroup rootGroup { get; private set; }
        public string scopePackageIDs { get; private set; }
        public Element scopeElement { get; private set; }

        public bool setScope(Element element)
        {
            this.scopeElement = element;
            return this.settings.setContextConfig(element);            
        }
        public Diagram scopeDiagram { get; private set; }
        public bool setScope(Diagram diagram)
        {
            this.scopeDiagram = diagram;
            return this.settings.setContextConfig(diagram?.owner);            
        }


        public EAValidatorController(TSF_EA.Model model, EAValidatorSettings settings)
        {
            this.model = model;
            this.validations = new List<Validation>();
            this.settings = settings;
            this.outputName = this.settings.outputName;
        }
        private void setContextSettings(Element scopeElement)
        {

            this.settings.setContextConfig(scopeElement);
        }
        private IEnumerable<Check> _checks;
        public IEnumerable<Check> checks
        {
            get
            {
                if (this._checks == null)
                {
                    this._checks = this.rootGroup.GetAllChecks();
                }
                return this._checks;
            }
        }

        public TSF_EA.Element getUserSelectedScopeElement()
        {
            return this.model.getUserSelectedElement(this.settings.scopeElementTypes) as TSF_EA.Element;
        }
        internal TSF_EA.Package getSelectedPackage()
        {
            return this.model.selectedTreePackage as TSF_EA.Package;
        }

        public TSF_EA.Diagram getSelectedScopeDiagram()
        {
            var scopeDiagram = this.model.selectedDiagram as TSF_EA.Diagram;
            if (scopeDiagram != null
                && this.settings.scopeDiagramTypes.Contains(scopeDiagram.diagramType))
            {
                return scopeDiagram;
            }
            else
            {
                MessageBox.Show("Please select a valid scope diagram in the project browser");
                return null;
            }
        }

        public void OpenInEA(Validation validation)
        {
            if (!(String.IsNullOrEmpty(validation.ItemGuid)))
            {
                // First find the type of Item in EA
                UMLItem item = null;
                item = this.model.getItemFromGUID(validation.ItemGuid);
                if (item != null)
                {
                    // Select in EA Package Browser
                    item.select();

                    if (item is TSF_EA.Diagram)
                    {
                        // Open the diagram
                        item.open();
                    }
                }
            }
        }
        public void openProperties(Validation validation)
        {
            if (!(String.IsNullOrEmpty(validation.ItemGuid)))
            {
                // First find the type of Item in EA
                var item = this.model.getItemFromGUID(validation.ItemGuid);
                item?.openProperties();
            }
        }

        public void loadChecks()
        {
            var directory = this.settings.ValidationChecks_Directory;
            // Check if directory exists
            if (Utils.FileOrDirectoryExists(directory))
            {
                this.rootGroup = new CheckGroup(new DirectoryInfo(directory), this.settings, this.model);
            }
            else
            {
                //try the default directory, which is the directory of the dll + \Files\Checks\
                var checksDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                    ,@"Files\Checks\") ;
                this.rootGroup = new CheckGroup(new DirectoryInfo(checksDirectory), this.settings, this.model);
            }
        }

        public void clearEAOutput()
        {
            EAOutputLogger.clearLog(this.model, this.outputName);
        }

        public void addLineToEAOutput(string outputline, string parameter)
        {
            if (this.settings.logToSystemOutput)
            {
                EAOutputLogger.log(this.model, this.outputName, string.Format("{0} {1} {2}", DateTime.Now.ToLongTimeString(), outputline, parameter), 0, LogTypeEnum.log);
            }
        }

        public bool ValidateChecks(ucEAValidator uc, List<Check> selectedchecks, TSF_EA.Element scopeElement, TSF_EA.Diagram EA_diagram)
        {
            // Clear the log
            this.clearEAOutput();

            // Check if the Enterprise Architect repository type is allowed
            if (!(this.settings.AllowedRepositoryTypes.Contains(this.model.repositoryType)))
            {
                MessageBox.Show($"Connectiontype of EA project not allowed: {this.model.repositoryType.ToString()}"
                    + $"{Environment.NewLine}Please connect to an EA project of an allowed repository type");
                this.addLineToEAOutput("Connectiontype of EA project not allowed: ", this.model.repositoryType.ToString());
                return false;
            }
            this.addLineToEAOutput("Connected to: ", this.model.repositoryType.ToString());

            // Check if any checks are selected
            int numberOfChecks = selectedchecks.Count();
            uc.InitProgressbar(numberOfChecks);
            // Check if the Enterprise Architect connection is sql
            this.addLineToEAOutput("Number of checks to validate: ", numberOfChecks.ToString());
            if (numberOfChecks > 0)
            {
                // Clear list of validations
                this.validations.Clear();

                // Perform the selected checks and return the validation-results
                this.addLineToEAOutput("START of Validations...", "");

                //get the ID's of the scope package tree
                var scopePackage = scopeElement as TSF_EA.Package;
                if (scopePackage != null)
                {
                    this.scopePackageIDs = scopePackage.packageTreeIDString;
                }
                else
                {
                    //make sure it won't hurt if used in a query anyway
                    this.scopePackageIDs = "0";
                }
                //reset status for all checks
                this.checks.ToList().ForEach(x => x.resetStatus());
                // Validate all selected checks
                foreach (var check in selectedchecks)
                {
                    this.addLineToEAOutput("Validating check: ", check.CheckDescription);
                    this.validations.AddRange(check.Validate(scopeElement, EA_diagram, this.settings.excludeArchivedPackages, this.scopePackageIDs));
                    uc.refreshCheck(check);
                    uc.IncrementProgressbar();
                }

                this.addLineToEAOutput("END of Validations.", "");
                this.addLineToEAOutput("Show list with validation results.", "");
            }

            // If one (or more) check gave an ERROR, then notify the user about it
            return selectedchecks.Any(x => x.Status == CheckStatus.Error);
        }

        internal void editCheck(Check check)
        {
            if (check == null) return;

            new CheckEditorForm(check).ShowDialog();
        }

        internal void copyAsNew(Check check)
        {
            if (check == null) return;
            //copy check
            var saveCheckDialog = new SaveFileDialog();
            saveCheckDialog.Title = "Save check as new file";
            saveCheckDialog.Filter = "Check files|*.xml";
            saveCheckDialog.FilterIndex = 1;
            saveCheckDialog.InitialDirectory = Path.GetDirectoryName(check.checkfile);
            var dialogResult = saveCheckDialog.ShowDialog(this.model.mainEAWindow);
            if (dialogResult == DialogResult.OK)
            {
                var filePath = saveCheckDialog.FileName;
                //copy contents of check file
                var checkfileContents = File.ReadAllText(check.checkfile);
                File.WriteAllText(filePath, checkfileContents);
                //edit new check
                var newCheck = new Check(filePath, check.group, this.settings, this.model);
                new CheckEditorForm(newCheck).ShowDialog();
            }
        }
    }
}
