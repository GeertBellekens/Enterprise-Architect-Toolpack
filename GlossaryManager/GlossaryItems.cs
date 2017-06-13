using System;
using System.Collections.Generic;
using System.Linq;

using FileHelpers;

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
  
  public abstract class GlossaryItem {

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

    [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
    [FieldNullValue(typeof(DateTime), "1900-01-01")]
    [FieldOrder(6)]
    public DateTime CreateDate;

    [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
    [FieldNullValue(typeof(DateTime), "1900-01-01")]
    [FieldOrder(7)]
    public DateTime UpdateDate;

    [FieldOrder(8)]
    [FieldNullValue(typeof(string), "")]
    public string UpdatedBy;    

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
        "[" + string.Join(",", this.Keywords) + "]",
        this.CreateDate.ToString(),
        this.UpdateDate.ToString(),
        this.UpdatedBy
      });
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
	}
}
