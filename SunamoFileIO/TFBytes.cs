namespace SunamoFileIO;

partial class TF
{
    #region Bytes

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

    public static
        async Task
        WriteAllBytes(string filePath, IEnumerable<byte> bytes)
    {
        if (LockedByBitLocker(filePath)) return;
        await FileAsync.WriteAllBytesAsync(filePath, bytes.ToArray());
    }

    #endregion
}
