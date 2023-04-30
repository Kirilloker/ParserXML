namespace CourseWork.TypeFile;

public struct JRNL
{
    //public string name;
    public string value;
    //public List<elementJRNL> values;
    public int level;
    //public int id;
    public List<JRNL> children;

    public JRNL()
    {
        //name = "";
        value = "";
        level = -1;
        //id = -1;
        children = new List<JRNL>();
    }
}

public readonly struct elementJRNL
{
    public readonly string name;
    public readonly string value;
    public elementJRNL(string _name, string _value) { name = _name; value = _value; }
}