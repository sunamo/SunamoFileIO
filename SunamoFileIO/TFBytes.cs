namespace SunamoFileIO;

partial class TF
{
    #region Bytes

    /// <summary>
    /// Reads all bytes from a file.
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <returns>List of bytes from file, or empty list if locked by BitLocker.</returns>
    public static
#if ASYNC
        async Task<List<byte>>
#else
List<byte>
#endif
        ReadAllBytes(string filePath)
    {
        if (LockedByBitLocker(filePath))
        {
            return new List<byte>();
        }

        return
#if ASYNC
            (await File.ReadAllBytesAsync(filePath)).ToList();
#else
File.ReadAllBytes(filePath).ToList();
#endif
    }

    /// <summary>
    /// Writes all bytes to a file.
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <param name="bytes">Bytes to write to file.</param>
    public static
#if ASYNC
        async Task
#else
void
#endif
        WriteAllBytes(string filePath, IEnumerable<byte> bytes)
    {
        if (LockedByBitLocker(filePath)) return;
#if ASYNC
        await File.WriteAllBytesAsync(filePath, bytes.ToArray());
#else
File.WriteAllBytes(filePath, bytes.ToArray());
#endif
    }

    #endregion
}