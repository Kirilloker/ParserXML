using CourseWork.TypeFile;
using Attribute = CourseWork.TypeFile.Attribute;

public class Converter
{

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

    public string XMLelementConvert(XML xml, int level) 
    {
        string xmlString = "";

        for (int i = 0; i < level; i++)
            xmlString += "~";

        xmlString += "\n";

        Dictionary<string, Dictionary<string, string>> at = new Dictionary<string, Dictionary<string, string>>();

        foreach (Attribute a in xml.attributes) 
            AddAttributes(ref at, a.name, "contains", a.value);

        // Разобрать дочерние объекты которые НЕ ДРУГИЕ УЗЛЫ

        foreach (XML child_attribut in xml.children) 
        {
            if (child_attribut.name == "node") continue;

            foreach (Attribute a in child_attribut.attributes)
                AddAttributes(ref at, child_attribut.name, a.name, a.value);

            foreach (var child_child in child_attribut.children)
                AddAttributes(ref at, child_attribut.name, child_child.name, child_child.value);
        }

        // xmlString += xml.name + "\n" + "-";

        foreach (var item in at)
        {
            xmlString += item.Key + ".";

            foreach (var attribute in item.Value) 
                xmlString += attribute.Key + ":" + attribute.Value + ";";
            xmlString += "\n";
           
        }

        //foreach (XML child_attribut in xml.children)
        //{
        //    if (child_attribut.name != "node") continue;

        //    xmlString += XMLelementConvert(child_attribut, level + 1) + "\n";
        //}

        return xmlString;
    }


    public string Converting(XML root, int level = 1) 
    {
        string JRNLstring = "";

        foreach (XML child in root.children)
        {

            if (child.name == "node")
            {
                JRNLstring += XMLelementConvert(child, level) ;
                JRNLstring += Converting(child, level + 1);
            }
        }

        return JRNLstring;
    }
}
