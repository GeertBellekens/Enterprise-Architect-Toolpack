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
    private UTF_EA.Model model = null;

	public EAAddin():base()
	{
		this.menuHeader = menuName;
		this.menuOptions = new string[] {menuOperation,menuDiagrams};
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
