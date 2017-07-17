using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using EAWrapped=TSF.UmlToolingFramework.Wrappers.EA;

namespace GlossaryManager {

  public abstract class FocusableTabPage : TabPage {
    public virtual void HandleFocus() {}
  }

  public abstract class GlossaryItemTabPage : FocusableTabPage {
    
    public GlossaryManagerUI ui { get; private set; }
    
    // the master/detail list/form combo
    private ListView        itemsList;
    private FlowLayoutPanel form;
    
    public GlossaryItemTabPage(GlossaryManagerUI ui) : base() {
      this.ui = ui;

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

      this.Controls.Add(splitContainer);

      this.createList();
      this.createForm();

      splitContainer.Panel1.Controls.Add(this.itemsList);
      splitContainer.Panel2.Controls.Add(this.form);

      splitContainer.ResumeLayout(false);

      this.addToolbar();

      this.ui.NewContext += new NewContextHandler(this.handleContextChange);
    }
    
    // avoid endless update loop:
    // context-change -> index-change -> select in browser -> context-change...
    private bool notifyProjectBrowser = true;
    private void handleContextChange(EAWrapped.ElementWrapper context) {
      if(context == null) { return; }
      this.notifyProjectBrowser = false;
      if(this.HasItemSelected) { this.Selected.Selected = false; }
      foreach(ListViewItem item in this.itemsList.Items) {
        if( ((GlossaryItem)item.Tag).Origin.Equals(context) ) {
          item.Selected = true;
          break;
        }
      }
      this.notifyProjectBrowser = true;
    }
    
    // construction of master/detail = list/form
    
    public bool HasItemSelected {
      get {
        return this.itemsList.SelectedItems.Count > 0;
      }
    }

    public ListViewItem Selected {
      get {
        if( ! this.HasItemSelected ) { return null; }
        return this.itemsList.SelectedItems[0];        
      }
    }

    public GlossaryItem Current {
      get {
        if( ! this.HasItemSelected ) { return null; }
        return (GlossaryItem)this.Selected.Tag;
      }
    }

    // to be implemented by specific item class, providing list of headers
    protected abstract List<string> listHeaders { get; }

    private ListView createList() {
      this.itemsList       = new ListView() {
        Dock               = DockStyle.Fill,
        FullRowSelect      = true,
        View               = View.Details,
        ListViewItemSorter = this.columnSorter,
        HideSelection      = false
      };
      this.itemsList.ColumnClick          += new ColumnClickEventHandler(this.sortColumn);
      this.itemsList.SelectedIndexChanged += new EventHandler(this.show);

      foreach(string label in this.listHeaders ) {
        this.itemsList.Columns.Add(label, -1, HorizontalAlignment.Left);
      }
      return this.itemsList;
    }

    protected virtual void createForm() {
      this.form       = new FlowLayoutPanel() {
        FlowDirection = FlowDirection.TopDown,
        Dock          = DockStyle.Fill,
      };
      this.addField(new Field("Name")     { Width = 125 });
      this.addField(new Field("Author")   { Width = 125 });
      this.addField(new Field("Version"));
      this.addField(new Field("Status", typeof(Status)));
      this.addField(new Field("Keywords") { Width = 250 });
      this.addField(new Field("Created")  { Enabled = false });
      this.addField(new Field("Updated")  { Enabled = false });
      Control last = this.addField(new Field("Updated by") { Width = 125 });
      // creates a column break, marking the difference between GI and BI/DI
      this.form.SetFlowBreak(last, true);
    }

    // mapping from field name (= Label) to corresponding Field
    protected Dictionary<string,Field> fields = new Dictionary<string,Field>();

    // adds a field to the form
    protected Control addField(Field field) {
      this.fields.Add(field.Label.Text, field);
      this.form.Controls.Add(field);
      field.ValueChanged += this.Update;
      return field;
    }

    protected virtual void Update(Field field) {
      if( ! this.HasItemSelected ) { return; }
      switch(field.Label.Text) {
        case "Name":        this.Current.Name        = field.Value; break;
        case "Author":      this.Current.Author      = field.Value; break;
        case "Version":     this.Current.Version     = field.Value; break;
        case "Status":      this.Current.Status      = (Status)Enum.Parse(typeof(Status), field.Value); break;
        case "Keywords":    this.Current.Keywords    = field.Value.Split(',').ToList(); break;
        case "Updated by":  this.Current.UpdatedBy   = field.Value; break;
      }
      this.Current.Save();
      this.refreshItemsList();
    }
    
    private void refreshItemsList() {
      foreach(ListViewItem item in this.itemsList.Items) {
        this.refreshItemsListItem(item);
      }
    }
    
    private void refreshItemsListItem(ListViewItem item) {
      int i = 0;
      foreach(string text in this.AsListItemData((GlossaryItem)item.Tag)) {
        if(item.SubItems.Count <= i) {
          item.SubItems.Add(text);
        } else {
          item.SubItems[i].Text = text;
        }
        i++;
      }
    }
    
    private void addToolbar() {
      ToolStrip toolStrip = new ToolStrip();
      toolStrip.Dock = DockStyle.Bottom;
      this.Controls.Add(toolStrip);

      var exportButton = new ToolStripButton();

      this.SuspendLayout();

      // exportButton
      exportButton.Name      = "exportButton";
      exportButton.Text      = "Export";

      // this.exportButton.Click += new System.EventHandler(this.ExportButtonClick);

      toolStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());
      toolStrip.Items.Add(exportButton);

      this.ResumeLayout(false);
    }

    protected abstract List<string> AsListItemData(GlossaryItem item);

    public virtual void Show<T>(List<T> items) where T : GlossaryItem {
      foreach(var item in items) {
        ListViewItem listItem = new ListViewItem() {
          Tag = item
        };
        this.refreshItemsListItem(listItem);
        this.itemsList.Items.Add(listItem);
      }
      foreach(ColumnHeader column in this.itemsList.Columns) {
        column.Width = -1;
      }
      this.itemsList.Refresh(); // TODO check if needed
    }

    protected virtual void show(object sender, EventArgs e) {
      this.clear();
      if( ! this.HasItemSelected ) { return; }

      if( this.notifyProjectBrowser ) {
        this.Current.SelectInProjectBrowser();
      }

      this.fields["Name"].Value       = this.Current.Name;
      this.fields["Author"].Value     = this.Current.Author;
      this.fields["Version"].Value    = this.Current.Version;
      this.fields["Status"].Value     = this.Current.Status.ToString();
      this.fields["Keywords"].Value   = string.Join(",", this.Current.Keywords);
      this.fields["Created"].Value    = this.Current.CreateDate.ToString();
      this.fields["Updated"].Value    = this.Current.UpdateDate.ToString();
      this.fields["Updated by"].Value = this.Current.UpdatedBy;
    }

    private void clear() {
      foreach(Field field in this.fields.Values) {
        field.Clear();
      }
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
      ((ListView)sender).Sort();
    }

    // splitter cursor handling

    private void splitterMoving(object sender, SplitterCancelEventArgs e) {
      Cursor.Current = System.Windows.Forms.Cursors.NoMoveVert;
    }

    private void splitterMoved(Object sender, SplitterEventArgs e) {
      Cursor.Current=System.Windows.Forms.Cursors.Default;
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