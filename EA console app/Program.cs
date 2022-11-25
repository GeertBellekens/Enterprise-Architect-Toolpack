using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EA_console_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // connect to user1
            var path = @"C:\temp\test project.qea";
            var repository =  new EA.Repository();
            repository.SuppressSecurityDialog = true;
            repository.SuppressEADialogs = true;
            var isOpened = repository.OpenFile2(path, "user1", "user1");

            foreach (EA.Package rootpackage in repository.Models)
            {
                //create package
                EA.Package newPackage = rootpackage.Packages.AddNew("newPackage", "");
                newPackage.Update();
                //create diagram
                EA.Diagram newDiagram = newPackage.Diagrams.AddNew("newDiagram", "Logical");
                newDiagram.Update();
            }

            //close EA
            //repository.Exit();
        }
    }
}
