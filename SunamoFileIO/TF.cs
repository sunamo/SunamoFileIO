namespace SunamoFileIO;

/// <summary>
/// Text File helper class providing file I/O operations.
/// </summary>
public partial class TF
{
#if DEBUG
    /// <summary>
    /// Wait time in milliseconds before reading a file (debug only).
    /// </summary>
    public const int WaitMsBeforeReadFile = 1000;
#endif

    /// <summary>
    /// Function to check if a file is currently in use.
    /// </summary>
    public static Func<string, bool> IsUsed = null;

#pragma warning disable
    /// <summary>
    /// Checks if a path is locked by BitLocker.
    /// </summary>
    /// <param name="path">Path to check.</param>
    /// <returns>Always returns false (BitLocker check not implemented).</returns>
    protected static bool LockedByBitLocker(string path)
#pragma warning restore
    {
        return false;
    }

    private static Type type = typeof(TF);

    /// <summary>
    /// Throw exception if file cannot be written.
    /// </summary>
    public static bool ThrowExcIfCantBeWrite = false;

    /// <summary>
    /// Enable file reading operations.
    /// </summary>
    public static bool ReadFile = true;
#if ASYNC
    /// <summary>
    /// Waits and returns empty string (async helper).
    /// </summary>
    /// <returns>Empty string.</returns>
    public static async Task<string> WaitD()
    {
        return await Task.Run(() => "");
    }
#endif

    /// <summary>
    /// Reads file and performs parallel string replacements with default line count of 1470.
    /// </summary>
    /// <param name="fileName">Path to file to read.</param>
    /// <param name="searchStrings">List of strings to search for.</param>
    /// <param name="replacementStrings">List of replacement strings.</param>
    /// <returns>Empty string.</returns>
    public static string ReadFileParallel(string fileName, IList<string> searchStrings, IList<string> replacementStrings)
    {
        return ReadFileParallel(fileName, 1470, searchStrings, replacementStrings);
    }

    /// <summary>
    /// Reads file and performs parallel string replacements.
    /// </summary>
    /// <param name="fileName">Path to file to read.</param>
    /// <param name="linesCount">Expected number of lines in file.</param>
    /// <param name="searchStrings">List of strings to search for.</param>
    /// <param name="replacementStrings">List of replacement strings.</param>
    /// <returns>Empty string.</returns>
    public static string ReadFileParallel(string fileName, int linesCount, IList<string> searchStrings, IList<string> replacementStrings)
    {
        var allLines = new string[linesCount];
        using (var sr = FileMs.OpenText(fileName))
        {
            var lineIndex = 0;
            while (!sr.EndOfStream)
            {
                allLines[lineIndex] = sr.ReadLine();
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
    /// Reads configuration file lines, excluding comment lines starting with #.
    /// </summary>
    /// <param name="configFilePath">Path to configuration file.</param>
    /// <returns>List of non-comment lines.</returns>
    public static
#if ASYNC
        async Task<List<string>>
#else
    List<string>
#endif
    ReadConfigLines(string configFilePath)
    {
        var list = SHGetLines.GetLines(
#if ASYNC
            await
#endif
        FileMs.ReadAllTextAsync(configFilePath)).ToList();
        list = list.Where(d => !d.StartsWith("#")).ToList();
        return list;
    }

    /// <summary>
    /// Gets encoding from file by reading its BOM (Byte Order Mark).
    /// </summary>
    /// <param name="filename">Path to the file.</param>
    /// <returns>Detected encoding.</returns>
    public static Encoding GetEncoding(string filename)
    {
        var file = new FileStream(filename, FileMode.Open, FileAccess.Read);
        var encoding = GetEncoding(file);
        file.Dispose();
        return encoding;
    }

    /// <summary>
    /// Gets encoding from file stream by reading its BOM (Byte Order Mark).
    /// Note: May not work correctly with all files - Air bank export returns US-ascii/1252 but file has diacritics.
    /// Atom with auto-encoding returns ISO-8859-2 which is correct.
    /// </summary>
    /// <param name="file">File stream to read from.</param>
    /// <returns>Detected encoding.</returns>
    public static Encoding GetEncoding(FileStream file)
    {
        var bom = new byte[4];
        file.Read(bom, 0, 4);
        return EncodingHelper.DetectEncoding(new List<byte>(bom));
    }

    /// <summary>
    /// Deletes a file.
    /// </summary>
    /// <param name="path">Path to file to delete.</param>
    public static void Delete(string path)
    {
        FileMs.Delete(path);
    }

    /// <summary>
    /// Moves a file from source to destination.
    /// </summary>
    /// <param name="source">Source file path.</param>
    /// <param name="destination">Destination file path.</param>
    /// <param name="isOverwriting">Whether to overwrite if destination exists.</param>
    public static void Move(string source, string destination, bool isOverwriting = false)
    {
        FileMs.Move(source, destination, isOverwriting);
    }

    /// <summary>
    /// Copies a file from source to destination.
    /// </summary>
    /// <param name="source">Source file path.</param>
    /// <param name="destination">Destination file path.</param>
    /// <param name="isOverwriting">Whether to overwrite if destination exists.</param>
    public static void Copy(string source, string destination, bool isOverwriting = false)
    {
        FileMs.Copy(source, destination, isOverwriting);
    }

    /// <summary>
    /// Checks if a file exists.
    /// </summary>
    /// <param name="path">Path to check.</param>
    /// <returns>True if file exists, false otherwise.</returns>
    public static bool Exists(string path)
    {
        return FileMs.Exists(path);
    }

    /// <summary>
    /// Appends text to the start of files if they don't already contain it.
    /// </summary>
    /// <param name="files">List of file paths to process.</param>
    /// <param name="textToAppend">Text to append to start of file.</param>
    private static
#if ASYNC
        async Task
#else
    void
#endif
    AppendToStartOfFileIfDontContains(List<string> files, string textToAppend)
    {
        textToAppend += Environment.NewLine;
        foreach (var item in files)
        {
            var content =
#if ASYNC
                await
#endif
            FileMs.ReadAllTextAsync(item);
            if (!content.Contains(textToAppend))
            {
                content = textToAppend + content;
                await FileMs.WriteAllTextAsync(item, content);
            }
        }
    }

    /// <summary>
    /// Reads file content if path is valid and shorter than 250 characters, otherwise returns the input string.
    /// </summary>
    /// <param name="pathOrText">File path or text content.</param>
    /// <returns>File content if path exists and is valid, otherwise returns input string.</returns>
    public static object ReadFileOrReturn(string pathOrText)
    {
        if (pathOrText.Length > 250)
            return pathOrText;
        if (FileMs.Exists(pathOrText))
            return FileMs.ReadAllTextAsync(pathOrText);
        return pathOrText;
    }

    /// <summary>
    /// Gets the line number of the last non-empty line (after trimming).
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <returns>Line number of last non-empty line, or 0 if all lines are empty.</returns>
    public static
#if ASYNC
        async Task<int>
#else
    int
#endif
    GetNumberOfLinesTrimEnd(string filePath)
    {
        var lines = SHGetLines.GetLines(
#if ASYNC
            await
#endif
        FileMs.ReadAllTextAsync(filePath)).ToList();
        for (var i = lines.Count - 1; i >= 0; i--)
            if (lines[i].Trim() != "")
                return i;
        return 0;
    }

    /// <summary>
    /// Replaces lines that contain a string but don't start with it by adding a prefix.
    /// </summary>
    /// <param name="files">List of file paths to process.</param>
    /// <param name="searchText">Text to search for in lines.</param>
    /// <param name="prefix">Prefix to add if line doesn't start with searchText.</param>
    private static
#if ASYNC
        async Task
#else
    void
#endif
    ReplaceIfDontStartWith(List<string> files, string searchText, string prefix)
    {
        foreach (var item in files)
        {
            var lines = SHGetLines.GetLines(
#if ASYNC
                await
#endif
            FileMs.ReadAllTextAsync(item, null)).ToList();
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i].Trim();
                if (line.StartsWith(searchText))
                    lines[i] = lines[i].Replace(searchText, prefix + searchText);
            }

            await FileMs.WriteAllLinesAsync(item, lines);
        }
    }

    /// <summary>
    /// Replaces text in a file.
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <param name="searchText">Text to search for.</param>
    /// <param name="replacementText">Text to replace with.</param>
    public static
#if ASYNC
        async Task
#else
    void
#endif
    Replace(string filePath, string searchText, string replacementText)
    {
        var content =
#if ASYNC
            await
#endif
        FileMs.ReadAllTextAsync(filePath);
        content = content.Replace(searchText, replacementText);
#if ASYNC
        await
#endif
        FileMs.WriteAllTextAsync(filePath, content);
    }

    /// <summary>
    /// Applies a transformation function to file content and writes back if content changed.
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <param name="transformFunction">Function that transforms file content with an argument.</param>
    /// <param name="argument">Argument to pass to transformation function.</param>
    /// <returns>True if file was modified, false otherwise.</returns>
    public static
#if ASYNC
        async Task<bool>
#else
    bool
#endif
    PureFileOperationWithArg(string filePath, Func<string, string, string> transformFunction, string argument)
    {
        var content =
#if ASYNC
            await
#endif
        FileMs.ReadAllTextAsync(filePath);
        var transformedContent = transformFunction.Invoke(content, argument);
        if (content.Trim() != transformedContent.Trim())
        {
            await FileMs.WriteAllTextAsync(filePath, transformedContent);
            return true;
        }

        return false;
    }
}