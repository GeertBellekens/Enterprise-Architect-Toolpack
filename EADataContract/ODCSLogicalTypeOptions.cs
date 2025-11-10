using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public class ODCSLogicalTypeOptions: ODCSItem
    {
        public ODCSLogicalTypeOptions() { }
        public ODCSLogicalTypeOptions(YamlNode node, ODCSLogicalType owner):base(node, owner)
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
            return; //no model element
        }

        public override void updateModelElement()
        {
            return; //no model element
        }
    }
}
