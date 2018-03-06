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
    class EDDColumn: IEDDItem
    {

        public EDDColumn(DB_EA.Column wrappedColumn, GlossaryManagerSettings settings)
        {
            this._wrappedColumn = wrappedColumn;
            this.settings = settings;
        }
        public static List<EDDColumn> createColumns(TSF_EA.Model model, List<DataItem> dataItems, GlossaryManagerSettings settings)
        {
            //get the columns based on the given data items
            var guidString = string.Join(",", dataItems.Select(x => "'" + x.GUID + "'"));
            string sqlGetColumns = @"select a.[ea_guid] from(t_attribute a
                                    inner join[t_attributetag] tv on(tv.[ElementID] = a.[ID]
                                                                      and tv.[Property] = 'EDD::dataitem'))
                                    where tv.VALUE in (" + guidString + ")";
            List<TSF_EA.Attribute> attributes = model.getAttributesByQuery(sqlGetColumns);
            var columns = new List<EDDColumn>();
            foreach (var attribute in attributes)
            {
                //for each attribute create the column
                var dbColumn = DB_EA.DatabaseFactory.createColumn(attribute);
                if (dbColumn != null) columns.Add(new EDDColumn(dbColumn, settings));
            }
            //return colums
            return columns;
        }
        private DB_EA.Column _wrappedColumn;
        private GlossaryManagerSettings settings { get; set; }
        public DatabaseFramework.Column column { get { return this._wrappedColumn; } }
        public string name { get { return this.column.name; } }
        public string tableName { get { return (string.IsNullOrEmpty(this.databaseName) ? 
                                                    string.Empty : 
                                                    this.databaseName + ".") 
                                                + this.column.ownerTable.name; } }
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
                    var tv = this._wrappedColumn.wrappedattribute.getTaggedValue("EDD::dataitem");
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
                this._dataItem = value;
                this.dataItemLoaded = true;
            }
        }

        public void selectInProjectBrowser()
        {
            this.column.Select();
        }

        public void openProperties()
        {
            ((DB_EA.Column)this.column).wrappedattribute.openProperties();
        }
        public void save()
        {
            this.column.save();
            this._wrappedColumn.wrappedattribute.addTaggedValue("EDD::dataitem", this.dataItem?.GUID);
        }

        public DataItem selectDataItem()
        {
            //let the user select a logical datatype
            var dataItemclass = this._wrappedColumn.wrappedattribute.model.getUserSelectedElement(new List<string>() { "Class" },
                new List<string>() {new DataItem().Stereotype }) as UML.Classes.Kernel.Class;
            if (dataItemclass != null)
                return new GlossaryItemFactory(this.settings).FromClass<DataItem>(dataItemclass);
            else
                return null;
        }
    }
}
