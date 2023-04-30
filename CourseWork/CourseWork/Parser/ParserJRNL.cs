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

    int id_jrnl = 0;

    public ParserJRNL(string _JRNLtext)
    {
        JRNLtext = _JRNLtext;
        numberCurrentChar = 0;
        c = JRNLtext[0];
    }

    public void AddAttributes(ref Dictionary<string, Dictionary<string, string>> at, string firstKey, string secondKey, string value)
    {
        if (at.ContainsKey(firstKey))
        {
            if ((at[firstKey]).ContainsKey(secondKey))
                at[firstKey][secondKey] = value;
            else
                at[firstKey].Add(secondKey, value);
        }
        else
            at.Add(firstKey, new Dictionary<string, string> { { secondKey, value } });
    }

    Dictionary<string, Dictionary<string, string>> getAttributes(string str) 
    {
        //name.sdf:sdf;sdf:sdf;
        //text.color:sdf;contains:sdf;

        Dictionary<string, Dictionary<string, string>> at = new Dictionary<string, Dictionary<string, string>>();


        string[] line_attributes = str.Split('\n');
        
        foreach (string line in line_attributes) 
        {
            if (line == "") continue;

            string[] at_name_with_param = line.Split('.');

            string name_at = at_name_with_param[0];

            string param_at = string.Join("", at_name_with_param.Skip(1));

            string[] array_params = param_at.Split(';');

            foreach (string arraay_param in array_params) 
            {
                if (arraay_param == "") continue;

                string[] param = arraay_param.Split(':');
                string name_param = param[0];
                string value_param = param[1];

                AddAttributes(ref at, name_at, name_param, value_param);
            }
        }

        return at;
    }

    public JRNL getNewElement()
    {
        int level = 0;

        //string nameElement = "";
        string valueElement = "";

        while (c == '~')
        {
            level++;
            c = NC();
        }

        while (c != '\n') c = NC();
        
        c = NC();

        while (c != '~' && c != '\0')
        {
            valueElement += c;
            c = NC();
        }

        JRNL element = new JRNL();

        //element.name = nameElement;
        //element.value = valueElement;
        element.attribtues = getAttributes(valueElement);
        element.name = element.attribtues["name"]["contains"];
        element.level = level;
        element.id = id_jrnl++;

        return element;
    }

    public JRNL Parsing()
    {
        JRNL root = new JRNL();

        while (c != '\0')
        {
            JRNL newElement = getNewElement();
            JRNL parent = root;

            while (newElement.level - parent.level != 1)
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
