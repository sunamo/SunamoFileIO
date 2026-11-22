namespace SunamoFileIO;

partial class TF
{
    #region Bytes

    /// <summary>
    /// Read all bytes.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>The read all bytes result.</returns>
    public static
        async Task<List<byte>>
        ReadAllBytes(string filePath)
    {
        if (LockedByBitLocker(filePath))
        {
            return new List<byte>();
        }

        return
            (await FileAsync.ReadAllBytesAsync(filePath)).ToList();
    }

    /// <summary>
    /// Write all bytes.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="bytes">The bytes.</param>
    public static
        async Task
        WriteAllBytes(string filePath, IEnumerable<byte> bytes)
    {
        if (LockedByBitLocker(filePath)) return;
        await FileAsync.WriteAllBytesAsync(filePath, bytes.ToArray());
    }

    #endregion
}
