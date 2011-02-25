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
    const string menuAbout = "&About";
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
        	if (MenuName == string.Empty) {
        		//return top level menu option
        		return this.menuHeader;
        	} else if (MenuName == this.menuHeader) {
        		// show about menu option only when called from main menu
        		if (MenuLocation == "MainMenu")
        		{
        			return new string[]{menuOperation,menuDiagrams,menuAbout};
        		}
        		// return submenu options
        		return this.menuOptions;
        	} else {
        		return string.Empty;
        	}
            
        }

    public override void EA_GetMenuState( EA.Repository Repository, 
                                 String Location,
                                 String MenuName, String ItemName, 
                                 ref bool IsEnabled, ref bool IsChecked )
    {
      if( IsProjectOpen(Repository) ) {
        Object selectedItem;
        global::EA.ObjectType selectedType = 
          Repository.GetContextItem(out selectedItem);

        switch(ItemName) {
          case menuOperation:
                IsEnabled = (selectedType == EA.ObjectType.otConnector
                    && ((EA.Connector)selectedItem).Type == "Sequence");
            break;
          case menuDiagrams:
            IsEnabled = (selectedType == EA.ObjectType.otMethod
                         ||(selectedType == EA.ObjectType.otConnector
                            && ((EA.Connector)selectedItem).Type == "Sequence"));
            break;
          case menuAbout:
            IsEnabled = true;
            break;
          default:
            IsEnabled = false;
            break;
        }
      } else {
        // If no open project, disable all menu options
        IsEnabled = false;
      }
    }

    public override void EA_MenuClick( global::EA.Repository Repository, 
                              String Location, 
                              String MenuName, String ItemName )
    {
		this.model = new UTF_EA.Model(Repository);
      switch(ItemName) {
        case menuOperation   : 
            this.selectOperation();        
            break;
        case menuDiagrams : 
            this.openDiagrams();      
            break;
        case menuAbout :
            new AboutWindow().ShowDialog();
            break;
            
      }
    }
    // opens all  diagrams
    private void openDiagrams()
    {
        
        UML.Classes.Kernel.Operation selectedOperation = this.model.selectedElement as UML.Classes.Kernel.Operation;
        // if the selectedOperation is null we try to get the operation from the selected message
        if (selectedOperation == null)
        {
        	selectedOperation = this.getCalledOperationFromSelectedMessage();
        }
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
	   UML.Classes.Kernel.Operation calledOperation = this.getCalledOperationFromSelectedMessage();
	   if (null != calledOperation){
	       this.model.selectElement(calledOperation);
	   }
   }
   /// <summary>
   /// Gets the selected message from the repository and returns the operation called by this message
   /// </summary>
   /// <returns>the operation called by the selected message</returns>
   private UML.Classes.Kernel.Operation getCalledOperationFromSelectedMessage()
   {
   	   
       UML.Interactions.BasicInteractions.Message selectedMessage = this.model.selectedElement as UML.Interactions.BasicInteractions.Message;
       UML.Classes.Kernel.Operation calledOperation = null;
       if (null != selectedMessage)
       {
        calledOperation = selectedMessage.calledOperation;
       }
       if (calledOperation == null)
       {
       	System.Windows.Forms.MessageBox.Show("Could not find operation!\nMake sure you either select:\n-An Operation in the project browser \n-A message in a sequence diagram that calls an existing Operation"
        	                                     ,"Missing Operation!",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
       }
       return calledOperation;
           
   }
   

  }
}
