namespace SunamoFileIO;

public partial class TF
{
    public static
        async Task<bool>
    PureFileOperation(string filePath, Func<string, string> transformFunction, string insertBetweenFilenameAndExtension)
    {
        var content =
            await FileAsync.ReadAllTextAsync(filePath);
        var transformedContent = transformFunction.Invoke(content);
        if (transformedContent != content)
        {
            await WriteAllText(FS.InsertBetweenFileNameAndExtension(filePath, insertBetweenFilenameAndExtension), content);
            return true;
        }

        return false;
    }

    public static
        async Task<bool>
    PureFileOperation(string filePath, Func<string, string> transformFunction)
    {
        var content = (
            await FileAsync.ReadAllTextAsync(filePath)
        ).Trim();
        var transformedContent = transformFunction.Invoke(content);
        if (string.Compare(content, transformedContent) != 0)
        {
            await FileAsync.WriteAllTextAsync(filePath, transformedContent);
            return true;
        }

        return false;
    }

    // StreamReader is derived from TextReader.
    public static StreamReader TextReader(string filePath)
    {
        return FileMs.OpenText(filePath);
    }

    public static async Task CreateEmptyFileWhenDoesntExists(string path)
    {
        await FileAsync.WriteAllTextAsync(path, "");
    }

    // UTF-8 BOM (Byte Order Mark) bytes: 239, 187, 191.
    public static readonly List<byte> BomUtf8 = new List<byte>([239, 187, 191]);

    public static
        async Task
    RemoveDoubleBomUtf8(string path)
    {
        var bytes = (
            await FileAsync.ReadAllBytesAsync(path)
        ).ToList();
        var endIndex = bytes.Count > 5 ? 6 : bytes.Count;
        for (var i = 3; i < endIndex; i++)
            if (BomUtf8[i - 3] != bytes[i])
                break;
        bytes = bytes.Skip(3).ToList();
        await FileAsync.WriteAllBytesAsync(path, bytes.ToArray());
    }
}
