using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using EAAddinFramework.Utilities;
using TSF.UmlToolingFramework.UML.Extended;
using System.Windows.Forms;

namespace EAValidationFramework
{
    /// <summary>
    /// EAValidatorController organizes the validations
    /// </summary>
    public class EAValidatorController
    {
        TSF_EA.Model model { get; set; }
        public string outputName { get; private set; }
        public List<Validation> validations { get; set; }
        public List<Check> checks { get; set; }
        public EAValidationFrameworkSettings settings { get; set; }

        public EAValidatorController()
        {
            model = new TSF_EA.Model();
            validations = new List<Validation>();
            checks = new List<Check>();
            this.settings = new EAValidationFrameworkSettings();
            this.outputName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        }

        public TSF_EA.Element getUserSelectedElement()
        {
            //return this.model.getUserSelectedElement(new List<string> { "Change", "Class", "Package" }) as TSF_EA.Element;
            return this.model.getUserSelectedElement(new List<string> { this.settings.SearchElementTypes }) as TSF_EA.Element;
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
                    else
                    {
                        // Open the properties
                        item.openProperties();
                    }

                }
            }
        }

        public void loadChecksFromDirectory(string directory)
        {
            //clear EA output
            this.clearEAOutput();
            this.addLineToEAOutput("Start loading checks ...", "");

            var extension = this.settings.ValidationChecks_Documenttype;
            string[] Files;

            // Check if directory exists
            if (Utils.FileOrDirectoryExists(directory))
            {
                this.addLineToEAOutput("Directory: ", directory);

                // Get files from given directory                
                Files = Utils.getFilesFromDirectory(directory, extension);
                foreach (string file in Files)
                {
                    // Verify that xml-doc is accepted by xsd
                    if (Utils.ValidToXSD(this, file))
                    {
                        // add new check
                        Check check = new Check(file, extension, this, this.model);
                        checks.Add(check);
                        this.addLineToEAOutput("Check added: ", check.CheckId + " - " + check.CheckDescription);
                    }
                }

                // Get subdirectories for main directory
                string[] subdirectories = Utils.getSubdirectoriesForDirectory(directory);

                // Load files from subdirectories as checks
                foreach (string subdir in subdirectories)
                {
                    this.addLineToEAOutput("Directory: ", subdir);

                    // Get files from subdirectory                
                    Files = Utils.getFilesFromDirectory(subdir, extension);
                    foreach (string file in Files)
                    {
                        // Verify that xml-doc is accepted by xsd
                        if (Utils.ValidToXSD(this, file))
                        {
                            Check check = new Check(file, extension, this, this.model);
                            checks.Add(check);
                            this.addLineToEAOutput("Check added: ", check.CheckId + " - " + check.CheckDescription);
                        }
                    }
                }
            }
            this.addLineToEAOutput("Finished loading checks.", "");

            // Sort list of checks
            checks = checks.OrderBy(x => x.CheckDescription).ToList<Check>();
        }

        public void clearEAOutput()
        {
            EAOutputLogger.clearLog(this.model, this.outputName);
        }

        public void addLineToEAOutput(string outputline, string parameter)
        {
            EAOutputLogger.log(this.model, this.outputName, string.Format("{0} {1} {2}", DateTime.Now.ToLongTimeString(), outputline, parameter), 0, LogTypeEnum.log);
        }

        public bool ValidateChecks(ucEAValidator uc, List<Check> selectedchecks, TSF_EA.Element EA_element)
        {
            // Clear the log
            clearEAOutput();

            // Check if the Enterprise Architect connection is sql
            var repositoryType = this.model.repositoryType.ToString();
            if (!(this.settings.AllowedRepositoryTypes.Contains(repositoryType)))
            {
                MessageBox.Show("Connectiontype of EA project not allowed: " + repositoryType + Environment.NewLine + "Please connect to another EA project.");
                addLineToEAOutput("Connectiontype of EA project not allowed: ", repositoryType);
                return false;
            }
            addLineToEAOutput("Connected to: ", model.repositoryType.ToString());

            // Check if any checks are selected
            int numberOfChecks = selectedchecks.Count();
            uc.InitProgressbar(numberOfChecks);
            // Check if the Enterprise Architect connection is sql
            addLineToEAOutput("Number of checks to validate: ", numberOfChecks.ToString());
            if (numberOfChecks > 0)
            {
                // Clear list of validations
                validations.Clear();

                // Perform the selected checks and return the validation-results
                addLineToEAOutput("START of Validations...", "");

                // Validate all selected checks
                foreach (var check in selectedchecks)
                {
                    addLineToEAOutput("Validating check: ", check.CheckDescription);

                    validations.AddRange(check.Validate(this, EA_element, uc.getExcludeArchivedPackagesState()));
                    var obj = checks.FirstOrDefault(x => x.CheckId == check.CheckId);
                    if (obj != null) obj.SetStatus(check.Status);

                    uc.IncrementProgressbar();
                }

                addLineToEAOutput("END of Validations.", "");
                addLineToEAOutput("Show list with validation results.", "");
            }

            // If one (or more) check gave an ERROR, then notify the user about it
            var objWithError = checks.FirstOrDefault(x => x.Status == "ERROR");
            if (objWithError != null)
                return false;
            else
                return true;
        }
    }
}
