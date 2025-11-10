using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public abstract class ODCSElement: ODCSItem
    {
        protected YamlMappingNode mappingNode => (YamlMappingNode)node;
        public ODCSElement(string name) 
        { 
            this.name = name;
        }
        public ODCSElement(YamlMappingNode node, ODCSItem owner) :base(node, owner)
        {
            this.name = getStringValue("name");
            this.physicalName = getStringValue("physicalName");
            this.physicalType = getStringValue("physicalType");
            this.description = getStringValue("description");
            this.businessName = getStringValue("businessName");
            if (node.Children.TryGetValue("quality", out var qualityNode))
            {
                this.quality = new ODCSQuality(qualityNode, this);
            }
        }

        public string name { get; set; }
        public string physicalName { get; set; }
        public string physicalType { get; set; }
        public string description { get; set; }
        public string businessName { get; set; }
        public ODCSQuality quality { get; set; }

    }
}
