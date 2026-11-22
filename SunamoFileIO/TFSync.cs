namespace SunamoFileIO;

partial class TF
{
    #region Lines

    /// <summary>
    /// Read all lines sync.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The read all lines sync result.</returns>
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
    /// Write all lines sync.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="lines">The lines.</param>
    public static void WriteAllLinesSync(string path, List<string> lines)
    {
        File.WriteAllLines(path, lines.ToArray());
    }

    /// <summary>
    /// Append all lines sync.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="lines">The lines.</param>
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
    /// Write all text sync.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="content">The content.</param>
    public static void WriteAllTextSync(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    /// <summary>
    /// Append all text sync.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="content">The content.</param>
    public static void AppendAllTextSync(string path, string content)
    {
        if (!File.Exists(path))
        {
            TF.WriteAllTextSync(path, "");
        }

        File.AppendAllText(path, content);
    }

    /// <summary>
    /// Read all text sync.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The read all text sync result.</returns>
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
    /// Read all bytes sync.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The read all bytes sync result.</returns>
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
    /// Write all bytes sync.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="bytes">The bytes.</param>
    public static void WriteAllBytesSync(string path, List<byte> bytes)
    {
        File.WriteAllBytes(path, bytes.ToArray());
    }
    #endregion
}
