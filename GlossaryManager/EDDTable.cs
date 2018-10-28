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
    public class EDDTable : IEDDItem
    {
        public DB_EA.Table wrappedTable { get { return this._wrappedTable; } }
        private DB_EA.Table _wrappedTable;
        private GlossaryManagerSettings settings;
        public TSF_EA.Model model => this._wrappedTable.wrappedClass.EAModel;
        public EDDTable(DB_EA.Table wrappedTable, GlossaryManagerSettings settings)
        {
            this._wrappedTable = wrappedTable;
            this.settings = settings;
        }
        public Boolean showAllColumns { get; set; }
        List<EDDColumn> _colums = new List<EDDColumn>();
        public List<EDDColumn> columns
        {
            get
            {
                return this._colums;
            }
        }
        public string name { get { return this.wrappedTable.name; } }
        public void addColumn(EDDColumn column)
        {
            this.columns.Add(column);
        }
        public EDDColumn addNewColumn(string name)
        {
            var newWrappedColumn = this.wrappedTable.addNewColumn(name);
            var newColumn = new EDDColumn(newWrappedColumn, this, this.settings);
            this.addColumn(newColumn);
            return newColumn;
        }
        public void loadAllColumns()
        {
            this.columns.Clear();
            foreach (var column in this.wrappedTable.columns)
            {
                this.addColumn(new EDDColumn((DB_EA.Column)column,this, this.settings));
            }
        }
        public string uniqueID
        {
            get
            {
                return this.wrappedTable.uniqueID;
            }
        }
        public void selectInProjectBrowser()
        {
            this.wrappedTable.Select();
        }

        public void openProperties()
        {
            this.wrappedTable.openProperties();
        }

        public void save()
        {
            this.wrappedTable.save();
        }
        //let the user select a table from the model
        public static EDDTable selectTable(TSF_EA.Model model, GlossaryManagerSettings settings)
        {
            var tableElement = model.getUserSelectedElement(new List<string> { "Class" }, new List<string> { "table" }) as TSF_EA.Class;
            if (tableElement == null) return null;
            var table = DB_EA.DatabaseFactory.createTable(tableElement);
            if (table == null) return null;
            return new EDDTable(table, settings);
        }

    }
}
