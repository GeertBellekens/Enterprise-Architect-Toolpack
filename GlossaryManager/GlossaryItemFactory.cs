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

        public List<T> getGlossaryItemsFromPackage<T>(TSF_EA.Package package, GlossaryItemSearchCriteria criteria) where T : GlossaryItem, new()
        {
            T dummy = new T(); // needed to get the stereotype
            //build a search string
            string sqlGetGlossaryItems = string.Format(@"select top 50 o.[Object_ID] from t_object o
                                                        where o.[Name] like '%{0}%'
                                                        and o.[Note] like '%{1}%'
                                                        and o.[Stereotype] = '{2}'
                                                        and o.[Package_ID] in ({3})",
                                                        criteria.nameSearchTerm,
                                                        criteria.descriptionSearchTerm,
                                                        dummy.Stereotype,
                                                        package.getPackageTreeIDString());
             
            
            var glossaryItems = new List<T>();
            if (package != null)
            {
                foreach (var classElement in package.EAModel.getElementWrappersByQuery(sqlGetGlossaryItems).OfType<UML.Classes.Kernel.Class>())
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
            item.origin = clazz as TSF_EA.ElementWrapper;
            return item;
        }
        public T addNew<T>(UML.Classes.Kernel.Package ownerPackage) where T : GlossaryItem, new()
        {
            var wrappedClass = ((TSF_EA.Package)ownerPackage).addOwnedElement<TSF_EA.Class>(string.Empty);
            var stereotype = new T().Stereotype;
            wrappedClass.setStereotype(stereotype);
            wrappedClass.name = stereotype + "1";
            return CreateFrom<T>(wrappedClass);
        }

    }
}
