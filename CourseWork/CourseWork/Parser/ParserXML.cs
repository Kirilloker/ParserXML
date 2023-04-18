using CourseWork.TypeFile;

public class ParserXML
{
    // Корневой элемент

    // XML документ
    private string XMLtext;
    // Номер символа, который сейчас проходит проверку
    private int numberCurrentChar;
    // Текущий символ
    private char c;

    public ParserXML(string _XMLtext)
    {
        // Удаляем все дублирующиеся пробелы, пробелы между символами < >
        while (_XMLtext.Contains("  "))  _XMLtext = _XMLtext.Replace("  ", " "); 
        while (_XMLtext.Contains("< "))  _XMLtext = _XMLtext.Replace("< ", "<"); 
        while (_XMLtext.Contains(" >"))  _XMLtext = _XMLtext.Replace(" >", ">"); 

        XMLtext = _XMLtext;
        numberCurrentChar = 0;
        c = XMLtext[0];
    }

    public XML Parsing()
    {
        if (c == '<') c = NC();

        XML test = newElement();

        return test;
    }

    string getNameNewElement()
    {
        string nameElement = "";

        while (c != ' ' && c != '>')
        {
            nameElement += c;
            c = NC();
        }

        return nameElement;
    }


    List<CourseWork.TypeFile.Attribute> getAttributesNewElement()
    {
        List<CourseWork.TypeFile.Attribute> attributes = new List<CourseWork.TypeFile.Attribute>();

        while (c != '>')
        {
            string nameAttributes = "";
            string valueAttributes = "";

            c = NC();

            while (c != '=')
            {
                nameAttributes += c;
                c = NC();
            }

            c = NC();

            if (c == '\'' || c == '"')
            {
                char typeQuotes = c;
                c = NC();

                while (c != typeQuotes)
                {
                    valueAttributes += c;
                    c = NC();
                }
            }

            attributes.Add(new CourseWork.TypeFile.Attribute(nameAttributes, valueAttributes));

            c = NC();
        }

        return attributes;
    }

    public XML newElement()
    {
        XML xmlelement = new XML();
        xmlelement.name = getNameNewElement();
        xmlelement.attributes = getAttributesNewElement();
        xmlelement.children = new List<XML>();

        c = NC();

        while (c == ' ') { c = NC(); }

        while (true)
        {
            if (c == '<')
            {
                while (true)
                {
                    c = NC();

                    if (c == '/')
                    {
                        c = NC();

                        // Если дошли до закрывающего тега элемента который собирали, то завершить сборку
                        if (getNameNewElement() == xmlelement.name)
                            return xmlelement;
                        else
                            Console.WriteLine("ERROR");
                    }
                    else
                    {
                        xmlelement.children.Add(newElement());
                        while (c != '<') c = NC(); 
                    }
                }
            }
            else
            {
                string valueElement = "";

                while (c != '<')
                {
                    valueElement += c;
                    c = NC();
                }

                xmlelement.value = valueElement;
            }
        }

    }

    // перейти на следующий символ
    char NC()
    {
        if (XMLtext.Length <= numberCurrentChar + 1)
            return '\0';

        return XMLtext[++numberCurrentChar];
    }
}
