using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;



namespace newFileFormat
{
    public class JRNL
    {
        public string name;
        public string value;
        public int level;
        public int id;
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
            this.Size = new Size(1200,650);
            richTextBox.Location = new Point(350, 50);
            richTextBox.Size = new Size(800, 550);
            lbl.Location = new Point(350, 10);
            lbl.BackColor = Color.AliceBlue;
            lbl.Size = new Size(800,40);
            this.Controls.Add(richTextBox);
            this.Controls.Add(lbl);
        }

        TreeView tv = new TreeView();
        Label lbl = new Label();
        RichTextBox richTextBox = new RichTextBox();
        JRNL root = new JRNL();

        int id_jrtnl = 0;

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
                child.id = id_jrtnl++;

                node.Name = child.id.ToString();
                node.Text = child.name;
                

                tree.Nodes.Add(node);
            }

            return tree;
        }


        private void TestFormTreeView_Load(object sender, EventArgs e)
        {
            root.name = "Root element";
            root.value = "-text";

            JRNL a_element = new JRNL();
            a_element.name = "a";
            a_element.value = "-text a";

            JRNL b_element = new JRNL();
            b_element.name = "b";
            b_element.value = "-text b";

            JRNL c_element = new JRNL();
            c_element.name = "c";
            c_element.value = "-text c";

            b_element.children.Add(c_element);

            JRNL d_element = new JRNL();
            d_element.name = "d";
            d_element.value = "-text d";

            b_element.children.Add(d_element);

            JRNL e_element = new JRNL();
            e_element.name = "e";
            e_element.value = "-text e";

            JRNL f_element = new JRNL();
            f_element.name = "f";
            f_element.value = "-text f";

            e_element.children.Add(f_element);

            JRNL g_element = new JRNL();
            g_element.name = "g";
            g_element.value = "-text g";

            e_element.children.Add(g_element);

            JRNL h_element = new JRNL();
            h_element.name = "h";
            h_element.value = "-text h";

            g_element.children.Add(h_element);

            JRNL i_element = new JRNL();
            i_element.name = "i";
            i_element.value = "-text i";

            g_element.children.Add(i_element);

            JRNL k_element = new JRNL();
            k_element.name = "k";
            k_element.value = "-text k";

            e_element.children.Add(k_element);

            JRNL l_element = new JRNL();
            l_element.name = "l";
            l_element.value = "-text l";

            e_element.children.Add(l_element);

            JRNL m_element = new JRNL();
            m_element.name = "m";
            m_element.value = "-text m";

            root.children.Add(a_element);
            root.children.Add(b_element);
            root.children.Add(e_element);
            root.children.Add(m_element);
           
            

            TreeView tv = new TreeView();


            foreach (TreeNode item in createTree(root).Nodes)
            {
                tv.Nodes.Add(item);
            }

            tv.Location = new Point(10, 10);
            tv.Size = new Size(300, 600);
            tv.BackColor = Color.AliceBlue;
            this.Controls.Add(tv);

            tv.DoubleClick += MyAfterSelectHandler;
        }

        JRNL searchJRNLByID(JRNL root, int id) 
        {
            foreach (var item in root.children)
            {
                if (item.id == id) return item; 
                JRNL jRNL = searchJRNLByID(item, id);
                if (jRNL != null) return jRNL;  
            }
            return null;
        }

        void MyAfterSelectHandler(object sender, EventArgs e) 
        {
            string selectedText = ((TreeView)sender).SelectedNode.Text;
            int selectedId = int.Parse(((TreeView)sender).SelectedNode.Name);

            Console.WriteLine("Был выбран элемент:" + selectedText);
            JRNL selectedJRNL = searchJRNLByID(root, selectedId);

            Console.WriteLine(selectedJRNL.value);


            
            richTextBox.Text = selectedJRNL.value;
            lbl.Text = selectedJRNL.name;
            richTextBox.Update();
        }
    }
}
