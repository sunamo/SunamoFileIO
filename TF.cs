namespace SunamoFileIO;


public partial class TF
{

#if DEBUG
    public const int waitMsBeforeReadFile = 1000;
#endif
    public static Func<string, bool> isUsed = null;

    protected static bool LockedByBitLocker(string path)
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
            var x = 0;
            while (!sr.EndOfStream)
            {
                AllLines[x] = sr.ReadLine();
                x += 1;
            }
        } //CLOSE THE FILE because we are now DONE with it.

        if (from != null)
            for (var i = 0; i < from.Count; i++)
                Parallel.For(0, AllLines.Length, x => { AllLines[x] = AllLines[x].Replace(from[i], to[i]); });
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
        var l = SHGetLines.GetLines(
#if ASYNC
            await
#endif
                FileMs.ReadAllTextAsync(syncLocations)).ToList();
        l = l.Where(d => !d.StartsWith("#")).ToList();
        return l;
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
    /// <param name="file"></param>
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

    public static object ReadFileOrReturn(string l)
    {
        if (l.Length > 250) return l;
        if (FileMs.Exists(l)) return FileMs.ReadAllTextAsync(l);
        return l;
    }


    /// <summary>
    ///     ...
    /// </summary>
    /// <param name="file"></param>
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
                if (line.StartsWith(contains)) lines[i] = lines[i].Replace(contains, prefix + contains);
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


    public static
#if ASYNC
        async Task<bool>
#else
void
#endif
        PureFileOperation(string f, Func<string, string> transformHtmlToMetro4,
            string insertBetweenFilenameAndExtension)
    {
        var content =
#if ASYNC
            await
#endif
                FileMs.ReadAllTextAsync(f);
        var contentNew = transformHtmlToMetro4.Invoke(content);

        if (contentNew != content)
        {
#if ASYNC
            await
#endif
                WriteAllText(FS.InsertBetweenFileNameAndExtension(f, insertBetweenFilenameAndExtension), content);
            return true;
        }
        return false;
    }


    public static
#if ASYNC
        async Task<bool>
#else
void
#endif
        PureFileOperation(string f, Func<string, string> transformHtmlToMetro4)
    {
        var content = (
#if ASYNC
            await
#endif
                FileMs.ReadAllTextAsync(f)).Trim();
        var content2 = transformHtmlToMetro4.Invoke(content);

        if (string.Compare(content, content2) != 0)
        {
            //TF.SaveFile(content, CompareFilesPaths.GetFile(CompareExt.cs, 1));
            //TF.SaveFile(content2, CompareFilesPaths.GetFile(CompareExt.cs, 2));

#if ASYNC
            await
#endif
                FileMs.WriteAllTextAsync(f, content2);
            return true;
        }
        return false;
    }


    /// <summary>
    ///     StreamReader is derived from TextReader
    /// </summary>
    /// <param name="file"></param>
    public static StreamReader TextReader(string file)
    {
        return FileMs.OpenText(file);
    }

    public static async Task CreateEmptyFileWhenDoesntExists(string path)
    {
        //await CreateEmptyFileWhenDoesntExists<string, string>(path, null);
        await FileMs.WriteAllTextAsync(path, "");
    }

    //public static void WriteAllBytes(string p, IEnumerable<byte> b)
    //{
    //    FileMs.WriteAllBytes(p, b.ToArray());
    //}





    #region For easy copy

    public static List<byte> bomUtf8 = new List<byte>([239, 187, 191]);

    public static
#if ASYNC
        async Task
#else
void
#endif
        RemoveDoubleBomUtf8(string path)
    {
        var b = (
#if ASYNC
            await
#endif
                FileMs.ReadAllBytesAsync(path)).ToList();
        var to = b.Count > 5 ? 6 : b.Count;

        for (var i = 3; i < to; i++)
            if (bomUtf8[i - 3] != b[i])
                break;

        b = b.Skip(3).ToList();
        await FileMs.WriteAllBytesAsync(path, b.ToArray());
    }

    #endregion


}