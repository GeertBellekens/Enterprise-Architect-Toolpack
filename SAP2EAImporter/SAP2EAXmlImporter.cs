using EAAddinFramework.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    class SAP2EAXmlImporter
    {
        const string SapAuthorizationObject = "SUSO";
        const string SapSingleRole = "singleRole";
        const string SapCompositeRole = "compositeRole";
        const string SapRolePackage = "RP";
        const string SapFunctionModule = "FUNC";
        const string SapUsercategory = "ORG";
        const string SapBopf = "BOBF";
        const string SapClass = "CLAS";
        const string outputName = "SAP2EAImporter"; //TODO move to settings
        private XDocument xDoc { get; set; }
        private UMLEA.Model model { get; set; }
        private Dictionary<string, SingleRole> singleRoles;
        private Dictionary<string, AuthorizationObject> authorizationOjects;
        private Dictionary<string, RolePackage> rolePackages;
        private Dictionary<string, FunctionModule> functionModules;
        private Dictionary<string, UserCategory> userCategories;

        public void import(UML.Classes.Kernel.Package selectedPackage)
        {
            // initialize dictionaries
            this.singleRoles = new Dictionary<string, SingleRole>();
            this.authorizationOjects = new Dictionary<string, AuthorizationObject>();
            this.rolePackages = new Dictionary<string, RolePackage>();
            this.functionModules = new Dictionary<string, FunctionModule>();
            this.userCategories = new Dictionary<string, UserCategory>();
            if (selectedPackage == null) return;
            this.model = (UMLEA.Model)selectedPackage.model;
            EAOutputLogger.clearLog(this.model, outputName);

            //1. Let user select the xml file.
            //-------------------------
            var browseImportFileDialog = new OpenFileDialog();
            browseImportFileDialog.Filter = "SAP import files|*.xml";
            browseImportFileDialog.FilterIndex = 1;
            browseImportFileDialog.Multiselect = false;
            var dialogResult = browseImportFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                // Load file. XDoc attribute filled.
                this.loadFile(browseImportFileDialog.FileName);
            }

            //2. Parse the selected xml file.
            // ----------------------------
            if (this.xDoc != null)
            {
                //3. Process content xml file
                // -----------------------------
                EAOutputLogger.log(this.model, outputName
                              , $" Starting import of file '{browseImportFileDialog.FileName}' in package '{selectedPackage.name}'"
                              , ((UMLEA.ElementWrapper)selectedPackage).id // can be used to navigate if the user doubleclick on the line in the log
                             , LogTypeEnum.log);
                this.processXDoc(selectedPackage);
                EAOutputLogger.log(this.model, outputName
                              , $" Finished import of file '{browseImportFileDialog.FileName}' in package '{selectedPackage.name}'"
                              , ((UMLEA.ElementWrapper)selectedPackage).id
                             , LogTypeEnum.log);

                var errorCount = EAOutputLogger.getErrors(this.model, outputName).Count();
                if (errorCount > 0)
                {
                    MessageBox.Show($"{errorCount} Errors found Importing SAP data. Check System Output", "Error Importing SAP data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }
        /// <summary>
        ///  Processes the packages within the system(ex: R3, CRM, WAS, ...) package
        /// </summary>
        /// <param name="selectedPackage"> The EA package in which the sap data are to be imported. </param>
        private void processXDoc(UML.Classes.Kernel.Package selectedPackage)
        {
            // Parameters descriptions:
            //SelectedPackage: The xml package containing metadata to be imported from SAP

            var systemName = this.xDoc.Root.Attribute("system").Value; // Ex: R3
                                                                       // Using the systemName, get the corresponding package in EA. If a package with the given system name does not exist, 
                                                                       // Create one in the selected package. everything in the input xml is imported in the system package.
            //TODO: Add system name to settings
            var systemPackage = getPackage(systemName, selectedPackage, "Bibliotheek Technisch");

            foreach (var packageNode in this.xDoc.Root.Elements("package") ?? Array.Empty<XElement>()) // packages
            {
                // Process package nodes
                this.processPackageNode(packageNode, systemPackage);
            }
        }

        /// <summary>
        /// Process the package nodes under a given system node.
        /// </summary>
        /// <param name="packageNode"> the xml package node to be processed</param>
        /// <param name="package"> The system node in which the packages will be processed in EA.</param>
        private void processPackageNode(XElement packageNode, UML.Classes.Kernel.Package package)
        {
            /*<exporter system="R3">
            < package name = "ZS_BEV_USR_R3" >

                 < element name = "ZC_ALT_FLW" type = "SUSO" tabclass = "" >
                    < notes > Campus management: Alternatieve flow in toepassingen </ notes >
                    < auth_field field1 = "Z_TOEP" />
                    < auth_field field2 = "Z_ACTVT" />     
                </element >
                    ...
            </package>
          */
            var packageName = packageNode.Attribute("name").Value;
            EAOutputLogger.log(this.model, outputName
                              , $" Processing package '{packageName}' in package '{package.name}'"
                              , ((UMLEA.ElementWrapper)package).id
                             , LogTypeEnum.log);

            // Find get the package with packageName from EA. If this package does not exist, it will be created.
            var pack = this.getPackageUnderPackage(packageName, package);



            // Process the elements in the package.
            foreach (var elementNode in packageNode.Elements("element") ?? Array.Empty<XElement>())
            {
                this.processElementNode(elementNode, pack);
            }

            //process the packages in the package
            foreach (var subPackageNode in packageNode.Elements("package") ?? Array.Empty<XElement>()) // packages.
            {
                // Process package nodes
                this.processPackageNode(subPackageNode, pack);
            }

        }
        /// <summary>
        /// Returns EA package element with given name under a given package.
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="parentPackage"></param>
        /// <returns></returns>
        private UML.Classes.Kernel.Package getPackageUnderPackage(string packageName, UML.Classes.Kernel.Package parentPackage)
        {
            var sqlSelectPackage = $@"SELECT o.Object_ID
                                    FROM t_object o
                                    INNER JOIN t_package p ON o.ea_guid = p.ea_guid
                                    WHERE p.Name = '{packageName}'
                                    and p.Parent_ID in ( {((UMLEA.Package)parentPackage).packageTreeIDString})";

            var package = (UML.Classes.Kernel.Package)this.model.getElementWrappersByQuery(sqlSelectPackage).FirstOrDefault();

            if (package == null)
            {
                package = ((UMLEA.Package)parentPackage).addOwnedElement<UML.Classes.Kernel.Package>(packageName);
                package.save();
            }

            return package;
        }


        /// <summary>
        /// Process xml elements such as authorization objects, roles, ...
        /// </summary>
        /// <param name="elementNode"> xml element node to be processed</param>
        /// <param name="package"> the EA package in which the element will be created or updated in EA</param>
        private void processElementNode(XElement elementNode, UML.Classes.Kernel.Package package)
        {
            var elementName = elementNode.Attribute("name").Value;
            EAOutputLogger.log(this.model, outputName
                            , $" Processing element '{elementName}' in package '{package.name}'"
                            , ((UMLEA.ElementWrapper)package).id
                            , LogTypeEnum.log);

            // Depending op the type of the element, it will be processed differently.
            switch (elementNode.Attribute("type").Value)
            {
                case SapAuthorizationObject:
                    processAuthorizationObject(elementNode, package);
                    break;
                case SapCompositeRole:
                    processCompositeRole(elementNode, package);
                    break;
                case SapSingleRole:
                    processSingleRole(elementNode, package);
                    break;
                case SapRolePackage:
                    processRolePackage(elementNode, package);
                    break;
                case SapFunctionModule:
                    processFunctionModule(elementNode, package);
                    break;
                case SapUsercategory:
                    processUserCategories(elementNode, package);
                    break;
                case SapBopf:
                    processBopf(elementNode, package);
                    break;
                case SapClass:
                    processClass(elementNode, package);
                    break;
            }

        }

        private SAPClass processClass(XElement elementNode, UML.Classes.Kernel.Package package)
        {
            var elementName = elementNode.Attribute("name").Value;
            var sapClass = new SAPClass(elementName, package);
            // Import notes
            var notesNode = elementNode.Elements("notes").FirstOrDefault();
            if (notesNode != null)
            {
                sapClass.notes = notesNode.Value;
            }
            sapClass.save();
            //process attributes
            this.processAttributes<UMLEA.Class>(elementNode, sapClass);
            return sapClass;

        }
        private void processAttributes<T>(XElement elementNode, SAPElement<T> owner) where T : UMLEA.ElementWrapper
        {
            //<Attribute name="MC_ISTAT" datatype="ISTAT_D" visibility="Protected" ownerScope="Constant" changeable="frozen">
            //	<notes>Planningsstatus</notes>
            //</Attribute>
            var attributePos = 0;
            foreach (var attributeNode in elementNode.Elements("Attribute") ?? Array.Empty<XElement>())
            {
                attributePos++;
                var attributeName = attributeNode.Attribute("name").Value;
                var key = attributeNode.Attribute("key")?.Value;
                var datatype = attributeNode.Attribute("datatype")?.Value;
                var notes = attributeNode.Elements("notes").FirstOrDefault()?.Value;
                //TODO: ownerScope and changeable, add to profile?
                owner.addOrUpdateAttribute(attributeName, key, string.Empty, notes, datatype, attributePos);
            }
        }
        private BOPFBusinessObject processBopf(XElement elementNode, UML.Classes.Kernel.Package package)
        {
            //<element name="ZA_TEXT_COLLECTION" type="BOBF" tabclass="" key="7EAE1BA4330B1EE5849B1B023574921A">
            //<notes>ZA - Tekstverzameling (herbruikbaar)</notes>
            //<object_category>Dependent Object</object_category>
            //<bo_has_auth_check>false</bo_has_auth_check>
            //<const_interface>ZA_IF_TEXT_COLLECTION_C</const_interface>
            //<status_class/>
            //<status_derivator/>
            //<data_access_class/>
            var elementName = elementNode.Attribute("name").Value;
            var key = elementNode.Attribute("key").Value;
            var businessObject = new BOPFBusinessObject(elementName, package, key);
            // Import notes
            var notesNode = elementNode.Elements("notes").FirstOrDefault();
            if (notesNode != null)
            {
                businessObject.notes = notesNode.Value;
            }
            //object category
            var objectCategoryNode = elementNode.Elements("object_category").FirstOrDefault();
            if (objectCategoryNode != null)
            {
                businessObject.objectCategory = objectCategoryNode.Value;
            }
            //TODO: implement once the UML profile is adapted
            //var hasAuthCheckNode = elementNode.Elements("bo_has_auth_check").FirstOrDefault();
            //if (hasAuthCheckNode != null)
            //{
            //    businessObject.hasAuthorizationCheck = hasAuthCheckNode.Value.Equals("True", StringComparison.InvariantCultureIgnoreCase)
            //}

            //TODO: other properties

            //save 
            businessObject.save();

            //Process Nodes
            processNodes(elementNode, businessObject);

            return businessObject;
        }

        private void processNodes(XElement elementNode, BOPFNodeOwner nodeOwner)
        {
            //<node name="TEXT_COLLECTION_HDR" key="7EAE1BA4330B1EE5849B1B0393DD921A">
            //<node_properties>
            //	<notes/>
            //	<node_settings>
            //		<auth_check_relevant>false</auth_check_relevant>
            //		<auth_class/>
            //		<node_type>Standard Node</node_type>
            //		<is_transient>false</is_transient>
            //		<datamodel>
            //			<data_structure>ZAS_TEXT_COLLECTION_HDR_D</data_structure>
            //			<Transient_structure/>
            //			<combined_structure>ZAS_TEXT_COLLECTION_HDR_K</combined_structure>
            //			<Combined_table_type>ZATT_TEXT_COLLECTION_HDR_D</Combined_table_type>
            //			<implementation>
            //				<node_class/>
            //			</implementation>
            //		</datamodel>
            //		<data_access>
            //			<database_table>ZAT_TEXT_COLLECT</database_table>
            //		</data_access>
            //	</node_settings>
            //</node_properties>
            //<node_elements>

            //process the NodeNodes
            foreach (var nodeNode in elementNode.Elements("node") ?? Array.Empty<XElement>())
            {
                var elementName = nodeNode.Attribute("name").Value;
                var key = nodeNode.Attribute("key").Value;
                var node = nodeOwner.addNode(elementName, key);

                // Import notes
                var notesNode = nodeNode.Element("node_properties")?.Element("notes");
                if (notesNode != null)
                {
                    node.notes = notesNode.Value;
                }
                //node type
                node.nodeType = nodeNode.Element("node_properties")?.Element("node_settings")?.Element("node_type")?.Value;
                //is transient
                node.isTransient = "true".Equals(nodeNode.Element("node_properties")?.Element("node_settings")?.Element("is_transient")?.Value, StringComparison.InvariantCultureIgnoreCase);
                //node links
                var datamodelNode = nodeNode.Element("node_properties")?.Element("node_settings")?.Element("datamodel");
                //combined structure
                var combinedStructureName = datamodelNode?.Element("combined_structure")?.Value;
                if (!string.IsNullOrEmpty(combinedStructureName))
                {
                    node.combinedStructure = new SAPDatatype(combinedStructureName, nodeOwner.elementWrapper.owningPackage);
                }
                //data_structure 
                var dataStructureName = datamodelNode?.Element("data_structure")?.Value;
                if (!string.IsNullOrEmpty(dataStructureName))
                {
                    node.dataStructure = new SAPDatatype(dataStructureName, nodeOwner.elementWrapper.owningPackage);
                }
                //transient structure 
                var transientStructureName = datamodelNode?.Element("Transient_structure")?.Value;
                if (!string.IsNullOrEmpty(transientStructureName))
                {
                    node.transientStructure = new SAPDatatype(transientStructureName, nodeOwner.elementWrapper.owningPackage);
                }
                //combined Table Type
                var combinedTableTypeName = datamodelNode?.Element("Combined_table_type")?.Value;
                if (!string.IsNullOrEmpty(combinedTableTypeName))
                {
                    node.combinedTableType = new SAPDatatype(combinedTableTypeName, nodeOwner.elementWrapper.owningPackage);
                }
                //node class
                var nodeClassName = datamodelNode?.Element("implementation")?.Element("node_class")?.Value;
                if (!string.IsNullOrEmpty(nodeClassName))
                {
                    node.nodeClass = new SAPClass(nodeClassName, nodeOwner.elementWrapper.owningPackage);
                }
                //check class
                var checkClassName = datamodelNode?.Element("auth_class")?.Value;
                if (!string.IsNullOrEmpty(checkClassName))
                {
                    node.checkClass = new SAPClass(checkClassName, nodeOwner.elementWrapper.owningPackage);
                }
                //Database table
                var databaseTableName = nodeNode.Element("node_properties")?.Element("node_settings")?.Element("data_access")?.Element("database_table")?.Value;
                if (!string.IsNullOrEmpty(databaseTableName))
                {
                    node.databaseTable = new SAPTable(databaseTableName, nodeOwner.elementWrapper.owningPackage);
                }


                node.save();
                //process subnodes
                this.processNodes(nodeNode, node);

                //  'import associations
                processAssociations(nodeNode, node);
                //'import determinations
                processDeterminations(nodeNode, node);
                //'import validations
                processValidations(nodeNode, node);
                //'import actions
                processActions(nodeNode, node);
                //'import queries
                processQueries(nodeNode, node);
                //'import altkeys
                processAltKeys(nodeNode, node);
                //'import authorisations
                processNodeAuthorizations(nodeNode, node);


            }
        }
        private void processNodeAuthorizations(XElement nodeNode, BOPFNode node)
        {
            //<authorization>
            //	<auth_object name="ZS_DELEG">
            //		<auth_field>ZS_TOEWTYP</auth_field>
            //		<node_attribute>TOEW_TYPE_ID</node_attribute>
            //		<Association>00000000000000000000000000000000</Association>
            //	</auth_object>
            //</authorization>
            foreach (var authorizationObjectNode in nodeNode.Element("node_elements")?. Element("authorization")?.Elements("auth_object") ?? Array.Empty<XElement>())
            {
                var authName = authorizationObjectNode.Attribute("name").Value;
                var authFieldName = authorizationObjectNode.Element("auth_field")?.Value;
                var authNodeAttributeName = authorizationObjectNode.Element("node_attribute")?.Value;
                var authorizationObject = new AuthorizationObject(authName, node.elementWrapper.owningPackage);
                if ( authorizationObject != null)
                {
                    var authorizationCheck = new BOPFAuthorizationCheck(node, authorizationObject);
                    authorizationCheck.constraint = $"{node.name}.{authNodeAttributeName} = {authName}.{authFieldName}";
                }
            }
        }
        private void processAltKeys(XElement nodeNode, BOPFNode node)
        {
            //<altkeys>
            //	<altkey name="HOST_PARENT" key="7EAE1BA4330B1ED5849B3C9FD476D55F">
            //		<notes>Alt. key voor bovenl. knooppunt host</notes>
            //		<altkey_settings>
            //			<altkey_unique>N</altkey_unique>
            //			<implementation>
            //				<data_type>/BOBF/S_LIB_K_DELEGATION</data_type>
            //				<data_table_type>/BOBF/T_LIB_K_DELEGATION</data_table_type>
            //			</implementation>
            //		</altkey_settings>
            //	</altkey>
            //</altkeys>
            foreach (var altkeyNode in nodeNode.Element("node_elements")?.Element("altkeys")?.Elements("altkey") ?? Array.Empty<XElement>())
            {
                var altkeyName = altkeyNode.Attribute("name").Value;
                var altkeyKey = altkeyNode.Attribute("key").Value;
                var altkey = new BOPFAlternativeKey(altkeyName, node, altkeyKey);
                //set notes
                var notesNode = altkeyNode.Element("notes");
                if (notesNode != null)
                {
                    altkey.notes = notesNode.Value;
                }
                //altkey_unique
                var altKeyUnique = altkeyNode.Element("altkey_settings")?.Element("altkey_unique")?.Value;
                if (altKeyUnique != null) altkey.unique = altKeyUnique;
                //datatype
                var datatypeClassName = altkeyNode.Element("implementation")?.Element("data_type")?.Value;
                if (!string.IsNullOrEmpty(datatypeClassName))
                {
                    altkey.dataType = new SAPClass(datatypeClassName, node.elementWrapper.owningPackage);
                }
                else
                {
                    altkey.dataType = null;
                }
                //datatype type
                var dataTypeTypeName = altkeyNode.Element("implementation")?.Element("data_table_type")?.Value;
                if (!string.IsNullOrEmpty(datatypeClassName))
                {
                    altkey.dataTableType = new SAPDatatype(dataTypeTypeName, node.elementWrapper.owningPackage);
                }
                else
                {
                    altkey.dataTableType = null;
                }
            }
        }
        private void processQueries(XElement nodeNode, BOPFNode node)
        {
            //<queries>
            //    <query name="SELECT_BY_ATTRIBUTES" key="7EAE1BA4330B1ED592DF2E8CEF0EC600">
            //	    <notes/>
            //	    <qry_settings/>
            //	    <implementation>
            //		    <query_class/>
            //		    <filter_structure>ZAS_TOEWIJZINGSTYPE_GROEP_D</filter_structure>
            //	    </implementation>
            //    </query>
            foreach (var queryNode in nodeNode.Element("node_elements")?.Element("queries")?.Elements("query") ?? Array.Empty<XElement>())
            {
                var queryName = queryNode.Attribute("name").Value;
                var queryKey = queryNode.Attribute("key").Value;
                var query = new BOPFQuery(queryName, node, queryKey);
                //set notes
                var notesNode = queryNode.Element("notes");
                if (notesNode != null)
                {
                    query.notes = notesNode.Value;
                }
                //Filter Structure
                var filterStructureName = queryNode.Element("implementation")?.Element("filter_structure")?.Value;
                if (!string.IsNullOrEmpty(filterStructureName))
                {
                    query.filterStructure = new SAPDatatype(filterStructureName, node.elementWrapper.owningPackage);
                }
                else
                {
                    query.filterStructure = null;
                }
            }
            //TODO: result structure, result table type (last one exists as tagged value in the profile, but not in xml files?)
        }
        private void processActions(XElement nodeNode, BOPFNode node)
        {
            //<actions>
            //	<action name="SELECT_ALL_BRONOBJ" key="7EAE1BA4330B1ED695ACBADBE6115AFC">
            //		<notes>alle gefilterde bronobjecten selecteren</notes>
            //		<action_settings>
            //			<cardinality>Multiple Node Instances</cardinality>
            //			<implementation>
            //				<action_class>ZA_CL_TOEWIJZINGSTYPE_ACT</action_class>
            //				<filter_structure/>
            //			</implementation>
            //		</action_settings>
            //	</action>
            foreach (var actionNode in nodeNode.Element("node_elements")?.Element("actions")?.Elements("action") ?? Array.Empty<XElement>())
            {
                var actionName = actionNode.Attribute("name").Value;
                var actionKey = actionNode.Attribute("key").Value;
                var action = new BOPFAction(actionName, node, actionKey);
                //set notes
                var notesNode = actionNode.Element("notes");
                if (notesNode != null)
                {
                    action.notes = notesNode.Value;
                }
                //Category and actionclass from action_settings
                action.cardinality = actionNode.Element("action_settings")?.Element("cardinality")?.Value;
                //TODO: action class and filter structure (to be added in profile)
                //var actionClassName = actionNode.Element("action_settings")?.Element("implementation")?.Element("action_class")?.Value;
                //if (!string.IsNullOrEmpty(actionClassName))
                //{
                //    action.actionClass = new SAPClass(actionClassName, action.elementWrapper.owningPackage);
                //}
                //else
                //{
                //    action.actionClass = null;
                //}
            }

        }
        private void processValidations(XElement nodeNode, BOPFNode node)
        {
            //<validations>
            //<validation name="CHECK_TITEL_NL" key="7EAE1BA4330B1ED59CE236A35BFDD2CD">
            //	<notes/>
            //	<validation_settings>
            //		<val_class>ZA_CL_TEXT_COLLECTION_VAL</val_class>
            //		<val_category>Action Check</val_category>
            //	</validation_settings>
            //	<trigger_actions>
            //		<node_category>
            //			<trigger_action>SAVE_TEXT_CONTENT</trigger_action>
            //		</node_category>
            //	</trigger_actions>
            //</validation>
            foreach (var validationNode in nodeNode.Element("node_elements")?.Element("validations")?.Elements("validation") ?? Array.Empty<XElement>())
            {
                var validationName = validationNode.Attribute("name").Value;
                var validationKey = validationNode.Attribute("key").Value;
                var validation = new BOPFValidation(validationName, node, validationKey);
                //set notes
                var notesNode = validationNode.Element("notes");
                if (notesNode != null)
                {
                    validation.notes = notesNode.Value;
                }
                //Category and validationclass from validation_settings
                validation.category = validationNode.Element("validation_settings")?.Element("val_category")?.Value;
                var validationClassName = validationNode.Element("validation_settings")?.Element("val_class")?.Value;
                if (!string.IsNullOrEmpty(validationClassName))
                {
                    validation.validationClass = new SAPClass(validationClassName, validation.elementWrapper.owningPackage);
                }
                else
                {
                    validation.validationClass = null;
                }
                //trigger actions
                foreach (var triggerActionNode in validationNode.Element("trigger_actions")?.Element("node_category")?.Elements("trigger_action") ?? Array.Empty<XElement>())
                {
                    var triggerActionName = triggerActionNode.Value;
                    if (!string.IsNullOrEmpty(triggerActionName))
                    {
                        //get the trigger action
                        var triggerAction = new BOPFAction(triggerActionName, node, "");
                        //create the relation between them
                        new BOPFActionValidationTrigger (validation, triggerAction);
                        //TODO: ActionValidation has a nodeCategory property, but that doesn't seem to be present in the xml
                    }
                }
            }
        }
        private void processDeterminations(XElement nodeNode, BOPFNode node)
        {
            //<node_elements>
            //<determinations>
            //	<determination name = "SET_READ_ONLY" key="7EAE1BA4330B1EE5A3A12E773D7B8AEA">
            //		<notes/>
            //		<determination_settings>
            //			<det_category>Transient</det_category>
            //			<det_class>ZA_CL_TEXT_COLLECTION_DET</det_class>
            //		</determination_settings>
            //		<trigger_conditions>
            //			<trigger_condition Trigger_node_bo = "ZA_TEXT_COLLECTION" Trigger_node="TEXT_CONTENT" Trigger_node_key="7EAE1BA4330B1ED5849BBA0133FA8059" Create="false" Update="false" Delete="false" Determine="false" Load="true"/>
            //		</trigger_conditions>
            //		<evaluation_timepoints>
            //			<evaluation_timepoint before_retrieve = "false" after_loading="True" after_creation="false" after_change="false" after_deletion="false" after_modification="false" after_validation="false" before_save_finalize="false" after_commit="false" after_failed_save_attempt="false" during_save_before_writing_data="false" before_save_draw_numbers="false" before_save_before_consistency_check="false" check_and_determine_before_consistency_check="false" cleanup="false" node_category="TEXT_CONTENT"/>
            //		</evaluation_timepoints>
            //		<necessary_determinations/>
            //		<dependent_determinations/>
            //	</determination>
            //</determinations>
            foreach (var determinationNode in nodeNode.Element("node_elements")?.Element("determinations")?.Elements("determination") ?? Array.Empty<XElement>())
            {
                var determinationName = determinationNode.Attribute("name").Value;
                var determinationKey = determinationNode.Attribute("key").Value;
                var determination = new BOPFDetermination(determinationName, node, determinationKey);
                //set notes
                var notesNode = determinationNode.Element("notes");
                if (notesNode != null)
                {
                    determination.notes = notesNode.Value;
                }
                //trigger conditions
                foreach (var triggerNode in determinationNode.Element("trigger_conditions")?.Elements("trigger_condition") ?? Array.Empty<XElement>())
                {
                    var triggerNodeName = triggerNode.Attribute("Trigger_node").Value;
                    var triggerBOName = triggerNode.Attribute("Trigger_node_bo").Value;
                    var triggerNodeKey = triggerNode.Attribute("Trigger_node_key").Value;
                    var triggerBOPFNodeBO = new BOPFBusinessObject(triggerBOName, node.wrappedElement.owningPackage, String.Empty);
                    var triggerBOPFNode = new BOPFNode(triggerNodeName, triggerBOPFNodeBO, triggerNodeKey);
                    //create the determinationTriggeredBy relation to the triggerBOPFNode
                    var triggerConnector = new BOPFDeterminationTrigger(determination, triggerBOPFNode);
                    //read trigger points
                    triggerConnector.triggersOnCreate = this.getAttributeBoolValue(triggerNode, "Create");
                    triggerConnector.triggersOnUpdate = this.getAttributeBoolValue(triggerNode, "Update");
                    triggerConnector.triggersOnDelete = this.getAttributeBoolValue(triggerNode, "Delete");
                    triggerConnector.triggersOnLoad = this.getAttributeBoolValue(triggerNode, "Load");
                    triggerConnector.triggersOnDetermine = this.getAttributeBoolValue(triggerNode, "Determine");
                }
                //evaluation timepoints
                var evaluationTimepointNode = determinationNode.Element("evaluation_timepoints")?.Element("evaluation_timepoint");
                if (evaluationTimepointNode != null)
                {
                    determination.evaluateBeforeRetrieve = getAttributeBoolValue(evaluationTimepointNode, "before_retrieve");
                    determination.evaluateAfterLoading = getAttributeBoolValue(evaluationTimepointNode, "after_loading");
                    //determination.eva.. = getAttributeBoolValue(evaluationTimepointNode, "after_creation"); TODO
                    //determination.eva.. = getAttributeBoolValue(evaluationTimepointNode, "after_change"); TODO
                    //determination.eva.. = getAttributeBoolValue(evaluationTimepointNode, "after_deletion"); TODO
                    determination.evaluateAfterModify = getAttributeBoolValue(evaluationTimepointNode, "after_modification");
                    determination.evaluateAfterValidation = getAttributeBoolValue(evaluationTimepointNode, "after_validation");
                    determination.evaluateBeforeSaveFinalize = getAttributeBoolValue(evaluationTimepointNode, "before_save_finalize");
                    determination.evaluateAfterCommit = getAttributeBoolValue(evaluationTimepointNode, "after_commit");
                    determination.evaluateAfterFailedSave = getAttributeBoolValue(evaluationTimepointNode, "after_failed_save_attempt");
                    determination.evaluateDuringSave = getAttributeBoolValue(evaluationTimepointNode, "during_save_before_writing_data");
                    determination.evaluateBeforeSaveDrawNumbers = getAttributeBoolValue(evaluationTimepointNode, "before_save_draw_numbers");
                    determination.evaluateBeforeSaveBeforeConsistency = getAttributeBoolValue(evaluationTimepointNode, "before_save_before_consistency_check");
                    determination.evaluateDuringCheckAndDetermine = getAttributeBoolValue(evaluationTimepointNode, "check_and_determine_before_consistency_check");
                    determination.evaluateCleanup = getAttributeBoolValue(evaluationTimepointNode, "cleanup");
                }
                //Category and determinationclass from determination_settings
                determination.category = determinationNode.Element("determination_settings")?.Element("det_category")?.Value;
                var determinationClassName = determinationNode.Element("determination_settings")?.Element("det_class")?.Value;
                if (! string.IsNullOrEmpty(determinationClassName))
                {
                    determination.determinationClass = new SAPClass(determinationClassName, determination.elementWrapper.owningPackage);
                }
                else
                {
                    determination.determinationClass = null;
                }
                //dependencies to other determinations
                //<necessary_determinations>
				//  <determination name="SET_ENDDATE_31_12_9999"/>
				//</necessary_determinations>
                foreach (var dependingDeterminationNode in determinationNode.Element("necessary_determinations")?.Elements("determination") ?? Array.Empty<XElement>())
                {
                    var dependingDeterminationName = dependingDeterminationNode.Attribute("name")?.Value;
                    if (!string.IsNullOrEmpty(determinationName))
                    {
                        //get the depending determination
                        var dependingDetermination = new BOPFDetermination(dependingDeterminationName, node, "");
                        //create the relation between them
                        new BOPFDeterminationDependency(determination, dependingDetermination);
                    }
                }
            }
        }
        private bool getAttributeBoolValue (XElement node, string attributeName)
        {
            return "True".Equals(node.Attribute(attributeName)?.Value, StringComparison.InvariantCultureIgnoreCase);
        }
        private void processAssociations(XElement nodeNode, BOPFNode sourceNode)
        {
            //<node_elements>
            //<static_properties/>
            //<associations>
            //	<association name="TO_TITELS" key="7EAE1BA4330B1ED5849B9268CC18955F">
            //		<notes>Titels van het hosting BO</notes>
            //		<assoc_settings>
            //			<cardinality>1:0..*</cardinality>
            //			<target_bo>ZA_TEXT_COLLECTION</target_bo>
            //			<target_node target_node_key="7EAE1BA4330B1ED5849B697B6CB18E1D">TEXT_HDR</target_node>
            //			<resolving_node>Target Node</resolving_node>
            //			<assoc_cat>Specialization association</assoc_cat>
            //			<assoc_type>Association</assoc_type>
            //			<implementation>
            //				<assoc_class/>
            //				<filter_structure/>
            //			</implementation>
            //		</assoc_settings>
            //	</association>
            foreach (var associationNode in nodeNode.Element("node_elements")?.Element("associations")?.Elements("association") ?? Array.Empty<XElement>())
            {
                var assocationName = associationNode.Attribute("name").Value;
                var associationKey = associationNode.Attribute("key").Value;
                var targetBOName = associationNode.Element("assoc_settings")?.Element("target_bo")?.Value;
                var targetNodeName = associationNode.Element("assoc_settings").Element("target_node")?.Value;
                var targetKeyName = associationNode.Element("assoc_settings").Element("target_node")?.Attribute("target_node_key")?.Value;

                //get target BO
                var targetBO = new BOPFBusinessObject(targetBOName, sourceNode.wrappedElement.owningPackage, null);
                var targetNode = new BOPFNode(targetNodeName, targetBO, targetKeyName);
                //get association
                var association = new SAPAssociation(sourceNode, targetNode, assocationName,associationKey );
                //set notes
                var notesNode = associationNode.Element("notes");
                if (notesNode != null)
                {
                    association.notes = notesNode.Value;
                }
                //settings
                var settingsNode = associationNode.Element("assoc_settings");
                if (settingsNode != null)
                {
                    //cardinality
                    var cardinalityString = settingsNode.Element("cardinality")?.Value;
                    var cardinalityParts = cardinalityString.Split(':');
                    if (cardinalityParts.Length > 0 && ! string.IsNullOrEmpty(cardinalityParts[0]))
                    {
                        try
                        {
                            association.sourceMultiplicity = cardinalityParts[0];
                        }
                        catch(FormatException)
                        {
                            EAOutputLogger.log(this.model, outputName
                            , $" Cannot convert '{cardinalityParts[0]}' into source multiplicity for association '{assocationName}' from node '{sourceNode.name}' to '{targetNode.name}'"
                            , sourceNode.elementWrapper.id
                            , LogTypeEnum.error);
                        }
                    }
                    if (cardinalityParts.Length > 1 && !string.IsNullOrEmpty(cardinalityParts[1]))
                    {
                        try
                        {
                            association.targetMultiplicity = cardinalityParts[1];
                        }
                        catch (FormatException)
                        {
                            EAOutputLogger.log(this.model, outputName
                            , $" Cannot convert '{cardinalityParts[1]}' into target multiplicity for association '{assocationName}' from node '{sourceNode.name}' to '{targetNode.name}'"
                            , sourceNode.elementWrapper.id
                            , LogTypeEnum.error);
                        }
                    }
                    //resolving node
                    var resolvingNode = settingsNode.Element("resolving_node")?.Value;
                    if (! string.IsNullOrEmpty(resolvingNode))
                    {
                        association.resolvingNode = resolvingNode;
                    }
                    //category
                    var category = settingsNode.Element("assoc_cat")?.Value;
                    if (!string.IsNullOrEmpty(category))
                    {
                        association.category = category;
                    }
                    //Association Class
                    var associationClassName = settingsNode?.Element("implementation")?.Element("assoc_class")?. Value;
                    if (!string.IsNullOrEmpty(associationClassName))
                    {
                        association.associationClass = new SAPClass(associationClassName, association.source.elementWrapper.owningPackage);
                    }
                }
                association.save();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementNode"></param>
        /// <param name="package"> The package in which the authorization object will be created.</param>
        /// <returns></returns>
        private AuthorizationObject processAuthorizationObject(XElement elementNode, UML.Classes.Kernel.Package package)
        {
            //-<element name="ZS_CBOX_CF" type="SUSO">
            //<notes>chatbox config</notes>
            //<auth_field name="field1">ACTVT</auth_field>
            //<auth_field name="field2">BO_SERVICE</auth_field>
            //<auth_field name="field3">ZA_BONAME</auth_field>
            //<auth_field name="field4">ZA_CONFIG</auth_field>
            //<auth_class>ZSAU</auth_class>
            //</element>
            var elementName = elementNode.Attribute("name").Value;
            var authorizationObject = new AuthorizationObject(elementName, package);

            //add to list of authorizationOBjects, used by authorizations
            this.authorizationOjects.Add(authorizationObject.name, authorizationObject);

            // Import notes
            var notesNode = elementNode.Elements("notes").FirstOrDefault();
            if (notesNode != null)
            {
                authorizationObject.notes = notesNode.Value;
            }

            // Import authorization fields.
            foreach (var authFieldNode in elementNode.Elements("auth_field"))
            {
                authorizationObject.addAuthorizationField(authFieldNode.Value);
            }

            // Import authorization class
            var authorizationClassNode = elementNode.Elements("auth_class").FirstOrDefault();
            if (authorizationClassNode != null)
            {
                authorizationObject.authorizationClass = authorizationClassNode.Value;
            }

            authorizationObject.save();
            return authorizationObject;
        }

        private void processRolePackage(XElement elementNode, UML.Classes.Kernel.Package package)
        {
            //<element name="ZS_RANOREX" type="RP">
            //	<notes/>
            //	<assignment>
            //		<orgunit name="50375977"/>
            //		<orgunit name="50509797"/>
            //	</assignment>
            //</element>
            var elementName = elementNode.Attribute("name").Value;
            var rolePackage = new RolePackage(elementName, package);

            //add to dictionary
            this.rolePackages.Add(rolePackage.name, rolePackage);

            // Import notes
            var notesNode = elementNode.Elements("notes").FirstOrDefault();
            if (notesNode != null)
            {
                rolePackage.notes = notesNode.Value;
            }
            //save rolePackage
            rolePackage.save();
            //process the orgunit nodes as User Categories
            foreach (var userCategoryNode in elementNode.Element("assignment")?.Elements("orgunit") ?? Array.Empty<XElement>())
            {
                var userCategoryName = userCategoryNode.Attribute("name").Value;

                // Get the userCategory with tthis name
                UserCategory userCategory;
                if (this.userCategories.TryGetValue(userCategoryName, out userCategory))
                {
                    // Create a relation between the RolePakcage and the user category
                    userCategory.addRolePackage(rolePackage);
                }

            }
        }

        private void processUserCategories(XElement elementNode, UML.Classes.Kernel.Package package)
        {
            //<package name="User Categories">
            //	<element name="Dienst Administratieve Toepassingen voor Algemeen Beheer" type="ORGUNIT">
            //	</element>
            //</package>
            //get the orgunit nodes and create for each of them a user category that can be linked to this role package.
            var elementName = elementNode.Attribute("name").Value;
            var userCategory = new UserCategory(elementName, package);

            //add to dictionary
            this.userCategories.Add(userCategory.name, userCategory);

            //save functionModule
            userCategory.save();
        }

        private void processFunctionModule(XElement elementNode, UML.Classes.Kernel.Package package)
        {
            //<element name="Z_S_GET_ACTIVE_EMPLOYEES" type="FUNC">
            //	<notes>Ophalen actieve personeelsleden</notes>
            //	<parameters>
            //		<parameter name="EX_USERIDS" datatype="ZSTT_USERIDS">
            //			<direction>out</direction>
            //		</parameter>
            //	</parameters>
            //</element>
            var elementName = elementNode.Attribute("name").Value;
            var functionModule = new FunctionModule(elementName, package);

            //add to dictionary
            this.functionModules.Add(functionModule.name, functionModule);

            // Import notes
            var notesNode = elementNode.Elements("notes").FirstOrDefault();
            if (notesNode != null)
            {
                functionModule.notes = notesNode.Value;
            }
            //save functionModule
            functionModule.save();
            // process parameters as ports
            foreach (var parameterNode in elementNode.Element("parameters")?.Elements("parameter") ?? Array.Empty<XElement>())
            {
                var parameterName = parameterNode.Attribute("name").Value;
                var parameterDatatypeType = parameterNode.Attribute("datatype").Value;
                var parameterDirection = parameterNode.Elements("direction").FirstOrDefault()?.Value;
                //add the parameter to the functionModule
                functionModule.addParameter(parameterName, parameterDatatypeType, parameterDirection);
            }


        }
        private void processSingleRole(XElement elementNode, UML.Classes.Kernel.Package package)
        {
            //       <element name="Y3CSTATA__" type="singleRole">
            //<short_description>CM_RG: Statuten transacties</short_description>
            //<notes></notes>
            //<authorizations>
            //	<authorization name="Y3CSTATA__00" auth_object="S_START">
            //		<auth_field name="AUTHOBJNAM">
            //			<field_value>ZC_FACILITEIT_SET_FPM</field_value>
            //			<field_value>ZC_RG_BEHEER_FACILITEIT_FPM_AP</field_value>
            //			<field_value>ZC_STATUUT_OPVOLGEN_FPM</field_value>
            //		</auth_field>
            var elementName = elementNode.Attribute("name").Value;
            var singleRole = new SingleRole(elementName, package);

            // Add singleRole to the singleRoles. The list is used to link with the aggregating composite role.
            singleRoles.Add(singleRole.name, singleRole);

            //Import SAP role short description
            var shortDescriptionNode = elementNode.Elements("short_description").FirstOrDefault();
            if (shortDescriptionNode != null)
            {
                singleRole.shortDescription = shortDescriptionNode.Value;
            }

            // Import notes
            var notesNode = elementNode.Elements("notes").FirstOrDefault();
            if (notesNode != null)
            {
                singleRole.notes = notesNode.Value;
            }
            //save singleRole
            singleRole.save();

            // Import authorizations
            var authorizationsNode = elementNode.Elements("authorizations").FirstOrDefault();
            processAuthorizations(authorizationsNode, singleRole);
            //process assignments
            processAssignments(elementNode, singleRole);

        }
        private void processAuthorizations(XElement authorizationsNode, SingleRole singleRole)
        {
            //	<authorizations>
            //		<authorization name="Y3SGWPRU__00">
            //			<auth_field name="S_ADMI_FCD">
            //				<field_value>SUM</field_value>
            //			</auth_field>
            //		</authorization>
            //	</authorizations>
            if (authorizationsNode != null)
            {
                foreach (var authorizationNode in authorizationsNode.Elements("authorization") ?? Array.Empty<XElement>())
                {
                    //get authorizationObject
                    var authorizationObjectName = authorizationNode.Attribute("auth_object").Value;

                    AuthorizationObject authorizationObject;
                    if (!this.authorizationOjects.TryGetValue(authorizationObjectName, out authorizationObject))
                    {
                        //log error
                        EAOutputLogger.log($"AuthorizationObject with name '{authorizationObjectName}' not found in file", 0, LogTypeEnum.error);
                        //continue next
                        continue;
                    }

                    // authorization name (from xml)
                    var authorizationName = authorizationNode.Attribute("name").Value;

                    //create authorization 
                    var authorization = new Authorization(authorizationName, singleRole, authorizationObject);
                    var authorizationFields = new Dictionary<string, string>();
                    //process authorization fields
                    foreach (var authFieldNode in authorizationNode.Elements("auth_field") ?? Array.Empty<XElement>())
                    {
                        var authFieldName = authFieldNode.Attribute("name").Value;
                        //get all values and concatenate with ","
                        var values = new List<string>();
                        foreach (var valueNode in authFieldNode.Elements("field_value") ?? Array.Empty<XElement>())
                        {
                            values.Add(valueNode.Value);
                        }
                        //add to dictionary
                        if (!authorizationFields.ContainsKey(authFieldName))
                        {
                            authorizationFields.Add(authFieldName, string.Join(",", values.ToArray()));
                        }
                    }
                    //set authorizationfields
                    authorization.authorizationFields = authorizationFields;
                    //save
                    authorization.save();
                }
            }
        }

        private void processCompositeRole(XElement elementNode, UML.Classes.Kernel.Package package)
        {
            //<element name="Y1CSTATA__" type="compositeRole">
            //< short_description > CM_ST: Statuten transacties</ short_description >
            //	<notes>CM_ST: Statuten transacties</notes>
            //	<single_roles>
            //		<single_role name="Y3CSTATA__"/>
            //		<single_role name="Y7SCX04U__"/>
            //	</single_roles>
            //  <assignment>
            //	<group name="ACTIVE_PERS">
            //		<function_module name="Z_S_GET_ACTIVE_EMPLOYEES"/>
            //	</group>
            //	<role_package name="ZS_RANOREX"/>
            //</assignment>
            //</element>

            var elementName = elementNode.Attribute("name").Value;
            var compositeRole = new CompositeRole(elementName, package);

            var shortDescriptionNode = elementNode.Elements("short_description").FirstOrDefault();
            if (shortDescriptionNode != null)
            {
                compositeRole.shortDescription = shortDescriptionNode.Value;
            }

            // Import notes
            var notesNode = elementNode.Elements("notes").FirstOrDefault();
            if (notesNode != null)
            {
                compositeRole.notes = notesNode.Value;
            }
            compositeRole.save();
            // Import the aggregated single roles.
            foreach (var singleRoleNode in elementNode.Element("single_roles")?.Elements("single_role") ?? Array.Empty<XElement>())
            {
                var singleRoleName = singleRoleNode.Attribute("name").Value;

                // Get the singleRole with name 'singleRoleName'
                SingleRole singleRole;
                if (this.singleRoles.TryGetValue(singleRoleName, out singleRole))
                {

                    // Create an aggregation relation between the single and its aggregating composite role.
                    compositeRole.addSingleRole(singleRole);
                }
            }
            //process assignments
            processAssignments(elementNode, compositeRole);

        }
        private void processAssignments(XElement elementNode, Role role)
        {
            //  <assignment>
            //	<group name="ACTIVE_PERS">
            //		<function_module name="Z_S_GET_ACTIVE_EMPLOYEES"/>
            //	</group>
            //	<role_package name="ZS_RANOREX"/>
            //</assignment>
            //</element>
            var assignmentNode = elementNode.Elements("assignment").FirstOrDefault();
            if (assignmentNode == null)
                return;
            //process functionModules in their groups
            foreach (var groupNode in assignmentNode.Elements("group") ?? Array.Empty<XElement>())
            {
                var groupName = groupNode.Attribute("name").Value;

                foreach (var functionModuleNode in groupNode.Elements("function_module") ?? Array.Empty<XElement>())
                {
                    var functionModuleName = functionModuleNode.Attribute("name").Value;
                    FunctionModule functionModule;
                    if (this.functionModules.TryGetValue(functionModuleName, out functionModule))
                    {
                        functionModule.addRole(role, groupName);
                    }
                }
            }
            //process rolePackages
            foreach (var rolePackageNode in assignmentNode.Elements("role_package") ?? Array.Empty<XElement>())
            {
                var rolePackageName = rolePackageNode.Attribute("name").Value;
                RolePackage rolePackage;
                if (this.rolePackages.TryGetValue(rolePackageName, out rolePackage))
                {
                    rolePackage.addRole(role);
                }
            }
        }



        private void loadFile(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                var reader = new XmlTextReader(new FileStream(fileName, FileMode.Open,
                                           FileAccess.Read, FileShare.ReadWrite));

                // parse file into XDocument
                this.xDoc = XDocument.Load(reader);


            }
        }
        /// <summary>
        /// called to get EA package element under a given parent package.
        /// If no package is found there, a new package will be created under the selected package.
        /// </summary>
        /// <param name="packageName">the name of the package to get</param>
        /// <param name="package">the selected package in which a package will be created if package with packageName is not found</param>
        /// <param name="parentPackageName">the name of the parent package of the selected package.</param>
        private UML.Classes.Kernel.Package getPackage(string packageName, UML.Classes.Kernel.Package package, string parentPackageName)
        {
            // Get the package with name systemName(eg: R3) within 'Bibliotheek Technisch'.
            // If the package is not found, create it. (Create also package 'Bibliotheek Technisch' )
            // TODO: make setting for 'bibliotheek technisch' to avoid hard coding.
            string sqlGetPackage = $@"SELECT o.Object_ID
                                            FROM t_package p
                                            inner join t_object o on p.ea_guid = o.ea_guid
                                            left join t_package pp on p.Parent_ID = pp.Package_ID
                                            where p.Name = '{packageName}' ";
            if (parentPackageName != null)
            {
                sqlGetPackage += $"and pp.Name = '{parentPackageName}'";
            }



            var containerPackage = (UML.Classes.Kernel.Package)this.model.getElementWrappersByQuery(sqlGetPackage).FirstOrDefault(); // This is the package which will contain the imported models

            if (containerPackage == null)
            {
                // Check whether the parent package(Bibliotheek technisch) bestaat.
                // Find the parent package in EA
                var parentPackage = package;
                if (parentPackageName != null)
                {
                    parentPackage = this.getPackage(parentPackageName, package);
                }
                // Create package under the parent package.
                containerPackage = ((UMLEA.ElementWrapper)parentPackage).addOwnedElement<UMLEA.Package>(packageName);
                containerPackage.save();
            }

            return containerPackage;

        }

        private UML.Classes.Kernel.Package getPackage(string packageName, UML.Classes.Kernel.Package package)
        {
            return this.getPackage(packageName, package, null);
        }
    }
}
