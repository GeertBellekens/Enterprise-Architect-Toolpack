/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 15/03/2015
 * Time: 5:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

using EAAddinFramework.EASpecific;
using Microsoft.Win32;

namespace EAAddinManager
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class EAAddinManagerAddinClass: EAAddinFramework.EAAddinBase
	{
		public List<EAAddin> addins = new List<EAAddin>();
		private const string menuMain = "-&Addin Manager";
		private const string menuSettings = "&Manage Addins";
		
		internal EAAddinManagerConfig config;
		
		public EAAddinManagerAddinClass():base()
		{
			this.menuHeader = menuMain;
			this.menuOptions = new String[] {menuSettings};
			this.config = new EAAddinManagerConfig();
						
		}
		
		/// <summary>
		/// copy the remote addin files to the local folder
		/// </summary>
		/// <param name="addinConfig">the addin config to copy the files for</param>
		/// <param name="alwaysCopy">if false then only copy if remote version is newer</param>
		private void copyAddinToLocalFolder(AddinConfig addinConfig, bool alwaysCopy)
		{
			string localDllPath = this.config.getLocalAddinPath(addinConfig);
			foreach (string addinSearchPath in this.config.addinSearchPaths) 
			{
				string remoteDllPath = this.config.getRemoteAddinPath(addinConfig, addinSearchPath);
				if (File.Exists (remoteDllPath))
				{
					//found the right folder
					if (alwaysCopy || this.remoteVersionIsNewer(remoteDllPath,localDllPath))
					{
						//copy the remote folder over the local folder
						directoryCopy(addinSearchPath + addinConfig.name ,this.config.localAddinPath +  addinConfig.name , true);
					}
				}
			}
		}
		/// <summary>
		/// Loads all the addins defined in the config
		/// </summary>
		internal void loadAddins()
		{
			this.addins.Clear();
			//loop the addins
			foreach (AddinConfig addinConfig in this.config.addinConfigs) 
			{
				loadAddin(addinConfig);
			}
		}
		/// <summary>
		/// load a single addin as defined by the given config
		/// </summary>
		/// <param name="addinConfig">the config for the addin to load</param>
		private void loadAddin(AddinConfig addinConfig)
		{
			//first make sure we have the last version
			this.copyAddinToLocalFolder(addinConfig,false);
			string addinPath = this.config.getLocalAddinPath(addinConfig);
			if (addinConfig.load && File.Exists(addinPath))
			{
				addins.Add(new EAAddinFramework.EASpecific.EAAddin(addinPath, addinConfig.name));
			}
		}
		/// <summary>
		/// load an addin based on a filepath
		/// </summary>
		/// <param name="filePath">the filepath to the addin dll</param>
		/// <returns></returns>
		internal AddinConfig loadAddin(string filePath)
		{
			AddinConfig addinConfig = new AddinConfig(filePath);
			this.loadAddin(addinConfig);
			return addinConfig;
		}
		/// <summary>
		/// gets the latest version of the addin files
		/// </summary>
		/// <param name="directory">the directory to start from</param>
		/// <param name="subPath">the path to the actual files</param>
		private void getLatestVersionOfFiles(string directory, string subPath)
		{
			string localPath = Path.Combine(this.config.localAddinPath + subPath);
			//check all the files
			foreach (string filepath in Directory.GetFiles(localPath,"*.dll"))
			{
				string localFilePath = Path.Combine(localPath, Path.GetFileName(filepath));
				if (File.Exists(localFilePath))
				{
					FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(localFilePath);
					//fileInfo.FileVersion
				}
				               
			}
		}
		/// <summary>
		/// returns true if the remote version is newer then the local version
		/// if we can't use the version then the creation date will be used to compare the files
		/// </summary>
		/// <param name="remotePath">the path for the remote file</param>
		/// <param name="localPath">the path for the local file</param>
		/// <returns>true if the remote version is newer then the local version</returns>
		private bool remoteVersionIsNewer(string remotePath, string localPath)
		{
			if (File.Exists(localPath))
			{
				FileVersionInfo localFileInfo = FileVersionInfo.GetVersionInfo(localPath);
				FileVersionInfo remoteFileInfo = FileVersionInfo.GetVersionInfo(remotePath);
				try
				{
					Version localVersion = new Version(localFileInfo.FileVersion);
					Version remoteVersion = new Version(remoteFileInfo.FileVersion);
					int compareResult = localVersion.CompareTo(remoteVersion);
					if (compareResult < 0 )
					{
						return true;
					}
					else
					{
						return false;
					}
				} catch (System.FormatException)
				{
					//fileversion not in correct format. check on created date instead
					DateTime localCreationDate =  File.GetCreationTime(localPath);
					DateTime remoteCreationDate =  File.GetCreationTime(remotePath);
					int compareResult = localCreationDate.CompareTo(remoteCreationDate);
					if (compareResult > 0 )
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			else
			{
				//file doesn't exist locally so the remote version is definitely newer
				return true;
			}
		}
		/// <summary>
		/// copy the source directory to the destination directory
		/// </summary>
		/// <param name="sourceDirName">path to the source directory</param>
		/// <param name="destDirName">path to the target directory</param>
		/// <param name="copySubDirs">if true then all subdirectories will be copied as well</param>
		private static void directoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
	    {
	        // Get the subdirectories for the specified directory.
	        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
	        DirectoryInfo[] dirs = dir.GetDirectories();
	
	        if (!dir.Exists)
	        {
	            throw new DirectoryNotFoundException(
	                "Source directory does not exist or could not be found: "
	                + sourceDirName);
	        }
	
	        // If the destination directory doesn't exist, create it. 
	        if (!Directory.Exists(destDirName))
	        {
	            Directory.CreateDirectory(destDirName);
	        }
	
	        // Get the files in the directory and copy them to the new location.
	        FileInfo[] files = dir.GetFiles();
	        foreach (FileInfo file in files)
	        {
	            string temppath = Path.Combine(destDirName, file.Name);
	            file.CopyTo(temppath, true);
	        }
	
	        // If copying subdirectories, copy them and their contents to new location. 
	        if (copySubDirs)
	        {
	            foreach (DirectoryInfo subdir in dirs)
	            {
	                string temppath = Path.Combine(destDirName, subdir.Name);
	                directoryCopy(subdir.FullName, temppath, copySubDirs);
	            }
	        }
	    }
		
		#region callMethods
		/// <summary>
		/// executes the corresponding methods on the addins with the given metho name and returns result of the methods
		/// </summary>
		/// <param name="methodname">the name of the method to call</param>
		/// <param name="parameters">the parameters to pass to the method</param>
		/// <returns>the object returned by the addinmethod</returns>		
		private object callMethods(string methodname, object[] parameters)
		{
			object returnValue = null;
			foreach (EAAddin addin in this.addins) 
			{
				object result = addin.callmethod(methodname, parameters);
				if (result != null)
				{
					returnValue = result;
				}
			}
			return returnValue;
		}
		/// <summary>
		/// executes the scriptmethods with the given methodName and returns the boolean result of the method
		/// </summary>
		/// <param name="methodName">the name of the method to call</param>
		/// <param name="parameters">the parameters to pass to the method</param>
		/// <returns>the object returned by the addinmethod</returns>		
		private bool callMethods(string methodname, object[] parameters, bool defaultValue)
		{
			bool returnValue = defaultValue;
			//call the object method method
			object result = this.callMethods(methodname, parameters);
			//if the returnvalue is a booolean then we return that.
			if (result is bool)
			{
				returnValue = (bool)result;
			}
			return returnValue;
		}
		
		/// <summary>
		/// executes the scriptmethods with the given methodName and returns the long result of the method
		/// </summary>
		/// <param name="methodName">the name of the method to call</param>
		/// <param name="parameters">the parameters to pass to the method</param>
		/// <returns>the object returned by the addinmethod</returns>		
		private long callMethods(string methodname, object[] parameters, long defaultValue)
		{
			long returnValue = defaultValue;
			//call the object method method
			object result = this.callMethods(methodname, parameters);
			//if the returnvalue is a long then we return that.
			if (result is long)
			{
				returnValue = (long)result;
			}
			return returnValue;
		}
		
		/// <summary>
		/// executes the scriptmethods with the given methodName and returns the string result of the method
		/// </summary>
		/// <param name="methodName">the name of the method to call</param>
		/// <param name="parameters">the parameters to pass to the method</param>
		/// <returns>the object returned by the addinmethod</returns>		
		private string callMethods(string methodname, object[] parameters, string defaultValue)
		{
			string returnValue = defaultValue;
			//call the object method method
			object result = this.callMethods(methodname, parameters);
			//if the returnvalue is a booolean then we return that.
			if (result is string)
			{
				returnValue = (string)result;
			}
			return returnValue;
		}
		#endregion
		
		private void showSettings()
		{
			//show the form
			//debug
			//this.loadAddins();
			
			EAAddinManagerSettingsForm settingsForm = new EAAddinManagerSettingsForm(this);
			settingsForm.ShowDialog();
		}
		
		#region EA Add-In operations
		/// <summary>
        /// The EA_FileOpen event enables the Add-In to respond to a File Open event. When Enterprise Architect opens a new model file, this event is raised and passed to all Add-Ins implementing this method.
        /// The event occurs when the model being viewed by the Enterprise Architect user changes, for whatever reason (through user interaction or Add-In activity).
        /// Also look at EA_FileClose and EA_FileNew.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        public override void EA_FileOpen(EA.Repository Repository)
        {
			this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{Repository});
        }
		
        /// <summary>
        /// EA_MenuClick events are received by an Add-In in response to user selection of a menu option.
        /// The event is raised when the user clicks on a particular menu option. When a user clicks on one of your non-parent menu options, your Add-In receives a MenuClick event, defined as follows:
        /// Sub EA_MenuClick(Repository As EA.Repository, ByVal MenuName As String, ByVal ItemName As String)
        /// Notice that your code can directly access Enterprise Architect data and UI elements using Repository methods.
        /// Also look at EA_GetMenuItems.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="MenuLocation">String representing the part of the user interface that brought up the menu. 
        /// Can be TreeView, MainMenu or Diagram.</param>
        /// <param name="MenuName">The name of the parent menu for which sub-items must be defined. In the case of the top-level menu it is an empty string.</param>
        /// <param name="ItemName">The name of the option actually clicked, for example, Create a New Invoice.</param>
        public override void EA_MenuClick(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
        {
        	//add-in part
			switch (ItemName) 
			{
					//TODO: add own settings menu
				case menuSettings:
					this.showSettings();
					break;
			}
			
			foreach (EAAddin addin in this.addins) 
            {
            	if ( addin.lastMenuOptions.Contains(ItemName) 
				    && (addin.lastMenuOptions.Contains(MenuName)||MenuName == this.menuHeader || MenuName == string.Empty ))
            	{
            		addin.callmethod(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, MenuLocation,MenuName,ItemName});
            	}
			}
        }
    	

		#region EA Add-In Events
        

    
        public override string EA_Connect(EA.Repository Repository)
        {
        	//load the addins
        	this.loadAddins();
        	//call the same method on the addins
        	return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository},string.Empty);
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
        	//clear the last menu options for all addins
        	if (MenuName == string.Empty)
        	{
        		foreach (EAAddin addin in this.addins) 
        		{
        			addin.lastMenuOptions.Clear();
        		}
        	}
        	
        	if (this.addins.Count > 1)
			{
				//if there is more then one addin then we show "Addin Manager" as top level menu, then show menu options for each addin and only then show the actual menu
				// so - EA Addin Manager
				//	    - Addin 1
				//			Addin1 menu
				//	    - Addin 2
				//			Addin2 menu
				//		Addin Manager settings
				if (MenuName == string.Empty)
	        	{
	        		return this.menuHeader;
				}
				else if (MenuName == this.menuHeader)
				{
					List<string> addinMenuHeaders = new List<string>();
					foreach (EAAddin addin in this.addins) 
					{
						//return an array with the names of the addins
						object functionReturn = addin.callmethod(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, MenuLocation,string.Empty});
						// return submenu options
		        		if (functionReturn is object[])
		        		{
		        			string[] functionMenuOptions = Array.ConvertAll<object, string>((object [])functionReturn, o => o.ToString());
		        			//store the menu options as the last menuoptions for this add-in
		        			addin.lastMenuOptions.AddRange(functionMenuOptions);
		        			addinMenuHeaders.AddRange(functionMenuOptions);
		        		}
		        		else if (functionReturn is string && (string)functionReturn != string.Empty )
		        		{
		        			//store this as the last menu options
		        			addin.lastMenuOptions.Add((string)functionReturn);
		        			addinMenuHeaders.Add((string)functionReturn);
		        		}
					}
					//then add the menu settings tot the end
					addinMenuHeaders.Add(menuSettings);
					return addinMenuHeaders.ToArray<string>();
				}
				else
				{
					//then we call each addin with the menu name	
					List<string> addinMenuOptions = new List<string>();					
					foreach (EAAddin addin in this.addins) 
					{
						//we only need to do this if the menuname is in the list of last menu options returned by this addin
						if (addin.lastMenuOptions.Contains(MenuName))
						{
							//return an array with the names of the addins
							object functionReturn = addin.callmethod(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, MenuLocation,MenuName});
							// return submenu options
			        		if (functionReturn is object[])
			        		{
			        			string[] functionMenuOptions = Array.ConvertAll<object, string>((object [])functionReturn, o => o.ToString());
			        			//store the menu options as the last menuoptions for this add-in
		        				addin.lastMenuOptions.AddRange(functionMenuOptions);
			        			addinMenuOptions.AddRange(functionMenuOptions);
			        		}
			        		else if (functionReturn is string && (string)functionReturn != string.Empty)
			        		{
			        			//store this as the last menu options
		        				addin.lastMenuOptions.Add((string)functionReturn);
			        			addinMenuOptions.Add((string)functionReturn);
			        		}
						}
					}
					return addinMenuOptions.ToArray<string>();
				}
			}
        	else //addins.count <= 1
        	{
	        	object functionReturn = this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, MenuLocation,MenuName});
	        	//store the add-ins last menu options
	        	if (this.addins.Count == 1)
	        	{
	        		if (functionReturn is object[])
	        		{
	        			string[] functionMenuOptions = Array.ConvertAll<object, string>((object [])functionReturn, o => o.ToString());
	        			//store the menu options as the last menuoptions for this add-in
	        			this.addins[0].lastMenuOptions.AddRange(functionMenuOptions);
	
	        		}
	        		else if (functionReturn is string && (string)functionReturn != string.Empty)
	        		{
	        			//store this as the last menu options
	    				this.addins[0].lastMenuOptions.Add((string)functionReturn);
	        		}
	        	}
	        	//add an "about" menu option.
	        	if (MenuLocation == "MainMenu")
	        	{
		        	if (MenuName == string.Empty)
		        	{
		        		//return top level menu option
		        		if( functionReturn == null)
		        		{
		        			return this.menuHeader;
		        		}
		        	} 
		        	else
		        	{
		        		// return submenu options
		        		if (functionReturn is object[])
		        		{
		        			string[] functionMenuOptions = Array.ConvertAll<object, string>((object [])functionReturn, o => o.ToString());
		        			//add the about menu to the end
		        			List<string> newMenuOptions = functionMenuOptions.ToList();
		        			newMenuOptions.Add(menuSettings);
		        			return newMenuOptions.ToArray<string>();
		        		}
		        		else if (functionReturn is string && (string)functionReturn != string.Empty)
		        		{
		        			return (string)functionReturn;
		        		}
		        		else
		        		{
		        			return this.menuOptions;
		        		}
		        		
		        	} 
	        	}
	        	return functionReturn;
        	}

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
            //object returnValue = this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository,  MenuLocation, MenuName, ItemName, IsEnabled, IsChecked});
            foreach (EAAddin addin in this.addins) 
            {
            	if (addin.lastMenuOptions.Contains(ItemName)
            	    && (addin.lastMenuOptions.Contains(MenuName)||MenuName == this.menuHeader || MenuName == string.Empty ))
            	{
            		object[] refParams = new object[]{ Repository,  MenuLocation, MenuName, ItemName, IsEnabled, IsChecked};
            		object returnValue = addin.callmethod(MethodBase.GetCurrentMethod().Name,refParams); 
	        		//get IsEnabled
	    			IsEnabled = (bool) refParams[4];
					//get IsChecked
	    			IsChecked = (bool) refParams[5];
            	}
            }
        }


        /// <summary>
        /// The EA_Disconnect event enables the Add-In to respond to user requests to disconnect the model branch from an external project.
        /// This function is called when the Enterprise Architect closes. If you have stored references to Enterprise Architect objects (not particularly recommended anyway), you must release them here.
        /// In addition, .NET users must call memory management functions as shown below:
        /// GC.Collect();
        /// GC.WaitForPendingFinalizers();
        /// Also look at EA_Connect.
        /// </summary>
        public override void EA_Disconnect()
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,null);
        	base.EA_Disconnect();
        }

        
        /// <summary>
        /// EA_OnOutputItemClicked events inform Add-Ins that the user has clicked on a list entry in the system tab or one of the user defined output tabs.
        /// Usually an Add-In responds to this event in order to capture activity on an output tab they had previously created through a call to Repository.AddTab().
        /// Note that every loaded Add-In receives this event for every click on an output tab in Enterprise Architect - irrespective of whether the Add-In created that tab. Add-Ins should therefore check the TabName parameter supplied by this event to ensure that they are not responding to other Add-Ins' events.
        /// Also look at EA_OnOutputItemDoubleClicked.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="TabName">The name of the tab that the click occurred in. 
        /// Usually this would have been created through Repository.AddTab().</param>
        /// <param name="LineText">The text that had been supplied as the String parameter in the original call to Repository.WriteOutput().</param>
        /// <param name="ID">The ID value specified in the original call to Repository.WriteOutput().</param>
        public override void EA_OnOutputItemClicked(EA.Repository Repository ,string TabName , string LineText ,long ID)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, TabName,LineText,ID});
        }
		
        /// <summary>
        /// EA_OnOutputItemDoubleClicked events informs Add-Ins that the user has used the mouse to double-click on a list entry in one of the user-defined output tabs.
        /// Usually an Add-In responds to this event in order to capture activity on an output tab they had previously created through a call to Repository.AddTab().
        /// Note that every loaded Add-In receives this event for every double-click on an output tab in Enterprise Architect - irrespective of whether the Add-In created that tab. Add-Ins should therefore check the TabName parameter supplied by this event to ensure that they are not responding to other Add-Ins' events.
        /// Also look at EA_OnOutputItemClicked.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="TabName">The name of the tab that the click occurred in. 
        /// Usually this would have been created through Repository.AddTab().</param>
        /// <param name="LineText">The text that had been supplied as the String parameter in the original call to Repository.WriteOutput().</param>
        /// <param name="ID">The ID value specified in the original call to Repository.WriteOutput().</param>
        public override void EA_OnOutputItemDoubleClicked(EA.Repository Repository ,string TabName , string LineText ,long ID)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, TabName,LineText,ID});
        }
        
        /// <summary>
        /// The EA_ShowHelp event enables the Add-In to show a help topic for a particular menu option. When the user has an Add-In menu option selected, pressing [F1] can be delegated to the required Help topic by the Add-In and a suitable help message shown.
        /// This event is raised when the user presses [F1] on a menu option that is not a parent menu.
        /// Also look at EA_GetMenuItems.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="MenuLocation">String representing the part of the user interface that brought up the menu. 
        /// Can be Treeview, MainMenu or Diagram.</param>
        /// <param name="MenuName">The name of the parent menu for which sub-items are to be defined. In the case of the top-level menu it is an empty string.</param>
        /// <param name="ItemName">The name of the option actually clicked, for example, Create a New Invoice.</param>
        public override void EA_ShowHelp(EA.Repository Repository ,string MenuLocation ,string MenuName , string ItemName)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, MenuLocation,MenuName,ItemName});
        }


		#endregion EA Add-In Events
		        
        #region EA Broadcast Events

        
        /// <summary>
        /// The EA_FileClose event enables the Add-In to respond to a File Close event. When Enterprise Architect closes an opened Model file, this event is raised and passed to all Add-Ins implementing this method.
        /// This event occurs when the model currently opened within Enterprise Architect is about to be closed (when another model is about to be opened or when Enterprise Architect is about to shutdown).
        /// Also look at EA_FileOpen and EA_FileNew.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        public override void EA_FileClose(EA.Repository Repository)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{Repository});
        }

        /// <summary>
        /// The EA_FileNew event enables the Add-In to respond to a File New event. When Enterprise Architect creates a new model file, this event is raised and passed to all Add-Ins implementing this method.
        /// The event occurs when the model being viewed by the Enterprise Architect user changes, for whatever reason (through user interaction or Add-In activity).
        /// Also look at EA_FileClose and EA_FileOpen.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        public override void EA_FileNew(EA.Repository Repository)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{Repository});
        }
        
        /// <summary>
        /// EA_OnPostCloseDiagram notifies Add-Ins that a diagram has been closed.
        /// Also look at EA_OnPostOpenDiagram.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="DiagramID">Contains the Diagram ID of the diagram that was closed.</param>
        public override void EA_OnPostCloseDiagram(EA.Repository Repository ,int DiagramID)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, DiagramID});
        }
		
        /// <summary>
        /// EA_OnPostOpenDiagram notifies Add-Ins that a diagram has been opened.
        /// Also look at EA_OnPostCloseDiagram.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="DiagramID">Contains the Diagram ID of the diagram that was opened.</param>
        public override void EA_OnPostOpenDiagram(EA.Repository Repository ,int DiagramID)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, DiagramID});
        }
        
        /// <summary>
		/// EA_OnPreExitInstance is not currently used.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		public override void EA_OnPreExitInstance(EA.Repository Repository)
		{
			this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{Repository});
		}


//repository not ready yet
//        public override void EA_OnPostInitialized(EA.Repository Repository)
//		{
//	//this.callMethods(MethodBase.GetCurrentMethod().Name,null);
//		}
		
		/// <summary>
		/// EA_OnPostTransform notifies Add-Ins that an MDG transformation has taken place with the output in the specified target package.
		/// This event occurs when a user runs an MDG transform on one or more target packages. The notification is provided for each transform/target package immediately after all transform processes have completed.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the transform performed:
		/// - Transform: A string value corresponding to the name of the transform used
		/// - PackageID: A long value corresponding to Package.PackageID of the destination package. </param>
		/// <returns>Reserved for future use.</returns>
        public override bool EA_OnPostTransform(EA.Repository Repository, EA.EventProperties Info)
        {
        	return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
        }
        
        /// <summary>
        /// EA_OnRetrieveModelTemplate requests that an Add-In pass a model template to Enterprise Architect.
        /// This event occurs when a user executes the Add a New Model Using Wizard command to add a model that has been defined by an MDG Technology. See the Incorporate Model Templates topic for details of how to define such model templates.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="sLocation">The name of the template requested. This should match the location attribute in the [ModelTemplates] section of an MDG Technology File. For more information, see the Incorporate Model Templates in a Technology topic.</param>	
        /// <returns>Return a string containing the XMI export of the model that is being used as a template.</returns>
        public override string EA_OnRetrieveModelTemplate(EA.Repository Repository,string sLocation)
        {
        	return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, sLocation},string.Empty);
        }
        
        /// <summary>
        /// EA_OnTabChanged notifies Add-Ins that the currently open tab has changed.
        /// Diagrams do not generate the message when they are first opened - use the broadcast event EA_OnPostOpenDiagram for this purpose.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="TabName">The name of the tab to which focus has been switched.</param>
        /// <param name="DiagramID">The diagram ID, or 0 if switched to an Add-In tab.</param>
        public override void EA_OnTabChanged(EA.Repository Repository,string TabName ,int DiagramID)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, TabName,DiagramID});
        }
		
		#region Add-In License Management Events
		/// <summary>
		/// When a user directly enters a license key that doesn't match a Sparx Systems key into the License Management dialog EA_AddInLicenseValidate is broadcast to all Enterprise Architect Add-Ins, 
		/// providing them with a chance to use the Add-In key to determine the level of functionality to provide. 
		/// When a key is retrieved from the Sparx Systems Keystore only the target Add-In will be called with the key.
		/// For the Add-In to validate itself against this key, the Add-In's EA_AddinLicenseValidate handler should return true to confirm that the license has been validated. 
		/// As the EA_AddinLicenseValidate event is broadcast to all Add-Ins, one license can validate many Add-Ins.
		/// If an Add-In elects to handle a license key by returning true to EA_AddinLicenseValidate, it is called upon to provide a description of the license key through the 
		/// EA_AddinLicenseGetDescription event. 
		/// If more than one Add-In elects to handle a license key, the first Add-In that returns true to EA_AddinLicenseValidate is queried for the license key description.
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="AddinKey">The Add-in license key that has been entered in the License Management dialog.</param>
		/// <returns>For the Add-in to validate against this key it should return true to indicate that the key is valid and has been handled.</returns>
		public override bool EA_AddInLicenseValidate(EA.Repository Repository, string AddinKey)
		{
			bool validated = this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, AddinKey},false);
			if (!validated)
			{
				//TODO
//				//not validated by one of the managed add-ins, so we try to validate it ourselves
//				License eaLicense = new License(AddinKey, publicKey);
//				if (eaLicense.isValid)
//				{
//					//the license is valid so replace the evaluation license with this one
//					this.license = eaLicense;
//				}
//				return license.isValid;
			}
			return validated;
		}
		
		/// <summary>
		/// Before the Enterprise Architect License Management dialog is displayed, EA_AddInLicenseGetDescription is sent once for each Add-In key to the first Add-In that elected to handle that key. 
		/// The value returned by EA_AddinLicenseGetDescription is used as the key's plain text description.
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="AddinKey">The Add-In license key that Enterprise Architect requires a description for.</param>
		/// <returns>A String containing a plain text description of the provided AddinKey.</returns>
		public override string EA_AddinLicenseGetDescription(EA.Repository Repository, string AddinKey)
		{
			string licensedescription = this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, AddinKey},string.Empty);
			//TODO
//			License license = new License(AddinKey, publicKey);
//			if (license.isValid)
//			{
//				licensedescription = "License for EA-Matic issued to " + license.client; 
//			}
			return licensedescription;
		}
		

 		/// <summary>
		/// As an add-in writer you can distribute keys to your add-in via the Enterprise Architect Keystore providing your 
		/// keys are generated using a prefix that allows Enterprise Architect to identify the add-in to which they belong. 
		/// EA_GetSharedAddinName is called by Enterprise Architect to determine what prefix an add-in is using. 
		/// If a matching key is found in the keystore the License Management dialog will display the name returned 
		/// by EA_AddinLicenseGetDescription to your users. 
		/// Finally, when the user selects a key, that key will be passed to your add-in to validate 
		/// by calling EA_AddinLicenseValidate.
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <returns>A String containing a product name code for the provided Add-In. This will be shown in plain text at the start of any keys added to the keystore. We recommend contacting Sparx Systems directly with proposed values to ensure you don't clash with any other add-ins.
		/// eg. The following keys would all be interpreted as belonging to an add-in returning "MYADDIN" from this function:
		/// · MYADDIN-Test
		/// · MYADDIN-{7AC4D426-9083-4fa2-93B7-25E2B7FB8DC5}
		/// · MYADDIN-7AC4D426-9083-4fa2-93B7
		/// · MYADDIN-25E2B7FB8DC5
		/// · MYADDIN-2hDfHKA5jf0GAjn92UvqAnxwC13dxQGJtH7zLHJ9Ym8=</returns>
		public override string EA_GetSharedAddinName(EA.Repository Repository)
		{
			return "AddinManager";
		}
		
		#endregion
		
		#region EA Compartment Events
        
        /// <summary>
        /// This event occurs when Enterprise Architect's diagrams are refreshed. It is a request for the Add-In to provide a list of user-defined compartments. The EA_GetCompartmentData event then queries each object for the data to display in each user-defined compartment.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <returns>A String containing a comma-separated list of user-defined compartments.</returns>
         public override object EA_QueryAvailableCompartments(EA.Repository Repository)
         {
         	return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{Repository});
         }
         
        /// <summary>
        /// This event occurs when Enterprise Architect is instructed to redraw an element. It requests that the Add-In provide the data to populate the element's compartment.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="sCompartment">The name of the compartment for which data is being requested.</param>
        /// <param name="sGUID">The GUID of the element for which data is being requested.</param>
        /// <param name="oType">The type of the element for which data is being requested.</param>
        /// <returns>Variant containing a formatted string. See the example in the EA Help file to understand the format.</returns>
        public override object EA_GetCompartmentData(EA.Repository Repository ,string sCompartment ,string sGUID , EA.ObjectType oType ) 
        {
        	return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, sCompartment, sGUID, oType});
        }
        
        #endregion EA Compartment Events
        
        #region EA Context Item Events
        
        /// <summary>
        /// EA_OnContextItemChanged notifies Add-Ins that a different item is now in context.
        /// This event occurs after a user has selected an item anywhere in the Enterprise Architect GUI. Add-Ins that require knowledge of the current item in context can subscribe to this broadcast function. If ot = otRepository, then this function behaves the same as EA_FileOpen.
        /// Also look at EA_OnContextItemDoubleClicked and EA_OnNotifyContextItemModified.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="GUID">Contains the GUID of the new context item. 
        /// This value corresponds to the following properties, depending on the value of the ot parameter:
        /// ot (ObjectType)	- GUID value
        /// otElement  		- Element.ElementGUID
        /// otPackage 		- Package.PackageGUID
        /// otDiagram		- Diagram.DiagramGUID
        /// otAttribute		- Attribute.AttributeGUID
        /// otMethod		- Method.MethodGUID
        /// otConnector		- Connector.ConnectorGUID
        /// otRepository	- NOT APPLICABLE, GUID is an empty string
        /// </param>
        /// <param name="ot">Specifies the type of the new context item.</param>
        public override void EA_OnContextItemChanged(EA.Repository Repository,string GUID, EA.ObjectType ot)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, GUID, ot});
        }
        
        /// <summary>
        /// EA_OnContextItemDoubleClicked notifies Add-Ins that the user has double-clicked the item currently in context.
        /// This event occurs when a user has double-clicked (or pressed [Enter]) on the item in context, either in a diagram or in the Project Browser. Add-Ins to handle events can subscribe to this broadcast function.
        /// Also look at EA_OnContextItemChanged and EA_OnNotifyContextItemModified.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="GUID">Contains the GUID of the new context item. 
        /// This value corresponds to the following properties, depending on the value of the ot parameter:
        /// ot (ObjectType)	- GUID value
        /// otElement  		- Element.ElementGUID
        /// otPackage 		- Package.PackageGUID
        /// otDiagram		- Diagram.DiagramGUID
        /// otAttribute		- Attribute.AttributeGUID
        /// otMethod		- Method.MethodGUID
        /// otConnector		- Connector.ConnectorGUID
        /// </param>
        /// <param name="ot">Specifies the type of the new context item.</param>
        /// <returns>Return True to notify Enterprise Architect that the double-click event has been handled by an Add-In. Return False to enable Enterprise Architect to continue processing the event.</returns>
		public override bool EA_OnContextItemDoubleClicked(EA.Repository Repository, string GUID, EA.ObjectType ot)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, GUID,ot},false);
		}
        
        /// <summary>
        /// EA_OnNotifyContextItemModified notifies Add-Ins that the current context item has been modified.
        /// This event occurs when a user has modified the context item. Add-Ins that require knowledge of when an item has been modified can subscribe to this broadcast function.
        /// Also look at EA_OnContextItemChanged and EA_OnContextItemDoubleClicked.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="GUID">Contains the GUID of the new context item. 
        /// This value corresponds to the following properties, depending on the value of the ot parameter:
        /// ot (ObjectType)	- GUID value
        /// otElement  		- Element.ElementGUID
        /// otPackage 		- Package.PackageGUID
        /// otDiagram		- Diagram.DiagramGUID
        /// otAttribute		- Attribute.AttributeGUID
        /// otMethod		- Method.MethodGUID
        /// otConnector		- Connector.ConnectorGUID
        /// </param>
        /// <param name="ot">Specifies the type of the new context item.</param>
        public override void EA_OnNotifyContextItemModified(EA.Repository Repository,string GUID, EA.ObjectType ot)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, GUID, ot});
        }
        
        #endregion EA Context Item Events

        #region EA Model Validation Broadcasts
        

//repository is not ready yet
//        public override void EA_OnInitializeUserRules(EA.Repository Repository)
//        {
//        	//this.callMethods(MethodBase.GetCurrentMethod().Name,null);
//        }
        
        /// <summary>
        /// EA_OnStartValidation notifies Add-Ins that a user has invoked the model validation command from Enterprise Architect.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="Args">Contains a (Variant) list of Rule Categories that are active for the current invocation of model validation.</param>
        public override void EA_OnStartValidation(EA.Repository Repository,object Args)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Args});
        }
        
        /// <summary>
        /// EA_OnEndValidation notifies Add-Ins that model validation has completed. Use this event to arrange any clean-up operations arising from the validation.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="Args">Contains a (Variant) list of Rule Categories that were active for the invocation of model validation that has just completed.</param>
        public override void EA_OnEndValidation(EA.Repository Repository, object Args)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Args});
        }
        
        /// <summary>
        /// This event is triggered once for each rule defined in EA_OnInitializeUserRules to be performed on each element in the selection being validated. If you don't want to perform the rule defined by RuleID on the given element, then simply return without performing any action. On performing any validation, if a validation error is found, use the Repository.ProjectInterface.PublishResult method to notify Enterprise Architect.
        /// Also look at EA_OnInitializeUserRules.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="RuleID">The ID that was passed into the Project.DefineRule command.</param>
        /// <param name="Element">The element to potentially perform validation on.</param>
        public override void EA_OnRunElementRule(EA.Repository Repository,string RuleID, EA.Element Element)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, RuleID, Element});
        }
        
        /// <summary>
        /// This event is triggered once for each rule defined in EA_OnInitializeUserRules to be performed on each package in the selection being validated.
		/// If you don't want to perform the rule defined by RuleID on the given package, then simply return without performing any action. 
		/// On performing any validation, if a validation error is found, use the Repository.ProjectInterface.PublishResult method to notify Enterprise Architect.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="RuleID">The ID that was passed into the Project.DefineRule command.</param>
        /// <param name="PackageID">The ID of the package to potentially perform validation on. Use the Repository.GetPackageByID method to retrieve the package object.</param>
        public override void EA_OnRunPackageRule(EA.Repository Repository,string RuleID, long PackageID)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, RuleID, PackageID});
        }
        
        /// <summary>
        /// This event is triggered once for each rule defined in EA_OnInitializeUserRules to be performed on each diagram in the selection being validated.
		/// If you don't want to perform the rule defined by RuleID on the given diagram, then simply return without performing any action. 
		/// On performing any validation, if a validation error is found, use the Repository.ProjectInterface.PublishResult method to notify Enterprise Architect.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="RuleID">The ID that was passed into the Project.DefineRule command.</param>
        /// <param name="DiagramID">The ID of the diagram to potentially perform validation on. Use the Repository.GetDiagramByID method to retrieve the diagram object.</param>
        public override void EA_OnRunDiagramRule(EA.Repository Repository,string RuleID, long DiagramID)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, RuleID, DiagramID});
        }
        
        /// <summary>
        /// This event is triggered once for each rule defined in EA_OnInitializeUserRules to be performed on each connector in the selection being validated.
		/// If you don't want to perform the rule defined by RuleID on the given connector, then simply return without performing any action. 
		/// On performing any validation, if a validation error is found, use the Repository.ProjectInterface.PublishResult method to notify Enterprise Architect.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="RuleID">The ID that was passed into the Project.DefineRule command.</param>
        /// <param name="ConnectorID">The ID of the connector to potentially perform validation on. Use the Repository.GetConnectorByID method to retrieve the connector object.</param>
        public override void EA_OnRunConnectorRule(EA.Repository Repository,string RuleID, long ConnectorID)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, RuleID, ConnectorID});
        }
        
        /// <summary>
        /// This event is triggered once for each rule defined in EA_OnInitializeUserRules to be performed on each attribute in the selection being validated.
		/// If you don't want to perform the rule defined by RuleID on the given attribute, then simply return without performing any action. 
		/// On performing any validation, if a validation error is found, use the Repository.ProjectInterface.PublishResult method to notify Enterprise Architect.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="RuleID">The ID that was passed into the Project.DefineRule command.</param>
        /// <param name="AttributeGUID">The GUID of the attribute to potentially perform validation on. Use the Repository.GetAttributeByGUID method to retrieve the attribute object.</param>
        /// <param name="ObjectID">The ID of the object that owns the given attribute. Use the Repository.GetObjectByID method to retrieve the object.</param>
        public override void EA_OnRunAttributeRule(EA.Repository Repository,string RuleID, string AttributeGUID,long ObjectID)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, RuleID, AttributeGUID, ObjectID});
        }
        
        /// <summary>
        /// This event is triggered once for each rule defined in EA_OnInitializeUserRules to be performed on each method in the selection being validated.
		/// If you don't want to perform the rule defined by RuleID on the given method, then simply return without performing any action. 
		/// On performing any validation, if a validation error is found, use the Repository.ProjectInterface.PublishResult method to notify Enterprise Architect.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="RuleID">The ID that was passed into the Project.DefineRule command.</param>
        /// <param name="MethodGUID">The GUID of the method to potentially perform validation on. Use the Repository.GetMethodByGUID method to retrieve the method object.</param>
        /// <param name="ObjectID">The ID of the object that owns the given method. Use the Repository.GetObjectByID method to retrieve the object.</param>
        public override void EA_OnRunMethodRule(EA.Repository Repository,string RuleID, string MethodGUID,long ObjectID)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, RuleID, MethodGUID, ObjectID});
        }
        
        /// <summary>
        /// This event is triggered once for each rule defined in EA_OnInitializeUserRules to be performed on each parameter in the selection being validated.
		/// If you don't want to perform the rule defined by RuleID on the given parameter, then simply return without performing any action. 
		/// On performing any validation, if a validation error is found, use the Repository.ProjectInterface.PublishResult parameter to notify Enterprise Architect.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="RuleID">The ID that was passed into the Project.DefineRule command.</param>
        /// <param name="ParameterGUID">The GUID of the parameter to potentially perform validation on. Use the Repository.GetParameterByGUID parameter to retrieve the parameter object.</param>
        /// <param name="MethodGUID">The GUID of the method that owns the given parameter. Use the Repository.GetMethodByGuid method to retrieve the method object.</param> 
        /// <param name="ObjectID">The ID of the object that owns the given parameter. Use the Repository.GetObjectByID parameter to retrieve the object.</param>
        public override void EA_OnRunParameterRule(EA.Repository Repository,string RuleID, string ParameterGUID,string MethodGUID, long ObjectID)
        {
        	this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, RuleID, ParameterGUID, MethodGUID, ObjectID});
        }
                
        #endregion EA Model Validation Broadcasts        
        
		#region EA Post-New Events
		
		/// <summary>
		/// EA_OnPostNewElement notifies Add-Ins that a new element has been created on a diagram. It enables Add-Ins to modify the element upon creation.
		/// This event occurs after a user has dragged a new element from the Toolbox or Resources window onto a diagram. The notification is provided immediately after the element is added to the model. Set Repository.SuppressEADialogs to true to suppress Enterprise Architect from showing its default dialogs.
		/// Also look at EA_OnPreNewElement.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the new element:
		/// - ElementID: A long value corresponding to Element.ElementID. </param>
		/// <returns>Return True if the element has been updated during this notification. Return False otherwise.</returns>
		public override bool EA_OnPostNewElement(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},false);
		}
		
		/// <summary>
		/// EA_OnPostNewConnector notifies Add-Ins that a new connector has been created on a diagram. It enables Add-Ins to modify the connector upon creation.
		/// This event occurs after a user has dragged a new connector from the Toolbox or Resources window onto a diagram. The notification is provided immediately after the connector is added to the model. Set Repository.SuppressEADialogs to true to suppress Enterprise Architect from showing its default dialogs.
		/// Also look at EA_OnPreNewConnector.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the new connector:
		/// - ConnectorID: A long value corresponding to Connector.ConnectorID.
		/// </param>
		/// <returns>Return True if the connector has been updated during this notification. Return False otherwise.</returns>
		public override bool EA_OnPostNewConnector(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},false);
		}
		
		/// <summary>
		/// EA_OnPostNewDiagram notifies Add-Ins that a new diagram has been created. It enables Add-Ins to modify the diagram upon creation.
		/// Set Repository.SuppressEADialogs to true to suppress Enterprise Architect from showing its default dialogs.
		/// Also look at EA_OnPreNewDiagram.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the new diagram:
		/// - DiagramID: A long value corresponding to Diagram.DiagramID.</param>
		/// <returns>Return True if the diagram has been updated during this notification. Return False otherwise.</returns>
		public override bool EA_OnPostNewDiagram(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},false);
		}
				
		/// <summary>
		/// EA_OnPostNewDiagramObject notifies Add-Ins that a new object has been created on a diagram. It enables Add-Ins to modify the object upon creation.
		/// This event occurs after a user has dragged a new object from the Project Browser or Resources window onto a diagram. The notification is provided immediately after the object is added to the diagram. Set Repository.SuppressEADialogs to true to suppress Enterprise Architect from showing its default dialogs.
		/// Also look at EA_OnPreNewDiagramObject.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the new element:
		/// - ObjectID: A long value corresponding to Object.ObjectID.</param>
		/// <returns>Return True if the element has been updated during this notification. Return False otherwise.</returns>
		public override bool EA_OnPostNewDiagramObject(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},false);
		}
		
		/// <summary>
		/// EA_OnPostNewAttribute notifies Add-Ins that a new attribute has been created on a diagram. It enables Add-Ins to modify the attribute upon creation.
		/// This event occurs when a user creates a new attribute on an element by either drag-dropping from the Project Browser, using the Attributes Properties dialog, or using the in-place editor on the diagram. The notification is provided immediately after the attribute is created. Set Repository.SuppressEADialogs to true to suppress Enterprise Architect from showing its default dialogs.
		/// Also look at EA_OnPreNewAttribute.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the new attribute:
		/// - AttributeID: A long value corresponding to Attribute.AttributeID.</param>
		/// <returns>Return True if the attribute has been updated during this notification. Return False otherwise.</returns>
		public override bool EA_OnPostNewAttribute(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},false);
		}
		
		/// <summary>
		/// EA_OnPostNewMethod notifies Add-Ins that a new method has been created on a diagram. It enables Add-Ins to modify the method upon creation.
		/// This event occurs when a user creates a new method on an element by either drag-dropping from the Project Browser, using the method's Properties dialog, or using the in-place editor on the diagram. The notification is provided immediately after the method is created. Set Repository.SuppressEADialogs to true to suppress Enterprise Architect from showing its default dialogs.
		/// Also look at EA_OnPreNewMethod.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the new method:
		/// - MethodID: A long value corresponding to Method.MethodID.</param>
		/// <returns>Return True if the method has been updated during this notification. Return False otherwise.</returns>
		public override bool EA_OnPostNewMethod(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},false);
		}
					
		/// <summary>
		/// EA_OnPostNewPackage notifies Add-Ins that a new package has been created on a diagram. It enables Add-Ins to modify the package upon creation.
		/// This event occurs when a user drags a new package from the Toolbox or Resources window onto a diagram, or by selecting the New Package icon from the Project Browser. Set Repository.SuppressEADialogs to true to suppress Enterprise Architect from showing its default dialogs.
		/// Also look at EA_OnPreNewPackage.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the new package:
		/// - PackageID: A long value corresponding to Package.PackageID.</param>
		/// <returns>Return True if the package has been updated during this notification. Return False otherwise.</returns>
		public override bool EA_OnPostNewPackage(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},false);
		}
		
		/// <summary>
		/// EA_OnPostNewGlossaryTerm notifies Add-Ins that a new glossary term has been created. It enables Add-Ins to modify the glossary term upon creation.
		/// The notification is provided immediately after the glossary term is added to the model. Set Repository.SuppressEADialogs to true to suppress Enterprise Architect from showing its default dialogs.
		/// Also look at EA_OnPreNewGlossaryTerm.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the glossary term to be deleted:
		/// TermID: A long value corresponding to Term.TermID.</param>
		/// <returns>Return True if the glossary term has been updated during this notification. Return False otherwise.</returns>
		public override bool EA_OnPostNewGlossaryTerm (EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},false);
		}
		
		#endregion EA Post-New Events        
        
		#region EA Pre-Deletion Events
		
		/// <summary>
		/// EA_OnPreDeleteElement notifies Add-Ins that an element is to be deleted from the model. It enables Add-Ins to permit or deny deletion of the element.
		/// This event occurs when a user deletes an element from the Project Browser or on a diagram. 
		/// The notification is provided immediately before the element is deleted, so that the Add-In can disable deletion of the element.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the element to be deleted:
		/// - ElementID: A long value corresponding to Element.ElementID.</param>	
		/// <returns>Return True to enable deletion of the element from the model. Return False to disable deletion of the element.</returns>
		public override bool EA_OnPreDeleteElement(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		/// <summary>
		/// EA_OnPreDeleteAttribute notifies Add-Ins that an attribute is to be deleted from the model. It enables Add-Ins to permit or deny deletion of the attribute.
		/// This event occurs when a user deletes an attribute from the Project Browser or on a diagram. 
		/// The notification is provided immediately before the attribute is deleted, so that the Add-In can disable deletion of the attribute.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the Attribute to be deleted:
		/// - AttributeID: A long value corresponding to Attribute.AttributeID.</param>	
		/// <returns>Return True to enable deletion of the attribute from the model. Return False to disable deletion of the attribute.</returns>
		public override bool EA_OnPreDeleteAttribute(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		/// <summary>
		/// EA_OnPreDeleteMethod notifies Add-Ins that an method is to be deleted from the model. It enables Add-Ins to permit or deny deletion of the method.
		/// This event occurs when a user deletes an method from the Project Browser or on a diagram. 
		/// The notification is provided immediately before the method is deleted, so that the Add-In can disable deletion of the method.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the Method to be deleted:
		/// - MethodID: A long value corresponding to Method.MethodID.</param>	
		/// <returns>Return True to enable deletion of the method from the model. Return False to disable deletion of the method.</returns>
		public override bool EA_OnPreDeleteMethod(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		/// <summary>
		/// EA_OnPreDeleteConnector notifies Add-Ins that an connector is to be deleted from the model. It enables Add-Ins to permit or deny deletion of the connector.
		/// This event occurs when a user attempts to permanently delete a connector on a diagram.
		/// The notification is provided immediately before the connector is deleted, so that the Add-In can disable deletion of the connector.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the connector to be deleted:
		/// - ConnectorID: A long value corresponding to Connector.ConnectorID.</param>	
		/// <returns>Return True to enable deletion of the connector from the model. Return False to disable deletion of the connector.</returns>
		public override bool EA_OnPreDeleteConnector(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		/// <summary>
		/// EA_OnPreDeleteDiagram notifies Add-Ins that an diagram is to be deleted from the model. It enables Add-Ins to permit or deny deletion of the diagram.
		/// This event occurs when a user attempts to permanently delete a diagram from the Project Browser.
		/// The notification is provided immediately before the diagram is deleted, so that the Add-In can disable deletion of the diagram.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the diagram to be deleted:
		/// - DiagramID: A long value corresponding to Diagram.DiagramID.</param>	
		/// <returns>Return True to enable deletion of the diagram from the model. Return False to disable deletion of the diagram.</returns>
		public override bool EA_OnPreDeleteDiagram(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		/// <summary>
		/// EA_OnPreDeleteDiagramObject notifies Add-Ins that a diagram object is to be deleted from the model. It enables Add-Ins to permit or deny deletion of the element.
		/// This event occurs when a user attempts to permanently delete an element from a diagram. The notification is provided immediately before the element is deleted, so that the Add-In can disable deletion of the element.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the element to be deleted:
		/// · ID: A long value corresponding to DiagramObject.ElementID</param>
		/// <returns>Return True to enable deletion of the element from the model. Return False to disable deletion of the element.</returns>
		public override bool EA_OnPreDeleteDiagramObject(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		/// <summary>
		/// EA_OnPreDeletePackage notifies Add-Ins that an package is to be deleted from the model. It enables Add-Ins to permit or deny deletion of the package.
		/// This event occurs when a user attempts to permanently delete a package from the Project Browser.
		/// The notification is provided immediately before the package is deleted, so that the Add-In can disable deletion of the package.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty object for the package to be deleted:
		/// - PackageID: A long value corresponding to Package.PackageID.</param>	
		/// <returns>Return True to enable deletion of the package from the model. Return False to disable deletion of the package.</returns>
		public override bool EA_OnPreDeletePackage(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		/// <summary>
		/// EA_OnPreDeleteGlossaryTerm notifies Add-Ins that a glossary term is to be deleted from the model. It enables Add-Ins to permit or deny deletion of the glossary term.
		/// The notification is provided immediately before the glossary term is deleted, so that the Add-In can disable deletion of the glossary term.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the glossary term to be deleted:
		/// TermID: A long value corresponding to Term.TermID.</param>
		/// <returns>Return True to enable deletion of the glossary term from the model. Return False to disable deletion of the glossary term.</returns>
		public override bool EA_OnPreDeleteGlossaryTerm (EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		
		#endregion EA Pre-Deletion Events
		
		#region EA Pre-New-Object Events
		
		/// <summary>
		/// EA_OnPreNewElement notifies Add-Ins that a new element is about to be created on a diagram. It enables Add-Ins to permit or deny creation of the new element.
		/// This event occurs when a user drags a new element from the Toolbox or Resources window onto a diagram. 
		/// The notification is provided immediately before the element is created, so that the Add-In can disable addition of the element.
		/// Also look at EA_OnPostNewElement.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the element to be created:
		/// - Type: A string value corresponding to Element.Type
		/// - Stereotype: A string value corresponding to Element.Stereotype
		/// - ParentID: A long value corresponding to Element.ParentID
		/// - DiagramID: A long value corresponding to the ID of the diagram to which the element is being added. </param>
		/// <returns>Return True to enable addition of the new element to the model. Return False to disable addition of the new element.</returns>
		public override bool EA_OnPreNewElement(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		/// <summary>
		/// EA_OnPreNewConnector notifies Add-Ins that a new connector is about to be created on a diagram. It enables Add-Ins to permit or deny creation of a new connector.
		/// This event occurs when a user drags a new connector from the Toolbox or Resources window, onto a diagram. The notification is provided immediately before the connector is created, so that the Add-In can disable addition of the connector.
		/// Also look at EA_OnPostNewConnector.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the connector to be created:
		/// - Type: A string value corresponding to Connector.Type
		/// - Subtype: A string value corresponding to Connector.Subtype
		/// - Stereotype: A string value corresponding to Connector.Stereotype
		/// - ClientID: A long value corresponding to Connector.ClientID
		/// - SupplierID: A long value corresponding to Connector.SupplierID
		/// - DiagramID: A long value corresponding to Connector.DiagramID.
		/// </param>
		/// <returns>Return True to enable addition of the new connector to the model. Return False to disable addition of the new connector.</returns>
		public override bool EA_OnPreNewConnector(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		/// <summary>
		/// EA_OnPreNewDiagram notifies Add-Ins that a new diagram is about to be created. It enables Add-Ins to permit or deny creation of the new diagram.
		/// The notification is provided immediately before the diagram is created, so that the Add-In can disable addition of the diagram.
		/// Also look at EA_OnPostNewDiagram.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the diagram to be created:
		/// - Type: A string value corresponding to Diagram.Type
		/// - ParentID: A long value corresponding to Diagram.ParentID
		/// - PackageID: A long value corresponding to Diagram.PackageID. </param>
		/// <returns>Return True to enable addition of the new diagram to the model. Return False to disable addition of the new diagram.</returns>
		public override bool EA_OnPreNewDiagram(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
				
		/// <summary>
		/// EA_OnPreNewDiagramObject notifies Add-Ins that a new diagram object is about to be dropped on a diagram. It enables Add-Ins to permit or deny creation of the new object.
		/// This event occurs when a user drags an object from the Enterprise Architect Project Browser or Resources window onto a diagram. The notification is provided immediately before the object is created, so that the Add-In can disable addition of the object.
		/// Also look at EA_OnPostNewDiagramObject.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the object to be created:
		/// - Type: A string value corresponding to Object.Type
		/// - Stereotype: A string value corresponding to Object.Stereotype
		/// - ParentID: A long value corresponding to Object.ParentID
		/// - DiagramID: A long value corresponding to the ID of the diagram to which the object is being added. </param>
		/// <returns>Return True to enable addition of the object to the model. Return False to disable addition of the object.</returns>
		public override bool EA_OnPreNewDiagramObject(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		/// <summary>
		/// When a user drags any kind of element from the Project Browser onto a diagram, EA_OnPreDropFromTree notifies the Add-In that a new item is about to be dropped onto a diagram. The notification is provided immediately before the element is dropped, so that the Add-In can override the default action that would be taken for this drag.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the element to be created:
		/// · ID: A long value of the type being dropped
		/// · Type: A string value corresponding to type of element being dropped
		/// · DiagramID: A long value corresponding to the ID of the diagram to which the element is being added
		/// · PositionX: The X coordinate into which the element is being dropped
		/// · PositionY: The Y coordinate into which the element is being dropped
		/// · DroppedID: A long value corresponding to the ID of the element the item has been dropped onto</param>
		/// <returns>Returns True to allow the default behavior to be executed. Return False if you are overriding this behavior.</returns>
		public override bool EA_OnPreDropFromTree(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		 
		/// <summary>
		/// EA_OnPreNewAttribute notifies Add-Ins that a new attribute is about to be created on an element. It enables Add-Ins to permit or deny creation of the new attribute.
		/// This event occurs when a user creates a new attribute on an element by either drag-dropping from the Project Browser, using the Attributes Properties dialog, or using the in-place editor on the diagram. The notification is provided immediately before the attribute is created, so that the Add-In can disable addition of the attribute.
		/// Also look at EA_OnPostNewAttribute.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the attribute to be created:
		/// - Type: A string value corresponding to Attribute.Type
		/// - Stereotype: A string value corresponding to Attribute.Stereotype
		/// - ParentID: A long value corresponding to Attribute.ParentID
		/// - ClassifierID: A long value corresponding to Attribute.ClassifierID. </param>
		/// <returns>Return True to enable addition of the new attribute to the model. Return False to disable addition of the new attribute.</returns>
		public override bool EA_OnPreNewAttribute(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		/// <summary>
		/// EA_OnPreNewMethod notifies Add-Ins that a new method is about to be created on an element. It enables Add-Ins to permit or deny creation of the new method.
		/// This event occurs when a user creates a new method on an element by either drag-dropping from the Project Browser, using the method Properties dialog, or using the in-place editor on the diagram. The notification is provided immediately before the method is created, so that the Add-In can disable addition of the method.
		/// Also look at EA_OnPostNewMethod.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the method to be created:
		/// - ReturnType: A string value corresponding to Method.ReturnType
		/// - Stereotype: A string value corresponding to Method.Stereotype
		/// - ParentID: A long value corresponding to Method.ParentID
		/// - ClassifierID: A long value corresponding to Method.ClassifierID. </param>
		/// <returns>Return True to enable addition of the new method to the model. Return False to disable addition of the new method.</returns>
		public override bool EA_OnPreNewMethod(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
					
		/// <summary>
		/// EA_OnPreNewPackage notifies Add-Ins that a new package is about to be created in the model. It enables Add-Ins to permit or deny creation of the new package.
		/// This event occurs when a user drags a new package from the Toolbox or Resources window onto a diagram, or by selecting the New Package icon from the Project Browser. The notification is provided immediately before the package is created, so that the Add-In can disable addition of the package.
		/// Also look at EA_OnPostNewPackage.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the package to be created:
		/// Stereotype: A string value corresponding to Package.Stereotype
		/// ParentID: A long value corresponding to Package.ParentID
		/// DiagramID: A long value corresponding to the ID of the diagram to which the package is being added. </param>
		/// <returns>Return True to enable addition of the new package to the model. Return False to disable addition of the new package.</returns>
		public override bool EA_OnPreNewPackage(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
		
		/// <summary>
		/// EA_OnPreNewGlossaryTerm notifies Add-Ins that a new glossary term is about to be created. It enables Add-Ins to permit or deny creation of the new glossary term.
		/// The notification is provided immediately before the glossary term is created, so that the Add-In can disable addition of the element.
		/// Also look at EA_OnPostNewGlossaryTerm.
		/// </summary>
		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="Info">Contains the following EventProperty objects for the glossary term to be deleted:
		/// TermID: A long value corresponding to Term.TermID.</param>
		/// <returns>Return True to enable addition of the new glossary term to the model. Return False to disable addition of the new glossary term.</returns>
		public override bool EA_OnPreNewGlossaryTerm (EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
				
		#endregion EA Pre-New Events
		
        #region EA Tagged Value Broadcasts
        /// <summary>
        /// EA_OnAttributeTagEdit is called when the user clicks the ellipsis ( ... ) button 
        /// for a Tagged Value of type AddinBroadcast on an attribute.
        /// The Add-In displays fields to show and change the value and notes; this function 
        /// provides the initial values for the Tagged Value notes and value, and takes on any 
        /// changes on exit of the function.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open 
        /// Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="AttributeID">The ID of the attribute that this Tagged Value is on</param>
        /// <param name="TagName">The name of the Tagged Value to edit.</param>
        /// <param name="TagValue">The current value of the tag; if the value is updated, 
        /// the new value is stored in the repository on exit of the function.</param>
        /// <param name="TagNotes">The current value of the Tagged Value notes; if the value 
        /// is updated, the new value is stored in the repository on exit of the function.</param>
        public override void EA_OnAttributeTagEdit(EA.Repository Repository, long AttributeID, ref string TagName, ref string TagValue, ref string TagNotes) 
        {
        	object[] refParams = new object[]{ Repository, AttributeID,TagName,TagValue,TagNotes};
        	this.callMethods(MethodBase.GetCurrentMethod().Name,refParams);
        	//get reference parameter values
        	TagName = (string) refParams[2];
        	TagValue = (string) refParams[3];
        	TagNotes = (string) refParams[4];
        }
		
        /// <summary>
        /// EA_OnConnectorTagEdit is called when the user clicks the ellipsis ( ... ) button 
        /// for a Tagged Value of type AddinBroadcast on a connector.The Add-In displays fields
		/// to show and change the value and notes; this function provides the initial values 
		/// for the Tagged Value notes and value, and takes on any changes on exit of the function
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open 
        /// Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="ConnectorID">The ID of the connector that this Tagged Value is on.</param>
        /// <param name="TagName">The name of the Tagged Value to edit.</param>
        /// <param name="TagValue">The current value of the tag; if the value is updated, 
        /// the new value is stored in the repository on exit of the function.</param>
        /// <param name="TagNotes">The current value of the Tagged Value notes; if the value 
        public override void EA_OnConnectorTagEdit(EA.Repository Repository, long ConnectorID, ref string TagName, ref string TagValue, ref string TagNotes) 
        {
        	object[] refParams = new object[]{ Repository, ConnectorID,TagName,TagValue,TagNotes};
        	this.callMethods(MethodBase.GetCurrentMethod().Name,refParams);
        	//get reference parameter values
        	TagName = (string) refParams[2];
        	TagValue = (string) refParams[3];
        	TagNotes = (string) refParams[4];     	
        }

        /// <summary>
        /// EA_OnElementTagEdit is called when the user clicks the ellipsis ( ... ) button 
        /// for a Tagged Value of type AddinBroadcast on a element.The Add-In displays fields
		/// to show and change the value and notes; this function provides the initial values 
		/// for the Tagged Value notes and value, and takes on any changes on exit of the function
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open 
        /// Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="ObjectID">The ID of the object (element) that this Tagged Value is on.</param>
        /// <param name="TagName">The name of the Tagged Value to edit.</param>
        /// <param name="TagValue">The current value of the tag; if the value is updated, 
        /// the new value is stored in the repository on exit of the function.</param>
        /// <param name="TagNotes">The current value of the Tagged Value notes; if the value 
        public override void EA_OnElementTagEdit(EA.Repository Repository, long ObjectID, ref string TagName, ref string TagValue, ref string TagNotes) 
        {
        	object[] refParams = new object[]{ Repository, ObjectID,TagName,TagValue,TagNotes};
        	this.callMethods(MethodBase.GetCurrentMethod().Name,refParams);
        	//get reference parameter values
        	TagName = (string) refParams[2];
        	TagValue = (string) refParams[3];
        	TagNotes = (string) refParams[4];            	
        }

        /// <summary>
        /// EA_OnMethodTagEdit is called when the user clicks the ellipsis ( ... ) button 
        /// for a Tagged Value of type AddinBroadcast on a method.The Add-In displays fields
		/// to show and change the value and notes; this function provides the initial values 
		/// for the Tagged Value notes and value, and takes on any changes on exit of the function
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open 
        /// Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="MethodID">The ID of the method that this Tagged Value is on.</param>
        /// <param name="TagName">The name of the Tagged Value to edit.</param>
        /// <param name="TagValue">The current value of the tag; if the value is updated, 
        /// the new value is stored in the repository on exit of the function.</param>
        /// <param name="TagNotes">The current value of the Tagged Value notes; if the value 
        public override void EA_OnMethodTagEdit(EA.Repository Repository, long MethodID, ref string TagName, ref string TagValue, ref string TagNotes) 
        {
        	object[] refParams = new object[]{ Repository, MethodID, TagName, TagValue, TagNotes};
        	this.callMethods(MethodBase.GetCurrentMethod().Name,refParams);
        	//get reference parameter values
        	TagName = (string) refParams[2];
        	TagValue = (string) refParams[3];
        	TagNotes = (string) refParams[4];            	
        }
        #endregion
		
        #region EA Technology Events
        


        public override object EA_OnInitializeTechnologies(EA.Repository Repository)
        {
        	return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{Repository});
        }
     
        public override bool EA_OnPreActivateTechnology(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
        

        public override bool EA_OnPostActivateTechnology(EA.Repository Repository, EA.EventProperties Info)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, Info},true);
		}
                
        #endregion EA Technology Events
        
  
		#endregion EA Broadcast Events   
		
		#region EA MDG Events
		
		/// <summary>
		/// MDG_BuildProject enables the Add-In to handle file changes caused by generation. This function is called in response to a user selecting the Add-Ins | Build Project menu option.
		/// Respond to this event by compiling the project source files into a running application.
		/// Also look at MDG_RunExe.
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageGuid">The GUID identifying the Enterprise Architect package sub-tree that is controlled by the Add-In.</param>
		public override void MDG_BuildProject(EA.Repository Repository, string PackageGuid)
		{
			this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, PackageGuid});
		}
		
		/// <summary>
		/// MDG_Connect enables the Add-In to handle user driven request to connect a model branch to an external application. This function is called when the user attempts to connect a particular Enterprise Architect package to an as yet unspecified external project. This event enables the Add-In to interact with the user to specify such a project.
		/// The Add-In is responsible for retaining the connection details, which should be stored on a per-user or per-workstation basis. That is, users who share a common Enterprise Architect model over a network should be able to connect and disconnect to external projects independently of one another.
		/// The Add-In should therefore not store connection details in an Enterprise Architect repository. A suitable place to store such details would be:
		/// SHGetFolderPath(..CSIDL_APPDATA..)\AddinName.
		/// The PackageGuid parameter is the same identifier as required for most events relating to the MDG Add-In. Therefore it is recommended that the connection details be indexed using the PackageGuid value.
		/// The PackageID parameter is provided to aid fast retrieval of package details from Enterprise Architect, should this be required.
		/// Also look at MDG_Disconnect.
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageID">The PackageID of the Enterprise Architect package the user has requested to have connected to an external project.</param>
		/// <param name="PackageGuid">The unique ID identifying the project provided by the Add-In when a connection to a project branch of an Enterprise Architect model was first established.</param>
		/// <returns>Returns a non-zero to indicate that a connection has been made; a zero indicates that the user has not nominated a project and connection should not proceed.</returns>
		public override long MDG_Connect(EA.Repository Repository,long PackageID,string PackageGuid)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, PackageID,PackageGuid},0L);
		}
		
		/// <summary>
		/// MDG_Disconnect enables the Add-In to respond to user requests to disconnect the model branch from an external project. This function is called when the user attempts to disconnect an associated external project. The Add-In is required to delete the details of the connection.
		/// Also look at MDG_Connect.
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageGuid">The GUID identifying the Enterprise Architect package sub-tree that is controlled by the Add-In.</param>
		/// <returns>Returns a non-zero to indicate that a disconnection has occurred enabling Enterprise Architect to update the user interface. A zero indicates that the user has not disconnected from an external project.</returns>
		public override long MDG_Disconnect(EA.Repository Repository ,string PackageGuid )
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, PackageGuid},0L);
		}
		
		/// <summary>
		/// MDG_GetConnectedPackages enables the Add-In to return a list of current connection between Enterprise Architect and an external application. This function is called when the Add-In is first loaded, and is expected to return a list of the available connections to external projects for this Add-In.
		/// Also look at MDG_Connect
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <returns>Returns an array of GUID strings representing individual Enterprise Architect packages.</returns>
		public override object MDG_GetConnectedPackages(EA.Repository Repository)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{Repository});
		}
		
		/// <summary>
		/// MDG_GetProperty provides miscellaneous Add-In details to Enterprise Architect. This function is called by Enterprise Architect to poll the Add-In for information relating to the PropertyName. This event should occur in as short a duration as possible as Enterprise Architect does not cache the information provided by the function.
		/// Values corresponding to the following PropertyNames must be provided:
		/// - IconID - Return the name of a DLL and a resource identifier in the format #ResID, where the resource ID indicates an Icon; for example, c:\program files\myapp\myapp.dlll#101
		/// - Language - Return the default language that Classes should be assigned when they are created in Enterprise Architect
		/// - HiddenMenus - Return one or more values from the MDGMenus enumeration to hide menus that do not apply to your Add-In. For example:
		/// if( PropertyName == "HiddenMenus" )
		/// return mgBuildProject + mgRun;
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageGuid">The GUID identifying the Enterprise Architect package sub-tree that is controlled by the Add-In.</param>
		/// <param name="PropertyName">The name of the property that is used by Enterprise Architect. See Details for the possible values.</param>
		/// <returns>see summary above</returns>
		public override object MDG_GetProperty(EA.Repository Repository,string PackageGuid,string PropertyName)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, PackageGuid, PropertyName});
		}
		
		/// <summary>
		/// MDG_Merge enables the Add-In to jointly handle changes to both the model branch and the code project that the model branch is connected to. This event should be called whenever the user has asked to merge their model branch with its connected code project, or whenever the user has established a new connection to a code project. The purpose of this event is to enable the Add-In to interact with the user to perform a merge between the model branch and the connected project.
		/// Also look at MDG_Connect, MDG_PreMerge and MDG_PostMerge.
		/// Merge
		/// A merge consists of three major operations:
		/// - Export: Where newly created model objects are exported into code and made available to the code project.
		/// - Import: Where newly created code objects, Classes and such things are imported into the model.
		/// - Synchronize: Where objects available both to the model and in code are jointly updated to reflect changes made in either the model, code project or both.
		/// - Synchronize Type
		/// The Synchronize operation can take place in one of four different ways. Each of these ways corresponds to a value returned by SynchType:
		/// - None: (SynchType = 0) No synchronization is to be performed
		/// - Forward: (SynchType = 1) Forward synchronization, between the model branch and the code project is to occur
		/// - Reverse: (SynchType = 2) Reverse synchronization, between the code project and the model branch is to occur
		/// - Both: (SynchType = 3) Reverse, then Forward synchronization's are to occur.
		/// Object ID Format
		/// Each of the Object IDs listed in the string arrays described above should be composed in the following format:
		/// (@namespace)*(#class)*($attribute|%operation|:property)*
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageGuid">The GUID identifying the Enterprise Architect package sub-tree that is controlled by the Add-In.</param>
		/// <param name="SynchObjects">A string array containing a list of objects (Object ID format) to be jointly synchronized between the model branch and the project. 
		/// See summary for the format of the Object IDs.</param>
		/// <param name="SynchType">The value determining the user-selected type of synchronization to take place. 
		/// See summary for a list of valid values.</param>
		/// <param name="ExportObjects">The string array containing the list of new model objects (in Object ID format) to be exported by Enterprise Architect to the code project.</param>
		/// <param name="ExportFiles">A string array containing the list of files for each model object chosen for export by the Add-In. 
		/// Each entry in this array must have a corresponding entry in the ExportObjects parameter at the same array index, 
		/// so ExportFiles(2) must contain the filename of the object by ExportObjects(2).</param>
		/// <param name="ImportFiles">A string array containing the list of code files made available to the code project to be newly imported to the model. 
		/// Enterprise Architect imports each file listed in this array for import into the connected model branch.</param>
		/// <param name="IgnoreLocked">A value indicating whether to ignore any files locked by the code project (that is, "TRUE" or "FALSE".</param>
		/// <param name="Language">The string value containing the name of the code language supported by the code project connected to the model branch.</param>
		/// <returns>Return a non-zero if the merge operation completed successfully and a zero value when the operation has been unsuccessful.</returns>
		public override long MDG_Merge(EA.Repository Repository,string PackageGuid ,ref object SynchObjects,
		                              ref string SynchType,ref object ExportObjects ,ref object ExportFiles , 
		                              ref object ImportFiles ,ref string IgnoreLocked ,ref string Language )
		{
			object[] refParams = new object[]{ Repository, PackageGuid , SynchObjects,
			                                      	SynchType, ExportObjects, ExportFiles, ImportFiles ,IgnoreLocked ,Language};
			long returnValue = this.callMethods(MethodBase.GetCurrentMethod().Name,refParams,0L);
	    	//get the ref parameter values
    	    SynchObjects = refParams[2];
    	    SynchType = (string) refParams[3];
    	    ExportObjects = refParams[4];
    	    ExportFiles = refParams[5];
    	    ImportFiles = refParams[6];   
			IgnoreLocked = (string) refParams[7];
			Language = (string) refParams[8];
        	//return 
			return returnValue;
		}
		
		/// <summary>
		/// MDG_NewClass enables the Add-In to alter details of a Class before it is created.
		/// This method is called when Enterprise Architect generates a new Class, and requires information relating to assigning the language and file path. The file path should be passed back as a return value and the language should be passed back via the language parameter.
		/// Also look at MDG_PreGenerate
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageGuid">The GUID identifying the Enterprise Architect package sub-tree that is controlled by the Add-In.</param>
		/// <param name="CodeID">A string used to identify the code element before it is created, for more information see MDG_View.</param>
		/// <param name="Language">A string used to identify the programming language for the new Class. The language must be supported by Enterprise Architect.</param>
		/// <returns>Returns a string containing the file path that should be assigned to the Class.</returns>
		public override string MDG_NewClass(EA.Repository Repository,string PackageGuid,string CodeID,ref string Language )
		{
			object[] refParams = new object[]{ Repository, PackageGuid, CodeID, Language};
			string returnValue = this.callMethods(MethodBase.GetCurrentMethod().Name,refParams,string.Empty);
        	//get the ref parameter values
        	Language = (string) refParams[3]; 	
        	//return
			return returnValue;
		}
		
		/// <summary>
		/// MDG_PostGenerate enables the Add-In to handle file changes caused by generation.
		/// This event is called after Enterprise Architect has prepared text to replace the existing contents of a file. Responding to this event enables the Add-In to write to the linked application's user interface rather than modify the file directly.
		/// When the contents of a file are changed, Enterprise Architect passes FileContents as a non-empty string. New files created as a result of code generation are also sent through this mechanism, enabling Add-Ins to add new files to the linked project's file list.
		/// When new files are created Enterprise Architect passes FileContents as an empty string. When a non-zero is returned by this function, the Add-In has successfully written the contents of the file. A zero value for the return indicates to Enterprise Architect that the file must be saved.
		/// Also look at MDG_PreGenerate.
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageGuid">The GUID identifying the Enterprise Architect package sub-tree that is controlled by the Add-In.</param>
		/// <param name="FilePath">The path of the file Enterprise Architect intends to overwrite.</param>
		/// <param name="FileContents">A string containing the proposed contents of the file.</param>
		/// <returns>Return value depends on the type of event that this function is responding to (see summary). 
		/// This function is required to handle two separate and distinct cases.</returns>
		public override long MDG_PostGenerate(EA.Repository Repository,string PackageGuid,string FilePath,string FileContents )
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, PackageGuid, FilePath, FileContents},0L);
		}
		
		/// <summary>
		/// MDG_PostMerge is called after a merge process has been completed.
		/// This function is called by Enterprise Architect after the merge process has been completed.
		/// Note:
		/// File save checking should not be performed with this function, but should be handled by MDG_PreGenerate, MDG_PostGenerate and MDG_PreReverse.
		/// Also look at MDG_PreMerge and MDG_Merge.
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageGuid">The GUID identifying the Enterprise Architect package sub-tree that is controlled by the Add-In.</param>
		/// <returns>Return a zero value if the post-merge process has failed, a non-zero return indicates that the post-merge has been successful. Enterprise Architect assumes a non-zero return if this method is not implemented</returns>
		public override long MDG_PostMerge(EA.Repository Repository,string PackageGuid)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, PackageGuid},1L);
		}
		
		/// <summary>
		/// MDG_PreGenerate enables the Add-In to deal with unsaved changes. 
		/// This function is called immediately before Enterprise Architect attempts to generate files from the model. 
		/// A possible use of this function would be to prompt the user to save unsaved source files.
		/// Also look at MDG_PostGenerate.
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageGuid">The GUID identifying the Enterprise Architect package sub-tree that is controlled by the Add-In.</param>
		/// <returns>Return a zero value to abort generation. Any other value enables the generation to continue.</returns>
		public override long MDG_PreGenerate(EA.Repository Repository,string PackageGuid)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, PackageGuid},1L);
		}
		
		/// <summary>
		/// MDG_PreMerge is called after a merge process has been initiated by the user and before Enterprise Architect performs the merge process.
		/// This event is called after a user has performed their interactions with the merge screen and has confirmed the merge with the OK button, but before Enterprise Architect performs the merge process using the data provided by the MDG_Merge call, before any changes have been made to the model or the connected project.
		/// This event is made available to provide the Add-In with the opportunity to generally set internal Add-In flags to augment the MDG_PreGenerate, MDG_PostGenerate and MDG_PreReverse events.
		/// Note:
		/// File save checking should not be performed with this function, but should be handled by MDG_PreGenerate, MDG_PostGenerate and MDG_PreReverse.
		/// Also look at MDG_Merge and MDG_PostMerge.
		/// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageGuid">The GUID identifying the Enterprise Architect package sub-tree that is controlled by the Add-In.</param>
		/// <returns>A return value of zero indicates that the merge process will not occur. If the value is not zero the merge process will proceed. If this method is not implemented then it is assumed that a merge process is used.</returns>
		public override long MDG_PreMerge(EA.Repository Repository, string PackageGuid)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, PackageGuid},1L);
		}
		
		/// <summary>
		/// MDG_PreReverse enables the Add-In to save file changes before being imported into Enterprise Architect.
		/// This function operates on a list of files that are about to be reverse-engineered into Enterprise Architect. If the user is working on unsaved versions of these files in an editor, you could either prompt the user or save automatically.
		/// Also look at MDG_PostGenerate and MDG_PreGenerate.
		/// </summary>
 		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageGuid">The GUID identifying the Enterprise Architect package sub-tree that is controlled by the Add-In.</param>
		/// <param name="FilePaths">A string array of filepaths pointed to the files that are to be reverse engineered.</param>
		public override void MDG_PreReverse(EA.Repository Repository, string PackageGuid,object FilePaths)
		{
			this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, PackageGuid,FilePaths});
		}
		
		/// <summary>
		/// MDG_RunExe enables the Add-In to run the target application. This function is called when the user selects the Add-Ins | Run Exe menu option. Respond to this event by launching the compiled application.
		/// Also look at MDG_BuildProject.
		/// </summary>
 		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageGuid">The GUID identifying the Enterprise Architect package sub-tree that is controlled by the Add-In.</param>
		public override void MDG_RunExe(EA.Repository Repository, string PackageGuid)
		{
			this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, PackageGuid});
		}
		
		/// <summary>
		/// MDG_View enables the Add-In to display user specified code elements. This function is called by Enterprise Architect when the user asks to view a particular code element. This enables the Add-In to present that element in its own way, usually in a code editor.
		/// </summary>
 		/// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="PackageGuid">The GUID identifying the Enterprise Architect package sub-tree that is controlled by the Add-In.</param>
		/// <param name="CodeID">Identifies the code element in the following format:
		/// [type]ElementPart[type]ElementPart...
		/// where each element is proceeded with a token identifying its type:
		/// @ -namespace
		/// # - Class
		/// $ - attribute
		/// % - operation
		/// For example if a user has selected the m_Name attribute of Class1 located in namespace Name1, the class ID would be passed through in the following format:
		/// @Name1#Class1%m_Name</param>
		/// <returns>Return a non-zero value to indicate that the Add-In has processed the request. Returning a zero value results in Enterprise Architect employing the standard viewing process which is to launch the associated source file.</returns>
		public override long MDG_View(EA.Repository Repository, string PackageGuid, string CodeID)
		{
			return this.callMethods(MethodBase.GetCurrentMethod().Name,new object[]{ Repository, PackageGuid,CodeID},0L);
		}
	
		#endregion EA MDG Events
		
		#endregion
		
	}
}