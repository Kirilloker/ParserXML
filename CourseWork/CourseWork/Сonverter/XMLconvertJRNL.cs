using CourseWork.TypeFile;
using System.Diagnostics;
using System.Xml.Linq;
using Attribute = CourseWork.TypeFile.Attribute;

namespace CourseWork.Сonverter;

public static class XMLconvertJRNL
{
    // Открытие файла
    static string openFile(string nameFile)
    {
        StreamReader streamReader = new StreamReader(nameFile);
        string str = "";

        while (!streamReader.EndOfStream)
            str += streamReader.ReadLine();

        return str;
    }

    // Сохранение файла
    static void saveFile(string name, string file)
    {
        File.WriteAllText(name, file);
    }


    // Получить JRNL дерево по xml файлу
    static public JRNL getJRNL(string nameFile = "test.xml")
    {
        var timer = new Stopwatch();
        string time = "";
        timer.Start();

        // XML файл парсим в ООП формат
        XML rootXML = ParserXML.getXMLTree(openFile(nameFile));

        timer.Stop();
        time += "1: " + timer.Elapsed.TotalSeconds + "\n";
        timer.Reset();

        timer.Start();
        // Конвертируем полученный файл в string типа jrnl
        string JRNLstring = ConvertingElement(rootXML);

        timer.Stop();
        time += "2: " + timer.Elapsed.TotalSeconds + "\n";
        timer.Reset();
        
        // сохраняем jrnl файл
        saveFile("D:\\Charp\\CourseWorkParser\\ParserXML\\CourseWork\\CourseWork\\temp.jrnl", JRNLstring);

        timer.Start();

        // JRNL файл парсим в ООП формат
        JRNL rootJRNL = ParserJRNL.getJRNLTree(JRNLstring);

        timer.Stop();
        time += "3: " + timer.Elapsed.TotalSeconds + "\n";
        timer.Reset();


        File.WriteAllText("D:\\Charp\\CourseWorkParser\\ParserXML\\CourseWork\\CourseWork\\log.txt", time);

        return rootJRNL;
    }



    // Добавление атрибута в параметры узла дерева
    // at - словарь где, ключ это тип атрибута, например text, name
    //      значение словаря - словарь, где ключ это название атрибута, например back-color
    //      а значение - значение атрибута, например green
    // typeAt - тип атрибута 
    // valueAt - значение атрибута 
    static void AddAttributes(ref Dictionary<string, Dictionary<string, string>> at, string firstKey, string secondKey, string value)
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

    // Форматирование XML в JRNL формат
    static string XMLConvert(XML xml, int level)
    {
        string xmlString = "";

        // Добавляем символы ~ взависимости от уровня элемента
        for (int i = 0; i < level; i++)
            xmlString += "~";

        xmlString += "\n";

        // Словарь атрибутов
        Dictionary<string, Dictionary<string, string>> at = new Dictionary<string, Dictionary<string, string>>();

        // Все атрибуты которые относятся к узлу XML добавляем в словарь
        foreach (Attribute a in xml.attributes)
            AddAttributes(ref at, a.name, "contains", a.value);

        // Конвертируем дочерние объекты, которые не являеются другими узлами
        foreach (XML child_attribut in xml.children)
        {
            if (child_attribut.name == "node") continue;

            foreach (Attribute a in child_attribut.attributes)
                AddAttributes(ref at, child_attribut.name, a.name, a.value);

            foreach (var child_child in child_attribut.children)
                AddAttributes(ref at, child_attribut.name, child_child.name, child_child.value);
        }

        foreach (var item in at)
        {
            // Добавляем тип атрибута
            xmlString += item.Key + ".";

            // Добавляем имя и значение атрибута
            foreach (var attribute in item.Value)
                xmlString += attribute.Key + ":" + attribute.Value + ";";
            
            xmlString += "\n";
        }

        return xmlString;
    }


    static public string ConvertingElement(XML root, int level = 1)
    {
        string JRNLstring = "";

        // Начинаем перебирать все элементы XML
        foreach (XML child in root.children)
        {
            // И если это проверяемый узел является следующий узлом, а не атрибутом
            if (child.name == "node")
            {
                // Конвертируем этот элемент под формат JRNL
                JRNLstring += XMLConvert(child, level);
                JRNLstring += ConvertingElement(child, level + 1);
            }
        }

        return JRNLstring;
    }
}
