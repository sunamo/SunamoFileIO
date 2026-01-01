namespace SunamoFileIO;

public partial class TF
{
    /// <summary>
    /// Applies a transformation function to file content and writes to a new file with inserted text in filename if content changed.
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <param name="transformFunction">Function that transforms file content.</param>
    /// <param name="insertBetweenFilenameAndExtension">Text to insert between filename and extension for output file.</param>
    /// <returns>True if file was modified, false otherwise.</returns>
    public static
#if ASYNC
        async Task<bool>
#else
    bool
#endif
    PureFileOperation(string filePath, Func<string, string> transformFunction, string insertBetweenFilenameAndExtension)
    {
        var content =
#if ASYNC
            await
#endif
        FileMs.ReadAllTextAsync(filePath);
        var transformedContent = transformFunction.Invoke(content);
        if (transformedContent != content)
        {
#if ASYNC
            await
#endif
            WriteAllText(FS.InsertBetweenFileNameAndExtension(filePath, insertBetweenFilenameAndExtension), content);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Applies a transformation function to file content and writes back if content changed.
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <param name="transformFunction">Function that transforms file content.</param>
    /// <returns>True if file was modified, false otherwise.</returns>
    public static
#if ASYNC
        async Task<bool>
#else
    bool
#endif
    PureFileOperation(string filePath, Func<string, string> transformFunction)
    {
        var content = (
#if ASYNC
            await
#endif
        FileMs.ReadAllTextAsync(filePath)).Trim();
        var transformedContent = transformFunction.Invoke(content);
        if (string.Compare(content, transformedContent) != 0)
        {
#if ASYNC
            await
#endif
            FileMs.WriteAllTextAsync(filePath, transformedContent);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Opens a text file and returns a StreamReader.
    /// StreamReader is derived from TextReader.
    /// </summary>
    /// <param name="filePath">Path to the file to open.</param>
    /// <returns>StreamReader for reading the file.</returns>
    public static StreamReader TextReader(string filePath)
    {
        return FileMs.OpenText(filePath);
    }

    /// <summary>
    /// Creates an empty file if it doesn't exist.
    /// </summary>
    /// <param name="path">Path to the file to create.</param>
    public static async Task CreateEmptyFileWhenDoesntExists(string path)
    {
        await FileMs.WriteAllTextAsync(path, "");
    }

    /// <summary>
    /// UTF-8 BOM (Byte Order Mark) bytes: 239, 187, 191.
    /// </summary>
    public static List<byte> BomUtf8 = new List<byte>([239, 187, 191]);

    /// <summary>
    /// Removes double UTF-8 BOM from file if present.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    public static
#if ASYNC
        async Task
#else
    void
#endif
    RemoveDoubleBomUtf8(string path)
    {
        var bytes = (
#if ASYNC
            await
#endif
        FileMs.ReadAllBytesAsync(path)).ToList();
        var endIndex = bytes.Count > 5 ? 6 : bytes.Count;
        for (var i = 3; i < endIndex; i++)
            if (BomUtf8[i - 3] != bytes[i])
                break;
        bytes = bytes.Skip(3).ToList();
        await FileMs.WriteAllBytesAsync(path, bytes.ToArray());
    }
}