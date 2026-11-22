namespace SunamoFileIO;

/// <summary>
/// Represents the TF type.
/// </summary>
public partial class TF
{

    /// <summary>
    /// Gets or sets the is used.
    /// </summary>
    public static Func<string, bool>? IsUsed { get; set; } = null;

#pragma warning disable
    protected static bool LockedByBitLocker(string path)
#pragma warning restore
    {
        return false;
    }

    private static Type tfType = typeof(TF);

    /// <summary>
    /// Gets or sets the throw exc if cannot be written.
    /// </summary>
    public static bool ThrowExcIfCannotBeWritten { get; set; } = false;

    /// <summary>
    /// Gets or sets the read file.
    /// </summary>
    public static bool ReadFile { get; set; } = true;
    /// <summary>
    /// Wait d.
    /// </summary>
    /// <returns>The wait d result.</returns>
    public static async Task<string> WaitD()
    {
        return await Task.Run(() => "");
    }

    /// <summary>
    /// Read file parallel.
    /// </summary>
    /// <returns>The read file parallel result.</returns>
    public static string ReadFileParallel(string fileName, IList<string> searchStrings, IList<string> replacementStrings)
    {
        return ReadFileParallel(fileName, 1470, searchStrings, replacementStrings);
    }

    /// <summary>
    /// Read file parallel.
    /// </summary>
    /// <param name="()">The ().</param>
    /// <returns>The read file parallel result.</returns>
    public static string ReadFileParallel(string fileName, int linesCount, IList<string> searchStrings, IList<string> replacementStrings)
    {
        var allLines = new string[linesCount];
        using (var sr = FileMs.OpenText(fileName))
        {
            var lineIndex = 0;
            while (!sr.EndOfStream)
            {
                allLines[lineIndex] = sr.ReadLine()!;
                lineIndex += 1;
            }
        }

        if (searchStrings != null)
            for (var i = 0; i < searchStrings.Count; i++)
                Parallel.For(0, allLines.Length, currentLineIndex =>
                {
                    allLines[currentLineIndex] = allLines[currentLineIndex].Replace(searchStrings[i], replacementStrings[i]);
                });
        return string.Empty;
    }

    /// <summary>
    /// Read config lines.
    /// </summary>
    /// <param name="configFilePath">The config file path.</param>
    /// <returns>The read config lines result.</returns>
    public static
        async Task<List<string>>
    ReadConfigLines(string configFilePath)
    {
        var list = SHGetLines.GetLines(
            await FileAsync.ReadAllTextAsync(configFilePath)
        ).ToList();
        list = list.Where(line => !line.StartsWith("#")).ToList();
        return list;
    }

    /// <summary>
    /// Initializes a new instance of the get encoding class.
    /// </summary>
    /// <param name="configFilePath">The config file path.</param>
    public static Encoding GetEncoding(string filename)
    {
        var file = new FileStream(filename, FileMode.Open, FileAccess.Read);
        var encoding = GetEncoding(file);
        file.Dispose();
        return encoding;
    }

    // Note: May not work correctly with all files - Air bank export returns US-ascii/1252 but file has diacritics.
    // Atom with auto-encoding returns ISO-8859-2 which is correct.
    /// <summary>
    /// Get encoding.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <param name="FileMode.Open">The file mode open.</param>
    /// <param name="FileAccess.Read">The file access read.</param>
    /// <returns>The get encoding result.</returns>
    public static Encoding GetEncoding(FileStream file)
    {
        var bom = new byte[4];
        file.ReadExactly(bom, 0, 4);
        return EncodingHelper.DetectEncoding(new List<byte>(bom));
    }

    /// <summary>
    /// Delete.
    /// </summary>
    /// <param name="file">The file.</param>
    /// <returns>The delete result.</returns>
    public static void Delete(string path)
    {
        FileMs.Delete(path);
    }

    /// <summary>
    /// Initializes a new instance of the move class.
    /// </summary>
    /// <param name="bom">The bom.</param>
    /// <param name="0">The 0.</param>
    /// <param name="4">The 4.</param>
    public static void Move(string source, string destination, bool isOverwriting = false)
    {
        FileCompat.Move(source, destination, isOverwriting);
    }

    /// <summary>
    /// Copy.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The copy result.</returns>
    public static void Copy(string source, string destination, bool isOverwriting = false)
    {
        FileMs.Copy(source, destination, isOverwriting);
    }

    /// <summary>
    /// Exists.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="isOverwriting">The is overwriting.</param>
    /// <returns>The exists result.</returns>
    public static bool Exists(string path)
    {
        return FileMs.Exists(path);
    }

    private static
        async Task
    AppendToStartOfFileIfDontContains(List<string> files, string textToAppend)
    {
        textToAppend += Environment.NewLine;
        foreach (var item in files)
        {
            var content =
                await FileAsync.ReadAllTextAsync(item);
            if (!content.Contains(textToAppend))
            {
                content = textToAppend + content;
                await FileAsync.WriteAllTextAsync(item, content);
            }
        }
    }

    /// <summary>
    /// Read file or return.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>The read file or return result.</returns>
    public static object ReadFileOrReturn(string pathOrText)
    {
        if (pathOrText.Length > 250)
            return pathOrText;
        if (FileMs.Exists(pathOrText))
            return FileAsync.ReadAllTextAsync(pathOrText);
        return pathOrText;
    }

    /// <summary>
    /// Get number of lines trim end.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>The get number of lines trim end result.</returns>
    public static
        async Task<int>
    GetNumberOfLinesTrimEnd(string filePath)
    {
        var lines = SHGetLines.GetLines(
            await FileAsync.ReadAllTextAsync(filePath)
        ).ToList();
        for (var i = lines.Count - 1; i >= 0; i--)
            if (lines[i].Trim() != "")
                return i;
        return 0;
    }

    private static
        async Task
    ReplaceIfDontStartWith(List<string> files, string searchText, string prefix)
    {
        foreach (var item in files)
        {
            var lines = SHGetLines.GetLines(
                await FileAsync.ReadAllTextAsync(item)
            ).ToList();
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i].Trim();
                if (line.StartsWith(searchText))
                    lines[i] = lines[i].Replace(searchText, prefix + searchText);
            }

            await FileAsync.WriteAllLinesAsync(item, lines);
        }
    }

    /// <summary>
    /// Replace.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="searchText">The search text.</param>
    /// <param name="replacementText">The replacement text.</param>
    public static
        async Task
    Replace(string filePath, string searchText, string replacementText)
    {
        var content =
            await FileAsync.ReadAllTextAsync(filePath);
        content = content.Replace(searchText, replacementText);
        await FileAsync.WriteAllTextAsync(filePath, content);
    }

    /// <summary>
    /// Pure file operation with arg.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="transformFunction">The transform function.</param>
    /// <param name="argument">The argument.</param>
    /// <returns>The pure file operation with arg result.</returns>
    public static
        async Task<bool>
    PureFileOperationWithArg(string filePath, Func<string, string, string> transformFunction, string argument)
    {
        var content =
            await FileAsync.ReadAllTextAsync(filePath);
        var transformedContent = transformFunction.Invoke(content, argument);
        if (content.Trim() != transformedContent.Trim())
        {
            await FileAsync.WriteAllTextAsync(filePath, transformedContent);
            return true;
        }

        return false;
    }
}
