using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryManager
{
    public interface IEDDItem
    {
        void selectInProjectBrowser();
        void openProperties();
        void save();
    }
}
