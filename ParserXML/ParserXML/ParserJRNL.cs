using System.Collections;
using System.Xml;

public class ParserJRNL
{
    // JRNL документ
    private string JRNLtext = "";
    Hashtable JRNLtable = new Hashtable();
    // Номер символа, который сейчас проходит проверку
    private int numberCurrentChar;
    // Текущий символ
    private char c;

    public ParserJRNL(string _JRNLtext) 
    {
        JRNLtext= _JRNLtext;
        numberCurrentChar = 0;
        c = JRNLtext[0];
    }

    

    public Hashtable Parsing(int prevLevel = 0) 
    {
        int newLevel = 0;

        string nameElement = "";
        string valueElement = "";
        
        while (c == '~') 
        {
            newLevel++;
            c = NC();
        }

        while (c != '-' && c != '\n' && c != '\0') 
        {
            nameElement += c;
            c = NC();
        }

        if (c == '\0') return JRNLtable;
                
        while (c != '~' && c != '\0') 
        {
            valueElement += c;
            c = NC();
        }

        JRNL element = new JRNL();
        element.name = nameElement;
        element.value = valueElement;

        // Если числа одинакового порядка
        if (prevLevel.ToString().Length == newLevel) 
        {
            newLevel = prevLevel + 1;
        }
        else if ( prevLevel.ToString().Length > newLevel) 
        {
            newLevel = prevLevel / 10 + 1;
        }
        else 
        {
            newLevel = prevLevel * 10 + 1;
        }

        JRNLtable.Add(newLevel, element);

        if (c == '\0') return JRNLtable;

        return Parsing(newLevel);
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
