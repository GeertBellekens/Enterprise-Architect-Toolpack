using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal class SAPClass: SAPElement<UMLEA.Class>
    {
        public SAPClass(string name, UML.Classes.Kernel.Namespace owner)
            : base(name, owner){}
    }
}
