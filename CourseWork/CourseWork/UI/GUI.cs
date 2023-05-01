using CourseWork.TypeFile;
using CourseWork.Сonverter;

namespace CourseWork;

public partial class GUI : Form
{    
    // Дерево записей
    TreeView tree = new TreeView();
    // Текст 
    RichTextBox textElement = new RichTextBox();
    // Название элемента
    Label nameElement = new Label();
    // Корневой элемент JRNL объекта
    JRNL root = new JRNL();

    public GUI()
    {
        InitializeComponent();
        this.Size = new Size(1200, 650);
        // Настройка дерева
        tree.Location = new Point(10, 10);
        tree.Size = new Size(300, 600);
        tree.BackColor = Color.AliceBlue;
        tree.DoubleClick += TreeDoubleClick;
        this.Controls.Add(tree);

        // Настройка текста
        textElement.Location = new Point(350, 50);
        textElement.Size = new Size(800, 550);
        this.Controls.Add(textElement);

        // Настройка заголовка
        nameElement.Location = new Point(350, 10);
        nameElement.BackColor = Color.AliceBlue;
        nameElement.Size = new Size(800, 40);
        this.Controls.Add(nameElement);
    }


    private void GUI_Load(object sender, EventArgs e)
    {
        root = XMLconvertJRNL.getJRNL("test.xml");

        foreach (TreeNode jrnl_element in createTree(root).Nodes) 
            tree.Nodes.Add(jrnl_element);
    }


    // Создание иерархиечкого дерево по структуре JRNL
    public TreeNode createTree(JRNL root)
    {
        TreeNode tree = new TreeNode();

        foreach (JRNL child in root.children)
        {
            TreeNode node = new TreeNode(child.name);

            foreach (TreeNode child_node in createTree(child).Nodes)
                node.Nodes.Add(child_node);

            node.Name = child.id.ToString();
            node.Text = child.name;

            tree.Nodes.Add(node);
        }

        return tree;
    }

    JRNL searchJRNLByID(JRNL root, int id)
    {
        foreach (var item in root.children)
        {
            if (item.id == id) return item;
            JRNL jRNL = searchJRNLByID(item, id);
            if (jRNL.id != -1) return jRNL;
        }
        return new JRNL();
    }

    void TreeDoubleClick(object sender, EventArgs e) 
    {
        Console.WriteLine("Double Click");
        int selectedId = int.Parse(((TreeView)sender).SelectedNode.Name);
        JRNL selectedJRNL = searchJRNLByID(root, selectedId);

        //textElement.Text = selectedJRNL.value;
        if (selectedJRNL.attribtues.ContainsKey("text"))
            settingsText(selectedJRNL.attribtues["text"]);
        
        if (selectedJRNL.attribtues.ContainsKey("name"))
            settingsName(selectedJRNL.attribtues["name"]);
    }

    void settingsText(Dictionary<string, string> settings) 
    {
        if (settings.ContainsKey("back-color")) 
        {
            textElement.BackColor = Color.FromName(settings["back-color"]);
        }
        else 
        {
            textElement.BackColor = Color.White;
        }

        if (settings.ContainsKey("contains")) 
        {
            textElement.Text = settings["contains"];
        }
        else 
        {
            textElement.Text = "Not found Text";
        }

        
        if (settings.ContainsKey("font-color"))
        {
            textElement.ForeColor = Color.FromName(settings["font-color"]);
        }
        else
        {
            textElement.ForeColor = Color.Black;
        }

        string familyName = "Times New Roman";
        int sizeText = 14;
        FontStyle fontStyle = FontStyle.Regular;

        if (settings.ContainsKey("family-name"))
        {
            familyName = settings["famil-yname"];
        }

        if (settings.ContainsKey("size-text"))
        {
            sizeText = int.Parse(settings["size-text"]);
        }

        if (settings.ContainsKey("font-style"))
        {
            switch (settings["font-style"])
            {
                case "bold":
                    fontStyle = FontStyle.Bold;
                    break;
                case "italic":
                    fontStyle = FontStyle.Italic;
                    break;
                case "normal":
                    fontStyle = FontStyle.Regular;
                    break;
                case "underline":
                    fontStyle = FontStyle.Underline;
                    break;
                default:
                    break;
            }
        }

        textElement.Font =new Font(familyName, sizeText, fontStyle);
    }








    void settingsName(Dictionary<string, string> settings)
    {
        if (settings.ContainsKey("back-color"))
        {
            nameElement.BackColor = Color.FromName(settings["back-color"]);
            nameElement.BackColor = Color.Azure;
        }
        else
        {
            nameElement.BackColor = Color.White;
        }

        if (settings.ContainsKey("contains"))
        {
            nameElement.Text = settings["contains"];
        }
        else
        {
            nameElement.Text = "Not found Text";
        }


        if (settings.ContainsKey("font-color"))
        {
            nameElement.ForeColor = Color.FromName(settings["font-color"]);
        }
        else
        {
            nameElement.ForeColor = Color.Black;
        }

        string familyName = "Times New Roman";
        int sizeText = 20;
        FontStyle fontStyle = FontStyle.Bold;

        if (settings.ContainsKey("family-name"))
        {
            familyName = settings["famil-yname"];
        }

        if (settings.ContainsKey("size-text"))
        {
            sizeText = int.Parse(settings["size-text"]);
        }

        if (settings.ContainsKey("font-style"))
        {
            switch (settings["font-style"])
            {
                case "bold":
                    fontStyle = FontStyle.Bold;
                    break;
                case "italic":
                    fontStyle = FontStyle.Italic;
                    break;
                case "normal":
                    fontStyle = FontStyle.Regular;
                    break;
                case "underline":
                    fontStyle = FontStyle.Underline;
                    break;
                default:
                    break;
            }
        }

        nameElement.Font = new Font(familyName, sizeText, fontStyle);
    }

}

