using System;
using System.Collections.Generic;
using System.Linq;

using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace GlossaryManager
{

    public class BusinessItem : GlossaryItem
    {

        public string Description
        {
            get { return this.origin.notes; }
            set { this.origin.notes = value; }
        }

        public override string ToString()
        {
            return "BusinessItem(" + base.ToString() + "," +
              string.Join(", ", new List<string>() {
              this.Description,
              this.domainPath
              }) +
            ")";
        }

        private IEnumerable<DataItem> _linkedDataItems;
        public IEnumerable<DataItem> linkedDataItems
        {
            get
            {
                if (this._linkedDataItems == null)
                {
                    this._linkedDataItems = getLinkedDataItems();
                }
                return _linkedDataItems;
            }
        }
        public string linkedDataItemsDisplayString
        {
            get
            {
                return string.Join(", ", this.linkedDataItems.Select(x => x.Name));
            }
        }

        private IEnumerable<DataItem> getLinkedDataItems()
        {
            var foundDataItems = new List<DataItem>();
            //get the EA elements
            var sqlGetLinkedDataItems = $@"select tv.[Object_ID] from t_objectproperties tv
                                        where tv.[Property] = 'business item'
                                        and tv.VALUE like '{this.origin.guid}'";

            return GlossaryItemFactory.getFactory(this.origin.EAModel, this.settings).getGlossaryItemsFromQuery<DataItem>(sqlGetLinkedDataItems);
        }

        public override string Stereotype { get { return "EDD_BusinessItem"; } }


        private List<UML.Classes.Kernel.Package> getDomains(UML.Classes.Kernel.Package domainPackage)
        {
            var domains = new List<UML.Classes.Kernel.Package>();
            domains.Add(domainPackage);
            if (domainPackage.owningPackage != null && !domainPackage.owningPackage.Equals(this.settings.businessItemsPackage))
            {
                domains.AddRange(getDomains(domainPackage.owningPackage));
            }
            return domains;
        }


        protected override void update()
        {
            //nothing specific to do
        }

        protected override void reloadData()
        {
            //nothing specific to do
        }

        protected override void setOwningPackage()
        {
            if (this.domain.businessItemsPackage == null) domain.createMissingPackage();
            this.origin.owningPackage = this.domain.businessItemsPackage;
        }
    }
}
