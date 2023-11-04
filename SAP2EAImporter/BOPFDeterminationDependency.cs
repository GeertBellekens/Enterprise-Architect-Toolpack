using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;


namespace SAP2EAImporter
{
    internal class BOPFDeterminationDependency : SAPConnector<UMLEA.Dependency>
    {

 
        public static string stereotype => "BOPF_determinationDependency";

        public BOPFDeterminationDependency(BOPFDetermination source, BOPFDetermination target)
        : base(source, target, "", "", stereotype)
        {

        }
        internal BOPFDeterminationDependency(UMLEA.Dependency dependency) : base(dependency) { }

    }
}
