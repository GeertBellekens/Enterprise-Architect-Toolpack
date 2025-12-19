using EAAddinFramework.Utilities;
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
    public class ODCSLogicalTypeOptions: ODCSItem
    {
        public ODCSLogicalTypeOptions() { }
        internal TSF_EA.Attribute modelAttribute => this.modelElement as TSF_EA.Attribute;
        public ODCSLogicalTypeOptions(YamlNode node, ODCSProperty owner):base(node, owner)
        {
            this.maxItems = getIntValue("maxItems");
            this.minItems = getIntValue("minItems");
            this.uniqueItems = getBooleanValue("uniqueItems");
            this.format = getStringValue("format");
            this.maximum = getStringValue("maximum");
            this.minimum = getStringValue("minimum");
            this.exclusiveMaximum = getStringValue("exclusiveMaximum");
            this.exclusiveMinimum = getStringValue("exclusiveMinimum");
            this.multipleOf = getDecimalValue("multipleOf");
            this.maxLength = getIntValue("maxLength");
            this.minLength = getIntValue("minLength");
            this.pattern = getStringValue("pattern");
            this.maxProperties = getIntValue("maxProperties");
            this.minProperties = getIntValue("minProperties");
            this.required = getBooleanValue("required");
        }
        public int? maxItems { get; set; }
        public int? minItems { get; set; }
        public bool? uniqueItems { get; set; }
        public string format { get; set; }
        public string exclusiveMaximum { get; set; }
        public string exclusiveMinimum { get; set; }
        public string maximum { get; set; }
        public string minimum { get; set; }
        public decimal? multipleOf { get; set; }
        public int? maxLength { get; set; }
        public int? minLength { get; set; }
        public string pattern { get; set; }
        public int? maxProperties { get; set; } 
        public int? minProperties { get; set; }
        public bool? required { get; set; }

        public override IEnumerable<ODCSItem> getChildItems()
        {
            return new List<ODCSItem>();//empty list
        }

        public override void getModelElement(Element context)
        {
            this.modelElement = context as TSF_EA.Attribute;
        }

        public override void updateModelElement()
        {
            EAOutputLogger.log($"Updating logical type options for attribute: {this.modelAttribute?.name}"
              , 0
              , LogTypeEnum.log);

            this.modelAttribute.addTaggedValue("maxItems", (this.maxItems ?? -1).ToString());
            this.modelAttribute.addTaggedValue("minItems", (this.minItems ?? -1).ToString());
            this.modelAttribute.addTaggedValue("uniqueItems", this.uniqueItems?.ToString());
            this.modelAttribute.addTaggedValue("format", this.format);
            this.modelAttribute.addTaggedValue("maximum", this.maximum);
            this.modelAttribute.addTaggedValue("minimum", this.minimum);
            this.modelAttribute.addTaggedValue("exclusiveMaximum", this.exclusiveMaximum);
            this.modelAttribute.addTaggedValue("exclusiveMinimum", this.exclusiveMinimum);
            this.modelAttribute.addTaggedValue("multipleOf", (this.multipleOf ?? -1).ToString());
            this.modelAttribute.addTaggedValue("maxLength", (this.maxLength ?? -1).ToString());
            this.modelAttribute.addTaggedValue("minLength", (this.minLength ?? -1).ToString());
            this.modelAttribute.addTaggedValue("pattern", this.pattern);
            this.modelAttribute.addTaggedValue("maxProperties", (this.maxProperties ?? -1).ToString());
            this.modelAttribute.addTaggedValue("minProperties", (this.minProperties ?? -1).ToString());
            this.modelAttribute.addTaggedValue("required", this.required?.ToString());

            this.modelAttribute.save();
        }
    }
}
