using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    interface BOPFNodeOwner : ISAPElement
    {
        BOPFNode addNode(string name, string key);
        string name { get; }
        List<BOPFNode> ownedNodes { get; }
        List<BOPFNode> allOwnedNodes { get; }
    }
}
