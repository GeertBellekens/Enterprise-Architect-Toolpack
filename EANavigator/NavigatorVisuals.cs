/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 29/05/2013
 * Time: 4:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;

namespace TSF.UmlToolingFramework.EANavigator
{
	/// <summary>
	/// Description of NavigatorIcons.
	/// </summary>
	public partial class NavigatorVisuals : UserControl
	{
		private int dummyIndex = 0;
		private int attributeIndex = 1;
		private int elementIndex = 2;
		private int operationIndex = 3;
		private int diagramIndex = 4;
		private int packageElementIndex = 5;
		private int primitiveIndex = 6;
		private int messagIndex = 7;
		private int actionIndex = 8;
		private int sequenceDiagramIndex = 9;
		private int classIndex = 10;
		private int stateMachineIndex = 11;
		private int interactionIndex = 12;
		private int activityIndex = 13;
		private int taggedValueIndex = 14;
		private int attributeTagIndex = 15;
		private int elementTagIndex = 16;
		private int operationTagIndex = 17;
		private int relationTagIndex = 18;
		private int parameterIndex = 19;
		private int packageIndex = 20;
		private int packageActionIndex = 21;
		private int packageAttributeIndex = 22;
		private int packageOperationIndex = 23;
		private int packageParameterIndex = 24;
		private int packageSequenceDiagramIndex = 25;
		private int packageTaggedValuesIndex = 26;
		private int parameterTagIndex = 27;
		private int rootPackageIndex = 28;
		private int communicationDiagramIndex = 29;
		private int enumerationIndex = 30;
		private int dataTypeIndex = 31;
		private int interfaceIndex = 32;
		private int signalIndex = 33;
		private int associationElementIndex = 34;
		private int packagingComponentIndex = 35;
		private int componentIndex = 36;
		private int providedInterfaceIndex = 37;
		private int requiredInterfaceIndex = 38;
		private int objectIndex = 39;
		private int portIndex = 40;
		private int artifactIndex = 41;
		private int partIndex = 42;
		private int collaborationIndex = 43;
		private int nodeIndex = 44;
		private int deploymentSpecificationIndex = 45;
		private int informationItemIndex = 46;
		private int actorIndex = 47;
		private int usecaseIndex = 48;
		private int boundaryIndex = 49;
		private int activityPartitionIndex = 50;
		private int decisionIndex = 51;
		private int eventIndex = 52;
		private int activityInitialIndex = 53;
		private int activityFinalIndex = 54;
		private int flowFinalNodeIndex = 55;
		private int synchronisationNodeIndex = 56;
		
		
		/// <summary>
		/// singleton instance
		/// </summary>
		private static NavigatorVisuals instance;
		/// <summary>
		/// the imageList to be used
		/// </summary>
		public ImageList imageList 
		{
			get
			{
				return this.NavigatorImageList;
			}
		}
		/// <summary>
		/// singleton getInstance
		/// </summary>
		/// <returns>the signle instance of this class</returns>
		public static NavigatorVisuals getInstance()
		{
			if (instance == null)
			{
				instance = new NavigatorVisuals();
			}
			return instance;
		}
		
		private NavigatorVisuals()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public Image getImage(UML.UMLItem element)
		{
			return this.imageList.Images[this.getImageIndex(element)];
		}
		
		public int getImageIndex(UML.UMLItem element)
		{
			int imageIndex;
			if (element is UML.Classes.Kernel.Property)
			{
				imageIndex = this.attributeIndex;
			}
			else if (element is UML.Classes.Kernel.Operation)
			{
				imageIndex = this.operationIndex;
			}
			else if (element is UML.Classes.Kernel.Package)
			{
				if (element.owner == null)
				{
					imageIndex = this.rootPackageIndex;
				}
				else
				{
					//packaging components are both components as packages.
					if (element is UTF_EA.ElementWrapper && ((UTF_EA.ElementWrapper)element).subType == "PackagingComponent")
					{
						imageIndex = this.packagingComponentIndex;
					}
					else
					{
						imageIndex = this.packageIndex;
					}
				}
			}
			else if (element is UML.Diagrams.SequenceDiagram)
			{
				imageIndex = this.sequenceDiagramIndex;
			}
			else if (element is UML.Diagrams.CommunicationDiagram)
			{
				imageIndex = this.communicationDiagramIndex;
			}
			else if( element is UML.Diagrams.Diagram)
			{
				imageIndex = this.diagramIndex;
			}
			else if (element is UML.Interactions.BasicInteractions.Interaction)
			{
				imageIndex = this.interactionIndex;
			}
			else if (element is UML.StateMachines.BehaviorStateMachines.StateMachine)
			{
				imageIndex = this.stateMachineIndex;
			}
			else if (element is UML.Activities.FundamentalActivities.Activity)
			{
				imageIndex = this.activityIndex;
			}
			else if (element is UML.Classes.Kernel.PrimitiveType)
			{
				imageIndex = this.primitiveIndex;
			}
			else if (element is UML.Classes.Kernel.Relationship)
			{
				imageIndex = this.messagIndex;
			}
			else if (element is UML.Actions.BasicActions.Action)
			{
				imageIndex = this.actionIndex;
			}
			else if (element is UML.Classes.Kernel.Class)
			{
				imageIndex = this.classIndex;
			}
			else if (element is UML.Classes.Kernel.Parameter)
			{
				imageIndex = this.parameterIndex;
			}
			else if (element is UML.Profiles.TaggedValue)
			{
				UML.Profiles.TaggedValue taggedValue = (UML.Profiles.TaggedValue)element;
				if (taggedValue.owner is UML.Classes.Kernel.Property)
				{
					imageIndex = this.attributeTagIndex;
				}
				else if (taggedValue.owner is UML.Classes.Kernel.Operation)
				{
					imageIndex = this.operationTagIndex;
				}
				else if (taggedValue.owner is UML.Classes.Kernel.Parameter)
				{
					imageIndex = this.parameterTagIndex;
				}
				else if (taggedValue.owner is UML.Classes.Kernel.Relationship)
				{
					imageIndex = this.relationTagIndex;
				}
				else if (taggedValue.owner is UML.Classes.Kernel.Element)
				{
					imageIndex = this.elementTagIndex;
				}
				else 
				{
					imageIndex = this.taggedValueIndex;
				}
			}
			else if (element is UML.Classes.Kernel.Enumeration)
			{
				imageIndex = this.enumerationIndex;
			}
			else if (element is UML.Classes.Kernel.DataType)
			{
				imageIndex = this.dataTypeIndex;
			}
			else if (element is UML.Classes.Interfaces.Interface)
			{
				imageIndex = this.interfaceIndex;
			}
			else if (element is TSF.UmlToolingFramework.Wrappers.EA.ElementWrapper)
			{
				//workaround to be able to display the correct icon, even if the element type hasn't been implemented in the UMLTooling Framework, of in case of EA specific element types such as requirement.
				TSF.UmlToolingFramework.Wrappers.EA.ElementWrapper elementWrapper = (TSF.UmlToolingFramework.Wrappers.EA.ElementWrapper)element;
				switch (elementWrapper.subType) 
				{
					case "Signal":
						imageIndex = this.signalIndex;
						break;
					case "Association":
						imageIndex = this.associationElementIndex;
						break;
					case "PackagingComponent": 
						imageIndex = this.packagingComponentIndex;
						break;
					case "Component":
						imageIndex = this.componentIndex;
						break;
					case "ProvidedInterface":
						imageIndex = this.providedInterfaceIndex;
						break;
					case "RequiredInterface":
						imageIndex = this.requiredInterfaceIndex;
						break;
					case "Object":
						imageIndex = this.objectIndex;
						break;
					case "Port":
						imageIndex = this.portIndex;
						break;
					case "Artifact":
						imageIndex = this.artifactIndex;
						break;
					case "Part":
						imageIndex = this.partIndex;
						break;
					case "Collaboration":
					case "CollaborationOccurrence":
						imageIndex = this.collaborationIndex;
						break;
					case "Node":
					case "Device":
					case "ExecutionEnvironment":
						imageIndex = this.nodeIndex;
						break;
					case "DeploymentSpecification":
						imageIndex = this.deploymentSpecificationIndex;
						break;
					case "InformationItem":
						imageIndex = this.informationItemIndex;
						break;
					case "Actor":
						imageIndex = this.actorIndex;
						break;	
					case "UseCase":
						imageIndex = this.usecaseIndex;
						break;	
					case "Boundary":
						imageIndex = this.boundaryIndex;
						break;
					case "ActivityPartition":
						imageIndex = this.activityPartitionIndex;
						break;
					case "CentralBufferNode":
						imageIndex = this.classIndex;
						break;	
					case "Decision":
					case "MergeNode":
						imageIndex = this.decisionIndex;
						break;
					case "Event":
						imageIndex = this.eventIndex;
						break;
					case "ActivityInitial":
						imageIndex = this.activityInitialIndex;
    					break;
    				case "ActivityFinal":
						imageIndex = this.activityFinalIndex;
    					break;
    				case "FlowFinal":
  						imageIndex = this.flowFinalNodeIndex;
    					break;
    				case "SynchronisationNode":
    					imageIndex = this.synchronisationNodeIndex;
    					break;
					default:
						imageIndex = this.elementIndex;
						break;
				}
			}
			else
			{
				imageIndex = this.elementIndex;
			}
			return imageIndex;
		}
		public int getFolderImageIndex(string menuOptionName)
		{
			int imageIndex;
			switch (menuOptionName)
			{
				case EAAddin.menuActions:
					imageIndex = this.packageActionIndex;
					break;
				case EAAddin.menuAttributes:
					imageIndex = this.packageAttributeIndex;
					break;
				case EAAddin.menuDependentTaggedValues:
					imageIndex = this.packageTaggedValuesIndex;
					break;
				case EAAddin.menuDiagramOperations:
					imageIndex = this.packageOperationIndex;
					break;
				case EAAddin.menuDiagrams:
					imageIndex = this.packageSequenceDiagramIndex;
					break;
				case EAAddin.menuImplementation:
					imageIndex = this.packageSequenceDiagramIndex;
					break;
				case EAAddin.menuOperation:
					imageIndex = this.packageOperationIndex;
					break;
				case EAAddin.menuImplementedOperations:
					imageIndex = this.packageOperationIndex;
					break;
				case EAAddin.menuParameters:
					imageIndex = this.packageParameterIndex;
					break;
				case EAAddin.menuParameterTypes:
					imageIndex = this.packageElementIndex;
					break;
				default:
					if( menuOptionName.StartsWith(EAAddin.taggedValueMenuPrefix)
				   	&& menuOptionName.EndsWith(EAAddin.taggedValueMenuSuffix))
				  	{
				   		imageIndex = this.packageElementIndex;
				  	}
					else
					{
						//just in case we forgot a case				 
						imageIndex = this.packageElementIndex;
					}
					break;
			}
			return imageIndex;
		}
		public int getDummyIndex()
		{
			return this.dummyIndex;
		}
				/// <summary>
		/// returns a string represenation of the stereotypes
		/// </summary>
		/// <param name="element">the element containing the stereotype</param>
		/// <returns>a string containing the stereotype «stereo1,ste..»</returns>
		private string getStereotypeString(UML.UMLItem element)
		{
			string stereotypeString = string.Empty;
			int maxLength = 20;
			if (element.stereotypes.Count > 0)
			{
				stereotypeString = "«";
				foreach (UML.Profiles.Stereotype stereotype in element.stereotypes) 
				{
					if (stereotypeString.Length > 1)
					{
						stereotypeString += ", ";
					}
					stereotypeString += stereotype.name;
					if (stereotypeString.Length > maxLength)
					{
						stereotypeString = stereotypeString.Substring(0,maxLength- 2) + "..";
					}
				}
				stereotypeString += "» ";
			}
			return stereotypeString;
		}
		/// <summary>
		/// returns the name to show as node name for this element
		/// </summary>
		/// <param name="element"></param>
		public string getNodeName(UML.UMLItem element)
		{
			
			string name = string.Empty;
			if (element != null)
			{
				name = this.getStereotypeString(element);
				name += element.name;
			}
			if (element is UML.Classes.Kernel.Parameter)
			{
				UML.Classes.Kernel.Parameter parameter = (UML.Classes.Kernel.Parameter)element;
				if (parameter.direction != UML.Classes.Kernel.ParameterDirectionKind._return)
				{
					name = parameter.name + " (" + this.getNodeName(parameter.operation) + ")";
				}
			}
			else if (element is UML.Classes.Kernel.Feature)
			{
				UML.Classes.Kernel.Feature feature = (UML.Classes.Kernel.Feature)element;
				UML.Classes.Kernel.Element owner = feature.owner;
				if (owner != null)
				{
					name = owner.name;
				}
				else
				{
					name = "[owner missing]";
				}
				name += "." + this.getStereotypeString(element)+ feature.name;
			}
			else if (element is UML.Profiles.TaggedValue)
			{
				UML.Profiles.TaggedValue taggedValue = (UML.Profiles.TaggedValue)element;
				if (taggedValue.owner.name.Length > 0)
				{
					name = taggedValue.owner.name + "." + taggedValue.name;
				}
			}
			return name;
			    
		}
	}
}
