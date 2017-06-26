using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace GlossaryManager {

	public class DataItemTabPage : GlossaryItemTabPage {

    public DataItemTabPage(GlossaryManagerUI ui) : base(ui) {
      this.Text = "Data Items";
    }

    protected override List<string> listHeaders {
      get {
        return new List<string>() {
          "Name", "Description", "Version", "Status", "Updated"
        };
      }
    }

    protected override List<string> AsListItemData(GlossaryItem item) {
      DataItem bi = item as DataItem;
      return new List<string>() {
        bi.Name,
        bi.Description,
        bi.Version,
        bi.Status.ToString(),
        bi.UpdateDate.ToString()
      };
    }

    protected override void createForm() {
      base.createForm();
      this.addField(new Field("Label"));
      this.addField(new Field("Logical Datatype"));
      this.addField(new Field("Size"));
      this.addField(new Field("Format"));
      this.addField(new Field("Description") {
        Multiline = true,
        Width     = 300,
        Height    = 100
      });
      this.addField(new Field("Initial Value"));
    }

    protected override void show(object sender, EventArgs e) {
      base.show(sender, e);
      if( ! this.HasItemSelected ) { return; }

      this.fields["Label"].Value            = ((DataItem)this.Current).Label;
      this.fields["Logical Datatype"].Value = ((DataItem)this.Current).LogicalDataType;
      this.fields["Size"].Value             = ((DataItem)this.Current).Size.ToString();
      this.fields["Format"].Value           = ((DataItem)this.Current).Format;
      this.fields["Description"].Value      = ((DataItem)this.Current).Description;
      this.fields["Initial Value"].Value    = ((DataItem)this.Current).InitialValue;
    }

    protected override void Update(Field field) {
      if( ! this.HasItemSelected ) { return; }
      switch(field.Label.Text) {
        case "Label":            ((DataItem)this.Current).Label           = field.Value; break;
        case "Logical Datatype": ((DataItem)this.Current).LogicalDataType = field.Value; break;
        case "Size":             ((DataItem)this.Current).Size            = Convert.ToInt32(field.Value); break;
        case "Format":           ((DataItem)this.Current).Format          = field.Value; break;
        case "Description":      ((DataItem)this.Current).Description     = field.Value; break;
        case "Initial Value":    ((DataItem)this.Current).InitialValue    = field.Value; break;        
      }
      base.Update(field);
    }

  }
}
