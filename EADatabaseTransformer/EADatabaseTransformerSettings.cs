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
		protected override string addinName => "EADatabaseTransformer";
		protected override string configSubPath
		{
			get 
			{
				return @"\Bellekens\EADatabaseTransformer\";
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

		
		/// <summary>
		/// the path for the abbreviations file
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
		/// <summary>
		/// the path for the abbreviations file
		/// </summary>
		public string outputName
		{
			get
			{
				return this.getValue("outputName");
			}
			set
			{
				this.setValue("outputName",value);
			}
		}

				

	}
}
