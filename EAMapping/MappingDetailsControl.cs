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
using TSF.UmlToolingFramework.Wrappers.EA;

namespace EAMapping
{
    public partial class MappingDetailsControl : UserControl
    {
        public MappingLogicControl defaultMappingLogicControl { get; set; }
        private List<ElementWrapper> contexts => ((MappingSet)this.mapping?.source.mappingSet).EAContexts ?? new List<ElementWrapper>();
        private const int padding = 6;
        public MappingDetailsControl()
        {
            InitializeComponent();
        }
        private void loadContent()
        {
            this.clearContents();
            this.fromTextBox.Text = this.mapping != null && this.mapping.isReverseEmpty ?
                                    "<none>"
                                    : this._mapping?.source?.name;
            this.toolTip.SetToolTip(this.fromTextBox, ((MappingNode)this._mapping?.source)?.getMappingPathExportString());
            this.toTextBox.Text = this.mapping != null && (this.mapping.isEmpty && ! this.mapping.isReverseEmpty) ? 
                                    "<none>" 
                                    : this._mapping?.target?.name;
            this.toolTip.SetToolTip(this.toTextBox, ((MappingNode)this._mapping?.target)?.getMappingPathExportString());
            if (this.mapping != null && this.mapping.mappingLogics.Any())
            {
                //TODO, add controls for each mapping
                foreach (var mappingLogic in this.mapping.mappingLogics)
                {
                    var mappingLogicControl = this.addMappingLogicControl((MappingLogic)mappingLogic);
                }
            }
            else
            {
                //add empty mapping logic control
                this.addEmptyMappingLogicControl();

            }
            enableDisable();
        }
        private void clearContents()
        {
            this.fromTextBox.Text = string.Empty;
            this.toolTip.SetToolTip(this.fromTextBox, string.Empty);
            this.toTextBox.Text = string.Empty;
            this.toolTip.SetToolTip(this.toTextBox, string.Empty);
            this.mappingLogicPanel.Controls.Clear();
            this.Height = 169; //default height
        }
        private void enableDisable()
        {
            //disable delete button when mapping is readonly
            var enable = this.mapping != null && !this._mapping.isReadOnly;
            this.deleteButton.Enabled = enable;
            this.addLogicButton.Enabled = enable && this.mappingLogicPanel.Controls.Count <  this.contexts.Count;
            //this.mappingLogicTextBox.ReadOnly = !enable;
            foreach (var mappingLogicControl in this.mappingLogicPanel.Controls.OfType<MappingLogicControl>())
            {
                mappingLogicControl.readOnly = !enable;
            }
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

        private void mappingLogicChanged(object sender, EventArgs e)
        {
            this.mapping?.save();
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

        private void addLogicButton_Click(object sender, EventArgs e)
        {
            this.addEmptyMappingLogicControl();
        }

        private void addEmptyMappingLogicControl()
        {
            if (this.mapping != null)
            {
                var mappingLogic = new MappingLogic(string.Empty);
                this.mapping.addMappingLogic(mappingLogic);
                this.addMappingLogicControl(mappingLogic);
            }
        }

        private MappingLogicControl addMappingLogicControl(MappingLogic logic )
        {
            var mappingLogicControl = new MappingLogicControl();
            mappingLogicControl.deleteButtonClicked += mappingLogicDeleteButtonClicked;
            mappingLogicControl.mappingLogicTextChanged += mappingLogicChanged;
            mappingLogicControl.selectedContextChanged += mappingLogicChanged;
            mappingLogicControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            mappingLogicControl.BorderStyle = BorderStyle.FixedSingle;
            mappingLogicControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mappingLogicControl.TabIndex = mappingLogicPanel.Controls.Count;
            mappingLogicControl.setContexts(this.contexts);
            mappingLogicControl.mappingLogic = logic;
            mappingLogicControl.Show();
            if (mappingLogicPanel.Controls.Count >= 1)
            {
                this.Height = this.Height + mappingLogicControl.Height + padding;
            }
            this.mappingLogicPanel.Controls.Add(mappingLogicControl, 0, mappingLogicPanel.Controls.Count);
            this.enableDisable();
            return mappingLogicControl;
        }

        private void mappingLogicDeleteButtonClicked(object sender, EventArgs e)
        {
            var control = (MappingLogicControl)sender;
            this.mapping.removeMappingLogic(control.mappingLogic);
            this.mapping.save();
            this.removeMappingLogicControl(control);
        }

        private void removeMappingLogicControl(MappingLogicControl control)
        {
            if (this.mappingLogicPanel.Controls.Count > 1)
            {
                this.mappingLogicPanel.Controls.Remove(control);
                this.Height = this.Height - (control.Height + padding);
            }
            this.enableDisable();
        }
    }
}
