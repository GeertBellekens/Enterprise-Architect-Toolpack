using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB_EA = EAAddinFramework.Databases;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace GlossaryManager
{
    public class EDDColumn: IEDDItem
    {
        public EDDTable table { get; private set; }
        public EDDColumn(DB_EA.Column wrappedColumn,EDDTable table, GlossaryManagerSettings settings)
        {
            this._wrappedColumn = wrappedColumn;
            this.table = table;
            this.settings = settings;
        }
        public static List<EDDTable> createColumns(TSF_EA.Model model, List<DataItem> dataItems, GlossaryManagerSettings settings)
        {
            //get the columns based on the given data items
            var guidString = string.Join(",", dataItems.Select(x => "'" + x.GUID + "'"));
            string sqlGetColumns = @"select a.[ea_guid] from(t_attribute a
                                    inner join[t_attributetag] tv on(tv.[ElementID] = a.[ID]
                                                                      and tv.[Property] = 'EDD::dataitem'))
                                    where tv.VALUE in (" + guidString + ")";
            List<TSF_EA.Attribute> attributes = model.getAttributesByQuery(sqlGetColumns);
            var tables = new Dictionary<string,EDDTable>();
            foreach (var attribute in attributes)
            {
                var classElement = attribute.owner as TSF_EA.Class;
                if (classElement != null)
                {
                    //for each attribute create the column
                    var dbColumn = DB_EA.DatabaseFactory.createColumn(attribute);
                    if (dbColumn != null)
                    {
                        EDDTable ownerTable = tables.ContainsKey(classElement.uniqueID) ?
                                              tables[classElement.uniqueID]
                                              : new EDDTable((DB_EA.Table)dbColumn.ownerTable, settings);
                        var newColumn = new EDDColumn(dbColumn, ownerTable, settings);
                        ownerTable.addColumn(newColumn);
                        tables[ownerTable.uniqueID] = ownerTable;
                    }
                }  
            }
            //return colums
            return tables.Values.ToList();
        }
        private DB_EA.Column _wrappedColumn;
        private GlossaryManagerSettings settings { get; set; }
        public DatabaseFramework.Column column { get { return this._wrappedColumn; } }
        public string name { get { return this.column.name; } }
        public string tableName { get {return this.column.ownerTable.name; } }
        public string databaseName { get { return this.column.ownerTable.owner?.name; } }
        public string properties {  get { return this.column.properties; } }
        private bool dataItemLoaded = false;
        private DataItem _dataItem;
        public DataItem dataItem
        {
            get
            {
                if (!this.dataItemLoaded)
                {
                    var tv = this._wrappedColumn.wrappedattribute?.getTaggedValue("EDD::dataitem");
                    if (tv != null)
                    {
                        var dataItemClass = tv.tagValue as TSF_EA.ElementWrapper;
                        if (dataItemClass != null)
                        {
                            this._dataItem = new DataItem();
                            this._dataItem.origin = dataItemClass;
                        }
                    }
                    this.dataItemLoaded = true;
                }
                return this._dataItem;
            }
            set
            {
                var newdataItem = value;
                this._dataItem = newdataItem;
                this.dataItemLoaded = true;
            }
        }
        public void setDataItemDefaults()
        {
            if (this._dataItem != null)
            {
                this.column.name = this._dataItem.Label;
                this.column.type = new DB_EA.DataType((DB_EA.BaseDataType)this._dataItem.logicalDatatype.getBaseDatatype(this.column.factory.databaseName)
                                        , this._dataItem.getSize(), this._dataItem.getPrecision());
                this.column.initialValue = this._dataItem.InitialValue;
            }
        }

        public void selectInProjectBrowser()
        {
            this.column.Select();
        }

        public void openProperties()
        {
            this._wrappedColumn?.wrappedattribute?.openProperties();
        }
        public void save()
        {
            this.column.save();
            this._wrappedColumn.wrappedattribute.addTaggedValue("EDD::dataitem", this.dataItem?.GUID);
        }
        public void reload()
        {
            this._wrappedColumn?.reload();
            this._dataItem = null;
            this.dataItemLoaded = false;
        }

        public DataItem selectDataItem()
        {
            //let the user select a logical datatype
            var defaultSelectionID = this.dataItem == null ?
                                   this.settings.dataItemsPackage.uniqueID :
                                   this.dataItem.GUID;
            var dataItemclass = this.table.model.getUserSelectedElement(new List<string>() { "Class" }
                ,new List<string>() {new DataItem().Stereotype }
                , defaultSelectionID) as UML.Classes.Kernel.Class;
            if (dataItemclass != null)
                return GlossaryItemFactory.getFactory(this.table.model, this.settings).FromClass<DataItem>(dataItemclass);
            else
                return null;
        }
    }
}
