using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Windows.Forms;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of AddCrossMDzipRelationsCorrector.
	/// </summary>
	public class AddCrossMDzipRelationsCorrector:MagicDrawCorrector
	{
		public AddCrossMDzipRelationsCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}

		#region implemented abstract members of MagicDrawCorrector

		public override void correct()
		{
			//loop all cros mdzip relations
			foreach (var crossRelation in magicDrawReader.allCrossMDzipRelations) 
			{
				//check if the relation doesn't exist yet
				string sqlGetExistingRelations = @"select c.Connector_ID from (t_connector c
												inner join t_connectortag tv on( c.Connector_ID = tv.ElementID
															and tv.Property = 'md_guid'))
												where tv.VALUE = '"+crossRelation.Key+"'";
				if (! this.model.getRelationsByQuery(sqlGetExistingRelations).Any())
				{
					MDElementRelation relation = crossRelation.Value;
					//find source
					var source = this.getElementByMDid(relation.sourceMDGUID);
					//find target
					var target = this.getElementByMDid(relation.targetMDGUID);
					//create relation
					if (source != null 
					    && target != null)
					{
						var newRelation = this.model.factory.createNewElement(source,relation.name,relation.relationType) as TSF_EA.ConnectorWrapper;
						if (newRelation != null)
						{
							newRelation.target = target;
							newRelation.save();
							//save md_guid tag
							newRelation.addTaggedValue("md_guid",crossRelation.Key);
						}
					}
				}

			}
		}

		#endregion
	}
}
