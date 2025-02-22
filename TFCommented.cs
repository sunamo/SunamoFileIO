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
///     Create folder hiearchy and write
/// </summary>
/// <param name="path"></param>
/// <param name="content"></param>
//public static async Task WriteAllText<StorageFolder, StorageFile>(StorageFile path, string content, AbstractCatalog<StorageFolder, StorageFile> ac)
//{
//    FS.CreateUpfoldersPsysicallyUnlessThereAc(path, ac);

//    await FileMs.WriteAllTextAsync(path, content, Encoding.UTF8, ac);
//}

/// <summary>
///     Precte soubor a vrati jeho obsah. Pokud soubor neexistuje, vytvori ho a vrati .
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
///     Just one command FileMs.Write* can be wrapped with it
/// </summary>
/// 
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

