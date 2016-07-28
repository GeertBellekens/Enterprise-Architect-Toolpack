/*
 * User: wij
 * Date: 25/02/2011 4:52
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EADatabaseTransformer
{
	/// <summary>
	/// Description of AboutWindow.
	/// </summary>
	public partial class AboutWindow : Form
	{
		public AboutWindow()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
		
			// Set the assembly version
			this.Version.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			
			// Set the assembly Date
			this.AssemblyDate.Text = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToShortDateString();
			// Set the home page
			this.HomePage.Links.Add(0,this.HomePage.Text.Length,"http://bellekens.com/");
			// Set the author email adress
			this.AuthorEmail.Links.Add(0,this.AuthorEmail.Text.Length,"mailto:geert@bellekens.com");
		}
		/// <summary>
		/// Close the about window
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		void OKButtonClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void AuthorEmailLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
		}
		
		void HomePageLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
		}
	}
}
