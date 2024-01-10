namespace SunamoFileIO;


public partial class TF
{
    #region For easy copy

    public static List<byte> bomUtf8 = new List<byte>( [239, 187, 191]);

    public static
#if ASYNC
    async Task
#else
void
#endif
    RemoveDoubleBomUtf8(string path)
    {
        var b =
#if ASYNC
        await
#endif
        TF.ReadAllBytes(path);
        var to = b.Count > 5 ? 6 : b.Count;

        for (int i = 3; i < to; i++)
        {
            if (bomUtf8[i - 3] != b[i])
            {
                break;
            }
        }

        b = b.Skip(3).ToList();
        TF.WriteAllBytes(path, b);
    }
    #endregion
}
