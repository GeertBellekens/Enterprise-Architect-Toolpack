using System;
using System.Collections.Generic;
using System.Linq;

using FileHelpers;

using EAWrapped=TSF.UmlToolingFramework.Wrappers.EA;
using UML=TSF.UmlToolingFramework.UML;

namespace GlossaryManager {

	[DelimitedRecord(";"), IgnoreFirst(1)]
	public class DataItem	: GlossaryItem {

    [FieldOrder(100)]
    [FieldNullValue(typeof(string), "")]
		public string Label;

    [FieldOrder(101)]
    [FieldNullValue(typeof(string), "")]
		public string LogicalDataType = "";

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
}
