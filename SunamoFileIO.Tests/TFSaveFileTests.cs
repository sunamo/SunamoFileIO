namespace SunamoFileIO.Tests;

/// <summary>
/// Tests for TF.SaveFile.
/// </summary>
public class TFSaveFileTests
{
    /// <summary>
    /// Tests that SaveFile creates the missing parent folders before writing.
    /// </summary>
    [Fact]
    public void SaveFile_CreatesMissingParentFolder()
    {
        var root = Path.Combine(Path.GetTempPath(), "SunamoFileIO.Tests", Guid.NewGuid().ToString("N"));
        var path = Path.Combine(root, "a", "b", "file.txt");
        try
        {
            TF.SaveFile("content", path);

            Assert.Equal("content", File.ReadAllText(path));
        }
        finally
        {
            if (Directory.Exists(root))
                Directory.Delete(root, true);
        }
    }

    /// <summary>
    /// Tests that SaveFile writes an empty file for null content and overwrites an existing file.
    /// </summary>
    [Fact]
    public void SaveFile_NullContentWritesEmptyFile()
    {
        var root = Path.Combine(Path.GetTempPath(), "SunamoFileIO.Tests", Guid.NewGuid().ToString("N"));
        var path = Path.Combine(root, "file.txt");
        try
        {
            TF.SaveFile("first", path);
            TF.SaveFile(null!, path);

            Assert.Equal(string.Empty, File.ReadAllText(path));
        }
        finally
        {
            if (Directory.Exists(root))
                Directory.Delete(root, true);
        }
    }
}
