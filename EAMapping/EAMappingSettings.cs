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
        		return this.getBooleanValue("useTaggedValues");
			}
			set
			{
				this.setBooleanValue("useTaggedValues",value);
			}
	    }
		public string linkedAttributeTagName
		{
			get
			{
				return this.getValue("linkedAttributeTagName");
			}
			set
			{
				this.setValue("linkedAttributeTagName",value);
			}
		}
        public string linkedAssociationTagName
        {
        	get
			{
				return this.getValue("linkedAssociationTagName");
			}
			set
			{
				this.setValue("linkedAssociationTagName",value);
			}
		}
    }
}





