using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAValidator
{
    public interface CheckItem
    {
        string name { get; }
        bool? selected { get; set; }
    }
}
