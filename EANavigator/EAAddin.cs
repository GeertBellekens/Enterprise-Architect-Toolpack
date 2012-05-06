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
	internal const string menuName = "-&Navigate";
    internal const string menuOperation = "&Operation";
    internal const string menuDiagrams = "&Diagrams";
    internal const string menuAbout = "&About EA Navigator";
    //internal const string menuClassifier = "&Type";
    internal const string menuParameterTypes = "&Parameter Types";
    internal const string menuAttributes = "&Dependent Attributes";
    internal const string menuParameters = "&Dependent Parameters";
    internal const string menuActions = "&Calling Actions";
    internal const string menuImplementation = "&Implementation";
    internal const string menuFQN = "&To FQN";
    internal const string menuDiagramOperations = "&Operations";
    internal const string menuImplementedOperations = "&Implemented Operation";
    internal const string menuDependentTaggedValues = "&Referencing Tagged Values";
    
    internal const string taggedValueMenuSuffix = " Tags";
    internal const string taggedValueMenuPrefix = "&";
    
    internal const string ownerMenuPrefix = "&Owner: ";
    internal const string typeMenuPrefix = "&Type: ";
    
    const string eaGUIDTagname = "ea_guid";
    const string eaOperationGUIDTagName = "operation_guid";
    
    private UTF_EA.Model model = null;
    private NavigatorControl navigatorControl;
    private bool fullyLoaded = false;

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
        this.fullyLoaded = true;
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
    		UML.UMLItem selectedElement = this.model.selectedItem;
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
    internal static string getOwnerMenuName(UML.UMLItem element)
    {
    	string ownerMenuname = ownerMenuPrefix;
    	if (element.owner != null)
    	{
    		ownerMenuname += element.owner.name;
    	}
    	return ownerMenuname;
    }
    internal static string getTypeMenuName(UML.UMLItem element)
    {
    	string typeMenuname = typeMenuPrefix;
    	UML.Classes.Kernel.Property attribute = element as UML.Classes.Kernel.Property;
    	if (attribute != null
    	   && attribute.type != null)
    	{
    		typeMenuname += attribute.type.name;
    	}
    	return typeMenuname;
    }
    /// <summary>
    /// returns the options depending on the type of the element
    /// </summary>
    /// <param name="element">element</param>
    /// <returns>a list of navigator menu options depending on the type of element</returns>
    internal static List<string> getMenuOptions (UML.UMLItem element)
    {
    		List<string> menuOptionsList = new List<string>();
    		if (element is UML.UMLItem
    		   && element.owner != null)
    		{
    			menuOptionsList.Add(getOwnerMenuName(element));
    		}
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
    			
    		}
    		else if (element is UML.Actions.BasicActions.CallOperationAction)
    		{
    			menuOptionsList.Add(menuOperation);
    		}
    		else if (element is UML.Classes.Kernel.PrimitiveType)
    		{
    			//add no options for primitive types	
    		}
    		else if (element is UML.Classes.Kernel.Type)
    		{
    			menuOptionsList.Add(menuAttributes);
    			menuOptionsList.Add(menuParameters);
    			
    		}else if (element is UML.Classes.Kernel.Property)
    		{
    			menuOptionsList.Add(getTypeMenuName(element));
    		}
    		else if (element is UML.Diagrams.SequenceDiagram)
    		{
    			menuOptionsList.Add(menuDiagramOperations);
    		}
    		//now for behavior, could be a type as well
    		if (element is UML.CommonBehaviors.BasicBehaviors.Behavior
    		    || element is UML.Diagrams.Diagram && 
    		    ((UML.Diagrams.Diagram)element).owner is UML.CommonBehaviors.BasicBehaviors.Behavior)
    		{
    			menuOptionsList.Add(menuImplementedOperations);
    		}
    		//tagged values can be added to any UML element
    		if (element is UML.Classes.Kernel.Element)
    		{
    			menuOptionsList.AddRange(getTaggedValueMenuItems(element as UML.Classes.Kernel.Element));
    		}
    		//tagged values can reference only ElementWrappers, attributes and operations
    		if (element is TSF.UmlToolingFramework.Wrappers.EA.ElementWrapper 
    		    || element is UML.Classes.Kernel.Property
    		    || element is UML.Classes.Kernel.Operation)
    		{
    			menuOptionsList.Add(menuDependentTaggedValues);
    		}
    		filterMenuOptions(menuOptionsList);
    		return menuOptionsList;
    }
    private static void filterMenuOptions(List<string> menuOptionsList)
    {
    	for (int i = menuOptionsList.Count -1 ;i>= 0 ;i--)
    	{
    		string settingName = menuOptionsList[i]+"Enabled";
    		//string configvalue = System.Configuration.ConfigurationSettings.AppSettings[settingName];
    		string configvalue2 = System.Configuration.ConfigurationManager.AppSettings[settingName];
    	}
    }
    /// <summary>
    /// creates a list of menuoptions based on the names of tagged values that reference another UML item
    /// </summary>
    /// <param name="ownerElement">the owner of the tagged values</param>
    /// <returns>menuoptions for tagged values</returns>
    private static List<string> getTaggedValueMenuItems(UML.Classes.Kernel.Element ownerElement)
    {
    	List<string> menuItems = new List<string>();
    	foreach (UML.Profiles.TaggedValue taggedValue in ownerElement.taggedValues) 
    	{
    		if (taggedValue.tagValue is UML.UMLItem)
    		{
    			string menuName = taggedValueMenuName(taggedValue.name);
    			//we don't want the "system" tagged values ea_guid and operation_guid to show up in the navigator
    			if(menuName != taggedValueMenuName(eaGUIDTagname)
    			   && menuName != taggedValueMenuName(eaOperationGUIDTagName)
    			   && !menuItems.Contains(menuName))
    			{
    				menuItems.Add(menuName);
    			}
    		}
    	}
    	return menuItems;
    }
    /// <summary>
    /// adds the menu prefix and menu suffix to the tagged value name
    /// </summary>
    /// <param name="taggedValueName">the tagged value name</param>
    /// <returns>the menu name for the tagged value name</returns>
    private static string taggedValueMenuName(string taggedValueName)
    {
    	return (taggedValueMenuPrefix + taggedValueName + taggedValueMenuSuffix);
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
	            UML.UMLItem selectedItem = this.model.selectedItem;
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
		            else
		            {
		            	string message = selectedItem.name + " does not have any " + ItemName.Replace("&",string.Empty);
		            	MessageBox.Show(message,"Nothing to navigate",MessageBoxButtons.OK,MessageBoxIcon.Warning);
		            }
	            }
            break;
            
      }
    }
	/// <summary>
	/// returns the items to navigate based on the given element and chosen option.
	/// </summary>
	/// <param name="menuChoice">the chosen option</param>
	/// <param name="parentElement">the elememnt</param>
	/// <returns>list of items to navigate</returns>
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
	        case menuImplementation:
	        	elementsToNavigate = this.getImplementation(parentElement);
	        	break;
	        case menuDiagramOperations:
	        	elementsToNavigate = this.getDiagramOperations(parentElement);
				break;
		    case menuImplementedOperations:
	        	elementsToNavigate = this.getImplementedOperation(parentElement);
				break;
			case menuDependentTaggedValues:
	        	elementsToNavigate = this.getDependentTaggedValues(parentElement);
				break;
			default:
				if(menuChoice.StartsWith(taggedValueMenuPrefix) 
				   && menuChoice.EndsWith(taggedValueMenuSuffix))
				{
					elementsToNavigate = this.getElementsViaTaggedValues(parentElement,menuChoice);
				}
				else if (menuChoice == getOwnerMenuName(parentElement))
				{
					elementsToNavigate = this.getOwner(parentElement);
				}
				else if (menuChoice == getTypeMenuName(parentElement))
				{
					elementsToNavigate = this.getClassifier(parentElement);
				}
				break;
		}
		 return elementsToNavigate;
		 
	}
	

	


	public override void EA_OnContextItemChanged(global::EA.Repository Repository, string GUID, global::EA.ObjectType ot)
    {
        if (fullyLoaded && this.model != null )
        {       
            if (this.model.selectedItem != null)
            {
            	this.navigate(this.model.selectedItem) ;
            }
        }
    }
	
	private void navigate(UML.UMLItem item)
	{
		if (fullyLoaded)
        {
            if (this.navigatorControl == null)
            {
                this.navigatorControl = this.model.addWindow("Navigate", "TSF.UmlToolingFramework.EANavigator.NavigatorControl") as NavigatorControl;
                this.navigatorControl.BeforeExpand += new TreeViewCancelEventHandler(this.NavigatorTreeBeforeExpand);
                this.navigatorControl.NodeDoubleClick += new TreeNodeMouseClickEventHandler(this.NavigatorTreeNodeDoubleClick);
            }
            if (this.navigatorControl != null && this.model != null)
            {
                if (item != null)
                {
                    this.navigatorControl.setElement(item);
                }

            }
        }
	}
	public override void EA_OnPostOpenDiagram(EA.Repository Repository, int DiagramID)
	{
		if (fullyLoaded && this.model != null )
        {       
            if (this.model.currentDiagram != null)
            {
            	this.navigate(this.model.currentDiagram) ;
            }
        }
	}
    public override void EA_FileClose(EA.Repository Repository)
    {
        this.fullyLoaded = false;
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
				string clipboardText = Clipboard.GetText();
				if (clipboardText.Length > 255 )
				{
					clipboardText = clipboardText.Substring(0,255);
				}
				MessageBox.Show("Could not find item with the string found on your clipboard:" + Environment.NewLine + clipboardText,"EA Navigator: FQN not found",MessageBoxButtons.OK,MessageBoxIcon.Warning);
			}
		}else
		{
			MessageBox.Show("Clipboard does nog contain text." + Environment.NewLine + "Please selecte a valid FQN");
		}
		
	}
	/// <summary>
	/// returns the owner of the parentElement
	/// </summary>
	/// <param name="parentElement">the selected element</param>
	/// <returns>the owner of the element</returns>
	private List<UML.UMLItem> getOwner(UML.UMLItem parentElement)
	{
		List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
		UML.UMLItem owner = parentElement.owner;
		if (owner != null)
		{
			elementsToNavigate.Add(owner);
		}
		return elementsToNavigate;
	}
	
	/// <summary>
	/// returns all tagged values that reference the given item
	/// </summary>
	/// <param name="parentItem"></param>
	/// <returns></returns>
	private List<UML.UMLItem>getDependentTaggedValues(UML.UMLItem parentItem)
	{
		List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
		UML.Classes.Kernel.Element parentElement = parentItem as UML.Classes.Kernel.Element;
		if (parentElement != null)
		{
			foreach (UML.Profiles.TaggedValue taggedValue in parentElement.getReferencingTaggedValues()) 
			{
				//not for the "system" tagged values
				if (taggedValue.name != eaGUIDTagname
				   && taggedValue.name != eaOperationGUIDTagName)
				{
					elementsToNavigate.Add(taggedValue);
				}
			}
		}
		return elementsToNavigate;
	}
	
	/// <summary>
	/// returns the operation implemented by the given parentElement, 
	/// or in case the parentElement is a diagram, by the owner of the parentElement
	/// </summary>
	/// <param name="parentElement">either a behavior, or a diagram owned by a behavior</param>
	/// <returns>the implmented operation (specification)</returns>
	private List<UML.UMLItem> getImplementedOperation(UML.UMLItem parentElement)
	{
		List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
		UML.CommonBehaviors.BasicBehaviors.Behavior behavior = parentElement as UML.CommonBehaviors.BasicBehaviors.Behavior;
		//if the parent element is not a behavior it might be a diagram owned by a behavior
		if (behavior == null
		   && parentElement is UML.Diagrams.Diagram)
		{
			behavior = ((UML.Diagrams.Diagram)parentElement).owner as UML.CommonBehaviors.BasicBehaviors.Behavior;
		}
		if (behavior != null)
		{
			elementsToNavigate.Add(behavior.specification as UML.Classes.Kernel.Operation);	
		}
		return elementsToNavigate;
	}
	/// <summary>
	/// returns all elements referenced by the tagged values of the parent element
	/// </summary>
	/// <param name="parentElement">any UML.Classes.Kernel.Element</param>
	/// <returns>all elements referenced by the tagged values of the parent element</returns>
	private List<UML.UMLItem> getElementsViaTaggedValues(UML.UMLItem parentElement, string menuOption)
	{
		List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
		UML.Classes.Kernel.Element taggedValueOwner = parentElement as UML.Classes.Kernel.Element;
		if (taggedValueOwner != null)
		{
			
			foreach ( UML.Profiles.TaggedValue taggedValue in taggedValueOwner.taggedValues) 
			{
				if ( menuOption.Equals(taggedValueMenuName(taggedValue.name)))
				{
					UML.UMLItem elementToNavigate = taggedValue.tagValue as UML.UMLItem;
					if (elementToNavigate != null)
					{
						elementsToNavigate.Add(elementToNavigate);
					}
				}
			}
		}
		return elementsToNavigate;
	}

	/// <summary>
	/// returns all operations called on the given diagram
	/// </summary>
	/// <param name="parentElement">the sequence diagram</param>
	/// <returns>all operations called on the given diagram</returns>
	private List<UML.UMLItem> getDiagramOperations(UML.UMLItem parentElement)
	{
		List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
		UML.Diagrams.SequenceDiagram diagram = parentElement as UML.Diagrams.SequenceDiagram;
		if (diagram != null)
		{
			elementsToNavigate.AddRange(diagram.getCalledOperations());
		}
		return elementsToNavigate;
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

   /// <summary>
   /// gets the operation for the parent element which can be
   /// - an operation itself
   /// - a message calling the operation
   /// - a parameter for an operation
   /// - a CallOperationAction
   /// </summary>
   /// <param name="parentElement">the element to get the operation from</param>
   /// <returns>the operation for this parent element</returns>
   private List<UML.UMLItem> getOperation(UML.UMLItem parentElement)
   {
   	   List<UML.UMLItem> elementsToNavigate = new List<UML.UMLItem>();
   	   UML.Classes.Kernel.Operation calledOperation = null;
   	   if (parentElement is UML.Classes.Kernel.Parameter)
   	   {
   	   		calledOperation = ((UML.Classes.Kernel.Parameter)parentElement).operation;
   	   }
   	   else if (parentElement is UML.Actions.BasicActions.CallOperationAction)
   	   {
   	   		calledOperation = ((UML.Actions.BasicActions.CallOperationAction)parentElement).operation;
   	   }
   	   else
   	   {
   	   		calledOperation = this.getSelectedOperation(parentElement);
   	   }
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
        if (selectedOperation != null)
        {
            HashSet<UML.Classes.Kernel.Parameter> parameters = selectedOperation.ownedParameters;
            foreach (UML.Classes.Kernel.Parameter parameter in parameters)
            {
                elementsToNavigate.Add(parameter.type);
            }
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
           UML.Interactions.BasicInteractions.Message selectedMessage = parentElement as UML.Interactions.BasicInteractions.Message;
	       
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
   
   
   void NavigatorTreeNodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
   {
   		UML.UMLItem selectedElement = e.Node.Tag as UML.UMLItem;
   		if( selectedElement is UML.Diagrams.Diagram)
   		{
   			this.navigatorControl.setElement(selectedElement);
   		}
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
