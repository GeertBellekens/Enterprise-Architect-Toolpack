/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 9/06/2013
 * Time: 8:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TSF.UmlToolingFramework.EANavigator
{
	/// <summary>
	/// Description of QuickSearchBox.
	/// </summary>
	public partial class QuickSearchBox : UserControl
	{
		private NavigatorVisuals navigatorVisuals {get;set;}
		private ToolTip itemTooltip = new ToolTip();
		
		public QuickSearchBox()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.navigatorVisuals = NavigatorVisuals.getInstance();
			// set the height of the listbox
			this.listBox.Height = (this.maxDropDownItems +1) * this.listBox.ItemHeight;
		}
		public  override string Text
		{
			get {return this.textBox.Text;}
			set {this.textBox.Text = value;}
		}
		public override Color ForeColor {
			get { return this.textBox.ForeColor; }
			set { this.textBox.ForeColor = value; }
		}
		public bool DroppedDown
		{
			get {return this.listBox.Visible;}
			set {this.listBox.Visible = value;}
		}
		public object SelectedItem
		{
			get{return this.listBox.SelectedItem;}
			set{this.listBox.SelectedItem = value;}
		}
		public int SelectedIndex
		{
			get{return this.listBox.SelectedIndex;}
			set{this.listBox.SelectedIndex = value;}
		}
		public DrawMode DrawMode
		{
			get{return this.listBox.DrawMode;}
			set{this.listBox.DrawMode = value;}			
		}
		public bool FormattingEnabled
		{
			get{return this.listBox.FormattingEnabled;}
			set{this.listBox.FormattingEnabled = value;}
		}
		private int maxDropDownItems = 10;
		public int MaxDropDownItems 
		{	
			get{return this.maxDropDownItems;}
			set{this.maxDropDownItems = value;}
		}
		public event EventHandler TextUpdate;
		void TextBoxTextChanged(object sender, EventArgs e)
		{
			if (this.TextUpdate != null)
			{
				this.TextUpdate(this,e);
			}
		}
		public event EventHandler SelectionChangeCommitted;
		//TODO fire selectionchangecommitted
		void ListBoxClick(object sender, EventArgs e)
		{
			if (this.SelectionChangeCommitted != null
			    && this.listBox.SelectedItem != null)
			{
				this.DroppedDown = false;
				this.SelectionChangeCommitted(this,e);
			}
		}
		
		void ListBoxKeyDown(object sender, KeyEventArgs e)
		{
			if (this.SelectionChangeCommitted != null
				&& e.KeyCode == Keys.Return
			    && this.listBox.SelectedItem != null)
			{
				this.DroppedDown = false;
				this.SelectionChangeCommitted(this,e);
			}
		}
		public System.Windows.Forms.ListBox.ObjectCollection Items
		{
			get {return this.listBox.Items;}
		}
		void ListBoxDrawItem(object sender, DrawItemEventArgs e)
		{
			//get the selected element and its associated image
			if (0 <= e.Index && e.Index  < this.Items.Count)
			{
				UML.UMLItem selectedElement = this.Items[e.Index] as UML.UMLItem;
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
		

		

		

	}
}
