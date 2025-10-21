// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoFileIO.CallOnlyFileIO;

public class FileText
{
    public async Task<string> ReadAllText(string path)
    {
        return await File.ReadAllTextAsync(path);
    }

    public async Task WriteAllText(string path, string c)
    {
        await File.WriteAllTextAsync(path, c);
    }
}
