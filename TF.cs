using SunamoStringReplace;

namespace SunamoFileIO;


public partial class TF : TFSE
{




    public static string ReadFileParallel(string fileName, IList<string> from, IList<string> to)
    {
        return ReadFileParallel(fileName, 1470, from, to);
    }

    public static
#if ASYNC
    async Task
#else
void
#endif
    ReplaceInFileOnLine(string path, int line, string what, string to, bool checkForMoreOccurences)
    {
        var c =
#if ASYNC
        await
#endif
        File.ReadAllTextAsync(path);
        var l = SHGetLines.GetLines(c);
        if (checkForMoreOccurences)
        {
            SHReplace.ReplaceInLine(l, line, what, to, checkForMoreOccurences);
        }
        else
        {
            if (c.Contains("import "))
            {
                SHReplace.ReplaceInLine(l, line, what, to, checkForMoreOccurences);
            }
        }


        var c2 = SHSE.JoinNL(l);
        if (c2 != c)
        {
            await File.WriteAllLinesAsync(path, l);
        }
    }

    public static string ReadFileParallel(string fileName, int linesCount, IList<string> from, IList<string> to)
    {
        string[] AllLines = new string[linesCount]; //only allocate memory here
        using (StreamReader sr = File.OpenText(fileName))
        {
            int x = 0;
            while (!sr.EndOfStream)
            {
                AllLines[x] = sr.ReadLine();
                x += 1;
            }
        } //CLOSE THE FILE because we are now DONE with it.

        if (from != null)
        {
            for (int i = 0; i < from.Count; i++)
            {
                Parallel.For(0, AllLines.Length, x =>
                {
                    AllLines[x] = AllLines[x].Replace(from[i], to[i]);
                });
            }
        }
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
        var l = (
#if ASYNC
        await
#endif
        File.ReadAllLinesAsync(syncLocations)).ToList();
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
    /// Dont working, with Air bank export return US-ascii / 1252, file has diacritic
    /// Atom with auto-encoding return ISO-8859-2 which is right
    /// </summary>
    /// <param name="file"></param>
    public static Encoding GetEncoding(FileStream file)
    {
        var bom = new byte[4];

        file.Read(bom, 0, 4);
        return EncodingHelper.DetectEncoding(new List<byte>(bom));
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
            string content =
#if ASYNC
            await
#endif
            File.ReadAllTextAsync(item);
            if (!content.Contains(append))
            {
                content = append + content;
                await File.WriteAllTextAsync(item, content);
            }
        }
    }

    public static object ReadFileOrReturn(string l)
    {
        if (l.Length > 250)
        {
            return l;
        }
        if (File.Exists(l))
        {
            return File.ReadAllTextAsync(l);
        }
        return l;
    }





    /// <summary>
    /// ...
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
        List<string> lines =
#if ASYNC
        await
#endif
        ReadAllLines(file);
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            if (lines[i].Trim() != "")
            {
                return i;
            }
        }
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
            var lines = (
#if ASYNC
            await
#endif
            File.ReadAllLinesAsync(item, null)).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i].Trim();
                if (line.StartsWith(contains))
                {
                    lines[i] = lines[i].Replace(contains, prefix + contains);
                }
            }

            await File.WriteAllLinesAsync(item, lines);
        }
    }

    //    /// <summary>
    //    /// Return all lines except empty
    //    /// GetLines return ALL lines, include empty
    //    ///
    //    /// Lze použít pouze pokud je A1 cesta ke serializovatelnému souboru, nikoliv samotný ser. řetězec
    //    /// Vrátí všechny řádky vytrimované z A1, ale nikoliv deserializované
    //    /// Every non empty line trim, every empty line don't return
    //    /// </summary>
    //    /// <param name="file"></param>
    //    public static
    //#if ASYNC
    //    async Task<List<string>>
    //#else
    //    List<string>
    //#endif
    // GetAllLines(string file)
    //    {
    //        return (
    //#if ASYNC
    //    await
    //#endif
    // File.ReadAllLinesAsync(file)).ToList();
    //    }

    public static
#if ASYNC
    async Task
#else
void
#endif
    AppendAllLines(string path, List<string> notRecognized, bool deduplicate = false)
    {
        var l =
#if ASYNC
        await
#endif
        ReadAllLines(path);
        l.AddRange(notRecognized);
        if (deduplicate)
        {
            l = CAG.RemoveDuplicitiesList<string>(l);
        }
        await File.WriteAllLinesAsync(path, notRecognized);
    }
}
