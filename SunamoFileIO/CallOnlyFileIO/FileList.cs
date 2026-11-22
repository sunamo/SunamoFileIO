namespace SunamoFileIO.CallOnlyFileIO;

/// <summary>
/// Represents the file list type.
/// </summary>
public class FileList
{
    /// <summary>
    /// Read all lines list async.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The read all lines list async result.</returns>
    public static async Task<List<string>> ReadAllLinesListAsync(string path)
    {
        return SHGetLines.GetLines(await FileAsync.ReadAllTextAsync(path)).ToList();
    }

    /// <summary>
    /// Read all bytes list async.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The read all bytes list async result.</returns>
    public static async Task<List<byte>> ReadAllBytesListAsync(string path)
    {
        return (await FileAsync.ReadAllBytesAsync(path)).ToList();
    }

    /// <summary>
    /// Write all lines list async.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="lines">The lines.</param>
    public static async Task WriteAllLinesListAsync(string path, IList<string> lines)
    {
        await FileAsync.WriteAllLinesAsync(path, lines.ToUnixLineEnding());
    }

    /// <summary>
    /// Write all bytes list async.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="bytes">The bytes.</param>
    public static async Task WriteAllBytesListAsync(string path, List<byte> bytes)
    {
        await FileAsync.WriteAllBytesAsync(path, bytes.ToArray());
    }
}
