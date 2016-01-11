
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UML=TSF.UmlToolingFramework.UML;
using SchemaBuilderFramework;
using UTF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using EAAddinFramework.SchemaBuilder;

namespace ECDMMessageComposer
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class ECDMMessageComposerAddin : EAAddinFramework.EAAddinBase
	{
		 // define menu constants
        const string menuName = "-&ECDM Message Composer";
        const string menuAbout = "&About";
		private UML.UMLModel model;
		private SchemaBuilderFactory schemaFactory;
		public ECDMMessageComposerAddin():base()
		{
			this.menuHeader = menuName;
			this.menuOptions = new string[]{menuAbout};
		}
		/// <summary>
        /// Initializes the model and schemaFactory with the new Repository object.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		public override void EA_FileOpen(EA.Repository Repository)
		{
			//initialize the model
			this.model = new UTF_EA.Model(Repository);
			this.schemaFactory = EASchemaBuilderFactory.getInstance(this.model);
		}
		/// <summary>
		/// Tell EA the name of this Schema composer add-in
		/// </summary>
		/// <param name="Repository">the repository object</param>
		/// <param name="displayName">the name that will be displayed</param>
		/// <returns>true</returns>
		public override bool EA_IsSchemaExporter(EA.Repository Repository, ref string displayName)
		{
			displayName = "ECDM Message Composer";
			return true;
		}
		
		/// <summary>
		/// The Add-in can optionally implement this function.
		/// Using the SchemaProfile interface an Add-in can adjust the capabilities of the Schema Composer. (See Automation Interface)
		/// </summary>
		/// <param name="Repository">the repository object</param>
		/// <param name="profile">the EA SchemaProfile object</param>
		public override void EA_GetProfileInfo(EA.Repository Repository, EA.SchemaProfile profile)
		{
            profile.AddExportFormat("ECDM Message");
		}
		
		/// <summary>
		/// If a user selects any of the outputs listed by the Add-in, this function will be invoked. 
		/// The function receives the Schema Composer automation interface, which can be used to traverse the schema.
		/// </summary>
		/// <param name="Repository"></param>
		/// <param name="composer"></param>
		public override void EA_GenerateFromSchema(EA.Repository Repository, EA.SchemaComposer composer, string exports)
		{
			Schema schema = this.schemaFactory.createSchema(composer);
			UML.Classes.Kernel.Package targetPackage = this.model.getUserSelectedPackage();
			schema.createSubsetModel(targetPackage);
			// then make a diagram and put the subset on it
			UML.Diagrams.ClassDiagram subsetDiagram = this.model.factory.createNewDiagram<UML.Diagrams.ClassDiagram>(targetPackage, targetPackage.name);
			subsetDiagram.save();	
			//put the subset elements on the new diagram
			foreach (SchemaElement schemaElement in schema.elements) 
			{
				if (schemaElement.subsetElement != null)
				{
					subsetDiagram.addToDiagram(schemaElement.subsetElement);
				}else
				{
					//we add the source element if the subset element doesn't exist.
					subsetDiagram.addToDiagram(schemaElement.sourceElement);
				}
			}
			//layout the diagram (this will open the diagram as well)
			subsetDiagram.autoLayout();
		}
	}
}