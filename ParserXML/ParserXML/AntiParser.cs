namespace ParserXML;

public class AntiParser
{
    public string AntiParsing(XMLelement element, int depth = 0) 
    {
        // Итоговая строка 
        string str = "";

        // Отступ от начала строки
        string indent = "";

        // Формируй отступ = табуляция * уровень рекурсии
        for (int i = 0; i < depth; i++)
            indent += "\t";

        // Формируем начальный тэг
        str += indent + "<" + element.name;

        // Добавляем атрибуты 
        if (element.attributes.Count != 0) 
            for (int i = 0; i < element.attributes.Count; i++)
                str += " " + element.attributes[i].name + "=" + '"' + element.attributes[i].value + '"';
        
        // Закрываем начальный тэгы
        str += ">";

        // Добавляем значение тэга если есть
        if (element.value.Length > 0)
            str += element.value;

        // Если есть дочерние объекты 
        if (element.children.Count != 0) 
        {
            // Добавляем каждый из них в строку с помощью рекурсии
            for (int i = 0; i < element.children.Count; i++)
            {
                str += "\n";
                str += AntiParsing(element.children[i], depth + 1);
            }

            // Делаем отступ для закрывающего тега
            str += "\n";
            str += indent;
        } 

        // Формируем закрывающий тэг
        str += "</" + element.name + ">"; 

        return str;
    }
}
