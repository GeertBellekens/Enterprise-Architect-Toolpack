using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace GlossaryManager {

	partial class GlossaryManagerUI	{

		private IContainer components = null;
		
		protected override void Dispose(bool disposing)	{
			if( disposing ) {
				if( components != null ) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		private ComponentResourceManager resources;
		
		private void InitializeComponent() {
      this.resources = new ComponentResourceManager(typeof(GlossaryManagerUI));

			this.SuspendLayout();

			// MappingControlGUI

			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
			this.Name                = "GlossaryManagerUI";
			this.Size                = new System.Drawing.Size(994, 584);

      this.createTabControl();

			this.ResumeLayout(false);
		}

    private void createTabControl() {
			TabControl tabs = new TabControl () {
        Alignment     = TabAlignment.Top,
        Dock          = DockStyle.Fill,
			  Appearance    = TabAppearance.FlatButtons,
        SelectedIndex = 0,
        Padding       = new System.Drawing.Point(15, 7)
      };
      
			tabs.Controls.Add( this.createBusinessItemsTabPage() );
			tabs.Controls.Add( this.createDataItemsTabPage()     );

			this.Controls.Add(tabs);
    }
    
    private TabPage createBusinessItemsTabPage() {
      TabPage tab = new TabPage() { Text   = "Business Items" };

      var splitContainer = new System.Windows.Forms.SplitContainer() {
        Orientation      = Orientation.Horizontal,
        Size             = new System.Drawing.Size(500, 500),
        FixedPanel       = FixedPanel.None,
        Name             = "splitContainer",
        SplitterDistance = 200,
        SplitterWidth    = 6,
        Panel1MinSize    = 200,
        Panel2MinSize    = 100
      };

      // set here else SplitterDistance causes issues
      splitContainer.Dock = DockStyle.Fill;

      splitContainer.SuspendLayout();

      splitContainer.SplitterMoved  += new SplitterEventHandler(splitterMoved);
      splitContainer.SplitterMoving += new SplitterCancelEventHandler(splitterMoving);

      tab.Controls.Add(splitContainer);

      splitContainer.Panel1.Controls.Add(this.createBusinessItemsList());
      splitContainer.Panel2.Controls.Add(this.createBusinessItemsForm());

      splitContainer.ResumeLayout(false);
      
      this.addToolbar(tab);

      return tab;
    }

    private void splitterMoving(object sender, SplitterCancelEventArgs e) {
      Cursor.Current = System.Windows.Forms.Cursors.NoMoveVert;
    }

    private void splitterMoved(Object sender, SplitterEventArgs e) {
      Cursor.Current=System.Windows.Forms.Cursors.Default;
    }

    // Business Item list and record viewers

    private ListView businessItemsList;

    private ListView createBusinessItemsList() {
      this.businessItemsList = new ListView() {
        Dock                 = DockStyle.Fill,
        FullRowSelect        = true,
        View                 = View.Details,
        ListViewItemSorter   = this.columnSorter
      };
      this.businessItemsList.ColumnClick +=
        new ColumnClickEventHandler(this.sortColumn);
      this.businessItemsList.SelectedIndexChanged +=
        new EventHandler(this.showBusinessItem);

      foreach(string label in new List<string>() {
        "Name", "Description", "Version", "Status", "Updated"
      }) {
        this.businessItemsList.Columns.Add(label, -1, HorizontalAlignment.Left);
      }
      return this.businessItemsList;
    }

    public void ShowBusinessItems(List<BusinessItem> items) {
      foreach(var item in items) {
        var listItem = new ListViewItem( new[] {
          item.Name,
          item.Description,
          item.Version,
          item.Status.ToString(),
          item.UpdateDate.ToString()
        });
        listItem.Tag = item;
        this.businessItemsList.Items.Add(listItem);
      }
      foreach(ColumnHeader column in businessItemsList.Columns) {
        column.Width = -1;
      }
      businessItemsList.Refresh();
    }

    // TODO make property using selected item in listview
    private GlossaryItem currentItem = null;

    public void showBusinessItem(object sender, EventArgs e) {
      this.clearBusinessItemsForm();
      if(this.businessItemsList.SelectedItems.Count < 1) { return; }
      BusinessItem bi = (BusinessItem)this.businessItemsList.SelectedItems[0].Tag;
      this.showGlossaryItem(bi, this.businessItemsFormFields);
      this.businessItemsFormFields["Description"].Value = bi.Description;
      this.businessItemsFormFields["Domain"].Value      = bi.Domain;
    }

    private void showGlossaryItem(GlossaryItem item, Dictionary<string,Field> fields) {
      fields["Name"].Value       = item.Name;
      this.businessItemsFormFields["Name"].Enabled = false;
      fields["Author"].Value     = item.Author;
      fields["Version"].Value    = item.Version;
      fields["Status"].Value     = item.Status.ToString();
      fields["Keywords"].Value   = string.Join(",", item.Keywords);
      fields["Created"].Value    = item.CreateDate.ToString();
      fields["Updated"].Value    = item.UpdateDate.ToString();
      fields["Updated by"].Value = item.UpdatedBy;
      this.currentItem = item;
    }

    private Dictionary<string,Field> businessItemsFormFields =
      new Dictionary<string,Field>();

    private Control addField(Field field, Panel panel) {
      this.businessItemsFormFields.Add(field.Label.Text, field);
      panel.Controls.Add(field);
      field.ValueChanged += this.UpdateBusinessItem;
      return field;
    }

    private void UpdateBusinessItem(Field field) {
      if( this.currentItem == null ) { return; }
      switch(field.Label.Text) {
        case "Author":      this.currentItem.Author      = field.Value; break;
        case "Version":     this.currentItem.Version     = field.Value; break;
        case "Status":      this.currentItem.Status      = (Status) Enum.Parse(typeof(Status), field.Value); break;
        case "Keywords":    this.currentItem.Keywords    = field.Value.Split(',').ToList(); break;
        case "Updated by":  this.currentItem.UpdatedBy   = field.Value; break;
        case "Description": ((BusinessItem)this.currentItem).Description = field.Value; break;
        case "Domain":      ((BusinessItem)this.currentItem).Domain      = field.Value; break;
      }
      this.currentItem.Save();
    }

    private Panel createBusinessItemsForm() {
      var panel = this.createGlossaryItemsForm();

      this.addField(new Field("Description") {
        Multiline = true,
        Width     = 300,
        Height    = 100
      }, panel);
      this.addField(new Field("Domain"), panel);
      
      return panel;
    }

    private void clearBusinessItemsForm() {
      this.currentItem = null;
      foreach(Field field in this.businessItemsFormFields.Values) {
        field.Clear();
      }
      this.businessItemsFormFields["Name"].Enabled = true;
    }

    // Data Items list and record viewers

    private TabPage createDataItemsTabPage() {
      TabPage tab = new TabPage() { Text   = "Data Items" };

      // TODO

      return tab;
    }

    // common controls

    private Panel createGlossaryItemsForm() {
      var panel       = new FlowLayoutPanel() {
        FlowDirection = FlowDirection.TopDown,
        Dock          = DockStyle.Fill,
      };

      this.addField(new Field("Name")      { Width = 125 },      panel);
      this.addField(new Field("Author")    { Width = 125 },      panel);
      this.addField(new Field("Version"),                        panel);
      this.addField(new Field("Status", typeof(Status)),         panel);
      this.addField(new Field("Keywords")   { Width = 250 },     panel);
      this.addField(new Field("Created")    { Enabled = false }, panel);
      this.addField(new Field("Updated")    { Enabled = false }, panel);
      panel.SetFlowBreak(
        this.addField(new Field("Updated by") { Width = 125 },     panel),
        true
      );

      return panel;      
    }

    private void addToolbar(TabPage tab) {
      ToolStrip toolStrip = new ToolStrip();
      toolStrip.Dock = DockStyle.Bottom;
      tab.Controls.Add(toolStrip);

      var gotoButton   = new ToolStripButton();
      var exportButton = new ToolStripButton();

      this.SuspendLayout();

      // gotoButton
      gotoButton.Image = this.resources.GetObject("gotoButton.Image") as Image;
      gotoButton.Name = "gotoButton";

      // exportButton
      exportButton.Name      = "exportButton";
      exportButton.Text      = "Export";

      // this.exportButton.Click += new System.EventHandler(this.ExportButtonClick);

      toolStrip.Items.Add(gotoButton);
      toolStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());
      toolStrip.Items.Add(exportButton);

      this.ResumeLayout(false);
    }

    // custom column sorter

    private ListViewColumnSorter columnSorter = new ListViewColumnSorter();

    private void sortColumn(object sender, ColumnClickEventArgs e) {
      // Determine if clicked column is already the column that is being sorted.
      if ( e.Column == columnSorter.ColumnToSort )  {
        // Reverse the current sort direction for this column.
        if( columnSorter.OrderOfSort == SortOrder.Ascending ) {
          columnSorter.OrderOfSort = SortOrder.Descending;
        } else {
          columnSorter.OrderOfSort = SortOrder.Ascending;
        }
      } else {
        columnSorter.ColumnToSort = e.Column;
        columnSorter.OrderOfSort = SortOrder.Ascending;
      }
      this.Addin.log(columnSorter.ToString());
      ((ListView)sender).Sort();
    }
	}
  
  class Field : FlowLayoutPanel {
    public Label    Label;
    public TextBox  TextBox;
    public ComboBox ComboBox;

    public Field(string label) : base() {
      this.createLabel(label);

      this.TextBox = new TextBox();
      this.TextBox.TextChanged += new EventHandler(this.change);


      this.Controls.Add(this.TextBox);
    }

    public Field(string label, Type options) : base() {
      this.createLabel(label);

      this.ComboBox            = new ComboBox();
      this.ComboBox.DataSource = Enum.GetValues(options);
      this.ComboBox.SelectedIndexChanged += new EventHandler(this.change);

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
          this.ComboBox.SelectedIndex = (int)Enum.Parse(typeof(Status), value);            
        }
      }
    }
    
  }

  // TODO implement date,... sorting
  public class ListViewColumnSorter : IComparer {
    public int ColumnToSort { get; set; }
    public SortOrder OrderOfSort { get; set; }
    private CaseInsensitiveComparer objectCompare;

    public ListViewColumnSorter() {
      this.ColumnToSort  = 0;
      this.OrderOfSort   = SortOrder.None;
      this.objectCompare = new CaseInsensitiveComparer();
    }

    public int Compare(object x, object y) {
      int compareResult = this.objectCompare.Compare(
        ((ListViewItem)x).SubItems[this.ColumnToSort].Text,
        ((ListViewItem)y).SubItems[this.ColumnToSort].Text
      );

      if(this.OrderOfSort == SortOrder.Ascending) {
        return compareResult;
      } else if(this.OrderOfSort == SortOrder.Descending) {
        return -compareResult;
      } else {
        return 0;
      }
    }

    public override string ToString() {
      return "sorting column " + this.ColumnToSort.ToString() + " " +
             this.OrderOfSort.ToString();
    }
  }

}
