using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

  // a Field is a combination of a Label and a TextBox or ComboBox

namespace GlossaryManager {

  public class Field : FlowLayoutPanel {
    public Label    Label;
    public TextBox  TextBox;
    public ComboBox ComboBox;

    public Field(string label) : base() {
      this.createLabel(label);

      this.TextBox = new TextBox();
      this.TextBox.TextChanged += new EventHandler(this.change);


      this.Controls.Add(this.TextBox);
    }

    public Type SelectionType { get; private set; }

    public Field(string label, Type options) : base() {
      this.createLabel(label);

      this.ComboBox = new ComboBox() {
        DataSource = Enum.GetValues(options),
      };
      this.ComboBox.SelectedIndexChanged += new EventHandler(this.change);
      this.SelectionType = options;

      this.Controls.Add(this.ComboBox);
    }

  	public event Action<Field> ValueChanged = delegate { }; 
  	private void change(object sender, EventArgs e) {
  		this.ValueChanged(this);
  	}

    private void createLabel(string label) {
      this.AutoSize = true;

      this.Label  = new Label() {
        Text      = label,
        Anchor    = AnchorStyles.Left | AnchorStyles.Top ,
        TextAlign = ContentAlignment.MiddleLeft
      };
      this.Controls.Add(this.Label);
    }
  
    public Control Control {
      get {
        return this.TextBox != null ? (Control)this.TextBox : (Control)this.ComboBox;
      }
    }

    public new bool Enabled {
      get {
        return this.TextBox != null ? this.TextBox.Enabled : this.ComboBox.Enabled;
      }
      set {
        if( this.TextBox != null ) {
          this.TextBox.Enabled = value;
        } else {
          this.ComboBox.Enabled = value;
        }
      }
    }

    public bool Multiline {
      get {
        return this.TextBox != null ? this.TextBox.Multiline : false;
      }
      set {
        if( this.TextBox != null ) {
          if( value ) {
            this.TextBox.Multiline     = true;
            this.TextBox.ScrollBars    = ScrollBars.Vertical;
            this.TextBox.AcceptsReturn = true;
            this.TextBox.AcceptsTab    = true;
            this.TextBox.WordWrap      = true;
          } else {
            this.TextBox.Multiline     = false;              
          }
        }
      }
    }

    public new int Width {
      get {
        return this.TextBox != null ? this.TextBox.Width : this.ComboBox.Width;
      }
      set {
        if( this.TextBox != null ) {
          this.TextBox.Width = value;
        } else {
          this.ComboBox.Width = value;
        }
      }
    }

    public new int Height {
      get {
        return this.TextBox != null ? this.TextBox.Height : this.ComboBox.Height;
      }
      set {
        if( this.TextBox != null ) {
          this.TextBox.Height = value;
        } else {
          this.ComboBox.Height = value;
        }
      }
    }

    public void Clear() {
      if(this.TextBox != null) {
        this.TextBox.Text = "";
      } else {
        this.ComboBox.SelectedIndex = 0;
      }
    }

    public string Value {
      get {
        if( this.TextBox != null ) {
          return this.TextBox.Text;
        } else if( this.ComboBox != null) {
          return this.ComboBox.Text;
        } 
        return null;
      }
      set {
        if( this.TextBox != null ) {
          this.TextBox.Text = value;
        } else if( this.ComboBox != null ) {
          this.ComboBox.SelectedIndex = (int)Enum.Parse(this.SelectionType, value);            
        }
      }
    }
  
  }
}
