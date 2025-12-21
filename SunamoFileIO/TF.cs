namespace SunamoFileIO;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public partial class TF
{
#if DEBUG
    public const int waitMsBeforeReadFile = 1000;
#endif
    public static Func<string, bool> isUsed = null;
#pragma warning disable
    protected static bool LockedByBitLocker(string path)
#pragma warning restore
    {
        // na to chce mít vlastní metodu v ThrowEx. Bitlocker jsem zatím do nugetů nepřevedl
        //return ThrowEx.Custom($"{path} locked by bitlocker");
        return false;
    }

    private static Type type = typeof(TF);
    public static bool throwExcIfCantBeWrite = false;
    public static bool readFile = true;
#if ASYNC
    public static async Task<string> WaitD()
    {
        return await Task.Run(() => "");
    }
#endif
    public static string ReadFileParallel(string fileName, IList<string> from, IList<string> to)
    {
        return ReadFileParallel(fileName, 1470, from, to);
    }

    public static string ReadFileParallel(string fileName, int linesCount, IList<string> from, IList<string> to)
    {
        var AllLines = new string[linesCount]; //only allocate memory here
        using (var sr = FileMs.OpenText(fileName))
        {
            var xValue = 0;
            while (!sr.EndOfStream)
            {
                AllLines[xValue] = sr.ReadLine();
                xValue += 1;
            }
        } //CLOSE THE FILE because we are now DONE with it.

        if (from != null)
            for (var i = 0; i < from.Count; i++)
                Parallel.For(0, AllLines.Length, lineIndex =>
                {
                    AllLines[lineIndex] = AllLines[lineIndex].Replace(from[i], to[i]);
                });
        return string.Empty;
    }

    public static 
#if ASYNC
        async Task<List<string>>
#else
    List<string> 
#endif
    ReadConfigLines(string syncLocations)
    {
        var list = SHGetLines.GetLines(
#if ASYNC
            await
#endif
        FileMs.ReadAllTextAsync(syncLocations)).ToList();
        list = list.Where(d => !d.StartsWith("#")).ToList();
        return list;
    }

    public static Encoding GetEncoding(string filename)
    {
        var file = new FileStream(filename, FileMode.Open, FileAccess.Read);
        // Read the BOM
        var enc = GetEncoding(file);
        file.Dispose();
        return enc;
    }

    /// <summary>
    ///     Dont working, with Air bank export return US-ascii / 1252, file has diacritic
    ///     Atom with auto-encoding return ISO-8859-2 which is right
    /// </summary>
    /// <param name = "file"></param>
    public static Encoding GetEncoding(FileStream file)
    {
        var bom = new byte[4];
        file.Read(bom, 0, 4);
        return EncodingHelper.DetectEncoding(new List<byte>(bom));
    }

    public static void Delete(string p)
    {
        FileMs.Delete(p);
    }

    public static void Move(string source, string dest, bool overwrite = false)
    {
        FileMs.Move(source, dest, overwrite);
    }

    public static void Copy(string source, string dest, bool overwrite = false)
    {
        FileMs.Copy(source, dest, overwrite);
    }

    public static bool Exists(string p)
    {
        return FileMs.Exists(p);
    }

    private static 
#if ASYNC
        async Task
#else
    void 
#endif
    AppendToStartOfFileIfDontContains(List<string> files, string append)
    {
        append += Environment.NewLine;
        foreach (var item in files)
        {
            var content = 
#if ASYNC
                await
#endif
            FileMs.ReadAllTextAsync(item);
            if (!content.Contains(append))
            {
                content = append + content;
                await FileMs.WriteAllTextAsync(item, content);
            }
        }
    }

    public static object ReadFileOrReturn(string list)
    {
        if (list.Length > 250)
            return list;
        if (FileMs.Exists(list))
            return FileMs.ReadAllTextAsync(list);
        return list;
    }

    /// <summary>
    ///     ...
    /// </summary>
    /// <param name = "file"></param>
    public static 
#if ASYNC
        async Task<int>
#else
    int 
#endif
    GetNumberOfLinesTrimEnd(string file)
    {
        var lines = SHGetLines.GetLines(
#if ASYNC
            await
#endif
        FileMs.ReadAllTextAsync(file)).ToList();
        for (var i = lines.Count - 1; i >= 0; i--)
            if (lines[i].Trim() != "")
                return i;
        return 0;
    }

    private static 
#if ASYNC
        async Task
#else
    void 
#endif
    ReplaceIfDontStartWith(List<string> files, string contains, string prefix)
    {
        foreach (var item in files)
        {
            var lines = SHGetLines.GetLines(
#if ASYNC
                await
#endif
            FileMs.ReadAllTextAsync(item, null)).ToList();
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i].Trim();
                if (line.StartsWith(contains))
                    lines[i] = lines[i].Replace(contains, prefix + contains);
            }

            await FileMs.WriteAllLinesAsync(item, lines);
        }
    }

    public static 
#if ASYNC
        async Task
#else
    void 
#endif
    Replace(string pathCsproj, string to, string from)
    {
        var content = 
#if ASYNC
            await
#endif
        FileMs.ReadAllTextAsync(pathCsproj);
        content = content.Replace(to, from);
#if ASYNC
        await
#endif
        FileMs.WriteAllTextAsync(pathCsproj, content);
    }

    public static 
#if ASYNC
        async Task<bool>
#else
    void 
#endif
    PureFileOperationWithArg(string f, Func<string, string, string> transformHtmlToMetro4, string arg)
    {
        var content = 
#if ASYNC
            await
#endif
        FileMs.ReadAllTextAsync(f);
        var content2 = transformHtmlToMetro4.Invoke(content, arg);
        if (content.Trim() != content2.Trim())
        {
            await FileMs.WriteAllTextAsync(f, content2);
            return true;
        }

        return false;
    }
}