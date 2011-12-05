using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
    const string menuFQN = "&To FQN";
    private UTF_EA.Model model = null;
    private NavigatorControl navigatorControl;

	public EAAddin():base()
	{
		this.menuHeader = menuName;
		
		this.menuOptions = new string[] {menuOperation,menuDiagrams};
		
	}
	public override void EA_OnPostInitialized(EA.Repository Repository)
	{
		// initialize the model
        this.model = new UTF_EA.Model(Repository);
        if (this.navigatorControl != null)
        {
        	this.navigatorControl.clear();
        }
	}
	public override void EA_FileOpen(EA.Repository Repository)
	{
		if (this.navigatorControl != null)
        {
        	this.navigatorControl.clear();
        }
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
        	
        	switch (MenuName)
        	{
        	case "":
        		//return top level menu option
        		return this.menuHeader;
        	case menuName:
        		List<string> menuOptionsList = new List<string>();
        		// get the selected element from the model
        		UML.Classes.Kernel.Element selectedElement = this.model.selectedElement;
        		//add the menuoptions depending on the type of element
        		menuOptionsList.AddRange(getMenuOptions(selectedElement));
        		// add FQN menu to all options
        		menuOptionsList.Add(menuFQN);
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
	    /// returns the options depending on the type of the element
	    /// </summary>
	    /// <param name="element">element</param>
	    /// <returns>a list of navigator menu options depending on the type of element</returns>
        internal static List<string> getMenuOptions (UML.UMLItem element)
        {
        		List<string> menuOptionsList = new List<string>();
        		
        		if (element is UML.Classes.Kernel.Operation)
        		{
        			menuOptionsList.Add(menuDiagrams);
        			menuOptionsList.Add(menuParameterTypes);
        			menuOptionsList.Add(menuActions);
        			menuOptionsList.Add(menuImplementation);
        			
        		}else if (element is UML.Interactions.BasicInteractions.Message)
        		{
        			menuOptionsList.Add(menuOperation);
        			menuOptionsList.Add(menuDiagrams);
        			menuOptionsList.Add(menuParameterTypes);
        			menuOptionsList.Add(menuImplementation);
        			
        		}else if (element is UML.Classes.Kernel.PrimitiveType)
        		{
        			//add no options for primitive types	
        		}
        		else if (element is UML.Classes.Kernel.Type)
        		{
        			menuOptionsList.Add(menuAttributes);
        			menuOptionsList.Add(menuParameters);
        			
        		}else if (element is UML.Classes.Kernel.Property)
        		{
        			menuOptionsList.Add(menuClassifier);
        		}
        		return menuOptionsList;
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

	       case menuFQN:
	        	this.selectFQN();
	        	break;
	       case menuAbout :
	            new AboutWindow().ShowDialog();
	            break;
           default:
	            UML.UMLItem selectedItem = this.model.selectedElement as UML.UMLItem;
	            if (selectedItem != null)
	            {
		            List<UML.UMLItem> elementsToNavigate = this.getElementsToNavigate(ItemName,selectedItem);
		            if (elementsToNavigate.Count == 1)
		            {
		            	elementsToNavigate[0].open();
		            }
		            else if (elementsToNavigate.Count > 1)
		            {
		            	NavigatorList dialog = new NavigatorList(elementsToNavigate);
		        		dialog.Show();
		            }
	            }
            break;
            
      }
    }
	public List<UML.UMLItem> getElementsToNavigate(string menuChoice,UML.UMLItem parentElement )
	{
		
		List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
		 switch(menuChoice) 
		 {
	        case menuOperation   : 
		 		elementsToNavigate = this.getOperation(parentElement);
	            break;
	        case menuDiagrams : 
	            elementsToNavigate = this.getDiagrams(parentElement);
	            break;
	        case menuParameterTypes:
	            elementsToNavigate = this.getParameterTypes(parentElement);
	            break;
	        case menuActions:
	            elementsToNavigate = this.getActions(parentElement);
	            break;
	        case menuAttributes:
	            elementsToNavigate = this.getAttributes(parentElement);
	        	break;
	        case menuParameters:
	        	elementsToNavigate = this.getParameters(parentElement);
	        	break;
	        case menuClassifier:
	        	elementsToNavigate = this.getClassifier(parentElement);
	        	break;
	        case menuImplementation:
	        	elementsToNavigate = this.getImplementation(parentElement);
	        	break;
			
		}
		 return elementsToNavigate;
		 
	}
	public override void EA_OnContextItemChanged(global::EA.Repository Repository, string GUID, global::EA.ObjectType ot)
    {
    	if (this.navigatorControl == null)
    	{
    		this.navigatorControl = Repository.AddWindow("Navigate","TSF.UmlToolingFramework.EANavigator.NavigatorControl") as NavigatorControl;
    		this.navigatorControl.BeforeExpand += new TreeViewCancelEventHandler( this.NavigatorTreeBeforeExpand);
    	}
    	if (this.navigatorControl != null && this.model != null)
    	{
    		if (this.model.selectedItem != null)
    		{
    			this.navigatorControl.setElement(this.model.selectedItem as UML.UMLItem);
    		}
    		
    	}
    }
    /// <summary>
    /// select the element that matches the fqn in the clipboard
    /// </summary>
	private void selectFQN()
	{
		UML.UMLItem item = null;
		if(Clipboard.ContainsText())
		{
		   
			item = this.model.getItemFromFQN(Clipboard.GetText());
			if (item != null)
			{
				item.select();
			} else
			{
				MessageBox.Show("Could not find item" + Environment.NewLine + Clipboard.GetText());
			}
		}else
		{
			MessageBox.Show("Clipboard does nog contain text." + Environment.NewLine + "Please selecte a valid FQN");
		}
		
	}
    // opens all  diagrams
    private List<UML.UMLItem> getDiagrams(UML.UMLItem parentElement)
    {
        List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
    	UML.Classes.Kernel.Operation selectedOperation = this.getSelectedOperation(parentElement);
        // if the selectedOperation is null we try to get the operation from the selected message
        if (selectedOperation != null)
        {
        	elementsToNavigate.AddRange( selectedOperation.getUsingDiagrams<UML.Diagrams.Diagram>());
        }
        return elementsToNavigate;
    }

   //selects the operation that is called by the selected message in the project browser 
   private List<UML.UMLItem> getOperation(UML.UMLItem parentElement)
   {
   	   List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
	   UML.Classes.Kernel.Operation calledOperation = this.getSelectedOperation(parentElement);
	   if (null != calledOperation)
	   {
	   	elementsToNavigate.Add(calledOperation);
	   }
	   return elementsToNavigate;
   }
   /// <summary>
   /// Opens the types of the parameters of the selected operation
   /// </summary>
   private List<UML.UMLItem> getParameterTypes(UML.UMLItem parentElement)
   {
   		List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
	   	UML.Classes.Kernel.Operation selectedOperation = this.getSelectedOperation(parentElement);
	   	HashSet<UML.Classes.Kernel.Parameter> parameters = selectedOperation.ownedParameters;
	   	foreach (UML.Classes.Kernel.Parameter parameter in parameters) 
	   	{
	   		elementsToNavigate.Add(parameter.type);
	   	}
	   	return elementsToNavigate;
   }
   /// <summary>
   /// Opens the attributes of that use the selected element as type
   /// </summary>
   private List<UML.UMLItem> getAttributes(UML.UMLItem parentElement)
   {
	   	List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
	   	UML.Classes.Kernel.Type selectedType = parentElement as UML.Classes.Kernel.Type;
	   	// get the attributes that use the selected Type as type
	   	elementsToNavigate.AddRange( selectedType.getDependentTypedElements<UML.Classes.Kernel.Property>());
	   	return elementsToNavigate;
   }
   /// <summary>
   /// Opens the parameters that use the selected element as type
   /// </summary>
   private List<UML.UMLItem> getParameters(UML.UMLItem parentElement)
   {
	   	List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
	   	UML.Classes.Kernel.Type selectedType = parentElement as UML.Classes.Kernel.Type;
	   	// get the parameters that use the selected classifier as type
	   	elementsToNavigate.AddRange(selectedType.getDependentTypedElements<UML.Classes.Kernel.Parameter>());
	   	return elementsToNavigate;
   }
   /// <summary>
   /// opens the CallOperationActions that call te selected operation
   /// </summary>
   private List<UML.UMLItem> getActions(UML.UMLItem parentElement)
   {
   	List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
   	UML.Classes.Kernel.Operation selectedOperation = this.getSelectedOperation( parentElement);
   	if( selectedOperation != null)
   	{
		elementsToNavigate.AddRange(selectedOperation.getDependentCallOperationActions());
    }
	return elementsToNavigate;
   }
   /// <summary>
   /// selects the implementation of the operation in the project browser, and opens all owned diagrams of the implementation.
   /// </summary>
   private List<UML.UMLItem> getImplementation(UML.UMLItem parentElement)
   {
   	List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
   	UML.Classes.Kernel.Operation selectedOperation = this.getSelectedOperation(parentElement);
   	if (selectedOperation != null )
   	{
   		foreach ( UML.CommonBehaviors.BasicBehaviors.Behavior implementation in selectedOperation.methods)
   		{
   			//select the behavior in the project browser
	   		elementsToNavigate.AddRange(implementation.ownedDiagrams);
   		}
   	}
   	return elementsToNavigate;
   }
   
   /// <summary>
   /// Opens the type of the attribute
   /// </summary>
   private List<UML.UMLItem> getClassifier(UML.UMLItem parentElement)
   {
   		List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
   		UML.Classes.Kernel.Property selectedAttribute = parentElement as UML.Classes.Kernel.Property;
   		if (null != selectedAttribute)
   		{
   			elementsToNavigate.Add(selectedAttribute.type);
   		}
   		return elementsToNavigate;
   }
   /// <summary>
   /// Gets the selected operation from the model, either directly or through the selected message
   /// </summary>
   /// <returns>the selected operation, or the operation called by the selected message</returns>
   private UML.Classes.Kernel.Operation getSelectedOperation(UML.UMLItem parentElement)
   {
   	   // try if the the users selected an operation
   	   UML.Classes.Kernel.Operation selectedOperation = parentElement as UML.Classes.Kernel.Operation;
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
   
   
   
   void NavigatorTreeBeforeExpand(object sender, TreeViewCancelEventArgs e)
   {
   	if (e.Node.Tag == null)
   	{
   		UML.UMLItem parentElement = e.Node.Parent.Tag as UML.UMLItem;
   		if (parentElement != null)
   		{
	   		string option = "&" + e.Node.Text;
	   		List<UML.UMLItem> subElements = this.getElementsToNavigate(option,parentElement);
	   		this.navigatorControl.setSubNodes(e.Node,subElements);
   		}
   	}
   }

  }
}
