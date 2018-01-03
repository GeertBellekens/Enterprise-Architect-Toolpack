using System;
using System.Collections.Generic;
using System.Linq;

using FileHelpers;

using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace GlossaryManager
{

    [DelimitedRecord(";"), IgnoreFirst(1)]
    public class DataItem : GlossaryItem
    {

        [FieldOrder(100)]
        [FieldNullValue(typeof(string), "")]
        public string Label;

        [FieldOrder(101)]
        [FieldNullValue(typeof(string), "")]
        public string LogicalDatatypeName = "";

        [FieldOrder(102)]
        [FieldNullValue(typeof(int), "0")]
        public int Size;

        [FieldOrder(103)]
        [FieldNullValue(typeof(string), "")]
        public string Format;

        [FieldOrder(104)]
        [FieldNullValue(typeof(string), "")]
        public string Description;

        [FieldOrder(105)]
        [FieldNullValue(typeof(string), "")]
        public string InitialValue;

        public override string ToString()
        {
            return "DataItem(" + base.ToString() + "," +
              string.Join(", ", new List<string>() {
          this.Label,
          this.LogicalDatatypeName,
          this.Size.ToString(),
          this.Format,
          this.Description,
          this.InitialValue
              }) +
            ")";
        }

        public LogicalDatatype logicalDatatype { get; set; }
        public BusinessItem businessItem { get; set; }

        // EA support

        public override string Stereotype { get { return "EDD_DataItem"; } }

        #region implemented abstract members of GlossaryItem
        protected override void setOriginValues()
        {
            this.Label = this.getTaggedValueString("label");
            //get the logical datatype
            getLogicalDatatypeFromOrigin();
            //get the business item
            getBusinessItemFromOrigin();
            int newSize;
            if (int.TryParse(this.getTaggedValueString("size"), out newSize))
                this.Size = newSize;
            this.Format = this.getTaggedValueString("format");
            this.InitialValue = this.getTaggedValueString("initial value");
        }
        private void getLogicalDatatypeFromOrigin()
        {
            if (this.Origin != null)
            {
                var tv = this.Origin.getTaggedValue("logical datatype");
                if (tv != null)
                {
                    var datatype = tv.tagValue as UML.Classes.Kernel.DataType;
                    if (datatype != null)
                        this.logicalDatatype = new LogicalDatatype(datatype);
                }
            }
        }
        private void getBusinessItemFromOrigin()
        {
            if (this.Origin != null)
            {
                var tv = this.Origin.getTaggedValue("business item");
                if (tv != null)
                {
                    var businessItemClass = tv.tagValue as UML.Classes.Kernel.Class;
                    if (businessItemClass != null)
                    {
                        this.businessItem = new BusinessItem();
                        this.businessItem.Origin = (TSF_EA.Class)businessItemClass;
                    }
                }
            }
        }
        #endregion
        protected override void update()
        {
            base.update();
            this.Origin.addTaggedValue("label", this.Label);
            if (logicalDatatype != null)
                this.Origin.addTaggedValue("logical datatype", this.logicalDatatype.wrappedDatatype);
            this.Origin.addTaggedValue("size", this.Size.ToString());
            this.Origin.addTaggedValue("format", this.Format);
            this.Origin.addTaggedValue("initial value", this.InitialValue);
            this.Origin.notes = this.Description;
        }

    }
}
