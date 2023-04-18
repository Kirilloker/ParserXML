using CourseWork.TypeFile;
using System.Collections;


public class ParserJRNL
{
    // JRNL документ
    private string JRNLtext = "";
    // Номер символа, который сейчас проходит проверку
    private int numberCurrentChar;
    // Текущий символ
    private char c;

    public ParserJRNL(string _JRNLtext)
    {
        JRNLtext = _JRNLtext;
        numberCurrentChar = 0;
        c = JRNLtext[0];
    }

    public JRNL getNewElement()
    {
        int level = 0;

        string nameElement = "";
        string valueElement = "";

        while (c == '~')
        {
            level++;
            c = NC();
        }

        while (c != '-' && c != '\n')
        {
            nameElement += c;
            c = NC();
        }

        while (c != '~' && c != '\0')
        {
            valueElement += c;
            c = NC();
        }

        JRNL element = new JRNL();

        element.name = nameElement;
        element.value = valueElement;
        element.level = level;
        //element.id = id_jrnl;

        return element;
    }

    public JRNL Parsing()
    {
        JRNL root = new JRNL();

        while (c != '\0')
        {
            JRNL newElement = getNewElement();
            JRNL parent = root;

            while (newElement.level - 1 != parent.level)
                parent = parent.children[^1];

            parent.children.Add(newElement);
        }

        return root;
    }


    // перейти на следующий символ
    char NC()
    {
        if (JRNLtext.Length <= numberCurrentChar + 1)
        {
            Console.WriteLine("END FILE!");
            return '\0';
        }

        return JRNLtext[++numberCurrentChar];
    }
}
