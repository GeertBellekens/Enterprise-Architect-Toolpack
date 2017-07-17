using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using EAWrapped=TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

// A Field is a combination of a Label and a TextBox or ComboBox
// A ComboBox can allow selecting from an Enumeration, or from a List of 
// FieldValues. In the latter case, the value can be picked using the EA
// ConstructPicker

namespace GlossaryManager {
  
  public class FieldValue {
    public string Key      { get; set; }
    public string Value    { get; set; }
    public bool   IsPicked { get; set; }
    public object Tag      { get; set; }
  }

  [Flags]
  public enum FieldOptions {
    None       = 0,
    WithNull   = 1,
    WithPicker = 2
  }

  public class Field : FlowLayoutPanel {
    public Label    Label;
    public TextBox  TextBox;
    public ComboBox ComboBox;
    
    private int previousSelectedIndex = -1;

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

    private GlossaryItemTabPage page = null;
    private EAWrapped.Model model {
      get { return this.page.ui.Addin.Model; }
    }

    private FieldOptions     options;

    // third option: combobox with predefined values
    public Field(string label, FieldOptions options) : base() {
      this.createLabel(label);

      this.options = options;
  
      this.ComboBox = new ComboBox() {
        DisplayMember = "Key",
        ValueMember   = "Value"
      };
      this.ComboBox.SelectedIndexChanged += new EventHandler(this.change);
      if( (options & FieldOptions.WithNull) != 0 ) {
        this.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
        this.ComboBox.DrawItem += new DrawItemEventHandler(drawItem);
      }

      this.Controls.Add(this.ComboBox);
    }

    public Field(string label, FieldOptions options, GlossaryItemTabPage page)
      : this(label, options)
    {
      this.page = page;
    }

    private List<FieldValue> items;
    public List<FieldValue> DataSource {
      set {
        this.items = value;

        if( (this.options & FieldOptions.WithNull) != 0 ) {
          // add a "null" value
          this.items.Insert(0, new FieldValue() {
            Key   = "",
            Value = ""
          });
        }

        if( (this.options & FieldOptions.WithPicker) != 0 && this.page != null) {
          // add option to select...
          this.items.Add(new FieldValue() { 
            Key      = "-Select Type...",
            Value    = null,
            IsPicked = true
          });
        }
        
        this.ComboBox.DataSource = new BindingSource(this.items, null);
      }
    }

    void drawItem(object sender, DrawItemEventArgs e) {
      e.DrawBackground();
      string label = ((FieldValue)this.ComboBox.Items[e.Index]).Key;
      if(label.Length > 0 && label.Substring(0, 1) == "-") {
        e.Graphics.DrawLine(
          Pens.Black,
          new Point(e.Bounds.Left,  e.Bounds.Top - 2),
          new Point(e.Bounds.Right, e.Bounds.Top - 2)
        );
        label = label.Substring(1, label.Length - 1);
      }
      TextRenderer.DrawText(e.Graphics, label, this.ComboBox.Font, e.Bounds,
                            this.ComboBox.ForeColor, TextFormatFlags.Left);
      e.DrawFocusRectangle();
    }

  	public event Action<Field> ValueChanged = delegate { }; 
  	private void change(object sender, EventArgs e) {
      if( this.ComboBox != null && this.ComboBox.SelectedItem is FieldValue) {
        if( ((FieldValue)this.ComboBox.SelectedItem).IsPicked ) {
          this.usePicker();
        }
        this.previousSelectedIndex = this.ComboBox.SelectedIndex;
      }
  		this.ValueChanged(this);
  	}

    private void usePicker() {
      EAWrapped.Class selection = (EAWrapped.Class)this.model.getUserSelectedElement(new List<string>() { "Class"} );
      if( selection == null ) {
        this.ComboBox.SelectedIndex = this.previousSelectedIndex;
        return;
      }
      int index = this.items.Count - 1;
      this.items.Insert(index, new FieldValue() {
        Key   = selection.name,
        Value = selection.guid
      });
      this.ComboBox.DataSource = new BindingSource(this.items, null);
      this.ComboBox.SelectedIndex = index;
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
          if(this.SelectionType == null) {
            return ((FieldValue)this.ComboBox.SelectedItem).Value.ToString();
          } else {
            return this.ComboBox.Text;
          }
        } 
        return null;
      }
      set {
        if( this.TextBox != null ) {
          this.TextBox.Text = value;
        } else if( this.ComboBox != null ) {
          if(this.SelectionType == null) {
            // find value
            int index = 0;
            foreach(FieldValue item in this.ComboBox.Items) {
              if(item.Value == value) { break; }
              index++;
            }
            // reset if not found
            if(index == this.ComboBox.Items.Count) { index = 0; }
            this.ComboBox.SelectedIndex = index;
          } else {
            this.ComboBox.SelectedIndex = (int)Enum.Parse(this.SelectionType, value);
          }
        }
      }
    }
  
  }
}
