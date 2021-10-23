using EAAddinFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UTF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace TSF.UmlToolingFramework.EANavigator
{
    public class EAAddin : EAAddinFramework.EAAddinBase
    {
        internal const string menuName = "-&Navigate";
        internal const string menuOperation = "&Operation";
        internal const string menuDiagrams = "&Diagrams";
        internal const string menuAbout = "&About EA Navigator";
        internal const string menuSettings = "&Settings";
        //internal const string menuClassifier = "&Type";
        internal const string menuParameterTypes = "&Parameter Types";
        internal const string menuAttributes = "&Dependent Attributes";
        internal const string menuParameters = "&Dependent Parameters";
        internal const string menuActions = "&Calling Actions";
        internal const string menuImplementation = "&Implementation";
        internal const string menuFQN = "&To FQN";
        internal const string menuGUID = "&To GUID";
        internal const string menuDiagramOperations = "&Operations";
        internal const string menuImplementedOperations = "&Implemented Operation";
        internal const string menuDependentTaggedValues = "&Referencing Tagged Values";
        internal const string menuOpenInNavigator = "&Open in Navigator";
        internal const string menuLinkedToElementFeature = "&Link to Element Feature";
        internal const string menuCompositeDiagram = "&Composite Diagram";
        internal const string menuCompositeElement = "&Composite Element";
        internal const string menuInDiagrams = "&In Diagrams";
        internal const string menuConveyedElements = "&Conveyed Elements";
        internal const string menuConveyingConnectors = "&Conveying Connectors";
        internal const string menuAssociationClass = "&Association Class";
        internal const string menuAssociation = "&Association";

        internal const string taggedValueMenuSuffix = " Tags";
        internal const string taggedValueMenuPrefix = "&";

        internal const string ownerMenuPrefix = "&Owner: ";
        internal const string typeMenuPrefix = "&Type: ";

        const string eaGUIDTagname = "ea_guid";
        const string eaOperationGUIDTagName = "operation_guid";

        private int maxQuickSearchResults = 10;
        private NavigatorControl _navigatorControl;

        
        internal NavigatorSettings settings { get; set; }

        public EAAddin() : base()
        {
            this.menuHeader = menuName;

            this.menuOptions = new string[] { menuOperation, menuDiagrams };
            this.settings = new NavigatorSettings();

        }
        private NavigatorControl navigatorControl
        {
            get
            {
                //we cannot show windows in the lite edition
                if (this.fullyLoaded && !this.model.isLiteEdition)
                {
                    if (this._navigatorControl == null)
                    {
                        this._navigatorControl = this.model.addWindow("Navigate", "TSF.UmlToolingFramework.EANavigator.NavigatorControl") as NavigatorControl;
                        this._navigatorControl.BeforeExpand += new TreeViewCancelEventHandler(this.NavigatorTreeBeforeExpand);
                        this._navigatorControl.NodeDoubleClick += new TreeNodeMouseClickEventHandler(this.NavigatorTreeNodeDoubleClick);
                        this._navigatorControl.fqnButtonClick += new EventHandler(this.FqnButtonClick);
                        this._navigatorControl.guidButtonClick += new EventHandler(this.GuidButtonClick);
                        this._navigatorControl.quickSearchTextChanged += new EventHandler(this.quickSearchTextChanged);
                        this._navigatorControl.openInNavigatorClick += new EventHandler(this.openInNavigatorClick);
                        this._navigatorControl.guidSearched += this.guidSearched;
                        this._navigatorControl.settings = this.settings;
                    }
                }
                return this._navigatorControl;
            }
        }

        private void guidSearched(object sender, EventArgs e)
        {
            var guidString = sender as string;
            if (!string.IsNullOrEmpty(guidString))
            {
                this.selectGUID(guidString);
            }
        }

        public override void EA_OnPostInitialized(EA.Repository Repository)
        {

            if (this.navigatorControl != null)
            {
                this.navigatorControl.clear();
            }
        }
        public override void EA_FileOpen(EA.Repository Repository)
        {
            base.EA_FileOpen(Repository);
            // clear the control
            if (this.navigatorControl != null)
            {
                this.navigatorControl.clear();
            }
        }
        public override void EA_FileClose(EA.Repository Repository)
        {
            base.EA_FileClose(Repository);
            // clear the control
            if (this.navigatorControl != null)
            {
                this.navigatorControl.clear();
            }
        }
        /// <summary>
        /// The EA_GetMenuItems event enables the Add-In to provide the Enterprise Architect user interface with additional Add-In menu options in various context and main menus. When a user selects an Add-In menu option, an event is raised and passed back to the Add-In that originally defined that menu option.
        /// This event is raised just before Enterprise Architect has to show particular menu options to the user, and its use is described in the Define Menu Items topic.
        /// Also look at:
        /// - EA_MenuClick
        /// - EA_GetMenuState.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="MenuLocation">String representing the part of the user interface that brought up the menu. 
        /// Can be TreeView, MainMenu or Diagram.</param>
        /// <param name="MenuName">The name of the parent menu for which sub-items are to be defined. In the case of the top-level menu it is an empty string.</param>
        /// <returns>One of the following types:
        /// - A string indicating the label for a single menu option.
        /// - An array of strings indicating a multiple menu options.
        /// - Empty (Visual Basic/VB.NET) or null (C#) to indicate that no menu should be displayed.
        /// In the case of the top-level menu it should be a single string or an array containing only one item, or Empty/null.</returns>
        public override object EA_GetMenuItems(EA.Repository Repository, string MenuLocation, string MenuName)
        {

            //check setting for context menus
            if (this.settings.contextmenuVisible || MenuLocation == "MainMenu"
               || !this.settings.trackSelectedElement)
            {
                switch (MenuName)
                {
                    case "":
                        //return top level menu option
                        return this.menuHeader;
                    case menuName:
                        List<string> menuOptionsList = new List<string>();
                        if (!this.settings.trackSelectedElement)
                        {
                            menuOptionsList.Add(menuOpenInNavigator);
                        }
                        //Context menu
                        if (MenuLocation != "MainMenu" && this.settings.contextmenuVisible)
                        {

                            // get the selected element from the model
                            UML.Extended.UMLItem selectedElement = this.model.selectedItem;
                            menuOptionsList.AddRange(getMenuOptions(selectedElement));

                        }
                        // Main menu
                        else if (MenuLocation == "MainMenu")
                        {
                            // To FQN
                            menuOptionsList.Add(menuFQN);
                            // To GUID
                            menuOptionsList.Add(menuGUID);
                            // setting
                            menuOptionsList.Add(menuSettings);
                            //menu about
                            menuOptionsList.Add(menuAbout);
                        }
                        // return submenu options
                        return menuOptionsList.ToArray();
                    default:
                        return string.Empty;
                }
            }
            //don't show menu;
            else
            {
                return null;
            }


        }
        internal static string getOwnerMenuName(UML.Extended.UMLItem element)
        {
            string ownerMenuname = ownerMenuPrefix;
            if (element.owner != null)
            {
                ownerMenuname += element.owner.name;
            }
            return ownerMenuname;
        }
        internal static string getTypeMenuName(UML.Extended.UMLItem element)
        {
            string typeMenuname = typeMenuPrefix;
            UML.Classes.Kernel.Property attribute = element as UML.Classes.Kernel.Property;
            if (attribute != null
               && attribute.type != null)
            {
                typeMenuname += attribute.type.name;
            }
            return typeMenuname;
        }
        /// <summary>
        /// returns the options depending on the type of the element
        /// </summary>
        /// <param name="element">element</param>
        /// <returns>a list of navigator menu options depending on the type of element</returns>
        internal static List<string> getMenuOptions(UML.Extended.UMLItem element)
        {
            List<string> menuOptionsList = new List<string>();
            if (element != null
                && !(element is UML.Classes.Kernel.Relationship)
               && element.owner != null)
            {
                menuOptionsList.Add(getOwnerMenuName(element));
            }
            if (element is UML.Classes.Kernel.Operation)
            {
                menuOptionsList.Add(menuDiagrams);
                menuOptionsList.Add(menuParameterTypes);
                menuOptionsList.Add(menuActions);
                menuOptionsList.Add(menuImplementation);
                //menuOptionsList.Add(menuLinkedToElementFeature);

            }
            else if (element is UML.Interactions.BasicInteractions.Message)
            {
                menuOptionsList.Add(menuOperation);
                menuOptionsList.Add(menuDiagrams);
                menuOptionsList.Add(menuParameterTypes);
                menuOptionsList.Add(menuImplementation);

            }
            else if (element is UML.Actions.BasicActions.CallOperationAction)
            {
                menuOptionsList.Add(menuOperation);
            }
            else if (element is UML.Classes.Kernel.Type
                     && !(element is UML.Classes.Kernel.Relationship)
                    || element is UML.Classes.AssociationClasses.AssociationClass)
            {
                menuOptionsList.Add(menuAttributes);
                menuOptionsList.Add(menuParameters);

            }
            else if (element is UML.Classes.Kernel.Property)
            {
                menuOptionsList.Add(getTypeMenuName(element));
                //menuOptionsList.Add(menuLinkedToElementFeature);
            }
            else if (element is UML.Diagrams.SequenceDiagram
                    || element is UML.Diagrams.CommunicationDiagram)
            {
                menuOptionsList.Add(menuDiagramOperations);
            }
            //now for behavior, could be a type as well
            if (element is UML.CommonBehaviors.BasicBehaviors.Behavior
                || element is UML.Diagrams.Diagram &&
                ((UML.Diagrams.Diagram)element).owner is UML.CommonBehaviors.BasicBehaviors.Behavior)
            {
                menuOptionsList.Add(menuImplementedOperations);
            }
            //conveying connectors
            if (element is UML.Classes.Kernel.Classifier
                && !(element is UML.Classes.Kernel.Relationship)
                || element is UML.Classes.AssociationClasses.AssociationClass)
            {
                menuOptionsList.Add(menuConveyingConnectors);
            }
            //conveyed elements
            if (element is UML.Classes.Kernel.Relationship
                && !(element is UML.Classes.AssociationClasses.AssociationClass))
            {
                menuOptionsList.Add(menuConveyedElements);
            }
            //composite diagram
            if (element is TSF.UmlToolingFramework.Wrappers.EA.ElementWrapper
                && !(element is TSF.UmlToolingFramework.Wrappers.EA.RootPackage))
            {
                menuOptionsList.Add(menuCompositeDiagram);
            }
            //and the element(s) for which this is a composite diagram
            if (element is UML.Diagrams.Diagram)
            {
                menuOptionsList.Add(menuCompositeElement);
            }
            //from associatioinclasses we allow navigation to the related association.
            if (element is UML.Classes.AssociationClasses.AssociationClass)
            {
                menuOptionsList.Add(menuAssociation);
            }
            //because an associationclass is also an association we add an else if
            //navigate to the related associationclass
            else if (element is UML.Classes.Kernel.Association)
            {
                menuOptionsList.Add(menuAssociationClass);
            }
            //tagged values can be added to any UML element
            if (element is UML.Classes.Kernel.Element)
            {
                menuOptionsList.AddRange(getTaggedValueMenuItems(element as UML.Classes.Kernel.Element));
            }
            //relationships
            if (element is UML.Classes.Kernel.Relationship)
            {
                menuOptionsList.Add(menuInDiagrams);
            }
            //tagged values can reference only ElementWrappers, attributes, relationships and operations
            //same for link to element feature links
            if (element is TSF.UmlToolingFramework.Wrappers.EA.ElementWrapper
                || element is UML.Classes.Kernel.Property
                || element is UML.Classes.Kernel.Operation
                || element is UML.Classes.Kernel.Relationship
                || element is UML.Classes.Kernel.EnumerationLiteral)
            {
                //the root package can't have links
                if (element.owner != null
                    && !(element is UML.Classes.Kernel.Relationship))
                {
                    menuOptionsList.Add(menuLinkedToElementFeature);
                }
                //but it could be referenced by a tagged value
                menuOptionsList.Add(menuDependentTaggedValues);
            }

            filterMenuOptions(menuOptionsList);
            return menuOptionsList;
        }
        private static void filterMenuOptions(List<string> menuOptionsList)
        {
            for (int i = menuOptionsList.Count - 1; i >= 0; i--)
            {
                string settingName = menuOptionsList[i] + "Enabled";
                //string configvalue = System.Configuration.ConfigurationSettings.AppSettings[settingName];
                string configvalue2 = System.Configuration.ConfigurationManager.AppSettings[settingName];
            }
        }
        /// <summary>
        /// creates a list of menuoptions based on the names of tagged values that reference another UML item
        /// </summary>
        /// <param name="ownerElement">the owner of the tagged values</param>
        /// <returns>menuoptions for tagged values</returns>
        private static List<string> getTaggedValueMenuItems(UML.Classes.Kernel.Element ownerElement)
        {
            List<string> menuItems = new List<string>();
            foreach (UML.Profiles.TaggedValue taggedValue in ownerElement.taggedValues)
            {
                if (taggedValue.tagValue is UML.Extended.UMLItem)
                {
                    string menuName = taggedValueMenuName(taggedValue.name);
                    //we don't want the "system" tagged values ea_guid and operation_guid to show up in the navigator
                    if (menuName != taggedValueMenuName(eaGUIDTagname)
                       && menuName != taggedValueMenuName(eaOperationGUIDTagName)
                       && !menuItems.Contains(menuName))
                    {
                        menuItems.Add(menuName);
                    }
                }
            }
            return menuItems;
        }
        /// <summary>
        /// adds the menu prefix and menu suffix to the tagged value name
        /// </summary>
        /// <param name="taggedValueName">the tagged value name</param>
        /// <returns>the menu name for the tagged value name</returns>
        private static string taggedValueMenuName(string taggedValueName)
        {
            return (taggedValueMenuPrefix + taggedValueName + taggedValueMenuSuffix);
        }
        /// <summary>
        /// Execute the actual functions
        /// </summary>
        /// <param name="Repository">the repository</param>
        /// <param name="Location">menu location</param>
        /// <param name="MenuName">menu name</param>
        /// <param name="ItemName">option clicked</param>
        public override void EA_MenuClick(global::EA.Repository Repository,
                                  String Location,
                                  String MenuName, String ItemName)
        {

            switch (ItemName)
            {

                case menuFQN:
                    this.selectFQN();
                    break;
                case menuGUID:
                    this.selectGUID();
                    break;
                case menuAbout:
                    new AboutWindow().ShowDialog();
                    break;
                case menuSettings:
                    new NavigatorSettingsForm(this.settings).ShowDialog();
                    //we need to set the toolbar in case that setting was changed
                    if (this.navigatorControl != null)
                    {
                        this.navigatorControl.toolbarVisible = this.settings.toolbarVisible;
                    }
                    break;
                case menuOpenInNavigator:
                    this.navigate();
                    break;
                default:
                    UML.Extended.UMLItem selectedItem = this.model.selectedItem;
                    if (selectedItem != null)
                    {
                        List<UML.Extended.UMLItem> elementsToNavigate = this.getElementsToNavigate(ItemName, selectedItem);
                        if (elementsToNavigate.Count == 1)
                        {
                            elementsToNavigate[0].open();
                        }
                        else if (elementsToNavigate.Count > 1)
                        {
                            NavigatorList dialog = new NavigatorList(elementsToNavigate, selectedItem);
                            dialog.Show();
                        }
                        else
                        {
                            string message = selectedItem.name + " does not have any " + ItemName.Replace("&", string.Empty);
                            MessageBox.Show(message, "Nothing to navigate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    break;

            }
        }
        /// <summary>
        /// returns the items to navigate based on the given element and chosen option.
        /// </summary>
        /// <param name="menuChoice">the chosen option</param>
        /// <param name="parentElement">the elememnt</param>
        /// <returns>list of items to navigate</returns>
        public List<UML.Extended.UMLItem> getElementsToNavigate(string menuChoice, UML.Extended.UMLItem parentElement)
        {

            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            switch (menuChoice)
            {
                case menuOperation:
                    elementsToNavigate = this.getOperation(parentElement);
                    break;
                case menuDiagrams:
                    elementsToNavigate = this.getDiagrams(parentElement);
                    break;
                case menuParameterTypes:
                    elementsToNavigate = this.getParameterTypes(parentElement);
                    break;
                case menuActions:
                    elementsToNavigate = this.getActions(parentElement);
                    break;
                case menuAttributes:
                    elementsToNavigate = this.getAttributes(parentElement);
                    break;
                case menuParameters:
                    elementsToNavigate = this.getParameters(parentElement);
                    break;
                case menuImplementation:
                    elementsToNavigate = this.getImplementation(parentElement);
                    break;
                case menuDiagramOperations:
                    elementsToNavigate = this.getDiagramOperations(parentElement);
                    break;
                case menuImplementedOperations:
                    elementsToNavigate = this.getImplementedOperation(parentElement);
                    break;
                case menuDependentTaggedValues:
                    elementsToNavigate = this.getDependentTaggedValues(parentElement);
                    break;
                case menuLinkedToElementFeature:
                    elementsToNavigate = this.getLinkedToElementFeatures(parentElement);
                    break;
                case menuCompositeDiagram:
                    elementsToNavigate = this.getCompositeDiagram(parentElement);
                    break;
                case menuCompositeElement:
                    elementsToNavigate = this.getCompositeElements(parentElement);
                    break;
                case menuInDiagrams:
                    elementsToNavigate = this.getDepentDiagrams(parentElement);
                    break;
                case menuConveyingConnectors:
                    elementsToNavigate = this.getConveyingConnectors(parentElement);
                    break;
                case menuConveyedElements:
                    elementsToNavigate = this.getConveyedElements(parentElement);
                    break;
                case menuAssociationClass:
                    elementsToNavigate = this.getAssociationClass(parentElement);
                    break;
                case menuAssociation:
                    elementsToNavigate = this.getRelatedAssociation(parentElement);
                    break;
                default:
                    if (menuChoice.StartsWith(taggedValueMenuPrefix)
                       && menuChoice.EndsWith(taggedValueMenuSuffix))
                    {
                        elementsToNavigate = this.getElementsViaTaggedValues(parentElement, menuChoice);
                    }
                    else if (menuChoice == getOwnerMenuName(parentElement))
                    {
                        elementsToNavigate = this.getOwner(parentElement);
                    }
                    else if (menuChoice == getTypeMenuName(parentElement))
                    {
                        elementsToNavigate = this.getClassifier(parentElement);
                    }
                    break;
            }
            return elementsToNavigate;

        }



        /// <summary>
        /// the EA hook we use to show the selected element in the Navigator window
        /// </summary>
        /// <param name="Repository"></param>
        /// <param name="GUID"></param>
        /// <param name="ot"></param>
        public override void EA_OnContextItemChanged(global::EA.Repository Repository, string GUID, global::EA.ObjectType ot)
        {
            try
            {
                if (this.settings.trackSelectedElement)
                {
                    this.navigate();
                }
            }
            catch (Exception e)
            {
                //log debug information
                Logger.logFileName = System.IO.Path.GetTempPath() + "EANavigatorDebug.log";
                Logger.logError(e.Message + " " + e.StackTrace);
                MessageBox.Show("Oops something went wrong! Please send the logfile at location: " + Environment.NewLine + Logger.logFileName
                                + Environment.NewLine + "to geert@bellekens.com for further investigation"
                               , "EA Navigator Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void navigate()
        {
            if (this.fullyLoaded && this.model != null)
            {
                if (this.model.selectedItem != null)
                {
                    this.navigate(this.model.selectedItem);
                }
            }
        }

        private void navigate(UML.Extended.UMLItem item)
        {
            if (this.fullyLoaded)
            {
                if (this.navigatorControl != null && this.model != null)
                {
                    if (item != null)
                    {
                        this.navigatorControl.setElement(item);
                    }
                }
            }
        }
        public override void EA_OnPostOpenDiagram(EA.Repository Repository, int DiagramID)
        {
            if (this.fullyLoaded && this.model != null && this.settings.trackSelectedElement)
            {
                if (this.model.currentDiagram != null)
                {
                    this.navigate(this.model.currentDiagram);
                }
            }
        }

        /// <summary>
        /// selects the element with the GUID in on the clipboard.
        /// If the clipboard doesn't contain a GUID we will ask the user to input one.
        /// </summary>
        private void selectGUID()
        {
            string guidString = string.Empty;
            if (Clipboard.ContainsText())
            {
                string clipboardText = Clipboard.GetText().Trim();
                if (this.isGUIDString(clipboardText))
                {
                    guidString = clipboardText;
                }
            }
            if (guidString == string.Empty)
            {
                //TODO
                FQNInputForm fqnForm = new FQNInputForm("", "Fill in the GUID", "GUID:");
                if (fqnForm.ShowDialog() == DialogResult.OK)
                {
                    guidString = fqnForm.fqn;
                }
            }
            if (guidString != string.Empty)
            {
                this.selectGUID(guidString);
            }
        }
        /// <summary>
        /// selects the element with the given guid
        /// </summary>
        /// <param name="guidString">string containing a guid</param>
        private void selectGUID(string guidString)
        {
            UML.Extended.UMLItem item = null;
            item = this.model.getItemFromGUID(guidString);

            if (item != null)
            {
                //select in EA
                item.select();
                //navigate in EA Navigator (often not needed but do it anyway)
                this.navigate(item);
            }
            else
            {
                //TODO make GUID iso fqn
                FQNInputForm fqnForm = new FQNInputForm("Could not find item with the GUID:"
                                + Environment.NewLine + guidString, "Fill in the GUID", "GUID:");
                if (fqnForm.ShowDialog() == DialogResult.OK
                   && fqnForm.fqn.Length > 0)
                {
                    this.selectGUID(fqnForm.fqn);
                }
            }
        }
        /// <summary>
        /// select the element that matches the fqn in the clipboard
        /// </summary>
        private void selectFQN()
        {
            string fqn = string.Empty;
            if (Clipboard.ContainsText())
            {
                string clipboardText = Clipboard.GetText().Trim();
                if (this.looksLikeFQN(clipboardText))
                {
                    fqn = clipboardText;
                }
            }
            if (fqn == string.Empty)
            {
                FQNInputForm fqnForm = new FQNInputForm();
                if (fqnForm.ShowDialog() == DialogResult.OK)
                {
                    fqn = fqnForm.fqn;
                }
            }
            if (fqn != string.Empty)
            {
                this.selectFQN(fqn);
            }

        }
        private void selectFQN(string fqn)
        {
            UML.Extended.UMLItem item = null;
            item = this.model.getItemFromFQN(fqn);

            if (item != null)
            {
                item.select();
            }
            else
            {
                string messageText;
                if (fqn.Length > 255)
                {
                    messageText = fqn.Substring(0, 255);
                }
                else
                {
                    messageText = fqn;
                }
                FQNInputForm fqnForm = new FQNInputForm("Could not find item with the string:"
                                + Environment.NewLine + messageText, "Fill in the Fully Qualified Name", "FQN:");
                if (fqnForm.ShowDialog() == DialogResult.OK
                   && fqnForm.fqn.Length > 0)
                {
                    this.selectFQN(fqnForm.fqn);
                }
            }
        }
        /// <summary>
        /// returns true if the given string is a valid GUID format
        /// </summary>
        /// <returns>true if the given string is a valid GUID format</returns>
        private bool isGUIDString(string guidString)
        {
            //xmlGUID strings start with EAID_ and cannot be parsed by Guid
            if (!guidString.StartsWith("EAID_", StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    //try to make a new GUID with given string.
                    Guid guid = new Guid(guidString);
                }
                catch (FormatException)
                {
                    //it didn't work, so no valid GUIDString
                    return false;
                }
            }
            //it worked, so the string was a correct guid format
            return true;
        }
        /// <summary>
        /// does a basic check to verify that the given string sort of resembles an FQN sttring
        /// </summary>
        /// <param name="fqnCandidate">the string tot test</param>
        /// <returns>true if the given string looks like an fqn</returns>
        private bool looksLikeFQN(string fqnCandidate)
        {
            bool looksFQN;
            // FQN should contain at least one "."
            looksFQN = fqnCandidate.Contains(".");
            // FQN should not contain any newlines
            if (looksFQN)
            {
                looksFQN = !fqnCandidate.Contains(Environment.NewLine);
            }
            return looksFQN;
        }
        /// <summary>
        /// returns the owner of the parentElement
        /// </summary>
        /// <param name="parentElement">the selected element</param>
        /// <returns>the owner of the element</returns>
        private List<UML.Extended.UMLItem> getOwner(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Extended.UMLItem owner = parentElement.owner;
            if (owner != null)
            {
                elementsToNavigate.Add(owner);
            }
            return elementsToNavigate;
        }

        /// <summary>
        /// returns all tagged values that reference the given item
        /// </summary>
        /// <param name="parentItem"></param>
        /// <returns></returns>
        private List<UML.Extended.UMLItem> getDependentTaggedValues(UML.Extended.UMLItem parentItem)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Classes.Kernel.Element parentElement = parentItem as UML.Classes.Kernel.Element;
            if (parentElement != null)
            {
                foreach (UML.Profiles.TaggedValue taggedValue in parentElement.getReferencingTaggedValues())
                {
                    //not for the "system" tagged values
                    if (taggedValue.name != eaGUIDTagname
                       && taggedValue.name != eaOperationGUIDTagName)
                    {
                        elementsToNavigate.Add(taggedValue);
                    }
                }
            }
            return elementsToNavigate;
        }

        /// <summary>
        /// returns all diagrams that show this item
        /// </summary>
        /// <param name="parentElement"></param>
        /// <returns></returns>
        private List<UML.Extended.UMLItem> getDepentDiagrams(UML.Extended.UMLItem parentElement)
        {
            var elementsToNavigate = new List<UML.Extended.UMLItem>();
            elementsToNavigate.AddRange(parentElement.getDependentDiagrams());
            return elementsToNavigate;
        }
        /// <summary>
        /// returns the conveying connectors for the given classifier
        /// </summary>
        /// <param name="parentElement">the selected classifier</param>
        /// <returns>the conveying connectors</returns>
        List<UML.Extended.UMLItem> getConveyingConnectors(UML.Extended.UMLItem parentElement)
        {
            var elementsToNavigate = new List<UML.Extended.UMLItem>();
            var classifier = parentElement as UML.Classes.Kernel.Classifier;
            if (classifier != null)
            {
                foreach (var informationFlow in classifier.getConveyingFlows())
                {
                    //if the informationFlow has no realizations then we add the informationflow itself.
                    if (informationFlow.realizations.Count == 0)
                    {
                        elementsToNavigate.Add(informationFlow);
                    }
                    else
                    {
                        //otherwise we add the realizations
                        elementsToNavigate.AddRange(informationFlow.realizations);
                    }
                }
            }
            return elementsToNavigate;
        }
        /// <summary>
        /// returnst he elements conveyed by the given connector
        /// </summary>
        /// <param name="parentElement">the selected connector</param>
        /// <returns>the elements conveyed by the given connector</returns>
        List<UML.Extended.UMLItem> getConveyedElements(UML.Extended.UMLItem parentElement)
        {
            var elementsToNavigate = new List<UML.Extended.UMLItem>();
            var connector = parentElement as UML.Classes.Kernel.Relationship;
            if (connector != null)
            {
                foreach (var informationFlow in connector.getInformationFlows())
                {
                    elementsToNavigate.AddRange(informationFlow.conveyed);
                }
            }
            return elementsToNavigate;
        }
        /// <summary>
        /// gets the AssociationClass for the given association
        /// </summary>
        /// <param name="parentElement">the selected association</param>
        /// <returns>the AssociationClass for the given association.</returns>
        List<UML.Extended.UMLItem> getAssociationClass(UML.Extended.UMLItem parentElement)
        {
            var elementsToNavigate = new List<UML.Extended.UMLItem>();
            var association = parentElement as UTF_EA.Association;
            if (association != null
                && association.associationClass != null)
            {
                elementsToNavigate.Add(association.associationClass);
            }
            return elementsToNavigate;
        }
        /// <summary>
        /// gets the related Association for an AssociationClass
        /// </summary>
        /// <param name="parentElement">the selected AssociationClass</param>
        /// <returns>the related association</returns>
        List<UML.Extended.UMLItem> getRelatedAssociation(UML.Extended.UMLItem parentElement)
        {
            var elementsToNavigate = new List<UML.Extended.UMLItem>();
            var associationClass = parentElement as UTF_EA.AssociationClass;
            if (associationClass != null
                && associationClass.relatedAssociation != null)
            {
                elementsToNavigate.Add(associationClass.relatedAssociation);
            }
            return elementsToNavigate;
        }

        /// <summary>
        /// returns all elements linked via the "link to element feature" 
        /// </summary>
        /// <param name="parentItem">the connected element</param>
        /// <returns>attribute, operations and classes that are linked usign the "link to element feature" functionality</returns>
        private List<UML.Extended.UMLItem> getLinkedToElementFeatures(UML.Extended.UMLItem parentItem)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            //either the parent is a property, or it is a element
            if (parentItem is UML.Classes.Kernel.Feature)
            {
                UML.Classes.Kernel.Feature parentFeature = (UML.Classes.Kernel.Feature)parentItem;

                foreach (UML.Classes.Kernel.Relationship relation in parentFeature.relationships)
                {
                    foreach (UML.Extended.UMLItem item in relation.relatedElements)
                    {
                        if (!item.Equals(parentItem))
                        {
                            elementsToNavigate.Add(item);
                        }
                    }
                }
            }
            else if (parentItem is UML.Classes.Kernel.Element)
            {
                UML.Classes.Kernel.Element parentElement = (UML.Classes.Kernel.Element)parentItem;
                foreach (UML.Classes.Kernel.Relationship relation in parentElement.relationships)
                {
                    foreach (UML.Extended.UMLItem item in relation.relatedElements)
                    {
                        if (item is UML.Classes.Kernel.Feature)
                        {
                            elementsToNavigate.Add(item);
                        }
                    }
                }
            }
            return elementsToNavigate;
        }

        /// <summary>
        /// returns the operation implemented by the given parentElement, 
        /// or in case the parentElement is a diagram, by the owner of the parentElement
        /// </summary>
        /// <param name="parentElement">either a behavior, or a diagram owned by a behavior</param>
        /// <returns>the implmented operation (specification)</returns>
        private List<UML.Extended.UMLItem> getImplementedOperation(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.CommonBehaviors.BasicBehaviors.Behavior behavior = parentElement as UML.CommonBehaviors.BasicBehaviors.Behavior;
            //if the parent element is not a behavior it might be a diagram owned by a behavior
            if (behavior == null
               && parentElement is UML.Diagrams.Diagram)
            {
                behavior = ((UML.Diagrams.Diagram)parentElement).owner as UML.CommonBehaviors.BasicBehaviors.Behavior;
            }
            if (behavior != null)
            {
                var specification = behavior.specification as UML.Classes.Kernel.Operation;
                if (specification != null)
                {
                    elementsToNavigate.Add(specification);
                }
            }
            return elementsToNavigate;
        }
        /// <summary>
        /// returns all elements referenced by the tagged values of the parent element
        /// </summary>
        /// <param name="parentElement">any UML.Classes.Kernel.Element</param>
        /// <returns>all elements referenced by the tagged values of the parent element</returns>
        private List<UML.Extended.UMLItem> getElementsViaTaggedValues(UML.Extended.UMLItem parentElement, string menuOption)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Classes.Kernel.Element taggedValueOwner = parentElement as UML.Classes.Kernel.Element;
            if (taggedValueOwner != null)
            {

                foreach (UML.Profiles.TaggedValue taggedValue in taggedValueOwner.taggedValues)
                {
                    if (menuOption.Equals(taggedValueMenuName(taggedValue.name)))
                    {
                        UML.Extended.UMLItem elementToNavigate = taggedValue.tagValue as UML.Extended.UMLItem;
                        if (elementToNavigate != null)
                        {
                            elementsToNavigate.Add(elementToNavigate);
                        }
                    }
                }
            }
            return elementsToNavigate;
        }

        /// <summary>
        /// returns all operations called on the given diagram
        /// </summary>
        /// <param name="parentElement">the sequence diagram</param>
        /// <returns>all operations called on the given diagram</returns>
        private List<UML.Extended.UMLItem> getDiagramOperations(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();

            if (parentElement is UML.Diagrams.SequenceDiagram)
            {
                UML.Diagrams.SequenceDiagram diagram = (UML.Diagrams.SequenceDiagram)parentElement;
                elementsToNavigate.AddRange(diagram.getCalledOperations());
            }
            else if (parentElement is UML.Diagrams.CommunicationDiagram)
            {
                UML.Diagrams.CommunicationDiagram diagram = (UML.Diagrams.CommunicationDiagram)parentElement;
                elementsToNavigate.AddRange(diagram.getCalledOperations());
            }
            return elementsToNavigate;
        }
        /// <summary>
        /// get all using diagrams for the given element 
        /// currently only used for operations
        /// </summary>
        /// <param name="parentElement">the element</param>
        /// <returns>the diagrams using the given element</returns>
        private List<UML.Extended.UMLItem> getDiagrams(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Classes.Kernel.Operation selectedOperation = this.getSelectedOperation(parentElement);

            if (selectedOperation != null)
            {
                elementsToNavigate.AddRange(selectedOperation.getUsingDiagrams<UML.Diagrams.Diagram>());
            }
            return elementsToNavigate;
        }

        /// <summary>
        /// gets the operation for the parent element which can be
        /// - an operation itself
        /// - a message calling the operation
        /// - a parameter for an operation
        /// - a CallOperationAction
        /// </summary>
        /// <param name="parentElement">the element to get the operation from</param>
        /// <returns>the operation for this parent element</returns>
        private List<UML.Extended.UMLItem> getOperation(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Classes.Kernel.Operation calledOperation = null;
            if (parentElement is UML.Classes.Kernel.Parameter)
            {
                calledOperation = ((UML.Classes.Kernel.Parameter)parentElement).operation;
            }
            else if (parentElement is UML.Actions.BasicActions.CallOperationAction)
            {
                calledOperation = ((UML.Actions.BasicActions.CallOperationAction)parentElement).operation;
            }
            else
            {
                calledOperation = this.getSelectedOperation(parentElement);
            }
            if (null != calledOperation)
            {
                elementsToNavigate.Add(calledOperation);
            }
            return elementsToNavigate;
        }
        /// <summary>
        /// Opens the types of the parameters of the selected operation
        /// </summary>
        private List<UML.Extended.UMLItem> getParameterTypes(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Classes.Kernel.Operation selectedOperation = this.getSelectedOperation(parentElement);
            if (selectedOperation != null)
            {
                HashSet<UML.Classes.Kernel.Parameter> parameters = selectedOperation.ownedParameters;
                foreach (UML.Classes.Kernel.Parameter parameter in parameters)
                {
                    elementsToNavigate.Add(parameter.type);
                }
            }
            return elementsToNavigate;
        }
        /// <summary>
        /// Opens the attributes of that use the selected element as type
        /// </summary>
        private List<UML.Extended.UMLItem> getAttributes(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Classes.Kernel.Type selectedType = parentElement as UML.Classes.Kernel.Type;
            // get the attributes that use the selected Type as type
            elementsToNavigate.AddRange(selectedType.getDependentTypedElements<UML.Classes.Kernel.Property>());
            return elementsToNavigate;
        }
        /// <summary>
        /// Opens the parameters that use the selected element as type
        /// </summary>
        private List<UML.Extended.UMLItem> getParameters(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Classes.Kernel.Type selectedType = parentElement as UML.Classes.Kernel.Type;
            // get the parameters that use the selected classifier as type
            elementsToNavigate.AddRange(selectedType.getDependentTypedElements<UML.Classes.Kernel.Parameter>());
            return elementsToNavigate;
        }
        /// <summary>
        /// opens the CallOperationActions that call te selected operation
        /// </summary>
        private List<UML.Extended.UMLItem> getActions(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Classes.Kernel.Operation selectedOperation = this.getSelectedOperation(parentElement);
            if (selectedOperation != null)
            {
                elementsToNavigate.AddRange(selectedOperation.getDependentCallOperationActions());
            }
            return elementsToNavigate;
        }
        /// <summary>
        /// selects the implementation of the operation in the project browser, and opens all owned diagrams of the implementation.
        /// </summary>
        private List<UML.Extended.UMLItem> getImplementation(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Classes.Kernel.Operation selectedOperation = this.getSelectedOperation(parentElement);
            if (selectedOperation != null)
            {
                foreach (UML.CommonBehaviors.BasicBehaviors.Behavior implementation in selectedOperation.methods)
                {
                    //select the behavior in the project browser
                    elementsToNavigate.AddRange(implementation.ownedDiagrams);
                }
            }
            return elementsToNavigate;
        }

        /// <summary>
        /// Opens the type of the attribute
        /// </summary>
        private List<UML.Extended.UMLItem> getClassifier(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Classes.Kernel.Property selectedAttribute = parentElement as UML.Classes.Kernel.Property;
            if (null != selectedAttribute)
            {
                elementsToNavigate.Add(selectedAttribute.type);
            }
            return elementsToNavigate;
        }
        /// <summary>
        /// Gets the selected operation from the model, either directly or through the selected message
        /// </summary>
        /// <returns>the selected operation, or the operation called by the selected message</returns>
        private UML.Classes.Kernel.Operation getSelectedOperation(UML.Extended.UMLItem parentElement)
        {
            // try if the the users selected an operation
            UML.Classes.Kernel.Operation selectedOperation = parentElement as UML.Classes.Kernel.Operation;
            //selected element is not an operation, try to get he operation from the selected message
            if (null == selectedOperation)
            {
                UML.Interactions.BasicInteractions.Message selectedMessage = parentElement as UML.Interactions.BasicInteractions.Message;

                if (null != selectedMessage)
                {
                    selectedOperation = selectedMessage.calledOperation;

                    if (selectedOperation == null)
                    {
                        System.Windows.Forms.MessageBox.Show("Could not find operation!\nMake sure you either select:\n-An Operation in the project browser \n-A message in a sequence diagram that calls an existing Operation"
                                                              , "Missing Operation!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }
            return selectedOperation;
        }
        private List<UML.Extended.UMLItem> getCompositeElements(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Diagrams.Diagram compositeDiagram = parentElement as UML.Diagrams.Diagram;
            if (compositeDiagram != null)
            {
                elementsToNavigate = compositeDiagram.compositeElements.Cast<UML.Extended.UMLItem>().ToList();
            }
            return elementsToNavigate;
        }

        private List<UML.Extended.UMLItem> getCompositeDiagram(UML.Extended.UMLItem parentElement)
        {
            List<UML.Extended.UMLItem> elementsToNavigate = new List<UML.Extended.UMLItem>();
            UML.Classes.Kernel.Element selectedElement = parentElement as UML.Classes.Kernel.Element;
            if (selectedElement != null)
            {
                UML.Diagrams.Diagram compositediagram = selectedElement.compositeDiagram;
                if (compositediagram != null)
                {
                    elementsToNavigate.Add(compositediagram);
                }
            }
            return elementsToNavigate;
        }

        /// <summary>
        /// When a diagram is doubleclicked we need to set it a the main element
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        void NavigatorTreeNodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            UML.Extended.UMLItem selectedElement = e.Node.Tag as UML.Extended.UMLItem;
            if (selectedElement is UML.Diagrams.Diagram)
            {
                this.navigatorControl.setElement(selectedElement);
            }
        }
        /// <summary>
        /// reacts to the event that the FQN button is clicked in the EA NavigatorControl
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments</param>
        void FqnButtonClick(object sender, EventArgs e)
        {
            this.selectFQN();
        }
        /// <summary>
        /// reacts to the event that the GUID button is clicked in the EA NavigatorControl
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments</param>
        void GuidButtonClick(object sender, EventArgs e)
        {
            this.selectGUID();
        }
        void openInNavigatorClick(object sender, EventArgs e)
        {
            this.navigate();
        }
        void quickSearchTextChanged(object sender, EventArgs e)
        {
            this.quickSearch(this.navigatorControl.quickSearchText);
        }
        public void quickSearch(string searchText)
        {
            List<UML.Extended.UMLItem> matchingElements = this.model.getQuickSearchResults(searchText, this.maxQuickSearchResults,
                                                                                  this.settings.quickSearchElements, this.settings.quickSearchOperations,
                                                                                  this.settings.quickSearchAttributes, this.settings.quickSearchDiagrams);
            this.navigatorControl.setQuickSearchResults(matchingElements, searchText);
        }
        /// <summary>
        /// gets the elements of a package right before expanding
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        void NavigatorTreeBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag == null)
            {
                UML.Extended.UMLItem parentElement = e.Node.Parent.Tag as UML.Extended.UMLItem;
                if (parentElement != null)
                {
                    string option = "&" + e.Node.Text;
                    List<UML.Extended.UMLItem> subElements = this.getElementsToNavigate(option, parentElement);
                    this.navigatorControl.setSubNodes(e.Node, subElements);
                }
            }
        }

    }
}
