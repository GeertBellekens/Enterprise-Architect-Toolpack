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
    private EA.Repository repository;

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
                IsEnabled = (selectedType == global::EA.ObjectType.otConnector
                    && ((EA.Connector)selectedItem).Type == "Sequence");
            break;
          case menuDiagrams:
            IsEnabled = selectedType == global::EA.ObjectType.otMethod;
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
        this.repository = Repository;
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
        UTF_EA.Model model = new UTF_EA.Model(this.repository);
        UML.Classes.Kernel.Operation selectedOperation = model.selectedElement as UML.Classes.Kernel.Operation;
        HashSet<UML.Diagrams.Diagram> usingDiagrams = selectedOperation.getUsingDiagrams<UML.Diagrams.Diagram>();

        NavigatorList dialog = new NavigatorList(usingDiagrams.ToList());
        dialog.Show();
        /*foreach (UML.Diagrams.Diagram usingDiagram in usingDiagrams)
        {
            usingDiagram.open();
        }*/
    }

   //selects the operation that is called by the selected message in the project browser 
   private void selectOperation()
   {
       UTF_EA.Model model = new UTF_EA.Model(this.repository);
       UML.Interactions.BasicInteractions.Message selectedMessage = model.selectedElement as UML.Interactions.BasicInteractions.Message;
       if (null != selectedMessage){
           UML.Classes.Kernel.Operation calledOperation = selectedMessage.calledOperation;
           if (null != calledOperation){
               model.selectElement(calledOperation);
           }

       }

   }

  }
}
