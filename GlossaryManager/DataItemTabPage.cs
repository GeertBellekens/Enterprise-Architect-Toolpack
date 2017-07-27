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

    public List<FieldValue> LogicalDataTypes {
      set {
        this.logicalDataTypesComboBox.DataSource = value;
      }
    }

    private Field logicalDataTypesComboBox;

    protected override void createForm() {
      base.createForm();
      
      this.addField(new Field("Label"));
      this.logicalDataTypesComboBox = this.addField(new Field(
        "Logical Datatype", FieldOptions.WithNull | FieldOptions.WithPicker, this
      ));
      this.addField(new Field("Size"));
      this.addField(new Field("Format"));
      this.addField(new Field("Description") {
        Multiline = true,
        Width     = 300,
        Height    = 100
      });
      this.addField(new Field("Initial Value"));
    }

    protected override void show() {
      base.show();
      if( ! this.HasItemSelected ) { return; }

      this.fields["Label"].Value            = ((DataItem)this.Current).Label;
      this.fields["Logical Datatype"].Value = ((DataItem)this.Current).LogicalDataType.ToString();
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

    internal override void addButtonClick(object sender, EventArgs e) {
      // create new BI in package
      DataItem item = new DataItem() { Name = "New Data Item" };
      item.AsClassIn(this.ui.Addin.managedPackage);
      // add new BI to ListView
      this.add<DataItem>(item);
      // select it for editing
      this.select(item);
      // focus Name field and select all text
      ((TextBox)this.fields["Name"].Control).SelectAll();
      this.fields["Name"].Control.Focus();
    }

    internal override void deleteButtonClick(object sender, EventArgs e) {
      if( ! this.HasItemSelected ) { return; }
      DataItem item = (DataItem)this.Current;
      var answer = MessageBox.Show( "Are you sure to delete " + item.Name,
                                    "Confirm Delete!!",
                                    MessageBoxButtons.YesNo);
      if( answer != DialogResult.Yes) { return; }
      // delete from model
      // TODO this seems to fail for newly create items. the loop in this method
      //      doesn't seem to see the newly create item in the package, although
      //      it _is_ visible in the project browser?!
      //      closing the UI and reopening it, allows for deletion to work?!
      //      some sort of refresh is needed, but I didn't find it (CVG)
      this.ui.Addin.managedPackage.deleteOwnedElement(item.Origin);
      // remove from ListView
      this.remove<DataItem>(item);
    }

    internal override void exportButtonClick(object sender, EventArgs e) {
      // TODO collect selected DataItems
      // List<DataItem> list = new List<DataItem>();
      // this.ui.Addin.export<DataItem>(list);
    }

    internal override void importButtonClick(object sender, EventArgs e) {
      this.ui.Addin.import<DataItem>(this.ui.Addin.managedPackage);
    }


  }
}
