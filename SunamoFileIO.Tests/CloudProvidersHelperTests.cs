namespace SunamoFileIO.Tests;

/// <summary>
/// Tests for CloudProvidersHelper class.
/// </summary>
public class CloudProvidersHelperTests
{
    /// <summary>
    /// Tests that CloudProvidersHelper properties have expected values.
    /// </summary>
    [Fact]
    public void TestPropertiesValues()
    {
        Assert.Equal(@"D:\Drive\", CloudProvidersHelper.GDriveFolder);
    }
}
