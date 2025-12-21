namespace SunamoFileIO;

partial class TF
{
    #region Lines
    public static
#if ASYNC
    async Task
#else
void
#endif
    AppendAllLines(string path, IEnumerable<string> notRecognized, bool deduplicate = false)
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
        list.AddRange(notRecognized);
        if (deduplicate)
            list = list.Distinct().ToList();
        await FileMs.WriteAllLinesAsync(path, list);
    }

    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllLines(string file, IList<string> lines)
    {
        if (LockedByBitLocker(file)) return;

#if ASYNC
        await File.WriteAllLinesAsync
#else
File.WriteAllLines
#endif
            (file, lines.ToArray());
    }

    public static
#if ASYNC
        async Task<List<string>>
#else
List<string>
#endif
        ReadAllLines(string file, bool trim = true)
    {
        if (!File.Exists(file))
        {
            await TF.WriteAllText(file, "");
            return new List<string>();
        }

        if (LockedByBitLocker(file)) return new List<string>();
#if ASYNC

#endif
        var result = SHGetLines.GetLines
#if ASYNC
            (await File.ReadAllTextAsync(file)).ToList();
#else
File.ReadAllText(file).ToList();
#endif
        if (trim) result = result.Where(d => !string.IsNullOrWhiteSpace(d)).ToList();
        return result;
    }

    #endregion
}