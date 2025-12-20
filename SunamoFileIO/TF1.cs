// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoFileIO;
public partial class TF
{
    public static 
#if ASYNC
        async Task<bool>
#else
    void 
#endif
    PureFileOperation(string f, Func<string, string> transformHtmlToMetro4, string insertBetweenFilenameAndExtension)
    {
        var content = 
#if ASYNC
            await
#endif
        FileMs.ReadAllTextAsync(f);
        var contentNew = transformHtmlToMetro4.Invoke(content);
        if (contentNew != content)
        {
#if ASYNC
            await
#endif
            WriteAllText(FS.InsertBetweenFileNameAndExtension(f, insertBetweenFilenameAndExtension), content);
            return true;
        }

        return false;
    }

    public static 
#if ASYNC
        async Task<bool>
#else
    void 
#endif
    PureFileOperation(string f, Func<string, string> transformHtmlToMetro4)
    {
        var content = (
#if ASYNC
            await
#endif
        FileMs.ReadAllTextAsync(f)).Trim();
        var content2 = transformHtmlToMetro4.Invoke(content);
        if (string.Compare(content, content2) != 0)
        {
            //TF.SaveFile(content, CompareFilesPaths.GetFile(CompareExt.cs, 1));
            //TF.SaveFile(content2, CompareFilesPaths.GetFile(CompareExt.cs, 2));
#if ASYNC
            await
#endif
            FileMs.WriteAllTextAsync(f, content2);
            return true;
        }

        return false;
    }

    /// <summary>
    ///     StreamReader is derived from TextReader
    /// </summary>
    /// <param name = "file"></param>
    public static StreamReader TextReader(string file)
    {
        return FileMs.OpenText(file);
    }

    public static async Task CreateEmptyFileWhenDoesntExists(string path)
    {
        //await CreateEmptyFileWhenDoesntExists<string, string>(path, null);
        await FileMs.WriteAllTextAsync(path, "");
    }

    //public static void WriteAllBytes(string p, IEnumerable<byte> builder)
    //{
    //    FileMs.WriteAllBytes(p, builder.ToArray());
    //}
    public static List<byte> bomUtf8 = new List<byte>([239, 187, 191]);
    public static 
#if ASYNC
        async Task
#else
    void 
#endif
    RemoveDoubleBomUtf8(string path)
    {
        var builder = (
#if ASYNC
            await
#endif
        FileMs.ReadAllBytesAsync(path)).ToList();
        var to = builder.Count > 5 ? 6 : builder.Count;
        for (var i = 3; i < to; i++)
            if (bomUtf8[i - 3] != builder[i])
                break;
        builder = builder.Skip(3).ToList();
        await FileMs.WriteAllBytesAsync(path, builder.ToArray());
    }
}