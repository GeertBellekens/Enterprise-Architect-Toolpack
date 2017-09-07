using System.Collections.Generic;
using System.Linq;
using System;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.Diagnostics;
using System.Xml;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of NotesCorrector.
	/// </summary>
	public class NotesCorrector:MagicDrawCorrector
	{
		string packageTreeIDString;
		public NotesCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
			packageTreeIDString = mdPackage.getPackageTreeIDString();
		}
		
		
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting fix notes'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                   ,LogTypeEnum.log);
			
			// get al the diagrams
			foreach (var mdDiagramKeyValue in magicDrawReader.allDiagrams) 
			{
				var mdDiagram = mdDiagramKeyValue.Value;
				var ownerID = magicDrawReader.getDiagramOnwerID(mdDiagramKeyValue.Key);
				var parentElement = getElementByMDid(ownerID); //this is a package, must be the diagram
				
				//find the corresponding diagram in EA
				string getCorrespondingdiagramSQL = 
				@"select d.Diagram_ID from ((t_diagram d
				inner join t_object o on o.Object_ID = d.ParentID)
				inner join t_objectproperties tv on (tv.Object_ID = o.Object_ID
													and tv.Property = 'md_guid'))
				where tv.Value = '"+ownerID+"'"
				+ " and d.Name = '"+mdDiagram.name.Replace("'","''")+ "'"
				+@" union
				select d.Diagram_ID from (((t_diagram d
				inner join t_package p on d.Package_ID = p.Package_ID)
				inner join t_object o on o.ea_guid = p.ea_guid)
				inner join t_objectproperties tv on (tv.Object_ID = o.Object_ID
													and tv.Property = 'md_guid'))
				where d.ParentID = 0
				and tv.Value = '"+ownerID+"'"
				+ " and d.Name = '"+mdDiagram.name.Replace("'","''")+ "'";
	
				var eaDiagrams = this.model.getDiagramsByQuery(getCorrespondingdiagramSQL);
				
				TSF_EA.Diagram eaDiagram = eaDiagrams.FirstOrDefault();
				
				foreach (var mdDiagramNote in mdDiagram.diagramNotes)
				{
					if (parentElement != null)
					{
						TSF_EA.NoteComment newNote = this.model.factory.createNewElement<TSF_EA.NoteComment>(parentElement, string.Empty);
						
						newNote.ownedComments.FirstOrDefault().body = mdDiagramNote.text;
						newNote.save();
						eaDiagram.addToDiagram(newNote,mdDiagramNote.x, mdDiagramNote.y, mdDiagramNote.bottom, mdDiagramNote.right);
						var linkedElement = getElementByMDid(mdDiagramNote.linkedElement);
						if (linkedElement != null)
						{
							TSF_EA.ConnectorWrapper noteLink = newNote.addOwnedElement<TSF_EA.ConnectorWrapper>(string.Empty, "NoteLink");
							noteLink.target = linkedElement;
							noteLink.save();
							EAOutputLogger.log(this.model,this.outputName
		                   ,string.Format("{0} Create note '{1}' for '{2}'"
		                                  ,DateTime.Now.ToLongTimeString()
		                                  ,newNote.ownedComments.FirstOrDefault().body
		                                 ,getElementByMDid(mdDiagramNote.linkedElement).name)
							             ,linkedElement.id
		                  ,LogTypeEnum.log);
						}
						else
						{
							EAOutputLogger.log(this.model,this.outputName
		                   ,string.Format("{0} Create note '{1}'"
		                                  ,DateTime.Now.ToLongTimeString()
		                                  ,newNote.ownedComments.FirstOrDefault().body)
		                             
		                   ,newNote.id
		                  ,LogTypeEnum.log);
						}
					}
					else
					{
						EAOutputLogger.log(this.model,this.outputName
		                   ,string.Format("{0} No parent for note '{1}'"
		                                  ,DateTime.Now.ToLongTimeString()
		                                  ,mdDiagramNote.text)
		                             
		                   ,0
		                  ,LogTypeEnum.log);
						
						
					}

				}
			}
			
			
			//Log finished
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished fix notes"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
		}
	}
}
