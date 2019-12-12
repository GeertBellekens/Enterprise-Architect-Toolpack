using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Design;

namespace TSF.UmlToolingFramework.EANavigator
{
    public partial class MyBaseControl : UserControl
    {
        public MyBaseControl()
        {
            TypeDescriptor.AddAttributes(typeof(ImageList.ImageCollection),
                                        new Attribute[] {
                                        new EditorAttribute(typeof(MyImageListEditor), 
                                        typeof(UITypeEditor)) });
            InitializeComponent();
        }
    }
}
