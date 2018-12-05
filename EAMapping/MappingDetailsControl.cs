using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MP = MappingFramework;
using EAAddinFramework.Mapping;

namespace EAMapping
{
    public partial class MappingDetailsControl : UserControl
    {
        public MappingDetailsControl()
        {
            InitializeComponent();
        }
        private void loadContent()
        {
            this.fromTextBox.Text = this._mapping?.source?.name;
            this.toolTip.SetToolTip(this.fromTextBox, ((MappingNode)this._mapping?.source)?.getMappingPathExportString());
            this.toTextBox.Text = this.mapping != null && this.mapping.isEmpty ? 
                                    "<none>" 
                                    : this._mapping?.target?.name;
            this.toolTip.SetToolTip(this.toTextBox, ((MappingNode)this._mapping?.target)?.getMappingPathExportString());
            this.mappingLogicTextBox.Text = this._mapping?.mappingLogicDescription;

            enableDisable();
        }
        private void enableDisable()
        {
            //disable delete button when mapping is readonly
            var enable = this.mapping != null && !this._mapping.isReadOnly;
            this.deleteButton.Enabled = enable;
            this.mappingLogicTextBox.ReadOnly = !enable;
        }

        private MP.Mapping _mapping;
        public MP.Mapping mapping
        {
            get
            {
                return this._mapping;
            }
            set
            {
                this._mapping = value;
                this.loadContent();
            }
        }

        private void mappingLogicTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.mapping != null
                && this.mapping.mappingLogicDescription != mappingLogicTextBox.Text)
            {
                this.mapping.mappingLogicDescription = mappingLogicTextBox.Text;
                this.mapping.save();
            }
        }
        public event EventHandler mappingDeleted;
        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.mappingDeleted?.Invoke(this, e);
        }
        public event EventHandler mapping_Enter;
        private void MappingDetailsControl_Enter(object sender, EventArgs e)
        {
            this.mapping_Enter?.Invoke(this, e);
        }
    }
}
