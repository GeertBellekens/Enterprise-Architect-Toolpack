using System;
using System.Drawing;
using System.Windows.Forms;

namespace GlossaryManager {

	public partial class AboutWindow : Form	{
		public AboutWindow() {
			InitializeComponent();

			this.Version.Text = System.Reflection.Assembly.GetExecutingAssembly()
        .GetName().Version.ToString();
			this.AssemblyDate.Text = System.IO.File.GetLastWriteTime(
        System.Reflection.Assembly.GetExecutingAssembly().Location
      ).ToShortDateString();

			this.HomePage.Links.Add(
        0,this.HomePage.Text.Length,"http://bellekens.com/enterprise-data-dictionary/"
      );
			this.AuthorEmail.Links.Add(
        0,this.AuthorEmail.Text.Length,"mailto:geert@bellekens.com"
      );
		}

		void OKButtonClick(object sender, EventArgs e) {
			this.Close();
		}
		
		void AuthorEmailLinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
			System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
		}
		
		void HomePageLinkClicked(object sender, LinkLabelLinkClickedEventArgs e){
			System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
		}
	}
}
