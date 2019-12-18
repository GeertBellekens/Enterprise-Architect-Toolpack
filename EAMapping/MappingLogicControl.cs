using EAAddinFramework.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TSF.UmlToolingFramework.Wrappers.EA;

namespace EAMapping
{
    public partial class MappingLogicControl : UserControl
    {
        private MappingLogic _mappingLogic;
        public MappingLogic mappingLogic
        {
            get => this._mappingLogic;
            set
            {
                this._mappingLogic = value;
                this.loadContent();
            }
        }
        public MappingLogicControl()
        {
            this.InitializeComponent();
            this.setContexts(null);
        }
        public void setContexts(List<ElementWrapper> contexts)
        {
            //add the "default" to the list of contexts (if there are more or less then 1 contexts)
            var dropDownItems = new List<object>();
            if (contexts == null || contexts.Count() != 1)
            {
                dropDownItems.Add(new DefaultSelection());
            }
            if (contexts != null)
            {
                dropDownItems.AddRange(contexts);
            }
            this.contextDropdown.Enabled = dropDownItems.Count() > 1;
            this.contextDropdown.DataSource = dropDownItems;
            this.contextDropdown.DisplayMember = "name";
        }
        private void loadContent()
        {
            if (this.mappingLogic?.context != null)
            {
                this.contextDropdown.SelectedItem = this.mappingLogic.context;
            }
            this.mappingLogicTextBox.Text = this.mappingLogic?.description;
        }
        public bool readOnly    
        {
            set
            {
                this.mappingLogicTextBox.ReadOnly = value;
                this.deleteButton.Enabled = !value;
            }
        }

        public event EventHandler browseButtonClicked;
        private void browseButton_Click(object sender, EventArgs e)
        {
            browseButtonClicked?.Invoke(this, e);
        }

        public event EventHandler mappingLogicTextChanged;
        private void mappingLogicTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.mappingLogicTextBox.Text.Equals(this.mappingLogic.description, StringComparison.InvariantCulture))
            {
                this.mappingLogic.description = this.mappingLogicTextBox.Text;
                mappingLogicTextChanged?.Invoke(this, e);
            }
        }

        public event EventHandler deleteButtonClicked;
        private void deleteButton_Click(object sender, EventArgs e)
        {
            deleteButtonClicked?.Invoke(this, e);
        }
        public event EventHandler selectedContextChanged;
        private void contextDropdown_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var context = this.contextDropdown.SelectedItem as ElementWrapper;
            if (contextDropdown.Items.Count > 1
                &&
                (context != null && !context.Equals(this.mappingLogic.context)
                || context == null && this.mappingLogic.context != null))
            {
                this.mappingLogic.context = context;
                selectedContextChanged?.Invoke(this, e);
            }
        }
        /// <summary>
        /// required to avoid having the value selected (blue) when the item changes of the context dropdown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextDropdown_Resize(object sender, EventArgs e)
        {
            ((ComboBox)sender).Select(0, 0);
        }

        private void contextDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            var context = this.contextDropdown.SelectedItem as ElementWrapper;
            if (context != null)
            {
                this.toolTip1.SetToolTip(this.contextDropdown, context.fqn);
            }
        }
        //dummy class to set default selection
        private class DefaultSelection
        {
            public string name => "Default";
        }
    }
}
