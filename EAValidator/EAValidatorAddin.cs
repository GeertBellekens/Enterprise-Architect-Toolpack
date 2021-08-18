using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EA;
using EAAddinFramework;
using EAAddinFramework.Utilities;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAValidator
{
    public class EAValidatorAddin : EAAddinBase
    {
        // menu constants
        const string menuName = "-&EA Validator";
        const string menuOpen = "&Open EA Validator...";
        const string menuSettings = "&Settings";
        const string menuAbout = "&About EA Validator";
        const string menuValidate = "&Validate";

        const string appTitle = "EA Validator";
        const string guiFQN = "EAValidator.ucEAValidator";
        

        private ucEAValidator _ucEAValidator;
        private EAValidatorSettings settings { get; set; } = new EAValidatorSettings();

        public EAValidatorAddin() : base()
        {
            // Add menu's to the Add-in in EA
            this.menuHeader = menuName;
        }
        public override void EA_FileOpen(EA.Repository Repository)
        {
            base.EA_FileOpen(Repository);
            //initialize the model
            this.settings.model = this.model;
        }
        private ucEAValidator ucEAValidator
        {
            get
            {
                if (this._ucEAValidator == null && this.model != null)
                {
                    this._ucEAValidator = this.model.addTab(appTitle, guiFQN) as ucEAValidator;
                    this._ucEAValidator.HandleDestroyed += this.handleHandleDestroyed;
                }
                return this._ucEAValidator;
            }
        }
        private void showValidatorTab()
        {
            if (this.ucEAValidator != null)
            {
                this.model.showTab(appTitle);
            }
        }

        private void handleHandleDestroyed(object sender, EventArgs e)
        {
            // Handle must be present for clean destroy
            this._ucEAValidator = null;
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
        public override object EA_GetMenuItems(Repository Repository, string MenuLocation, string MenuName)
        {
            switch (MenuLocation)
            {
                case locationMainMenu:
                    this.menuOptions = new string[] {
                                menuOpen,
                                menuSettings,
                                menuAbout
                                };
                    break;
                case locationTreeview:
                    this.menuOptions = new string[] {
                                menuValidate
                                };
                    break;
                case locationDiagram:
                    this.menuOptions = new string[] {
                                menuValidate
                                };
                    break;
            }
            return base.EA_GetMenuItems(Repository, MenuLocation, MenuName);
        }
        public override void EA_GetMenuState(Repository Repository, string MenuLocation, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            //disable Validate option if the selected diagram type is not in the list of scope diagramss
            if (ItemName == menuValidate 
                && MenuLocation != locationMainMenu
                && IsProjectOpen(Repository)
                )
            {
                TSF_EA.Diagram scopeDiagram;
                //determine scope diagram
                if (MenuLocation == locationDiagram)
                {
                    scopeDiagram = this.model.currentDiagram as TSF_EA.Diagram;
                }
                else
                {
                    scopeDiagram = this.model.selectedDiagram as TSF_EA.Diagram;
                }
                if (scopeDiagram != null)
                {
                    //set context settings
                    this.settings.setContextConfig(scopeDiagram.owner);
                    //check if scope diagram is of the correct type
                    IsEnabled = this.settings.scopeDiagramTypes.Contains(scopeDiagram.diagramType);
                }
                else
                {
                    var selectedElement = this.model.selectedElement as TSF_EA.ElementWrapper ;
                    if (selectedElement != null)
                    {
                        this.settings.setContextConfig(selectedElement);
                    }
                    IsEnabled = this.settings.scopeElementTypes.Contains(selectedElement?.EAElementType);
                }   
            }            
            else
            {
                base.EA_GetMenuState(Repository, MenuLocation, MenuName, ItemName, ref IsEnabled, ref IsChecked);
            }
            
        }
        public override void EA_MenuClick(EA.Repository repository, string location,
                                          string menuName, string itemName)
        {
            // Selection of a menu-item of the Add-in
            switch (itemName)
            {
                case menuOpen:
                    this.openEAValidator();
                    break;
                case menuSettings:
                    new AddinSettingsForm(new SettingsForm(this.settings)).ShowDialog(this.model?.mainEAWindow);
                    break;
                case menuAbout:
                    new AboutWindow().ShowDialog(this.model.mainEAWindow);
                    break;
                case menuValidate:
                    TSF_EA.Diagram scopeDiagram;
                    //determine scope diagram
                    if (location == locationDiagram)
                    {
                        scopeDiagram = this.model.currentDiagram as TSF_EA.Diagram;
                    }
                    else
                    {
                        scopeDiagram = this.model.selectedDiagram as TSF_EA.Diagram;
                    }
                    this.openEAValidator();
                    if (scopeDiagram != null)
                    {
                        this.ucEAValidator.setScopeToDiagram(scopeDiagram);
                    }
                    else
                    {
                        this.ucEAValidator.setScopeToElement(this.model.selectedElement as TSF_EA.Element);
                    }               
                    break;
            }
        }
        

        private void openEAValidator()
        {
            // Open the EA Validator (user control)
            if (this.model == null) { return; }
            var controller = new EAValidatorController(this.model, this.settings);
            this.ucEAValidator.setController(controller);
            //make sure to show the validator GUI
            showValidatorTab();

        }
    }
}
