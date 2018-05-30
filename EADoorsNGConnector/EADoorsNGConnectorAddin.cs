using EAAddinFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UML = TSF.UmlToolingFramework.UML;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.Diagnostics;
using DoorsNG = EAAddinFramework.Requirements.DoorsNG;

namespace EADoorsNGConnector
{
    public class EADoorsNGConnectorAddin : EAAddinFramework.EAAddinBase
    {
        // define menu constants
        const string menuName = "-&Doors NG Connector";
        const string menuSynchDoorsNGtoEA = "&Doors NG => EA";
        const string menuSynchEAtoDoorsNG = "&EA => Doors NG";
        const string menuSettings = "&Settings";
        const string menuSetProject = "Set &Project";
        const string menuGoToTFSWeb = "&Open in Doors NG";
        const string menuAbout = "&About";



        //private attributes
        private EADoorsNGSettings settings = new EADoorsNGSettings();
        private UML.Extended.UMLModel model;
        private TSF_EA.Model EAModel { get { return this.model as TSF_EA.Model; } }
        private bool fullyLoaded = false;
        /// <summary>
        /// constructor, set menu names
        /// </summary>
        public EADoorsNGConnectorAddin() : base()
        {
            this.menuHeader = menuName;
            this.menuOptions = new string[] { menuSynchDoorsNGtoEA, menuSynchEAtoDoorsNG, menuSetProject, menuGoToTFSWeb, menuSettings, menuAbout };
        }
        public override void EA_FileOpen(EA.Repository Repository)
        {
            // initialize the model
            this.model = new TSF_EA.Model(Repository);
            // indicate that we are now fully loaded
            this.fullyLoaded = true;
        }
        /// <summary>
        /// The EA_GetMenuState event enables the Add-In to set a particular menu option to either enabled or disabled. This is useful when dealing with locked packages and other situations where it is convenient to show a menu option, but not enable it for use.
        /// This event is raised just before Enterprise Architect has to show particular menu options to the user. Its use is described in the Define Menu Items topic.
        /// Also look at EA_GetMenuItems.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="MenuLocation">String representing the part of the user interface that brought up the menu. 
        /// Can be TreeView, MainMenu or Diagram.</param>
        /// <param name="MenuName">The name of the parent menu for which sub-items must be defined. In the case of the top-level menu it is an empty string.</param>
        /// <param name="ItemName">The name of the option actually clicked, for example, Create a New Invoice.</param>
        /// <param name="IsEnabled">Boolean. Set to False to disable this particular menu option.</param>
        /// <param name="IsChecked">Boolean. Set to True to check this particular menu option.</param>
        public override void EA_GetMenuState(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            switch (ItemName)
            {
                case menuSetProject:
                    IsEnabled = (MenuLocation == "TreeView" && this.model.selectedElement is TSF_EA.Package);
                    break;
                case menuSynchDoorsNGtoEA:
                    IsEnabled = (this.model.selectedElement is TSF_EA.Package
                                 && !(this.model.selectedElement is TSF_EA.RootPackage)
                                 || (this.getCurrentRequirement() != null && this.getCurrentRequirement().ID.Length > 0));
                    break;
                case menuSynchEAtoDoorsNG:
                    IsEnabled = (this.model.selectedElement is TSF_EA.Package
                                || this.model.selectedElement is TSF_EA.ElementWrapper);
                    break;
                case menuGoToTFSWeb:
                    IsEnabled = (this.getCurrentProject() != null);
                    break;
                default:
                    base.EA_GetMenuState(Repository, MenuLocation, MenuName, ItemName, ref IsEnabled, ref IsChecked);
                    break;
            }
        }
        /// <summary>
        /// Called when user makes a selection in the menu.
        /// This is your main exit point to the rest of your Add-in
        /// </summary>
        /// <param name="Repository">the repository</param>
        /// <param name="Location">the location of the menu</param>
        /// <param name="MenuName">the name of the menu</param>
        /// <param name="ItemName">the name of the selected menu item</param>
        public override void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                case menuSynchDoorsNGtoEA:
                    synchDoorsNGToEA();
                    break;
                case menuSynchEAtoDoorsNG:
                    synchEAToDoorsNG();
                    break;
                case menuSetProject:
                    setProject();
                    break;
                case menuGoToTFSWeb:
                    goToDoorsNGWeb();
                    break;
                case menuAbout:
                    new AboutWindow().ShowDialog(EAModel.mainEAWindow);
                    break;
                case menuSettings:
                    new EADoorsNGSettingsForm(this.settings, EAModel).ShowDialog(EAModel.mainEAWindow);
                    break;
            }
        }

        void goToDoorsNGWeb()
        {
            try
            {
                string doorsNGUrl;
                var currentProject = this.getCurrentProject();
                if (currentProject != null)
                {
                    //TODO: add folder
                    var requirement = this.getCurrentRequirement();
                    if (requirement != null)
                    {
                        doorsNGUrl = requirement.url;
                    }
                    //TODO: add folder
                    else
                    {
                        doorsNGUrl = currentProject.url;
                    }
                    //open url
                    Process.Start(doorsNGUrl);
                }
                else
                {
                    MessageBox.Show("No Doors NG project found", "No Doors NG Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                Logger.logError("Error Message: " + e.Message + Environment.NewLine + "Stacktrace: " + e.StackTrace);
                MessageBox.Show("An error occured: " + e.Message + Environment.NewLine +
                                "Please check the logfile at: " + Logger.logFileName, "Doors NG Connector Error"
                                , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private DoorsNG.DoorsNGRequirement getCurrentRequirement()
        {
            var currentProject = this.getCurrentProject();
            if (currentProject != null)
            {
                var selectedItem = this.model.selectedElement as TSF_EA.ElementWrapper;
                //check if the type or the stereotype corresponds to one of the mapped elementtypes or stereotypes
                if (selectedItem != null &&
                    (this.settings.mappedElementTypes.Contains(selectedItem.EAElementType)
                     || this.settings.mappedStereotypes.Any(selectedItem.stereotypeNames.Contains)
                    ))
                {
                    return new DoorsNG.DoorsNGRequirement(selectedItem);
                }
            }
            //no project or no appropriate selected element
            return null;
        }

        public DoorsNG.DoorsNGFolder getCurrentFolder()
        {
            var selectedPackage = this.model.selectedItem as TSF_EA.Package;
            return selectedPackage != null
                ? new DoorsNG.DoorsNGFolder(selectedPackage, this.getCurrentProject(), null)
                : null;

        }
        void synchEAToDoorsNG()
        {
            try
            {
                var results = new Dictionary<DoorsNG.DoorsNGRequirement, bool>();
                //if a package is selected then synchronize all owned workitems
                var currentFolder = this.getCurrentFolder();
                if (currentFolder != null)
                {
                    var currentProject = this.getCurrentFolder();
                    if (currentProject != null)
                    {
                        foreach (DoorsNG.DoorsNGRequirement ownedRequirement in currentFolder.getAllOwnedRequirements())
                        {
                            results.Add(ownedRequirement, ownedRequirement.synchronizeToDoorsNG());
                        }
                    }
                }
                //if a requirement was selected then synchronize this single requirement
                var currentWorkitem = getCurrentRequirement();
                if (currentWorkitem != null)
                {
                    results.Add(currentWorkitem, currentWorkitem.synchronizeToDoorsNG());
                }
                //tell the user what happened
                MessageBox.Show(string.Format("{0} workitems were succesfully synchronized\n{1} workitems could not be synchronized"
                                              , results.Count(x => x.Value), results.Count(x => !x.Value)), "Synchronize to EA => TFS result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                if (!(e is OperationCanceledException))
                {
                    Logger.logError("Error Message: " + e.Message + Environment.NewLine + "Stacktrace: " + e.StackTrace);
                    MessageBox.Show("An error occured: " + e.Message + Environment.NewLine +
                                    "Please check the logfile at: " + Logger.logFileName, "TFS Connector Error"
                                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void synchDoorsNGToEA()
        {
            try
            {
                var results = new Dictionary<DoorsNG.DoorsNGRequirement, bool>();
                //get a list of all workitems of a certain type
                var currentFolder = getCurrentFolder();
                //get the workitems if project was found
                if (currentFolder != null)
                {
                    var selectImportTypes = new SelectImportTypesForm(this.settings);
                    if (selectImportTypes.ShowDialog(this.EAModel.mainEAWindow) == DialogResult.OK)
                    {
                        if (selectImportTypes.allTypes)
                        {
                            //import all types
                            foreach (DoorsNG.DoorsNGRequirement requirement in currentFolder.requirements)
                            {
                                if (settings.requirementMappings.Any(x => x.Value.Equals(requirement.type, StringComparison.InvariantCultureIgnoreCase)))
                                {
                                    var mapping = settings.requirementMappings.First(x => x.Value.Equals(requirement.type, StringComparison.InvariantCultureIgnoreCase));
                                    //TODO: determine the type of element to use based on the mapping
                                    results.Add(requirement, requirement.synchronizeToEA());
                                }
                            }
                        }
                        else
                        {
                            //import only the selected type
                            foreach (DoorsNG.DoorsNGRequirement requirement in currentFolder.requirements
                                     .Where(x => x.type.Equals(selectImportTypes.DoorsNGRequirementType, StringComparison.InvariantCultureIgnoreCase)))
                            {
                                results.Add(requirement, requirement.synchronizeToEA());
                            }
                        }
                    }
                }
                else
                {
                    var currentRequirement = this.getCurrentRequirement();
                    if (currentRequirement.ID.Length > 0)
                    {
                        results.Add(currentRequirement, currentRequirement.synchronizeToEA());
                    }
                }

                //tell the user what happened
                MessageBox.Show(string.Format("{0} workitems were succesfully synchronized \n{1} workitems could not be synchronized"
                                          , results.Count(x => x.Value), results.Count(x => !x.Value)), "Synchronize TFS => EA result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                if (!(e is OperationCanceledException))
                {
                    Logger.logError("Error Message: " + e.Message + Environment.NewLine + "Stacktrace: " + e.StackTrace);
                    MessageBox.Show("An error occured: " + e.Message + Environment.NewLine +
                                    "Please check the logfile at: " + Logger.logFileName, "TFS Connector Error"
                                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void setProject()
        {

            var currentProject = getCurrentProject();
            var projectForm = new SetProjectForm();
            if (currentProject != null) projectForm.projectName = currentProject.name;

            //TODO: fix by creating Project class
            //set on root node
            var selectedRoot = this.model.selectedItem as TSF_EA.RootPackage;
            if (selectedRoot != null)
            {
                if (projectForm.ShowDialog(EAModel.mainEAWindow) == DialogResult.OK
                   && projectForm.projectName.Length > 0)
                {
                    selectedRoot.notes = "RQProject=" + projectForm.projectName;
                    selectedRoot.save();
                }
            }
            else
            {
                //set on package
                var selectedPackage = this.model.selectedItem as TSF_EA.Package;
                if (selectedPackage != null)
                {
                    if (projectForm.ShowDialog(EAModel.mainEAWindow) == DialogResult.OK
                       && projectForm.projectName.Length > 0)
                    {
                        selectedPackage.addTaggedValue("RQProject", projectForm.projectName);
                    }
                }
            }
        }
        /// <summary>
        /// returns the project based on the currently selected element.
        /// </summary>
        /// <returns>the TFSProject based on the currently selected item</returns>
        private DoorsNG.DoorsNGProject getCurrentProject()
        {
            var currentProject = DoorsNG.DoorsNGProject.getCurrentProject(this.model.selectedElement as TSF_EA.Element, this.settings.getURL(this.EAModel), this.settings) as DoorsNG.DoorsNGProject;
            if (currentProject == null)
            {
                //if not found return default project
                //TODO:
                //currentProject = new TFS.TFSProject(settings.defaultProject, this.settings.getURL(this.EAModel), this.settings);
            }
            return currentProject;
        }
    }
}
