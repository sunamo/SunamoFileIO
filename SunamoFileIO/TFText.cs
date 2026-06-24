namespace SunamoFileIO;

partial class TF
{
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

    public static
async Task
WriteAllText(string path, StringBuilder stringBuilder)
    {
        await WriteAllText(path, stringBuilder.ToString().ToUnixLineEnding());
    }

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

    public static
        async Task
        WriteAllText(string path, string content, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;

        if (LockedByBitLocker(path)) return;
        await FileAsync.WriteAllTextAsync(path, content);
    }
    #endregion

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
