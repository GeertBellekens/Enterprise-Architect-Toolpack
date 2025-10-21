using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public class ODCSSchema : ODCSItem
    {
        public ODCSSchema(YamlNode node) : base(node)
        {

        }
        private List<ODCSObject> _objects = null;
        public IEnumerable<ODCSObject> objects
        {
            get
            {
                if (_objects == null)
                {
                    var sequenceNode = this.node as YamlSequenceNode;
                    if (sequenceNode == null) { return null; }

                    this._objects = new List<ODCSObject>();
                    foreach (var objectNode in sequenceNode.Children.OfType<YamlMappingNode>())
                    {
                        var odcsObject = new ODCSObject(objectNode);
                        this._objects.Add(odcsObject);
                    }
                }
                return _objects;
            }
        }
    }
}