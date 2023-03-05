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
            var systemPackage = getPackage(systemName, selectedPackage, "Bibliotheek Technisch");

            foreach (var packageNode in this.xDoc.Root.Elements("package")) // packages
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
                              , $" Processing package '{packageName}' in parent package '{package.name}'"
                              , ((UMLEA.ElementWrapper)package).id
                             , LogTypeEnum.log);

            // Find get the package with packageName from EA. If this package does not exist, it will be created.
            var pack = this.getPackageUnderPackage(packageName, package);



            // Process the elements in the package.
            foreach (var elementNode in packageNode.Elements("element"))
            {
                this.processElementNode(elementNode, pack);
            }

            //process the packages in the package
            foreach (var subPackageNode in packageNode.Elements("package")) // packages.
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
                            , $" Processing element '{elementName}' in parent package '{package.name}'"
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
            foreach (var attributeNode in elementNode.Elements("Attribute"))
            {
                attributePos++;
                var attributeName = attributeNode.Attribute("name").Value;
                var key = attributeNode.Attribute("key")?.Value;
                var datatype = attributeNode.Attribute("datatype")?.Value;
                var notes = attributeNode.Elements("notes").FirstOrDefault()?.Value;
                //TODO: ownerScope and changeable, add to profile?
                owner.addOrUpdateAttribute(attributeName,key, string.Empty, notes, datatype, attributePos);
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
            foreach (var nodeNode in elementNode.Elements("node"))
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
                //importDeterminations nodeNode, element, package
                //'import validations
                //importValidations nodeNode, element
                //'import actions
                //importActions nodeNode, element
                //'import queries
                //importQueries nodeNode, element
                //'import altkeys
                //importAltkeys nodeNode, element
                //'import authorisations
                //importAuth nodeNode, element

                
            }
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
            foreach (var associationNode in nodeNode.Element("node_elements").Element("associations").Elements("association"))
            {
                var assocationName = associationNode.Attribute("name").Value;
                var targetBOName = associationNode.Element("assoc_settings")?.Element("target_bo")?.Value;
                var targetNodeName = associationNode.Element("assoc_settings").Element("target_node")?.Value;
                var targetKeyName = associationNode.Element("assoc_settings").Element("target_node")?.Attribute("target_node_key")?.Value;

                //get target BO
                var targetBO = new BOPFBusinessObject(targetBOName, sourceNode.wrappedElement.owningPackage, null);
                var targetNode = new BOPFNode(targetNodeName, targetBO, targetKeyName);
                //get association
                var association = new SAPAssociation(sourceNode, targetNode, assocationName);
                //set notes
                var notesNode = associationNode.Element("notes");
                if (notesNode != null)
                {
                    association.notes = notesNode.Value;
                }
                association.save();
                //TODO set other properties
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
            foreach (var userCategoryNode in elementNode.Element("assignment")?.Elements("orgunit"))
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
            foreach (var parameterNode in elementNode.Element("parameters")?.Elements("parameter"))
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
                foreach (var authorizationNode in authorizationsNode.Elements("authorization"))
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
                    foreach (var authFieldNode in authorizationNode.Elements("auth_field"))
                    {
                        var authFieldName = authFieldNode.Attribute("name").Value;
                        //get all values and concatenate with ","
                        var values = new List<string>();
                        foreach (var valueNode in authFieldNode.Elements("field_value"))
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
            foreach (var singleRoleNode in elementNode.Element("single_roles")?.Elements("single_role"))
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
            foreach (var groupNode in assignmentNode.Elements("group"))
            {
                var groupName = groupNode.Attribute("name").Value;

                foreach (var functionModuleNode in groupNode.Elements("function_module"))
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
            foreach (var rolePackageNode in assignmentNode.Elements("role_package"))
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
