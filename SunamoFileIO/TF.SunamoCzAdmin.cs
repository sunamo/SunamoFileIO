namespace SunamoFileIO;

public partial class TF
{
    /// <summary>
    /// Save lines.
    /// </summary>
    /// <param name="lines">The lines.</param>
    /// <param name="path">The path.</param>
    public static void SaveLines(List<string> lines, string path)
    {
        File.WriteAllLines(path, lines);
    }

    /// <summary>
    /// Save file.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="path">The path.</param>
    public static void SaveFile(string content, string path)
    {
        File.WriteAllText(path, content);
    }
}
