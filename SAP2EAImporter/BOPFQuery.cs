﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal class BOPFQuery : SAPElement<UMLEA.Activity>
    {
        public static string stereotype => "BOPF_query";
        const string filterStructureTagName = "Filter Structure";

        public BOPFQuery(string name, BOPFNode ownerNode, string key)
            : base(name, ownerNode.wrappedElement, stereotype, key)
        {
            this.owner = ownerNode;
        }
        public BOPFQuery(UMLEA.Activity activity) : base(activity) { }

        BOPFNodeOwner _owner;
        public BOPFNodeOwner owner
        {
            get => this._owner;
            set
            {
                //get the existing composition before setting the owner
                var composition = this.compositionToOwner;
                this._owner = value;
                //check if there is a composition to the new owner
                if (composition == null)
                {
                    composition = this.compositionToOwner;
                }
                //ceate new composition if needed
                if (composition == null)
                {
                    composition = new SAPComposition(value, this);
                }
                composition.source = value;
                composition.save();

                //set ownership of wrapped element 
                this.wrappedElement.owner = value.elementWrapper;
                this.save();
            }
        }
        public SAPComposition compositionToOwner
        {
            get => SAPComposition.getExisitingComposition(this.owner, this);
        }
        public SAPDatatype filterStructure
        {
            get => new SAPDatatype(this.getLinkProperty<UMLEA.DataType>(filterStructureTagName));
            set => this.setLinkProperty(filterStructureTagName, value?.wrappedElement);
        }
    }
}
