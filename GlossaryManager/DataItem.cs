using System;
using System.Collections.Generic;
using System.Linq;

using FileHelpers;

using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace GlossaryManager
{

    public class DataItem : GlossaryItem
    {
        private string _label;
        public string Label
        {
            get
            {   if (_label == null)
                {
                    this._label = this.origin.getTaggedValue("label")?.eaStringValue;
                }
                return _label;
            }
            set { this._label = value; }
        }

        public string LogicalDatatypeName
        {
            get { return this.logicalDatatype?.name; }
        }

        private int? _size;
        public int? Size
        {
            get
            {
                if (!this._size.HasValue)
                {
                    int newSize;
                    if (int.TryParse(this.origin.getTaggedValue("size")?.eaStringValue, out newSize))
                        this._size = newSize;
                }
                return _size;
            }
            set { this._size = value; }
        }

        private string _format;
        public string Format
        {
            get
            {
                if (this._format == null)
                {
                    this._format = this.origin.getTaggedValue("format")?.eaStringValue;
                }
                return this._format;
            }
            set { this._format = value; }
        }

        public string Description
        {
            get { return this.origin.notes; }
            set { this.origin.notes = value; }
        }
        private string _initialValue;
        public string InitialValue
        {
            get
            {
                if (this._initialValue == null)
                {
                    this._initialValue = this.origin.getTaggedValue("initial value")?.eaStringValue;
                }
                return this._initialValue;
            }
            set { this._initialValue = value; }
        }

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

        protected override void update()
        {
            //tagged values cannot be held in memory and are always saved immediately to the database
            this.origin.addTaggedValue("label", this.Label);
            this.origin.addTaggedValue("initial value", this.InitialValue);
            this.origin.addTaggedValue("size", this.Size.HasValue ? this.Size.Value.ToString(): string.Empty);
            this.origin.addTaggedValue("format", this.Format);
        }

        protected override void reloadData()
        {
            this._label = null;
            this._initialValue = null; 
            this._size = null; 
            this._format = null;
            this._logicalDatatype = null;
            this._businessItem = null;
        }

        private LogicalDatatype _logicalDatatype;
        public LogicalDatatype logicalDatatype
        {
            get
            {
                if (this._logicalDatatype == null)
                {
                    var tv = this.origin.getTaggedValue("logical datatype");
                    if (tv != null)
                    {
                        var datatype = tv.tagValue as UML.Classes.Kernel.DataType;
                        if (datatype != null)
                            this._logicalDatatype = new LogicalDatatype(datatype);
                    } 
                }
                return this._logicalDatatype;
             }
            set
            {
                this._logicalDatatype = value;
                this.origin.addTaggedValue("logical datatype", value.GUID);
            }
        }
        private BusinessItem _businessItem;
        public BusinessItem businessItem
        {
            get
            {
                if (this._businessItem == null)
                {
                    var tv = this.origin.getTaggedValue("business item");
                    if (tv != null)
                    {
                        var businessItemClass = tv.tagValue as UML.Classes.Kernel.Class;
                        if (businessItemClass != null)
                        {
                            this._businessItem = new BusinessItem();
                            this._businessItem.origin = (TSF_EA.Class)businessItemClass;
                        }
                    }
                }
                return this._businessItem;

            }
            set
            {
                this._businessItem = value;
            }
        }

        // EA support

        public override string Stereotype { get { return "EDD_DataItem"; } }



    }
}
