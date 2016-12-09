
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace EATFSConnector
{
	/// <summary>
	/// Description of SelectImportTypes.
	/// </summary>
	public partial class SetProjectForm : Form
	{

		public SetProjectForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
		}
		void CancelButtonClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		
		void OkButtonClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		public string projectName
		{
			get
			{
				return this.projectNameTextBox.Text;
			}
			set
			{
				this.projectNameTextBox.Text = value;
			}
		}
		
	}
}
