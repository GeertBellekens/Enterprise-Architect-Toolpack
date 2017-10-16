using System;
using System.Collections.Generic;
using System.Linq;

using FileHelpers;

using EAWrapped=TSF.UmlToolingFramework.Wrappers.EA;
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

      // base GlossaryItem
      item.Name        = item.Origin.name;
      item.Author      = item.Origin.author;
      item.Version     = item.Origin.version;
      item.Status      = (Status) Enum.Parse(typeof(Status), item.Origin.status);
      item.Keywords    = item.Origin.keywords;
      item.CreateDate  = item.Origin.created;
      item.UpdateDate  = item.Origin.modified;
      item.UpdatedBy   = item.Origin.modifier;

      if( typeof(T).Name == "BusinessItem" ) {
        ((BusinessItem)item).Description = item.Origin.notes;
        ((BusinessItem)item).Domain      = item.Origin.domain;
      } else if( typeof(T).Name == "DataItem" ){
        ((DataItem)item).Label           = item.Origin.label;
        ((DataItem)item).LogicalDataType = item.Origin.type;
        ((DataItem)item).Size            = Convert.ToInt32(item.Origin.size);
        ((DataItem)item).Format          = item.Origin.format;
        ((DataItem)item).Description     = item.Origin.notes;
        ((DataItem)item).InitialValue    = item.Origin.initialValue;
      } else {
        return null; // shouldn't happen, tested before ;-)
      }

      return (T)item;
    }

    private static T CreateFrom(UML.Classes.Kernel.Class clazz) {
      GlossaryItem item;
      if( typeof(T).Name == "BusinessItem" ) {
        item = new BusinessItem();
      } else if( typeof(T).Name == "DataItem" ) {
        item = new DataItem();
      } else {
        return null;
      }

      if( ! clazz.stereotypes.Any(x => x.name.Equals(item.Stereotype)) ) 
      {
        return null;
      }

      item.Origin = clazz as EAWrapped.ElementWrapper;
      return (T)item;
    }

  }
}
