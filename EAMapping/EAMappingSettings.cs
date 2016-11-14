using System;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace EAMapping
{
	/// <summary>
	/// Description of ECDMMessageComposerSettings.
	/// </summary>
	public class EAMappingSettings:EAAddinFramework.Utilities.AddinSettings
	{
		#region implemented abstract members of AddinSettings

		protected override string configSubPath
		{
			get 
			{
				return @"\Bellekens\EAMapping\";
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
        /// Copy Datatypes to subset
        /// </summary>
        public bool useTaggedValues
	    {
        	get
			{
				bool result;
				return bool.TryParse(this.getValue("useTaggedValues"), out result) ? result : true;
			}
			set
			{
				this.setValue("useTaggedValues",value.ToString());
			}
	    }
		public string sourceAttributeTagName
		{
			get
			{
				return this.getValue("sourceAttributeTagName");
			}
			set
			{
				this.setValue("sourceAttributeTagName",value);
			}
		}
        public string sourceAssociationTagName
        {
        	get
			{
				return this.getValue("sourceAssociationTagName");
			}
			set
			{
				this.setValue("sourceAssociationTagName",value);
			}
		}
    }
}





