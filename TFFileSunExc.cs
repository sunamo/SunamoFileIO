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
//     se.TF.WriteAllLinesArray(path, c);
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
//     se.TF.WriteAllBytesArray(path, c);
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
// se.TF.ReadAllBytesArray(path);
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
// se.TF.ReadAllBytes(file);
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
//     se.TF.WriteAllBytes(file, b);
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
//     se.TF.WriteAllLines(file, lines);
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
// se.TF.ReadAllLines(file);
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
//    //     se.TF.WriteAllText(path, content);
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
// se.TF.ReadAllText(f);
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
// se.TF.ReadAllText(path, enc);
//    }
//    #endregion
//    #endregion
//}
