using System;
using System.Collections.Generic;
using System.Linq;

using FileHelpers;

using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using UML=TSF.UmlToolingFramework.UML;

namespace GlossaryManager {
  
  public class GlossaryItemFactory<T> where T : GlossaryItem {

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

    private static T CreateFrom(UML.Classes.Kernel.Class clazz) {
      GlossaryItem item;
      Type TType = typeof(T);
      if( TType == typeof(BusinessItem) ) 
      {
        item = new BusinessItem();
      } 
      else if( TType == typeof(DataItem) )
      {
        item = new DataItem();
      } 
      else
      {
        return null;
      }
      if( ! clazz.stereotypes.Any(x => x.name.Equals(item.Stereotype)) ) 
      {
        return null;
      }

      item.Origin = clazz as TSF_EA.ElementWrapper;
      return (T)item;
    }

  }
}
