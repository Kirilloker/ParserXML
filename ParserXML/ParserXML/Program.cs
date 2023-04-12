using ParserXML;

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
    Parser parser = new Parser(getFile("test.xml"));
    
    XMLelement root = parser.Parsing();

    AntiParser antiParser = new AntiParser();
    string XMLfile = antiParser.AntiParsing(root, 0);

    Console.WriteLine(XMLfile);
    saveFile(XMLfile);

    return 0;
}

main();