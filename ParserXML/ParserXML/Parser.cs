using System.Xml;

public class Parser
{
    // Корневой элемент

    // XML документ
    private string XMLtext;
    // Номер символа, который сейчас проходит проверку
    private int numberCurrentChar;
    // Текущий символ
    private char c;

    public Parser(string _XMLtext)
    {
        // Удаляем все дублирующиеся пробелы, пробелы между символами < >
        while (_XMLtext.Contains("  ")) { _XMLtext = _XMLtext.Replace("  ", " "); }
        while (_XMLtext.Contains("< ")) { _XMLtext = _XMLtext.Replace("< ", "<"); }
        while (_XMLtext.Contains(" >")) { _XMLtext = _XMLtext.Replace(" >", ">"); }

        XMLtext = _XMLtext;
        numberCurrentChar = 0;
        c = XMLtext[0];
    }

    public XMLelement Parsing()
    {
        if (c == '<') { c = NC(); }

        XMLelement test = newElement();

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


    List<Attribute> getAttributesNewElement() 
    {
        List<Attribute> attributes = new List<Attribute>();

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

            attributes.Add(new Attribute(nameAttributes, valueAttributes));

            c = NC();
        }

        return attributes;
    }

    public XMLelement newElement()
    {
        // После символа < 
        XMLelement xmlelement = new XMLelement();
        xmlelement.name = getNameNewElement();
        xmlelement.attributes = getAttributesNewElement();
        xmlelement.children = new List<XMLelement>();
        //сейчас я на символе > 
        c = NC() ;

        // Пропускаем пробелы
        while (c == ' ') { c = NC(); }
        
        while (true) 
        {
            if (c == '<')
            {
                while (true)
                {
                    c = NC();
                    // Если это закрывающий тег элемента 

                    if (c == '/')
                    {
                        c = NC();

                        // Если дошли до закрывающего тега элемента который собирали, то завершить сборку
                        if (getNameNewElement() == xmlelement.name)
                        {
                            return xmlelement;
                        }
                        else 
                        {
                            Console.WriteLine("ERROR");
                        }
                        // Сейчас символ >
                    }
                    else
                    {
                        // начался внутренний элемент, нужно сделать рекусию
                        xmlelement.children.Add(newElement());

                        while (c != '<') { c = NC(); }
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
        {
            Console.WriteLine("END FILE!");
            return '?';
        }

        return XMLtext[++numberCurrentChar];
    }

    // показать следующий символ
    char getNC() 
    {
        if (XMLtext.Length <= numberCurrentChar)
        {
            Console.WriteLine("END FILE!");
            return '\0';
        }

        return XMLtext[numberCurrentChar];
    }
}
 