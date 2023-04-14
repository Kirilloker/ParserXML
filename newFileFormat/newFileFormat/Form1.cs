using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newFileFormat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TreeView treeView = new TreeView();

            TreeNode x = new TreeNode();
            
            //treeView.Nodes.Add("", new )
            treeView.Location = new Point(10, 10);
            treeView.Size = new Size(100, 300);
            this.Controls.Add(treeView);



        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
       
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
