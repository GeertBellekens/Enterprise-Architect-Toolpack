using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Xml;
using UML=TSF.UmlToolingFramework.UML;
using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using EAAddinFramework.Utilities;


namespace EAImvertor
{
	/// <summary>
	/// Description of NavigatorSettings.
	/// </summary>
	public class EAImvertorSettings : EAAddinFramework.Utilities.AddinSettings
	{
		protected override string addinName => "EAImvertor";
		public EAImvertorSettings(TSF_EA.Model model)
		{
			this.model = model;
		}
			
		private List<string> _availableProcesses;
		public List<string> availableProcesses
		{
			get
			{
				if (_availableProcesses == null)
				{
					this.setAvailableConfigs();
				}
				return _availableProcesses;
			}
		}
		private List<string> _availableProperties;
		public List<string> availableProperties
		{
			get
			{
				if (_availableProperties == null)
				{
					this.setAvailableConfigs();
				}
				return _availableProperties;
			}
		}
		
		#region implemented abstract members of AddinSettings

		protected override string configSubPath
		{
			get 
			{
				return @"\Bellekens\EAImvertor\";
			}
		}
		protected override string defaultConfigAssemblyFilePath 
		{
			get 
			{
				return System.Reflection.Assembly.GetExecutingAssembly().Location;
			}
		}
		#endregion
		private void setAvailableConfigs()
		{
			this._availableProcesses = new List<string>();
			this._availableProperties = new List<string>();
			var xmlConfigs = this.getConfigXml();
			if (xmlConfigs != null)
			{
				XmlNodeList releaseNodes = xmlConfigs.SelectNodes("//release/name");
				foreach (XmlNode releaseNode in releaseNodes) 
				{
					_availableProcesses.Add(releaseNode.InnerText);
				}
				XmlNodeList runtypeNodes = xmlConfigs.SelectNodes("//runtype/name");
				foreach (XmlNode runtypeNode in runtypeNodes) 
				{
					_availableProperties.Add(runtypeNode.InnerText);
				}
			}
		}
		internal void resetConfigs()
		{
			this.setAvailableConfigs();
			this.defaultProcessName = this.availableProcesses.FirstOrDefault();
			this.defaultProperties = this.availableProperties.FirstOrDefault();
			
		}
		private XmlDocument getConfigXml()
		{
			try
			{
				using (var client = this.getHttpClient())
				{
					string configURL = this.imvertorURL + this.urlPostFix +"config?pin=" + this.defaultPIN;
					var response = client.GetAsync(configURL).Result;
					if (!response.IsSuccessStatusCode)
			        {
			            return null;
			        }
			        StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result);
					string responseText = reader.ReadToEnd();
					XmlDocument xmlResponse = new XmlDocument();
					xmlResponse.LoadXml(responseText);
					return xmlResponse;
				}
			}
			catch(Exception e)
			{
				Logger.logError(string.Format("Could not get configs because of error: {0} Stacktrace: {1}", e.Message, e.StackTrace));
				return null;
			}
		}
		public EAImvertorSettings Clone()
		{
			return this.MemberwiseClone() as EAImvertorSettings;
		}
		
		/// <summary>
		/// the URL for the Imvertor Service
		/// </summary>
		public string imvertorURL
		{
			get
			{
				return this.getValue("imvertorURL");
			}
			set
			{	string url = value;
				if (!url.EndsWith("/")) url += "/";
				this.setValue("imvertorURL",url);
			}
		}
		/// <summary>
		/// the default pincode
		/// </summary>
		public string defaultPIN
		{
			get
			{
				return this.getValue("defaultPIN");
			}
			set
			{
				if (value != defaultPIN)
				{
					this.setValue("defaultPIN",value);
					resetConfigs();
				}else
				{
					this.setValue("defaultPIN",value);
				}
			}
		}
		public string defaultProcessName
		{
			get
			{
				return this.getValue("defaultProcessName");
			}
			set
			{
				this.setValue("defaultProcessName",value);
			}
		}
		public string defaultProperties
		{
			get
			{
				return this.getValue("defaultProperties");
			}
			set
			{
				this.setValue("defaultProperties",value);
			}
		}
		public string defaultPropertiesFilePath
		{
			get
			{
				return this.getValue("defaultPropertiesFilePath");
			}
			set
			{
				this.setValue("defaultPropertiesFilePath",value);
			}
		}
		public string defaultHistoryFilePath
		{
			get
			{
				return this.getValue("defaultHistoryFilePath");
			}
			set
			{
				this.setValue("defaultHistoryFilePath",value);
			}
		}
		public List<string> imvertorStereotypes
		{
			get
			{
				return this.getListValue("imvertorStereotypes");
			}
			set
			{
				this.setListValue("imvertorStereotypes",value);
			}
		}
		public int timeOutInSeconds
		{
			get
			{
				return this.getIntValue("timeOutInSeconds");
			}
			set
			{
				this.setIntValue("timeOutInSeconds",value);
			}
		}
		public int retryInterval
		{
			get
			{
				return this.getIntValue("retryInterval");
			}
			set
			{
				this.setIntValue("retryInterval",value);
			}
		}
		public string urlPostFix
		{
			get
			{
				return  this.getValue("urlPostFix");
			}
			set
			{
				this.setValue("urlPostFix",value);
			}
		}
		public string proxy
		{
			get
			{
				return  this.getValue("proxy");
			}
			set
			{
				if (value != proxy)
				{
					this.setValue("proxy",value);
					resetConfigs();
				}else
				{
					this.setValue("proxy",value);
				}
			}
		}
		public string resultsPath
		{
			get
			{
				string newValue = this.getValue("resultsPath");
				if (newValue.ToLower().Contains("%appdata%"))
				{
					newValue = newValue.ToLower().Replace("%appdata%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				}
				return newValue;
			}
			set
			{
				string newValue = value;
				if (value.ToLower().Contains("%appdata%"))
				{
					newValue = value.ToLower().Replace("%appdata%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				}
				 this.setValue("resultsPath",newValue);
			}
		}
		public bool includeDiagrams
		{
			get
			{
				return this.getBooleanValue("includeDiagrams");
				
			}
			set
			{
				 this.setBooleanValue("includeDiagrams",value);
			}
		}
		public HttpClient getHttpClient()
		{
			if (! string.IsNullOrEmpty(this.proxy))
		    {
				//in case of a proxy we need to add the proxy to the httpclient
				var httpClientHandler = new HttpClientHandler() ;
				httpClientHandler. Proxy = new WebProxy(this.proxy, false);
				httpClientHandler.UseProxy = true;
				return new HttpClient(httpClientHandler);
		    }
			else
			{
				return new HttpClient();
			}
			
		}
	    
	}
}
