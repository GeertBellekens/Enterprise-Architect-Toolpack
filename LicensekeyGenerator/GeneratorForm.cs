/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 5/12/2014
 * Time: 4:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using EAAddinFramework.Licensing;

namespace LicensekeyGenerator
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class GeneratorForm : Form
	{
		private KeyGenerator keyGenerator;
		public GeneratorForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.setkeysizeValues();
			this.keyGenerator = new KeyGenerator();
			
		}
		
		/// <summary>
		/// RSA keys can be from 384 bits to 16384 bit with 8 bits incremements
		/// </summary>
		private void setkeysizeValues()
		{
			this.keySizeDropDown.Items.AddRange(KeyGenerator.validKeySizes().Cast<object>().ToArray());
			if (this.keySizeDropDown.Items.Count > 0)
			{
				//select the first one
				this.keySizeDropDown.SelectedIndex = 0;
			}
		}
		

		
		void generatKeyPairButtonClick(object sender, EventArgs e)
		{
			string publicKey;
			string privateKey;
			KeyGenerator.getKeyPair(int.Parse(this.keySizeDropDown.Text),out publicKey, out privateKey);
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				string keypathFolder = folderBrowserDialog.SelectedPath;
				System.IO.File.WriteAllText(keypathFolder + @"\publicKey.txt", publicKey);
				System.IO.File.WriteAllText(keypathFolder + @"\privateKey.txt", privateKey);
			}
		}
		
		void GenerateButtonClick(object sender, EventArgs e)
		{
			this.keyTextBox.Clear();
			int i = 1;
			//generate the licenses and show them int he textbox
			foreach (License license in this.keyGenerator.generateLicenses(this.applicationDropDown.Text
			                                                               ,this.validUntilDatePicker.Value
			                                                               ,this.floatingCheckbox.Checked
			                                                               ,this.clientTextBox.Text
			                                                               ,decimal.ToInt32(this.numberOfLicensesUpDown.Value)))
			{
				this.keyTextBox.Text += "Key nbr " +i.ToString() +":" + Environment.NewLine;
				this.keyTextBox.Text += (license.key + Environment.NewLine);
				i++;								
			}

		}
	}
}
