/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 20/09/2013
 * Time: 20:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using EAAddinFramework;
using System.Windows.Forms;
using System.Xml;

namespace BalsamiqConnector
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class BalsamiqConnectorAddin:EAAddinBase
	{
		public BalsamiqConnectorAddin():base()
		{
			this.menuHeader = "-&Balsamiq";
			this.menuOptions =  new String[]{"&Test"};
		}
		
		public override void EA_MenuClick(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
		{
			switch (ItemName) 
			{
				case "&Test":
					this.test();
					break;
			}
		}
		
		private void test()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Balsamiq files (*.bmml)|*.bmml|XML files (*.xml)|*.xml";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string balsamiqFileName = openFileDialog.FileName;
				XmlDocument balsamiqXmlDocument = new XmlDocument();
				balsamiqXmlDocument.Load(balsamiqFileName);
				XmlNode controlsNode = balsamiqXmlDocument.SelectSingleNode("//controls");
				foreach (XmlNode controlNode in controlsNode.ChildNodes) 
				{
					string name = controlsNode.Name;	
				}
			}
		}
		
	}
}