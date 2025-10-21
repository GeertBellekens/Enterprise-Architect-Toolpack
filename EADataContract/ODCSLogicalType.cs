using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public class ODCSLogicalType:ODCSItem
    {
        public ODCSLogicalType() { }
        public ODCSLogicalType(string logicalTypeName, YamlMappingNode optionsNode):base(optionsNode)
        {
            if (Enum.TryParse<ODCSLogicalTypeEnum>(logicalTypeName, out var typeEnumValue))
            {
                this.type = typeEnumValue;
            }
        }
        public ODCSLogicalTypeEnum type { get; set; }
        private ODCSLogicalTypeOptions _options = null;
        public ODCSLogicalTypeOptions options 
        { 
            get
            {
                if (_options == null
                    && this.node != null)
                {
                    _options = new ODCSLogicalTypeOptions(this.node);
                }
                return _options;
            }
        }
    }
    public enum ODCSLogicalTypeEnum
    {
        @string,
        date,
        number,
        integer,
        @object,
        array,
        boolean
    }
}
