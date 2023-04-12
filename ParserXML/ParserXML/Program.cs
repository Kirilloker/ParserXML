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

int main() 
{
    //Console.WriteLine("start file:" + getFile("test.xml"));
    Parser parser = new Parser(getFile("test.xml"));
    XMLelement root = parser.Parsing();


    Console.WriteLine(root);
    //Console.WriteLine("End Programm");

    return 0;
}

main();