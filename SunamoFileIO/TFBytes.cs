// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
﻿namespace SunamoFileIO;
partial class TF
{
    #region Bytes
    public static
#if ASYNC
        async Task<List<byte>>
#else
List<byte>
#endif
        ReadAllBytes(string file)
    {
        if (LockedByBitLocker(file))
        {
            return new List<byte>();
        }
#if ASYNC

#endif
        return
#if ASYNC
            (await File.ReadAllBytesAsync(file)).ToList();
#else
File.ReadAllBytes(file).ToList();
#endif
    }

    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllBytes(string file, IEnumerable<byte> b)
    {
        if (LockedByBitLocker(file)) return;
#if ASYNC
        await File.WriteAllBytesAsync(file, b.ToArray());
#else
File.WriteAllBytes(file, b.ToArray());
#endif
    }

    #endregion
}
