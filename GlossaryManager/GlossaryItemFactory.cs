using System;
using System.Collections.Generic;
using System.Linq;

using FileHelpers;

using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using UML=TSF.UmlToolingFramework.UML;

namespace GlossaryManager {
  
	public class GlossaryItemFactory<T> where T : GlossaryItem, new() {

    private GlossaryItemFactory() {}

    public static bool IsA(UML.Classes.Kernel.Class clazz) {
      return GlossaryItemFactory<T>.CreateFrom(clazz) != null;
    }

    public static T FromClass(UML.Classes.Kernel.Class clazz) {
      if( clazz == null )                { return null; }
      if( clazz.stereotypes.Count != 1 ) { return null; }

      GlossaryItem item = GlossaryItemFactory<T>.CreateFrom(clazz);
      if( item == null ) { return null; }
	
      return (T)item;
    }

    public static List<T> getGlossaryItemsFromPackage(TSF_EA.Package package)
    {
    	T dummy = new T(); // needed to get the stereotype
    	var glossaryItems = new List<T>();
    	if (package != null)
    	{
	    	foreach (var classElement in package.getOwnedElementWrappers<TSF_EA.Class>(dummy.Stereotype, true)) 
	    	{
	    		glossaryItems.Add(CreateFrom(classElement));
	    	}
    	}
    	return glossaryItems;
    	
    }

    private static T CreateFrom(UML.Classes.Kernel.Class clazz) 
    {
    	T item = new T();
		if( ! clazz.stereotypes.Any(x => x.name.Equals(item.Stereotype)) ) 
		{
			return null;
		}
		item.Origin = clazz as TSF_EA.ElementWrapper;
		return item;
    }

  }
}
