namespace SunamoFileIO.CallOnlyFileIO;

/// <summary>
/// EN: Provides async methods for reading and writing text files
/// CZ: Poskytuje asynchronní metody pro čtení a zápis textových souborů
/// </summary>
public class FileText
{
    /// <summary>
    /// EN: Reads all text from file asynchronously
    /// CZ: Čte veškerý text ze souboru asynchronně
    /// </summary>
    public async Task<string> ReadAllText(string path)
    {
        return await File.ReadAllTextAsync(path);
    }

    /// <summary>
    /// EN: Writes all text to file asynchronously
    /// CZ: Zapisuje veškerý text do souboru asynchronně
    /// </summary>
    public async Task WriteAllText(string path, string content)
    {
        await File.WriteAllTextAsync(path, content);
    }
}