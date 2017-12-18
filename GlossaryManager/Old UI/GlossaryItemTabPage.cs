using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;

namespace GlossaryManager {

  public abstract class FocusableTabPage : TabPage {
		public virtual void HandleFocus(){}
  }

  public abstract class GlossaryItemTabPage : FocusableTabPage {
    
    public GlossaryManagerUI ui { get; private set; }
    
    // the master/detail list/form combo
    private ListView        itemsList;
    protected FlowLayoutPanel form;
    
    protected GlossaryItemTabPage(GlossaryManagerUI ui) : base() {
      this.ui = ui;

      var splitContainer = new System.Windows.Forms.SplitContainer() {
        Orientation      = Orientation.Horizontal,
        Size             = new System.Drawing.Size(500, 500),
        FixedPanel       = FixedPanel.None,
        Name             = "splitContainer",
        SplitterDistance = 200,
        SplitterWidth    = 6,
        Panel1MinSize    = 50,
        Panel2MinSize    = 100,
      };

      // set here else SplitterDistance causes issues
      splitContainer.Dock = DockStyle.Fill;

      splitContainer.SuspendLayout();

      splitContainer.SplitterMoved  += splitterMoved;
      splitContainer.SplitterMoving += splitterMoving;

      this.Controls.Add(splitContainer);

      this.createList();
      this.createForm();

      splitContainer.Panel1.Controls.Add(this.itemsList);
      splitContainer.Panel2.Controls.Add(this.form);
      splitContainer.Panel2.AutoScroll = true;

      splitContainer.ResumeLayout(false);

      this.addToolbar();
      this.deleteButton.Enabled = false;
      this.exportButton.Enabled = false;

      this.ui.NewContext += new NewContextHandler(this.handleContextChange);
    }
    
    // avoid endless update loop:
    // context-change -> index-change -> select in browser -> context-change...
    private bool notifyProjectBrowser = true;
    private void handleContextChange(TSF_EA.ElementWrapper context) 
    {
    	//only do something when visible
    	var ownerTabControl = this.Parent as TabControl;
    	if (ownerTabControl != null 
    	    && ownerTabControl.SelectedTab == this)
    	{
			if(context == null) { return; }
			//turn notify off to avoid endless loop
			this.notifyProjectBrowser = false;
			//only do something if another element is selected then the already selected element or if no item is selected
			if (! HasItemSelected
			      || ! context.Equals(this.Current.Origin))
	      	{
				//deselect previous selected item
				if (this.selectedItem != null) this.selectedItem.Selected = false;
				//find the corresponsing item and select it
				foreach(ListViewItem item in this.itemsList.Items) 
				{
					var glossaryItem = item.Tag as GlossaryItem;
					if( glossaryItem != null
						&& context.Equals(glossaryItem.Origin))
					{
					  item.Selected = true;
					  break;
					}
				}
	     	 }
			//turn notify back on
			this.notifyProjectBrowser = true;
    	}
    }

    protected void select(GlossaryItem selected) {
      foreach(ListViewItem item in this.itemsList.Items) {
        item.Selected = ((GlossaryItem)item.Tag).Equals(selected);
      }
    }

    // construction of master/detail = list/form
    
    public bool HasItemSelected 
    {
      get { return this.selectedItem != null;}
    }

    public ListViewItem selectedItem {get; set;}


    public GlossaryItem Current 
    {
    	get { return this.selectedItem != null ? (GlossaryItem)this.selectedItem.Tag : null;}
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
      this.itemsList.ItemSelectionChanged += this.handleSelection;
      this.itemsList.KeyDown              += new KeyEventHandler(this.listKeyDown);

      foreach(string label in this.listHeaders ) {
        this.itemsList.Columns.Add(label, -1, HorizontalAlignment.Left);
      }
      return this.itemsList;
    }

    public void listKeyDown(object sender, KeyEventArgs e) {
      if (e.Control) {
        switch (e.KeyCode) {
          case Keys.A: this.selectAll(); break;
        }
      }
    }

    private void selectAll() {
      this.itemsList.Items.OfType<ListViewItem>()
                          .ToList()
                          .ForEach(item => item.Selected = true);
    }
    /// <summary>
    /// add the specific fields for the actual concrete type displayed
    /// </summary>
    protected abstract void addSpecificFields();
    /// <summary>
    /// create the form with all the fields
    /// </summary>
    protected void createForm() 
    {
		this.form       = new FlowLayoutPanel() { 
				FlowDirection = FlowDirection.TopDown,
				Dock          = DockStyle.Fill,
				};
		this.addField(new Field("Name")     { Width = 200 });
		addSpecificFields();
		this.addField(new Field("Author")   { Width = 200 });
		this.addField(new Field("Version")  { Width = 200 });
		this.addField(new Field("Status", GlossaryItem.statusValues){ Width = 200 });
		this.addField(new Field("Keywords") { Width = 200 });
		this.addField(new Field("Created")  {  Width = 200 ,Enabled = false });
		this.addField(new Field("Updated")  {  Width = 200 ,Enabled = false });
		this.addField(new Field("Updated by") { Width = 200 });
    }

    // mapping from field name (= Label) to corresponding Field
    protected Dictionary<string,Field> fields = new Dictionary<string,Field>();

    // adds a field to the form
    protected Field addField(Field field) {
      this.fields.Add(field.Label.Text, field);
      this.form.Controls.Add(field);
      field.ValueChanged += this.Update;
      return field;
    }

    protected virtual void Update(Field field) {
      if( this.showing ) { return; }
      if( ! this.HasItemSelected ) { return; }
      if( this.itemsList.SelectedItems.Count > 1) { return; }

      switch(field.Label.Text) {
        case "Name":        this.Current.Name        = field.Value; break;
        case "Author":      this.Current.Author      = field.Value; break;
        case "Version":     this.Current.Version     = field.Value; break;
        case "Status":      this.Current.Status      = field.Value; break;
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

    private ToolStripButton addButton;
    private ToolStripButton deleteButton;
    private ToolStripButton importButton;
    private ToolStripButton exportButton;

    private void addToolbar() {
      this.SuspendLayout();

      ToolStrip toolStrip = new ToolStrip() {
        Dock = DockStyle.Bottom
      };

      this.addButton = new ToolStripButton() {
        Name        = "addButton",
        ToolTipText = "Create New Glossary Item",
        Image       = (Image)this.ui.resources.GetObject("addButton.Image")
      };
      this.addButton.Click += new EventHandler(this.addButtonClick);
      toolStrip.Items.Add(this.addButton);

      this.deleteButton = new ToolStripButton() {
        Name        = "deleteButton",
        ToolTipText = "Delete Selected Glossary Item",
			  Image       = (Image)this.ui.resources.GetObject("deleteButton.Image")
      };
      this.deleteButton.Click += new EventHandler(this.deleteButtonClick);
      toolStrip.Items.Add(this.deleteButton);

      this.importButton = new ToolStripButton() {
        Name        = "importButton",
        ToolTipText = "Import Glossary Items",
			  Image       = (Image)this.ui.resources.GetObject("importButton.Image")
      };
      this.importButton.Click += new System.EventHandler(this.importButtonClick);
      toolStrip.Items.Add(this.importButton);

      this.exportButton = new ToolStripButton() {
        Name        = "exportButton",
        ToolTipText = "Export Glossary Items",
        Image       = (Image)this.ui.resources.GetObject("exportButton.Image")
      };
      this.exportButton.Click += new EventHandler(this.exportButtonClick);
      toolStrip.Items.Add(this.exportButton);

      this.Controls.Add(toolStrip);

      this.ResumeLayout(false);
    }

    // these need to be implemented by the concrete tabpage classes
    // because they know what kind/type of Glossary Item they are working on
    internal abstract void addButtonClick   (object sender, EventArgs e);
    internal abstract void exportButtonClick(object sender, EventArgs e);
    internal abstract void importButtonClick(object sender, EventArgs e);

    private void deleteButtonClick(object sender, EventArgs e) {
      switch(this.itemsList.SelectedItems.Count) {
        case 1:  this.deleteSingleCurrent();   break;
        default: this.deleteMultipleCurrent(); break;
      }
    }

    private void deleteSingleCurrent() {
      GlossaryItem item = this.Current;
      var answer = MessageBox.Show(
        "Are you sure to delete " + item.Name + "?",
        "Confirm Delete!!",
        MessageBoxButtons.YesNo
      );
      if( answer != DialogResult.Yes) { return; }
      this.delete(this.Current);
      this.ui.Addin.refresh();
    }

    private void deleteMultipleCurrent() {
      var answer = MessageBox.Show(
        "Are you sure to delete " +
          this.itemsList.SelectedItems.Count.ToString() + " items?",
        "Confirm Delete!!",
        MessageBoxButtons.YesNo
      );
      if( answer != DialogResult.Yes) { return; }
      foreach(ListViewItem row in this.itemsList.SelectedItems) {
        this.delete((GlossaryItem)row.Tag);
      }
      this.ui.Addin.refresh();
    }

    protected void add(GlossaryItem item) {
      // create new BI in package
      item.AsClassIn(this.ui.Addin.managedPackage);
      // request addin to refresh all lists (BI, DI, Links)
      this.ui.Addin.refresh();
      // select it for editing
      this.select(item);
      // focus Name field and select all text
      ((TextBox)this.fields["Name"].Control).SelectAll();
      this.fields["Name"].Control.Focus();
    }

    private void delete(GlossaryItem item) {
      // delete from model
      this.ui.Addin.managedPackage.deleteOwnedElement(item.Origin);
      // request addin to refresh all lists (BI, DI, Links)
      this.ui.Addin.refresh();
    }

    protected void export<T>() where T : GlossaryItem, new() {
      List<T> list = new List<T>();
      foreach(ListViewItem row in this.itemsList.SelectedItems) {
        list.Add((T)row.Tag);
      }
      this.ui.Addin.export<T>(list);
    }

    protected void import<T>() where T : GlossaryItem, new() {
      this.ui.Addin.import<T>(this.ui.Addin.managedPackage);
    }

    protected abstract List<string> AsListItemData(GlossaryItem item);

    public virtual void Show<T>(List<T> items) where T : GlossaryItem {
      this.itemsList.Items.Clear();
      foreach(var item in items) {
        ListViewItem listItem = new ListViewItem() { Tag = item };
        this.refreshItemsListItem(listItem);
        this.itemsList.Items.Add(listItem);
      }
      foreach(ColumnHeader column in this.itemsList.Columns) {
        column.Width = -1;
      }
      this.itemsList.Refresh(); // TODO check if needed
    }

    private void handleSelection(object sender, ListViewItemSelectionChangedEventArgs e) 
    {
    	//set the selected item
    	if (e.IsSelected) this.selectedItem = e.Item;
    	else this.selectedItem = null;
    	//show details
		this.show();
		//enable or disable buttons
		enableDisable(); 	
    }
    private void enableDisable()
    {
    	this.deleteButton.Enabled = this.Current != null;
    	this.exportButton.Enabled = this.Current != null;
    }

    private bool showing = false;

    protected virtual void show() {
      //set showing mode
      this.showing = true;
      //clear all fields
      this.clear();
      if( ! this.HasItemSelected ) {return;}
	  //set the fields
      this.setFields();
	  //turn "showing" mode off again
      this.showing = false;
      //select the item in the project browser
      if( this.notifyProjectBrowser ) {
        this.Current.SelectInProjectBrowser();
      }
    }
    protected virtual void setFields()
    {
		// set the fields
		this.fields["Name"].Value       = this.Current.Name;
		this.fields["Author"].Value     = this.Current.Author;
		this.fields["Version"].Value    = this.Current.Version;
		this.fields["Status"].Value     = this.Current.Status.ToString();
		this.fields["Keywords"].Value   = this.Current.Keywords == null ? "" : string.Join(",", this.Current.Keywords);
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