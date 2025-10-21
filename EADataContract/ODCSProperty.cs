using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public class ODCSProperty : ODCSElement
    {
        public ODCSProperty(string name) : base(name)
        {
        }
        public ODCSProperty(YamlMappingNode node) : base(node)
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
                this.logicalType = new ODCSLogicalType(logicalTypeName, logicalTypeOptionsNode);
            }
        }
        public bool? primaryKey { get; set; }
        public bool? unique { get; set; }
        public bool? required { get; set; }
        public bool? criticalDataElement { get; set; }
        public string dataClassification { get; set; }

        

        public ODCSLogicalType logicalType { get; set; }

    }

}
