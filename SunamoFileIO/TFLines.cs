namespace SunamoFileIO;

partial class TF
{
    #region Lines

    public static
    async Task
    AppendAllLines(string filePath, IEnumerable<string> linesToAppend, bool isDuplicatingRemoving = false)
    {
        if (!File.Exists(filePath))
        {
            await TF.WriteAllText(filePath, "");
        }

        var list = SHGetLines.GetLines(
            await FileAsync.ReadAllTextAsync(filePath)
                ).ToList();
        list.AddRange(linesToAppend);
        if (isDuplicatingRemoving)
            list = list.Distinct().ToList();
        await FileAsync.WriteAllLinesAsync(filePath, list);
    }

    public static
        async Task
        WriteAllLines(string filePath, IList<string> lines)
    {
        if (LockedByBitLocker(filePath)) return;

        await FileAsync.WriteAllLinesAsync
            (filePath, lines.ToArray());
    }

    public static
        async Task<List<string>>
        ReadAllLines(string filePath, bool isTrimmingEmptyLines = true)
    {
        if (!File.Exists(filePath))
        {
            await TF.WriteAllText(filePath, "");
            return new List<string>();
        }

        if (LockedByBitLocker(filePath)) return new List<string>();

        var result = SHGetLines.GetLines(
            await FileAsync.ReadAllTextAsync(filePath)
            ).ToList();
        if (isTrimmingEmptyLines) result = result.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
        return result;
    }

    #endregion
}
