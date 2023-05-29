    namespace CourseWork.TypeFile;

    public struct XML
    {
        public string name;
        public string value;
        public List<Attribute> attributes;
        public List<XML> children;

        public XML()
        {
            name = "";
            value = "";
            attributes = new List<Attribute>();
            children = new List<XML>();
        }

    public void PrintXml(XML xml, int indentationLevel = 0)
    {
        string indentation = new string(' ', indentationLevel * 2);  // каждый уровень отступа увеличивается на 2 пробела

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(indentation + "<" + xml.name);

        foreach (var attr in xml.attributes)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" " + attr.name);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("=");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\"" + attr.value + "\"");
        }

        if (string.IsNullOrEmpty(xml.value) && xml.children.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("/>");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">");

            if (!string.IsNullOrEmpty(xml.value))
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(indentation + "  " + xml.value);
            }

            foreach (var child in xml.children)
            {
                PrintXml(child, indentationLevel + 1);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(indentation + "</" + xml.name + ">");
        }

        Console.ForegroundColor = ConsoleColor.White;
    }


}


public struct Attribute
    {
        public string name;
        public string value;
        public Attribute(string _name, string _value) { name = _name; value = _value; }
    }