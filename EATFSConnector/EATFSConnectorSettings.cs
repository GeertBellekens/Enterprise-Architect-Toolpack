using System;
using UML=TSF.UmlToolingFramework.UML;
using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using EAAddinFramework.WorkTracking.TFS;

namespace EATFSConnector
{
	/// <summary>
	/// Description of ECDMMessageComposerSettings.
	/// </summary>
	public class EATFSConnectorSettings:EAAddinFramework.Utilities.AddinSettings,TFSSettings
	{
		#region implemented abstract members of AddinSettings

		protected override string configSubPath
		{
			get 
			{
				return @"\Bellekens\EATFSConnector\";
			}
		}
		protected override string defaultConfigFilePath 
		{
			get 
			{
				return System.Reflection.Assembly.GetExecutingAssembly().Location;
			}
		}
		#endregion
		public string getTFSUrl(TSF_EA.Model model)
		{
			string TFSUrl;
        	//get the TFS location
        	projectConnections.TryGetValue(model.projectGUID,out TFSUrl);
        	return TFSUrl;
		}
		
        public Dictionary<string,string> projectConnections
		{
			get
			{	
				var returnedConnections = new Dictionary<string,string>();
				foreach (var projectConnection in this.getValue("projectConnections").Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries)) 
				{
					var connectionParameters = projectConnection.Split(new char[]{';'},StringSplitOptions.RemoveEmptyEntries);
					if (connectionParameters.Count() == 2)
					{
						returnedConnections.Add(connectionParameters[0],connectionParameters[1]);
					}
				}
				return returnedConnections;
			}
			set
			{
				var connections = new List<string>();
				foreach (var keyValuePair in value) 
				{
					connections.Add(string.Join(";",keyValuePair.Key,keyValuePair.Value));
				}
				this.setValue("projectConnections",string.Join(",",connections));
			}
		}
        public Dictionary<string,string> workitemMappings
		{
			get
			{	
				var returnedWorkItemMappings = new Dictionary<string,string>();
				foreach (var mappings in this.getValue("workitemMappings").Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries)) 
				{
					var mapping = mappings.Split(new char[]{';'},StringSplitOptions.RemoveEmptyEntries);
					if (mapping.Count() == 2)
					{
						returnedWorkItemMappings.Add(mapping[0],mapping[1]);
					}
				}
				return returnedWorkItemMappings;
			}
			set
			{
				var connections = new List<string>();
				foreach (var keyValuePair in value) 
				{
					connections.Add(string.Join(";",keyValuePair.Key,keyValuePair.Value));
				}
				this.setValue("workitemMappings",string.Join(",",connections));
			}
		}
        public string defaultProject
		{
			get
			{
				return this.getValue("defaultProject");
			}
			set
			{
				this.setValue("defaultProject",value);
			}
		}
        public string defaultUserName
		{
			get
			{
				return this.getValue("defaultUserName");
			}
			set
			{
				this.setValue("defaultUserName",value);
			}
		}
        public string defaultPassword {get;set;} //do not persist default password
        
		public List<string> mappedElementTypes 
		{
			get 
			{
				return this.workitemMappings.Values.Where( x => !x.Contains("::")).ToList();
			}
		}
		public List<string> mappedStereotypes 
		{
			get 
			{
				var stereotypes = new List<string>();
				foreach (var fullStereotype in this.workitemMappings.Values.Where(x => x.Contains("::")))
				{
					string simpleStereotype = fullStereotype.Split(new string[]{"::"},StringSplitOptions.None)[1];
					if (simpleStereotype.Length > 0) stereotypes.Add(simpleStereotype);
				} 
				return stereotypes;
			}
		}
    }
}





