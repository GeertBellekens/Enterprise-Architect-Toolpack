using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EADataContract
{
    public class ODCSSchema : ODCSItem
    {
        
        public ODCSSchema(YamlNode node, ODCSDataContract owner) : base(node, owner)
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
                        var odcsObject = new ODCSObject(objectNode, this);
                        this._objects.Add(odcsObject);
                    }
                }
                return _objects;
            }
        }

        public override void getModelElement(TSF_EA.Element context)
        {
            this.modelElement = context;
        }

        public override void updateModelElement(int position)
        {
            return; //schema has no model element to update
        }

        public override IEnumerable<ODCSItem> getChildItems()
        {
            return this.objects;
        }
    }
}