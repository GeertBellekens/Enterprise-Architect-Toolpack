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
    public class Check
    {
        private TSF_EA.Model model { get; set; }

        // Check to validate
        // *****************
        public bool Selected { get; set; }                          // Value for Checkbox in list
        public string CheckId { get; set; }                         // Unique Identifier of the check
        public string CheckDescription { get; set; }                // Title of the check
        public string Status { get; set; }                         // Status of the check  (Not validated / Passed / Failed)
        public string Group => this.group?.name;                          // Group of the check

        public string QueryToFindElements { get; set; }                                     // sql-query to search for elements that must be checked
        public Dictionary<string, string> QueryToFindElementsFilters { get; set; }          // sql-filters that can be applied to QueryToFindElements
        public string NumberOfElementsFound { get; set; }                                   // Total number of elements found 

        public string QueryToCheckFoundElements { get; set; }                               // sql-query that performs the check on elements found
        public Dictionary<string, string> QueryToCheckFoundElementsParameters { get; set; } // sql-filters that can be applied to QueryToFindElements
        public string NumberOfValidationResults { get; set; }                               // Number of Validation Results found (using Query)

        public string WarningType { get; set; }                     // Severity of the impact when problems are found. i.e. error, warning, (information)
        public string Rationale { get; set; }                       // Explanation of the logic of the check
        public string ProposedSolution { get; set; }                // Proposed Solution of the check
        private EAValidatorSettings settings { get; set; }
        private CheckGroup group { get; set; }

        public Check(string file, CheckGroup group, EAValidatorSettings settings, TSF_EA.Model model)
        {

            //validate the xml file
            this.ValidToXSD(file);
            // Constructor
            this.model = model;
            this.settings = settings;
            this.group = group;
            // Initiate the Check
            this.SetDefaultValues();

            // Load file contents into the Check class
            switch (Path.GetExtension(file))
            {
                case "xml":
                    
                    // Load xml-document
                    var xmldoc = new XmlDocument();
                    xmldoc.Load(file);

                    // Interprete xml node and subnodes
                    XmlNode node = xmldoc.DocumentElement.SelectSingleNode(this.settings.XML_CheckMainNode);
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        this.InterpreteCheckSubNode(subNode);
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
            switch (newstatus)
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
            this.SetStatus("Passed");
            this.NumberOfElementsFound = "";
            this.NumberOfValidationResults = "";

            // Search elements that need to be checked depending on filters and give back their guids.
            var foundelementguids = this.getElementGuids(controller, EA_element, EA_diagram, excludeArchivedPackages);
            if (this.Status == "ERROR")
            {
                controller.addLineToEAOutput("- Error while searching elements.", "");
                return validations;
            }

            if (foundelementguids.Length > 0)
            {
                controller.addLineToEAOutput("- Elements found: ", this.NumberOfElementsFound);

                foundelementguids = foundelementguids.Substring(1);   // remove first ","
                // Perform the checks for the elements found (based on their guids)
                validations = this.CheckFoundElements(controller, foundelementguids);
                if (this.Status == "ERROR")
                {
                    controller.addLineToEAOutput("- Error while validating found elements.", "");
                }
                controller.addLineToEAOutput("- Validation results found: ", this.NumberOfValidationResults);
            }
            else
            {
                this.NumberOfValidationResults = "0";
                controller.addLineToEAOutput("- No elements found.", "");
            }
            return validations;
        }

        private string getElementGuids(EAValidatorController controller, TSF_EA.Element EA_element, TSF_EA.Diagram EA_diagram, bool excludeArchivedPackages)
        {
            var qryToFindElements = this.QueryToFindElements;
            int numberOfElementsFound = 0;

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
                            this.SetStatus("ERROR");
                            return "";
                        }
                        else
                        {
                            // Replace Branch with package-guids of branch
                            if (whereclause.Contains(controller.settings.PackageBranch))
                            {
                                whereclause = whereclause.Replace(controller.settings.PackageBranch, this.getBranchPackageIDsByGuid(EA_element.guid));
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
                            this.SetStatus("ERROR");
                            return "";
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
                        this.SetStatus("ERROR");
                        return "";
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

            var foundelementguids = "";
            try
            {
                // Execute the query using EA
                var elementsFound = this.model.SQLQuery(qryToFindElements);

                // Parse xml document with elements found and count number of elements found
                foreach (XmlNode node in elementsFound.SelectNodes("//Row"))
                {
                    numberOfElementsFound += 1;
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode.Name == "ItemGuid")
                        {
                            foundelementguids += ",'" + subNode.InnerText + "'";
                        }
                    }
                }
                this.NumberOfElementsFound = numberOfElementsFound.ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in query: " + qryToFindElements);
                this.SetStatus("ERROR");
            }
            return foundelementguids;
        }

        private string getBranchPackageIDsByGuid(string packageguid)
        {
            // Get query to select Package guids 9 levels deep of selected package
            string qrybranchselectids = "SELECT p1.Package_ID" +
                                       " FROM t_package AS p1" +
                                       " WHERE p1.ea_guid = '" + packageguid + "'" +
                                       " UNION ALL" +
                                       " SELECT p2.Package_ID FROM (t_package AS p1 " +
                                       " LEFT JOIN t_package p2 ON p2.Parent_ID = p1.Package_ID)" +
                                       " WHERE p1.ea_guid = '" + packageguid + "'" +
                                       " UNION ALL" +
                                       " SELECT p3.Package_ID FROM ((t_package AS p1 " +
                                       " LEFT JOIN t_package p2 ON p2.Parent_ID = p1.Package_ID)" +
                                       " LEFT JOIN t_package p3 ON p3.Parent_ID = p2.Package_ID)" +
                                       " WHERE p1.ea_guid = '" + packageguid + "'" +
                                       " UNION ALL" +
                                       " SELECT p4.Package_ID FROM (((t_package AS p1 " +
                                       " LEFT JOIN t_package p2 ON p2.Parent_ID = p1.Package_ID)" +
                                       " LEFT JOIN t_package p3 ON p3.Parent_ID = p2.Package_ID)" +
                                       " LEFT JOIN t_package p4 ON p4.Parent_ID = p3.Package_ID)" +
                                       " WHERE p1.ea_guid = '" + packageguid + "'" +
                                       " UNION ALL" +
                                       " SELECT p5.Package_ID FROM ((((t_package AS p1 " +
                                       " LEFT JOIN t_package p2 ON p2.Parent_ID = p1.Package_ID)" +
                                       " LEFT JOIN t_package p3 ON p3.Parent_ID = p2.Package_ID)" +
                                       " LEFT JOIN t_package p4 ON p4.Parent_ID = p3.Package_ID)" +
                                       " LEFT JOIN t_package p5 ON p5.Parent_ID = p4.Package_ID)" +
                                       " WHERE p1.ea_guid = '" + packageguid + "'" +
                                       " UNION ALL" +
                                       " SELECT p6.Package_ID FROM (((((t_package AS p1 " +
                                       " LEFT JOIN t_package p2 ON p2.Parent_ID = p1.Package_ID)" +
                                       " LEFT JOIN t_package p3 ON p3.Parent_ID = p2.Package_ID)" +
                                       " LEFT JOIN t_package p4 ON p4.Parent_ID = p3.Package_ID)" +
                                       " LEFT JOIN t_package p5 ON p5.Parent_ID = p4.Package_ID)" +
                                       " LEFT JOIN t_package p6 ON p6.Parent_ID = p5.Package_ID)" +
                                       " WHERE p1.ea_guid = '" + packageguid + "'" +
                                       " UNION ALL" +
                                       " SELECT p7.Package_ID FROM ((((((t_package AS p1 " +
                                       " LEFT JOIN t_package p2 ON p2.Parent_ID = p1.Package_ID)" +
                                       " LEFT JOIN t_package p3 ON p3.Parent_ID = p2.Package_ID)" +
                                       " LEFT JOIN t_package p4 ON p4.Parent_ID = p3.Package_ID)" +
                                       " LEFT JOIN t_package p5 ON p5.Parent_ID = p4.Package_ID)" +
                                       " LEFT JOIN t_package p6 ON p6.Parent_ID = p5.Package_ID)" +
                                       " LEFT JOIN t_package p7 ON p7.Parent_ID = p6.Package_ID)" +
                                       " WHERE p1.ea_guid = '" + packageguid + "'" +
                                       " UNION ALL" +
                                       " SELECT p8.Package_ID FROM (((((((t_package AS p1 " +
                                       " LEFT JOIN t_package p2 ON p2.Parent_ID = p1.Package_ID)" +
                                       " LEFT JOIN t_package p3 ON p3.Parent_ID = p2.Package_ID)" +
                                       " LEFT JOIN t_package p4 ON p4.Parent_ID = p3.Package_ID)" +
                                       " LEFT JOIN t_package p5 ON p5.Parent_ID = p4.Package_ID)" +
                                       " LEFT JOIN t_package p6 ON p6.Parent_ID = p5.Package_ID)" +
                                       " LEFT JOIN t_package p7 ON p7.Parent_ID = p6.Package_ID)" +
                                       " LEFT JOIN t_package p8 ON p8.Parent_ID = p7.Package_ID)" +
                                       " WHERE p1.ea_guid = '" + packageguid + "'" +
                                       " UNION ALL" +
                                       " SELECT p9.Package_ID FROM ((((((((t_package AS p1 " +
                                       " LEFT JOIN t_package p2 ON p2.Parent_ID = p1.Package_ID)" +
                                       " LEFT JOIN t_package p3 ON p3.Parent_ID = p2.Package_ID)" +
                                       " LEFT JOIN t_package p4 ON p4.Parent_ID = p3.Package_ID)" +
                                       " LEFT JOIN t_package p5 ON p5.Parent_ID = p4.Package_ID)" +
                                       " LEFT JOIN t_package p6 ON p6.Parent_ID = p5.Package_ID)" +
                                       " LEFT JOIN t_package p7 ON p7.Parent_ID = p6.Package_ID)" +
                                       " LEFT JOIN t_package p8 ON p8.Parent_ID = p7.Package_ID)" +
                                       " LEFT JOIN t_package p9 ON p9.Parent_ID = p8.Package_ID)" +
                                       " WHERE p1.ea_guid = '" + packageguid + "'";

            return qrybranchselectids;
        }

        private List<Validation> CheckFoundElements(EAValidatorController controller, string foundelementguids)
        {
            // Prepare
            var validations = new List<Validation>();
            int numberOfValidationResults = 0;

            // Replace SearchTerm with list of guids
            var qryToCheckFoundElements = this.QueryToCheckFoundElements;
            qryToCheckFoundElements = qryToCheckFoundElements.Replace(controller.settings.ElementGuidsInQueryToCheckFoundElements, foundelementguids);

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
                    this.SetStatus("Failed");

                    // Add results of validation to list
                    validations.Add(new Validation(this, validationNode));
                    numberOfValidationResults += 1;
                }
                this.NumberOfValidationResults = numberOfValidationResults.ToString();
            }
            catch (Exception)
            {
                //MessageBox.Show("Error in query: " + qryToCheckFoundElements);
                this.SetStatus("ERROR");
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
