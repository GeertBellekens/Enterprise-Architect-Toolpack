using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public abstract class ODCSItem
    {
        protected ODCSItem() { }
        public ODCSItem(YamlNode node)
        {
            this.node = node;
        }
        public YamlNode node { get; protected set; }
        protected string getStringValue(string key)
        {
            if (this.node is YamlMappingNode mappingNode)
            {
                if (mappingNode.Children.TryGetValue(key, out var valueNode)
                    && valueNode is YamlScalarNode)
                {
                    return ((YamlScalarNode)valueNode).Value;
                }
            }
            return null;
        }
        protected bool? getBooleanValue(string key)
        {
            if (bool.TryParse(getStringValue(key), out var booleanValue))
            {
                return booleanValue;
            }
            else
            {
                return null;
            }
        }
        protected int? getIntValue(string key)
        {
            if (int.TryParse(getStringValue(key), out var intValue))
            {
                return intValue;
            }
            else
            {
                return null;
            }
        }
        public override string ToString()
        {
            return this.node?.ToString();
        }
    }
}
