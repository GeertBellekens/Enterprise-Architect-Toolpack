
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
		public RenameWindow(string initialName)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.newNameTextBox.Text = initialName;

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
