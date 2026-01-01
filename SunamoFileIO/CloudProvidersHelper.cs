namespace SunamoFileIO;

/// <summary>
/// Helper class for managing cloud provider configurations and folders.
/// </summary>
public class CloudProvidersHelper
{
    /// <summary>
    /// Primary OneDrive folder path.
    /// </summary>
    public static string OneDriveFolder0 = null;

    /// <summary>
    /// Secondary OneDrive folder path.
    /// </summary>
    public static string OneDriveFolder1 = null;

    /// <summary>
    /// Google Drive folder path.
    /// </summary>
    public static string GDriveFolder = null;

    /// <summary>
    /// OneDrive executable path.
    /// </summary>
    public static string OneDriveExe = null;

    /// <summary>
    /// Google Drive executable path.
    /// </summary>
    public static string GDriveExe = null;

    /// <summary>
    /// OneDrive executable filename without extension.
    /// </summary>
    public static string OneDriveFn = null;

    /// <summary>
    /// Google Drive executable filename without extension.
    /// </summary>
    public static string GDriveFn = null;

    /// <summary>
    /// Dynamic stations configuration object.
    /// </summary>
    public static dynamic myStations = null;

    private CloudProvidersHelper()
    {
    }

    /// <summary>
    /// Initializes cloud provider settings. Currently returns early if GDriveFolder is already set.
    /// </summary>
    public static void Init()
    {
        if (GDriveFolder != null) return;
    }
}