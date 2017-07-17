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

      this.tree = new TreeView() {
        Dock          = DockStyle.Fill,
        HideSelection = false
      };
      this.tree.AfterSelect += new TreeViewEventHandler(this.show);

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
        EAWrapped.TaggedValue tv = this.Current.getTaggedValue("DataItem");
        if(tv != null) {
          return GlossaryItemFactory<DataItem>.FromClass(tv.tagValue as EAWrapped.Class);
        }
        return null;
      }
    }

    private bool showing = false;

    private void show(object sender, TreeViewEventArgs e) {
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

    private void clear() {
      foreach(Field field in this.fields.Values) {
        field.Clear();
      }
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
      if(this.showing) { return; }

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
      if(context == null) { return; }
      this.checkContext(context);
    }

    public override void HandleFocus() {
      this.checkContext();
    }

    private void checkContext() {
      this.checkContext(this.ui.Addin.SelectedItem);
    }

    private EAWrapped.Class context = null;

    private void checkContext(EAWrapped.ElementWrapper context) {
      this.context = null;
      this.notify("Please select a Data Item or Table");

      this.tree.Nodes.Clear();

      // detect context and dispatch to approriate UI builder
      if(context == null) { return; }

      if( ! (context is EAWrapped.Class) ) { return; }

      EAWrapped.Class clazz = context as EAWrapped.Class;
      if( clazz.stereotypes.Count != 1 ) { return; }

      // passed all tests, we got a valid context
      this.context = clazz;
      this.hideNotifications();

      this.refreshTree();
    }

    EAWrapped.Attribute prevSelection = null;

    private void refreshTree() {
      this.prevSelection = this.Current;
      this.tree.Nodes.Clear();
      if(this.context == null) { return; }
      if(this.context.HasStereotype("Data Item"))  { this.showDataItem(); }
      else if(this.context.HasStereotype("table")) { this.showTable();    }
      this.tree.ExpandAll();
      this.prevSelection = null;
    }

    private void showTable() {
      var tableNode = new TreeNode(this.context.name);
      this.tree.Nodes.Add(tableNode);
      // TODO: I'm not using Table here, because that requires a Database
      //       Unsure if having a DB is really a requirement
      //       So just using the Class representation to work on.
			foreach(EAWrapped.Attribute attribute in this.context.attributes) {
        if(attribute.HasStereotype("column")) {
          var columnNode = new TreeNode(attribute.name) { Tag = attribute };
          tableNode.Nodes.Add(columnNode);
          if(this.prevSelection != null && attribute.guid == this.prevSelection.guid) {
            this.tree.SelectedNode = columnNode;
          }
          EAWrapped.TaggedValue tv = attribute.getTaggedValue("DataItem");
          if(tv != null) {
            var di = tv.tagValue as EAWrapped.Class;
            if(di != null) {
              columnNode.Nodes.Add(new TreeNode(di.name) { Tag = di });
            }
          }
        }
			}
    }

    private void showDataItem() {
      this.tree.Nodes.Add(new TreeNode(this.context.name));
      // TODO find columns that link to this (??)
      // TODO add tables and columns
    }

    private void addToolbar() {
      this.SuspendLayout();

      ToolStrip toolStrip = new ToolStrip() {
        Dock = DockStyle.Bottom
      };

      this.notificationLabel = new ToolStripLabel();
      toolStrip.Items.Add(this.notificationLabel);
      this.notificationLabel.Alignment =
        System.Windows.Forms.ToolStripItemAlignment.Right;

      var linkDataItemButton = new ToolStripButton();

      // linkDataItemButton
      linkDataItemButton.Name  = "linkDataItemButton";
			linkDataItemButton.Image =
        (System.Drawing.Image)this.ui.resources.GetObject("linkButton.Image");

      linkDataItemButton.Click += new System.EventHandler(this.linkDataItemButtonClick);

      toolStrip.Items.Add(linkDataItemButton);

      this.Controls.Add(toolStrip);

      this.ResumeLayout(false);
    }

    private void linkDataItemButtonClick(object sender, EventArgs e) {
      // TODO
    }

    private void notify(string msg) {
      this.notificationLabel.Text = msg;
      this.notificationLabel.Visible = true;
    }

    private void hideNotifications() {
      this.notificationLabel.Visible = false;
    }

  }
}
