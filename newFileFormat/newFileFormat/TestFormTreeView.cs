using System;
using System.Collections;
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
    public struct JRNL
    {
        public string name;
        public string value;
    }

    public partial class TestFormTreeView : Form
    {
        public TestFormTreeView()
        {
            InitializeComponent();
        }

        TreeView tv = new TreeView();

        Hashtable JRNLtable = new Hashtable();

        void createTestHashTable() 
        {
            JRNL first = new JRNL();
            first.name = "a";
            first.value = "-e-e";
            JRNL second = new JRNL();
            first.name = "b";
            first.value = "-e-e";
            JRNL thrist = new JRNL();
            first.name = "c";
            first.value = "-e-e";

            JRNLtable.Add(11, first);
            JRNLtable.Add(2, second);
            JRNLtable.Add(1, thrist);
        }

        private void TestFormTreeView_Load(object sender, EventArgs e)
        {

            createTestHashTable();

            JRNLtable.Remove(1);

            string x = "~a" +
                "Desctiprtop" +
                "Txt" +
                "~b" +
                "dew" +
                "sdf" +
                "~~c" +
                "sdfsd" +
                "qwvf" +
                "~~d" +
                "dsf" +
                "dsf" +
                "dsf" +
                "ew" +
                "we" +
                "" +
                "" +
                "" +
                "" +
                "" +
                "" +
                "";

            Console.WriteLine("Hello, World");

            //TreeView tv = new TreeView();
            tv.Location = new Point(10, 10);
            tv.Size = new Size(100, 300);
            


            TreeNode tn1 = new TreeNode("a");
            TreeNode tn2 = new TreeNode("b");
            TreeNode tn2_1 = new TreeNode("c");
            TreeNode tn2_2 = new TreeNode("d");
            TreeNode tn3 = new TreeNode("e");
            TreeNode tn3_1 = new TreeNode("f");
            TreeNode tn3_2 = new TreeNode("g");
            TreeNode tn3_2_1 = new TreeNode("h");
            TreeNode tn3_2_2 = new TreeNode("i");
            TreeNode tn3_3 = new TreeNode("k");
            TreeNode tn3_4 = new TreeNode("l");
            TreeNode tn4 = new TreeNode("m");
            
            

            tn2.Nodes.Add(tn2_1);
            tn2.Nodes.Add(tn2_2);
            tn3.Nodes.Add(tn3_1);
            tn3.Nodes.Add(tn3_2);
            tn3_2.Nodes.Add(tn3_2_1);
            tn3_2.Nodes.Add(tn3_2_2);
            tn3.Nodes.Add(tn3_3);
            tn3.Nodes.Add(tn3_4);


            tv.Nodes.Add(tn1);
            tv.Nodes.Add(tn2);
            tv.Nodes.Add(tn3);
            tv.Nodes.Add(tn4);

            this.Controls.Add(tv);

            tv.DoubleClick += MyAfterSelectHandler;
        }

        void MyAfterSelectHandler(object sender, EventArgs e) 
        {
            Console.WriteLine(e);
            string selectedName = ((TreeView)sender).SelectedNode.Text;

            Console.WriteLine("Был выбран элемент:" + selectedName);
            Console.WriteLine("Был выбран элемент:" + tv.SelectedNode);
            
            Dictionary<int, int> map = new Dictionary<int, int>();

        }
    }
}
