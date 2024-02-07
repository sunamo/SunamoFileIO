using SunamoExceptions.OnlyInSE;

namespace SunamoFileIO;
public partial class TF
{
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
