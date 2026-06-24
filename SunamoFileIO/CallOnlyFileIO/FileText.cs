namespace SunamoFileIO.CallOnlyFileIO;

public class FileText
{
    public async Task<string> ReadAllText(string path)
    {
        return await FileAsync.ReadAllTextAsync(path);
    }

    public async Task WriteAllText(string path, string content)
    {
        await FileAsync.WriteAllTextAsync(path, content);
    }
}
