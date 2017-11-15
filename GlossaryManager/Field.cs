using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;
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
  	public Label    Label {get;set;}
    public TextBox  TextBox {get;set;}
    public ComboBox ComboBox {get;set;}
    public string pickerType {get;set;}
    public string pickerStereotype {get;set;}
    
    private int previousSelectedIndex = -1;

    // option 1 : TextBox with label
    public Field(string label) : base() {
      this.createLabel(label);

      this.TextBox = new TextBox();
      this.TextBox.TextChanged += new EventHandler(this.change);

      this.Controls.Add(this.TextBox);
    }

   
    // option 2 : Combobox with a Type/Enumeration of values
    public Field(string label, IEnumerable<string> dataValues) : base() {
      this.createLabel(label);
	  
      this.ComboBox = new ComboBox() {
        DataSource = dataValues
      };
      this.ComboBox.SelectedIndexChanged += new EventHandler(this.change);

      this.Controls.Add(this.ComboBox);
    }

    private GlossaryItemTabPage page = null;
    private TSF_EA.Model model {
      get { return this.page.ui.Addin.Model; }
    }

    private FieldOptions     options;

    private readonly BindingList<FieldValue> data = new BindingList<FieldValue>();

    // third option: combobox with predefined values
    public Field(string label, FieldOptions options)
      : this(label, options, null) {}
    
    // if we need a picker, we need to provide a page
    public Field(string label, FieldOptions options, GlossaryItemTabPage page)
      : base()
    {
      this.page = page;

      this.createLabel(label);

      this.options = options;
  
      this.ComboBox = new ComboBox() {
        DisplayMember = "Key",
        ValueMember   = "Value",
        DataSource    = this.data
      };

      this.setupCleanComboBox();

      this.ComboBox.SelectedIndexChanged += new EventHandler(this.change);
      if( this.IsNullable ) {
        this.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
        this.ComboBox.DrawItem += new DrawItemEventHandler(drawItem);
      }

      this.Controls.Add(this.ComboBox);
    }

    public bool IsNullable {
      get {
        return (this.options & FieldOptions.WithNull) != 0;
      }
    }

    public bool IsPickable {
      get {
        return (this.options & FieldOptions.WithPicker) != 0 && this.page != null;
      }
    }

    private void setupCleanComboBox() {
      this.data.Clear();
      if( this.IsNullable ) {
        this.data.Insert(0, new FieldValue() {
          Key   = "",
          Value = ""
        });
      }
      if( this.IsPickable ) {
        this.data.Add(new FieldValue() { 
          Key      = "-Select Type...",
          Value    = "",
          IsPicked = true
        });
      }
    }

    public List<FieldValue> DataSource {
      set {
        this.setupCleanComboBox();
        foreach(FieldValue field in value) {
          if(this.IsPickable) {
            this.data.Insert(this.data.Count - 1, field);
          } else {
            this.data.Add(field);
          }
        }
      }
    }

    // called when drawing the dropped down (list-)part of the combobox
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
      TSF_EA.Class selection = (TSF_EA.Class)this.model.getUserSelectedElement(new List<string>() { "Class"} );
      if( selection == null ) {
        this.ComboBox.SelectedIndex = this.previousSelectedIndex;
        return;
      }
      int index = this.data.Count - 1;
      this.data.Insert(index, new FieldValue() {
        Key   = selection.name,
        Value = selection.guid
      });
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

    public override Color BackColor {
      set {
        if( this.TextBox != null) {
          this.TextBox.BackColor = value;
        } else {
          this.ComboBox.BackColor = value;
        }
      }
    }

    public string Value {
      get {
        if( this.TextBox != null ) {
          return this.TextBox.Text;
        } else if( this.ComboBox != null) 
  		{
  			var fieldValue = this.ComboBox.SelectedItem as FieldValue;
  			return fieldValue != null ? fieldValue.Value : this.ComboBox.SelectedItem.ToString();
        } 
        return null;
      }
      set {
        if( this.TextBox != null ) 
        {
        	this.TextBox.Text = value;
        } 
  		else if( this.ComboBox != null )
  		{
	        // find value
	        int index = 0;
	        foreach(var item in this.ComboBox.Items) 
	        {
	        	if (item is FieldValue)
	        	{
	        		if(((FieldValue)item).Value == value)  break; 
	        	}
	        	else
	        	{
	        		if (item.Equals(value)) break;
	        	}
	          // go to next value
	          index++;
	        }
	        // reset if not found
	        if(index == this.ComboBox.Items.Count) { index = 0; }
	        this.ComboBox.SelectedIndex = index;
        }
      }
    }
  }
}
