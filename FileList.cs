using SunamoFileIO._sunamo;

namespace SunamoFileIO;
public class FileList
{
    public static async Task<List<string>> ReadAllLinesListAsync(string path)
    {
        return (await File.ReadAllLinesAsync(path)).ToList();
    }

    public static async Task<List<byte>> ReadAllBytesListAsync(string path)
    {
        return (await File.ReadAllBytesAsync(path)).ToList();
    }

    public static async Task WriteAllLinesListAsync(string path, IList<string> lines)
    {
        await File.WriteAllLinesAsync(path, lines.ToUnixLineEnding());
    }

    public static async Task WriteAllBytesListAsync(string path, List<byte> bytes)
    {
        await File.WriteAllBytesAsync(path, bytes.ToArray());
    }
}
