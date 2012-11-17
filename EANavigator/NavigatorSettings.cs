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
		  
		  //the roamingConfig now get a path such as C:\Users\<user>\AppData\Roaming\Sparx_Systems_Pty_Ltd\DefaultDomain_Path_2epjiwj3etsq5yyljkyqqi2yc4elkrkf\9,_2,_0,_921\user.config
		  // which I don't like. So we move up three directories and then add a directory for the EA Navigator so that we get
		  // C:\Users\<user>\AppData\Roaming\GeertBellekens\EANavigator\user.config
		  string configFileName =  System.IO.Path.GetFileName(roamingConfig.FilePath);
		  string configDirectory = System.IO.Directory.GetParent(roamingConfig.FilePath).Parent.Parent.Parent.FullName;
		  
		  string newConfigFilePath = configDirectory + @"\Geert Bellekens\EANavigator\" + configFileName;
		  // Map the roaming configuration file. This
		  // enables the application to access 
		  // the configuration file using the
		  // System.Configuration.Configuration class
		  ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
		  configFileMap.ExeConfigFilename = newConfigFilePath;		
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
		public bool projectBrowserDefaultAction
		{
			get
			{
				return (this.currentConfig.AppSettings.Settings["defaultAction"].Value == "ProjectBrowser");
			}
			set
			{
				if (value)
				{
					this.currentConfig.AppSettings.Settings["defaultAction"].Value = "ProjectBrowser";
				}
				else
				{
					this.currentConfig.AppSettings.Settings["defaultAction"].Value = "Properties";
				}
			}
		}
		public bool toolbarVisible
		{
			get
			{
				bool result;
				if( bool.TryParse(this.currentConfig.AppSettings.Settings["toolbarVisible"].Value, out result))
			   	{
			   		return result;
			   	}
			    else 
			  	{
			   		return true;
			   	}
			}
			set
			{
				this.currentConfig.AppSettings.Settings["toolbarVisible"].Value = value.ToString();
			}
		}
		public bool contextmenuVisible
		{
			get
			{
				bool result;
				if( bool.TryParse(this.currentConfig.AppSettings.Settings["contextMenu"].Value, out result))
			   	{
			   		return result;
			   	}
			    else 
			  	{
			   		return true;
			   	}
			}
			set
			{
				this.currentConfig.AppSettings.Settings["contextMenu"].Value = value.ToString();
			}
		}
		public bool trackSelectedElement
		{
			get
			{
				bool result;
				if( bool.TryParse(this.currentConfig.AppSettings.Settings["trackSelectedElement"].Value, out result))
			   	{
			   		return result;
			   	}
			    else 
			  	{
			   		return true;
			   	}
			}
			set
			{
				this.currentConfig.AppSettings.Settings["trackSelectedElement"].Value = value.ToString();
			}
		}
		/// <summary>
		/// saves the settings to the config file
		/// </summary>
		public void save()
		{
			this.currentConfig.Save();
		}
		
		public void refresh()
		{
		  ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
		  configFileMap.ExeConfigFilename = currentConfig.FilePath;		
		  // Get the mapped configuration file.
		   currentConfig = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
		}
	}
}
