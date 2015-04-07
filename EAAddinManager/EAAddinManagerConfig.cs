/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 25/03/2015
 * Time: 4:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;

namespace EAAddinManager
{
	/// <summary>
	/// Description of EAAddinManagerConfig.
	/// </summary>
	public class EAAddinManagerConfig
	{
		protected Configuration defaultConfig {get;set;}
		protected Configuration currentConfig {get;set;}
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
		public AddinConfigs addinConfigs
		{
			get
			{
				return this.addinConfigSection.addinConfigs;
			}
		}
		private AddinConfigSection addinConfigSection
		{
			get
			{
				AddinConfigSection section = (AddinConfigSection)this.currentConfig.GetSection("AddinConfigSection");
				if (section == null)
				{
					section = new AddinConfigSection();
				}
				return section;
				//return (AddinConfigSection)this.currentConfig.GetSection("addinConfigSection") ?? new AddinConfigSection();
			}
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
		public List<string> addinFileLocations
		{
			get
			{
				List<string> addinLocations = new List<string>();
				string configValue = this.currentConfig.AppSettings.Settings["AddinDllLocation"].Value;
				addinLocations.AddRange(configValue.Split(';'));
			   	return addinLocations;
			}
			set
			{
				this.currentConfig.AppSettings.Settings["AddinDllLocation"].Value = string.Join(";", value);
			}
		}
		
	}
		public class AddinConfig :ConfigurationElement
		{
	        [ConfigurationProperty("name", IsRequired = true)]
	        public string name
	        {
	            get
	            {
	                return this["name"] as string;
	            }
	            set
	            {
	            	this["name"] = value;
	            }
	        }
	        [ConfigurationProperty("version", IsRequired = true)]
	        public string version
	        {
	            get
	            {
	                return this["version"] as string;
	            }
	           	set
	            {
	            	this["version"] = value;
	            }
	        }
	        [ConfigurationProperty("load", IsRequired = true, DefaultValue = true)]
	        public bool load
	        {
	            get
	            {
	            	return (bool)this["load"];
	            }
	           	set
	            {
	            	this["load"] = value;
	            }
	        }
	        [ConfigurationProperty("dllPath", IsRequired = true)]
	        public string dllPath
	        {
	            get
	            {
	                return this["dllPath"] as string;
	            }
	           	set
	            {
	            	this["dllPath"] = value;
	            }
	        }
		}
		
		public class AddinConfigs : ConfigurationElementCollection
	    {
	        public AddinConfig this[int index]
	        {
	            get
	            {
	                return base.BaseGet(index) as AddinConfig ;
	            }
	            set
	            {
	                if (base.BaseGet(index) != null)
	                {
	                    base.BaseRemoveAt(index);
	                }
	                this.BaseAdd(index, value);
	            }
	        }
	
	       public new AddinConfig this[string responseString]
	       {
	            get { return (AddinConfig) BaseGet(responseString); }
	            set
	            {
	                if(BaseGet(responseString) != null)
	                {
	                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
	                }
	                BaseAdd(value);
	            }
	        }
	
	        protected override ConfigurationElement CreateNewElement()
	        {
	            return new AddinConfig();
	        }
	
	        protected override object GetElementKey(ConfigurationElement element)
	        {
	            return ((AddinConfig)element).name;
	        }
	    }
		
		public class AddinConfigSection: ConfigurationSection
	    {
	
	        public static AddinConfigSection GetConfig()
	        {
	            return (AddinConfigSection)ConfigurationManager.GetSection("AddinConfigSection") ?? new AddinConfigSection();
	        }
	
	        [ConfigurationProperty("addinConfigs")]
	        [ConfigurationCollection(typeof(AddinConfigs), AddItemName = "addinConfig")]
	        public AddinConfigs addinConfigs
	        {
	            get
	            {
	            	return (AddinConfigs)this["addinConfigs"];
	            }
	        }
		
	    }
}
