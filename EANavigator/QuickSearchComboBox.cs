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
		private NavigatorVisuals navigatorVisuals {get;set;}
		private ToolTip itemTooltip = new ToolTip();
		private TextBox textBox;
		public QuickSearchComboBox()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.navigatorVisuals = NavigatorVisuals.getInstance();
			

			this.DrawMode = DrawMode.OwnerDrawFixed;
			this.DrawItem += new DrawItemEventHandler(QuickSearchComboBox_DrawItem);
			
			//textBox
			this.textBox = new TextBox();
			this.textBox.Location = this.Location;
			this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox.Size = new Size(this.Width - 20, this.Height);
			this.Controls.Add(this.textBox);
			this.textBox.TextChanged += new System.EventHandler(this.textBoxTextChanged);
						
		}
		
		void textBoxTextChanged(object sender, EventArgs e)
		{
			//this.OnTextChanged(e);
			this.OnTextUpdate(e);
		}
		/// <summary>
		/// drawns the icon and the name of the element in the dropdown list
		/// </summary>
		/// <param name="sender">the sender</param>
		/// <param name="e">the params</param>
		private void QuickSearchComboBox_DrawItem(object sender, DrawItemEventArgs e)
        { 
			//get the selected element and its associated image
			if (0 <= e.Index && e.Index  < this.Items.Count)
			{
				UML.Extended.UMLItem selectedElement = this.Items[e.Index] as UML.Extended.UMLItem;
				if (selectedElement != null)
				{
					Image elementImage = this.navigatorVisuals.getImage(selectedElement);
					//draw standard background and focusrectangle
					e.DrawBackground();
					e.DrawFocusRectangle();		
								
					//draw the name of the element
					e.Graphics.DrawString(this.navigatorVisuals.getNodeName(selectedElement), e.Font, new SolidBrush(e.ForeColor),
		                                  new Point(elementImage.Width + 2,e.Bounds.Y)); 
		            // draw the icon
		            e.Graphics.DrawImage(elementImage, new Point(e.Bounds.X, e.Bounds.Y));   
		            
		            // draw tooltip
		            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
		            {
		            	 this.itemTooltip.Show(selectedElement.fqn,
                            this, e.Bounds.Right, e.Bounds.Bottom);
		            }
		            else
		            {
		            	this.itemTooltip.Hide(this);
		            }
				}
			}
             
        }
		protected override void OnDropDownClosed(EventArgs e)
		{
			base.OnDropDownClosed(e);
			this.itemTooltip.Hide(this);
		}
		public override string Text {
			get { return this.textBox.Text; }
			set { this.textBox.Text = value; }
		}
		
		
	}
}
