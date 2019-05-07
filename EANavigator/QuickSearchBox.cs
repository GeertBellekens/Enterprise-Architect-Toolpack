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
			set {
				this.listBox.Visible = value;
				if (! value) this.itemTooltip.Hide(this);}
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
            this.TextUpdate?.Invoke(this, e);
        }
		public event EventHandler SelectionChangeCommitted;
		
		void ListBoxClick(object sender, EventArgs e)
		{
			if (this.SelectionChangeCommitted != null
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
		            	 //this.itemTooltip.Show(selectedElement.fqn,this, e.Bounds.Right, e.Bounds.Bottom);
		            	 this.itemTooltip.Show(selectedElement.fqn,this, e.Bounds.Left + 20, (int) (e.Bounds.Bottom + e.Bounds.Height * 1.5));
		            }
		            else
		            {
		            	this.itemTooltip.Hide(this);
		            }
				}
			}
		}
		

		

		/// <summary>
		/// calculates the on which item the mouse is currently pointing.
		/// </summary>
		/// <returns>the index of the item on which the mouse is pointing</returns>
		private int mousePositionItemIndex()
		{
			int indexToSelect = -1;
			
			Point relativePosition = this.listBox.PointToClient(Cursor.Position);
			if (relativePosition.Y >= 0)
			{
				indexToSelect = relativePosition.Y / this.listBox.ItemHeight;
			}
			if (indexToSelect >= this.listBox.Items.Count)
			{
				indexToSelect = -1;
			}						
			return indexToSelect;
		}
		
		void ListBoxMouseEnter(object sender, EventArgs e)
		{
			this.listBox.SelectedIndex = this.mousePositionItemIndex();
		}
		
		
		void ListBoxMouseLeave(object sender, EventArgs e)
		{
			this.SelectedIndex = -1;
			this.itemTooltip.Hide(this);
		}
		
		void ListBoxMouseMove(object sender, MouseEventArgs e)
		{
			int mouseIndex = this.mousePositionItemIndex();
			if (mouseIndex < this.listBox.Items.Count)
			{
				this.listBox.SelectedIndex = mouseIndex;
			}
		}
		
		
		void ListBoxPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (this.SelectionChangeCommitted != null
				&& e.KeyCode == Keys.Return
			    && this.listBox.SelectedItem != null)
			{
				this.DroppedDown = false;
				this.SelectionChangeCommitted(this,e);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				this.DroppedDown = false;
			}
		}
		
		void TextBoxKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
			{
				this.DroppedDown = true;
				this.listBox.Focus();
				this.SelectedIndex = 0;
			}
			
		}
	}
}
