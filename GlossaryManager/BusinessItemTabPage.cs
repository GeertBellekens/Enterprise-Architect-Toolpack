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


  }

}