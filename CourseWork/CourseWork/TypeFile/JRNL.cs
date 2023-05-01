namespace CourseWork.TypeFile;

public struct JRNL
{
    public Dictionary<string, Dictionary<string, string>> attribtues;
    public int level;
    public int id;
    public List<JRNL> children;

    public JRNL()
    {
        attribtues = new Dictionary<string, Dictionary<string, string>>();
        level = 0;
        id = -1;
        children = new List<JRNL>();
    }
}
