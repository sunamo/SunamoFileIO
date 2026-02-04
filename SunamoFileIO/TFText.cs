namespace SunamoFileIO;

partial class TF
{
    /// <summary>
    /// Reads all text from a file, creating it if it doesn't exist.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="encoding">Encoding to use (defaults to UTF-8).</param>
    /// <returns>Content of the file, or empty string if file doesn't exist or is locked.</returns>
    public static
#if ASYNC
async Task<string>
#else
string
#endif
ReadAllText(string path, Encoding? encoding = null)
    {
        if (!File.Exists(path))
        {
            await TF.WriteAllText(path, "");
            return "";
        }

        if (encoding == null)
        {
            encoding = Encoding.UTF8;
        }

        if (LockedByBitLocker(path)) return string.Empty;

        if (IsUsed != null)
            if (IsUsed.Invoke(path))
                return string.Empty;

#if ASYNC
        return await File.ReadAllTextAsync(path, encoding);
#else
return File.ReadAllText(path, encoding);
#endif
    }

    #region WriteAllText

    /// <summary>
    /// Writes all text from StringBuilder to a file with Unix line endings.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="stringBuilder">StringBuilder containing content to write.</param>
    public static
#if ASYNC
async Task
#else
void
#endif
WriteAllText(string path, StringBuilder stringBuilder)
    {
#if ASYNC
        await
#endif
            WriteAllText(path, stringBuilder.ToString().ToUnixLineEnding());
    }

    /// <summary>
    /// Writes or appends text to a file with Unix line endings.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="content">Content to write or append.</param>
    /// <param name="isAppending">Whether to append to file instead of overwriting.</param>
    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllText(string path, string content, bool isAppending)
    {
        if (isAppending)
            await AppendAllText(path, content.ToUnixLineEnding());
        else
            await WriteAllText(path, content.ToUnixLineEnding());
    }

    /// <summary>
    /// Writes all text to a file.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="content">Content to write to file.</param>
    /// <param name="encoding">Encoding to use (defaults to UTF-8).</param>
    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllText(string path, string content, Encoding? encoding = null)
    {
        if (encoding == null)
        {
            encoding = Encoding.UTF8;
        }

        if (LockedByBitLocker(path)) return;
#if ASYNC
        await File.WriteAllTextAsync(path, content);
#else
File.WriteAllText(path, content);
#endif
    }
    #endregion

    /// <summary>
    /// Appends text to a file, creating it if it doesn't exist.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    /// <param name="content">Content to append to file.</param>
    public static
#if ASYNC
        async Task
#else
void
#endif
        AppendAllText(string path, string content)
    {
        if (!File.Exists(path))
        {
            await File.WriteAllTextAsync(path, "");
        }

        if (LockedByBitLocker(path)) return;

        try
        {
#if ASYNC
            await File.AppendAllTextAsync(path, content);
#else
File.AppendAllText(path, content);
#endif
        }
        catch (Exception)
        {
        }
    }
}