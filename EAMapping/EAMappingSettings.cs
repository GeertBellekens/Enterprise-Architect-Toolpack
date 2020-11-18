namespace EAMapping
{
    /// <summary>
    /// Description of ECDMMessageComposerSettings.
    /// </summary>
    public class EAMappingSettings : EAAddinFramework.Utilities.AddinSettings, EAAddinFramework.Mapping.MappingSettings
    {
        #region implemented abstract members of AddinSettings
        protected override string addinName => "EAMapping";
        protected override string configSubPath => @"\Bellekens\EAMapping\";
        protected override string defaultConfigAssemblyFilePath => System.Reflection.Assembly.GetExecutingAssembly().Location;
        #endregion

        /// <summary>
        /// use tagged values as a way to map elements.
        /// If false we use relations with "link to element feature"
        /// </summary>
        public bool useTaggedValues
        {
            get => this.getBooleanValue("useTaggedValues");
            set => this.setBooleanValue("useTaggedValues", value);
        }
        /// <summary>
        /// the tagged value to use for mappings to attributes
        /// </summary>
		public string linkedAttributeTagName
        {
            get => this.getValue("linkedAttributeTagName");
            set => this.setValue("linkedAttributeTagName", value);
        }
        /// <summary>
        /// the tagged value to use for mappings to associations
        /// </summary>
        public string linkedAssociationTagName
        {
            get => this.getValue("linkedAssociationTagName");
            set => this.setValue("linkedAssociationTagName", value);
        }
        /// <summary>
        /// the tagged value to use for mappings to classifiers
        /// </summary>
        public string linkedElementTagName
        {
            get => this.getValue("linkedElementTagName");
            set => this.setValue("linkedElementTagName", value);
        }
        /// <summary>
		/// indicates that we use inline mapping logic (only description) in the comments of the tagged value when adding mapping logic.
		/// this only applies when using tagged values for mapping. (only for newly created items)
		/// </summary>
        public bool useInlineMappingLogic
        {
            get => this.getBooleanValue("useInlineMappingLogic");
            set => this.setBooleanValue("useInlineMappingLogic", value);
        }
        /// <summary>
        /// the (EA) type of element to use for the mapping logic (only for newly created items)
        /// </summary>
        public string mappingLogicType
        {
            get => this.getValue("mappingLogicType");
            set => this.setValue("mappingLogicType", value);
        }
        /// <summary>
		/// the (EA) type of element to use for the mapping logic (only for newly created items)
		/// </summary>
        public string outputName
        {
            get => this.getValue("outputName");
            set => this.setValue("outputName", value);
        }
        /// <summary>
		/// the query to use when searching for context items
		/// </summary>
        public string contextQuery
        {
            get => this.getValue("contextQuery");
            set => this.setValue("contextQuery", value);
        }

        
    }
}





