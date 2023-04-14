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
    // XML
    Parser parser = new Parser(getFile("test.xml"));

    XMLelement root = parser.Parsing();

    AntiParser antiParser = new AntiParser();
    string XMLfile = antiParser.AntiParsing(root);

    saveFile(XMLfile);


    // JRNL
    ParserJRNL parserJRNL = new ParserJRNL(getFile("testJRNL.jrnl"));
    JRNL root = parserJRNL.Parsing();


    return 0;
}

main();