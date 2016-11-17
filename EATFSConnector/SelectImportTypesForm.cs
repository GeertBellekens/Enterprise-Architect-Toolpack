
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace EATFSConnector
{
	/// <summary>
	/// Description of SelectImportTypes.
	/// </summary>
	public partial class SelectImportTypesForm : Form
	{
		EATFSConnectorSettings settings;
		public SelectImportTypesForm(EATFSConnectorSettings settings)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.settings = settings;
			this.TFSTypeComboBox.Items.AddRange(this.settings.workitemMappings.Values.Distinct().ToArray());
			//select the first one
			this.TFSTypeComboBox.SelectedIndex = 0;
			
		}
		void TFSTypeComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			this.SparxTypesComboBox.Items.Clear();
			this.SparxTypesComboBox.Items.AddRange(this.settings.workitemMappings
		                                       .Where(x => x.Value == TFSTypeComboBox.Text).Select(x => x.Key).ToArray());
			//select the first entry
			this.SparxTypesComboBox.SelectedIndex = 0;
		}
		void enableDisable()
		{
			this.importButton.Enabled = (this.TFSTypeComboBox.Text.Length > 0 
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
		public string TFSWorkitemType
		{
			get
			{
				return TFSTypeComboBox.Text;
			}
		}
		public string SparxType
		{
			get
			{
				return SparxTypesComboBox.Text;
			}
		}
	}
}
