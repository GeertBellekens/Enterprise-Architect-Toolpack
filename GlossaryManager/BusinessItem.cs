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
        public string Domain = "";

        public override string ToString()
        {
            return "BusinessItem(" + base.ToString() + "," +
              string.Join(", ", new List<string>() {
              this.Description,
              this.Domain
              }) +
            ")";
        }




        // EA support

        public override string Stereotype { get { return "EDD_BusinessItem"; } }

        protected override void setOriginValues()
        {
            this.Description = this.Origin.notes;
            this.Domain = this.Origin.owningPackage.name;
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
