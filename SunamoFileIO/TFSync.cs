namespace SunamoFileIO;

partial class TF
{
    #region Lines

    /// <summary>
    /// Reads all lines from a file synchronously.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <returns>List of lines from file.</returns>
    public static List<string> ReadAllLinesSync(string path)
    {
        if (!File.Exists(path))
        {
            WriteAllTextSync(path, string.Empty);
            return new List<string>();
        }

        return SHGetLines.GetLines(File.ReadAllText(path));
    }

    /// <summary>
    /// Writes all lines to a file synchronously.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="lines">Lines to write to file.</param>
    public static void WriteAllLinesSync(string path, List<string> lines)
    {
        File.WriteAllLines(path, lines.ToArray());
    }

    /// <summary>
    /// Appends lines to a file synchronously.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="lines">Lines to append to file.</param>
    public static void AppendAllLinesSync(string path, List<string> lines)
    {
        if (!File.Exists(path))
        {
            TF.WriteAllTextSync(path, "");
        }

        File.AppendAllLines(path, lines.ToArray());
    }
    #endregion

    #region Text

    /// <summary>
    /// Writes text to a file synchronously.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="content">Content to write to file.</param>
    public static void WriteAllTextSync(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    /// <summary>
    /// Appends text to a file synchronously.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="content">Content to append to file.</param>
    public static void AppendAllTextSync(string path, string content)
    {
        if (!File.Exists(path))
        {
            TF.WriteAllTextSync(path, "");
        }

        File.AppendAllText(path, content);
    }

    /// <summary>
    /// Reads all text from a file synchronously.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <returns>Content of the file, or empty string if file doesn't exist.</returns>
    public static string ReadAllTextSync(string path)
    {
        if (!File.Exists(path))
        {
            TF.WriteAllTextSync(path, "");
            return "";
        }

        return File.ReadAllText(path);
    }
    #endregion

    #region Bytes

    /// <summary>
    /// Reads all bytes from a file synchronously.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <returns>List of bytes from file.</returns>
    public static List<byte> ReadAllBytesSync(string path)
    {
        if (!File.Exists(path))
        {
            TF.WriteAllTextSync(path, "");
            return new List<byte>();
        }

        return File.ReadAllBytes(path).ToList();
    }

    /// <summary>
    /// Writes all bytes to a file synchronously.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="bytes">Bytes to write to file.</param>
    public static void WriteAllBytesSync(string path, List<byte> bytes)
    {
        File.WriteAllBytes(path, bytes.ToArray());
    }
    #endregion
}
