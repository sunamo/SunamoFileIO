namespace SunamoFileIO;

partial class TF
{
    #region Array

    /// <summary>
    /// Write all lines array.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="lines">The lines.</param>
    public static
        async Task
        WriteAllLinesArray(string path, string[] lines)
    {
        await
            WriteAllLines(path, lines.ToList());
    }

    /// <summary>
    /// Read all lines array.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The read all lines array result.</returns>
    public static
        async Task<String[]>
        ReadAllLinesArray(string path)
    {
        return (
            await
                ReadAllLines(path)).ToArray();
    }

    /// <summary>
    /// Write all bytes array.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="bytes">The bytes.</param>
    public static
        async Task
        WriteAllBytesArray(string path, byte[] bytes)
    {
        await
            WriteAllBytes(path, bytes.ToList());
    }

    /// <summary>
    /// Read all bytes array.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The read all bytes array result.</returns>
    public static
        async Task<byte[]>
        ReadAllBytesArray(string path)
    {
        return (
            await
                ReadAllBytes(path)).ToArray();
    }

    #endregion
}
