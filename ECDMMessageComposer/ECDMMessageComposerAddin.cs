
using EAAddinFramework.SchemaBuilder;
using EAAddinFramework.Utilities;
using SchemaBuilderFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace ECDMMessageComposer
{
    /// <summary>
    /// Description of MyClass.
    /// </summary>
    public class ECDMMessageComposerAddin : EAAddinFramework.EAAddinBase
    {
        // define menu constants
        const string menuName = "-&EA Message Composer";
        const string menuAbout = "&About";
        const string menuSettings = "&Settings";
        const string targetPackageTagName = "targetPackage";

        private SchemaBuilderFactory schemaFactory;
        private ECDMMessageComposerSettings settings = new ECDMMessageComposerSettings();
        public ECDMMessageComposerAddin() : base()
        {
            this.menuHeader = menuName;
            this.menuOptions = new string[] { menuSettings, menuAbout };
        }
        /// <summary>
        /// Initializes the model and schemaFactory with the new Repository object.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        public override void EA_FileOpen(EA.Repository Repository)
        {
            base.EA_FileOpen(Repository);
            //initialize the model
            this.initialize(Repository);
            this.settings.model = this.model;
        }
        /// <summary>
        /// initialize the add-in class
        /// </summary>
        /// <param name="Repository"></param>
        private void initialize(EA.Repository Repository)
        {
            //initialize factory
            this.schemaFactory = EASchemaBuilderFactory.getInstance(this.model);
        }

        public override object EA_GetMenuItems(EA.Repository Repository, string MenuLocation, string MenuName)
        {
            //only show the menu in the main menu
            if (MenuLocation == "MainMenu")
            {
                return base.EA_GetMenuItems(Repository, MenuLocation, MenuName);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// only needed for the about menu
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="MenuLocation">String representing the part of the user interface that brought up the menu. 
        /// Can be TreeView, MainMenu or Diagram.</param>
        /// <param name="MenuName">The name of the parent menu for which sub-items must be defined. In the case of the top-level menu it is an empty string.</param>
        /// <param name="ItemName">The name of the option actually clicked, for example, Create a New Invoice.</param>
        public override void EA_MenuClick(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                case menuAbout:
                    new AboutWindow().ShowDialog();
                    break;
                case menuSettings:
                    new AddinSettingsForm(new SettingsWindow(this.settings)).ShowDialog(this.model?.mainEAWindow);
                    //in case the tagged values have changed names
                    this.checkTaggedValueTypes();
                    break;
            }
        }

        /// <summary>
        /// Tell EA the name of this Schema composer add-in
        /// </summary>
        /// <param name="Repository">the repository object</param>
        /// <param name="displayName">the name that will be displayed</param>
        /// <returns>true</returns>
        //public override bool EA_IsSchemaExporter(EA.Repository Repository, ref string displayName)
        public bool EA_IsSchemaExporter(EA.Repository Repository, ref string displayName)
        {
            displayName = "ECDM Message Composer";
            return true;
        }

        /// <summary>
        /// The Add-in can optionally implement this function.
        /// Using the SchemaProfile interface an Add-in can adjust the capabilities of the Schema Composer. (See Automation Interface)
        /// </summary>
        /// <param name="Repository">the repository object</param>
        /// <param name="profile">the EA SchemaProfile object</param>
        public override void EA_GetProfileInfo(EA.Repository Repository, EA.SchemaProfile profile)
        {
            //for some reason EA seems to sometimes create a new instance of the add-in.
            //to avoid nullpointer exception we inititialize the model and factory again if needed
            if (this.model == null)
            {
                base.EA_FileOpen(Repository);
            }
            if (this.schemaFactory == null)
            {
                this.initialize(Repository);
            }
            //make sure the tagged value types we need are there
            if (this.schemaFactory != null)
            {
                this.checkTaggedValueTypes();
            }
            //tell EA our export format name
            if (profile != null)
            {
                profile.AddExportFormat("ECDM Message");
            }
        }
        public override void EA_OnOutputItemDoubleClicked(EA.Repository Repository, string TabName, string LineText, long ID)
        {
            var outputElement = this.model.getElementWrapperByID((int)ID);
            if (outputElement != null)
            {
                outputElement.select();
            }
        }

        /// <summary>
        /// in order for the relations to work we need tagged value types "sourceAttribute and "sourceAssociation".
        /// If they don't exists we add them
        /// </summary>
        private void checkTaggedValueTypes()
        {
            if (this.model != null)
            {
                if (!this.model.taggedValueTypeExists(this.settings.sourceAttributeTagName))
                {
                    const string sourceAttributeTagDetail = @"Type=RefGUID;
Values=Attribute;
AppliesTo=Attribute;";
                    this.model.addTaggedValueType(this.settings.sourceAttributeTagName, "is derived from this Attribute", sourceAttributeTagDetail);
                }
                if (!this.model.taggedValueTypeExists(this.settings.sourceAssociationTagName))
                {
                    const string sourceAssociationTagDetail = @"Type=String;
AppliesTo=Association,Aggregation;";
                    this.model.addTaggedValueType(this.settings.sourceAssociationTagName, "is derived from this Association", sourceAssociationTagDetail);
                }
                if (this.settings.tvInsteadOfTrace
                    && this.settings.elementTagName.Length > 0
                    && !this.model.taggedValueTypeExists(this.settings.elementTagName))
                {
                    const string elementTagDetail = @"Type=RefGUID;
Values=Class,DataType,Enumeration,PrimitiveType;
AppliesTo=Class,DataType,Enumeration,PrimitiveType;";
                    this.model.addTaggedValueType(this.settings.elementTagName, "is derived from this Element", elementTagDetail);
                }
            }
        }
        /// <summary>
        /// save the target package in a tagged value on the schema container element
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="package"></param>
        private void saveTargetPackageTag(Schema schema, UML.Classes.Kernel.Package targetPackage)
        {
            if (schema.containerElement != null)
            {
                if (schema.containerElement.isReadOnly)
                {
                    schema.containerElement.makeWritable(false);
                }
                if (!schema.containerElement.isReadOnly)
                {
                    ((TSF_EA.ElementWrapper)schema.containerElement).addTaggedValue(targetPackageTagName, targetPackage.uniqueID);
                }
            }
        }
        /// <summary>
        /// If a user selects any of the outputs listed by the Add-in, this function will be invoked. 
        /// The function receives the Schema Composer automation interface, which can be used to traverse the schema.
        /// </summary>
        /// <param name="Repository"></param>
        /// <param name="composer"></param>
        public override void EA_GenerateFromSchema(EA.Repository Repository, EA.SchemaComposer composer, string exports)
        {
            try
            {
                Schema schema = this.schemaFactory.createSchema(composer, this.settings);
                UML.Classes.Kernel.Package targetPackage = null;
                if (this.settings.generateToArtifactPackage)
                {
                    targetPackage = schema.containerElement?.owningPackage;
                }
                else
                {
                    var targetPackageTag = schema.containerElement?.taggedValues.FirstOrDefault(x => x.name == targetPackageTagName);
                    if (targetPackageTag != null)
                    {
                        targetPackage = targetPackageTag?.tagValue as UML.Classes.Kernel.Package;
                    }
                }
                if (targetPackage == null)
                {
                    targetPackage = this.model.getUserSelectedPackage() as UML.Classes.Kernel.Package;
                }
                if (targetPackage != null)
                {
                    //set the settings based on the target package
                    this.settings.setContextConfig(targetPackage);
                    //save target package as tagged value on schema artifact
                    if (!this.settings.generateToArtifactPackage)
                    {
                        this.saveTargetPackageTag(schema, targetPackage);
                    }
                    Cursor.Current = Cursors.WaitCursor;
                    bool writable = true;
                    //check if package is writable
                    if (this.settings.checkSecurity)
                    {
                        writable = targetPackage.isCompletelyWritable;
                        if (!writable)
                        {
                            var lockPackageResponse = MessageBox.Show("Package is read-only" + Environment.NewLine + "Would you like to lock the package?"
                                            , "Lock target Package?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            //lock the elements immediately
                            if (lockPackageResponse == DialogResult.Yes)
                            {
                                writable = this.makeCompletelyWritable(targetPackage);
                                if (!writable)
                                {
                                    //if not writable then inform user and stop further processing;
                                    MessageBox.Show("Target package could not be locked", "Target Read-Only", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    //only proceed if target package is writable
                    if (writable)
                    {
                        //check if the already contains classes
                        var response = DialogResult.No;
                        response = MessageBox.Show($"Are you sure you want to generate the subset to package '{targetPackage.name}'?"
                                                   , "Generate Subset?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (response == DialogResult.Yes)
                        {
                            if (this.model.EAVersion >= 1308)
                            {
                                //disable ui updates to speed up process. Only for v13 or higher
                                this.model.wrappedModel.EnableUIUpdates = false;
                            }
                            //update the subset
                            this.updateMessageSubset(schema, targetPackage);
                            //refresh package to make changes visible Only for v13 or higher
                            if (this.model.EAVersion >= 1308)
                            {
                                targetPackage.refresh();
                            }
                            //check for any errors or warnings
                            var errorCount = EAOutputLogger.getErrors(this.model, this.settings.outputName).Count();
                            var warningCount = EAOutputLogger.getWarnings(this.model, this.settings.outputName).Count();
                            if (errorCount > 0 || warningCount > 0)
                            {
                                MessageBox.Show(this.model.mainEAWindow, $"Generation resulted in {errorCount} errors and {warningCount} warnings.\nPlease check the output log"
                                    , "Errors or Warnings!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception e)
            {
                processException(e);
            }
            finally
            {
                if (this.model.EAVersion >= 1308)
                {
                    //re-enable gui updates. Only for V13 or higher
                    this.model.wrappedModel.EnableUIUpdates = true;
                }
            }
        }
        /// <summary>
        /// updates an existing message subset for a schema
        /// </summary>
        /// <param name="schema">the schema to use as basis</param>
        /// <param name="messageElement">the root element of the subset</param>
        private void updateMessageSubset(Schema schema, UML.Classes.Kernel.Package targetPackage)
        {
            //log progress
            var startTime = DateTime.Now;
            EAOutputLogger.clearLog(this.model, this.settings.outputName);
            EAOutputLogger.log(this.model, this.settings.outputName
                               , $"{startTime.ToLongTimeString()} Starting update of existing subset for schema '{schema.name}' in package '{targetPackage.name}'"
                               , ((TSF_EA.ElementWrapper)targetPackage).id
                              , LogTypeEnum.log);

            bool copyDataType = this.settings.copyDataTypes;
            List<String> datatypesToCopy = null;
            if (copyDataType && this.settings.limitDataTypes)
            {
                datatypesToCopy = this.settings.dataTypesToCopy;
            }
            bool useMessage = false;
            if (!this.settings.usePackageSchemasOnly)
            {
                //check if we have a message element to folow
                var messageElement = targetPackage.ownedElements.OfType<UML.Classes.Kernel.Classifier>().FirstOrDefault();
                if (messageElement != null)
                {
                    useMessage = true;
                    schema.updateSubsetModel(messageElement);
                }
            }
            if (this.settings.usePackageSchemasOnly || !useMessage)
            {
                schema.updateSubsetModel(targetPackage);
            }
            if (this.settings.generateDiagram)
            {
                var subsetDiagrams = targetPackage.ownedDiagrams;
                if (subsetDiagrams.Count > 0)
                {
                    //if there are existing diagram then we update the existing diagrams
                    this.updateExistingDiagrams(schema, subsetDiagrams);
                }
                else
                {
                    //if not we create a new diagram
                    this.createNewSubsetDiagram(schema, targetPackage);
                }
            }
            //log progress
            var endTime = DateTime.Now;
            var processingTime = (endTime - startTime).TotalSeconds;
            EAOutputLogger.log(this.model, this.settings.outputName
                               , $"{endTime.ToLongTimeString()} Finished update of existing subset for schema '{schema.name}' in package '{targetPackage.name}' in {processingTime.ToString("N0")} seconds"
                               , ((TSF_EA.Package)targetPackage).id
                              , LogTypeEnum.log);
        }
        /// <summary>
        /// Creates a new message subset from the given schema in the given targetPackage
        /// </summary>
        /// <param name="schema">the Schema to generate a message subset from</param>
        /// <param name="targetPackage">the Package to create the new Message subset in</param>
        private void createNewMessageSubset(Schema schema, UML.Classes.Kernel.Package targetPackage)
        {
            //log progress
            EAOutputLogger.clearLog(this.model, this.settings.outputName);
            EAOutputLogger.log(this.model, this.settings.outputName
                               , string.Format("{0} Starting creation of new subset for schema '{1}' in package '{2}'"
                                              , DateTime.Now.ToLongTimeString()
                                              , schema.name
                                              , targetPackage.name)
                               , ((TSF_EA.ElementWrapper)targetPackage).id
                              , LogTypeEnum.log);
            if (targetPackage != null)
            {
                //Logger.log("before ECDMMessageComposerAddin::schema.createSubsetModel");
                //Todo create global setting
                bool copyDataType = this.settings.copyDataTypes;
                List<String> datatypesToCopy = null;
                if (copyDataType && this.settings.limitDataTypes)
                {
                    datatypesToCopy = this.settings.dataTypesToCopy;
                }
                schema.createSubsetModel(targetPackage, schema.elements);
                this.createNewSubsetDiagram(schema, targetPackage);
            }
            //log progress
            EAOutputLogger.log(this.model, this.settings.outputName
                               , string.Format("{0} Finished creation of new subset for schema '{1}' in package '{2}'"
                                              , DateTime.Now.ToLongTimeString()
                                              , schema.name
                                              , targetPackage.name)
                               , ((TSF_EA.ElementWrapper)targetPackage).id
                              , LogTypeEnum.log);
        }
        /// <summary>
        /// update the given diagrams with the schema elements that don't appear ont he diagram yet.
        /// </summary>
        /// <param name="schema">the schema that should be visualised</param>
        /// <param name="subsetDiagrams">list of diagrams to update</param>
        void updateExistingDiagrams(Schema schema, HashSet<UML.Diagrams.Diagram> subsetDiagrams)
        {
            //add all elements to all diagrams in the same package as the messageElement
            foreach (UML.Diagrams.Diagram diagram in subsetDiagrams)
            {
                int xPos = 10;
                int yPos = 10;
                foreach (SchemaElement schemaElement in schema.elements)
                {
                    if (this.shouldElementBeOnDiagram(schemaElement.subsetElement)
                        && !diagram.contains(schemaElement.subsetElement))
                    {
                        UML.Diagrams.DiagramElement diagramElement = diagram.addToDiagram(schemaElement.subsetElement);
                        if (diagramElement != null)
                        {
                            //save before changing the element position
                            diagramElement.save();
                            diagramElement.xPosition = xPos;
                            diagramElement.yPosition = yPos;
                            diagramElement.save();
                            xPos += 50;
                            yPos += 20;
                        }
                    }
                }
                //show the diagram
                diagram.reFresh();
                diagram.open();
            }
        }
        private bool shouldElementBeOnDiagram(UML.Classes.Kernel.Classifier element)
        {
            return element != null
                    && !this.settings.hiddenElementTypes.Any(x => x.Equals(element.GetType().Name, StringComparison.InvariantCulture))
                    && !this.settings.hiddenElementTypes.Intersect(element.stereotypes.Select(x => x.name)).Any();
        }

        /// <summary>
        /// try to make this element is completely writable, including all its owned elements recursively
        /// </summary>
        /// <param name="element">the element to make writable</param>
        /// <returns>true if this element is now completely writable</returns>
        private bool makeCompletelyWritable(UML.Classes.Kernel.Element element)
        {
            if (!element.makeWritable(false))
            {
                return false;
            }

            foreach (var subElement in element.ownedElements)
            {
                if (!this.makeCompletelyWritable(subElement))
                {
                    return false;
                }
            }
            var diagramOwner = element as UML.Classes.Kernel.Namespace;
            if (diagramOwner != null)
            {
                foreach (var diagram in diagramOwner.ownedDiagrams)
                {
                    if (!diagram.makeWritable(false))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// create a new subsetdiagram that will visualize the whole schema
        /// </summary>
        /// <param name="schema">the schema to visualize</param>
        /// <param name="targetPackage">the package where the new diagram should be created.</param>
        void createNewSubsetDiagram(Schema schema, UML.Classes.Kernel.Package targetPackage)
        {
            if (targetPackage != null)
            {
                //Logger.log("after ECDMMessageComposerAddin::schema.createSubsetModel");
                // then make a diagram and put the subset on it
                UML.Diagrams.ClassDiagram subsetDiagram = this.model.factory.createNewDiagram<UML.Diagrams.ClassDiagram>(targetPackage, targetPackage.name);
                subsetDiagram.save();
                //Logger.log("after ECDMMessageComposerAddin::create subsetDiagram");				
                //put the subset elements on the new diagram

                foreach (SchemaElement schemaElement in schema.elements)
                {
                    if (this.shouldElementBeOnDiagram(schemaElement.subsetElement))
                    {
                        subsetDiagram.addToDiagram(schemaElement.subsetElement);
                    }
                    bool addSourceElement = false;
                    //check the settings to see if we need to add the source element
                    if (schemaElement.sourceElement is UML.Classes.Kernel.Class
                        || schemaElement.sourceElement is UML.Classes.Kernel.Enumeration)
                    {
                        addSourceElement = this.settings.addSourceElements;
                    }
                    else
                    {
                        //or the datatype
                        if (schemaElement.subsetElement != null)
                        {
                            addSourceElement = this.settings.addSourceElements;
                        }
                        else
                        {
                            addSourceElement = this.settings.addDataTypes;
                        }
                    }
                    //add source element to the diagram if needed
                    if (addSourceElement
                        && this.shouldElementBeOnDiagram(schemaElement.sourceElement))
                    {
                        //we add the source element if the subset element doesn't exist.
                        subsetDiagram.addToDiagram(schemaElement.sourceElement);
                    }
                }
                //Logger.log("after ECDMMessageComposerAddin::adding elements");	
                //layout the diagram (this will open the diagram as well)
                subsetDiagram.autoLayout();
                //Logger.log("after ECDMMessageComposerAddin::autolayout");
            }
        }
    }
}