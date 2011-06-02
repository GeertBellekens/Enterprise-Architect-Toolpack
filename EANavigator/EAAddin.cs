using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UML=TSF.UmlToolingFramework.UML;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;

namespace TSF.UmlToolingFramework.EANavigator
{
public class EAAddin:EAAddinFramework.EAAddinBase
{
	const string menuName = "-&Navigate";
    const string menuOperation = "&Operation";
    const string menuDiagrams = "&Diagrams";
    const string menuAbout = "&About EA Navigator";
    const string menuClassifier = "&Type";
    const string menuParameterTypes = "&Parameter Types";
    const string menuAttributes = "&Dependent Attributes";
    const string menuParameters = "&Dependent Parameters";
    const string menuActions = "&Calling Actions";
    const string menuImplementation = "&Implementation";
    private UTF_EA.Model model = null;
    

	public EAAddin():base()
	{
		this.menuHeader = menuName;
		
		this.menuOptions = new string[] {menuOperation,menuDiagrams};
	}
    
	    /// <summary>
        /// The EA_GetMenuItems event enables the Add-In to provide the Enterprise Architect user interface with additional Add-In menu options in various context and main menus. When a user selects an Add-In menu option, an event is raised and passed back to the Add-In that originally defined that menu option.
        /// This event is raised just before Enterprise Architect has to show particular menu options to the user, and its use is described in the Define Menu Items topic.
        /// Also look at:
        /// - EA_MenuClick
        /// - EA_GetMenuState.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="MenuLocation">String representing the part of the user interface that brought up the menu. 
        /// Can be TreeView, MainMenu or Diagram.</param>
        /// <param name="MenuName">The name of the parent menu for which sub-items are to be defined. In the case of the top-level menu it is an empty string.</param>
        /// <returns>One of the following types:
        /// - A string indicating the label for a single menu option.
        /// - An array of strings indicating a multiple menu options.
        /// - Empty (Visual Basic/VB.NET) or null (C#) to indicate that no menu should be displayed.
        /// In the case of the top-level menu it should be a single string or an array containing only one item, or Empty/null.</returns>
        public override object EA_GetMenuItems(EA.Repository Repository, string MenuLocation, string MenuName)
        {
        	// initialize the model
        	this.model = new UTF_EA.Model(Repository);
        	switch (MenuName)
        	{
        	case "":
        		//return top level menu option
        		return this.menuHeader;
        	case menuName:
        		List<string> menuOptionsList = new List<string>();
        		// get the selected element from the model
        		UML.Classes.Kernel.Element selectedElement = this.model.selectedElement;
        		if (selectedElement is UML.Classes.Kernel.Operation)
        		{
        			menuOptionsList.Add(menuDiagrams);
        			menuOptionsList.Add(menuParameterTypes);
        			menuOptionsList.Add(menuActions);
        			menuOptionsList.Add(menuImplementation);
        			
        		}else if (selectedElement is UML.Interactions.BasicInteractions.Message)
        		{
        			menuOptionsList.Add(menuOperation);
        			menuOptionsList.Add(menuDiagrams);
        			menuOptionsList.Add(menuParameterTypes);
        			menuOptionsList.Add(menuImplementation);
        			
        		}else if (selectedElement is UML.Classes.Kernel.Type)
        		{
        			menuOptionsList.Add(menuAttributes);
        			menuOptionsList.Add(menuParameters);
        			
        		}else if (selectedElement is UML.Classes.Kernel.Property)
        		{
        			menuOptionsList.Add(menuClassifier);
        		}
        		// show about menu option only when called from main menu
        		if (MenuLocation == "MainMenu")
        		{
        			menuOptionsList.Add(menuAbout);
        			        		}
        		// return submenu options
        		return menuOptionsList.ToArray();
        	default: 
        		return string.Empty;
        	 }
            
        }

	/// <summary>
	/// Execute the actual functions
	/// </summary>
	/// <param name="Repository">the repository</param>
	/// <param name="Location">menu location</param>
	/// <param name="MenuName">menu name</param>
	/// <param name="ItemName">option clicked</param>
    public override void EA_MenuClick( global::EA.Repository Repository, 
                              String Location, 
                              String MenuName, String ItemName )
    {
		
      switch(ItemName) {
        case menuOperation   : 
            this.selectOperation();        
            break;
        case menuDiagrams : 
            this.openDiagrams();      
            break;
        case menuParameterTypes:
            this.openParameterTypes();
            break;
        case menuActions:
            this.openActions();
            break;
        case menuAttributes:
        	this.openAttributes();
        	break;
        case menuParameters:
        	this.OpenParameters();
        	break;
        case menuClassifier:
        	this.openClassifier();
        	break;
        case menuImplementation:
        	this.openImplementation();
        	break;
        case menuAbout :
            new AboutWindow().ShowDialog();
            break;
            
      }
    }
    // opens all  diagrams
    private void openDiagrams()
    {
        
    	UML.Classes.Kernel.Operation selectedOperation = this.getSelectedOperation();
        // if the selectedOperation is null we try to get the operation from the selected message
        if (selectedOperation != null)
        {
	        HashSet<UML.Diagrams.Diagram> usingDiagrams = selectedOperation.getUsingDiagrams<UML.Diagrams.Diagram>();
	
	        NavigatorList dialog = new NavigatorList(usingDiagrams.ToList());
	        dialog.Show();
        }
    }

   //selects the operation that is called by the selected message in the project browser 
   private void selectOperation()
   {
	   UML.Classes.Kernel.Operation calledOperation = this.getSelectedOperation();
	   if (null != calledOperation){
	       this.model.selectElement(calledOperation);
	   }
   }
   /// <summary>
   /// Opens the types of the parameters of the selected operation
   /// </summary>
   private void openParameterTypes()
   {
	   	UML.Classes.Kernel.Operation selectedOperation = this.getSelectedOperation();
	   	HashSet<UML.Classes.Kernel.Parameter> parameters = selectedOperation.ownedParameters;
	   	List<UML.Classes.Kernel.NamedElement> parameterTypes = new List<UML.Classes.Kernel.NamedElement>();
	   	foreach (UML.Classes.Kernel.Parameter parameter in parameters) 
	   	{
	   		parameterTypes.Add(parameter.type);
	   	}
	   	NavigatorList dialog = new NavigatorList(parameterTypes);
	   	dialog.Show();
   }
   /// <summary>
   /// Opens the attributes of that use the selected element as type
   /// </summary>
   private void openAttributes()
   {
   	UML.Classes.Kernel.Type selectedType = this.model.selectedElement as UML.Classes.Kernel.Type;
   	// get the attributes that use the selected Type as type
   	List<UML.Classes.Kernel.NamedElement> attributes = selectedType.getDependentTypedElements<UML.Classes.Kernel.Property>().Cast<UML.Classes.Kernel.NamedElement>().ToList();

   	NavigatorList dialog = new NavigatorList(attributes);
   	dialog.Show();
   }
   /// <summary>
   /// Opens the parameters that use the selected element as type
   /// </summary>
   private void OpenParameters()
   {
   	UML.Classes.Kernel.Type selectedType = this.model.selectedElement as UML.Classes.Kernel.Type;
   	// get the parameters that use the selected classifier as type
   	List<UML.Classes.Kernel.NamedElement> parameters = selectedType.getDependentTypedElements<UML.Classes.Kernel.Parameter>().Cast<UML.Classes.Kernel.NamedElement>().ToList();

   	NavigatorList dialog = new NavigatorList(parameters);
   	dialog.Show();
   }
   /// <summary>
   /// opens the CallOperationActions that call te selected operation
   /// </summary>
   private void openActions()
   {
   	UML.Classes.Kernel.Operation selectedOperation = this.getSelectedOperation();
   	HashSet<UML.Actions.BasicActions.CallOperationAction> callingActions = selectedOperation.getDependentCallOperationActions();
   	NavigatorList dialog = new NavigatorList(callingActions.Cast<UML.Classes.Kernel.NamedElement>().ToList());
   	dialog.Show();
   }
   /// <summary>
   /// selects the implementation of the operation in the project browser, and opens all owned diagrams of the implementation.
   /// </summary>
   private void openImplementation()
   {
   	UML.Classes.Kernel.Operation selectedOperation = this.getSelectedOperation();
   	if (selectedOperation != null )
   	{
   		foreach ( UML.CommonBehaviors.BasicBehaviors.Behavior implementation in selectedOperation.methods)
   		{
   			//select the behavior in the project browser
	   		this.model.selectElement(implementation);
	   		//open all owned diagrams
	   		foreach (UML.Diagrams.Diagram diagram in implementation.ownedDiagrams) 
	   		{
	   			diagram.open();
	   		}
   		}
   	}
   }
   
   /// <summary>
   /// Opens the type of the attribute
   /// </summary>
   private void openClassifier()
   {
   		UML.Classes.Kernel.Property selectedAttribute = this.model.selectedElement as UML.Classes.Kernel.Property;
   		if (null != selectedAttribute)
   		{
   			this.model.selectElement(selectedAttribute.type);
   		}
   }
   /// <summary>
   /// Gets the selected operation from the model, either directly or through the selected message
   /// </summary>
   /// <returns>the selected operation, or the operation called by the selected message</returns>
   private UML.Classes.Kernel.Operation getSelectedOperation()
   {
   	   // try if the the users selected an operation
   	   UML.Classes.Kernel.Operation selectedOperation = this.model.selectedElement as UML.Classes.Kernel.Operation;
   	   //selected element is not an operation, try to get he operation from the selected message
   	   if (null == selectedOperation)
   	   {
	       UML.Interactions.BasicInteractions.Message selectedMessage = this.model.selectedElement as UML.Interactions.BasicInteractions.Message;
	       
	       if (null != selectedMessage)
	       {
		        selectedOperation = selectedMessage.calledOperation;
	       
		       if (selectedOperation == null)
		       {
		       	System.Windows.Forms.MessageBox.Show("Could not find operation!\nMake sure you either select:\n-An Operation in the project browser \n-A message in a sequence diagram that calls an existing Operation"
		        	                                     ,"Missing Operation!",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
		       }
	       }
   	   }
       return selectedOperation;
   }
   

  }
}
