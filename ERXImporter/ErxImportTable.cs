using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERXImporter
{
    public class ErxImportTable
    {
        public string ddl { get; set; }
        public string imports { get; set; }
        public string tableName { get; set; }
        public ErxImportTable (string tableName, string ddl, string imports)
        {
            this.tableName = tableName;
            this.ddl = ddl;
            this.imports = imports;
        }
    }
}
