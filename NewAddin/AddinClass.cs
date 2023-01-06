using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;


namespace NewAddin
{
    public class AddinClass: EAAddinFramework.EAAddinBase
    {
        // define menu constants
        const string menuName = "-&MyAddin";
        const string menuHello = "&Say Hello";
        const string menuGoodbye = "&Say Goodbye";
        const string menuCreatePackage = "&Create Package";
        const string menuShowControl = "&Show Control";
        const string testPackage = "testPackage";
        const string testDiagram = "testDiagram";

        // remember if we have to say hello or goodbye
        private bool shouldWeSayHello = true;



        /// <summary>
        /// constructor where we set the menuheader and menuOptions
        /// </summary>
        public AddinClass() : base()
        {
            this.menuHeader = menuName;
            this.menuOptions = new string[] { menuHello, menuGoodbye, menuCreatePackage, menuShowControl };
        }

        /// <summary>
        /// Called once Menu has been opened to see what menu items should active.
        /// </summary>
        /// <param name="Repository">the repository</param>
        /// <param name="Location">the location of the menu</param>
        /// <param name="MenuName">the name of the menu</param>
        /// <param name="ItemName">the name of the menu item</param>
        /// <param name="IsEnabled">boolean indicating whethe the menu item is enabled</param>
        /// <param name="IsChecked">boolean indicating whether the menu is checked</param>
        public override void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            if (IsProjectOpen(Repository))
            {
                switch (ItemName)
                {
                    // define the state of the hello menu option
                    case menuHello:
                        IsEnabled = shouldWeSayHello;
                        break;
                    // define the state of the goodbye menu option
                    case menuGoodbye:
                        IsEnabled = !shouldWeSayHello;
                        break;
                    // Define the state of the MenuCreatePackage menu option: 
                    // Enable this menu option if 1)There is a repository open and 2)there is a package selected.
                    case menuCreatePackage:
                        IsEnabled = (Repository != null) && (this.model.selectedElement != null);
                        break;
                    case menuShowControl:
                        IsEnabled = true;
                        break;
                    // there shouldn't be any other, but just in case disable it.
                    default:
                        IsEnabled = false;
                        break;
                }
            }
            else
            {
                // If no open project, disable all menu options
                IsEnabled = false;
            }
        }
        /// <summary>
        /// return the MDG content for the EDD MDG (so it doesn't have to be loaded separately
        /// </summary>
        /// <param name="Repository"></param>
        /// <returns>the MDG file contents</returns>
        public override object EA_OnInitializeTechnologies(global::EA.Repository Repository)
        {
            return Properties.Resources.DEMO4_MDG;
        }
        /// <summary>
        /// Called when user makes a selection in the menu.
        /// This is your main exit point to the rest of your Add-in
        /// </summary>
        /// <param name="Repository">the repository</param>
        /// <param name="Location">the location of the menu</param>
        /// <param name="MenuName">the name of the menu</param>
        /// <param name="ItemName">the name of the selected menu item</param>
        public override void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                // user has clicked the menuHello menu option
                case menuHello:
                    this.sayHello();
                    break;
                // user has clicked the menuGoodbye menu option
                case menuGoodbye:
                    this.sayGoodbye();
                    break;
                // user has clicked the menuCreatePackage menue option
                case menuCreatePackage:
                    testContextObject();
                    break;
                case menuShowControl:
                    this.showControl();
                    break;

            }
        }
        private void showControl()
        {
            this.model.addWindow("Title", "NewAddin.AddinControl");
        }

        private void testContextObject()
        {
            var selectedItem = this.model.selectedItem;
            if (selectedItem is UML.Classes.Kernel.Class)
            {
                MessageBox.Show($"We hebben een class met naam {selectedItem.name}");
            }
        }
        private void createPackage(TSF_EA.Package selectedPackage)
        {
            // Query to get an existing package.
            string sqlGetExistingElement = @"select Object_ID From t_object where " +
                                            "name= '" + testPackage + "'";
            // Get the existing package.
            TSF_EA.ElementWrapper myNewTestPackage = this.model.getElementWrappersByQuery(sqlGetExistingElement).FirstOrDefault();
            if (myNewTestPackage == null) // The package does not exist. 
            {
                // If the element does not exist, create a new one.
                myNewTestPackage = selectedPackage.addOwnedElement<TSF_EA.Package>(testPackage, "Package");
            }

            if (myNewTestPackage != null)
            {
                myNewTestPackage.save();
            }

            // Create a diagram in the newly created package.
            string sqlGetExistingDiagram = @"select Diagram_ID " +
                                            "from t_diagram " +
                                            "where name='" + testDiagram + "'";

            var testDiagramObject = this.model.getDiagramsByQuery(sqlGetExistingDiagram).FirstOrDefault();
            if (testDiagramObject == null)
            {
                testDiagramObject = myNewTestPackage.addOwnedDiagram<TSF_EA.PackageDiagram>(testDiagram);
                testDiagramObject.save();
            }

            //Create some class elements in the package
            for (int i = 0; i < 10; i++)
            {
                // Before you create an element make sure it does not exist in the current package.
                string className = "class" + i;
                sqlGetExistingElement = @"select Object_ID from t_object as o inner join t_package as p " +
                                                           "on o.Package_ID = p.Package_ID " +
                                                           "where  o.name = '" + className + "' " + "and  + p.Package_ID = '" + myNewTestPackage.id + "'";

                /*"select Object_ID From t_object where " +
                                        "name= '" + className + "' " + "and " + "Object_ID= '" + myNewTestPackage.id + "'";*/
                var cls = this.model.getElementWrappersByQuery(sqlGetExistingElement).FirstOrDefault();
                if (cls == null)
                {
                    myNewTestPackage.addOwnedElement<TSF_EA.Class>(className, "Class");
                }

                testDiagramObject.addToDiagram(cls, 0, 0, 20, 30);
                testDiagramObject.autoLayout();
                testDiagramObject.reFresh();

            }



        }

        /// <summary>
        /// Called when EA start model validation. Just shows a message box
        /// </summary>
        /// <param name="Repository">the repository</param>
        /// <param name="Args">the arguments</param>
        public override void EA_OnStartValidation(EA.Repository Repository, object Args)
        {
            MessageBox.Show("Validation started");
        }
        /// <summary>
        /// Called when EA ends model validation. Just shows a message box
        /// </summary>
        /// <param name="Repository">the repository</param>
        /// <param name="Args">the arguments</param>
        public override void EA_OnEndValidation(EA.Repository Repository, object Args)
        {
            MessageBox.Show("Validation ended");
        }

        /// <summary>
        /// Say Hello to the world
        /// </summary>
        private void sayHello()
        {
            MessageBox.Show("Hello World");
            this.shouldWeSayHello = false;
        }

        /// <summary>
        /// Say Goodbye to the world
        /// </summary>
        private void sayGoodbye()
        {
            MessageBox.Show("Goodbye World");
            this.shouldWeSayHello = true;
        }

    }
}
