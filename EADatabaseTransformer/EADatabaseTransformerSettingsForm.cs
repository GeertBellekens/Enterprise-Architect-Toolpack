/*
 * Created by SharpDevelop.
 * User: LaptopGeert
 * Date: 28/07/2016
 * Time: 13:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace EADatabaseTransformer
{
	/// <summary>
	/// Description of EADatabaseTransformerSettingsForm.
	/// </summary>
	public partial class EADatabaseTransformerSettingsForm : Form
	{
		private EADatabaseTransformerSettings settings;
		public EADatabaseTransformerSettingsForm(EADatabaseTransformerSettings settings)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.settings = settings;
			loadData();
		}
		private void loadData()
		{
			this.abbreviationsFileTextBox.Text = settings.abbreviationsPath;
		}
		private void saveChanges()
		{
			settings.abbreviationsPath = this.abbreviationsFileTextBox.Text;
			settings.save();
		}
		void OkButtonClick(object sender, EventArgs e)
		{
			this.saveChanges();
			this.Close();
		}
		void ApplyButtonClick(object sender, EventArgs e)
		{
			this.saveChanges();
		}
		void BrowseAbbreviationsfileButtonClick(object sender, EventArgs e)
		{
			//let the user select a file
            OpenFileDialog browseAbbrevFileDialog = new OpenFileDialog();
            browseAbbrevFileDialog.Filter = "Character Separated Values files (.csv)|*.csv";
            browseAbbrevFileDialog.FilterIndex = 1;
            browseAbbrevFileDialog.Multiselect = false;
            var dialogResult = browseAbbrevFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
            	//if the user selected the file then put the filename in the abbreviationsfileTextBox
                this.abbreviationsFileTextBox.Text = browseAbbrevFileDialog.FileName;
            }
		}
	}
}
