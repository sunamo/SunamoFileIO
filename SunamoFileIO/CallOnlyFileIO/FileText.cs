namespace SunamoFileIO.CallOnlyFileIO;

/// <summary>
/// Represents the file text type.
/// </summary>
public class FileText
{
    /// <summary>
    /// Read all text.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The read all text result.</returns>
    public async Task<string> ReadAllText(string path)
    {
        return await FileAsync.ReadAllTextAsync(path);
    }

    /// <summary>
    /// Write all text.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="content">The content.</param>
    public async Task WriteAllText(string path, string content)
    {
        await FileAsync.WriteAllTextAsync(path, content);
    }
}
