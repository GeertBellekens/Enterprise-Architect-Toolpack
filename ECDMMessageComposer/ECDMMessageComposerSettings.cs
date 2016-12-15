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
				return this.getValue("ignoredTaggedValues").Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries).ToList<string>();;
			}
			set
			{
				this.setValue("ignoredTaggedValues",string.Join(",",value));
			}
		}
		/// <summary>
		/// list of stereotypes of elements to ignore when updating a subset model
		/// </summary>
		public List<string> ignoredStereotypes
		{
			get
			{
				return this.getValue("ignoredStereotypes").Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries).ToList<string>();;
			}
			set
			{
				this.setValue("ignoredStereotypes",string.Join(",",value));
			}
		}
		/// <summary>
		/// indicates if the data types should be added to the diagram
		/// </summary>
		public bool addDataTypes
		{
			get
			{
				bool result;
				return bool.TryParse(this.getValue("addDataTypes"), out result) ? result : true;
			}
			set
			{
				this.setValue("addDataTypes",value.ToString());
			}
		}
		/// <summary>
		/// indicates if datatype generalizations should be copied tot he subset datatypes
		/// </summary>
		public bool copyDataTypeGeneralizations
		{
			get
			{
				bool result;
				return bool.TryParse(this.getValue("copyDataTypeGeneralizations"), out result) ? result : true;
			}
			set
			{
				this.setValue("copyDataTypeGeneralizations",value.ToString());
			}
		}
		/// <summary>
		/// indicates if the data types should be added to the diagram
		/// </summary>
		public bool limitDataTypes
		{
			get
			{
				bool result;
				return bool.TryParse(this.getValue("limitDataTypes"), out result) ? result : true;
			}
			set
			{
				this.setValue("limitDataTypes",value.ToString());
			}
		}
		/// <summary>
		/// list of stereotypes of elements to ignore when updating a subset model
		/// </summary>
		public List<string> dataTypesToCopy
		{
			get
			{
				return this.getValue("dataTypesToCopy").Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries).ToList<string>();;
			}
			set
			{
				this.setValue("dataTypesToCopy",string.Join(",",value));
			}
		}
        /// <summary>
        /// Copy Datatypes to subset
        /// </summary>
        public bool copyDataTypes
	    {
        	get
			{
				bool result;
				return bool.TryParse(this.getValue("copyDataTypes"), out result) ? result : true;
			}
			set
			{
				this.setValue("copyDataTypes",value.ToString());
			}
	    }
		/// <summary>
		/// indicates if the data types should be added to the diagram
		/// </summary>
		public bool addSourceElements
		{
			get
			{
				bool result;
				return bool.TryParse(this.getValue("addSourceElements"), out result) ? result : true;
			}
			set
			{
				this.setValue("addSourceElements",value.ToString());
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
				bool result;
				return bool.TryParse(this.getValue("redirectGeneralizationsToSubset"), out result) ? result : true;
			}
			set
			{
				this.setValue("redirectGeneralizationsToSubset",value.ToString());
			}			
		}
		/// <summary>
		/// indicates whether the notes in the subset elements should be prefixed
		/// </summary>
		public bool prefixNotes 
		{
			get
			{
				bool result;
				return bool.TryParse(this.getValue("prefixNotes"), out result) ? result : true;
			}
			set
			{
				this.setValue("prefixNotes",value.ToString());
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
				bool result;
				return bool.TryParse(this.getValue("checkSecurity"), out result) ? result : true;
			}
			set
			{
				this.setValue("checkSecurity",value.ToString());
			}				
		}
    }
}





