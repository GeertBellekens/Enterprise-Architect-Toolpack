using EAAddinFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using TSF.UmlToolingFramework.UML.Extended;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAValidator
{
    /// <summary>
    /// Class Validation defined to show in objectListView.
    /// </summary>
    public class Check : CheckItem
    {
        private TSF_EA.Model model { get; set; }

        // Check to validate
        // *****************
        
        public string CheckId { get; set; }                         // Unique Identifier of the check
        public string CheckDescription { get; set; }                // Title of the check

        
        public string Group => this.group?.name;                          // Group of the check

        public string QueryToFindElements { get; set; }                                     // sql-query to search for elements that must be checked
        public Dictionary<string, string> QueryToFindElementsFilters { get; set; }          // sql-filters that can be applied to QueryToFindElements
                                           

        public string QueryToCheckFoundElements { get; set; }                               // sql-query that performs the check on elements found
        public Dictionary<string, string> QueryToCheckFoundElementsParameters { get; set; } // sql-filters that can be applied to QueryToFindElements
        

        public string WarningType { get; set; }                     // Severity of the impact when problems are found. i.e. error, warning, (information)
        public string Rationale { get; set; }                       // Explanation of the logic of the check
        public string ProposedSolution { get; set; }                // Proposed Solution of the check
        private EAValidatorSettings settings { get; set; }
        private CheckGroup group { get; set; }
        public string name => this.CheckDescription;
        public string helpUrl { get; set; }
        private bool _selected = true;
        public override bool? selected
        {
            get => this._selected;
            set
            {
                if (!value.HasValue)
                {
                    //toggle
                    this._selected = !this._selected;
                }
                else
                {
                    this._selected = value.Value;
                }
            }
        }

        public Check(string checkFile, CheckGroup group, EAValidatorSettings settings, TSF_EA.Model model)
        {

            //validate the xml file
            this.ValidToXSD(checkFile);
            // Constructor
            this.model = model;
            this.settings = settings;
            this.group = group;
            // Initiate the Check
            this.SetDefaultValues();

            // Load file contents into the Check class
            if (Path.GetExtension(checkFile).ToLower() == ".xml")
            {
                // Load xml-document
                var xmldoc = new XmlDocument();
                xmldoc.Load(checkFile);

                // Interprete xml node and subnodes
                XmlNode node = xmldoc.DocumentElement.SelectSingleNode(this.settings.XML_CheckMainNode);
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    this.InterpreteCheckSubNode(subNode);
                }
            }
            //check if helpUrl is filled in. If not then we check if there is a local file with the same name that has a .pdf extension
            this.checkHelpUrl(checkFile);

            // Verify that the check has all mandatory content
            if (!this.HasMandatoryContent())
            {
                EAOutputLogger.log($"XML file does not have all mandatory content. - {checkFile}", 0, LogTypeEnum.error);
            }
        }
        private void checkHelpUrl(string checkFile)
        {
            if (string.IsNullOrEmpty(this.helpUrl))
            {
                var helpPdf = Path.GetDirectoryName(checkFile) 
                              + "\\" 
                              + Path.GetFileNameWithoutExtension(checkFile) + ".pdf";
                
                if (System.IO.File.Exists(helpPdf))
                {
                    this.helpUrl = helpPdf;
                }
            }
        }

        public void resetStatus()
        {
            this.Status = CheckStatus.NotValidated;
            this.NumberOfElementsFound = null;
            this.NumberOfValidationResults = null;
        }
        private void SetDefaultValues()
        {
            this.resetStatus();
            // Defaults
            this.selected = true;
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
                case "helpurl":
                    this.helpUrl = node.InnerText;
                    break;
                default:
                    // do nothing
                    break;
            }
        }

        private bool HasMandatoryContent()
        {
            // Verify that the check has all mandatory content
            if (string.IsNullOrEmpty(this.CheckDescription))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.QueryToFindElements))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.QueryToCheckFoundElements))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.WarningType))
            {
                return false;
            }

            return true;
        }

        public List<Validation> Validate(EAValidatorController controller, TSF_EA.Element EA_element, TSF_EA.Diagram EA_diagram, bool excludeArchivedPackages)
        {
            var validations = new List<Validation>();

            // Default status to Passed
            this.Status = CheckStatus.Passed;
            this.NumberOfElementsFound = null;
            this.NumberOfValidationResults = null;

            // Search elements that need to be checked depending on filters and give back their guids.
            var foundelementguids = this.getElementGuids(controller, EA_element, EA_diagram, excludeArchivedPackages);
            //set the numberOfElementsFound
            this.NumberOfElementsFound = foundelementguids.Count();
            if (this.Status == CheckStatus.Error)
            {
                controller.addLineToEAOutput("- Error while searching elements.", "");
                return validations;
            }

            if (foundelementguids.Any())
            {
                controller.addLineToEAOutput("- Elements found: ", foundelementguids.Count().ToString());
                
                // Perform the checks for the elements found (based on their guids)
                validations = this.CheckFoundElements(controller, foundelementguids);
                this.NumberOfValidationResults = validations.Count();
                if (this.Status == CheckStatus.Error)
                {
                    controller.addLineToEAOutput("- Error while validating found elements.", "");
                }
                controller.addLineToEAOutput("- Validation results found: ", this.NumberOfValidationResults.ToString());
            }
            else
            {
                this.NumberOfValidationResults = 0;
                controller.addLineToEAOutput("- No elements found.", "");
            }
            return validations;
        }

        private IEnumerable<string> getElementGuids(EAValidatorController controller, TSF_EA.Element EA_element, TSF_EA.Diagram EA_diagram, bool excludeArchivedPackages)
        {
            var qryToFindElements = this.QueryToFindElements;
            var foundelementguids = new List<string>();
            // Check EA_element => Change / Release / Package / ...  and add to query
            if (EA_element != null)
            {
                if (!(String.IsNullOrEmpty(EA_element.guid)))
                {
                    string filterType;
                    string whereclause = string.Empty;
                    if (EA_element is TSF_EA.Package)
                    {
                        filterType = "Package";
                        this.QueryToFindElementsFilters.TryGetValue(filterType, out whereclause);
                        if (string.IsNullOrEmpty(whereclause))
                        {
                            this.Status = CheckStatus.Error;
                            return foundelementguids;
                        }
                        else
                        {
                            // Replace Branch with package-guids of branch
                            if (whereclause.Contains(controller.settings.PackageBranch))
                            {
                                whereclause = whereclause.Replace(controller.settings.PackageBranch, controller.scopePackageIDs);
                            }
                            else
                            {
                                // Replace Search Term with Element guid
                                whereclause = whereclause.Replace(controller.settings.SearchTermInQueryToFindElements, EA_element.guid);
                            }
                        }
                    }
                    else
                    {
                        filterType = EA_element.stereotypeNames.FirstOrDefault();
                        this.QueryToFindElementsFilters.TryGetValue(filterType, out whereclause);
                        if (string.IsNullOrEmpty(whereclause))
                        {
                            this.Status = CheckStatus.Error;
                            return foundelementguids;
                        }
                        else
                        {
                            // Replace Search Term with Element guid
                            whereclause = whereclause.Replace(controller.settings.SearchTermInQueryToFindElements, EA_element.guid);
                        }
                    }
                    qryToFindElements = qryToFindElements + whereclause;
                }
            }

            // Check EA_diagram => Use Case diagram (= Functional Design) and add to query
            if (EA_diagram != null)
            {
                if (!(String.IsNullOrEmpty(EA_diagram.diagramGUID)))
                {
                    string filterType;
                    string whereclause = string.Empty;
                    filterType = "FunctionalDesign";
                    this.QueryToFindElementsFilters.TryGetValue(filterType, out whereclause);
                    if (string.IsNullOrEmpty(whereclause))
                    {
                        this.Status = CheckStatus.Error;
                        return foundelementguids;
                    }
                    else
                    {
                        // Replace Search Term with diagram guid
                        whereclause = whereclause.Replace(controller.settings.SearchTermInQueryToFindElements, EA_diagram.diagramGUID);
                    }
                    qryToFindElements = qryToFindElements + whereclause;
                }
            }

            if (excludeArchivedPackages)
            {
                qryToFindElements = qryToFindElements + " " + controller.settings.QueryExcludeArchivedPackages;
            }
            
            try
            {
                // Execute the query using EA
                var elementsFound = this.model.SQLQuery(qryToFindElements);

                // Parse xml document with elements found and count number of elements found
                foreach (XmlNode node in elementsFound.SelectNodes("//Row"))
                {
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode.Name == "ItemGuid")
                        {
                            foundelementguids.Add("'" + subNode.InnerText + "'");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Status = CheckStatus.Error;
            }
            return foundelementguids;
        }
        public static IEnumerable<List<T>> SplitList<T>(List<T> list, int chunkSize)
        {
            for (int i = 0; i < list.Count; i += chunkSize)
            {
                yield return list.GetRange(i, Math.Min(chunkSize, list.Count - i));
            }
        }
        private List<Validation> CheckFoundElements(EAValidatorController controller, IEnumerable<string> foundelementguids)
        {
            // Prepare
            var validations = new List<Validation>();

            //split list into chunks of maximum 1000 elements
            foreach (var guidsToCheck in SplitList<String>(foundelementguids.ToList(), 1000))
            {
                // Replace SearchTerm with list of guids
                var qryToCheckFoundElements = this.QueryToCheckFoundElements;
                qryToCheckFoundElements = qryToCheckFoundElements.Replace(controller.settings.ElementGuidsInQueryToCheckFoundElements
                                                                        , String.Join(",", guidsToCheck));

                // Search for Parameters in query and replace them
                foreach (KeyValuePair<string, string> parameter in this.QueryToCheckFoundElementsParameters)
                {
                    string searchKey = "#" + parameter.Key + "#";
                    qryToCheckFoundElements = qryToCheckFoundElements.Replace(searchKey, parameter.Value);
                }

                try
                {
                    // Execute the query using EA
                    var results = this.model.SQLQuery(qryToCheckFoundElements);

                    // Parse xml document with results and create validation for every row found
                    foreach (XmlNode validationNode in results.SelectNodes("//Row"))
                    {
                        // Set status of Check to FAILED
                        this.Status = CheckStatus.Failed;

                        // Add results of validation to list
                        validations.Add(new Validation(this, validationNode));
                    }
                }
                catch (Exception)
                {
                    this.Status = CheckStatus.Error;
                }
            }

            return validations;
        }
        public bool ValidToXSD(string file)
        {
            bool valid = true;
            string schemaNamespace = "";
            string schemaFileName = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + @"\Files\check.xsd";
            if (!(Utils.FileOrDirectoryExists(schemaFileName)))
            {
                throw new FileNotFoundException("XSD schema not found: ", schemaFileName);
            }
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(schemaNamespace, schemaFileName);
            string filename = new FileInfo(file).Name;
            XDocument doc = XDocument.Load(file);
            doc.Validate(schemas, (o, e) => { throw new XmlSchemaValidationException($"Check {filename} is invalid: {e.Message}"); });
            return valid;
        }
    }
}
