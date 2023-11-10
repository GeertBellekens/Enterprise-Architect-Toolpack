using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal interface ISAPElement
    {
        UMLEA.ElementWrapper elementWrapper { get; set; }
        void formatDiagrams();
    }
}
