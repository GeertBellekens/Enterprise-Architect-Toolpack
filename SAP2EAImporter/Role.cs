using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    abstract class Role : SAPElement<UMLEA.Class>
    {
        public Role(string elementName, UML.Classes.Kernel.Package package, string stereotypeName)
            : base(elementName, package, stereotypeName)
        {
        }
        protected Role(UMLEA.Class element) : base(element) { }
        public string shortDescription
        {
            get => this.wrappedElement.taggedValues.FirstOrDefault(x => x.name == "Short Description").tagValue as string;
            set => this.wrappedElement.addTaggedValue("Short Description", value);
        }

    }

}