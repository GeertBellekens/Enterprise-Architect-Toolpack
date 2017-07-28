using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

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

      this.Controls.Add(tree);
      
      this.ui.NewContext += new NewContextHandler(this.handleContextChange);

      this.createForm();

      splitContainer.Panel1.Controls.Add(this.tree);
      splitContainer.Panel2.Controls.Add(this.form);

      splitContainer.ResumeLayout(false);
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

    public DataItem CurrentDataItem {
      get {
        if( this.Current == null ) { return null; }
        EAWrapped.TaggedValue tv = this.Current.getTaggedValue("DataItem");
        if(tv == null || tv.tagValue == null) { return null; }
        return GlossaryItemFactory<DataItem>.FromClass(tv.tagValue as EAWrapped.Class);
      }
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


        if(this.CurrentDataItem != null) {
          this.fields["Data Item"].Value = this.CurrentDataItem.GUID;

          // automatically sync when the Name == "<new>"
          if(this.fields["Name"].Value == "<new>") {
            this.sync();
          }

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
      if(this.showing || this.clearing) { return; }

      // store the linked DI in a TV
      this.Current.dataitem = field.Value;
      this.Current.save();
      
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

    private EAWrapped.Class context = null;

    private void checkContext(EAWrapped.ElementWrapper context) {
      // clean up for new context build-up
      this.clear();
      this.notify("Please select a Data Item or Table");
      this.context = null;
      this.tree.Nodes.Clear();

      // validate context
      if( context == null )                { return; } // no context
      if( ! (context is EAWrapped.Class) ) { return; } // wrong context type

      EAWrapped.Class clazz = context as EAWrapped.Class;
      if(!(clazz.HasStereotype("Data Item") || clazz.HasStereotype("table"))) {
        return;
      }

      // passed all tests, we got a valid context
      this.context = clazz;
      this.hideNotifications();
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
      this.tree.Nodes.Clear();

      if(this.ContextIsDataItem)   { this.populateTreeForDataItem(); }
      else if(this.ContextIsTable) { this.populateTreeForTable();    }

      this.tree.ExpandAll();
      this.prevSelection = null;
    }

    private void populateTreeForDataItem() {
      var diNode = this.createDataItemNode(this.context);
      this.tree.Nodes.Add(diNode);
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
                    tableNode = this.createTableNode(clazz);
                    diNode.Nodes.Add(tableNode);
                  }
                  var columnNode = this.createColumnNode(attribute);
                  tableNode.Nodes.Add(columnNode);
                }
              }
            }
          }
        }
      }
    }

    private void populateTreeForTable() {
      var tableNode = this.createTableNode(this.context);
      this.tree.Nodes.Add(tableNode);
      // TODO: I'm not using Table here, because that requires a Database
      //       Unsure if having a DB is really a requirement
      //       So just using the Class representation to work on.
			foreach(EAWrapped.Attribute attribute in this.context.attributes) {
        if(attribute.HasStereotype("column")) {
          var columnNode = this.createColumnNode(attribute);
          tableNode.Nodes.Add(columnNode);
          if(this.prevSelection != null && attribute.guid == this.prevSelection.guid) {
            this.tree.SelectedNode = columnNode;
          }
          EAWrapped.TaggedValue tv = attribute.getTaggedValue("DataItem");
          if(tv != null) {
            var di = tv.tagValue as EAWrapped.Class;
            if(di != null) {
              columnNode.Nodes.Add(this.createDataItemNode(di));
              // mark column if not in sync
              if( this.notInSync(attribute, di) ) {
                columnNode.ForeColor = Color.Red;
              }
            }
          }
        }
			}
    }

    private TreeNode createTableNode(EAWrapped.Class clazz) {
      return new TreeNode(clazz.name) {
        Tag              = clazz,
        ImageKey         = "table",
        SelectedImageKey = "table"
      };
    }

    private TreeNode createColumnNode(EAWrapped.Attribute attribute) {
      return new TreeNode(attribute.name) {
        Tag              = attribute,
        ImageKey         = "column",
        SelectedImageKey = "column"
      };
    }

    private TreeNode createDataItemNode(EAWrapped.Class clazz) {
      return new TreeNode(clazz.name) {
        Tag              = clazz,
        ImageKey         = "data item",
        SelectedImageKey = "data item"
      };
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

    private void addToolbar() {
      this.SuspendLayout();

      ToolStrip toolStrip = new ToolStrip() {
        Dock = DockStyle.Bottom
      };

      this.notificationLabel = new ToolStripLabel();
      toolStrip.Items.Add(this.notificationLabel);
      this.notificationLabel.Alignment =
        System.Windows.Forms.ToolStripItemAlignment.Right;

      var syncButton = new ToolStripButton();
      syncButton.Name  = "syncButton";
			syncButton.Image =
        (System.Drawing.Image)this.ui.resources.GetObject("syncButton.Image");
      syncButton.Click += new System.EventHandler(this.syncButtonClick);
      toolStrip.Items.Add(syncButton);

      var addButton = new ToolStripButton();
      addButton.Name  = "addButton";
			addButton.Image =
        (System.Drawing.Image)this.ui.resources.GetObject("addButton.Image");
      addButton.Click += new System.EventHandler(this.addButtonClick);
      toolStrip.Items.Add(addButton);

      this.Controls.Add(toolStrip);

      this.ResumeLayout(false);
    }

    private void syncButtonClick(object sender, EventArgs e) {
      this.sync();
    }
    
    private void sync() {
      if(this.CurrentDataItem == null) { return; }
      this.Current.name = this.CurrentDataItem.Label;
      // TODO DataType
      this.Current.length = this.CurrentDataItem.Size;
      // TODO Format
      // TODO ValueSpecification
      this.Current.defaultValue =
        this.ui.Addin.Model.factory.createValueSpecificationFromString(
          this.CurrentDataItem.InitialValue
        );
      this.Current.save();
      this.refreshTree();
    }

    private void addButtonClick(object sender, EventArgs e) {
      if(this.ContextIsTable)         { this.addEmptyColumn(); }
      else if(this.ContextIsDataItem) { this.AddColumnFromDataItem(); }
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

    private void AddColumnFromDataItem() {
      // TODO: selected item must be table
      // - add empty column
      // - set dataitem reference
      // - sync
    }

    // utility methods to manage the notification text on the ToolStrip

    private void notify(string msg) {
      this.notificationLabel.Text = msg;
      this.notificationLabel.Visible = true;
    }

    private void hideNotifications() {
      this.notificationLabel.Visible = false;
    }

  }
}
