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
    class EDDColumn
    {
        public EDDColumn(DB_EA.Column wrappedColumn)
        {
            this._wrappedColumn = wrappedColumn;
        }
        private DB_EA.Column _wrappedColumn;
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
    }
}
