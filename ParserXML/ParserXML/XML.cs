public struct XMLelement
{
    public string name;
    public string value;
    public List<Attribute> attributes;
    public List<XMLelement> children;
    //public XMLelement parent;

    public XMLelement() 
    {
        name = "";
        value = "";
        attributes = new List<Attribute>();
        children = new List<XMLelement>();
    }
}


public readonly struct Attribute 
{
    public readonly string name;
    public readonly string value;

    public Attribute(string _name, string _value) { name = _name; value = _value; }
}





/*
    public override string ToString()
    {
        string print = "";
        print += "Name: " + name + "\n";
        print += "Value: " + value + "\n";
        

        if (attributes.Count != 0) 
        {
            print += "Attributes: \n";
            for (int i = 0; i < attributes.Count; i++)
            {
                print += "----------------------" + "\n";
                print += "\tname: " + attributes[i].name + "\n";
                print += "\tvalue: " + attributes[i].value + "\n";
                print += "----------------------" + "\n";
            }
        }


        if (children.Count != 0) 
        {
            print += "Children: \n";
            for (int i = 0; i < children.Count; i++)
            {
                print += "----------------------" + "\n";
                print += children[i].ToString();
                print += "----------------------" + "\n";
            }
        }


        return print;
    }
 */