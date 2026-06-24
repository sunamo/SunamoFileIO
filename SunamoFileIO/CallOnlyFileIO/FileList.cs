namespace SunamoFileIO.CallOnlyFileIO;

public class FileList
{
    public static async Task<List<string>> ReadAllLinesListAsync(string path)
    {
        return SHGetLines.GetLines(await FileAsync.ReadAllTextAsync(path)).ToList();
    }

    public static async Task<List<byte>> ReadAllBytesListAsync(string path)
    {
        return (await FileAsync.ReadAllBytesAsync(path)).ToList();
    }

    public static async Task WriteAllLinesListAsync(string path, IList<string> lines)
    {
        await FileAsync.WriteAllLinesAsync(path, lines.ToUnixLineEnding());
    }

    public static async Task WriteAllBytesListAsync(string path, List<byte> bytes)
    {
        await FileAsync.WriteAllBytesAsync(path, bytes.ToArray());
    }
}
