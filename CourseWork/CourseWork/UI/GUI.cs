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
        textElement.Text = selectedJRNL.attribtues["text"]["contains"];
        nameElement.Text = selectedJRNL.name;
    }
}
