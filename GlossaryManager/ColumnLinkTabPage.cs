using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using System.Runtime.InteropServices;

using EAWrapped=TSF.UmlToolingFramework.Wrappers.EA;

namespace GlossaryManager {

	public class ColumnLinkTabPage : FocusableTabPage {

    private GlossaryManagerUI ui;
    private ToolStripLabel    notificationLabel;
    private TreeView          tree;
    private ImageList         treeIcons;
    private FlowLayoutPanel   form;

    public ColumnLinkTabPage(GlossaryManagerUI ui) : base() {
      this.ui = ui;
      this.Text = "Column Links";

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

      this.addToolbar();

      // prepare TreeView icons
      this.treeIcons = new ImageList();
      this.treeIcons.Images.Add(
        "data item",
        (System.Drawing.Image)this.ui.resources.GetObject("dataItem.Image")
      );
      this.treeIcons.Images.Add(
        "table",
        (System.Drawing.Image)this.ui.resources.GetObject("table.Image")
      );
      this.treeIcons.Images.Add(
        "column",
        (System.Drawing.Image)this.ui.resources.GetObject("column.Image")
      );

      this.tree = new TreeView() {
        Dock          = DockStyle.Fill,
        HideSelection = false,
        ImageList     = this.treeIcons
      };
      this.tree.AfterSelect += new TreeViewEventHandler(this.showDetail);
      this.tree.KeyDown     += new KeyEventHandler(this.treeKeyDown);

      this.Controls.Add(tree);
      
      this.ui.NewContext += new NewContextHandler(this.handleContextChange);

      this.createForm();

      splitContainer.Panel1.Controls.Add(this.tree);
      splitContainer.Panel2.Controls.Add(this.form);

      splitContainer.ResumeLayout(false);
    }

    public void treeKeyDown(object sender, KeyEventArgs e) {
      if (e.Control) {
        switch (e.KeyCode) {
          case Keys.A: this.selectAll(); break;
        }
      }
    }

    private void selectAll() {
      if( ! this.tree.CheckBoxes ) { return; }
      this.tree.Nodes.Descendants()
                     .Where(n => n.Tag is EAWrapped.Attribute)
                     .ToList()
                     .ForEach(n => n.Checked = true);
    }

    public bool HasColumnSelected {
      get {
        return this.tree.SelectedNode != null
            && this.tree.SelectedNode.Tag is EAWrapped.Attribute;
      }
    }

    public EAWrapped.Attribute Current {
      get {
        if( ! this.HasColumnSelected ) { return null; }
        return (EAWrapped.Attribute)this.tree.SelectedNode.Tag;
      }
    }

    public List<EAWrapped.Attribute> SelectedColumns {
      get {
        if(this.tree.CheckBoxes) {
          return this.tree.Nodes.Descendants()
                                .Where(n => n.Checked)
                                .Select(n => n.Tag)
                                .Cast<EAWrapped.Attribute>()
                                .ToList();
        } else if( this.HasColumnSelected ) {
          return new List<EAWrapped.Attribute>() {
            this.Current
          };
        } else {
          return new List<EAWrapped.Attribute>();
        }
      }
    }

    public DataItem CurrentDataItem {
      get {
        return this.getDataItem(this.Current);
      }
    }

    private DataItem getDataItem(EAWrapped.Attribute attribute) {
      if( attribute == null ) { return null; }
      EAWrapped.TaggedValue tv = attribute.getTaggedValue("DataItem");
      if(tv == null || tv.tagValue == null) { return null; }
      return GlossaryItemFactory<DataItem>.FromClass(tv.tagValue as EAWrapped.Class);
    }

    private bool showing = false;

    private void showDetail(object sender, TreeViewEventArgs e) {
      this.showing = true; // avoid updates being called, causing tree refresh
      this.clear();

      if( this.HasColumnSelected ) {
        this.fields["Name"].Value          = this.Current.name;
        this.fields["Type"].Value          = "TODO: datatype"; // TODO
        this.fields["Size"].Value          = this.Current.length.ToString();
        this.fields["Format"].Value        = "TODO: what?"; // TODO
        this.fields["Initial Value"].Value = this.Current.defaultValue.ToString();

        this.syncButton.Enabled = true;

        // validate/autosync against DataItem
        if(this.CurrentDataItem != null) {
          this.fields["Data Item"].Value = this.CurrentDataItem.GUID;

          // automatically sync when the Name == "<new>"
          if(this.fields["Name"].Value == "<new>") {
            this.sync();
          } else {
            // check if column values (aka field values), match the in the 
            // prototype (aka the DataItem)
            // Name == Label
            this.fields["Name"].BackColor =
              this.fields["Name"].Value != this.CurrentDataItem.Label ?
                Color.LightYellow : Color.White;
            
            // DataType ??? == LogicalDataType
            // TODO
            // Length == Size
            this.fields["Size"].BackColor =
              this.fields["Size"].Value != this.CurrentDataItem.Size.ToString() ?
                Color.LightYellow : Color.White;
            
            // Format ??? == Format
            // TODO
            // DefaultValue == InitialValue
            this.fields["Initial Value"].BackColor =
              this.fields["Initial Value"].Value != this.CurrentDataItem.InitialValue ?
                Color.LightYellow : Color.White;   
          }
        }
      } else {
        this.syncButton.Enabled = false || this.filterButton.Checked;
      }

      this.showing = false;
    }

    private bool clearing = false;

    private void clear() {
      this.clearing = true;  // avoid updates being called, causing tree refresh
      foreach(Field field in this.fields.Values) {
        field.Clear();
        field.BackColor = Color.White;
      }
      this.clearing = false;
    }

    // splitter cursor handling

    private void splitterMoving(object sender, SplitterCancelEventArgs e) {
      Cursor.Current = System.Windows.Forms.Cursors.NoMoveVert;
    }

    private void splitterMoved(Object sender, SplitterEventArgs e) {
      Cursor.Current=System.Windows.Forms.Cursors.Default;
    }
    
    private Field dataItemsComboBox;

    private void createForm() {
      this.form       = new FlowLayoutPanel() {
        FlowDirection = FlowDirection.TopDown,
        Dock          = DockStyle.Fill,
      };
      this.dataItemsComboBox = new Field("Data Item", FieldOptions.WithNull);
      this.addField(this.dataItemsComboBox);
      this.addField(new Field("Name")           { Enabled = false });
      this.addField(new Field("Type")           { Enabled = false });
      this.addField(new Field("Size")           { Enabled = false });
      this.addField(new Field("Format")         { Enabled = false });
      this.addField(new Field("Initial Value")  { Enabled = false });
    }

    // mapping from field name (= Label) to corresponding Field
    private Dictionary<string,Field> fields = new Dictionary<string,Field>();

    // adds a field to the form
    private Field addField(Field field) {
      this.fields.Add(field.Label.Text, field);
      this.form.Controls.Add(field);
      field.ValueChanged += this.update;
      return field;
    }

    public List<DataItem> DataItems {
      set {
        List<FieldValue> values = new List<FieldValue>();
        foreach(var di in value) {
          values.Add(new FieldValue() {
            Key   = di.Name,
            Value = di.GUID,
            Tag   = di
          });
        }
        this.dataItemsComboBox.DataSource = values;
      }
    }

    // Update is called when a Field (in this case only "Data Item") changes
    
    private void update(Field field) {
      if( this.showing || this.clearing ) { return; }
      if( this.Current == null )          { return; }

      // store the linked DI in a TV
      this.Current.dataitem = field.Value;
      var context = this.context;
      this.Current.save();
      this.ui.Addin.SelectedItem = context;
      
      // make sure that changes in the DI<->column structure are updated in tree
      this.refreshTree();
      // that will trigger an re-selection of the currently selected column
      // and that will trigger a "show"ing of the link information, which will
      // also update any problems wrt the DI
    }

    private void handleContextChange(EAWrapped.ElementWrapper context) {
      this.checkContext(context);
    }

    public override void HandleFocus() {
      this.checkContext(this.ui.Addin.SelectedItem);
    }

    // this is the Table or DataItem that is selected in the Project Browser
    // and is the root of the TreeView
    private EAWrapped.Class context = null;

    private void checkContext(EAWrapped.ElementWrapper context) {
      // clean up for new context build-up
      this.clear();
      this.notify("Please select a Data Item or Table");
      this.context = null;
      this.syncButton.Enabled   = false;
      this.addButton.Enabled    = false;
      this.filterButton.Enabled = false;

      // validate context
      if( context != null && context is EAWrapped.Class) {
        EAWrapped.Class clazz = context as EAWrapped.Class;
        if( clazz.HasStereotype("Data Item") || clazz.HasStereotype("table")) {
          this.context = clazz;
          this.addButton.Enabled = true;
          this.filterButton.Enabled = true;
          this.hideNotifications();
        }
      }

      this.refreshTree();
    }

    public bool ContextIsDataItem {
      get {
        if( this.context == null ) { return false; }
        return this.context.HasStereotype("Data Item");
      }
    }

    public bool ContextIsTable {
      get {
        if( this.context == null ) { return false; }
        return this.context.HasStereotype("table");
      }
    }

    // used for resetting the selection after selecting the picker and not
    // selecting a valid next choice
    EAWrapped.Attribute prevSelection = null;

    private void refreshTree() {
      this.prevSelection = this.Current;

      this.tree.SuspendLayout();

      this.tree.Nodes.Clear();

      if(this.ContextIsDataItem)   { this.populateTreeForDataItem(); }
      else if(this.ContextIsTable) { this.populateTreeForTable();    }

      this.tree.ExpandAll();

      this.tree.ResumeLayout(false);

      this.prevSelection = null;
    }

    private void populateTreeForDataItem() {
      var diNode = this.createDataItemNode(this.context, this.tree.Nodes);
      // find tables with columns that point to this DI
      // TODO: query beyond scope of Glossay Package?!
      foreach(EAWrapped.Class clazz in this.ui.Addin.managedPackage.ownedElements.OfType<EAWrapped.Class>()) {
        if(clazz.HasStereotype("table")) {
          TreeNode tableNode = null;
    			foreach(EAWrapped.Attribute attribute in clazz.attributes) {
            if(attribute.HasStereotype("column")) {
              EAWrapped.TaggedValue tv = attribute.getTaggedValue("DataItem");
              if(tv != null) {
                var di = tv.tagValue as EAWrapped.Class;
                if(di != null && di.guid == this.context.guid) {
                  if(tableNode == null) {
                    tableNode = this.createTableNode(clazz, diNode.Nodes);
                  }
                  var columnNode = this.createColumnNode(attribute, tableNode.Nodes);
                  // mark column if not in sync
                  if( this.notInSync(attribute, di) ) {
                    columnNode.ForeColor = Color.Red;
                  }
                }
              }
            }
          }
        }
      }
    }

    private void populateTreeForTable() {
      var tableNode = this.createTableNode(this.context, this.tree.Nodes);
      // TODO: I'm not using Table here, because that requires a Database
      //       Unsure if having a DB is really a requirement
      //       So just using the Class representation to work on.
			foreach(EAWrapped.Attribute attribute in this.context.attributes) {
        if(attribute.HasStereotype("column")) {
          var columnNode = this.createColumnNode(attribute, tableNode.Nodes);
          if(this.prevSelection != null && attribute.guid == this.prevSelection.guid) {
            this.tree.SelectedNode = columnNode;
          }
          EAWrapped.TaggedValue tv = attribute.getTaggedValue("DataItem");
          if(tv != null) {
            var di = tv.tagValue as EAWrapped.Class;
            if(di != null) {
              this.createDataItemNode(di, columnNode.Nodes);
              // mark column if not in sync
              if( this.notInSync(attribute, di) ) {
                columnNode.ForeColor = Color.Red;
              }
            }
          }
        }
			}
    }

    private TreeNode createTableNode(EAWrapped.Class clazz,
                                     TreeNodeCollection parent)
    {
      TreeNode node = new TreeNode(clazz.name) {
        Tag              = clazz,
        ImageKey         = "table",
        SelectedImageKey = "table"
      };
      parent.Add(node);
      this.hideCheckBox(node);
      return node;
    }

    private TreeNode createColumnNode(EAWrapped.Attribute attribute,
                                      TreeNodeCollection parent)
    {
      TreeNode node = new TreeNode(attribute.name) {
        Tag              = attribute,
        ImageKey         = "column",
        SelectedImageKey = "column"
      };
      parent.Add(node);
      return node;
    }

    private TreeNode createDataItemNode(EAWrapped.Class clazz,
                                        TreeNodeCollection parent)
    {
      TreeNode node = new TreeNode(clazz.name) {
        Tag              = clazz,
        ImageKey         = "data item",
        SelectedImageKey = "data item"
      };
      parent.Add(node);
      this.hideCheckBox(node);
      return node;
    }

    private bool notInSync(EAWrapped.Attribute column, EAWrapped.Class clazz) {
      DataItem di = GlossaryItemFactory<DataItem>.FromClass(clazz);
      if(di == null) { return true; } // ???
      if( column.name != di.Label ) { return true; }
      // TODO DataType
      if( column.length != di.Size ) { return true; }
      // TODO Format
      if( column.defaultValue.ToString() != di.InitialValue ) { return true; }
      return false;
    }

    // toolbar and buttons
    
    private ToolStripButton syncButton;
    private ToolStripButton addButton;
    private ToolStripButton filterButton;

    private void addToolbar() {
      this.SuspendLayout();

      ToolStrip toolStrip = new ToolStrip() {
        Dock = DockStyle.Bottom
      };

      this.notificationLabel = new ToolStripLabel();
      toolStrip.Items.Add(this.notificationLabel);
      this.notificationLabel.Alignment =
        System.Windows.Forms.ToolStripItemAlignment.Right;

      this.syncButton = new ToolStripButton() {
        Name  = "syncButton",
			  Image = (System.Drawing.Image)this.ui.resources.GetObject("syncButton.Image")
      };
      this.syncButton.Click += new EventHandler(this.syncButtonClick);
      toolStrip.Items.Add(this.syncButton);

      this.addButton = new ToolStripButton() {
        Name  = "addButton",
        Image = (System.Drawing.Image)this.ui.resources.GetObject("addButton.Image")
      };
      this.addButton.Click += new EventHandler(this.addButtonClick);
      toolStrip.Items.Add(this.addButton);

      this.filterButton = new ToolStripButton() {
        Name  = "filterButton",
        Image = (System.Drawing.Image)this.ui.resources.GetObject("filterButton.Image"),
        CheckOnClick = true  
      };
      this.filterButton.CheckedChanged += new EventHandler(this.filterButtonCheckedChanged); 
      toolStrip.Items.Add(this.filterButton);

      this.Controls.Add(toolStrip);

      this.ResumeLayout(false);
    }

    private void syncButtonClick(object sender, EventArgs e) {
      this.sync();
    }

    private void sync() {
      foreach(var column in this.SelectedColumns) {
        this.sync(column);
      }
    }

    private void sync(EAWrapped.Attribute column) {
      DataItem di = this.getDataItem(column);
      column.name = di.Label;
      // TODO DataType
      column.length = di.Size;
      // TODO Format
      // TODO ValueSpecification
      column.defaultValue =
        this.ui.Addin.Model.factory.createValueSpecificationFromString(
          di.InitialValue
        );
      var context = this.context;
      column.save();
      this.ui.Addin.SelectedItem = context;
      this.refreshTree();
    }

    private void addButtonClick(object sender, EventArgs e) {
      if(this.ContextIsTable)         { this.addEmptyColumn(); }
      else if(this.ContextIsDataItem) { this.linkColumnToDataItem(); }
    }

    // adds an empty column and selects it. user can now select a Data Item.
    // because of magic values in de new empty column, validation will not only
    // validate, but also immediately perform a sync
    private void addEmptyColumn() {
      EAWrapped.Attribute attribute =
        this.ui.Addin.Model.factory.createNewElement<EAWrapped.Attribute>(
          this.context, "<new>"
        );
      attribute.AddStereotype("column");
      attribute.save();
      this.context.save();
      this.refreshTree();
      this.selectNewColumn();
    }

    private void selectNewColumn() {
      foreach(TreeNode column in this.tree.Nodes[0].Nodes) {
        if(column.Text == "<new>") {
          this.tree.SelectedNode = column;
          return;
        }
      }
    }

    private void linkColumnToDataItem() {
      // TODO: this allows for selecting/creating a new table anywhere
      //       currently the TreeView will only be populated with tables
      //       that are within the managed (gloaasry) package.

      // select a table (or create a new one)
      EAWrapped.Class table =
        (EAWrapped.Class)this.ui.Addin.Model.getUserSelectedElement(
          new List<string>() { "Class"}
        );
      if( ! table.HasStereotype("table") ) { return; }

      // create new column on table
      EAWrapped.Attribute attribute =
        this.ui.Addin.Model.factory.createNewElement<EAWrapped.Attribute>(
          table, "<new>"
        );
      attribute.AddStereotype("column");
      var context = this.context;
      attribute.save();

      // link the column to the DataItem
      attribute.dataitem = context.guid;
      attribute.save();
      
      // sync
      this.sync(attribute);

      this.ui.Addin.SelectedItem = context;
    }

    private void filterButtonCheckedChanged(object sender, EventArgs e) {
      this.tree.CheckBoxes = ((ToolStripButton)sender).Checked;
      this.syncButton.Enabled = this.tree.CheckBoxes;
      this.refreshTree();
    }

    // utility methods to manage the notification text on the ToolStrip

    private void notify(string msg) {
      this.notificationLabel.Text = msg;
      this.notificationLabel.Visible = true;
    }

    private void hideNotifications() {
      this.notificationLabel.Visible = false;
    }

    // support for hiding checkboxes on selected TreeNodes
    // via: https://stackoverflow.com/questions/4826556

    private const int TVIF_STATE = 0x8;
    private const int TVIS_STATEIMAGEMASK = 0xF000;
    private const int TV_FIRST = 0x1100;
    private const int TVM_SETITEM = TV_FIRST + 63;

    [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
    private struct TVITEM {
      public int mask;
      public IntPtr hItem;
      public int state;
      public int stateMask;
      [MarshalAs(UnmanagedType.LPTStr)]
      public string lpszText;
      public int cchTextMax;
      public int iImage;
      public int iSelectedImage;
      public int cChildren;
      public IntPtr lParam;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam,
                                             ref TVITEM lParam);

    private void hideCheckBox(TreeNode node) {
      TVITEM tvi = new TVITEM();
      tvi.hItem     = node.Handle;
      tvi.mask      = TVIF_STATE;
      tvi.stateMask = TVIS_STATEIMAGEMASK;
      tvi.state     = 0;
      SendMessage(this.tree.Handle, TVM_SETITEM, IntPtr.Zero, ref tvi);
    }
  }
}

// extension method for listing all descendants of a treenode collection
// via https://stackoverflow.com/questions/26542568

public static class TreeViewDescendants {
  internal static IEnumerable<TreeNode> Descendants(this TreeNodeCollection c) {
    foreach (var node in c.OfType<TreeNode>()) {
      yield return node;
      foreach (var child in node.Nodes.Descendants()) {
        yield return child;
      }
    }
  }
}
