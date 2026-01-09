namespace SunamoFileIO;

/// <summary>
/// Helper class for managing cloud provider configurations and folders.
/// </summary>
public class CloudProvidersHelper
{
    /// <summary>
    /// Primary OneDrive folder path.
    /// </summary>
    public static string? OneDriveFolder0 { get; set; } = null;

    /// <summary>
    /// Secondary OneDrive folder path.
    /// </summary>
    public static string? OneDriveFolder1 { get; set; } = null;

    /// <summary>
    /// Google Drive folder path.
    /// </summary>
    public static string? GDriveFolder { get; set; } = null;

    /// <summary>
    /// OneDrive executable path.
    /// </summary>
    public static string? OneDriveExe { get; set; } = null;

    /// <summary>
    /// Google Drive executable path.
    /// </summary>
    public static string? GDriveExe { get; set; } = null;

    /// <summary>
    /// OneDrive executable filename without extension.
    /// </summary>
    public static string? OneDriveFilename { get; set; } = null;

    /// <summary>
    /// Google Drive executable filename without extension.
    /// </summary>
    public static string? GDriveFilename { get; set; } = null;

    /// <summary>
    /// Dynamic stations configuration object.
    /// </summary>
    public static dynamic? MyStations { get; set; } = null;

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