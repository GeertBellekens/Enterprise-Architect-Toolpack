using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;


namespace SAP2EAImporter
{
    internal class SAPComposition : SAPConnector<UMLEA.Association>
    {
        public static string stereotype => "SAP_Composition";

        
        public SAPComposition(ISAPElement source, ISAPElement target)
            : base(source, target, string.Empty, string.Empty, stereotype)
        {
            this.wrappedConnector.sourceEnd.aggregation = UML.Classes.Kernel.AggregationKind.composite;
            this.wrappedConnector.targetEnd.isNavigable = true;
            this.wrappedConnector.save();
        }



        internal SAPComposition(UMLEA.Association association) : base(association) { }
        public static SAPComposition getExisitingComposition(ISAPElement source, ISAPElement target)
        {
            var existingConnector = getExistingConnector(source, target, stereotype, string.Empty);

            return existingConnector != null ? 
                    new SAPComposition(existingConnector)
                    : null; 
        }

    }
}



