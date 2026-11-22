namespace SunamoFileIO;

/// <summary>
/// Represents the cloud providers helper type.
/// </summary>
public class CloudProvidersHelper
{
    /// <summary>
    /// Gets or sets the one drive folder 0.
    /// </summary>
    public static string? OneDriveFolder0 { get; set; } = null;

    /// <summary>
    /// Gets or sets the one drive folder 1.
    /// </summary>
    public static string? OneDriveFolder1 { get; set; } = null;

    /// <summary>
    /// Gets or sets the g drive folder.
    /// </summary>
    public static string? GDriveFolder { get; set; } = null;

    /// <summary>
    /// Gets or sets the one drive exe.
    /// </summary>
    public static string? OneDriveExe { get; set; } = null;

    /// <summary>
    /// Gets or sets the g drive exe.
    /// </summary>
    public static string? GDriveExe { get; set; } = null;

    /// <summary>
    /// Gets or sets the one drive filename.
    /// </summary>
    public static string? OneDriveFilename { get; set; } = null;

    /// <summary>
    /// Gets or sets the g drive filename.
    /// </summary>
    public static string? GDriveFilename { get; set; } = null;

    /// <summary>
    /// Gets or sets the my stations.
    /// </summary>
    public static dynamic? MyStations { get; set; } = null;

    private CloudProvidersHelper()
    {
    }

    /// <summary>
    /// Init.
    /// </summary>
    public static void Init()
    {
        if (GDriveFolder != null) return;
    }
}
