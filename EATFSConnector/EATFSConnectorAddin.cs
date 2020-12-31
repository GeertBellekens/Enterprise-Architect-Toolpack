
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Windows.Forms;
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
using EA_WT=EAAddinFramework.WorkTracking;
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
        const string menuGoToTFSWeb = "&Open TFS website";
        const string menuAbout = "&About";
        

        
        //private attributes
        private EATFSConnectorSettings settings = new EATFSConnectorSettings();
        
        /// <summary>
        /// constructor, set menu names
        /// </summary>
        public EATFSConnectorAddin():base()
        {
        	this.menuHeader = menuName;
			this.menuOptions = new string[]{menuSynchTFStoEA,menuSynchEAtoTFS,menuSetProject,menuGoToTFSWeb, menuSettings, menuAbout};
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
				case menuSynchTFStoEA:
					IsEnabled = (this.model.selectedElement is TSF_EA.Package 
					             && ! (this.model.selectedElement is TSF_EA.RootPackage)
					             || (this.getCurrentWorkitem() != null && this.getCurrentWorkitem().ID.Length > 0));
					break;
				case menuSynchEAtoTFS:
					IsEnabled = (this.model.selectedElement is TSF_EA.Package
					            || this.model.selectedElement is TSF_EA.ElementWrapper);
					break;
				case menuGoToTFSWeb:
					IsEnabled = ( this.getCurrentProject() != null);
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
            		sychEAToTFS();
                    break;
                case menuSetProject:
            		setProject();
            		break;
            	case menuGoToTFSWeb:
            		goToTFSWeb();
            		break;
		        case menuAbout :
            		new AboutWindow().ShowDialog(model.mainEAWindow);
		            break;
	            case menuSettings:
		            new TFSConnectorSettingsForm(this.settings,model).ShowDialog(model.mainEAWindow);
	                break;
            }
        }

		void goToTFSWeb()
		{
			try
			{
				string TFSUrl;
				var currentProject = this.getCurrentProject();
				if (currentProject != null)
				{
					var selectedWorkitem = this.getCurrentWorkitem();
					if (selectedWorkitem != null)
					{
						TFSUrl = selectedWorkitem.url;
					}
					else
					{
						TFSUrl = currentProject.url;
					}
					//open url
					Process.Start(TFSUrl);
				}
				else
				{
					MessageBox.Show("No TFS project found","No TFS Project",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
			catch(Exception e)
        	{
        		Logger.logError("Error Message: " + e.Message + Environment.NewLine + "Stacktrace: " + e.StackTrace);
        		MessageBox.Show("An error occured: " + e.Message + Environment.NewLine + 
        		                "Please check the logfile at: " + Logger.logFileName,"TFS Connector Error"
        		                ,MessageBoxButtons.OK,MessageBoxIcon.Error);
        	}
				
		}
		private TFS.TFSWorkItem getCurrentWorkitem()
		{
			var currentProject = this.getCurrentProject();
			if (currentProject != null)
			{
				 var selectedItem = this.model.selectedElement as TSF_EA.ElementWrapper;
				//check if the type or the stereotype corresponds to one of the mapped elementtypes or stereotypes
				if (selectedItem != null && 
				    ( this.settings.mappedElementTypes.Contains(selectedItem.EAElementType)
				     || this.settings.mappedStereotypes.Any(selectedItem.stereotypeNames.Contains)
				    ))
				{
					return new TFS.TFSWorkItem(currentProject,selectedItem);
				}
			}
			//no project or no appropriate selected element
			return null;
		}
		
		void sychEAToTFS()
		{
			try
			{
				var results = new Dictionary<WT.Workitem, bool>();
				//if a package is selected then synchronize all owned workitems
				var selectedPackage = this.model.selectedItem as UML.Classes.Kernel.Package;
				if (selectedPackage != null)
				{
					var currentProject = this.getCurrentProject();
					if (currentProject != null)
					{
						foreach (TFS.TFSWorkItem ownedWorkItem in currentProject.getOwnedWorkitems(selectedPackage, true)) 
						{
							results.Add(ownedWorkItem, ownedWorkItem.synchronizeToTFS());
						} 
					}
				}
				//if a workitem was selected then synchronize this single workitem
				var currentWorkitem = getCurrentWorkitem();
				if (currentWorkitem != null)
				{
					results.Add(currentWorkitem,currentWorkitem.synchronizeToTFS());
				}
				//tell the user what happened
				MessageBox.Show(string.Format("{0} workitems were succesfully synchronized\n{1} workitems could not be synchronized"
				                              , results.Count(x => x.Value), results.Count(x => !x.Value)),"Synchronize to EA => TFS result",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			catch(Exception e)
        	{
				if (!(e is OperationCanceledException))
				{
	        		Logger.logError("Error Message: " + e.Message + Environment.NewLine + "Stacktrace: " + e.StackTrace);
	        		MessageBox.Show("An error occured: " + e.Message + Environment.NewLine + 
	        		                "Please check the logfile at: " + Logger.logFileName,"TFS Connector Error"
	        		                ,MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
        	}
		}

        private void synchTFSToEA()
        {
        	try
	        {
	        	var results = new Dictionary<WT.Workitem, bool>();
	    		//get a list of all workitems of a certain type
	    		var currentProject = getCurrentProject();
	    		//get the workitems is project was found
	    		if (currentProject != null) 
	    		{
	    			//get the selected package
	    			var selectedPackage = this.model.selectedItem as TSF_EA.Package;
	    			if (selectedPackage != null)
	    			{
	        			var selectImportTypes = new SelectImportTypesForm(this.settings);
	        			if (selectImportTypes.ShowDialog(this.model.mainEAWindow) == DialogResult.OK)
	        			{
	        				if (selectImportTypes.allTypes)
	        				{
	        					//import all types
	        					foreach (EA_WT.WorkItem workitem in currentProject.workitems)
	        					{
	        						if (settings.workitemMappings.Any(x => x.Value.Equals(workitem.type, StringComparison.InvariantCultureIgnoreCase)))
	        						{
		        						var mapping = settings.workitemMappings.First(x => x.Value.Equals(workitem.type, StringComparison.InvariantCultureIgnoreCase));
		        						results.Add(workitem, workitem.synchronizeToEA(selectedPackage,mapping.Key));
	        						}
	        					}
	        				}
	        				else
	        				{
	        					//import only the selected type
			        			foreach (EA_WT.WorkItem workitem in currentProject.workitems
			        			         .Where(x => x.type.Equals(selectImportTypes.TFSWorkitemType,StringComparison.InvariantCultureIgnoreCase)))
			        			{
		        					results.Add(workitem, workitem.synchronizeToEA(selectedPackage,selectImportTypes.SparxType));
			        			}
	        				}
	        			}
	    			}
	    			else
	    			{
	    				var currentWorkitem = this.getCurrentWorkitem();
	    				if (currentWorkitem.ID.Length > 0)
	    				{
	    					results.Add(currentWorkitem,currentWorkitem.synchronizeToEA());
	    				}
	    			}
	    		}
	    		//tell the user what happened
				MessageBox.Show(string.Format("{0} workitems were succesfully synchronized \n{1} workitems could not be synchronized"
			                              , results.Count(x => x.Value), results.Count(x => !x.Value)),"Synchronize TFS => EA result",MessageBoxButtons.OK,MessageBoxIcon.Information);
        	}
        	catch(Exception e)
        	{
        		if (!(e is OperationCanceledException))
				{
	        		Logger.logError("Error Message: " + e.Message + Environment.NewLine + "Stacktrace: " + e.StackTrace);
	        		MessageBox.Show("An error occured: " + e.Message + Environment.NewLine + 
	        		                "Please check the logfile at: " + Logger.logFileName,"TFS Connector Error"
	        		                ,MessageBoxButtons.OK,MessageBoxIcon.Error);
        		}
        	}
        }

		void setProject()
		{
				
			var currentProject = getCurrentProject();
			var projectForm = new SetProjectForm();
			if (currentProject != null) projectForm.projectName = currentProject.name;
			
			//set on root node
			var selectedRoot = this.model.selectedItem as TSF_EA.RootPackage;
			if (selectedRoot != null)
			{
				if (projectForm.ShowDialog(model.mainEAWindow) == DialogResult.OK
				   && projectForm.projectName.Length > 0)
				{
					selectedRoot.notes = "project=" + projectForm.projectName;
					selectedRoot.save();
				}
			}
			else
			{
				//set on package
				var selectedPackage = this.model.selectedItem as TSF_EA.Package;
				if (selectedPackage != null)
				{
					if (projectForm.ShowDialog(model.mainEAWindow) == DialogResult.OK
					   && projectForm.projectName.Length > 0)
					{
						selectedPackage.addTaggedValue("project",projectForm.projectName);
					}
				}
			}
		}
		/// <summary>
		/// returns the project based on the currently selected element.
		/// </summary>
		/// <returns>the TFSProject based on the currently selected item</returns>
        private TFS.TFSProject getCurrentProject()
        {
			var currentProject = TFS.TFSProject.getCurrentProject(this.model.selectedElement as TSF_EA.Element,this.settings.getTFSUrl(this.model),this.settings);
			if (currentProject == null)
			{
        		//if not found return default project
        		currentProject = new TFS.TFSProject(settings.defaultProject,this.settings.getTFSUrl(this.model),this.settings);
			}
			return currentProject;
        }


	}
	
	
	
	
}