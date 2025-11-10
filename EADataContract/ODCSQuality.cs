using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public class ODCSQuality:ODCSItem
    {
        public ODCSQuality() { }
        public ODCSQuality(YamlNode node, ODCSElement owner):base(node, owner)
        {
        }

        public override IEnumerable<ODCSItem> getChildItems()
        {
            return new List<ODCSItem>(); //no child items
        }

        public override void getModelElement(Element context)
        {
            //TODO: implement
        }

        public override void updateModelElement()
        {
            //TODO: implement
        }
    }
}