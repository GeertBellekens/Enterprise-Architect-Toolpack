
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace EADoorsNGConnector
{
	/// <summary>
	/// Description of SelectImportTypes.
	/// </summary>
	internal partial class SelectImportTypesForm : Form
	{
		EADoorsNGSettings settings;
		public SelectImportTypesForm(EADoorsNGSettings settings)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.settings = settings;
			this.DoorsNGTypeComboBox.Items.AddRange(this.settings.requirementMappings.Values.Distinct().ToArray());
			//select the first one
			this.DoorsNGTypeComboBox.SelectedIndex = 0;
			
		}
		void TFSTypeComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			this.SparxTypesComboBox.Items.Clear();
			this.SparxTypesComboBox.Items.AddRange(this.settings.requirementMappings
		                                       .Where(x => x.Value == DoorsNGTypeComboBox.Text).Select(x => x.Key).ToArray());
			//select the first entry
			this.SparxTypesComboBox.SelectedIndex = 0;
		}
		void enableDisable()
		{
			DoorsNGTypeComboBox.Enabled = ! allTypesCheckBox.Checked;
			SparxTypesComboBox.Enabled = ! allTypesCheckBox.Checked;
			this.importButton.Enabled = allTypesCheckBox.Checked 
										|| (this.DoorsNGTypeComboBox.Text.Length > 0
			                             && this.SparxTypesComboBox.Text.Length > 0);
		}
		void CancelButtonClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		void ImportButtonClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		public string DoorsNGRequirementType
		{
			get
			{
				return DoorsNGTypeComboBox.Text;
			}
		}
		public string SparxType
		{
			get
			{
				return SparxTypesComboBox.Text;
			}
		}
		public bool allTypes
		{
			get
			{
				return allTypesCheckBox.Checked;
			}
		}
		void AllTypesCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.enableDisable();
		}
	}
}
