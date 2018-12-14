using EAAddinFramework.Mapping;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TSF.UmlToolingFramework.Wrappers.EA;
using System.Linq;

namespace EAMapping
{
    public partial class MappingLogicControl : UserControl
    {
        private bool _isDefault = false;
        private MappingLogic _mappingLogic;
        public MappingLogic mappingLogic
        {
            get
            {
                return _mappingLogic;
            }
            set
            {
                this._mappingLogic = value;
                this.loadContent();
            }
        }
        public MappingLogicControl()
        {
            this.InitializeComponent();
            this.enableDisable();
        }
        public void setContexts(List<ElementWrapper> contexts)
        {
            //add the "default" to the list of contexts
            var dropDownItems = new List<object> { new DefaultSelection() };
            dropDownItems.AddRange(contexts);
            this.contextDropdown.Enabled = contexts.Any();
            this.contextDropdown.DataSource = dropDownItems;
            this.contextDropdown.DisplayMember = "name";            
        }
        private void loadContent()
        {
            if ( mappingLogic?.context != null)
            {
                this.contextDropdown.Text = mappingLogic.context.name;
                //TODO: set tooltip
            }
            else
            {
                this.isDefault = true;
            }
            this.mappingLogicTextBox.Text = mappingLogic?.description;
        }
        private void enableDisable()
        {
            //todo
        } 
        public bool isDefault
        {
            get => this._isDefault;
            set
            {
                this._isDefault = value;
                this.enableDisable();
            }
        }
        public event EventHandler browseButtonClicked;

        private void browseButton_Click(object sender, EventArgs e)
        {
            this.browseButtonClicked?.Invoke(this, e);
        }

        private void mappingLogicTextBox_TextChanged(object sender, EventArgs e)
        {
            this.mappingLogic.description = mappingLogicTextBox.Text;
        }
        private class DefaultSelection
        {
            public string name => "Default";
        }
    }
}
