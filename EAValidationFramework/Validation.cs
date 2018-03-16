
using System.Windows.Forms;
using System.Xml;

namespace EAValidationFramework
{
    /// <summary>
    /// Validation = validation results of performed checks.
    /// </summary>
    public class Validation
    {
        public string CheckId { get; set; }                 // Reference to the check
        public string CheckDescription { get; set; }        // Description of the check

        public string ItemName { get; set; }                // Item that gives error/warning
        public string EAItemType { get; set; }              // Type of item in EA : object/diagram/... (only informational)
        public string ItemGuid { get; set; }                // Guid of the Item
        public string ElementType { get; set; }             // Type of the element that gives error/warning
        public string ElementStereotype { get; set; }       // Stereotype of the element that gives error/warning

        public string PackageName { get; set; }                  // EA package that contains the element that gives error/warning
        public string PackageParentLevel1 { get; set; }          // Parent package of Package (level +1)
        public string PackageParentLevel2 { get; set; }          // Parent package of Package (level +2)
        public string PackageParentLevel3 { get; set; }          // Parent package of Package (level +3)
        public string PackageParentLevel4 { get; set; }          // Parent package of Package (level +4)
        public string PackageParentLevel5 { get; set; }          // Parent package of Package (level +5)

        public bool HasMandatoryContent()
        {
            // Verify that the validation has all mandatory content
            if (string.IsNullOrEmpty(this.CheckDescription)) return false;
            if (string.IsNullOrEmpty(this.EAItemType)) return false;
            if (string.IsNullOrEmpty(this.ItemGuid)) return false;
            return true;
        }

        public Validation (Check check, XmlNode validationresultNode)
        {
            this.CheckId = check.CheckId;
            this.CheckDescription = check.CheckDescription;
            foreach (XmlNode subNode in validationresultNode.ChildNodes)
            {
                InterpreteValidationResultSubNode(subNode);
            }
            if (!this.HasMandatoryContent())
                MessageBox.Show("Validation result does not have all mandatory content." + " - " + check.CheckDescription);
        }

        private void InterpreteValidationResultSubNode(XmlNode subNode)
        {
            switch (subNode.Name.ToLower())
            {
                case "itemname":
                    this.ItemName = subNode.InnerText;
                    break;
                case "itemtype":
                    this.EAItemType = subNode.InnerText;
                    break;
                case "itemguid":
                    this.ItemGuid = subNode.InnerText;
                    break;
                case "elementtype":
                    this.ElementType = subNode.InnerText;
                    break;
                case "elementstereotype":
                    this.ElementStereotype = subNode.InnerText;
                    break;
                case "packagename":
                    this.PackageName = subNode.InnerText;
                    break;
                case "packageparentlevel1":
                    this.PackageParentLevel1 = subNode.InnerText;
                    break;
                case "packageparentlevel2":
                    this.PackageParentLevel2 = subNode.InnerText;
                    break;
                case "packageparentlevel3":
                    this.PackageParentLevel3 = subNode.InnerText;
                    break;
                case "packageparentlevel4":
                    this.PackageParentLevel4 = subNode.InnerText;
                    break;
                case "packageparentlevel5":
                    this.PackageParentLevel5 = subNode.InnerText;
                    break;
            }
        }
    }
}
