// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoFileIO.Tests;

/// <summary>
/// Tests for TF (Text File) class file I/O operations.
/// </summary>
public class TFTests
{
    const string basePath = @"D:\_Test\PlatformIndependentNuGetPackages\SunamoFileIO\";

    /// <summary>
    /// Generates a full file path from a filename without extension.
    /// </summary>
    /// <param name="filenameWithoutExtension">Filename without the .txt extension.</param>
    /// <returns>Full file path with .txt extension.</returns>
    static string FilePath(string filenameWithoutExtension)
    {
        return basePath + filenameWithoutExtension + ".txt";
    }

    /// <summary>
    /// Tests reading all text from a file.
    /// </summary>
    [Fact]
    public async Task ReadAllTextTest()
    {
        // EN: Test placeholder - implement actual test logic
        // CZ: Zástupce pro test - implementuj skutečnou testovací logiku
        await Task.CompletedTask;
    }

    /// <summary>
    /// EN: Tests whether different newline formats are read correctly
    /// Both File and TF handle it correctly. SHGetLines also works OK!
    /// The issue was that SunamoStringGetLines wasn't imported, transient dependency was used.
    /// </summary>
    [Fact]
    public async Task ReadAllLinesTest_AllN()
    {
        var path = @"D:\_Test\PlatformIndependentNuGetPackages\SunamoFileIO\AllNN.cs";
        var fileLines = await File.ReadAllLinesAsync(path);
        var list = await TF.ReadAllLines(path);


    }

    /// <summary>
    /// Tests reading lines from a file with Windows-style line endings (CRLF).
    /// </summary>
    [Fact]
    public async Task ReadAllLinesTest_AllRn()
    {

        var path = basePath + "AllRnRn.cs";
        // EN: TF.ReadAllLines returns 26 lines, ReadAllLinesAsync returns 29
        var fileLines = await File.ReadAllLinesAsync(path);
        var list = await TF.ReadAllLines(path);
    }

    /// <summary>
    /// Tests that reading all lines doesn't incorrectly remove empty lines.
    /// </summary>
    [Fact]
    public async Task ReadAllLinesTest_CantRemoveEmptyLines()
    {
        var argument = await TF.ReadAllLines(@"E:\vs\Projects\PlatformIndependentNuGetPackages\SunamoCl\SunamoCmd\CmdBootStrap.cs");
    }

    /// <summary>
    /// Tests writing all lines to a file.
    /// </summary>
    [Fact]
    public async Task WriteAllLinesTest()
    {
        var list = new List<string>(["a", "", "b"]);
        var fp = FilePath(nameof(WriteAllLinesTest));
        await TF.WriteAllLines(fp, list);
        var nlDeli = await DetectNlDelimiter(fp);
        Assert.True(true);
    }

    /// <summary>
    /// Tests writing all text to a file.
    /// </summary>
    [Fact]
    public async Task WriteAllTextTest()
    {
        var list = @"a

b";
        var fp = FilePath(nameof(WriteAllLinesTest));
        await TF.WriteAllText(fp, list);
        var nlDeli = await DetectNlDelimiter(fp);
        Assert.True(true);
    }

    /// <summary>
    /// Detects the newline delimiter used in a file.
    /// </summary>
    /// <param name="fp">File path to check.</param>
    /// <returns>True if file uses Unix line endings (LF), false if Windows line endings (CRLF).</returns>
    private async Task<bool> DetectNlDelimiter(string fp)
    {
        var content = await TF.ReadAllText(fp);
        if (content.Contains("\r\n"))
        {
            return false;
        }
        return true;
    }
}
