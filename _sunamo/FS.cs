namespace SunamoFileIO._sunamo;

internal class FS
{
    internal static Func<string, bool> ExistsFile;
    internal static Func<string, long> GetFileSize;
    internal static Func<string, string> GetDirectoryName;
    internal static Action<string> CreateUpfoldersPsysicallyUnlessThere;
    internal static Action<string> CreateFoldersPsysicallyUnlessThere;
    internal static Func<string, string> GetFileNameWithoutExtension;
    internal static Func<string, string, string> InsertBetweenFileNameAndExtension;
    internal static Func<string, string, bool, List<string>> GetFiles;

    internal static bool ExistsFileAc<StorageFolder, StorageFile>(StorageFile selectedFile, AbstractCatalog<StorageFolder, StorageFile> ac = null)
    {
        if (ac == null)
        {
            return ExistsFile(selectedFile.ToString());
        }
        return ac.fs.existsFile.Invoke(selectedFile);
    }

    #region MakeUncLongPath
    internal static void MakeUncLongPath<StorageFolder, StorageFile>(ref StorageFile path, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            path = (StorageFile)(dynamic)MakeUncLongPath(path.ToString());
        }
        else
        {
            ThrowNotImplementedUwp();
        }
        //return path;
    }

    /// <summary>
    ///     Usage: ExistsDirectoryWorker
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    internal static string MakeUncLongPath(string path)
    {
        return MakeUncLongPath(ref path);
    }

    internal static string MakeUncLongPath(ref string path)
    {
        if (!path.StartsWith(Consts.UncLongPath))
        {
            // V ASP.net mi vrátilo u každé directory.exists false. Byl jsem pod ApplicationPoolIdentity v IIS a bylo nastaveno Full Control pro IIS AppPool\DefaultAppPool
#if !ASPNET
            //  asp.net / vps nefunguje, ve windows store apps taktéž, NECHAT TO TRVALE ZAKOMENTOVANÉ
            // v asp.net toto způsobí akorát zacyklení, IIS začne vyhazovat 0xc00000fd, pak už nejde načíst jediná stránka
            //path = Consts.UncLongPath + path;
#endif
        }

        return path;
    }
    #endregion

    internal static StorageFolder GetDirectoryNameAc<StorageFolder, StorageFile>(StorageFile rp2, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac != null)
        {
            return ac.fs.getDirectoryName.Invoke(rp2);
        }

        var rp = rp2.ToString();
        return (dynamic)GetDirectoryName(rp);
    }

    internal static void CreateUpfoldersPsysicallyUnlessThereAc<StorageFolder, StorageFile>(StorageFile nad, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            CreateUpfoldersPsysicallyUnlessThere(nad.ToString());
        }
        else
        {
            CreateFoldersPsysicallyUnlessThereFolderAc<StorageFolder, StorageFile>(FS.GetDirectoryNameAc<StorageFolder, StorageFile>(nad, ac), ac);
        }
    }

    internal static void CreateFoldersPsysicallyUnlessThereFolderAc<StorageFolder, StorageFile>(StorageFolder nad, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            CreateFoldersPsysicallyUnlessThere(nad.ToString());
        }
        else
        {
            ThrowNotImplementedUwp();
        }
    }

    internal static void ThrowNotImplementedUwp()
    {
        ThrowEx.Custom("Not implemented in UWP");
    }
}
