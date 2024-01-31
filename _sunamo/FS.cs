//namespace SunamoFileIO._sunamo;

//public class FS
//{
//    public static Func<string, bool> ExistsFile;
//    public static Func<string, long> GetFileSize;
//    public static Func<string, string> GetDirectoryName;
//    public static Action<string> CreateUpfoldersPsysicallyUnlessThere;
//    public static Action<string> CreateFoldersPsysicallyUnlessThere;
//    public static Func<string, string> GetFileNameWithoutExtension;
//    public static Func<string, string, string> InsertBetweenFileNameAndExtension;
//    public static Func<string, string, bool?, List<string>> GetFilesWithoutArgs;

//    public static bool ExistsFileAc<StorageFolder, StorageFile>(StorageFile selectedFile, AbstractCatalog<StorageFolder, StorageFile> ac = null)
//    {
//        if (ac == null)
//        {
//            return ExistsFile(selectedFile.ToString());
//        }
//        return ac.fs.existsFile.Invoke(selectedFile);
//    }

//    #region MakeUncLongPath
//    public static void MakeUncLongPath<StorageFolder, StorageFile>(ref StorageFile path, AbstractCatalog<StorageFolder, StorageFile> ac)
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
//    public static string MakeUncLongPath(string path)
//    {
//        return MakeUncLongPath(ref path);
//    }

//    public static string MakeUncLongPath(ref string path)
//    {
//        if (!path.StartsWith(Consts.UncLongPath))
//        {
//            // V ASP.net mi vrátilo u každé directory.exists false. Byl jsem pod ApplicationPoolIdentity v IIS a bylo nastaveno Full Control pro IIS AppPool\DefaultAppPool
//#if !ASPNET
//            //  asp.net / vps nefunguje, ve windows store apps taktéž, NECHAT TO TRVALE ZAKOMENTOVANÉ
//            // v asp.net toto způsobí akorát zacyklení, IIS začne vyhazovat 0xc00000fd, pak už nejde načíst jediná stránka
//            //path = Consts.UncLongPath + path;
//#endif
//        }

//        return path;
//    }
//    #endregion

//    public static StorageFolder GetDirectoryNameAc<StorageFolder, StorageFile>(StorageFile rp2, AbstractCatalog<StorageFolder, StorageFile> ac)
//    {
//        if (ac != null)
//        {
//            return ac.fs.getDirectoryName.Invoke(rp2);
//        }

//        var rp = rp2.ToString();
//        return (dynamic)GetDirectoryName(rp);
//    }

//    public static void CreateUpfoldersPsysicallyUnlessThereAc<StorageFolder, StorageFile>(StorageFile nad, AbstractCatalog<StorageFolder, StorageFile> ac)
//    {
//        if (ac == null)
//        {
//            CreateUpfoldersPsysicallyUnlessThere(nad.ToString());
//        }
//        else
//        {
//            CreateFoldersPsysicallyUnlessThereFolderAc<StorageFolder, StorageFile>(FS.GetDirectoryNameAc<StorageFolder, StorageFile>(nad, ac), ac);
//        }
//    }

//    public static void CreateFoldersPsysicallyUnlessThereFolderAc<StorageFolder, StorageFile>(StorageFolder nad, AbstractCatalog<StorageFolder, StorageFile> ac)
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

//    public static void ThrowNotImplementedUwp()
//    {
//        ThrowEx.Custom("Not implemented in UWP");
//    }
//}
