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
    [FieldNullValue(typeof(string), "")]
    public string GUID;

    [FieldOrder(1)]
    [FieldConverter(typeof(BoolConverter))]
    public bool Delete;

    [FieldOrder(2)]
		public string Name;

    [FieldOrder(3)]
    [FieldNullValue(typeof(string), "")]
    public string Author;

    [FieldOrder(4)]
    [FieldNullValue(typeof(string), "")]
    public string Version;

    [FieldOrder(5)]
    [FieldNullValue(typeof(Status), "Approved")]
    public Status Status;

    [FieldOrder(6)]
    [FieldConverter(typeof(StringListConverter))]
    public List<string> Keywords;

    [FieldOrder(7)]
    [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
    [FieldNullValue(typeof(DateTime), "1900-01-01")]
    public DateTime CreateDate;

    [FieldOrder(8)]
    [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
    [FieldNullValue(typeof(DateTime), "1900-01-01")]
    public DateTime UpdateDate;

    [FieldOrder(9)]
    [FieldNullValue(typeof(string), "")]
    public string UpdatedBy;    


    [FieldValueDiscarded]
    [FieldHidden]
    private EAWrapped.ElementWrapper origin = null;
    public EAWrapped.ElementWrapper Origin {
      get {
        return this.origin;
      }
      set {
        this.origin = value;
        this.GUID = this.origin.guid;
      }
    }

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
        this.GUID,
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
      eaClass.name     = this.Name;
      eaClass.author   = this.Author;
      eaClass.version  = this.Version;
      eaClass.status   = this.Status.ToString();
      eaClass.keywords = this.Keywords;
      eaClass.created  = this.CreateDate;
      eaClass.modified = this.UpdateDate;
      eaClass.modifier = this.UpdatedBy;
      
      eaClass.save();
    }

    public void SelectInProjectBrowser() {
      this.Origin.model.selectedItem = this.Origin;
    }

  }
}
