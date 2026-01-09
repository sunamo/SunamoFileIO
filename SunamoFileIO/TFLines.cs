namespace SunamoFileIO;

partial class TF
{
    #region Lines

    /// <summary>
    /// Appends lines to a file, optionally removing duplicates.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="linesToAppend">Lines to append to file.</param>
    /// <param name="isDuplicatingRemoving">Whether to remove duplicate lines after appending.</param>
    public static
#if ASYNC
    async Task
#else
void
#endif
    AppendAllLines(string path, IEnumerable<string> linesToAppend, bool isDuplicatingRemoving = false)
    {
        if (!File.Exists(path))
        {
            await TF.WriteAllText(path, "");
        }

        var list = SHGetLines.GetLines(
#if ASYNC
            await
#endif
                FileMs.ReadAllTextAsync(path)).ToList();
        list.AddRange(linesToAppend);
        if (isDuplicatingRemoving)
            list = list.Distinct().ToList();
        await FileMs.WriteAllLinesAsync(path, list);
    }

    /// <summary>
    /// Writes all lines to a file.
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <param name="lines">Lines to write to file.</param>
    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllLines(string filePath, IList<string> lines)
    {
        if (LockedByBitLocker(filePath)) return;

#if ASYNC
        await File.WriteAllLinesAsync
#else
File.WriteAllLines
#endif
            (filePath, lines.ToArray());
    }

    /// <summary>
    /// Reads all lines from a file, optionally trimming empty lines.
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <param name="isTrimmingEmptyLines">Whether to remove empty or whitespace-only lines.</param>
    /// <returns>List of lines from file.</returns>
    public static
#if ASYNC
        async Task<List<string>>
#else
List<string>
#endif
        ReadAllLines(string filePath, bool isTrimmingEmptyLines = true)
    {
        if (!File.Exists(filePath))
        {
            await TF.WriteAllText(filePath, "");
            return new List<string>();
        }

        if (LockedByBitLocker(filePath)) return new List<string>();

        var result = SHGetLines.GetLines
#if ASYNC
            (await File.ReadAllTextAsync(filePath)).ToList();
#else
File.ReadAllText(filePath).ToList();
#endif
        if (isTrimmingEmptyLines) result = result.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
        return result;
    }

    #endregion
}