namespace SunamoFileIO._sunamo.SunamoFileSystem;

internal class FS
{
    internal static string InsertBetweenFileNameAndExtension(string orig, string whatInsert)
    {
        //return InsertBetweenFileNameAndExtension<string, string>(orig, whatInsert, null);

        // Cesta by se zde hodila kvůli FS.CiStorageFile
        // nicméně StorageFolder nevím zda se používá, takže to bude umět i bez toho

        var origS = orig.ToString();

        string fn = Path.GetFileNameWithoutExtension(origS);
        string e = Path.GetExtension(origS);

        if (origS.Contains('/') || origS.Contains('\\'))
        {
            string p = Path.GetDirectoryName(origS);

            return Path.Combine(p, fn + whatInsert + e);
        }
        return fn + whatInsert + e;
    }



    //    internal static Func<string, bool> ExistsFile;
    //    internal static Func<string, long> GetFileSize;
    //    internal static Func<string, string> GetDirectoryName;
    //    internal static Action<string> CreateUpfoldersPsysicallyUnlessThere;
    //    internal static Action<string> CreateFoldersPsysicallyUnlessThere;
    //    internal static Func<string, string> GetFileNameWithoutExtension;
    //    internal static Func<string, string, string> InsertBetweenFileNameAndExtension;
    //    internal static Func<string, string, bool?, List<string>> GetFilesWithoutArgs;

    //    internal static bool ExistsFileAc<StorageFolder, StorageFile>(StorageFile selectedFile, AbstractCatalog<StorageFolder, StorageFile> ac = null)
    //    {
    //        if (ac == null)
    //        {
    //            return ExistsFile(selectedFile.ToString());
    //        }
    //        return ac.fs.existsFile.Invoke(selectedFile);
    //    }

    //    #region MakeUncLongPath
    //    internal static void MakeUncLongPath<StorageFolder, StorageFile>(ref StorageFile path, AbstractCatalog<StorageFolder, StorageFile> ac)
    //    {
    //        if (ac == null)
    //        {
    //            path = (StorageFile)(dynamic)MakeUncLongPath(path.ToString());
    //        }
    //        else
    //        {
    //            ThrowNotImplementedUwp();
    //        }
    //        //return path;
    //    }

    //    /// <summary>
    //    ///     Usage: ExistsDirectoryWorker
    //    /// </summary>
    //    /// <param name="path"></param>
    //    /// <returns></returns>
    //    internal static string MakeUncLongPath(string path)
    //    {
    //        return MakeUncLongPath(ref path);
    //    }

    //    internal static string MakeUncLongPath(ref string path)
    //    {
    //        if (!path.StartsWith(@"\\?\"))
    //        {
    //            // V ASP.net mi vrátilo u každé directory.exists false. Byl jsem pod ApplicationPoolIdentity v IIS a bylo nastaveno Full Control pro IIS AppPool\DefaultAppPool
    //#if !ASPNET
    //            //  asp.net / vps nefunguje, ve windows store apps taktéž, NECHAT TO TRVALE ZAKOMENTOVANÉ
    //            // v asp.net toto způsobí akorát zacyklení, IIS začne vyhazovat 0xc00000fd, pak už nejde načíst jediná stránka
    //            //path = @"\\?\" + path;
    //#endif
    //        }

    //        return path;
    //    }
    //    #endregion

    //    internal static StorageFolder GetDirectoryNameAc<StorageFolder, StorageFile>(StorageFile rp2, AbstractCatalog<StorageFolder, StorageFile> ac)
    //    {
    //        if (ac != null)
    //        {
    //            return ac.Path.GetDirectoryName.Invoke(rp2);
    //        }

    //        var rp = rp2.ToString();
    //        return (dynamic)GetDirectoryName(rp);
    //    }

    //    internal static void CreateUpfoldersPsysicallyUnlessThereAc<StorageFolder, StorageFile>(StorageFile nad, AbstractCatalog<StorageFolder, StorageFile> ac)
    //    {
    //        if (ac == null)
    //        {
    //            CreateUpfoldersPsysicallyUnlessThere(nad.ToString());
    //        }
    //        else
    //        {
    //            CreateFoldersPsysicallyUnlessThereFolderAc<StorageFolder, StorageFile>(Path.GetDirectoryNameAc<StorageFolder, StorageFile>(nad, ac), ac);
    //        }
    //    }

    //    internal static void CreateFoldersPsysicallyUnlessThereFolderAc<StorageFolder, StorageFile>(StorageFolder nad, AbstractCatalog<StorageFolder, StorageFile> ac)
    //    {
    //        if (ac == null)
    //        {
    //            CreateFoldersPsysicallyUnlessThere(nad.ToString());
    //        }
    //        else
    //        {
    //            ThrowNotImplementedUwp();
    //        }
    //    }

    //    internal static void ThrowNotImplementedUwp()
    //    {
    //        throw new Exception("Not implemented in UWP");
    //    }
}