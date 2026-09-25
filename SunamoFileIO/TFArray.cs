namespace SunamoFileIO;

partial class TF
{
    #region Array

    public static
        async Task
        WriteAllLinesArray(string path, string[] lines)
    {
        await
            WriteAllLines(path, lines.ToList());
    }

    public static
        async Task<String[]>
        ReadAllLinesArray(string path)
    {
        return (
            await
                ReadAllLines(path)).ToArray();
    }

    public static
        async Task
        WriteAllBytesArray(string path, byte[] bytes)
    {
        await
            WriteAllBytes(path, bytes.ToList());
    }

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
