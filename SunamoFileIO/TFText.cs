namespace SunamoFileIO;

partial class TF
{
    /// <summary>
    /// Read all text.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>The read all text result.</returns>
    public static
async Task<string>
ReadAllText(string path, Encoding? encoding = null)
    {
        if (!File.Exists(path))
        {
            await TF.WriteAllText(path, "");
            return "";
        }

        encoding ??= Encoding.UTF8;

        if (LockedByBitLocker(path)) return string.Empty;

        if (IsUsed != null)
            if (IsUsed.Invoke(path))
                return string.Empty;

        return await FileAsync.ReadAllTextAsync(path, encoding);
    }

    #region WriteAllText

    /// <summary>
    /// Write all text.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="stringBuilder">The string builder.</param>
    public static
async Task
WriteAllText(string path, StringBuilder stringBuilder)
    {
        await WriteAllText(path, stringBuilder.ToString().ToUnixLineEnding());
    }

    /// <summary>
    /// Write all text.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="content">The content.</param>
    /// <param name="isAppending">The is appending.</param>
    public static
        async Task
        WriteAllText(string path, string content, bool isAppending)
    {
        if (isAppending)
        {
            await AppendAllText(path, content.ToUnixLineEnding());
        }
        else
        {
            await WriteAllText(path, content.ToUnixLineEnding());
        }
    }

    /// <summary>
    /// Write all text.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="content">The content.</param>
    /// <param name="encoding">The encoding.</param>
    public static
        async Task
        WriteAllText(string path, string content, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;

        if (LockedByBitLocker(path)) return;
        await FileAsync.WriteAllTextAsync(path, content);
    }
    #endregion

    /// <summary>
    /// Append all text.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="content">The content.</param>
    public static
        async Task
        AppendAllText(string path, string content)
    {
        if (!File.Exists(path))
        {
            await FileAsync.WriteAllTextAsync(path, "");
        }

        if (LockedByBitLocker(path)) return;

        try
        {
            await FileAsync.AppendAllTextAsync(path, content);
        }
        catch (Exception)
        {
        }
    }
}
