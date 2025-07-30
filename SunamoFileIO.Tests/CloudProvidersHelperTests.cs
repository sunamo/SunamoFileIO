namespace SunamoFileIO.Tests;

public class CloudProvidersHelperTests
{
    [Fact]
    public void TestPropertiesValues()
    {
        Assert.Equal(@"D:\Drive\", CloudProvidersHelper.GDriveFolder);
    }
}
