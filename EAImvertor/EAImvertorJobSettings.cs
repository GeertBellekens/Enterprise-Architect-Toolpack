
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace EAImvertor
{
	/// <summary>
	/// Description of EAImvertorJobSettings.
	/// </summary>
	public class EAImvertorJobSettings
	{
		private EAImvertorSettings _settings;
		public EAImvertorJobSettings(EAImvertorSettings settings)
		{
			this._settings = settings;
			this.availableProcesses = settings.availableProcesses;
			this.availableProperties = settings.availableProperties;
			this.imvertorURL = settings.imvertorURL;
			this.PIN = settings.defaultPIN;
			this.ProcessName = settings.defaultProcessName;
			this.Properties = settings.defaultProperties;
			this.PropertiesFilePath = settings.defaultPropertiesFilePath;
			this.HistoryFilePath = settings.defaultHistoryFilePath;
			this.imvertorStereotypes = settings.imvertorStereotypes;
			this.timeOutInSeconds = settings.timeOutInSeconds;
			this.retryInterval = settings.retryInterval;
			this.urlPostFix = settings.urlPostFix;
			this.resultsPath = settings.resultsPath;
			this.includeDiagrams = settings.includeDiagrams;
		}
		public List<string> availableProcesses {get;set;}

		public List<string> availableProperties{get;set;}

		/// <summary>
		/// the URL for the Imvertor Service
		/// </summary>
		public string imvertorURL {get;set;}

		/// <summary>
		/// the pincode
		/// </summary>
		public string PIN {get;set;}

		public string ProcessName {get;set;}

		public string Properties {get;set;}

		public string PropertiesFilePath {get;set;}

		public string HistoryFilePath {get;set;}

		public List<string> imvertorStereotypes {get;set;}

		public int timeOutInSeconds {get;set;}

		public int retryInterval {get;set;}
		public string urlPostFix {get;set;}
		public string resultsPath {get;set;} 	
		public string proxy {get;set;}
		public bool includeDiagrams {get;set;}
		public HttpClient getHttpClient()
		{
			return _settings.getHttpClient();
		}
		

	}
}
