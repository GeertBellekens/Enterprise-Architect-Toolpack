using System;
using System.Collections.Generic;
using System.Linq;

using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace GlossaryManager
{

    public class GlossaryItemFactory
    {
        static Dictionary<string, GlossaryItemFactory> factories = new Dictionary<string, GlossaryItemFactory>();
        GlossaryManagerSettings settings { get; set; }
        public TSF_EA.Model model { get; private set; }
        public static GlossaryItemFactory getFactory(TSF_EA.Model model, GlossaryManagerSettings settings)
        {
            GlossaryItemFactory factory;
            if (! factories.TryGetValue(model.projectGUID, out factory))
            {
                factory = new GlossaryItemFactory(model, settings);
                factories.Add(model.projectGUID, factory);
            }
            return factory;
        }
        private GlossaryItemFactory(TSF_EA.Model model, GlossaryManagerSettings settings)
        {
            this.settings = settings;
            this.model = model;
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
            var sqlTop = criteria.showAll ? string.Empty : "top 50";
            var nameClause = string.IsNullOrEmpty(criteria.nameSearchTerm) ? 
                $"(o.[Name] like '%{criteria.nameSearchTerm}%' or o.[Name] is null )"
                : $"o.[Name] like '%{criteria.nameSearchTerm}%'";
            var descriptionClause = string.IsNullOrEmpty(criteria.descriptionSearchTerm) ?
                $"(o.[Note] like '%{criteria.descriptionSearchTerm}%' or o.[Note] is null )"
                : $"o.[Note] like '%{criteria.descriptionSearchTerm}%'";
            string sqlGetGlossaryItems = $"select {sqlTop} o.[Object_ID] from t_object o"
                                        + $" where {nameClause} "
                                        + $" and {descriptionClause} "
                                        + $" and o.[Stereotype] = '{dummy.Stereotype}' "
                                        + $" and o.[Package_ID] in ({ package.packageTreeIDString})";
            return this.getGlossaryItemsFromQuery<T>(sqlGetGlossaryItems);
        }
        public List<T> getGlossaryItemsFromQuery<T>(string query) where T : GlossaryItem, new()
        {
            var glossaryItems = new List<T>();
            if (model != null)
            {
                foreach (var classElement in this.model.getElementWrappersByQuery(query).OfType<UML.Classes.Kernel.Class>())
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
