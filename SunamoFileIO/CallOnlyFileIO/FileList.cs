namespace SunamoFileIO.CallOnlyFileIO;

/// <summary>
/// EN: Provides async methods for reading and writing files as lists (lines or bytes)
/// CZ: Poskytuje asynchronní metody pro čtení a zápis souborů jako seznamů (řádků nebo bytů)
/// </summary>
public class FileList
{
    /// <summary>
    /// EN: Reads all lines from file asynchronously and returns as list
    /// CZ: Čte všechny řádky ze souboru asynchronně a vrátí jako seznam
    /// </summary>
    /// <param name="path">Path to the file</param>
    /// <returns>List of lines from file</returns>
    public static async Task<List<string>> ReadAllLinesListAsync(string path)
    {
        return SHGetLines.GetLines (await File.ReadAllTextAsync(path)).ToList();
    }

    /// <summary>
    /// EN: Reads all bytes from file asynchronously and returns as list
    /// CZ: Čte všechny byty ze souboru asynchronně a vrátí jako seznam
    /// </summary>
    /// <param name="path">Path to the file</param>
    /// <returns>List of bytes from file</returns>
    public static async Task<List<byte>> ReadAllBytesListAsync(string path)
    {
        return (await File.ReadAllBytesAsync(path)).ToList();
    }

    /// <summary>
    /// EN: Writes all lines to file asynchronously with Unix line endings
    /// CZ: Zapisuje všechny řádky do souboru asynchronně s Unix konci řádků
    /// </summary>
    /// <param name="path">Path to the file</param>
    /// <param name="lines">Lines to write</param>
    public static async Task WriteAllLinesListAsync(string path, IList<string> lines)
    {
        await File.WriteAllLinesAsync(path, lines.ToUnixLineEnding());
    }

    /// <summary>
    /// EN: Writes all bytes to file asynchronously
    /// CZ: Zapisuje všechny byty do souboru asynchronně
    /// </summary>
    /// <param name="path">Path to the file</param>
    /// <param name="bytes">Bytes to write</param>
    public static async Task WriteAllBytesListAsync(string path, List<byte> bytes)
    {
        await File.WriteAllBytesAsync(path, bytes.ToArray());
    }
}