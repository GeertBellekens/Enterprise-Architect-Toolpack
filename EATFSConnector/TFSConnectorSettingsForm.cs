
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EATFSConnector
{
	/// <summary>
	/// Description of TFSConnectorSettingsForm.
	/// </summary>
	public partial class TFSConnectorSettingsForm : Form
	{
		private EATFSConnectorSettings settings;

		public TFSConnectorSettingsForm(EATFSConnectorSettings settings)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.settings = settings;
			this.loadData();
			this.enableDisable();
			
		}
		private void loadData()
		{
			//TODO
		}
		private void enableDisable()
		{
			//TODO
		}
		private void saveChanges()
		{
			//TODO
			this.settings.save();
		}
	}
}
