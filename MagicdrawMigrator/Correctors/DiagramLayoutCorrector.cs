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
		public DiagramLayoutCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}
		public override void correct()
		{
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
				+@" union
				select d.Diagram_ID from (((t_diagram d
				inner join t_package p on d.Package_ID = p.Package_ID)
				inner join t_object o on o.ea_guid = p.ea_guid)
				inner join t_objectproperties tv on (tv.Object_ID = o.Object_ID
													and tv.Property = 'md_guid'))
				where d.ParentID = 0
				and tv.Value = '"+ownerID+"'"
				+ " and d.Name = '"+mdDiagram.name+ "'";
				var eaDiagrams = this.model.getDiagramsByQuery(getCorrespondingdiagramSQL);
				//loop the found diagrams
				foreach (var eaDiagram in eaDiagrams) 
				{
					EAOutputLogger.log(this.model,this.outputName
		                   ,string.Format("{0} Processing diagram '{1}.{2}'"
		                   				,DateTime.Now.ToLongTimeString()
		                   				,eaDiagram.owner.name
		                   				,eaDiagram.name)
		                   ,0
		                  ,LogTypeEnum.log);	
					//loop all diagramObjects in the mdDiagram
					foreach (var mdDiagramObject in mdDiagram.diagramObjects) 
					{
						//get the EA element represented by this MDDiagramObject
						string getEAElementSQL = 
						@"select o.Object_ID from (t_object o
						inner join t_objectproperties tv on (tv.Object_ID = o.Object_ID
															and tv.Property = 'md_guid'))
						where tv.Value = '" + mdDiagramObject.mdID + "'";
						var eaElement = this.model.getElementWrappersByQuery(getEAElementSQL).FirstOrDefault();
						if (eaElement != null)
						{
							//first check if the elemnt is already on the diagram
							if (!eaDiagram.contains(eaElement))
							{
								//for each of the elements on the diagram create the diagramobject with the appropriate link to the elemment and geometry.								
								eaDiagram.addToDiagram(eaElement,mdDiagramObject.x,mdDiagramObject.y,mdDiagramObject.height,mdDiagramObject.width);
							}
						}

					}
				//save the diagram
				eaDiagram.save();
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
	}
}
