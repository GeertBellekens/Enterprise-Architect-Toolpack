
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using UML=TSF.UmlToolingFramework.UML;
using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using EAAddinFramework;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;
using WT=WorkTrackingFramework;
using TFS=EAAddinFramework.WorkTracking.TFS;
using EAAddinFramework.Utilities;


namespace EATFSConnector
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class EATFSConnectorAddin:EAAddinBase
	{
		// define menu constants
        const string menuName = "-&TFS Connector";
        const string menuSynchTFStoEA = "&TFS => EA";
        const string menuSynchEAtoTFS = "&EA => TFS";
        const string menuSettings = "&Settings";
        const string menuSetProject = "Set &Project";
        const string menuAbout = "&About";
        

        
        //private attributes
        private EATFSConnectorSettings settings = new EATFSConnectorSettings();
        private UML.Extended.UMLModel model;
		private TSF_EA.Model EAModel {get{return this.model as TSF_EA.Model;}}
		private bool fullyLoaded = false;
        /// <summary>
        /// constructor, set menu names
        /// </summary>
        public EATFSConnectorAddin():base()
        {
        	this.menuHeader = menuName;
			this.menuOptions = new string[]{menuSynchTFStoEA,menuSynchEAtoTFS, menuSettings, menuAbout};
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
					IsEnabled = (MenuLocation == "TreeView" && this.model.selectedElement is TSF_EA.RootPackage);
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
            	case menuSynchTFStoEA:
            		synchTFSToEA();
            		break;
                case menuSynchEAtoTFS:
					//TODO
                    break;
		        case menuAbout :
		            new AboutWindow().ShowDialog();
		            break;
	            case menuSettings:
		            new TFSConnectorSettingsForm(this.settings).ShowDialog();
	                break;
            }
        }

        private void synchTFSToEA()
        {
        	string TFSUrl;
        	//get the TFS location
        	if (this.settings.projectConnections.TryGetValue(this.EAModel.projectGUID,out TFSUrl))
        	{
        		//ok we have an URL for the TFS
        		//get a list of all workitems of a certain type
        		string currentProjectName = getCurrentProject();
        		//get the workitems is project was found
        		if (! string.IsNullOrEmpty(currentProjectName)) 
        		{
        			WT.Project project = new TFS.Project(currentProjectName,TFSUrl,this.settings);
        			foreach (var workitem in project.workitems) 
        			{
        				Logger.log ("workitem ID: " + workitem.ID + " Title: " + workitem.title);
        			}
        		}
        	}
        }
        
        private string getCurrentProject()
        {
        	var currentRoot = this.model.getCurrentRootPackage() as TSF_EA.RootPackage;
        	if (currentRoot != null)
        	{
        		//check if the current root contains a project value
        		var keyValues = currentRoot.notes.Split('=');
        		if (keyValues.Count() == 2
        		    && keyValues[0] == "project"
        		    && keyValues[1].Length > 0)
        		{
        			return keyValues[1];
        		}
        	}
        	//if not found return default project
        	return settings.defaultProject;
        }


	}
	
	
	
	
}