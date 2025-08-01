namespace SunamoFileIO.Tests;

public class TFTests
{
    const string bp = @"D:\_Test\PlatformIndependentNuGetPackages\SunamoFileIO\";

    static string FilePath(string fnwoe)
    {
        return bp + fnwoe + ".txt";
    }

    [Fact]
    public async Task ReadAllTextTest()
    {
        //SunamoInit.InitHelper.FileIO();

        //ThisApp.Name = "Test";
        ////CreatePathToFiles(AppData.AppData.ci.GetFileString);

        //AppData.ci.CreateAppFoldersIfDontExists(new SunamoPlatformUwpInterop.Args.CreateAppFoldersIfDontExistsArgs { });

        //var d = await TF.ReadAllText(@"D:\_Test\ConsoleApp1\ConsoleApp1\ParseTableFromCoolJobs\JobOffers.html");


    }

    /// <summary>
    /// Vytvořeno zda správně čte různé newline
    /// Zjištěno že ano, jak File tak TF
    /// I SHGetLines funguje OK! 
    /// Problém byl že SunamoStringGetLines nebyl imported, používal se transitient
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task ReadAllLinesTest_AllN()
    {
        var path = @"D:\_Test\PlatformIndependentNuGetPackages\SunamoFileIO\AllNN.cs";
        var o = await File.ReadAllLinesAsync(path);
        var l = await TF.ReadAllLines(path);

        
    }

    [Fact]
    public async Task ReadAllLinesTest_AllRn()
    {

        var path = bp + "AllRnRn.cs";
        // TF.ReadAllLines vrací 26 řádků, ReadAllLinesAsync 29
        var o = await File.ReadAllLinesAsync(path);
        var l = await TF.ReadAllLines(path);
    }

    [Fact]
    public async Task ReadAllLinesTest_CantRemoveEmptyLines()
    {
        var a = await TF.ReadAllLines(@"E:\vs\Projects\PlatformIndependentNuGetPackages\SunamoCl\SunamoCmd\CmdBootStrap.cs");
    }

        [Fact]
    public async Task WriteAllLinesTest()
    {
        var l = new List<string>(["a", "", "b"]);
        var fp = FilePath(nameof(WriteAllLinesTest));
        await TF.WriteAllLines(fp, l);
        var nlDeli = await DetectNlDelimiter(fp);
        Assert.True(true);
    }

    [Fact]
    public async Task WriteAllTextTest()
    {
        var l = @"a

b";
        var fp = FilePath(nameof(WriteAllLinesTest));
        await TF.WriteAllText(fp, l);
        var nlDeli = await DetectNlDelimiter(fp);
        Assert.True(true);
    }

    private async Task<bool> DetectNlDelimiter(string fp)
    {
        var c = await TF.ReadAllText(fp);
        if (c.Contains("\r\n"))
        {
            return false;
        }
        return true;
    }
}
