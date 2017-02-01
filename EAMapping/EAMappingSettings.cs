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
	public class EAMappingSettings:EAAddinFramework.Utilities.AddinSettings,EAAddinFramework.Mapping.MappingSettings
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
		/// use tagged values as a way to map elements.
		/// If false we use relations with "link to element feature"
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
        /// <summary>
        /// the tagged value to use for attributes when using tagged values for the links (only for newly created items)
        /// </summary>
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
		/// <summary>
		/// the tagge value to use for associations when using tagged values for the links (only for newly created items)
		/// </summary>
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
        /// <summary>
		/// indicates that we use inline mapping logic (only description) in the comments of the tagged value when adding mapping logic.
		/// this only applies when using tagged values for mapping. (only for newly created items)
		/// </summary>
        public bool useInlineMappingLogic
	    {
        	get
			{
        		return this.getBooleanValue("useInlineMappingLogic");
			}
			set
			{
				this.setBooleanValue("useInlineMappingLogic",value);
			}
	    }
       	/// <summary>
		/// the (EA) type of element to use for the mapping logic (only for newly created items)
		/// </summary>
        public string mappingLogicType
        {
        	get
			{
				return this.getValue("mappingLogicType");
			}
			set
			{
				this.setValue("mappingLogicType",value);
			}
		}
        /// <summary>
		/// the (EA) type of element to use for the mapping logic (only for newly created items)
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





