/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 29/05/2013
 * Time: 4:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace TSF.UmlToolingFramework.EANavigator
{
	partial class NavigatorIcons
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigatorIcons));
			this.NavigatorImageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// NavigatorImageList
			// 
			this.NavigatorImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("NavigatorImageList.ImageStream")));
			this.NavigatorImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.NavigatorImageList.Images.SetKeyName(0, "dummy.png");
			this.NavigatorImageList.Images.SetKeyName(1, "Attribute.png");
			this.NavigatorImageList.Images.SetKeyName(2, "Element.png");
			this.NavigatorImageList.Images.SetKeyName(3, "Operation.png");
			this.NavigatorImageList.Images.SetKeyName(4, "Diagram.png");
			this.NavigatorImageList.Images.SetKeyName(5, "Package_element.png");
			this.NavigatorImageList.Images.SetKeyName(6, "Primitive.png");
			this.NavigatorImageList.Images.SetKeyName(7, "Message.png");
			this.NavigatorImageList.Images.SetKeyName(8, "Action.png");
			this.NavigatorImageList.Images.SetKeyName(9, "SequenceDiagram.png");
			this.NavigatorImageList.Images.SetKeyName(10, "Class.png");
			this.NavigatorImageList.Images.SetKeyName(11, "StateMachine.png");
			this.NavigatorImageList.Images.SetKeyName(12, "Interaction.png");
			this.NavigatorImageList.Images.SetKeyName(13, "Activity.png");
			this.NavigatorImageList.Images.SetKeyName(14, "TaggedValue.png");
			this.NavigatorImageList.Images.SetKeyName(15, "AttributeTag.png");
			this.NavigatorImageList.Images.SetKeyName(16, "ElementTag.png");
			this.NavigatorImageList.Images.SetKeyName(17, "OperationTag.png");
			this.NavigatorImageList.Images.SetKeyName(18, "RelationTag.png");
			this.NavigatorImageList.Images.SetKeyName(19, "Parameter.png");
			this.NavigatorImageList.Images.SetKeyName(20, "Package.png");
			this.NavigatorImageList.Images.SetKeyName(21, "Package_action.png");
			this.NavigatorImageList.Images.SetKeyName(22, "Package_attribute.png");
			this.NavigatorImageList.Images.SetKeyName(23, "Package_operation.png");
			this.NavigatorImageList.Images.SetKeyName(24, "Package_parameter.png");
			this.NavigatorImageList.Images.SetKeyName(25, "Package_sequenceDiagram.png");
			this.NavigatorImageList.Images.SetKeyName(26, "Package_taggedValue.png");
			this.NavigatorImageList.Images.SetKeyName(27, "ParameterTag.png");
			this.NavigatorImageList.Images.SetKeyName(28, "Rootpackage.png");
			this.NavigatorImageList.Images.SetKeyName(29, "CommunicationDiagram.png");
			this.NavigatorImageList.Images.SetKeyName(30, "Enumeration.png");
			this.NavigatorImageList.Images.SetKeyName(31, "DataType.png");
			// 
			// NavigatorIcons
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "NavigatorIcons";
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ImageList NavigatorImageList;
	}
}
