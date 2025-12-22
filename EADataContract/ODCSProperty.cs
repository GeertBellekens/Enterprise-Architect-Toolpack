using EAAddinFramework.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using YamlDotNet.RepresentationModel;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EADataContract
{
    public class ODCSProperty : ODCSElement
    {
        public static string stereotype => profile + "ODCS_Property";
        internal TSF_EA.Attribute modelAttribute => this.modelElement as TSF_EA.Attribute;
        public ODCSProperty(string name) : base(name)
        {
        }
        private YamlMappingNode propertyNode => this.node as YamlMappingNode;
        public ODCSProperty(YamlMappingNode node, ODCSObject owner) : base(node, owner)
        {
            this.primaryKey = getBooleanValue("primaryKey");
            this.unique = getBooleanValue("unique");
            this.required = getBooleanValue("required");
            this.criticalDataElement = getBooleanValue("criticalDataElement");
            this.classification = getStringValue("classification");
            this.logicalType = getStringValue("logicalType");
            //get logicalTypeOptions if present
            YamlMappingNode logicalTypeOptionsNode = null;
            //check for sibling node with key logicalTypeOptions
            if (node.Children.TryGetValue("logicalTypeOptions", out var optionsNode)
                && optionsNode is YamlMappingNode)
            {
                logicalTypeOptionsNode = (YamlMappingNode)optionsNode;
            }
        }
        public bool? primaryKey { get; set; }
        public bool? unique { get; set; }
        public bool? required { get; set; }
        public bool? criticalDataElement { get; set; }
        public string classification { get; set; }
        public string logicalType { get; set; }

        private ODCSLogicalTypeOptions _options = null;
        public ODCSLogicalTypeOptions options
        {
            get
            {
                if (_options == null
                    && this.propertyNode.Children.TryGetValue("logicalTypeOptions", out var optionsNode)
                && optionsNode is YamlMappingNode)
                {
                    _options = new ODCSLogicalTypeOptions(optionsNode, this);
                }
                return _options;
            }
        }


        public override IEnumerable<ODCSItem> getChildItems()
        {
            var childItems = new List<ODCSItem>();
            if (this.options != null)
            {
                childItems.Add(this.options);
            }
            childItems.AddRange(this.qualityRules);
            return childItems;
        }

        public override void getModelElement(Element context)
        {
            var contextClass = context as Class;
            if (contextClass == null)
            {
                throw new InvalidDataException("ODCS Property must be imported into a class");
            }
            //get existing attribute
            var existingAttribute = contextClass.attributes
                                    .OfType<TSF_EA.Attribute>()
                                    .FirstOrDefault(x => x.name == this.name
                                                    && x.fqStereotype == stereotype);
            if (existingAttribute != null)
            {
                this.modelElement = existingAttribute;
            }
            else
            {
                //create new attribute
                var newAttribute = contextClass.addOwnedElement<TSF_EA.Attribute>(this.name, "string"); //default to string type
                newAttribute.fqStereotype = stereotype;
                newAttribute.save();
                this.modelElement = newAttribute;
            }

        }

        public override void updateModelElement(int position)
        {
            EAOutputLogger.log($"Updating attribute: {this.name}"
               , 0
               , LogTypeEnum.log);
            this.modelAttribute.position = position;

            this.modelAttribute.isID = this.primaryKey == true;
            this.modelAttribute.addTaggedValue("unique", this.unique?.ToString());
            this.modelAttribute.lower = (uint)(this.required == true ? 1 : 0);
            this.modelAttribute.addTaggedValue("criticalDataElement", this.criticalDataElement?.ToString());
            this.modelAttribute.addTaggedValue("classification", this.classification);
            this.modelAttribute.addTaggedValue("logicalType", this.logicalType);
            this.modelAttribute.save();
        }
    }

}
