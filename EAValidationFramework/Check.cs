using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.Windows.Forms;
using EAAddinFramework.Utilities;

namespace EAValidationFramework
{
    /// <summary>
    /// Class Validation defined to show in objectListView.
    /// </summary>
    public class Check
    {
        private TSF_EA.Model model { get; set; }

        // Check to validate
        // *****************
        public bool Selected { get; set; }                          // Value for Checkbox in list
        public string CheckId { get; set; }                         // Unique Identifier of the check
        public string CheckDescription { get; set; }                // Title of the check
        public string Status { get; set;  }                         // Status of the check  (Not validated / Passed / Failed)
        public string Group { get; set; }                           // Group of the check

        public string QueryToFindElements { get; set; }                                     // sql-query to search for elements that must be checked
        public Dictionary<string, string> QueryToFindElementsFilters { get; set; }          // sql-filters that can be applied to QueryToFindElements
        public string NumberOfElementsFound { get; set; }                                   // Total number of elements found 

        public string QueryToCheckFoundElements { get; set; }                               // sql-query that performs the check on elements found
        public Dictionary<string, string> QueryToCheckFoundElementsParameters { get; set; } // sql-filters that can be applied to QueryToFindElements
        public string NumberOfValidationResults { get; set; }                               // Number of Validation Results found (using Query)

        public string WarningType { get; set; }                     // Severity of the impact when problems are found. i.e. error, warning, (information)
        public string Rationale { get; set; }                       // Explanation of the logic of the check
        public string ProposedSolution { get; set; }                // Proposed Solution of the check
        
        public Check(string file, string extension, EAValidatorController controller, TSF_EA.Model model)
        {
            // Constructor
            this.model = model;

            // Initiate the Check
            SetDefaultValues();

            // Load file contents into the Check class
            switch (extension)
            {
                case "xml":
                    // Checks are grouped per directory
                    string path = Path.GetDirectoryName(file);
                    this.Group = path.Substring(path.LastIndexOf("\\") + 1);

                    // Load xml-document
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(file);

                    // Interprete xml node and subnodes
                    XmlNode node = xmldoc.DocumentElement.SelectSingleNode(controller.settings.XML_CheckMainNode);
                    foreach(XmlNode subNode in node.ChildNodes)
                    {
                        InterpreteCheckSubNode(subNode);                        
                    }
                    break;

                default:
                    break;
            }

            // Verify that the check has all mandatory content
            if (!this.HasMandatoryContent())
            {
                MessageBox.Show("XML file does not have all mandatory content." + " - " + file);
            }
        }
        
        public void SetStatus(string newstatus)
        {
            switch(newstatus)
            {
                case "Not Validated":
                    this.Status = newstatus;
                    break;
                case "Passed":
                    this.Status = newstatus;
                    break;
                case "Failed":
                    this.Status = newstatus;
                    break;
                case "ERROR":
                    this.Status = newstatus;
                    break;
                default:
                    // Don't change
                    break;
            }
        }

        private void SetDefaultValues()
        {
            // Defaults
            this.Selected = true;
            this.SetStatus("Not Validated");
            this.NumberOfElementsFound = "";
            this.NumberOfValidationResults = "";
            this.CheckId = "0";
            this.CheckDescription = "";
            this.QueryToFindElements = "";
            this.QueryToCheckFoundElements = "";
            this.WarningType = "";
            this.Rationale = "";
            this.ProposedSolution = "";
            this.QueryToFindElementsFilters = new Dictionary<string, string>();
            this.QueryToCheckFoundElementsParameters = new Dictionary<string, string>();
        }

        private void InterpreteCheckSubNode(XmlNode node)
        {
            switch (node.Name.ToLower())
            {
                case "checkid":
                    this.CheckId = node.InnerText;
                    break;
                case "checkdescription":
                    this.CheckDescription = node.InnerText;
                    break;
                case "querytofindelements":
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        switch (subNode.Name.ToLower())
                        {
                            case "main":
                                this.QueryToFindElements = Utils.ReplaceXMLCharsInQuery(subNode.InnerText);
                                break;
                            case "filters":
                                foreach (XmlNode filterNode in subNode.ChildNodes)
                                {
                                    this.QueryToFindElementsFilters.Add(filterNode.Name, Utils.ReplaceXMLCharsInQuery(filterNode.InnerText));
                                }
                                break;
                            default:
                                // do nothing
                                break;
                        }
                    }
                    break;
                case "querytocheckfoundelements":
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        switch (subNode.Name.ToLower())
                        {
                            case "main":
                                this.QueryToCheckFoundElements = Utils.ReplaceXMLCharsInQuery(subNode.InnerText);
                                break;
                            case "parameters":
                                foreach (XmlNode parameterNode in subNode.ChildNodes)
                                {
                                    this.QueryToCheckFoundElementsParameters.Add(parameterNode.Name, Utils.ReplaceXMLCharsInQuery(parameterNode.InnerText));
                                }
                                break;
                            default:
                                // do nothing
                                break;
                        }
                    }
                    break;
                case "warningtype":
                    this.WarningType = node.InnerText;
                    break;
                case "rationale":
                    this.Rationale = node.InnerText;
                    break;
                case "proposedsolution":
                    this.ProposedSolution = node.InnerText;
                    break;
                default:
                    // do nothing
                    break;
            }
        }

        private bool HasMandatoryContent()
        {
            // Verify that the check has all mandatory content
            if (string.IsNullOrEmpty(this.CheckDescription)) return false;
            if (string.IsNullOrEmpty(this.QueryToFindElements)) return false;
            if (string.IsNullOrEmpty(this.QueryToCheckFoundElements)) return false;
            if (string.IsNullOrEmpty(this.WarningType)) return false;
            return true;
        }

        public List<Validation> Validate(EAValidatorController controller, TSF_EA.Element EA_element, bool excludeArchivedPackages)
        {
            var validations = new List<Validation>();
            
            // Default status to Passed
            this.SetStatus("Passed");
            this.NumberOfElementsFound = "";
            this.NumberOfValidationResults = "";

            // Search elements that need to be checked depending on filters and give back their guids.
            var foundelementguids = getElementGuids(controller, EA_element, excludeArchivedPackages);
            if(this.Status == "ERROR")
            {
                controller.addLineToEAOutput("- Error while searching elements.", "");
            }
            else if (foundelementguids.Length > 0)
            {
                controller.addLineToEAOutput("- Elements found: ", this.NumberOfElementsFound);
                
                foundelementguids = foundelementguids.Substring(1);   // remove first ","
                // Perform the checks for the elements found (based on their guids)
                validations = CheckFoundElements(controller, foundelementguids);
            }
            controller.addLineToEAOutput("- Validation results found: ", this.NumberOfValidationResults);
            return validations;
        }

        private string getElementGuids(EAValidatorController controller, TSF_EA.Element EA_element, bool excludeArchivedPackages)
        {
            var qryToFindElements = this.QueryToFindElements;
            int numberOfElementsFound = 0;

            // Check EA_element => Change / Release / Package / ...  and add to query
            if (EA_element != null)
            {                
                if (!(String.IsNullOrEmpty(EA_element.guid)))
                {
                    string filterType;
                    if (EA_element is TSF_EA.Package)
                    {
                        filterType = "Package";
                    }
                    else
                    {
                        filterType = EA_element.stereotypeNames.FirstOrDefault();
                    }
                    string whereclause = string.Empty;
                    this.QueryToFindElementsFilters.TryGetValue(filterType, out whereclause);
                    if (string.IsNullOrEmpty(whereclause))
                    {
                        //MessageBox.Show("Filter not found for check: " + Environment.NewLine + filterType + " -> " + this.CheckDescription);
                        this.SetStatus("ERROR");
                        return "";
                    }
                    else
                    {
                        whereclause = whereclause.Replace(controller.settings.SearchTermInQuery, EA_element.guid);
                        qryToFindElements = qryToFindElements + whereclause;
                    }
                }
            }
            if (excludeArchivedPackages)
            {
                qryToFindElements = qryToFindElements + " " + controller.settings.QueryExcludeArchivedPackages;
            }

            var foundelementguids = "";
            try
            {
                var elementsFound = this.model.SQLQuery(qryToFindElements);
                
                // Parse xml document with elements found and count number of elements found
                foreach (XmlNode node in elementsFound.SelectNodes("//Row"))
                {
                    numberOfElementsFound += 1;
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode.Name == "ItemGuid")
                            foundelementguids += ",'" + subNode.InnerText + "'";
                    }
                }
                this.NumberOfElementsFound = numberOfElementsFound.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error in query: " + qryToFindElements);
                this.SetStatus("ERROR");
            }
            return foundelementguids;
        }

        private List<Validation> CheckFoundElements(EAValidatorController controller, string foundelementguids)
        {
            // Prepare
            var validations = new List<Validation>();
            int numberOfValidationResults = 0;            

            // Replace SearchTerm with list of guids
            var qryToCheckFoundElements = this.QueryToCheckFoundElements.Replace(controller.settings.SearchTermInQuery, foundelementguids);

            // Search for Parameters in query and replace them
            foreach(KeyValuePair<string, string> parameter in this.QueryToCheckFoundElementsParameters)
            {
                string searchKey = "#" + parameter.Key + "#";
                qryToCheckFoundElements = qryToCheckFoundElements.Replace(searchKey, parameter.Value);
            }
            
            try
            {
                var results = this.model.SQLQuery(qryToCheckFoundElements);

                // Parse xml document with results and create validation for every row found
                foreach (XmlNode validationNode in results.SelectNodes("//Row"))
                {
                    // Set status of Check to FAILED
                    this.SetStatus("Failed");

                    // Add results of validation to list
                    validations.Add(new Validation(this, validationNode));
                    numberOfValidationResults += 1;
                }
                this.NumberOfValidationResults = numberOfValidationResults.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error in query: " + qryToCheckFoundElements);
                this.SetStatus("ERROR");
            }

            return validations;
        }
    }
}
