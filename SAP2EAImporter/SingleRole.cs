using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    class SingleRole: Role
    {
        const string stereotypeName = "SAP_singleRole";

        public SingleRole(string elementName, UML.Classes.Kernel.Package package)
            : base(elementName, package, stereotypeName)
        {

        }
    }
}
