namespace SunamoFileIO;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
partial class TF
{

    public static
#if ASYNC
async Task<string>
#else
string
#endif
ReadAllText(string path, Encoding? enc = null)
    {
        if (!File.Exists(path))
        {
            await TF.WriteAllText(path, "");
            return "";
        }

        if (enc == null)
        {
            enc = Encoding.UTF8;
        }

        if (LockedByBitLocker(path)) return string.Empty;

        if (isUsed != null)
            if (isUsed.Invoke(path))
                return string.Empty;

#if ASYNC
        return await File.ReadAllTextAsync(path, enc);
#else
return File.ReadAllText(path, enc);
#endif
    }

    #region WriteAllText
    public static
#if ASYNC
async Task
#else
void
#endif
WriteAllText(string file, StringBuilder sb)
    {
#if ASYNC
        await
#endif
            WriteAllText(file, sb.ToString().ToUnixLineEnding());
    }

    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllText(string file, string content, bool append)
    {
        if (append)
            await AppendAllText(file, content.ToUnixLineEnding());
        else
            await WriteAllText(file, content.ToUnixLineEnding());
    }


    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllText(string path, string content, Encoding encoding = null)
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
#if ASYNC

#endif
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