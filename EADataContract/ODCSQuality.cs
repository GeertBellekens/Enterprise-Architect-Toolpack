using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using YamlDotNet.RepresentationModel;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EADataContract
{
    public class ODCSQuality:ODCSItem
    {
        public ODCSQuality() { }
        public ODCSQuality(YamlNode node, ODCSElement owner):base(node, owner)
        {
        }
        private YamlMappingNode qualityNode => this.node as YamlMappingNode;
        public override IEnumerable<ODCSItem> getChildItems()
        {
            return new List<ODCSItem>(); //no child items
        }

        public override void getModelElement(Element context)
        {
            //check if this is an enumeration quality Rule
            var contextAttribute = context as TSF_EA.Attribute;
            if(contextAttribute != null)
            {
                var enumValues = this.getEnumValues();
                if (enumValues != null && enumValues.Count > 0)
                {
                    // enumeration detected; enumValues contains the allowed values
                    // Use these values as needed by the caller
                    updateEnumAttribute(contextAttribute, enumValues);
                }
            }

        }

        private void updateEnumAttribute(TSF_EA.Attribute attribute, List<string> enumValues)
        {
            if (attribute == null || enumValues == null || enumValues.Count == 0) return;
            //check if the attribute already has an enumeration type with the same values
            var enumType = attribute.type as TSF_EA.Enumeration;
            if (enumType == null)
            {
                //create new enumeration type
                enumType = attribute.getOwner<Package>().addOwnedElement<TSF_EA.Enumeration>(attribute.name + "_Enum");
                attribute.type = enumType;
                attribute.save();
            }
            //remove existing literals that don't match the new values
            foreach (var literal in enumType.ownedLiterals.Where(x => !enumValues.Contains(x.name)))
            {
                literal.delete();
            }
            //add missing literals
            foreach (var literal in enumValues.Where(v => !enumType.ownedLiterals.Any(x => x.name == v)))
            {
                var newLiteral = enumType.addOwnedElement<TSF_EA.EnumerationLiteral>(literal);
                newLiteral.save();
            }
        }

        public override void updateModelElement()
        {
            //TODO: implement
        }
        private List<string> getEnumValues()
        {
            var result = new List<string>();
            
            if (this.qualityNode == null) return null;
            var metricName = getStringValue("metric");
            if (metricName != "invalidValues") return null;
            var mustBeValue = getIntValue("mustBe");
            if (mustBeValue != 0) return null; 

            // find 'arguments' mapping
            var argsKey = this.qualityNode.Children.Keys
                .OfType<YamlScalarNode>()
                .FirstOrDefault(k => string.Equals(k.Value, "arguments", StringComparison.OrdinalIgnoreCase));
            if (argsKey == null) return result;

            if (!this.qualityNode.Children.TryGetValue(argsKey, out var argsNode)) return result;
            if (!(argsNode is YamlMappingNode argsMapping)) return result;

            // find 'validValues' sequence inside arguments
            var validKey = argsMapping.Children.Keys
                .OfType<YamlScalarNode>()
                .FirstOrDefault(k => string.Equals(k.Value, "validValues", StringComparison.OrdinalIgnoreCase));
            if (validKey == null) return result;

            if (!argsMapping.Children.TryGetValue(validKey, out var validNode)) return result;
            if (!(validNode is YamlSequenceNode validSeq)) return result;

            foreach (var item in validSeq.Children)
            {
                if (item is YamlScalarNode s && s.Value != null)
                {
                    result.Add(s.Value);
                }
            }

            return result;
        }
    }
}