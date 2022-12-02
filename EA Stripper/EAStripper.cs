using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EA_Stripper
{
    internal class EAStripper
    {
        public void strip()
        {
            var model = new TSF_EA.Model();
            foreach (var package in model.rootPackages)
            {
                foreach(var subPackage in package.nestedPackages)
                {
                    subPackage.delete();
                }
            }
        }
    }
}
