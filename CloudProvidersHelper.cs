namespace SunamoFileIO;

public class CloudProvidersHelper
{
    //private Dictionary<string, string> folders = null;
    public static string OneDriveFolder0 = null;
    public static string OneDriveFolder1 = null;
    public static string GDriveFolder = null;
    public static string OneDriveExe = null;
    public static string GDriveExe = null;

    public static string OneDriveFn = null;
    public static string GDriveFn = null;

    public static /*IMyStations*/ dynamic myStations = null;

    //private static CloudProvidersHelper Instance = null;
    static bool isUseCloud = false;

    private CloudProvidersHelper()
    {

    }

    public static void Init()
    {
        if (GDriveFolder != null)
        {
            return;
        }

        // Return always, also for my PC
        // Nemůžu, občas to používám
        //return;

        //if (Instance != null)
        //{
        //    return;
        //}
        //Instance = this;
        var fCloudProviders = AppData.ci.GetFileCommonSettings("CloudProviders.txt");

        var (header, l) = SF.GetAllElementsFileAdvanced(fCloudProviders, null);

        if (header.Count != 0)
        {

            isUseCloud = true;
            //folders = SF.ToDictionary<string, string>(l);
            var OneDriveFolders = SHSE.Split(header[0], AllStringsSE.ast);
            OneDriveFolder0 = OneDriveFolders[0];
            OneDriveFolder1 = OneDriveFolders[1];
            GDriveFolder = l[0][0];

            string oneDriveExe = header[1];

            if (myStations != null)
            {
                oneDriveExe = oneDriveExe.Replace(SH.WrapWithBs(myStations.Vps), SH.WrapWithBs(myStations.Mb));
            }

            //if (!VpsHelperSunamo.IsVps)
            //{
            //    OneDriveExe = oneDriveExe;
            //}

            GDriveExe = l[0][1];

            OneDriveFn = Path.GetFileNameWithoutExtension(OneDriveExe);
            GDriveFn = Path.GetFileNameWithoutExtension(GDriveExe);
        }
    }



    public static void OpenSyncAppIfNotRunning(string ss2)
    {
        if (!isUseCloud)
        {
            return;
        }

        if (OneDriveExe == null)
            return;

        if (ss2.StartsWith(OneDriveFolder0) || ss2.StartsWith(OneDriveFolder1))
        {
            if (!PH.IsAlreadyRunning(OneDriveFn))
            {
                AIStore.winPi?.PHWinPiRunAsDesktopUserNoAdmin(OneDriveExe);
                Thread.Sleep(5000);
            }
        }
        else if (ss2.StartsWith(GDriveFolder))
        {
            if (!PH.IsAlreadyRunning(GDriveFn))
            {
                Process.Start(GDriveExe);
                Thread.Sleep(5000);
            }
        }

    }
}
