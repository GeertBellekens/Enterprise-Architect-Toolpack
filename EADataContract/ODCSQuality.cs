using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public class ODCSQuality:ODCSItem
    {
        public ODCSQuality() { }
        public ODCSQuality(YamlNode node):base(node)
        {
        }
    }
}