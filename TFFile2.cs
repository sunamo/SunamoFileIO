namespace SunamoFileIO;



/// <summary>
/// Methods which is calling with TFFIle.cs
/// </summary>
public partial class TF
{
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
            TF.AppendAllText(file.ToString(), content);
        }
        else
        {
            TF.WriteAllText(file.ToString(), content);
        }
    }

    /// <summary>
    /// A1 cant be storagefile because its
    /// not in
    /// </summary>
    /// <param name="file"></param>
    /// <param name="content"></param>
    public static
#if ASYNC
    async Task
#else
void
#endif
    WriteAllText<StorageFolder, StorageFile>(StorageFile file, string content, Encoding enc, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            try
            {

#if ASYNC
                await
#endif
                File.WriteAllTextAsync(file.ToString(), content, enc);
            }
            catch (Exception)
            {
                if (throwExcIfCantBeWrite)
                {
                    throw;
                }
            }
        }
        else
        {
            ac.tf.writeAllText.Invoke(file, content);
        }
    }

    public static
#if ASYNC
    async Task
#else
void
#endif
    WriteAllBytes<StorageFolder, StorageFile>(StorageFile file, List<byte> b, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            var fileS = file.ToString();

            if (LockedByBitLocker(fileS))
            {
                return;
            }

            File.WriteAllBytesAsync(fileS, b.ToArray());

        }
        else
        {
            ac.tf.writeAllBytes(file, b);
        }

    }

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
    public static void WriteAllText<StorageFolder, StorageFile>(StorageFile path, string content, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        FS.CreateUpfoldersPsysicallyUnlessThereAc(path, ac);

        TF.WriteAllText<StorageFolder, StorageFile>(path, content, Encoding.UTF8, ac);
    }

#if DEBUG
    public const int waitMsBeforeReadFile = 1000;

    public static void WaitD()
    {
#if DEBUG
        if (waitMsBeforeReadFile != 0)
        {
            Thread.Sleep(waitMsBeforeReadFile);
        }
#endif
    }
#endif



    /// <summary>
    /// Precte soubor a vrati jeho obsah. Pokud soubor neexistuje, vytvori ho a vrati SE.
    /// </summary>
    /// <param name="s"></param>
    public static
#if ASYNC
    async Task<string>
#else
string
#endif
    ReadAllText<StorageFolder, StorageFile>(StorageFile s, AbstractCatalog<StorageFolder, StorageFile> ac = null)
    {
        if (readFile)
        {
            var ss = s.ToString();

#if DEBUG
            var f = AppData.ci.GetFile(AppFolders.Data, "ReadedFiles.txt");
            if (ss.EndsWith(".cs"))
            {
#if ASYNC
                await
#endif
                TF.AppendAllText(f, ss + Environment.NewLine);
            }
#endif

            if (!File.Exists(ss))
            {
                return string.Empty;
            }

            if (ac == null)
            {
                FS.MakeUncLongPath<StorageFolder, StorageFile>(ref s, ac);
            }

            if (isUsed != null)
            {
                if (isUsed.Invoke(ss))
                {
                    return string.Empty;
                }
            }

            if (ac == null)
            {
                var ss2 = s.ToString();

                // Způsobovalo mi chybu v asp.net Could not find file 'D:\Documents\sunamo\Common\Settings\CloudProviders'.


                CloudProvidersHelper.Init();
                CloudProvidersHelper.OpenSyncAppIfNotRunning(ss2);


                if (LockedByBitLocker(ss2))
                {
                    return String.Empty;
                }

                //ThisApp.firstReadingFromCloudProvider =

                //result = enc.GetString(bytesArray);
#if ASYNC
                //await WaitD();
#endif


                return File.ReadAllText(ss2);
            }
            else
            {
                return ac.tf.readAllText(s);
            }
        }
        return string.Empty;

    }


}
