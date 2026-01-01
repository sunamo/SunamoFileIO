namespace SunamoFileIO;

partial class TF
{
    #region Array

    /// <summary>
    /// Writes all lines from string array to file.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="lines">Array of lines to write.</param>
    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllLinesArray(string path, string[] lines)
    {
#if ASYNC
        await
#endif
            WriteAllLines(path, lines.ToList());
    }

    /// <summary>
    /// Reads all lines from file and returns as string array.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <returns>Array of lines from file.</returns>
    public static
#if ASYNC
        async Task<String[]>
#else
String[]
#endif
        ReadAllLinesArray(string path)
    {
        return (
#if ASYNC
            await
#endif
                ReadAllLines(path)).ToArray();
    }

    /// <summary>
    /// Writes all bytes from byte array to file.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="bytes">Array of bytes to write.</param>
    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllBytesArray(string path, byte[] bytes)
    {
#if ASYNC
        await
#endif
            WriteAllBytes(path, bytes.ToList());
    }

    /// <summary>
    /// Reads all bytes from file and returns as byte array.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <returns>Array of bytes from file.</returns>
    public static
#if ASYNC
        async Task<byte[]>
#else
byte[]
#endif
        ReadAllBytesArray(string path)
    {
        return (
#if ASYNC
            await
#endif
                ReadAllBytes(path)).ToArray();
    }

    #endregion
}