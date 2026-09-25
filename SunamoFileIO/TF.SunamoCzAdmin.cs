namespace SunamoFileIO;

public partial class TF
{
    public static void SaveLines(List<string> lines, string path)
    {
        File.WriteAllLines(path, lines);
    }

    public static void SaveFile(string content, string path)
    {
        File.WriteAllText(path, content);
    }
}
