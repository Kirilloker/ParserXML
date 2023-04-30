using CourseWork.TypeFile;

namespace CourseWork.Сonverter;

public static class XMLconvertJRNL
{
    static string getFile(string nameFile)
    {
        StreamReader streamReader = new StreamReader(nameFile);
        string str = "";

        while (!streamReader.EndOfStream)
            str += streamReader.ReadLine();

        return str;
    }

    static void saveFile(string name, string file)
    {
        File.WriteAllText(name, file);
    }

    static public JRNL getJRNL(string nameFile = "test.xml")
    {
        // XML файл парсим в ООП формат
        ParserXML parserXML = new ParserXML(getFile(nameFile));
        XML rootXML = parserXML.Parsing();

        // Конвертируем полученный файл в string типа jrnl
        Converter converter = new Converter();
        string JRNLstring = converter.Converting(rootXML);
        Console.WriteLine(JRNLstring);
        saveFile("testJRNL.jrnl", JRNLstring);

        // JRNL файл парсим в ООП формат
        ParserJRNL parserJRNL = new ParserJRNL(JRNLstring);
        JRNL rootJRNL = parserJRNL.Parsing();

        return rootJRNL;
    }
}
