using System;
using System.Collections.Generic;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using System.Configuration;
using System.Linq;


namespace EAImvertor
{
	/// <summary>
	/// Description of NavigatorSettings.
	/// </summary>
	public class EAImvertorSettings : EAAddinFramework.Utilities.AddinSettings
	{
		#region implemented abstract members of AddinSettings

		protected override string configSubPath
		{
			get 
			{
				return @"\Bellekens\EAImvertor\";
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
			{
				this.setValue("imvertorURL",value);
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
				this.setValue("defaultPIN",value);
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
				return  this.getValue("imvertorStereotypes").Split(',').ToList();;
			}
			set
			{
				this.setValue("imvertorStereotypes",string.Join(",",value.ToArray()));
			}
		}
	}
}
