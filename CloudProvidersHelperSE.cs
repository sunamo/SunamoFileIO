namespace SunamoFileIO;
// Nevím proč tu NS je, odstraním ho
//namespace sunamo.Helpers.FileSystem
//{

public class CloudProvidersHelperSE
{
    //private Dictionary<string, string> folders = null;
    public static string OneDriveFolder0;
    public static string OneDriveFolder1;

    /// <summary>
    /// Mám to tady zakomentované, proto to musím nejdříve opravit, zkontrolovat v UT a vrátit zpět
    /// Dočasně tu cestu tu budu mít hard coded
    /// 
    /// tak já tomu nerozumím
    /// je to prokazatelně uložené, na základě uložených souborů jsem vytvořil pakcage
    /// unit testy procházejí
    /// a přesto 
    /// </summary>
    private static string gDriveFolder = @"D:\Drive\";
    public static string OneDriveExe;
    public static string GDriveExe;

    public static bool OneDriveExeExists = false;
    public static bool GDriveExeExists;

    public static bool OneDriveExeOpened;
    public static bool GDriveExeOpened;

    public static string OneDriveFn;
    public static string GDriveFn;

    public static /*IMyStations*/ dynamic myStations = null;

    private static CloudProvidersHelperSE Instance;
    private static bool isUseCloud;

    public CloudProvidersHelperSE()
    {

    }

    public async Task Init(string fCloudProviders)
    {
        // Return always, also for my PC
        // nemůžu, získávám takto cesty buď D:\Drive nebo D:\Sync, furt jsem ty adresáře měnil. To jaký je aktuálně adresář získávám ze CloudProviders
        // Krom toho je i výhoda když budu mít aktuální soubory na cloudu pro případ ztráty dat
        //return;

        if (Instance != null)
        {
            return;
        }

        Instance = this;


        //       string fCloudProviders =
        //AppData.ci.GetFileCommonSettings("CloudProviders.txt");

        //var (header, l) = SF.GetAllElementsFileAdvanced(fCloudProviders, null);


        //if (header.Count != 0)
        //{
        //    isUseCloud = true;
        //    //folders = SF.ToDictionary<string, string>(l);
        //    List<string> OneDriveFolders = SHSunamoExceptions.Split(header[0], AllStrings.ast);
        //    OneDriveFolder0 = OneDriveFolders[0];
        //    OneDriveFolder1 = OneDriveFolders[1];
        //    gDriveFolder = l[0][0];

        //    string oneDriveExe = header[1];

        //    if (myStations != null)
        //    {
        //        oneDriveExe = oneDriveExe.Replace(SHSH.WrapWithBs(myStations.Vps), SHSH.WrapWithBs(myStations.Mb));
        //    }

        //    //if (!VpsHelperXlf.IsVps)
        //    //{
        //    //    OneDriveExe = oneDriveExe;
        //    //    GDriveExeExists =File.Exists(oneDriveExe);
        //    //}

        //    GDriveExe = l[0][1];
        //    GDriveExeExists = File.Exists(GDriveExe);

        //    OneDriveFn = Path.GetFileNameWithoutExtension(OneDriveExe);
        //    GDriveFn = Path.GetFileNameWithoutExtension(GDriveExe);
        //}
    }

    public static string GDriveFolder
    {
        get
        {
            if (gDriveFolder == null)
            {
                new CloudProvidersHelperSE();
            }

            return gDriveFolder;
        }
    }

    //public static void OpenSyncAppIfNotRunning(string ss2)
    //{
    //    if (OneDriveExeOpened && GDriveExeOpened)
    //    {
    //        return;
    //    }

    //    if (!isUseCloud)
    //    {
    //        return;
    //    }

    //    if (OneDriveExe == null)
    //    {
    //        OneDriveExeOpened = true;
    //        return;
    //    }

    //    if (OneDriveExeExists)
    //    {
    //        if (ss2.StartsWith(OneDriveFolder0) || ss2.StartsWith(OneDriveFolder1))
    //        {
    //            if (!PH.IsAlreadyRunning(OneDriveFn))
    //            {
    //                OneDriveExeOpened = true;
    //                AIStore.winPi?.PHWinPiRunAsDesktopUserNoAdmin(OneDriveExe);
    //                Thread.Sleep(5000);
    //            }
    //        }
    //    }
    //    else if (GDriveExeExists)
    //    {
    //        if (ss2.StartsWith(GDriveFolder))
    //        {
    //            if (!PH.IsAlreadyRunning(GDriveFn))
    //            {
    //                GDriveExeOpened = true;
    //                Process.Start(GDriveExe);
    //                Thread.Sleep(5000);
    //            }
    //        }
    //    }
    //}
}
//}
