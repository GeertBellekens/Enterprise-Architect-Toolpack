using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



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
            {
                if (_label == null)
                {
                    this._label = this.origin.getTaggedValue("label")?.eaStringValue;
                }
                return _label;
            }
            set { this._label = value; }
        }

        public string datatypeDisplayName
        {
            get
            {
                string suffix = Size.HasValue && Size.Value > 0 ? "(" + Size.ToString() : string.Empty;
                if (! string.IsNullOrEmpty(suffix))
                {
                    suffix += this.Precision.HasValue && this.Precision.Value > 0 ? "," + this.Precision.ToString() : string.Empty;
                    suffix += ")";
                }
                return this.logicalDatatype?.name + suffix;
            }
        }
        private bool sizeLoaded = false;
        private int? _size;
        public int? Size
        {
            get
            {
                if (!this.sizeLoaded)
                {
                    int newSize;
                    if (int.TryParse(this.origin.getTaggedValue("size")?.eaStringValue, out newSize))
                    {
                        this._size = newSize;
                    }
                }
                this.sizeLoaded = true;
                return _size;
            }
            set
            {
                this._size = value;
                this.sizeLoaded = true;
            }
        }
        private bool precisionLoaded = false;
        private int? _precision;
        public int? Precision
        {
            get
            {
                if (!this.precisionLoaded)
                {
                    int newprecision;
                    if (int.TryParse(this.origin.getTaggedValue("precision")?.eaStringValue, out newprecision))
                        this._precision = newprecision;
                }
                this.precisionLoaded = true;
                return _precision;
            }
            set
            {
                this._precision = value;
                this.precisionLoaded = true;
            }
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
          this.datatypeDisplayName,
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
            this.origin.addTaggedValue("size", this.Size.HasValue ? this.Size.Value.ToString() : string.Empty);
            this.origin.addTaggedValue("precision", this.Precision.HasValue ? this.Precision.Value.ToString() : string.Empty);
            this.origin.addTaggedValue("format", this.Format);
            this.origin.addTaggedValue("logical datatype", this.logicalDatatype?.GUID);
            this.origin.addTaggedValue("business item", this.businessItem?.GUID);
        }

        internal int getPrecision()
        {
            return this.Precision.HasValue ? this.Precision.Value : 0;
        }

        internal int getSize()
        {
            return this.Size.HasValue ? this.Size.Value : 0;
        }

        protected override void reloadData()
        {
            this._label = null;
            this._initialValue = null;
            this._size = null;
            this._format = null;
            this._logicalDatatype = null;
            this.logicalDatatypeLoaded = false;
            this._businessItem = null;
            this.businessItemLoaded = false;
        }

        private bool logicalDatatypeLoaded = false;
        private LogicalDatatype _logicalDatatype;
        public LogicalDatatype logicalDatatype
        {
            get
            {
                if (!this.logicalDatatypeLoaded)
                {
                    var tv = this.origin.getTaggedValue("logical datatype");
                    if (tv != null)
                    {
                        var datatype = tv.tagValue as UML.Classes.Kernel.DataType;
                        if (datatype != null)
                            this._logicalDatatype = new LogicalDatatype(datatype);
                    }
                    this.logicalDatatypeLoaded = true;
                }
                return this._logicalDatatype;
            }
            set
            {
                this._logicalDatatype = value;
                this.logicalDatatypeLoaded = true;
            }
        }
        private bool businessItemLoaded = false;
        private BusinessItem _businessItem;
        public BusinessItem businessItem
        {
            get
            {
                if (!this.businessItemLoaded)
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
                    this.businessItemLoaded = true;
                }
                return this._businessItem;

            }
            set
            {
                this._businessItem = value;
                this.businessItemLoaded = true;
            }
        }
        public string businessItemName
        {
            get
            {
                return this.businessItem != null && String.IsNullOrWhiteSpace(this.businessItem.Name) ?
                 "<empty>" :
                 this.businessItem?.Name;
            }
        }
        public override string Stereotype { get { return "EDD_DataItem"; } }

        /// <summary>
        /// allows the user to select a business item and return it
        /// </summary>
        /// <returns>the business item selected by the user</returns>
        internal BusinessItem selectBusinessItem()
        {
            //let the user select a business item
            var businessItemOrigin = this.origin.model.getUserSelectedElement(new List<string>() { "Class" }
                                    , new List<string>() { new BusinessItem().Stereotype }
                                    , this.businessItem?.GUID) as UML.Classes.Kernel.Class;
            return GlossaryItemFactory.getFactory(this.origin.EAModel, this.settings).FromClass<BusinessItem>(businessItemOrigin);
        }
        protected override void setOwningPackage()
        {
            if (this.domain.dataItemsPackage == null) domain.createMissingPackage();
            this.origin.owningPackage = this.domain.dataItemsPackage;
        }
        /// <summary>
        /// allows the user to select a logical datatype
        /// </summary>
        /// <returns>the logical datatype selected by the user</returns>
        internal LogicalDatatype selectLogicalDataType()
        {
            //let the user select a logical datatype
            var dataType = this.origin.model.getUserSelectedElement(new List<string>() { "DataType" }
                                    ,new List<string>() { LogicalDatatype.stereoType }
                                    ,this.logicalDatatype?.GUID) as UML.Classes.Kernel.DataType;
            if (dataType != null)
                return new LogicalDatatype(dataType);
            else
                return null;
        }

        internal IEnumerable<EDDTable> getLinkedColumns()
        {
            return EDDColumn.createColumns(this.origin.EAModel, new List<DataItem> { this }, this.settings);
        }
    }
}
