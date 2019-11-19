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
	partial class NavigatorVisuals
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigatorVisuals));
            this.NavigatorImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // NavigatorImageList
            // 
            this.NavigatorImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("NavigatorImageList.ImageStream")));
            this.NavigatorImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.NavigatorImageList.Images.SetKeyName(0, "dummy.png");
            this.NavigatorImageList.Images.SetKeyName(1, "Attribute.png");
            this.NavigatorImageList.Images.SetKeyName(2, "Operation.png");
            this.NavigatorImageList.Images.SetKeyName(3, "Element.png");
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
            this.NavigatorImageList.Images.SetKeyName(32, "Interface.png");
            this.NavigatorImageList.Images.SetKeyName(33, "Signal.png");
            this.NavigatorImageList.Images.SetKeyName(34, "Association_Element.png");
            this.NavigatorImageList.Images.SetKeyName(35, "PackagingComponent.png");
            this.NavigatorImageList.Images.SetKeyName(36, "Component.png");
            this.NavigatorImageList.Images.SetKeyName(37, "ProvidedInterface.png");
            this.NavigatorImageList.Images.SetKeyName(38, "RequiredInterface.png");
            this.NavigatorImageList.Images.SetKeyName(39, "Object.png");
            this.NavigatorImageList.Images.SetKeyName(40, "Port.png");
            this.NavigatorImageList.Images.SetKeyName(41, "Artifact.png");
            this.NavigatorImageList.Images.SetKeyName(42, "Part.png");
            this.NavigatorImageList.Images.SetKeyName(43, "Collaboration.png");
            this.NavigatorImageList.Images.SetKeyName(44, "Node.png");
            this.NavigatorImageList.Images.SetKeyName(45, "DeploymentSpecification.png");
            this.NavigatorImageList.Images.SetKeyName(46, "InformationItem.png");
            this.NavigatorImageList.Images.SetKeyName(47, "Actor.png");
            this.NavigatorImageList.Images.SetKeyName(48, "Usecase.png");
            this.NavigatorImageList.Images.SetKeyName(49, "Boundary.png");
            this.NavigatorImageList.Images.SetKeyName(50, "ActivityPartition.png");
            this.NavigatorImageList.Images.SetKeyName(51, "Decision.png");
            this.NavigatorImageList.Images.SetKeyName(52, "Event.png");
            this.NavigatorImageList.Images.SetKeyName(53, "ActivityInitial.png");
            this.NavigatorImageList.Images.SetKeyName(54, "ActivityFinal.png");
            this.NavigatorImageList.Images.SetKeyName(55, "FlowFinal.png");
            this.NavigatorImageList.Images.SetKeyName(56, "SynchronisationNode.png");
            this.NavigatorImageList.Images.SetKeyName(57, "InteruptableActivityRegion.png");
            this.NavigatorImageList.Images.SetKeyName(58, "ExpansionRegion.png");
            this.NavigatorImageList.Images.SetKeyName(59, "ExceptionHandler.png");
            this.NavigatorImageList.Images.SetKeyName(60, "ObjectNode.png");
            this.NavigatorImageList.Images.SetKeyName(61, "Synchronisation.png");
            this.NavigatorImageList.Images.SetKeyName(62, "LifeLine.png");
            this.NavigatorImageList.Images.SetKeyName(63, "Gate.png");
            this.NavigatorImageList.Images.SetKeyName(64, "Fragment.png");
            this.NavigatorImageList.Images.SetKeyName(65, "InteractionState.png");
            this.NavigatorImageList.Images.SetKeyName(66, "State.png");
            this.NavigatorImageList.Images.SetKeyName(67, "EntryPoint.png");
            this.NavigatorImageList.Images.SetKeyName(68, "TimeLine.png");
            this.NavigatorImageList.Images.SetKeyName(69, "Requirement.png");
            this.NavigatorImageList.Images.SetKeyName(70, "Feature.png");
            this.NavigatorImageList.Images.SetKeyName(71, "Risk.png");
            this.NavigatorImageList.Images.SetKeyName(72, "Issue.png");
            this.NavigatorImageList.Images.SetKeyName(73, "Change.png");
            this.NavigatorImageList.Images.SetKeyName(74, "Screen.png");
            this.NavigatorImageList.Images.SetKeyName(75, "UIControl.png");
            this.NavigatorImageList.Images.SetKeyName(76, "Note.png");
            this.NavigatorImageList.Images.SetKeyName(77, "Constraint.png");
            this.NavigatorImageList.Images.SetKeyName(78, "Text.png");
            this.NavigatorImageList.Images.SetKeyName(79, "Legend.png");
            this.NavigatorImageList.Images.SetKeyName(80, "DiagramNotes.png");
            this.NavigatorImageList.Images.SetKeyName(81, "Hyperlink.png");
            this.NavigatorImageList.Images.SetKeyName(82, "ActivityDiagram.png");
            this.NavigatorImageList.Images.SetKeyName(83, "ComponentDiagram.png");
            this.NavigatorImageList.Images.SetKeyName(84, "CompositeStructureDiagram.png");
            this.NavigatorImageList.Images.SetKeyName(85, "CustomDiagram.png");
            this.NavigatorImageList.Images.SetKeyName(86, "DeploymentDiagram.png");
            this.NavigatorImageList.Images.SetKeyName(87, "InterActionOverviewDiagram.png");
            this.NavigatorImageList.Images.SetKeyName(88, "ObjectDiagram.png");
            this.NavigatorImageList.Images.SetKeyName(89, "PackageDiagram.png");
            this.NavigatorImageList.Images.SetKeyName(90, "StateMachineDiagram.png");
            this.NavigatorImageList.Images.SetKeyName(91, "Timingdiagram.png");
            this.NavigatorImageList.Images.SetKeyName(92, "UseCaseDiagram.png");
            this.NavigatorImageList.Images.SetKeyName(93, "Package_composite.png");
            this.NavigatorImageList.Images.SetKeyName(94, "Package_Diagram.png");
            this.NavigatorImageList.Images.SetKeyName(95, "Package_informationItem.png");
            this.NavigatorImageList.Images.SetKeyName(96, "AssociationClass.png");
            this.NavigatorImageList.Images.SetKeyName(97, "Package_AssociationClass.png");
            this.NavigatorImageList.Images.SetKeyName(98, "EnumerationLiteral.png");
            // 
            // NavigatorVisuals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "NavigatorVisuals";
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.ImageList NavigatorImageList;
	}
}
