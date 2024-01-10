namespace SunamoFileIO;


public partial class TF
{
    public static
#if ASYNC
    async Task
#else
void
#endif
    Replace(string pathCsproj, string to, string from)
    {
        string content =
#if ASYNC
        await
#endif
        TF.ReadAllText(pathCsproj);
        content = content.Replace(to, from);

#if ASYNC
        await
#endif
        TF.WriteAllText(pathCsproj, content);
    }

    public static
#if ASYNC
    async Task
#else
void
#endif
    PureFileOperationProcessEveryLine(string f, Func<string, string> transformHtmlToMetro4, string insertBetweenFilenameAndExtension)
    {
        var content =
#if ASYNC
        await
#endif
        TF.ReadAllText(f);
        //content = transformHtmlToMetro4.Invoke(content);

#if ASYNC
        await
#endif
        TF.WriteAllText(FS.InsertBetweenFileNameAndExtension(f, insertBetweenFilenameAndExtension), content);
    }


    public static
#if ASYNC
    async Task
#else
void
#endif
    PureFileOperationWithArg(string f, Func<string, string, string> transformHtmlToMetro4, string arg)
    {
        var content =
#if ASYNC
        await
#endif
        TF.ReadAllText(f);

#if DEBUG
        if (f.Contains(@"\scz.sln"))
        {

        }
#endif

        var content2 = transformHtmlToMetro4.Invoke(content, arg);
        if (content.Trim() != content2.Trim())
        {
            TF.WriteAllText(f, content2);
        }
    }



    public static
#if ASYNC
    async Task
#else
void
#endif
    PureFileOperation(string f, Func<string, string> transformHtmlToMetro4, string insertBetweenFilenameAndExtension)
    {
        var content =
#if ASYNC
        await
#endif
        TF.ReadAllText(f);
        content = transformHtmlToMetro4.Invoke(content);

#if ASYNC
        await
#endif
        TF.WriteAllText(FS.InsertBetweenFileNameAndExtension(f, insertBetweenFilenameAndExtension), content);
    }





    public static
#if ASYNC
    async Task
#else
void
#endif
    PureFileOperation(string f, Func<string, string> transformHtmlToMetro4)
    {
        var content = (
#if ASYNC
        await
#endif
        TF.ReadAllText(f)).Trim();
        var content2 = transformHtmlToMetro4.Invoke(content);

        if (String.Compare(content, content2) != 0)
        {
            //TF.SaveFile(content, CompareFilesPaths.GetFile(CompareExt.cs, 1));
            //TF.SaveFile(content2, CompareFilesPaths.GetFile(CompareExt.cs, 2));

#if ASYNC
            await
#endif
            TF.WriteAllText(f, content2);
        }
    }












    /// <summary>
    /// StreamReader is derived from TextReader
    /// </summary>
    /// <param name="file"></param>
    public static StreamReader TextReader(string file)
    {
        return File.OpenText(file);
    }

    public static
#if ASYNC
    async Task
#else
void
#endif
    WriteAllText(string file, StringBuilder sb)
    {

#if ASYNC
        await
#endif
        WriteAllText(file, sb.ToString());
    }

    public static
#if ASYNC
    async Task
#else
void
#endif
    WriteAllText(string file, string content, Encoding encoding)
    {

#if ASYNC
        await
#endif
        WriteAllText<string, string>(file, content, encoding, null);
    }




}
