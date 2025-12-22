using EAAddinFramework.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public class ODCSObject: ODCSElement
    {
        public static string stereotype => profile + "ODCS_Object";
        private string dataGranularityDescription { get;set; }

        public ODCSObject(YamlMappingNode node, ODCSSchema owner) : base(node, owner)
        {
            this.dataGranularityDescription = getStringValue("dataGranularityDescription");
        }
        private Class modelClass => this.modelElement as Class;
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
                        var odcsProperty = new ODCSProperty(propertyNode, this);
                        this._properties.Add(odcsProperty);
                    }
                }
                return _properties;
            }
        }

        public override void getModelElement(Element context)
        {
            var contextWrapper = context as ElementWrapper;
            if (contextWrapper == null)
            {
                throw new InvalidDataException("ODCS Object must be imported into an package");
            }

            //get existing element
            var existingElement = context.ownedElements
            .OfType<Class>()
            .FirstOrDefault(x => x.name == this.name 
                            && x.fqStereotype == stereotype);
            if (existingElement != null)
            {
                this.modelElement = existingElement;
            }
            else 
            {
                //create new element
                var newElement = contextWrapper.addOwnedElement<Class>(this.name);
                newElement.fqStereotype = stereotype;
                newElement.save();
                this.modelElement = newElement;
            }
        }

        public override void updateModelElement(int position)
        {
            EAOutputLogger.log( $"Updating object: {this.name}"
                   , this.modelClass.id
                  , LogTypeEnum.log);
            base.updateModelElement(position);
            this.modelClass.position = position;
            this.modelClass.addTaggedValue("dataGranularityDescription", this.dataGranularityDescription);
            //TODO : quality
            this.modelClass.save();
        }

        public override IEnumerable<ODCSItem> getChildItems()
        {
            var childItems = new List<ODCSItem>(this.properties);
            childItems.AddRange(this.qualityRules);
            return childItems;
        }
    }
}
