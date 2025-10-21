// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
﻿namespace SunamoFileIO;
partial class TF
{
    #region Array

    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllLinesArray(string path, string[] c)
    {
#if ASYNC
        await
#endif
            WriteAllLines(path, c.ToList());
    }

    public static
#if ASYNC
        async Task<String[]>
#else
byte[]
#endif
        ReadAllLinesArray(string path)
    {
        return (
#if ASYNC
            await
#endif
                ReadAllLines(path)).ToArray();
    }

    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllBytesArray(string path, byte[] c)
    {
#if ASYNC
        await
#endif
            WriteAllBytes(path, c.ToList());
    }

    public static
#if ASYNC
        async Task<byte[]>
#else
byte[]
#endif
        ReadAllBytesArray(string path)
    {
        return (
#if ASYNC
            await
#endif
                ReadAllBytes(path)).ToArray();
    }

    #endregion
}
