
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EADatabaseTransformer
{
	/// <summary>
	/// Description of RenameWindow.
	/// </summary>
	public partial class RenameWindow : Form
	{
		public RenameWindow()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public string newName
		{
			get
			{
				return this.newNameTextBox.Text;
			}
		}
		void OkButtonClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
