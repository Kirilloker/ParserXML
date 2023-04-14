using ParserXML;
using System.Collections;
using System.Text.Json;

string getFile(string nameFile) 
{
    StreamReader streamReader = new StreamReader(nameFile);
    string str = "";

    while (!streamReader.EndOfStream)
    {
        str += streamReader.ReadLine();
    }

    return str;
}

void saveFile(string file) 
{
    File.WriteAllText("testXMl.xml", file);
}


int main() 
{
    //Parser parser = new Parser(getFile("test.xml"));
    
    //XMLelement root = parser.Parsing();

    //AntiParser antiParser = new AntiParser();
    //string XMLfile = antiParser.AntiParsing(root);

    //Console.WriteLine(XMLfile);
    //saveFile(XMLfile);

    ParserJRNL parserJRNL = new ParserJRNL(getFile("testJRNL.jrnl"));

    Hashtable hashtable = parserJRNL.Parsing();
    foreach (DictionaryEntry entry in hashtable) 
    {
        Console.WriteLine(entry.Key);
        Console.WriteLine(((JRNL)entry.Value).name);
        Console.WriteLine(((JRNL)entry.Value).value);
        Console.WriteLine("0000000");
    }
    return 0;
}

main();