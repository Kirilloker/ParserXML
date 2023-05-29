using CourseWork.TypeFile;
using System.Xml;
using Attribute = CourseWork.TypeFile.Attribute;

public static class ParserXML
{
    // Корневой элемент

    // XML документ
    static string? XMLtext;
    // Номер символа, который сейчас проходит проверку
    static int numberCurrentChar;
    // Текущий символ
    static char c;

    public static XML getXMLTree(string _XMLtext)
    {
        XMLtext = _XMLtext;
        // Удаляем все дублирующиеся пробелы, пробелы между символами < >
        while (XMLtext.Contains("  ")) XMLtext = XMLtext.Replace("  ", " ");
        while (XMLtext.Contains("< ")) XMLtext = XMLtext.Replace("< ", "<");
        while (XMLtext.Contains(" >")) XMLtext = XMLtext.Replace(" >", ">");
        while (XMLtext.Contains("\t")) XMLtext = XMLtext.Replace("\t", "");
        
        numberCurrentChar = 0;
        c = XMLtext[0];

        if (c == '<') c = NC();

        return newElement();
    }

    // Возвращает следующие имя элемента
    static string getNameNewElement()
    {
        string nameElement = "";

        while (c != ' ' && c != '>' && c != '/')
        {
            nameElement += c;
            c = NC();
        }

        return nameElement;
    }

    // Возвращает список атрибутов элемента
    // Возвращает список атрибутов элемента
    static List<Attribute> getAttributesNewElement()
    {
        List<Attribute> attributes = new List<Attribute>();

        string nameAttribute;
        string valueAttribute;

        while (c != '>' && c != '/')  // добавляем проверку на '/'
        {
            nameAttribute = "";
            valueAttribute = "";

            // ?
            c = NC();

            // Пропускаем пробел
            if (c == ' ') { c = NC(); }

            // Получаем название атрибута
            while (c != '=')
            {
                nameAttribute += c;
                c = NC();
            }

            c = NC();

            // Если начались ковычки - получаем значение атрибута
            if (c == '\'' || c == '"')
            {
                char typeQuotes = c;
                c = NC();

                while (c != typeQuotes)
                {
                    valueAttribute += c;
                    c = NC();
                }
            }
            else
                throw new Exception("Проблема с ковычками");

            attributes.Add(new Attribute(nameAttribute, valueAttribute));
            // пропускаем закрывающиеся ковычку
            c = NC();

            if (c == ',') { c = NC(); }
        }

        return attributes;
    }


    // получение нового элемента
    // получение нового элемента
    static XML newElement()
    {
        XML xmlelement = new XML();
        xmlelement.name = getNameNewElement();
        xmlelement.attributes = getAttributesNewElement();
        xmlelement.children = new List<XML>();

        if (c == '/')
        {
            // пропускаем символ '/' и ожидаем '>'
            c = NC();
            if (c == '>')
            {
                c = NC();
                return xmlelement;
            }
            else
            {
                throw new Exception("Не правильный закрывающий тэг");
            }
        }

        c = NC();

        if (c == ' ') { c = NC(); }

        while (true)
        {
            // Если начала другого узла
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
                        {
                            c = NC();
                            return xmlelement;
                        }
                        else
                            throw new Exception("Не правильный закрывающий тэг");
                    }
                    else
                    {
                        xmlelement.children.Add(newElement());
                        while (c != '<') c = NC();
                    }
                }
            }
            // Иначе обрабатываем значение элемента
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
    static char NC()
    {
        if (XMLtext.Length <= numberCurrentChar + 1)
            return '\0';

        return XMLtext[++numberCurrentChar];
    }
}
