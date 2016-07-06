using System;
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

		
		public EAImvertorSettings()
		{}
		
		/// <summary>
		/// the URL for the Imvertor Service
		/// </summary>
		public string imvertorURL
		{
			get
			{
				return this.currentConfig.AppSettings.Settings["imvertorURL"].Value;
			}
			set
			{
				this.currentConfig.AppSettings.Settings["imvertorURL"].Value = value;
			}
		}
		/// <summary>
		/// the default pincode
		/// </summary>
		public string defaultPIN
		{
			get
			{
				return this.currentConfig.AppSettings.Settings["defaultPIN"].Value;
			}
			set
			{
				this.currentConfig.AppSettings.Settings["defaultPIN"].Value = value;
			}
		}
				

	}
}
