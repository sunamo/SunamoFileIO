namespace SunamoFileIO;
internal class FileEncodingHelper
{
    /// <summary>
    /// A2 can be null
    /// </summary>
    /// <param name="files"></param>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <param name="insert"></param>
    public static
#if ASYNC
async Task
#else
void
#endif
ConvertToEncodingWorker(List<string> files, Encoding input, Encoding output, string insert = null)
    {
        foreach (var item in files)
        {
#if DEBUG

            if (item.Contains(@"src\packages\webui\src\TextArea\TextArea.tsx"))
            {
            }
#endif
            string v = null;
            if (input == null)
            {
                v =
#if ASYNC
await
#endif
TF.ReadAllText(item);
            }
            else
            {
                v =
#if ASYNC
await
#endif
            TF.ReadAllText(item, input);
            }
            var newFile = item;
            if (insert != null)
            {
                newFile = FS.InsertBetweenFileNameAndPath(item, null, insert);
            }
            await TF.WriteAllText(newFile, v, output);
        }
    }

    //    /// <summary>
    //    /// A2 can be null
    //    /// </summary>
    //    /// <param name="files"></param>
    //    /// <param name="input"></param>
    //    /// <param name="output"></param>
    //    public static
    //#if ASYNC
    //async Task
    //#else
    //    void
    //#endif
    //ConvertToEncoding(List<string> files, Encoding input, Encoding output)
    //    {
    //        await ConvertToEncodingWorker(files, input, output);
    //    }

    /// <summary>
    /// A2 can be null
    /// </summary>
    /// <param name="folder"></param>
    public static
#if ASYNC
async Task
#else
    void
#endif
ConvertToEncoding(List<string> files, Encoding input, Encoding output)
    {
        var insert = "Converted";
        await ConvertToEncodingWorker(files, input, output, insert);
    }
}
