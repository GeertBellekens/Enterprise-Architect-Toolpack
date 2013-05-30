/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 29/05/2013
 * Time: 5:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TSF.UmlToolingFramework.EANavigator
{
	/// <summary>
	/// Description of QuickSearchComboBox.
	/// </summary>
	public partial class QuickSearchComboBox : ComboBox
	{
		private NavigatorIcons navigatorIcons {get;set;}
		public QuickSearchComboBox()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.navigatorIcons = NavigatorIcons.getInstance();
			

			this.DrawMode = DrawMode.OwnerDrawFixed;
			this.DrawItem += new DrawItemEventHandler(QuickSearchComboBox_DrawItem);
		}
		/// <summary>
		/// drawns the icon and the name of the element in the dropdown list
		/// </summary>
		/// <param name="sender">the sender</param>
		/// <param name="e">the params</param>
		private void QuickSearchComboBox_DrawItem(object sender, DrawItemEventArgs e)
        { 
			//get the selected element and its associated image
			UML.UMLItem selectedElement = (UML.UMLItem)this.Items[e.Index];
			Image elementImage = this.navigatorIcons.getImage(selectedElement);
			//draw standard background and focusrectangle
			e.DrawBackground();
			e.DrawFocusRectangle();		
						
			//draw the name of the element
			e.Graphics.DrawString(selectedElement.name, e.Font, new SolidBrush(e.ForeColor),
                                  new Point(elementImage.Width + 2,e.Bounds.Y)); 
            // draw the icon
            e.Graphics.DrawImage(elementImage, new Point(e.Bounds.X, e.Bounds.Y));   
             
        }  
	}
}
