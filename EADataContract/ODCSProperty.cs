using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using TFS_EA = TSF.UmlToolingFramework.Wrappers.EA;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public class ODCSProperty : ODCSElement
    {
        public static string stereotype => profile + "ODCS_Property";
        internal TFS_EA.Attribute modelAttribute => this.modelElement as TFS_EA.Attribute;
        public ODCSProperty(string name) : base(name)
        {
        }
        public ODCSProperty(YamlMappingNode node, ODCSObject owner) : base(node, owner)
        {
            this.primaryKey = getBooleanValue("primaryKey");
            this.unique = getBooleanValue("unique");
            this.required = getBooleanValue("required");
            this.criticalDataElement = getBooleanValue("criticalDataElement");
            this.dataClassification = getStringValue("dataClassification");


            var logicalTypeName = getStringValue("logicalType");
            if (logicalTypeName != null)
            {
                YamlMappingNode logicalTypeOptionsNode = null;
                //check for sibling node with key logicalTypeOptions
                if (node.Children.TryGetValue("logicalTypeOptions", out var optionsNode)
                    && optionsNode is YamlMappingNode)
                {
                    logicalTypeOptionsNode = (YamlMappingNode)optionsNode;
                }
                //create logical type
                this.logicalType = new ODCSLogicalType(logicalTypeName, logicalTypeOptionsNode, this);
            }
        }
        public bool? primaryKey { get; set; }
        public bool? unique { get; set; }
        public bool? required { get; set; }
        public bool? criticalDataElement { get; set; }
        public string dataClassification { get; set; }

        
        public ODCSLogicalType logicalType { get; set; }
  

        public override IEnumerable<ODCSItem> getChildItems()
        {
            return new List<ODCSItem>() { this.logicalType };
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
                                    .OfType<TFS_EA.Attribute>()
                                    .FirstOrDefault(x => x.name == this.name
                                                    && x.fqStereotype == stereotype); 
            if (existingAttribute != null)
            {
                this.modelElement = existingAttribute;
            }
            else
            {
                //create new attribute
                var newAttribute = contextClass.addOwnedElement<TFS_EA.Attribute>(this.name, "string"); //default to string type
                newAttribute.fqStereotype = stereotype;
                newAttribute.save();
                this.modelElement = newAttribute;
            }

        }

        public override void updateModelElement()
        {
            this.modelAttribute.name = this.name;
            this.modelAttribute.notes = this.description;
            this.modelAttribute.addTaggedValue("physicalName", this.physicalName);
            this.modelAttribute.addTaggedValue("physicalType", this.physicalType);
            this.modelAttribute.addTaggedValue("businessName", this.businessName);
            this.modelAttribute.addTaggedValue("primaryKey", this.primaryKey?.ToString());
            this.modelAttribute.addTaggedValue("unique", this.unique?.ToString());
            this.modelAttribute.lower = (uint)(this.required == true ? 1 : 0);
            this.modelAttribute.addTaggedValue("criticalDataElement", this.criticalDataElement?.ToString());
            this.modelAttribute.addTaggedValue("dataClassification", this.dataClassification);
            this.modelAttribute.save();
        }
    }

}
