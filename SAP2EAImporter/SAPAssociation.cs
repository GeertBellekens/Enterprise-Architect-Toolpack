using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;


namespace SAP2EAImporter
{
    internal class SAPAssociation : SAPConnector<UMLEA.Association>
    {
        
        const string resolvingNodeTagName= "Resolving Node";
        const string categoryTagName = "Category";
        const string associationClassTagName = "Association Class";
        public static string stereotype =>  "SAP_Association";
        
        public SAPAssociation(BOPFNode source, BOPFNode target, string name, string connectorKey)
        : base(source , target, name, connectorKey , stereotype)
        {

        }


        public string resolvingNode
        {
            get => this.getStringProperty(resolvingNodeTagName);
            set => this.setStringProperty(resolvingNodeTagName, value);
        }
        public string category
        {
            get => this.getStringProperty(categoryTagName);
            set => this.setStringProperty(categoryTagName, value);
        }
        public SAPClass associationClass
        {
            get => new SAPClass(this.getLinkProperty<UMLEA.Class>(associationClassTagName));
            set => this.setLinkProperty(associationClassTagName, value.wrappedElement);
        }

        internal SAPAssociation(UMLEA.Association association):base(association){}
        
    }
}
