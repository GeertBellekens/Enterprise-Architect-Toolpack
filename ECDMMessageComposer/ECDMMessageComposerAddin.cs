
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UML=TSF.UmlToolingFramework.UML;
using SchemaBuilderFramework;
using UTF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using EAAddinFramework.SchemaBuilder;
using EAAddinFramework.Utilities;

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
		/// initialize the add-in class
		/// </summary>
		/// <param name="Repository"></param>
		private void initialize(EA.Repository Repository)
		{
			//initialize the model
			this.model = new UTF_EA.Model(Repository);
			this.schemaFactory = EASchemaBuilderFactory.getInstance(this.model);
		}
		
		/// <summary>
		/// only needed for the about menu
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="MenuLocation">String representing the part of the user interface that brought up the menu. 
        /// Can be TreeView, MainMenu or Diagram.</param>
        /// <param name="MenuName">The name of the parent menu for which sub-items must be defined. In the case of the top-level menu it is an empty string.</param>
        /// <param name="ItemName">The name of the option actually clicked, for example, Create a New Invoice.</param>
		public override void EA_MenuClick(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
		{
			switch(ItemName) 
			{
		       case menuAbout :
		            new AboutWindow().ShowDialog();
		            break;
			}
		}

		/// <summary>
		/// Tell EA the name of this Schema composer add-in
		/// </summary>
		/// <param name="Repository">the repository object</param>
		/// <param name="displayName">the name that will be displayed</param>
		/// <returns>true</returns>
		//public override bool EA_IsSchemaExporter(EA.Repository Repository, ref string displayName)
		public bool EA_IsSchemaExporter(EA.Repository Repository, ref string displayName)
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
			//for some reason EA seems to sometimes create a new instance of the add-in.
			//to avoid nullpointer exception we inititialize the model and factory again if needed
			if (this.model == null || this.schemaFactory == null)
			{
				this.initialize(Repository);
			}
			//make sure the tagged value types we need are there
			if (this.schemaFactory != null)
			{
				((EASchemaBuilderFactory)this.schemaFactory).checkTaggedValueTypes();
			}
			//tell EA our export format name
			if (profile != null)
			{
            	profile.AddExportFormat("ECDM Message");
			}
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
			UML.Classes.Kernel.Element selectedElement = this.model.getUserSelectedElement(new List<string>{"Class", "Package"});
			var targetPackage = selectedElement as UML.Classes.Kernel.Package;
			if (targetPackage != null )
			{

				this.createNewMessageSubset(schema, targetPackage);
			}
			else
			{
				this.updateMessageSubset(schema, selectedElement as UML.Classes.Kernel.Class);
				//refresh all open diagram to show the changes
				//TODO: go through framework
				Repository.RefreshOpenDiagrams(true);
			}

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="schema"></param>
		/// <param name="messageElement"></param>
		private void updateMessageSubset(Schema schema, UML.Classes.Kernel.Class messageElement)
		{
			if (messageElement != null)
			{
				schema.updateSubsetModel(messageElement);
			}
		}
		/// <summary>
		/// Creates a new message subset from the given schema in the given targetPackage
		/// </summary>
		/// <param name="schema">the Schema to generate a message subset from</param>
		/// <param name="targetPackage">the Package to create the new Message subset in</param>
		private void createNewMessageSubset(Schema schema, UML.Classes.Kernel.Package targetPackage)
		{
			if (targetPackage != null)
			{

				//Logger.log("before ECDMMessageComposerAddin::schema.createSubsetModel");
				schema.createSubsetModel(targetPackage);
				//Logger.log("after ECDMMessageComposerAddin::schema.createSubsetModel");
				// then make a diagram and put the subset on it
				UML.Diagrams.ClassDiagram subsetDiagram = this.model.factory.createNewDiagram<UML.Diagrams.ClassDiagram>(targetPackage, targetPackage.name);
				subsetDiagram.save();
				//Logger.log("after ECDMMessageComposerAddin::create subsetDiagram");				
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
				//Logger.log("after ECDMMessageComposerAddin::adding elements");	
				//layout the diagram (this will open the diagram as well)
				subsetDiagram.autoLayout();
				//Logger.log("after ECDMMessageComposerAddin::autolayout");
			}
		}
	}
}