using EAAddinFramework.EASpecific;
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
using System.Xml.XPath;
using TSF.UmlToolingFramework.UML.Extended;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAValidator
{
    /// <summary>
    /// Class Validation defined to show in objectListView.
    /// </summary>
    public class Check : CheckItem
    {
        const string resolveFunctionName = "resolve";
        private TSF_EA.Model model { get; set; }
        private XDocument xdoc;

        // Check to validate
        // *****************

        private string _checkID = null;
        /// <summary>
        /// Unique Identifier of the check
        /// </summary>
        public string CheckId
        {
            get
            {
                if (this._checkID == null)
                {
                    this._checkID = xdoc.Root.Element("CheckId").Value;
                }
                return this._checkID;
            }
        }
        private string _checkDescription = null;
        /// <summary>
        /// Title of the check
        /// </summary>
        public string CheckDescription 
        {
            get
            {
                if (this._checkDescription == null)
                {
                    this._checkDescription = xdoc.Root.Element("CheckDescription").Value;
                }
                return this._checkDescription;
            }
        }
        private string _queryToFindElements = null;
        /// <summary>
        /// sql-query to search for elements that must be checked
        /// </summary>
        public string QueryToFindElements 
        {
            get
            {
                if (this._queryToFindElements == null)
                {
                    this._queryToFindElements = xdoc.Root.Element("QueryToFindElements").Element("Main").Value;
                }
                return this._queryToFindElements;
            }
        }                                     

        
        private Dictionary<string, string> _queryToFindElementsFilters = null;
        /// <summary>
        /// sql-filters that can be applied to QueryToFindElements
        /// </summary>
        public Dictionary<string, string> QueryToFindElementsFilters 
        { 
            get
            {
                if (this._queryToFindElementsFilters == null)
                {
                    this._queryToFindElementsFilters = new Dictionary<string, string>();
                    foreach (var filterNode in xdoc.Root.Element("QueryToFindElements").Element("Filters")?.Elements())
                    {
                        this._queryToFindElementsFilters.Add(filterNode.Name.LocalName, filterNode.Value);
                    }
                }
                return this._queryToFindElementsFilters;

            }
        }

        private string _queryToCheckFoundElements = null;
        public string QueryToCheckFoundElements
        {
            get
            {
                if (this._queryToCheckFoundElements == null)
                {
                    this._queryToCheckFoundElements = xdoc.Root.Element("QueryToCheckFoundElements").Element("Main").Value;
                }
                return this._queryToCheckFoundElements;
            }
        }
        private Dictionary<string, string> _queryToCheckFoundElementsParameters = null;
        /// <summary>
        /// sql-filters that can be applied to QueryToFindElements
        /// </summary>
        public Dictionary<string, string> QueryToCheckFoundElementsParameters
        {
            get
            {
                if (this._queryToCheckFoundElementsParameters == null)
                {
                    this._queryToCheckFoundElementsParameters = new Dictionary<string, string>();
                    foreach (var parameterNode in xdoc.Root.Element("QueryToCheckFoundElements").Element("Parameters")?.Elements())
                    {
                        this._queryToCheckFoundElementsParameters.Add(parameterNode.Name.LocalName, parameterNode.Value);
                    }
                }
                return this._queryToCheckFoundElementsParameters;
            }
        }


        private string _warningType = null;
        public string WarningType
        {
            get
            {
                if (this._warningType == null)
                {
                    this._warningType = xdoc.Root.Element("WarningType").Value;
                }
                return this._warningType;
            }
        }

        private string _rationale = null;
        public string Rationale
        {
            get
            {
                if (this._rationale == null)
                {
                    this._rationale = xdoc.Root.Element("Rationale").Value;
                }
                return this._rationale;
            }
        }
        private string _proposedSolution = null;
        public string ProposedSolution
        {
            get
            {
                if (this._proposedSolution == null)
                {
                    this._proposedSolution = xdoc.Root.Element("ProposedSolution").Value;
                }
                return this._proposedSolution;
            }
        }
        private string _helpUrl = null;
        public string helpUrl
        {
            get
            {
                if (this._helpUrl == null)
                {
                    this._helpUrl = xdoc.Root.Element("HelpUrl")?.Value;
                    if (string.IsNullOrEmpty(this._helpUrl))
                    {
                        var helpPdf = Path.GetDirectoryName(this.checkfile)
                                      + "\\"
                                      + Path.GetFileNameWithoutExtension(this.checkfile) + ".pdf";

                        if (System.IO.File.Exists(helpPdf))
                        {
                            this._helpUrl = helpPdf;
                        }
                    }
                }
                return this._helpUrl;
            }
        }
        private string _resolveCode = null;
        private string resolveCode
        {
            get
            {
                if (this._resolveCode == null)
                {
                    this._resolveCode = xdoc.Root.Element("ResolveCode")?.Value;
                }
                return this._resolveCode;
            }
        }

        public string Group => this.group?.name;
        private EAValidatorSettings settings { get; set; }
        private CheckGroup group { get; set; }
        public string name => this.CheckDescription;
        
        private Script _resolveScript;
        private Script resolveScript
        {
            get
            {
                if (this._resolveScript == null 
                    && !String.IsNullOrEmpty(this.resolveCode ))
                {
                    var language = xdoc.Root.Element("ResolveCode").Attribute("language").Value;
                    this._resolveScript = new Script(this.CheckId, this.name, "validation scripts", this.resolveCode, language, this.model);
                    this._resolveScript.reloadCode();
                }
                return this._resolveScript;
            }
        }
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

        public string checkfile { get; }
        public bool canBeResolved { get => !string.IsNullOrEmpty(this.resolveCode);}

        public Check(string checkFile, CheckGroup group, EAValidatorSettings settings, TSF_EA.Model model)
        {
            // Constructor
            this.model = model;
            this.settings = settings;
            this.group = group;
            this.checkfile = checkFile;
            //load the contents from the the xml file
            this.loadXml(checkFile);
        }
        private void loadXml(string file)
        {
            string schemaNamespace = "";
            string schemaFileName = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + @"\Files\check.xsd";
            if (!(Utils.FileOrDirectoryExists(schemaFileName)))
            {
                throw new FileNotFoundException("XSD schema not found: ", schemaFileName);
            }
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(schemaNamespace, schemaFileName);
            string filename = new FileInfo(file).Name;
            this.xdoc = XDocument.Load(file);
            xdoc.Validate(schemas, (o, e) => { throw new XmlSchemaValidationException($"Check {filename} is invalid: {e.Message}"); });
        }

        internal bool resolve(string itemGuid)
        {
            bool result = false;
            if (this.resolveScript != null)
            {
                var scriptResult = this.resolveScript.functions.FirstOrDefault(x => x.name == resolveFunctionName)
                    ?.execute(new object[] { itemGuid }) as bool?;
                if (scriptResult == true)
                {
                    //re-evaluate the validation rule
                    var validation = this.ValidateItem(itemGuid);
                    if (validation == null)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public void resetStatus()
        {
            this.Status = CheckStatus.NotValidated;
            this.NumberOfElementsFound = null;
            this.NumberOfValidationResults = null;
        }


       
        public List<Validation> Validate(TSF_EA.Element EA_element, TSF_EA.Diagram EA_diagram, bool excludeArchivedPackages, string scopePackageIDs)
        {
            var validations = new List<Validation>();

            // Default status to Passed
            this.Status = CheckStatus.Passed;
            this.NumberOfElementsFound = null;
            this.NumberOfValidationResults = null;

            // Search elements that need to be checked depending on filters and give back their guids.
            var foundelementguids = this.getElementGuids(EA_element, EA_diagram, excludeArchivedPackages, scopePackageIDs);
            //set the numberOfElementsFound
            this.NumberOfElementsFound = foundelementguids.Count();
            if (this.Status == CheckStatus.Error)
            {
                return validations;
            }

            if (foundelementguids.Any())
            {
                // Perform the checks for the elements found (based on their guids)
                validations = this.CheckFoundElements( foundelementguids);
                this.NumberOfValidationResults = validations.Count();
            }
            else
            {
                this.NumberOfValidationResults = 0;
            }
            return validations;
        }
        public Validation ValidateItem (string itemGUID)
        {
            return this.CheckFoundElements( new List<string> { $"'{itemGUID}'"}).FirstOrDefault();
        }

        private IEnumerable<string> getElementGuids( TSF_EA.Element EA_element, TSF_EA.Diagram EA_diagram, bool excludeArchivedPackages, string scopePackageIDs)
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
                            if (whereclause.Contains(this.settings.PackageBranch))
                            {
                                whereclause = whereclause.Replace(this.settings.PackageBranch, scopePackageIDs);
                            }
                            else
                            {
                                // Replace Search Term with Element guid
                                whereclause = whereclause.Replace(this.settings.SearchTermInQueryToFindElements, EA_element.guid);
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
                            whereclause = whereclause.Replace(this.settings.SearchTermInQueryToFindElements, EA_element.guid);
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
                        whereclause = whereclause.Replace(this.settings.SearchTermInQueryToFindElements, EA_diagram.diagramGUID);
                    }
                    qryToFindElements = qryToFindElements + whereclause;
                }
            }

            if (excludeArchivedPackages)
            {
                qryToFindElements = qryToFindElements + " " + this.settings.QueryExcludeArchivedPackages;
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
        private List<Validation> CheckFoundElements(IEnumerable<string> foundelementguids)
        {
            // Prepare
            var validations = new List<Validation>();

            //split list into chunks of maximum 1000 elements
            foreach (var guidsToCheck in SplitList<String>(foundelementguids.ToList(), 1000))
            {
                // Replace SearchTerm with list of guids
                var qryToCheckFoundElements = this.QueryToCheckFoundElements;
                qryToCheckFoundElements = qryToCheckFoundElements.Replace(this.settings.ElementGuidsInQueryToCheckFoundElements
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
       
    }
}
