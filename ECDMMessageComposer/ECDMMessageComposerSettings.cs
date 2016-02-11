using System;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace ECDMMessageComposer
{
	/// <summary>
	/// Description of ECDMMessageComposerSettings.
	/// </summary>
	public class ECDMMessageComposerSettings
	{

		protected Configuration defaultConfig {get;set;}
		protected Configuration currentConfig {get;set;}
		public ECDMMessageComposerSettings()
		{
		  Configuration roamingConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
		  
		  //the roamingConfig now get a path such as C:\Users\<user>\AppData\Roaming\Sparx_Systems_Pty_Ltd\DefaultDomain_Path_2epjiwj3etsq5yyljkyqqi2yc4elkrkf\9,_2,_0,_921\user.config
		  // which I don't like. So we move up three directories and then add a directory for the ECDM MessageComposer so that we get
		  // C:\Users\<user>\AppData\Roaming\Bellekens\ECDMMessageComposer\user.config
		  string configFileName =  System.IO.Path.GetFileName(roamingConfig.FilePath);
		  string configDirectory = System.IO.Directory.GetParent(roamingConfig.FilePath).Parent.Parent.Parent.FullName;
		  
		  string newConfigFilePath = configDirectory + @"\Bellekens\ECDMMessageComposer\" + configFileName;
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
		
		/// <summary>
		/// list of tagged value names to ignore when updating tagged values
		/// </summary>
		public List<string> ignoredTaggedValues
		{
			get
			{
				return this.currentConfig.AppSettings.Settings["ignoredTaggedValues"].Value.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries).ToList<string>();
			}
			set
			{
				this.currentConfig.AppSettings.Settings["ignoredTaggedValues"].Value = string.Join(",",value);
			}
		}
		/// <summary>
		/// list of stereotypes of elements to ignore when updating a subset model
		/// </summary>
		public List<string> ignoredStereotypes
		{
			get
			{
				return this.currentConfig.AppSettings.Settings["ignoredStereotypes"].Value.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries).ToList<string>();
			}
			set
			{
				this.currentConfig.AppSettings.Settings["ignoredStereotypes"].Value = string.Join(",",value);
			}
		}
		/// <summary>
		/// indicates if the data types should be added to the diagram
		/// </summary>
		public bool addDataTypes
		{
			get
			{
				bool result;
				return bool.TryParse(this.currentConfig.AppSettings.Settings["addDataTypes"].Value, out result) ? result : true;
			}
			set
			{
				this.currentConfig.AppSettings.Settings["addDataTypes"].Value = value.ToString();
			}
		}
		/// <summary>
		/// indicates if the data types should be added to the diagram
		/// </summary>
		public bool addSourceElements
		{
			get
			{
				bool result;
				return bool.TryParse(this.currentConfig.AppSettings.Settings["addSourceElements"].Value, out result) ? result : true;
			}
			set
			{
				this.currentConfig.AppSettings.Settings["addSourceElements"].Value = value.ToString();
			}
		}
		

	}
}





