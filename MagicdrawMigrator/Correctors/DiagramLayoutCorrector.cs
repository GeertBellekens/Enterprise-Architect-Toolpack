using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Windows.Forms;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using Excel = Microsoft.Office.Interop.Excel;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of DiagramLayoutCorrector.
	/// </summary>
	public class DiagramLayoutCorrector:MagicDrawCorrector
	{
		string packageTreeIDString;
		public DiagramLayoutCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
			packageTreeIDString = mdPackage.getPackageTreeIDString();
		}
		public override void correct()
		{
			int diagramCounter = 0;
		EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for diagrams'"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			//Loop each diagram
			foreach (var mdDiagramKeyValue in magicDrawReader.allDiagrams) 
			{
				//find the corresponding diagram in EA
				var ownerID = magicDrawReader.getDiagramOnwerID(mdDiagramKeyValue.Key);
				var mdDiagram = mdDiagramKeyValue.Value;
				string getCorrespondingdiagramSQL = 
				@"select d.Diagram_ID from ((t_diagram d
				inner join t_object o on o.Object_ID = d.ParentID)
				inner join t_objectproperties tv on (tv.Object_ID = o.Object_ID
													and tv.Property = 'md_guid'))
				where tv.Value = '"+ownerID+"'"
				+ " and d.Name = '"+mdDiagram.name+ "'"
				+ " and d.Package_ID in ("+packageTreeIDString+")"
				+@" union
				select d.Diagram_ID from (((t_diagram d
				inner join t_package p on d.Package_ID = p.Package_ID)
				inner join t_object o on o.ea_guid = p.ea_guid)
				inner join t_objectproperties tv on (tv.Object_ID = o.Object_ID
													and tv.Property = 'md_guid'))
				where d.ParentID = 0
				and tv.Value = '"+ownerID+"'"
				+ " and d.Name = '"+mdDiagram.name+ "'"
				+ " and d.Package_ID in ("+packageTreeIDString+")";
				var eaDiagrams = this.model.getDiagramsByQuery(getCorrespondingdiagramSQL);
				//loop the found diagrams
				foreach (var eaDiagram in eaDiagrams) 
				{
					diagramCounter++;
					EAOutputLogger.log(this.model,this.outputName
		                   ,string.Format("{0} Processing diagram number {1}: '{2}.{3}'"
		                   				,DateTime.Now.ToLongTimeString()
		                   				,diagramCounter
		                   				,eaDiagram.owner.name
		                   				,eaDiagram.name)
					      ,((TSF_EA.ElementWrapper)eaDiagram.owner).id
		                  ,LogTypeEnum.log);	
					//loop all diagramObjects in the mdDiagram that are not activity partitions
					foreach (var mdDiagramObject in mdDiagram.diagramObjects.Where(x => ! x.umlType.StartsWith("Swimlane")))
					{
						addElementToDiagram(mdDiagramObject,eaDiagram);
					}
					//save diagram in between?
					//eaDiagram.save();
					//then do all Activity Partitions
					foreach (var mdDiagramObject in mdDiagram.diagramObjects.Where(x => x.umlType.StartsWith("Swimlane")))
					{
						addElementToDiagram(mdDiagramObject,eaDiagram);
					}
					//save the diagram
					eaDiagram.save();
					//set the line styles
					correctLines(eaDiagram);
				}
				//if no diagram found in EA then report it as error
				if (!eaDiagrams.Any())
				{
					EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Could not find EA diagram for diagram with ID '{1}'"
                                  ,DateTime.Now.ToLongTimeString()
                                  , mdDiagramKeyValue.Key )
                   ,0
                  ,LogTypeEnum.error);	
				}
			}
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished corrections for diagrams'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);			
	
		}

		public void addElementToDiagram(MDDiagramObject mdDiagramObject, TSF_EA.Diagram eaDiagram)
		{
			//get the EA element represented by this MDDiagramObject
			string getEAElementSQL = @"select o.Object_ID from (t_object o
						inner join t_objectproperties tv on (tv.Object_ID = o.Object_ID
															and tv.Property = 'md_guid'))
						where tv.Value = '" + mdDiagramObject.mdID + "'";
			var eaElement = this.model.getElementWrappersByQuery(getEAElementSQL).FirstOrDefault();
			if (eaElement != null) {
				//first check if the elemnt is already on the diagram
				if (!eaDiagram.contains(eaElement)) {
					//for each of the elements on the diagram create the diagramobject with the appropriate link to the elemment and geometry.								
					var newDiagramObject = eaDiagram.addToDiagram(eaElement, mdDiagramObject.x, mdDiagramObject.y, mdDiagramObject.height, mdDiagramObject.width);
					//if the diagramObject is an ActivityPartition then we need to set its orientation to vertical
					if (mdDiagramObject.umlType.StartsWith("Swimlane"))
					{
						newDiagramObject.setOrientation(true);
						newDiagramObject.save();
					}
				}
			}

		}
		//correct the lines 
		void correctLines(TSF_EA.Diagram eaDiagram)
		{
			//first do the line styles
			foreach (var diagramLink in eaDiagram.diagramLinkWrappers) 
			{
				TSF_EA.LinkStyle linkStyle = getDefaultStyle(diagramLink);
				diagramLink.setStyle(linkStyle);
				diagramLink.save();
			}
			//then remove all the weights
			string removeWeightsSQL = @"update t_connector set PDATA3 = null
									where Connector_ID in (
									select c.Connector_ID from (t_connector c
									inner join t_object o on o.Object_ID = c.Start_Object_ID)
									where c.PDATA3 = '1'
									and o.Package_ID in ("+ packageTreeIDString+"))";
			this.model.executeSQL(removeWeightsSQL);
		}

		TSF_EA.LinkStyle getDefaultStyle(TSF_EA.DiagramLinkWrapper diagramLink)
		{
			switch (diagramLink.typeString) 
			{
				case "ControlFlow":
				case "ObjectFlow":
					return TSF_EA.LinkStyle.lsOrthogonalRoundedTree;
				case "Realization":
				case "Generalizaion":
				case "Realisation":	
					return TSF_EA.LinkStyle.lsTreeVerticalTree;
				case "NoteLink":
				case "Abstraction":
				case "Dependency":
				case "Usage":
				case "UseCase":
					return TSF_EA.LinkStyle.lsDirectMode;
				default:
					return TSF_EA.LinkStyle.lsOrthogonalSquareTree;
				
			}
		}
	}
}
