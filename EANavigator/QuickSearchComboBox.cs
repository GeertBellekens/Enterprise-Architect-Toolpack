
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;




namespace TSF.UmlToolingFramework.EANavigator
{
	/// <summary>
	/// Description of QuickSearchComboBox.
	/// </summary>
	public class QuickSearchComboBox:ComboBox
	{
		public ImageList imageList {get;set;}

		public QuickSearchComboBox()
		{
//			this.DrawMode = DrawMode.OwnerDrawFixed;
//			this.DrawItem += new DrawItemEventHandler(QuickSearchComboBox_DrawItem);
		}
		
//		private void QuickSearchComboBox_DrawItem(object sender, DrawItemEventArgs e)
//        {    
//            // Let's highlight the currently selected item like any well 
//            // behaved combo box should
//            e.Graphics.FillRectangle(Brushes.Bisque, e.Bounds);                
//            e.Graphics.DrawString(arr[e.Index], myFont, Brushes.Blue, 
//                                  new Point(imageArr[e.Index].Width*2,e.Bounds.Y)); 
//           
//            e.Graphics.DrawImage(imageArr[e.Index], new Point(e.Bounds.X, e.Bounds.Y));   
//            
//            //is the mouse hovering over a combobox item??            
//            if((e.State & DrawItemState.Focus)==0)
//            {                                                    
//                //this code keeps the last item drawn from having a Bisque background. 
//                e.Graphics.FillRectangle(Brushes.White, e.Bounds);                    
//                e.Graphics.DrawString(arr[e.Index], myFont, Brushes.Blue, 
//                                      new Point(imageArr[e.Index].Width*2,e.Bounds.Y));
//                e.Graphics.DrawImage(imageArr[e.Index], new Point(e.Bounds.X, e.Bounds.Y)); 
//            }    
//        }  
	}
}
