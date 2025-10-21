using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public class ODCSObject: ODCSElement
    {

        public ODCSObject(string name) : base(name)
        {
        }
        public ODCSObject(YamlMappingNode node) : base(node)
        {
        }
        private List<ODCSProperty> _properties = null;
        public IEnumerable<ODCSProperty> properties
        {
            get
            {
                if (_properties == null)
                {
                    if (mappingNode == null) { return null; }
                    if (!mappingNode.Children.TryGetValue("properties", out var propertiesNode))
                    {
                        return null;
                    }
                    var propertiesSequenceNode = propertiesNode as YamlSequenceNode;
                    if (propertiesSequenceNode == null) { return null; }
                    this._properties = new List<ODCSProperty>();
                    foreach (var propertyNode in propertiesSequenceNode.Children.OfType<YamlMappingNode>())
                    {
                        var odcsProperty = new ODCSProperty(propertyNode);
                        this._properties.Add(odcsProperty);
                    }
                }
                return _properties;
            }
        }
    }
}
