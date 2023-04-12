
using System.Xml.Linq;

public class XMLelement
{
    public string name = "";
    public string value = "";
    public List<Attribute> attributes = new List<Attribute>();
    public List<XMLelement> children = new List<XMLelement>();
    //public XMLelement parent;


    public XMLelement() { }

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
}


public readonly struct Attribute 
{
    public readonly string name;
    public readonly string value;

    public Attribute(string _name, string _value) { name = _name; value = _value; }
}