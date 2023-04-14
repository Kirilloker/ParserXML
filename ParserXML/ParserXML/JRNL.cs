public struct JRNL
{
    public string name;
    public string value;
    public int level;
    public List<JRNL> children;

    public JRNL() 
    {
        level = 0;
        name = "";
        value = "";
        children = new List<JRNL>();
    }
}
