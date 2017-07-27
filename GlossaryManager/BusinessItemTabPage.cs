using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace GlossaryManager {

	public class BusinessItemTabPage : GlossaryItemTabPage {

    public BusinessItemTabPage(GlossaryManagerUI ui) : base(ui) {
      this.Text = "Business Items";
    }

    protected override List<string> listHeaders {
      get {
        return new List<string>() {
          "Name", "Description", "Version", "Status", "Updated"
        };
      }
    }

    protected override List<string> AsListItemData(GlossaryItem item) {
      BusinessItem bi = item as BusinessItem;
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
      this.addField(new Field("Description") {
        Multiline = true,
        Width     = 300,
        Height    = 100
      });
      this.addField(new Field("Domain"));
    }

    protected override void show(object sender, EventArgs e) {
      base.show(sender, e);
      if( ! this.HasItemSelected ) { return; }

      this.fields["Description"].Value = ((BusinessItem)this.Current).Description;
      this.fields["Domain"].Value      = ((BusinessItem)this.Current).Domain;
    }

    protected override void Update(Field field) {
      if( ! this.HasItemSelected ) { return; }
      switch(field.Label.Text) {
        case "Description": ((BusinessItem)this.Current).Description = field.Value; break;
        case "Domain":      ((BusinessItem)this.Current).Domain      = field.Value; break;        
      }
      base.Update(field);
    }

    internal override void addButtonClick(object sender, EventArgs e) {
      // create new BI in package
      BusinessItem item = new BusinessItem() { Name = "New Business Item" };
      item.AsClassIn(this.ui.Addin.managedPackage);
      // add new BI to ListView
      this.add<BusinessItem>(item);
      // select it for editing
      this.select(item);
      // focus Name field and select all text
      ((TextBox)this.fields["Name"].Control).SelectAll();
      this.fields["Name"].Control.Focus();
    }

    internal override void deleteButtonClick(object sender, EventArgs e) {
      if( ! this.HasItemSelected ) { return; }
      BusinessItem item = (BusinessItem)this.Current;
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
      this.remove<BusinessItem>(item);
    }

    internal override void exportButtonClick(object sender, EventArgs e) {
      // TODO collect selected BusinessItems
      // List<BusinessItem> list = new List<BusinessItem>();
      // this.ui.Addin.export<BusinessItem>(list);
    }

    internal override void importButtonClick(object sender, EventArgs e) {
      this.ui.Addin.import<BusinessItem>(this.ui.Addin.managedPackage);
    }

  }
}
