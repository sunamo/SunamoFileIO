namespace SunamoFileIO;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
partial class TF
{
    #region Lines
    public static List<string> ReadAllLinesSync(string path)
    {
        if (!File.Exists(path))
        {
            WriteAllTextSync(path, string.Empty);
            return new List<string>();
        }

        return SHGetLines.GetLines(File.ReadAllText(path));
    }

    public static void WriteAllLinesSync(string path, List<string> content)
    {
        File.WriteAllLines(path, content.ToArray());
    }

    public static void AppendAllLinesSync(string path, List<string> content)
    {
        if (!File.Exists(path))
        {
            TF.WriteAllTextSync(path, "");
        }

        File.AppendAllLines(path, content.ToArray());
    }
    #endregion

    #region Text
    public static void WriteAllTextSync(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    public static void AppendAllTextSync(string path, string content)
    {
        if (!File.Exists(path))
        {
            TF.WriteAllTextSync(path, "");
        }

        File.AppendAllText(path, content);
    }

    public static string ReadAllTextSync(string path)
    {
        if (!File.Exists(path))
        {
            TF.WriteAllTextSync(path, "");
            return "";
        }

        return ReadAllTextSync(path);
    }
    #endregion





    #region Bytes
    public static List<byte> ReadAllBytesSync(string path)
    {
        if (!File.Exists(path))
        {
            TF.WriteAllTextSync(path, "");
            return new List<byte>();
        }

        return File.ReadAllBytes(path).ToList();
    }

    public static void WriteAllBytesSync(string path, List<byte> content)
    {
        File.WriteAllBytes(path, content.ToArray());
    }
    #endregion






}