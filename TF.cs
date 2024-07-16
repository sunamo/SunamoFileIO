using FileMs = System.IO.File;

namespace SunamoFileIO;

public class TF
{

    public static string ReadAllTextSync(string path)
    {
        return ReadAllTextSync(path, false);
    }
    public static string ReadAllTextSync(string path, bool createEmptyIfWasNotExists = false)
    {
        if (createEmptyIfWasNotExists)
            if (!File.Exists(path))
            {
                WriteAllTextSync(path, string.Empty);
                return string.Empty;
            }
        return File.ReadAllText(path);
    }
    public static void WriteAllTextSync(string path, string content)
    {
        File.WriteAllText(path, content);
    }
    public static void AppendAllTextSync(string path, string content)
    {
        File.AppendAllText(path, content);
    }
    public static List<string> ReadAllLinesSync(string path)
    {
        return ReadAllLinesSync(path, false);
    }
    public static List<string> ReadAllLinesSync(string path, bool createEmptyIfWasNotExists = false)
    {
        if (createEmptyIfWasNotExists)
            if (!File.Exists(path))
            {
                WriteAllTextSync(path, string.Empty);
                return new List<string>();
            }
        return SHGetLines.GetLines(File.ReadAllText(path));
    }
    public static void WriteAllLinesSync(string path, List<string> content)
    {
        File.WriteAllLines(path, content.ToArray());
    }
    public static void AppendAllLinesSync(string path, List<string> content)
    {
        File.AppendAllLines(path, content.ToArray());
    }
    public static List<byte> ReadAllBytesSync(string path)
    {
        return File.ReadAllBytes(path).ToList();
    }
    public static void WriteAllBytesSync(string path, List<byte> content)
    {
        File.WriteAllBytes(path, content.ToArray());
    }
    public static Func<string, bool> isUsed = null;
    #region
    protected static bool LockedByBitLocker(string path)
    {
        return ThrowEx.LockedByBitLocker(path);
    }
    public static
#if ASYNC
        async Task<string>
#else
string
#endif
        ReadAllText(string path, Encoding enc)
    {
        if (isUsed != null)
            if (isUsed.Invoke(path))
                return string.Empty;
#if ASYNC

#endif

#if ASYNC
        return await File.ReadAllTextAsync(path, enc);
#else
return File.ReadAllText(path, enc);
#endif
    }
    #region Array
    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllLinesArray(string path, string[] c)
    {
#if ASYNC
        await
#endif
            WriteAllLines(path, c.ToList());
    }
    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllBytesArray(string path, byte[] c)
    {
#if ASYNC
        await
#endif
            WriteAllBytes(path, c.ToList());
    }
    public static
#if ASYNC
        async Task<byte[]>
#else
byte[]
#endif
        ReadAllBytesArray(string path)
    {
        return (
#if ASYNC
            await
#endif
                ReadAllBytes(path)).ToArray();
    }
    #endregion
    #region Bytes





    public static
#if ASYNC
        async Task<List<byte>>
#else
List<byte>
#endif
        ReadAllBytes(string file)
    {
        if (LockedByBitLocker(file)) return new List<byte>();
#if ASYNC

#endif
        return
#if ASYNC
            (await File.ReadAllBytesAsync(file)).ToList();
#else
File.ReadAllBytes(file).ToList();
#endif
    }
    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllBytes(string file, List<byte> b)
    {
        if (LockedByBitLocker(file)) return;
#if ASYNC
        await File.WriteAllBytesAsync(file, b.ToArray());
#else
File.WriteAllBytes(file, b.ToArray());
#endif
    }
    #endregion
    #region Lines
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
    #region Text
    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllText(string path, string content)
    {
        if (LockedByBitLocker(path)) return;
#if ASYNC
        await File.WriteAllTextAsync(path, content);
#else
File.WriteAllText(path, content);
#endif
    }
    public static
#if ASYNC
        async Task<string>
#else
string
#endif
        ReadAllText(string f)
    {
        if (LockedByBitLocker(f)) return string.Empty;
#if ASYNC

#endif
        try
        {
#if ASYNC
            return await File.ReadAllTextAsync(f);
#else
return File.ReadAllText(f);
#endif
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
    public static
#if ASYNC
        async Task
#else
void
#endif
        AppendAllText(string path, string content)
    {
        if (LockedByBitLocker(path)) return;
#if ASYNC

#endif
        try
        {
#if ASYNC
            await File.AppendAllTextAsync(path, content);
#else
File.AppendAllText(path, content);
#endif
        }
        catch (Exception)
        {
        }
    }
    #endregion
    #endregion
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
    //        FileMs.ReadAllTextAsync(path);
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


    //        var c2 = SH.JoinNL(l);
    //        if (c2 != c)
    //        {
    //            await FileMs.WriteAllLinesAsync(path, l);
    //        }
    //    }

    public static string ReadFileParallel(string fileName, int linesCount, IList<string> from, IList<string> to)
    {
        string[] AllLines = new string[linesCount]; //only allocate memory here
        using (StreamReader sr = FileMs.OpenText(fileName))
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
            string content =
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
        if (l.Length > 250)
        {
            return l;
        }
        if (FileMs.Exists(l))
        {
            return FileMs.ReadAllTextAsync(l);
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
                FileMs.ReadAllTextAsync(file)).ToList();
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
                    FileMs.ReadAllTextAsync(item, null)).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i].Trim();
                if (line.StartsWith(contains))
                {
                    lines[i] = lines[i].Replace(contains, prefix + contains);
                }
            }

            await FileMs.WriteAllLinesAsync(item, lines);
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
    // FileMs.ReadAllTextAsync(file)).ToList();
    //    }

    public static
#if ASYNC
        async Task
#else
void
#endif
        AppendAllLines(string path, IEnumerable<string> notRecognized, bool deduplicate = false)
    {
        var l = SHGetLines.GetLines(
#if ASYNC
            await
#endif
                FileMs.ReadAllTextAsync(path)).ToList();
        l.AddRange(notRecognized);
        if (deduplicate)
        {
            //l = CAG.RemoveDuplicitiesList<string>(l);
            l = l.Distinct().ToList();
        }
        await FileMs.WriteAllLinesAsync(path, l);
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
                FileMs.ReadAllTextAsync(pathCsproj);
        content = content.Replace(to, from);

#if ASYNC
        await
#endif
            FileMs.WriteAllTextAsync(pathCsproj, content);
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
    //        FileMs.ReadAllTextAsync(f);
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
                FileMs.ReadAllTextAsync(f);

#if DEBUG
        if (f.Contains(@"\scz.sln"))
        {

        }
#endif

        var content2 = transformHtmlToMetro4.Invoke(content, arg);
        if (content.Trim() != content2.Trim())
        {
            await FileMs.WriteAllTextAsync(f, content2);
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
                FileMs.ReadAllTextAsync(f);
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
                FileMs.ReadAllTextAsync(f)).Trim();
        var content2 = transformHtmlToMetro4.Invoke(content);

        if (String.Compare(content, content2) != 0)
        {
            //TF.SaveFile(content, CompareFilesPaths.GetFile(CompareExt.cs, 1));
            //TF.SaveFile(content2, CompareFilesPaths.GetFile(CompareExt.cs, 2));

#if ASYNC
            await
#endif
                FileMs.WriteAllTextAsync(f, content2);
        }
    }












    /// <summary>
    /// StreamReader is derived from TextReader
    /// </summary>
    /// <param name="file"></param>
    public static StreamReader TextReader(string file)
    {
        return FileMs.OpenText(file);
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
            FileMs.WriteAllTextAsync(file, content.ToUnixLineEnding(), encoding);
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
                FileMs.ReadAllBytesAsync(path)).ToList();
        var to = b.Count > 5 ? 6 : b.Count;

        for (int i = 3; i < to; i++)
        {
            if (bomUtf8[i - 3] != b[i])
            {
                break;
            }
        }

        b = b.Skip(3).ToList();
        await FileMs.WriteAllBytesAsync(path, b.ToArray());
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
    //                FileMs.WriteAllText(FileMs.ToString(), content, enc);
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
    //            var fileS = FileMs.ToString();

    //            if (LockedByBitLocker(fileS))
    //            {
    //                return;
    //            }

    //            await FileMs.WriteAllBytesAsync(fileS, b.ToArray());

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
    //        FileMs.WriteAllLines(file, list);
    //    }

    /// <summary>
    /// Create folder hiearchy and write
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    //public static async Task WriteAllText<StorageFolder, StorageFile>(StorageFile path, string content, AbstractCatalog<StorageFolder, StorageFile> ac)
    //{
    //    FS.CreateUpfoldersPsysicallyUnlessThereAc(path, ac);

    //    await FileMs.WriteAllTextAsync(path, content, Encoding.UTF8, ac);
    //}

#if DEBUG
    public const int waitMsBeforeReadFile = 1000;
#endif



    /// <summary>
    /// Precte soubor a vrati jeho obsah. Pokud soubor neexistuje, vytvori ho a vrati .
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

    //            if (!FileMs.Exists(ss))
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


    //                return FileMs.ReadAllText(ss2);
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
    /// Just one command FileMs.Write* can be wrapped with it
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
            FileMs.AppendAllText(soubor, obsah);
        }
        else
        {
            await FileMs.WriteAllTextAsync(soubor, obsah, Encoding.UTF8);
        }
    }


    public static void AppendAllText_DontUse(string obsah, string soubor)
    {
        //SaveFile(obsah, soubor, true);
    }



    public static bool readFile = true;

    public static async Task CreateEmptyFileWhenDoesntExists(string path)
    {
        //await CreateEmptyFileWhenDoesntExists<string, string>(path, null);
        await FileMs.WriteAllTextAsync(path, "");

    }

    //public static void WriteAllBytes(string p, IEnumerable<byte> b)
    //{
    //    FileMs.WriteAllBytes(p, b.ToArray());
    //}

    public static async Task WriteAllBytes(string p, IEnumerable<byte> b)
    {
        await FileMs.WriteAllBytesAsync(p, b.ToArray());
    }

    //public static List<byte> ReadAllBytes(string p)
    //{
    //    return FileMs.ReadAllBytes(p).ToList();
    //}


    //public static async Task CreateEmptyFileWhenDoesntExists<StorageFolder, StorageFile>(StorageFile path, AbstractCatalog<StorageFolder, StorageFile> ac)
    //{
    //    if (!FS.ExistsFileAc(path, ac))
    //    {
    //        FS.CreateUpfoldersPsysicallyUnlessThereAc<StorageFolder, StorageFile>(path, ac);
    //        await FileMs.WriteAllText<StorageFolder, StorageFile>(path, "", Encoding.UTF8, ac);
    //    }
    //}

    //public static async Task<string> ReadAllText(string p)
    //{
    //    return await FileMs.ReadAllTextAsync(p);
    //}

}
