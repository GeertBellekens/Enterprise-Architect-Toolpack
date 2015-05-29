/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 25/03/2015
 * Time: 4:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace EAAddinManager
{
	/// <summary>
	/// Description of EAAddinManagerConfig.
	/// </summary>
	public class EAAddinManagerConfig
	{
		protected Configuration defaultConfig {get;set;}
		protected Configuration currentConfig {get;set;}
		private List<string> _addinSearchPaths;
		private List<AddinConfig> _addinConfigs;
		public EAAddinManagerConfig()
		{
		  Configuration roamingConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
		  
		  //the roamingConfig now get a path such as C:\Users\<user>\AppData\Roaming\Sparx_Systems_Pty_Ltd\DefaultDomain_Path_2epjiwj3etsq5yyljkyqqi2yc4elkrkf\9,_2,_0,_921\user.config
		  // which I don't like. So we move up three directories and then add a directory for the EA Navigator so that we get
		  // C:\Users\<user>\AppData\Roaming\GeertBellekens\EANavigator\user.config
		  string configFileName =  System.IO.Path.GetFileName(roamingConfig.FilePath);
		  string configDirectory = System.IO.Directory.GetParent(roamingConfig.FilePath).Parent.Parent.Parent.FullName;
		  
		  string newConfigFilePath = configDirectory + @"\Bellekens\EAAddinManager\" + configFileName;
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
		
		private string _localAddinPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) , @"Bellekens\EAAddinManager\Addins\");
		public string localAddinPath {get {return this._localAddinPath;}}
		
		public string getLocalAddinPath (AddinConfig addinConfig)
		{
			return localAddinPath +  addinConfig.name + "\\" + addinConfig.dllPath;
		}
		public string getRemoteAddinPath (AddinConfig addinConfig, string remotePath)
		{
			return remotePath +  addinConfig.name + "\\" + addinConfig.dllPath;
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
		public List<string> addinSearchPaths
		{
			get
			{
				if (this._addinSearchPaths == null)
				{
					this.getAddinSearchPaths();
				}
				return this._addinSearchPaths;
			}
			set
			{
				this._addinSearchPaths = value;
			}
		}
		public List<AddinConfig> addinConfigs {
			get
			{
				if (this._addinConfigs == null)
				{
					this.getAddinConfigs();
				}
				return this._addinConfigs;
			}
			set
			{
				this._addinConfigs = value;
			}
		}
		public void save()
		{
			this.setAddinConfigs();
			this.setAddinSearchPaths();
			
			this.currentConfig.Save();
		}
		private void getAddinConfigs()
		{
			this._addinConfigs = new List<AddinConfig>();
			foreach (ConnectionStringSettings  connectionString  in this.currentConfig.ConnectionStrings.ConnectionStrings) 
			{
				if (connectionString.ProviderName == "EA Addin Manager")
				{
					this._addinConfigs.Add(new AddinConfig(connectionString));
				}
			}
		}
		private void setAddinConfigs()
		{
			//first remove all connectionstrings for EA Addin Manager
			foreach (ConnectionStringSettings  connectionString  in this.currentConfig.ConnectionStrings.ConnectionStrings) 
			{
				if (connectionString.ProviderName == "EA Addin Manager")
				{
					this.currentConfig.ConnectionStrings.ConnectionStrings.Remove(connectionString);
				}
			}
			//then add them 
			foreach (AddinConfig addinConfig in this._addinConfigs) 
			{
				currentConfig.ConnectionStrings.ConnectionStrings.Add(addinConfig.getConnectionString());
			}
		}
		private void setAddinSearchPaths()
		{
			this.currentConfig.AppSettings.Settings["AddinSearchPaths"].Value = string.Join(";", this.addinSearchPaths);
		}
		private void getAddinSearchPaths()
		{
				this._addinSearchPaths = new List<string>();
				string configValue = this.currentConfig.AppSettings.Settings["AddinSearchPaths"].Value;
				this._addinSearchPaths.AddRange(configValue.Split(';'));
		}
		
	

		
	}
}
