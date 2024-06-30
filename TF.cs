



namespace SunamoFileIO;

public class TF : TFSE
{
    public static string ReadFileParallel(string fileName, IList<string> from, IList<string> to)
    {
        return ReadFileParallel(fileName, 1470, from, to);
    }

    //    public static
    //#if ASYNC
    //    async Task
    //#else
    //void
    //#endif
    //    ReplaceInFileOnLine(string path, int line, string what, string to, bool checkForMoreOccurences)
    //    {
    //        var c =
    //#if ASYNC
    //        await
    //#endif
    //        File.ReadAllTextAsync(path);
    //        var l = SHGetLines.GetLines(c);
    //        if (checkForMoreOccurences)
    //        {
    //            SHReplace.ReplaceInLine(l, line, what, to, checkForMoreOccurences);
    //        }
    //        else
    //        {
    //            if (c.Contains("import "))
    //            {
    //                SHReplace.ReplaceInLine(l, line, what, to, checkForMoreOccurences);
    //            }
    //        }


    //        var c2 = SHSunamoExceptions.JoinNL(l);
    //        if (c2 != c)
    //        {
    //            await File.WriteAllLinesAsync(path, l);
    //        }
    //    }

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
        var l = SHGetLines.GetLines(
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(syncLocations)).ToList();
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
        List<string> lines = SHGetLines.GetLines(
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(file)).ToList();
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
            var lines = SHGetLines.GetLines(
#if ASYNC
                await
#endif
                    File.ReadAllTextAsync(item, null)).ToList();
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
    // File.ReadAllTextAsync(file)).ToList();
    //    }

    public static
#if ASYNC
        async Task
#else
void
#endif
        AppendAllLines(string path, List<string> notRecognized, bool deduplicate = false)
    {
        var l = SHGetLines.GetLines(
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(path)).ToList();
        l.AddRange(notRecognized);
        if (deduplicate)
        {
            //l = CAG.RemoveDuplicitiesList<string>(l);
            l = l.Distinct().ToList();
        }
        await File.WriteAllLinesAsync(path, notRecognized);
    }

    public static
#if ASYNC
        async Task
#else
void
#endif
        Replace(string pathCsproj, string to, string from)
    {
        string content =
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(pathCsproj);
        content = content.Replace(to, from);

#if ASYNC
        await
#endif
            File.WriteAllTextAsync(pathCsproj, content);
    }

    //    public static
    //#if ASYNC
    //    async Task
    //#else
    //void
    //#endif
    //    PureFileOperationProcessEveryLine(string f, Func<string, string> transformHtmlToMetro4, string insertBetweenFilenameAndExtension)
    //    {
    //        var content =
    //#if ASYNC
    //        await
    //#endif
    //        File.ReadAllTextAsync(f);
    //        //content = transformHtmlToMetro4.Invoke(content);

    //#if ASYNC
    //        await
    //#endif
    //        TF.WriteAllText(FS.InsertBetweenFileNameAndExtension(f, insertBetweenFilenameAndExtension), content);
    //    }


    public static
#if ASYNC
        async Task
#else
void
#endif
        PureFileOperationWithArg(string f, Func<string, string, string> transformHtmlToMetro4, string arg)
    {
        var content =
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(f);

#if DEBUG
        if (f.Contains(@"\scz.sln"))
        {

        }
#endif

        var content2 = transformHtmlToMetro4.Invoke(content, arg);
        if (content.Trim() != content2.Trim())
        {
            await File.WriteAllTextAsync(f, content2);
        }
    }



    public static
#if ASYNC
        async Task
#else
void
#endif
        PureFileOperation(string f, Func<string, string> transformHtmlToMetro4, string insertBetweenFilenameAndExtension)
    {
        var content =
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(f);
        content = transformHtmlToMetro4.Invoke(content);

#if ASYNC
        await
#endif
            TF.WriteAllText(FS.InsertBetweenFileNameAndExtension(f, insertBetweenFilenameAndExtension), content);
    }





    public static
#if ASYNC
        async Task
#else
void
#endif
        PureFileOperation(string f, Func<string, string> transformHtmlToMetro4)
    {
        var content = (
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(f)).Trim();
        var content2 = transformHtmlToMetro4.Invoke(content);

        if (String.Compare(content, content2) != 0)
        {
            //TF.SaveFile(content, CompareFilesPaths.GetFile(CompareExt.cs, 1));
            //TF.SaveFile(content2, CompareFilesPaths.GetFile(CompareExt.cs, 2));

#if ASYNC
            await
#endif
                File.WriteAllTextAsync(f, content2);
        }
    }












    /// <summary>
    /// StreamReader is derived from TextReader
    /// </summary>
    /// <param name="file"></param>
    public static StreamReader TextReader(string file)
    {
        return File.OpenText(file);
    }

    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllText(string file, StringBuilder sb)
    {

#if ASYNC
        await
#endif
            WriteAllText(file, sb.ToString().ToUnixLineEnding());
    }

    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllText(string file, string content, Encoding encoding)
    {

#if ASYNC
        await
#endif
            //WriteAllText<string, string>(file, content, encoding, null);
            File.WriteAllTextAsync(file, content.ToUnixLineEnding(), encoding);
    }

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
                File.ReadAllBytesAsync(path)).ToList();
        var to = b.Count > 5 ? 6 : b.Count;

        for (int i = 3; i < to; i++)
        {
            if (bomUtf8[i - 3] != b[i])
            {
                break;
            }
        }

        b = b.Skip(3).ToList();
        await File.WriteAllBytesAsync(path, b.ToArray());
    }
    #endregion

    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllText(string file, string content, bool append)
    {
        if (append)
        {
            await TF.AppendAllText(file.ToString(), content.ToUnixLineEnding());
        }
        else
        {
            await TF.WriteAllText(file.ToString(), content.ToUnixLineEnding());
        }
    }

    public static async Task AppendAllText(string v, string content)
    {
        await File.WriteAllTextAsync(v, content.ToUnixLineEnding());
    }

    /// <summary>
    /// A1 cant be storagefile because its
    /// not in
    /// </summary>
    /// <param name="file"></param>
    /// <param name="content"></param>
    //    public static
    //#if ASYNC
    //    async Task
    //#else
    //void
    //#endif
    //    WriteAllText<StorageFolder, StorageFile>(StorageFile file, string content, Encoding enc, AbstractCatalog<StorageFolder, StorageFile> ac)
    //    {
    //        if (ac == null)
    //        {
    //            try
    //            {

    //#if ASYNC
    //                await
    //#endif
    //                File.WriteAllTextAsync(file.ToString(), content, enc);
    //            }
    //            catch (Exception)
    //            {
    //                if (throwExcIfCantBeWrite)
    //                {
    //                    throw;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            ac.tf.writeAllText.Invoke(file, content);
    //        }
    //    }

    //    public static
    //#if ASYNC
    //    async Task
    //#else
    //void
    //#endif
    //    WriteAllBytes<StorageFolder, StorageFile>(StorageFile file, List<byte> b, AbstractCatalog<StorageFolder, StorageFile> ac)
    //    {
    //        if (ac == null)
    //        {
    //            var fileS = file.ToString();

    //            if (LockedByBitLocker(fileS))
    //            {
    //                return;
    //            }

    //            await File.WriteAllBytesAsync(fileS, b.ToArray());

    //        }
    //        else
    //        {
    //            ac.tf.writeAllBytes(file, b);
    //        }

    //    }

    //    public static
    //#if ASYNC
    //        async Task
    //#else
    //        void
    //#endif
    //        SaveLines(IList<string> list, string file)
    //    {
    //        File.WriteAllLines(file, list);
    //    }

    /// <summary>
    /// Create folder hiearchy and write
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    //public static async Task WriteAllText<StorageFolder, StorageFile>(StorageFile path, string content, AbstractCatalog<StorageFolder, StorageFile> ac)
    //{
    //    FS.CreateUpfoldersPsysicallyUnlessThereAc(path, ac);

    //    await File.WriteAllTextAsync(path, content, Encoding.UTF8, ac);
    //}

#if DEBUG
    public const int waitMsBeforeReadFile = 1000;

    public static void WaitD()
    {
#if DEBUG
        if (waitMsBeforeReadFile != 0)
        {
            Thread.Sleep((int)waitMsBeforeReadFile);
        }
#endif
    }
#endif



    /// <summary>
    /// Precte soubor a vrati jeho obsah. Pokud soubor neexistuje, vytvori ho a vrati SE.
    /// </summary>
    /// <param name="s"></param>
    //    public static
    //#if ASYNC
    //    async Task<string>
    //#else
    //string
    //#endif
    //    ReadAllText<StorageFolder, StorageFile>(StorageFile s, AbstractCatalog<StorageFolder, StorageFile> ac = null)
    //    {
    //        if (readFile)
    //        {
    //            var ss = s.ToString();

    //#if DEBUG
    //            var f = AppData.ci.GetFile(AppFolders.Data, "ReadedFiles.txt");
    //            if (ss.EndsWith(".cs"))
    //            {
    //#if ASYNC
    //                await
    //#endif
    //                TF.AppendAllText(f, ss + Environment.NewLine);
    //            }
    //#endif

    //            if (!File.Exists(ss))
    //            {
    //                return string.Empty;
    //            }

    //            if (ac == null)
    //            {
    //                FS.MakeUncLongPath<StorageFolder, StorageFile>(ref s, ac);
    //            }

    //            if (isUsed != null)
    //            {
    //                if (isUsed.Invoke(ss))
    //                {
    //                    return string.Empty;
    //                }
    //            }

    //            if (ac == null)
    //            {
    //                var ss2 = s.ToString();

    //                // Způsobovalo mi chybu v asp.net Could not find file 'D:\Documents\sunamo\Common\Settings\CloudProviders'.


    //                CloudProvidersHelper.Init();
    //                CloudProvidersHelper.OpenSyncAppIfNotRunning(ss2);


    //                if (LockedByBitLocker(ss2))
    //                {
    //                    return String.Empty;
    //                }

    //                //ThisApp.firstReadingFromCloudProvider =

    //                //result = enc.GetString(bytesArray);
    //#if ASYNC
    //                //await WaitD();
    //#endif


    //                return File.ReadAllText(ss2);
    //            }
    //            else
    //            {
    //                return ac.tf.readAllText(s);
    //            }
    //        }
    //        return string.Empty;

    //    }
    static Type type = typeof(TF);
    //public static Func<string, bool> isUsed
    //{
    //    get => se.TF.isUsed;
    //    set => se.TF.isUsed = value;
    //}

    //    public static
    //#if ASYNC
    //    async Task<List<string>>
    //#else
    //    List<string>
    //#endif
    // GetLines(string item)
    //    {
    //        return
    //#if ASYNC
    //    await
    //#endif
    // GetLines<string, string>(item, null);
    //    }

    //    public static
    //#if ASYNC
    //    async Task<List<string>>
    //#else
    //    List<string>
    //#endif
    // GetLines<StorageFolder, StorageFile>(StorageFile item, AbstractCatalog<StorageFolder, StorageFile> ac)
    //    {
    //        return
    //#if ASYNC
    //    await
    //#endif
    // ReadAllLines<StorageFolder, StorageFile>(item, ac);
    //    }







    //    public static
    //#if ASYNC
    //    async Task
    //#else
    //    void
    //#endif
    // SaveLinesIList(IList belowZero, string f)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        foreach (var item in belowZero)
    //        {
    //            sb.AppendLine(item.ToString());
    //        }

    //#if ASYNC
    //        await
    //#endif
    //        TF.WriteAllText(f, sb.ToString());
    //    }




    //    public static
    //#if ASYNC
    //    async Task<List<string>>
    //#else
    //List<string>
    //#endif
    //    ReadAllLines<StorageFolder, StorageFile>(StorageFile file, AbstractCatalog<StorageFolder, StorageFile> ac)
    //    {
    //        return SHGetLines.GetLines(
    //#if ASYNC
    //        await
    //#endif
    //        ReadAllText<StorageFolder, StorageFile>(file, ac));
    //    }

    /// <summary>
    /// Just one command File.Write* can be wrapped with it
    /// </summary>
    public static bool throwExcIfCantBeWrite = false;



    private static async Task SaveFile_DontUse(string obsah, string soubor, bool pripsat)
    {
        var dir = Path.GetDirectoryName(soubor);

        ThrowEx.DirectoryWasntFound(dir);

        if (soubor == null)
        {
            return;
        }
        if (pripsat)
        {
            File.AppendAllText(soubor, obsah);
        }
        else
        {
            await File.WriteAllTextAsync(soubor, obsah, Encoding.UTF8);
        }
    }


    public static void AppendAllText_DontUse(string obsah, string soubor)
    {
        //SaveFile(obsah, soubor, true);
    }

    /// <summary>
    /// Return string.Empty when file won't exists
    /// Use FileUtil.WhoIsLocking to avoid error The process cannot access the file because it is being used by another process
    /// </summary>
    /// <param name="s"></param>
    public static
#if ASYNC
        async Task<string>
#else
string
#endif
        ReadAllText(string s)
    {
        //        return
        //#if ASYNC
        //        await
        //#endif
        //        ReadAllText<string, string>(s);

        return (await File.ReadAllTextAsync(s));
    }

    public static bool readFile = true;

    public static async Task CreateEmptyFileWhenDoesntExists(string path)
    {
        //await CreateEmptyFileWhenDoesntExists<string, string>(path, null);
        await File.WriteAllTextAsync(path, "");
    }

    //public static async Task CreateEmptyFileWhenDoesntExists<StorageFolder, StorageFile>(StorageFile path, AbstractCatalog<StorageFolder, StorageFile> ac)
    //{
    //    if (!FS.ExistsFileAc(path, ac))
    //    {
    //        FS.CreateUpfoldersPsysicallyUnlessThereAc<StorageFolder, StorageFile>(path, ac);
    //        await File.WriteAllTextAsync<StorageFolder, StorageFile>(path, "", Encoding.UTF8, ac);
    //    }
    //}
}
