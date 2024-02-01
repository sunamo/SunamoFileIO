namespace SunamoFileIO;

//using se = SunamoExceptions;

//public partial class TF
//{
//    #region For easy copy
//    static bool LockedByBitLocker(string path)
//    {
//        return ThrowEx.LockedByBitLocker(path);
//    }

//    #region Array
//    public static
//#if ASYNC
//    async Task
//#else
//    void
//#endif
// WriteAllLinesArray(string path, String[] c)
//    {

//#if ASYNC
//        await
//#endif
//     se.File.WriteAllLinesAsyncArray(path, c);
//    }

//    public static
//#if ASYNC
//    async Task
//#else
//    void
//#endif
// WriteAllBytesArray(string path, Byte[] c)
//    {

//#if ASYNC
//        await
//#endif
//     se.File.WriteAllBytesAsyncArray(path, c);
//    }

//    public static
//#if ASYNC
//    async Task<Byte[]>
//#else
//      Byte[]
//#endif
// ReadAllBytesArray(string path)
//    {
//        return
//#if ASYNC
//    await
//#endif
// se.File.ReadAllBytesAsyncArray(path).ToList();
//    }
//    #endregion

//    #region Bytes
//    /// <summary>
//    /// Only one method where could be TF.ReadAllBytes
//    /// </summary>
//    /// <param name="file"></param>
//    /// <returns></returns>
//    public static
//#if ASYNC
//    async Task<List<byte>>
//#else
//      List<byte>
//#endif
// ReadAllBytes(string file)
//    {
//        return
//#if ASYNC
//    await
//#endif
// se.File.ReadAllBytesAsync(file).ToList();
//    }
//    public static
//#if ASYNC
//    async Task
//#else
//    void
//#endif
// WriteAllBytes(string file, List<byte> b)
//    {

//#if ASYNC
//        await
//#endif
//     se.File.WriteAllBytesAsync(file, b);
//    }
//    #endregion

//    #region Lines
//    public static
//#if ASYNC
//    async Task
//#else
//    void
//#endif
// WriteAllLines(string file, IList<string> lines)
//    {

//#if ASYNC
//        await
//#endif
//     se.File.WriteAllLinesAsync(file, lines);
//    }

//    public static
//#if ASYNC
//    async Task<List<string>>
//#else
//    List<string>
//#endif
// ReadAllLines(string file)
//    {
//        return
//#if ASYNC
//    await
//#endif
// se.File.ReadAllLinesAsync(file).ToList();
//    }
//    #endregion

//    #region Text
//    //    public static
//    //#if ASYNC
//    //    async Task
//    //#else
//    //    void
//    //#endif
//    // WriteAllText(string path, string content)
//    //    {

//    //#if ASYNC
//    //        await
//    //#endif
//    //     se.File.WriteAllTextAsync(path, content);
//    //    }

//    public static
//#if ASYNC
//    async Task<string>
//#else
//    string
//#endif
// ReadAllText(string f)
//    {
//        return
//#if ASYNC
//    await
//#endif
// se.File.ReadAllTextAsync(f);
//    }

//    public static
//#if ASYNC
//    async Task<string>
//#else
//    string
//#endif
// ReadAllText(string path, Encoding enc)
//    {
//        return
//#if ASYNC
//    await
//#endif
// se.File.ReadAllTextAsync(path, enc);
//    }
//    #endregion
//    #endregion
//}
