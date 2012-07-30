/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 26/07/2012
 * Time: 5:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using System.Configuration;
using System.Linq;


namespace TSF.UmlToolingFramework.EANavigator
{
	/// <summary>
	/// Description of NavigatorSettings.
	/// </summary>
	public class NavigatorSettings
	{
		protected Configuration defaultConfig {get;set;}
		protected Configuration currentConfig {get;set;}
		public NavigatorSettings()
		{
		  Configuration roamingConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
		  // Map the roaming configuration file. This
		  // enables the application to access 
		  // the configuration file using the
		  // System.Configuration.Configuration class
		  ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
		  configFileMap.ExeConfigFilename = roamingConfig.FilePath;		
		  // Get the mapped configuration file.
		   currentConfig = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
		  //merge the default settings
		  this.mergeDefaultSettings();
		}
		
		/// <summary>
		/// gets the default settings config.
		/// </summary>
		protected void getDefaultSettings()
		{
			string defaultConfigFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			defaultConfig = ConfigurationManager.OpenExeConfiguration(defaultConfigFilePath);
		}
		/// <summary>
		/// merge the default settings with the current config.
		/// </summary>
		protected void mergeDefaultSettings()
		{
			if (this.defaultConfig == null)
			{
				this.getDefaultSettings();
			}
			//defaultConfig.AppSettings.Settings["menuOwnerEnabled"].Value
			foreach ( KeyValueConfigurationElement configEntry in defaultConfig.AppSettings.Settings) 
			{
				if (!currentConfig.AppSettings.Settings.AllKeys.Contains(configEntry.Key))
				{
					currentConfig.AppSettings.Settings.Add(configEntry.Key,configEntry.Value);
				}
			}
			// save the configuration
			currentConfig.Save();
		}
		public bool isOptionEnabled(UML.UMLItem parentElement,string option)
		{
			//default
			return true;
		}
		/// <summary>
		/// returns true when the selecting an element in the project browser is the default action on doubleclick.
		/// </summary>
		/// <returns>true when the selecting an element in the project browser is the default action on doubleclick.</returns>
		public bool isProjectBrowserDefaultAction()
		{
			return (this.currentConfig.AppSettings.Settings["defaultAction"].Value == "ProjectBrowser");
		}
	}
}
