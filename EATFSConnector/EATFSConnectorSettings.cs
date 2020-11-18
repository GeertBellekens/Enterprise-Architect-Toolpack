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

		protected override string addinName => "EATFSConnector";
		protected override string configSubPath
		{
			get 
			{
				return @"\Bellekens\EATFSConnector\";
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
				return getDictionaryValue("projectConnections");
			}
			set
			{
				this.setDictionaryValue("projectConnections",value);
			}
		}
        public Dictionary<string,string> workitemMappings
		{
			get
			{	
				return getDictionaryValue("workitemMappings");
			}
			set
			{
				this.setDictionaryValue("workitemMappings",value);
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
        public string defaultWorkitemType
		{
			get
			{
				return this.getValue("defaultWorkitemType");
			}
			set
			{
				this.setValue("defaultWorkitemType",value);
			}
		}
        public string defaultStatus
		{
			get
			{
				return this.getValue("defaultStatus");
			}
			set
			{
				this.setValue("defaultStatus",value);
			}
		}
        
		public List<string> mappedElementTypes 
		{
			get 
			{
				return this.workitemMappings.Keys.Where( x => !x.Contains("::")).ToList();
			}
		}
		public List<string> mappedStereotypes 
		{
			get 
			{
				var stereotypes = new List<string>();
				foreach (var fullStereotype in this.workitemMappings.Keys.Where(x => x.Contains("::")))
				{
					string simpleStereotype = fullStereotype.Split(new string[]{"::"},StringSplitOptions.None)[1];
					if (simpleStereotype.Length > 0) stereotypes.Add(simpleStereotype);
				} 
				return stereotypes;
			}
		}

		public string TFSFilterTag
		{
			get
			{
				return this.getValue("TFSFilterTag");
			}
			set
			{
				this.setValue("TFSFilterTag",value);
			}
		}

		public string defaultCollection 
		{
			get
			{
				return this.getValue("defaultCollection");
			}
			set
			{
				this.setValue("defaultCollection",value);
			}
		}
    }
}





