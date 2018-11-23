using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UML = TSF.UmlToolingFramework.UML;

namespace EAMapping
{
    public partial class SelectTargetForm : Form
    {
        public SelectTargetForm()
        {
            this.InitializeComponent();
        }
        public void setItems(IEnumerable<UML.Extended.UMLItem> items)
        {
            this.elementsListView.Objects = items;
        }
        public UML.Extended.UMLItem selectedItem { get; private set; }
        private void okButton_Click(object sender, EventArgs e)
        {
            this.selectItem();
        }
        private void elementsListView_DoubleClick(object sender, EventArgs e)
        {
            this.selectItem();
        }
        private void selectItem()
        {
            this.selectedItem = this.elementsListView.SelectedObject as UML.Extended.UMLItem;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.selectedItem = null;
            this.Close();
        }
    }
}
