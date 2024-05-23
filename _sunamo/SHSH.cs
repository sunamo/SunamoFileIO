namespace SunamoFileIO;


using System.Runtime.CompilerServices;

//namespace SunamoFileIO;

public class SHSH
{
    public static string WrapWithBs(string commitMessage)
    {
        return WrapWithChar(commitMessage, AllChars.bs);
    }

    //    public static Action<List<string>, int, string, string, bool> ReplaceInLine;
    //    public static Func<string, string> WrapWithBs;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WrapWith(string value, string h)
    {
        return h + value + h;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string WrapWithChar(string value, char v, bool _trimWrapping = false, bool alsoIfIsWhitespaceOrEmpty = true)
    {
        if (string.IsNullOrWhiteSpace(value) && !alsoIfIsWhitespaceOrEmpty)
        {
            return string.Empty;
        }

        // TODO: Make with StringBuilder, because of WordAfter and so
        return WrapWith(_trimWrapping ? value.Trim() : value, v.ToString());
    }
}
