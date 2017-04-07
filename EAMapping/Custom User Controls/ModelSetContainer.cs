using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMapping.Custom_User_Controls
{
    public partial class ModelSetContainer : UserControl
    {
        private GroupBox groupBox;
        public PTreeView tv { get; set; }
        public ModelSetContainer()
        {
            InitializeComponent();
        }

        public ModelSetContainer(string name, List<TreeItem> set)
        {
            InitializeComponent();
            this.Size = new Size(250, 540);
            
            groupBox = new GroupBox();
            groupBox.Text = name;
            groupBox.Location = new Point(5,5);
            groupBox.Size = new Size(240, 530);
            
            tv = new PTreeView();
            PopulateTree(tv, set, '.');
            tv.Location = new Point(5, 15);
            tv.Size = new Size(230, 480);
            tv.ExpandAll();

            //Buttons
            Button expand = new Button();
            expand.Location = new Point(5, 500);
            expand.Size = new Size(40, 25);
            expand.Text = "+";
            groupBox.Controls.Add(expand);
            expand.Click += new System.EventHandler(this.expandTree);

            Button minimize = new Button();
            minimize.Location = new Point(50, 500);
            minimize.Size = new Size(40, 25);
            minimize.Text = "-";
            minimize.Click += new System.EventHandler(this.minimizeTree);

            groupBox.Controls.Add(tv);
            groupBox.Controls.Add(expand);
            groupBox.Controls.Add(minimize);
            this.Controls.Add(groupBox);
                       
        }
        private void expandTree(object sender, EventArgs e)
        {
            tv.ExpandAll();
        }

        private void minimizeTree(object sender, EventArgs e)
        {
            tv.CollapseAll();
        }

        public static void PopulateTree(TreeView treeView, ICollection<TreeItem> items, char pathSeparator)
        {
           /* TreeNode lastNode = null;
            string subPathAgg;
            foreach (TreeItem path in items)
            {
                subPathAgg = string.Empty;
                foreach (string subPath in path.Split(pathSeparator))
                {
                    subPathAgg += subPath + pathSeparator;
                    TreeNode[] nodes = treeView.Nodes.Find(subPathAgg, true);
                    if (nodes.Length == 0)
                        if (lastNode == null)
                            lastNode = treeView.Nodes.Add(subPathAgg, subPath);
                        else
                            lastNode = lastNode.Nodes.Add(subPathAgg, subPath);
                    else
                        lastNode = nodes[0];
                }
                lastNode = null; // This is the place code was changed
                
            }*/
/*            tree.Nodes.Clear();
            List<TreeNode> roots = new List<TreeNode>();
            roots.Add(tree.Nodes.Add("Items"));
            foreach (TreeItem item in items)
            {
                if (item.Level == roots.Count) roots.Add(roots[roots.Count - 1].LastNode);
                roots[item.Level].Nodes.Add(item.Name);
            }*/
        }
    }
}
