using System;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using System.Linq;


namespace EADatabaseTransformer
{
	/// <summary>
	/// Description of NavigatorSettings.
	/// </summary>
	public class EADatabaseTransformerSettings : EAAddinFramework.Utilities.AddinSettings
	{
		#region implemented abstract members of AddinSettings

		protected override string configSubPath
		{
			get 
			{
				return @"\Bellekens\EADatabaseTransformer\";
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
		public string abbreviationsPath
		{
			get
			{
				return this.getValue("abbreviationsPath");
			}
			set
			{
				this.setValue("abbreviationsPath",value);
			}
		}

				

	}
}
