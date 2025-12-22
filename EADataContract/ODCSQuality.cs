using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using YamlDotNet.RepresentationModel;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace EADataContract
{
    public class ODCSQuality : ODCSItem
    {
        public ODCSQuality() { }
        public ODCSQuality(YamlNode node, ODCSElement owner) : base(node, owner)
        {
        }
        private TSF_EA.Enumeration enumType = null;
        private List<string> enumValues = null;
        private YamlMappingNode qualityNode => this.node as YamlMappingNode;
        public override IEnumerable<ODCSItem> getChildItems()
        {
            return new List<ODCSItem>(); //no child items
        }

        public override void getModelElement(Element context)
        {
            this.modelElement = context;
            //check if this is an enumeration quality Rule
            var contextAttribute = context as TSF_EA.Attribute;
            if (contextAttribute != null)
            {
                this.getEnumValues();
                if (this.enumValues.Count > 0)
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
            this.enumType = attribute.type as TSF_EA.Enumeration;
            if (this.enumType == null)
            {
                var enumName = string.IsNullOrEmpty(this.id)
                                ? attribute.name + "_Enum"
                                : this.id;
                //get existing enum
                this.enumType = attribute.getOwner<Package>().ownedElementWrappers.OfType<TSF_EA.Enumeration>()
                    .FirstOrDefault(e => e.name == enumName);
                //create new if neded
                if (this.enumType == null)
                {
                    this.enumType = attribute.getOwner<Package>().addOwnedElement<TSF_EA.Enumeration>(enumName);
                }
                //set enum type as type of attribute
                attribute.type = this.enumType;
                attribute.save();
            }
        }

        public override void updateModelElement(int position)
        {
            if (this.enumType != null)
            {
                updateLiterals();
            }
            else
            {
                //regular quality rule, to be stored in the constraint
                var constrainName = string.IsNullOrEmpty(this.id) ? $"Quality_{position}" : this.id;
                var constraint = this.modelElement.constraints
                       .FirstOrDefault(c => c.name == constrainName);
                if (constraint == null)
                {
                    //create new constraint
                    constraint = this.modelElement.addOwnedElement<UML.Classes.Kernel.Constraint>(constrainName);
                }
                constraint.specification  = new OpaqueExpression(this.getYamlString(), "Quality");
                constraint.save();
            }
        }

        private void updateLiterals()
        {
            //remove existing literals that don't match the new values
            foreach (var literal in this.enumType.ownedLiterals.Where(x => !this.enumValues.Contains(x.name)))
            {
                literal.delete();
            }
            //add missing literals
            foreach (var literal in this.enumValues.Where(v => !this.enumType.ownedLiterals.Any(x => x.name == v)))
            {
                var newLiteral = enumType.addOwnedElement<TSF_EA.EnumerationLiteral>(literal);
                newLiteral.save();
            }
        }
        private void getEnumValues()
        {
            this.enumValues = new List<string>();

            if (this.qualityNode == null) return;
            var metricName = getStringValue("metric");
            if (metricName != "invalidValues") return;
            var mustBeValue = getIntValue("mustBe");
            if (mustBeValue != 0) return;

            // find 'arguments' mapping
            var argsKey = this.qualityNode.Children.Keys
                .OfType<YamlScalarNode>()
                .FirstOrDefault(k => string.Equals(k.Value, "arguments", StringComparison.OrdinalIgnoreCase));
            if (argsKey == null) return;

            if (!this.qualityNode.Children.TryGetValue(argsKey, out var argsNode)) return;
            if (!(argsNode is YamlMappingNode argsMapping)) return;

            // find 'validValues' sequence inside arguments
            var validKey = argsMapping.Children.Keys
                .OfType<YamlScalarNode>()
                .FirstOrDefault(k => string.Equals(k.Value, "validValues", StringComparison.OrdinalIgnoreCase));
            if (validKey == null) return;

            if (!argsMapping.Children.TryGetValue(validKey, out var validNode)) return;
            if (!(validNode is YamlSequenceNode validSeq)) return;

            foreach (var item in validSeq.Children)
            {
                if (item is YamlScalarNode s && s.Value != null)
                {
                    this.enumValues.Add(s.Value);
                }
            }
        }
    }
}