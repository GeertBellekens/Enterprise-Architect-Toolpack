﻿using EAAddinFramework.EASpecific;
using EAAddinFramework.Utilities;
using EAScriptAddin;
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
    public abstract class Check : CheckItem
    {
        const string resolveFunctionName = "resolve";
        protected TSF_EA.Model model { get; set; }
        protected XDocument xdoc;

        public IEnumerable<Validation> validations { get; private set; }
        public IEnumerable<Validation> ignoredValidations { get; private set; } = new List<Validation>();


        // Check to validate
        // *****************

        /// <summary>
        /// Unique Identifier of the check
        /// </summary>
        public string CheckId
        {
            get => this.xdoc.Root.Element("CheckId").Value;
            set => this.xdoc.Root.Element("CheckId").Value = value;
        }
        /// <summary>
        /// Title of the check
        /// </summary>
        public string CheckDescription
        {
            get => this.xdoc.Root.Element("CheckDescription").Value;
            set => this.xdoc.Root.Element("CheckDescription").Value = value;
        }
        /// <summary>
        /// sql-query to search for elements that must be checked
        /// </summary>
        public string QueryToFindElements
        {
            get => this.xdoc.Root.Element("QueryToFindElements").Element("Main").Value;
            set => this.xdoc.Root.Element("QueryToFindElements").Element("Main").Value = value;
        }
        public string packageFilter
        {
            get => this.xdoc.Root.Element("QueryToFindElements").Element("Filters").Element("Package")?.Value;
            set => getOrCreateElement(this.xdoc.Root.Element("QueryToFindElements").Element("Filters"), "Package").Value = value;
        }
        public string changeFilter
        {
            get => this.xdoc.Root.Element("QueryToFindElements").Element("Filters").Element("Change")?.Value;
            set => getOrCreateElement(this.xdoc.Root.Element("QueryToFindElements").Element("Filters"), "Change").Value = value;
        }
        public string releaseFilter
        {
            get => this.xdoc.Root.Element("QueryToFindElements").Element("Filters").Element("Release")?.Value;
            set => getOrCreateElement(this.xdoc.Root.Element("QueryToFindElements").Element("Filters"), "Release").Value = value;
        }
        public string diagramFilter
        {
            get => this.xdoc.Root.Element("QueryToFindElements").Element("Filters").Element("FunctionalDesign")?.Value;
            set => getOrCreateElement(this.xdoc.Root.Element("QueryToFindElements").Element("Filters"), "FunctionalDesign").Value = value;
        }

        public string QueryToCheckFoundElements
        {
            get => this.xdoc.Root.Element("QueryToCheckFoundElements").Element("Main").Value;
            set => this.xdoc.Root.Element("QueryToCheckFoundElements").Element("Main").Value = value;
        }
        public string WarningType
        {
            get => this.xdoc.Root.Element("WarningType").Value;
            set => this.xdoc.Root.Element("WarningType").Value = value;
        }
        public string Rationale
        {
            get => this.xdoc.Root.Element("Rationale").Value;
            set => this.xdoc.Root.Element("Rationale").Value = value;
        }
        public string ProposedSolution
        {
            get => this.xdoc.Root.Element("ProposedSolution").Value;
            set => this.xdoc.Root.Element("ProposedSolution").Value = value;
        }

        public virtual string helpUrl => this.helpUrlText;

        public string helpUrlText
        {
            get => this.xdoc.Root.Element("HelpUrl")?.Value;
            set => getOrCreateElement(this.xdoc.Root, "HelpUrl").Value = value;
        }
        public string resolveCode
        {
            get => this.xdoc.Root.Element("ResolveCode")?.Value;
            set
            {
                if (! string.IsNullOrEmpty(value) 
                    || this.xdoc.Root.Element("ResolveCode") != null)
                {
                    var resolveNode = getOrCreateElement(this.xdoc.Root, "ResolveCode");
                    //make sure to create the attribute as well if needed
                    var languageAttribute = resolveNode.Attribute("language");
                    if (languageAttribute == null)
                    {
                        resolveNode.Add(new XAttribute("language", "VBScript")); 
                    }
                    if (!value.Equals(resolveNode.Value))
                    {
                        resolveNode.Value = value;
                        //reset script
                        this._resolveScript = null;
                    }
                }
            }
        }
        public string resolveCodeLanguage
        {
            get => this.xdoc.Root.Element("ResolveCode")?.Attribute("language")?.Value;
            set
            {
                if (!string.IsNullOrEmpty(this.resolveCode))
                {
                    if (!value.Equals(this.xdoc.Root.Element("ResolveCode").Attribute("language").Value))
                    {
                        this.xdoc.Root.Element("ResolveCode").Attribute("language").Value = value;
                        //reset script
                        this._resolveScript = null;
                    }
                }
            }
        }
        private Dictionary<string, string> _ignoredItems;
        public Dictionary<string, string> ignoredItems
        {
            get
            {
                if (this._ignoredItems == null)
                {
                    this._ignoredItems = new Dictionary<string, string>();
                    foreach (var item in this.xdoc.Root.Elements("IgnoredItem"))
                    {
                        this._ignoredItems.Add(item.Attribute("itemguid").Value, item.Value);
                    }
                }
                return this._ignoredItems;
            }
            set
            {
                this._ignoredItems = value;
                // replace the xml items
                this.xdoc.Root.Elements("IgnoredItem").Remove();
                foreach (var tuple in value)
                {
                    this.xdoc.Root.Add(new XElement("IgnoredItem", tuple.Value, new XAttribute("itemguid", tuple.Key)));
                }
            }
        }
        public void addIgnoredItem(string itemGuid, string reason)
        {
            if (!this.ignoredItems.ContainsKey(itemGuid))
            {
                this.ignoredItems.Add(itemGuid, reason);
                this.xdoc.Root.Add(new XElement("IgnoredItem", reason, new XAttribute("itemguid", itemGuid)));
            }
            else
            {
                this.ignoredItems[itemGuid] = reason;
                this.xdoc.Root.Elements("IgnoredItem").FirstOrDefault(x => x.Attribute("itemguid").Value == itemGuid).Value = reason;
            }
        }
        public void removeIgnoredItem(string itemGuid)
        {
            if (this.ignoredItems.ContainsKey(itemGuid))
            {
                this.ignoredItems.Remove(itemGuid);
                this.xdoc.Root.Elements("IgnoredItem").FirstOrDefault(x => x.Attribute("itemguid").Value == itemGuid).Remove();
            }
        }

        public string Group => this.group?.name;
        protected EAValidatorSettings settings { get; set; }
        public CheckGroup group { get; set; }
        public string name => this.CheckDescription;

        private Script _resolveScript;
        private Script resolveScript
        {
            get
            {
                if (this._resolveScript == null
                    && !String.IsNullOrEmpty(this.resolveCode))
                {
                    var language = xdoc.Root.Element("ResolveCode").Attribute("language").Value;
                    this._resolveScript = new Script(EAScriptAddinAddinClass.globalScriptRepository, this.CheckId, this.name, "validation scripts", this.resolveCode, language, this.model);
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

        
        public bool canBeResolved { get => !string.IsNullOrEmpty(this.resolveCode); }

        public Check(CheckGroup group, EAValidatorSettings settings, TSF_EA.Model model)
        {
            // Constructor
            this.model = model;
            this.settings = settings;
            this.group = group;

        }
        protected abstract string xmlString { get; }
        protected abstract string checkName { get; }
        protected void loadXml()
        {
            string schemaNamespace = "";
            string schemaFileName = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + @"\Files\check.xsd";
            if (!(Utils.FileOrDirectoryExists(schemaFileName)))
            {
                throw new FileNotFoundException("XSD schema not found: ", schemaFileName);
            }
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(schemaNamespace, schemaFileName);
            this.xdoc = XDocument.Parse(this.xmlString);
            xdoc.Validate(schemas, (o, e) => { throw new XmlSchemaValidationException($"Check {this.checkName} is invalid: {e.Message}"); });
        }
        public abstract void save();
        
        internal void reload()
        {
            this.loadXml();
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



        public IEnumerable<Validation> Validate(TSF_EA.Element EA_element, TSF_EA.Diagram EA_diagram, bool excludeArchivedPackages, string scopePackageIDs)
        {
            this.validations = new List<Validation>();

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
                return this.validations;
            }

            if (foundelementguids.Any())
            {
                // Perform the checks for the elements found (based on their guids)
                this.validations = this.CheckFoundElements(foundelementguids);
                this.setIgnoredValidations();
                this.NumberOfValidationResults = validations.Count();
            }
            else
            {
                this.NumberOfValidationResults = 0;
            }
            return validations;
        }
        private void setIgnoredValidations()
        {
            //check if the item is in the ignored items list
           foreach (var validation in this.validations)
           {
                if (this.ignoredItems.ContainsKey(validation.ItemGuid))
                {
                    validation.ignoreReason = this.ignoredItems[validation.ItemGuid];
                    this.ignoredValidations.Append(validation);
                }
           }
           //remove ignored validations from the list of validations
           this.validations = this.validations.Except(this.ignoredValidations);
        }
        public Validation ValidateItem(string itemGUID)
        {
            return this.CheckFoundElements(new List<string> { $"'{itemGUID}'" }).FirstOrDefault();
        }

        private IEnumerable<string> getElementGuids(TSF_EA.Element EA_element, TSF_EA.Diagram EA_diagram, bool excludeArchivedPackages, string scopePackageIDs)
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
                        whereclause = this.packageFilter;
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
                        if (filterType == "Change")
                        {
                            whereclause = this.changeFilter;
                        }
                        else if (filterType == "Release")
                        {
                            whereclause = this.releaseFilter;
                        }
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
                    string whereclause = this.diagramFilter;
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
        private static XElement getOrCreateElement(XContainer container, string name)
        {
            var element = container.Element(name);
            if (element == null)
            {
                element = new XElement(name);
                container.Add(element);
            }
            return element;
        }

    }
}
