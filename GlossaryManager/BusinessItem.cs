using System;
using System.Collections.Generic;
using System.Linq;

using FileHelpers;

using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace GlossaryManager
{

    [DelimitedRecord(";"), IgnoreFirst(1)]
    public class BusinessItem : GlossaryItem
    {

        [FieldOrder(100)]
        [FieldNullValue(typeof(string), "")]
        public string Description = "";

        public string domainPath
        {
            get
            {
                return this.domain != null ?
                        string.Join(".", this.domain.parentDomains.Select(x => x.name))
                        : string.Empty;
            }
        }

        [FieldHidden]
        private Domain _domain = null;
        public Domain domain
        {
            get
            {
                if (_domain == null
                    && this.Origin != null)
                {
                    _domain = Domain.getDomain(this.Origin.owningPackage);
                }
                return _domain;
            }
            set
            {
                _domain = value;
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

        protected override void setOriginValues()
        {
            this.Description = this.Origin.notes;
            
        }

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
            base.update();
            this.Origin.notes = this.Description;
            this.Origin.owningPackage = this.domain.wrappedPackage;
        }


    }
}
