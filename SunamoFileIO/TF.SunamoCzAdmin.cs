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
    /// Saves the content to the file and creates the missing parent folder first; null content is written as an empty file.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="path">The path.</param>
    public static void SaveFile(string content, string path)
    {
        var folder = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(folder))
            Directory.CreateDirectory(folder);
        File.WriteAllText(path, content ?? string.Empty);
    }
}
