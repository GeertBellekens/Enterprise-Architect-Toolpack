using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal class BOPFAlternativeKey : SAPAttribute<UMLEA.Attribute>
    {
        public static string stereotype => "BOPF_alternativeKey";
        const string uniqueTagName = "Altern. Key Unique";
        const string dataTableTypeTagName = "Data Table Type";

        public BOPFAlternativeKey(string name, BOPFNode ownerNode, string key)
            : base(ownerNode, name, key, stereotype)
        {
            this.owner = ownerNode;
        }
        public BOPFAlternativeKey(UMLEA.Attribute attribute) : base(attribute) { }

        public string unique
        {
            get => this.getStringProperty(uniqueTagName);
            set => this.setStringProperty(uniqueTagName, value);
        }
        public SAPDatatype dataTableType
        {
            get => new SAPDatatype(this.getLinkProperty<UMLEA.DataType>(dataTableTypeTagName));
            set => this.setLinkProperty(dataTableTypeTagName, value?.wrappedElement);
        }
        public SAPClass dataType
        {
            get
            {
                var wrappedType = this.wrappedAttribute.type as UMLEA.Class;
                return wrappedType != null
                        ? new SAPClass(wrappedType) 
                        : null;
            }
            set => this.wrappedAttribute.type = value?.wrappedElement;
        }

    }
}
