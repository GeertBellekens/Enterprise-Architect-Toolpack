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
		
		void ValidateButtonClick(object sender, EventArgs e)
		{
			string publicKey = @"<DSAKeyValue><P>hthfjDblWyX41cxvWEBwtmp02jOzBxSCSOFwMrlqdaNIYSn5nORPTXe+iV4mnvTg1taoAeao8t60T9BOQsSFu7AFgSCfviWS6mdk83iXkiA2pY74rc4frw==</P><Q>mkBpY5bMZ21PssiAkMf1b/s+O3s=</Q><G>FsxPsCco0DXMs6zNVywshs5CGiT3ThPbPKlqnY9aP4zX2QDwiW/M0ATc/6ERMtQgC7MPiZQOj6RiywvHuM3N2n/znIH4rc/4CtbVV5kM28IcoCPAz0xL4w==</G><Y>HAUdHYBwrISc5d4l8/jjR7gt+y67iHC7ReOMXDACUyGpzVp9S2ML98ElfuhUDJ0CnMrFrIHtfB2nL3tR2YQ1sgP2MG9yKfMwjkwsrOmHS/geNs9m4Fjnug==</Y><J>38rp34SlY2zMSi5gG5xXIrS1n2nWvdiJY0Il0mYegG+qtxm2FoXigxXrRqkp133nbKz94mwPAUNkuS4yP2BeSDid4Ko=</J><Seed>Wp+kyp+INeZpu+8ZQlSK9uliM2Y=</Seed><PgenCounter>sg==</PgenCounter></DSAKeyValue>";
			License license = new License(this.keyTextBox.Text, publicKey);
			if (license.isValid)
			{
				this.applicationDropDown.Text = license.application;
				this.validUntilDatePicker.Value = license.validUntil;
				this.floatingCheckbox.Checked = license.floating;
				this.clientTextBox.Text = license.client;
				this.keyTextBox.ForeColor = Color.LawnGreen;
			}
			else
			{
				this.applicationDropDown.Text = string.Empty;
				this.validUntilDatePicker.Checked = false;
				this.floatingCheckbox.Checked = false;
				this.clientTextBox.Clear();
				this.keyTextBox.ForeColor = Color.Red;
			}
			
		}
	}
}
