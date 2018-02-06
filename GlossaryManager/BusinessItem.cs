using System;
using System.Collections.Generic;
using System.Linq;

using FileHelpers;

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

        public string domainPath
        {
            get
            {
                return this.domain != null ?
                        string.Join(".", this.domain.parentDomains.Select(x => x.name))
                        : string.Empty;
            }
        }

        private Domain _domain = null;
        public Domain domain
        {
            get
            {
                if (_domain == null
                    && this.origin != null)
                {
                    _domain = Domain.getDomain(this.origin.owningPackage);
                }
                return _domain;
            }
            set
            {
                _domain = value;
                this.origin.owningPackage = this.domain.wrappedPackage;
            }
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



        // EA support

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
    }
}
