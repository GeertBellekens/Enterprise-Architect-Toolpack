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

        [FieldOrder(101)]
        [FieldNullValue(typeof(string), "")]
        public string domainPath = "";

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

            this.domainPath = getDomainPath();
        }

        private string getDomainPath()
        {
            setDomainList();
            return string.Join(".", domainList.Select(x => x.name));
        }
        private List<UML.Classes.Kernel.Package> domainList;

        private void setDomainList()
        {
            if (this.Origin != null)
            {
                this.domainList = getDomains(this.Origin.owningPackage);
                //set it from top to bottom
                this.domainList.Reverse();
            }
        }
        private List<UML.Classes.Kernel.Package> getDomains(UML.Classes.Kernel.Package domainPackage)
        {
            var domains = new List<UML.Classes.Kernel.Package>();
            domains.Add(domainPackage);
            if (domainPackage.owningPackage != null  && ! domainPackage.owningPackage.Equals(this.settings.businessItemsPackage))
            {
                domains.AddRange(getDomains(domainPackage.owningPackage));
            }
            return domains;
        }


        public override void Update(UML.Classes.Kernel.Class clazz)
        {
            base.Update(clazz);

            var eaClass = clazz as TSF_EA.ElementWrapper;
            eaClass.notes = this.Description;

            clazz.save();
        }

    }
}
