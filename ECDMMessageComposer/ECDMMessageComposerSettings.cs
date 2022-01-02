using System;
using UML = TSF.UmlToolingFramework.UML;
using UTF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using SBF = SchemaBuilderFramework;

namespace ECDMMessageComposer
{
    /// <summary>
    /// Description of ECDMMessageComposerSettings.
    /// </summary>
    public class ECDMMessageComposerSettings : EAAddinFramework.Utilities.AddinSettings, SBF.SchemaSettings
    {
        #region implemented abstract members of AddinSettings

        protected override string addinName => "EAMessageComposer";
        protected override string configSubPath
        {
            get
            {
                return @"\Bellekens\ECDMMessageComposer\";
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
                this.setListValue("ignoredTaggedValues", value);
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
                this.setListValue("ignoredStereotypes", value);
            }
        }
        /// <summary>
        /// list of constraint types to ignore when updating constraints
        /// </summary>
        public List<string> ignoredConstraintTypes
        {
            get
            {
                return this.getListValue("ignoredConstraintTypes");
            }
            set
            {
                this.setListValue("ignoredConstraintTypes", value);
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
                this.setBooleanValue("addDataTypes", value);
            }
        }
        /// <summary>
        /// ignores wether or not the generalization is selected in the profile
        /// </summary>
        public bool copyAllGeneralizations
        {
            get
            {
                return this.getBooleanValue("copyAllGeneralizations");
            }
            set
            {
                this.setBooleanValue("copyAllGeneralizations", value);
            }
        }
        public bool copyExternalGeneralizations
        {
            get
            {
                return this.getBooleanValue("copyGeneralizations");
            }
            set
            {
                this.setBooleanValue("copyGeneralizations", value);
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
                this.setBooleanValue("copyDataTypeGeneralizations", value);
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
                this.setBooleanValue("limitDataTypes", value);
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
                this.setListValue("dataTypesToCopy", value);
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
                this.setBooleanValue("copyDataTypes", value);
            }
        }
        /// <summary>
        /// Copy Datatypes to subset
        /// </summary>
        public bool generateToArtifactPackage
        {
            get
            {
                return this.getBooleanValue("generateToArtifactPackage");
            }
            set
            {
                this.setBooleanValue("generateToArtifactPackage", value);
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
                this.setBooleanValue("addSourceElements", value);
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
                this.setValue("sourceAttributeTagName", value);
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
                this.setValue("sourceAssociationTagName", value);
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
                this.setBooleanValue("redirectGeneralizationsToSubset", value);
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
                this.setBooleanValue("prefixNotes", value);
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
                this.setValue("prefixNotesText", value);
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
                this.setValue("outputName", value);
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
                this.setBooleanValue("checkSecurity", value);
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
                this.setBooleanValue("deleteUnusedSchemaElements", value);
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
                this.setBooleanValue("usePackageSchemasOnly", value);
            }
        }

        public List<string> hiddenElementTypes
        {
            get
            {
                return this.getListValue("hiddenElementTypes");
            }
            set
            {
                this.setListValue("hiddenElementTypes", value);
            }
        }

        public bool dontCreateAttributeDependencies
        {
            get
            {
                return this.getBooleanValue("dontCreateAttributeDependencies");
            }
            set
            {
                this.setBooleanValue("dontCreateAttributeDependencies", value);
            }
        }

        public bool orderAssociationsAlphabetically
        {
            get
            {
                return this.getBooleanValue("orderAssociationsAlphabetically");
            }
            set
            {
                this.setBooleanValue("orderAssociationsAlphabetically", value);
            }
        }
        public bool orderAssociationsAmongstAttributes
        {
            get
            {
                return this.getBooleanValue("orderAssociationsAmongstAttributes");
            }
            set
            {
                this.setBooleanValue("orderAssociationsAmongstAttributes", value);
            }
        }

        public bool tvInsteadOfTrace
        {
            get
            {
                return this.getBooleanValue("tvInsteadOfTrace");
            }
            set
            {
                this.setBooleanValue("tvInsteadOfTrace", value);
            }
        }

        public string elementTagName
        {
            get
            {
                return this.getValue("elementTagName");
            }
            set
            {
                this.setValue("elementTagName", value);
            }
        }
        /// <summary>
        /// Indicates that the original attribute order will be kep. If false then all new attributes will be added to the end.
        /// </summary>
        public bool keepOriginalAttributeOrder
        {
            get
            {
                return this.getBooleanValue("keepOriginalAttributeOrder");
            }
            set
            {
                this.setBooleanValue("keepOriginalAttributeOrder", value);
            }
        }
        /// <summary>
        /// Indicates that the attribute order for all attributes in the subset will be set to 0
        /// </summary>
        public bool setAttributeOrderZero
        {
            get
            {
                return this.getBooleanValue("setAttributeOrderZero");
            }
            set
            {
                this.setBooleanValue("setAttributeOrderZero", value);
            }
        }
        /// <summary>
        /// Associations to an XmlChoice element will be ordered before any attributes
        /// </summary>
        public bool orderXmlChoiceBeforeAttributes
        {
            get
            {
                return this.getBooleanValue("orderXmlChoiceBeforeAttributes");
            }
            set
            {
                this.setBooleanValue("orderXmlChoiceBeforeAttributes", value);
            }
        }
        /// <summary>
        /// Indicates whether or not to create/update a diagram
        /// </summary>
        public bool generateDiagram
        {
            get
            {
                return this.getBooleanValue("generateDiagram");
            }
            set
            {
                this.setBooleanValue("generateDiagram", value);
            }
        }

        public bool keepNotesInSync
        {
            get
            {
                return this.getBooleanValue("keepNotesInSync");
            }
            set
            {
                this.setBooleanValue("keepNotesInSync", value);
            }
        }

        public string customPositionTag
        {
            get
            {
                return this.getValue("customPositionTag");
            }
            set
            {
                this.setValue("customPositionTag", value);
            }
        }

        public List<string> synchronizedTaggedValues
        {
            get
            {
                return this.getListValue("synchronizedTaggedValues");
            }
            set
            {
                this.setListValue("synchronizedTaggedValues", value);
            }
        }
        public bool useAliasForRedefinedElements
        {
            get
            {
                return this.getBooleanValue("useAliasForRedefinedElements");
            }
            set
            {
                this.setBooleanValue("useAliasForRedefinedElements", value);
            }
        }
    }
}




