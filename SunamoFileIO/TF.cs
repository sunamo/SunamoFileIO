namespace SunamoFileIO;

public partial class TF
{

    public static Func<string, bool>? IsUsed { get; set; } = null;

#pragma warning disable
    protected static bool LockedByBitLocker(string path)
#pragma warning restore
    {
        return false;
    }

    private static Type tfType = typeof(TF);

    public static bool ThrowExcIfCannotBeWritten { get; set; } = false;

    public static bool ReadFile { get; set; } = true;
    public static async Task<string> WaitD()
    {
        return await Task.Run(() => "");
    }

    public static string ReadFileParallel(string fileName, IList<string> searchStrings, IList<string> replacementStrings)
    {
        return ReadFileParallel(fileName, 1470, searchStrings, replacementStrings);
    }

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

    public static Encoding GetEncoding(string filename)
    {
        var file = new FileStream(filename, FileMode.Open, FileAccess.Read);
        var encoding = GetEncoding(file);
        file.Dispose();
        return encoding;
    }

    // Note: May not work correctly with all files - Air bank export returns US-ascii/1252 but file has diacritics.
    // Atom with auto-encoding returns ISO-8859-2 which is correct.
    public static Encoding GetEncoding(FileStream file)
    {
        var bom = new byte[4];
        file.ReadExactly(bom, 0, 4);
        return EncodingHelper.DetectEncoding(new List<byte>(bom));
    }

    public static void Delete(string path)
    {
        FileMs.Delete(path);
    }

    public static void Move(string source, string destination, bool isOverwriting = false)
    {
        FileCompat.Move(source, destination, isOverwriting);
    }

    public static void Copy(string source, string destination, bool isOverwriting = false)
    {
        FileMs.Copy(source, destination, isOverwriting);
    }

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

    public static object ReadFileOrReturn(string pathOrText)
    {
        if (pathOrText.Length > 250)
            return pathOrText;
        if (FileMs.Exists(pathOrText))
            return FileAsync.ReadAllTextAsync(pathOrText);
        return pathOrText;
    }

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

    public static
        async Task
    Replace(string filePath, string searchText, string replacementText)
    {
        var content =
            await FileAsync.ReadAllTextAsync(filePath);
        content = content.Replace(searchText, replacementText);
        await FileAsync.WriteAllTextAsync(filePath, content);
    }

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
