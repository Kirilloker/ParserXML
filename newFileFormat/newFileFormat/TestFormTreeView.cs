﻿using System;
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
        public int level;
        public List<JRNL> children;

        public JRNL()
        {
            level = 0;
            name = "";
            value = "";
            children = new List<JRNL>();
        }
    }
    public partial class TestFormTreeView : Form
    {
        public TestFormTreeView()
        {
            InitializeComponent();
        }

        TreeView tv = new TreeView();

        public TreeNode createTree(JRNL root)
        {
            TreeNode tree = new TreeNode();

            foreach (JRNL child in root.children)
            {
                TreeNode node = new TreeNode(child.name);

                foreach (TreeNode child_node in createTree(child).Nodes) 
                {
                    node.Nodes.Add(child_node);
                }

                tree.Nodes.Add(node);
            }

            return tree;
        }


        private void TestFormTreeView_Load(object sender, EventArgs e)
        {



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
