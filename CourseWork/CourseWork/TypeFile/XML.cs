namespace CourseWork.TypeFile;

public struct XML
{
    public string name;
    public string value;
    public List<Attribute> attributes;
    public List<XML> children;

    public XML()
    {
        name = "";
        value = "";
        attributes = new List<Attribute>();
        children = new List<XML>();
    }
}


public readonly struct Attribute
{
    public readonly string name;
    public readonly string value;
    public Attribute(string _name, string _value) { name = _name; value = _value; }
}