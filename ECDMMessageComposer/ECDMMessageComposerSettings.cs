using System;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using SBF=SchemaBuilderFramework;

namespace ECDMMessageComposer
{
	/// <summary>
	/// Description of ECDMMessageComposerSettings.
	/// </summary>
	public class ECDMMessageComposerSettings:EAAddinFramework.Utilities.AddinSettings,SBF.SchemaSettings
	{
		#region implemented abstract members of AddinSettings

		protected override string configSubPath
		{
			get 
			{
				return @"\Bellekens\ECDMMessageComposer\";
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
		/// list of tagged value names to ignore when updating tagged values
		/// </summary>
		public List<string> ignoredTaggedValues
		{
			get
			{
				return this.getListValue("ignoredTaggedValues");
			}
			set
			{
				this.setListValue("ignoredTaggedValues",value);
			}
		}
		/// <summary>
		/// list of stereotypes of elements to ignore when updating a subset model
		/// </summary>
		public List<string> ignoredStereotypes
		{
			get
			{
				return this.getListValue("ignoredStereotypes");
			}
			set
			{
				this.setListValue("ignoredStereotypes",value);
			}
		}
		/// <summary>
		/// indicates if the data types should be added to the diagram
		/// </summary>
		public bool addDataTypes
		{
        	get
			{
        		return this.getBooleanValue("addDataTypes");
			}
			set
			{
				this.setBooleanValue("addDataTypes",value);
			}
		}

		public bool copyGeneralizations 
		{
        	get
			{
        		return this.getBooleanValue("copyGeneralizations");
			}
			set
			{
				this.setBooleanValue("copyGeneralizations",value);
			}
		}

		/// <summary>
		/// indicates if datatype generalizations should be copied tot he subset datatypes
		/// </summary>
		public bool copyDataTypeGeneralizations
		{
        	get
			{
        		return this.getBooleanValue("copyDataTypeGeneralizations");
			}
			set
			{
				this.setBooleanValue("copyDataTypeGeneralizations",value);
			}
		}
		/// <summary>
		/// indicates if the data types should be added to the diagram
		/// </summary>
		public bool limitDataTypes
		{
        	get
			{
        		return this.getBooleanValue("limitDataTypes");
			}
			set
			{
				this.setBooleanValue("limitDataTypes",value);
			}
		}
		/// <summary>
		/// list of stereotypes of elements to ignore when updating a subset model
		/// </summary>
		public List<string> dataTypesToCopy
		{
			get
			{
				return this.getListValue("dataTypesToCopy");
			}
			set
			{
				this.setListValue("dataTypesToCopy",value);
			}
		}
        /// <summary>
        /// Copy Datatypes to subset
        /// </summary>
        public bool copyDataTypes
	    {
        	get
			{
        		return this.getBooleanValue("copyDataTypes");
			}
			set
			{
				this.setBooleanValue("copyDataTypes",value);
			}
	    }
		/// <summary>
		/// indicates if the data types should be added to the diagram
		/// </summary>
		public bool addSourceElements
		{
        	get
			{
        		return this.getBooleanValue("addSourceElements");
			}
			set
			{
				this.setBooleanValue("addSourceElements",value);
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
		/// <summary>
		/// indicates whether the generalizations should be copied to the subset if the parent element is in the subset as well.
		/// </summary>
		public bool redirectGeneralizationsToSubset 
		{
        	get
			{
        		return this.getBooleanValue("redirectGeneralizationsToSubset");
			}
			set
			{
				this.setBooleanValue("redirectGeneralizationsToSubset",value);
			}		
		}
		/// <summary>
		/// indicates whether the notes in the subset elements should be prefixed
		/// </summary>
		public bool prefixNotes 
		{
        	get
			{
        		return this.getBooleanValue("prefixNotes");
			}
			set
			{
				this.setBooleanValue("prefixNotes",value);
			}					
		}
		/// <summary>
		/// the prefix to use when prefixing the notes
		/// </summary>
		public string prefixNotesText
        {
        	get
			{
				return this.getValue("prefixNotesText");
			}
			set
			{
				this.setValue("prefixNotesText",value);
			}
		}
		/// <summary>
		/// the output name to use
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
		/// <summary>
		/// indicates whether the notes in the subset elements should be prefixed
		/// </summary>
		public bool checkSecurity 
		{
        	get
			{
        		return this.getBooleanValue("checkSecurity");
			}
			set
			{
				this.setBooleanValue("checkSecurity",value);
			}				
		}
		/// <summary>
		/// delete subset elements that are not used?
		/// </summary>
		public bool deleteUnusedSchemaElements 
		{
        	get
			{
        		return this.getBooleanValue("deleteUnusedSchemaElements");
			}
			set
			{
				this.setBooleanValue("deleteUnusedSchemaElements",value);
			}				
		}
		/// <summary>
		/// delete subset elements that are not used?
		/// </summary>
		public bool usePackageSchemasOnly 
		{
        	get
			{
        		return this.getBooleanValue("usePackageSchemasOnly");
			}
			set
			{
				this.setBooleanValue("usePackageSchemasOnly",value);
			}				
		}
    }
}





