using System;
using System.Collections.Generic;
using System.Linq;

using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace GlossaryManager
{

    public class GlossaryItemFactory 
    {
        GlossaryManagerSettings settings { get; set; }
        public GlossaryItemFactory(GlossaryManagerSettings settings)
        {
            this.settings = settings;
        }

        public bool IsA<T>(UML.Classes.Kernel.Class clazz) where T : GlossaryItem, new()
        {
            return this.CreateFrom<T>(clazz) != null;
        }

        public T FromClass<T>(UML.Classes.Kernel.Class clazz) where T : GlossaryItem, new()
        {
            if (clazz == null) { return null; }
            if (clazz.stereotypes.Count != 1) { return null; }

            GlossaryItem item = this.CreateFrom<T>(clazz);
            if (item == null) { return null; }

            return (T)item;
        }

        public List<T> getGlossaryItemsFromPackage<T>(TSF_EA.Package package) where T : GlossaryItem, new()
        {
            T dummy = new T(); // needed to get the stereotype
            var glossaryItems = new List<T>();
            if (package != null)
            {
                foreach (var classElement in package.getOwnedElementWrappers<TSF_EA.Class>(dummy.Stereotype, true))
                {
                    var item = this.CreateFrom<T>(classElement);
                    if (item != null) glossaryItems.Add(item);
                }
            }
            return glossaryItems;

        }

        private T CreateFrom<T>(UML.Classes.Kernel.Class clazz) where T : GlossaryItem, new()
        {
            T item = new T();
            if (!clazz.stereotypes.Any(x => x.name.Equals(item.Stereotype)))
            {
                return null;
            }
            item.settings = this.settings;
            item.Origin = clazz as TSF_EA.ElementWrapper;
            return item;
        }

    }
}
