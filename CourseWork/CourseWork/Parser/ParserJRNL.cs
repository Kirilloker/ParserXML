using CourseWork.TypeFile;


public static class ParserJRNL
{
    // JRNL документ в виде строки
    static string JRNLtext = "";
    // Номер символа, который сейчас проходит проверку
    static int numberCurrentChar = 0;
    // Текущий символ
    static char c;

    // Идентификатор узла дерева 
    static int id_jrnl = 0;

    // Добавление атрибута в параметры узла дерева
    // at - словарь где, ключ это тип атрибута, например text, name
    //      значение словаря - словарь, где ключ это название атрибута, например back-color
    //      а значение - значение атрибута, например green
    // typeAt - тип атрибута 
    // valueAt - значение атрибута 
    static void AddAttributes(ref Dictionary<string, Dictionary<string, string>> at, string typeAt, string nameAt, string valueAt)
    {
        if (at.ContainsKey(typeAt))
        {
            if ((at[typeAt]).ContainsKey(nameAt))
                at[typeAt][nameAt] = valueAt;
            else
                at[typeAt].Add(nameAt, valueAt);
        }
        else
            at.Add(typeAt, new Dictionary<string, string> { { nameAt, valueAt } });
    }

    // Возвращает словарь атрибутов. 
    // На вход передается строка типа:
    // "name.sdf:sdf;sdf:sdf;
    // text.color:sdf;contains:sdf;"
    static Dictionary<string, Dictionary<string, string>> getAttributes(string str) 
    {
        Dictionary<string, Dictionary<string, string>> at = new Dictionary<string, Dictionary<string, string>>();
        
        // разбиваем файл на отдельные строки
        string[] line_attributes = str.Split('\n');
        
        // начинаем цикл по каждой строке
        foreach (string line in line_attributes) 
        {
            if (line == "") continue;

            // выделяем название тип атрибута, например name или text
            string[] at_line = line.Split('.');

            // тип атрибута
            string typeAt = at_line[0];

            // склеиваем строку в изначальный вид, убирая тип атрибута
            string param_at = string.Join(".", at_line.Skip(1));

            // разбиваем строку на массив отдельных атрибутов
            string[] array_attributes = param_at.Split(';');

            // начинаем цикл по атрибутам 
            foreach (string array_attribute in array_attributes) 
            {
                if (array_attribute == "") continue;

                string[] attribute = array_attribute.Split(':');
                string nameAt = attribute[0];
                string valueAt = attribute[1];

                AddAttributes(ref at, typeAt, nameAt, valueAt);
            }
        }

        return at;
    }

    // получаем новый элемент JRNL
    static JRNL getNewElement()
    {
        // Уровень узла, определяется количеством символов ~
        int level = 0;
        string JRNLstring = "";
        JRNL element = new JRNL();

        // подсчитываем количество символов ~
        while (c == '~')
        {
            level++;
            c = NC();
        }

        while (c != '\n') c = NC();
        
        //  ?
        c = NC();

        // Считываем узел дерева пока не дойдем до следующего узла или конца строки
        while (c != '~' && c != '\0')
        {
            JRNLstring += c;
            c = NC();
        }
        
        // Выделяем атрибуты из JRNL узла
        element.attribtues = getAttributes(JRNLstring);
        element.level = level;
        element.id = id_jrnl++;

        return element;
    }

    // Получаение JRNL дерева по строке
    static public JRNL getJRNLTree(string _JRNLtext)
    {
        JRNLtext = _JRNLtext;
        // Указатель показывает на первый символ
        c = JRNLtext[0];

        JRNL root = new JRNL();

        while (c != '\0')
        {
            JRNL newElement = getNewElement();
            // Делаем теорию, что родитель нового элемента - это корневой узел
            JRNL parent = root;

            // Делаем цикл, пока не найдем родителя нового элемента
            while (newElement.level - parent.level != 1)
                // Если разница между уровнями не равен 1 - значит это не ближайшие узлы
                // Берем последний дочерний объект у родителя
                parent = parent.children[^1];

            parent.children.Add(newElement);
        }

        return root;
    }


    // перейти на следующий символ
    static char NC()
    {
        if (JRNLtext.Length <= numberCurrentChar + 1)
        {
            Console.WriteLine("END FILE!");
            return '\0';
        }

        return JRNLtext[++numberCurrentChar];
    }
}
