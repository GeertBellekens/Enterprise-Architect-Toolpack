using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryManager
{
    public class GlossaryItemSearchCriteria
    {
        public string nameSearchTerm { get; set; }
        public string descriptionSearchTerm { get; set; }
        public bool showAll { get; set; }
    }
}
