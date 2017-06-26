using System;
using System.Collections.Generic;
using System.Linq;

using FileHelpers;

using EAWrapped=TSF.UmlToolingFramework.Wrappers.EA;
using UML=TSF.UmlToolingFramework.UML;

namespace GlossaryManager {
  
  public enum Status {
    Proposed,
    Approved,
    Rejected
  }

  public class StringListConverter : ConverterBase {
    public override object StringToField(string from) {
      if(from  == null) {
        return new List<string>();
      }
      return from.Split(',').ToList();
    }

    public override string FieldToString(object fieldValue) {
      return string.Join(",", (List<string>)fieldValue);
    }

    // we want to handle null values
    protected override bool CustomNullHandling { get { return true; } }
  }

  // anything except nothing is true ;-)
  public class BoolConverter : ConverterBase {
    public override object StringToField(string delete) {
      if(delete != null && delete.Length > 0) { return true; }
      return false;
    }

    public override string FieldToString(object fieldValue) {
      return "";
    }

    // we want to handle null values
    protected override bool CustomNullHandling { get { return true; } }
  }
  
  public abstract class GlossaryItem {

    [FieldOrder(0)]
    [FieldConverter(typeof(BoolConverter))]
    public bool Delete;

    [FieldOrder(1)]
		public string Name;

    [FieldOrder(2)]
    [FieldNullValue(typeof(string), "")]
    public string Author;

    [FieldOrder(3)]
    [FieldNullValue(typeof(string), "")]
    public string Version;

    [FieldOrder(4)]
    [FieldNullValue(typeof(Status), "Approved")]
    public Status Status;

    [FieldOrder(5)]
    [FieldConverter(typeof(StringListConverter))]
    public List<string> Keywords;

    [FieldOrder(6)]
    [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
    [FieldNullValue(typeof(DateTime), "1900-01-01")]
    public DateTime CreateDate;

    [FieldOrder(7)]
    [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
    [FieldNullValue(typeof(DateTime), "1900-01-01")]
    public DateTime UpdateDate;

    [FieldOrder(8)]
    [FieldNullValue(typeof(string), "")]
    public string UpdatedBy;    

    public EAWrapped.ElementWrapper Origin { get; set; }

  	public static List<T> Load<T>(string file)
      where T : GlossaryItem
    {
      var engine = new FileHelperEngine<T>();
      engine.HeaderText = engine.GetFileHeader();
      return engine.ReadFile(file).ToList();
  	}

    public static void Save<T>(string file, List<T> items)
      where T : GlossaryItem
    {
      var engine = new FileHelperEngine<T>();
      engine.HeaderText = engine.GetFileHeader();
      engine.WriteFile(file, items);
    }

    public override string ToString() {
      return string.Join(", ", new List<string> {
        this.Name,
        this.Author,
        this.Version,
        this.Status.ToString(),
        "[" + string.Join(",", this.Keywords) + "]",
        this.CreateDate.ToString(),
        this.UpdateDate.ToString(),
        this.UpdatedBy
      });
    }

    // EA support

    public abstract string Stereotype { get; }

    public UML.Classes.Kernel.Class AsClassIn(EAWrapped.Package package) {
      var clazz = package.model.factory.createNewElement<UML.Classes.Kernel.Class>(
        package, this.Name
      );

      var stereotypes = new HashSet<UML.Profiles.Stereotype>();
      stereotypes.Add(new EAWrapped.Stereotype(
        package.model, clazz as EAWrapped.Element, this.Stereotype
      ));
      clazz.stereotypes = stereotypes;

      this.Update(clazz);
      this.Origin = clazz as EAWrapped.ElementWrapper;

      return clazz;
    }

    public void Save() {
      if( this.Origin == null ) {
        // TODO assertion ?
        //      Can't Save without an Origin and should not be possible?!
        return;
      }
      this.Update(this.Origin as UML.Classes.Kernel.Class);
    }

    public virtual void Update(UML.Classes.Kernel.Class clazz) {
      var eaClass = clazz as EAWrapped.ElementWrapper;
      eaClass.author   = this.Author;
      eaClass.version  = this.Version;
      eaClass.status   = this.Status.ToString();
      eaClass.keywords = this.Keywords;
      eaClass.created  = this.CreateDate;
      eaClass.modified = this.UpdateDate;
      eaClass.modifier = this.UpdatedBy;
      
      eaClass.save();
    }
  }

	[DelimitedRecord(";"), IgnoreFirst(1)]
	public class BusinessItem	: GlossaryItem {

    [FieldOrder(100)]
    [FieldNullValue(typeof(string), "")]
		public string Description;

    [FieldOrder(101)]
    [FieldNullValue(typeof(string), "")]
		public string Domain;

    public override string ToString() {
      return "BusinessItem(" + base.ToString() + "," +
        string.Join(", ", new List<string>() {
          this.Description,
          this.Domain
        }) +
      ")";
    }

    // EA support

    public override string Stereotype { get { return "Business Item"; } }
    
    public override void Update(UML.Classes.Kernel.Class clazz) {
      base.Update(clazz);

      var eaClass = clazz as EAWrapped.ElementWrapper;
      eaClass.notes  = this.Description;
      eaClass.domain = this.Domain;

      clazz.save();
    }
  }

	[DelimitedRecord(";"), IgnoreFirst(1)]
	public class DataItem	: GlossaryItem {

    [FieldOrder(100)]
    [FieldNullValue(typeof(string), "")]
		public string Label;

    [FieldOrder(101)]
    [FieldNullValue(typeof(string), "")]
		public string LogicalDataType;

    [FieldOrder(102)]
    [FieldNullValue(typeof(int), "0")]
		public int Size;

    [FieldOrder(103)]
    [FieldNullValue(typeof(string), "")]
		public string Format;

    [FieldOrder(104)]
    [FieldNullValue(typeof(string), "")]
		public string Description;

    [FieldOrder(105)]
    [FieldNullValue(typeof(string), "")]
		public string InitialValue;

    public override string ToString() {
      return "DataItem(" + base.ToString() + "," + 
        string.Join(", ", new List<string>() {
          this.Label,
          this.LogicalDataType,
          this.Size.ToString(),
          this.Format,
          this.Description,
          this.InitialValue
        }) +
      ")";
    }

    // EA support

    public override string Stereotype { get { return "Data Item"; } }

    public override void Update(UML.Classes.Kernel.Class clazz) {
      base.Update(clazz);

      var eaClass = clazz as EAWrapped.ElementWrapper;
      eaClass.label        = this.Label;
      eaClass.type         = this.LogicalDataType;
      eaClass.size         = this.Size.ToString();
      eaClass.format       = this.Format;
      eaClass.notes        = this.Description;
      eaClass.initialValue = this.InitialValue;

      clazz.save();
    }

	}

  public class GlossaryItemFactory<T> where T : GlossaryItem {

    private GlossaryItemFactory() {}

    public static bool IsA(UML.Classes.Kernel.Class clazz) {
      return GlossaryItemFactory<T>.CreateFrom(clazz) != null;
    }

    public static T FromClass(UML.Classes.Kernel.Class clazz) {
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

      if( ! clazz.stereotypes.ToList()[0].name.Equals(item.Stereotype) ) {
        return null;
      }

      item.Origin = clazz as EAWrapped.ElementWrapper;
      return (T)item;
    }
  }

}
