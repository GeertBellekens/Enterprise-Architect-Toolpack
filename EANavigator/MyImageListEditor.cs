using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace TSF.UmlToolingFramework.EANavigator
{

public class MyImageListEditor : CollectionEditor
    {
        Type ImageListImageType;
        public MyImageListEditor(Type type) : base(type)
        {
            ImageListImageType = typeof(ControlDesigner).Assembly
                .GetType("System.Windows.Forms.Design.ImageListImage");
        }
        protected override string GetDisplayText(object value)
        {
            if (value == null)
                return string.Empty;
            PropertyDescriptor property = TypeDescriptor.GetProperties(value)["Name"];
            if (property != null)
            {
                string str = (string)property.GetValue(value);
                if (str != null && str.Length > 0)
                    return str;
            }
            if (value.GetType() == ImageListImageType)
                value = (object)((dynamic)value).Image;
            string name = TypeDescriptor.GetConverter(value).ConvertToString(value);
            if (name == null || name.Length == 0)
                name = value.GetType().Name;
            return name;
        }
        protected override object CreateInstance(Type type)
        {
            return ((UITypeEditor)TypeDescriptor.GetEditor(ImageListImageType,
                typeof(UITypeEditor))).EditValue(Context, null);
        }
        protected override CollectionEditor.CollectionForm CreateCollectionForm()
        {
            CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
            collectionForm.Text = "My Image Collection Editor";
            var overArchingTableLayoutPanel =
                (TableLayoutPanel)collectionForm.Controls["overArchingTableLayoutPanel"];
            var propertyBrowser =
                (PropertyGrid)overArchingTableLayoutPanel.Controls["propertyBrowser"];
            propertyBrowser.BrowsableAttributes = new AttributeCollection();

            return collectionForm;
        }
        protected override IList GetObjectsFromInstance(object instance)
        {
            return (IList)(instance as ArrayList) ?? (IList)null;
        }
    }
}
