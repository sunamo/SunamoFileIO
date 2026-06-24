namespace SunamoFileIO;

public class CloudProvidersHelper
{
    public static string? OneDriveFolder0 { get; set; } = null;

    public static string? OneDriveFolder1 { get; set; } = null;

    public static string? GDriveFolder { get; set; } = null;

    public static string? OneDriveExe { get; set; } = null;

    public static string? GDriveExe { get; set; } = null;

    public static string? OneDriveFilename { get; set; } = null;

    public static string? GDriveFilename { get; set; } = null;

    public static dynamic? MyStations { get; set; } = null;

    private CloudProvidersHelper()
    {
    }

    public static void Init()
    {
        if (GDriveFolder != null) return;
    }
}
