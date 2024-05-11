namespace SunamoFileIO._sunamo;

using SunamoValues;
using System.Runtime.CompilerServices;

//namespace SunamoFileIO._sunamo;

internal class SHSH
{
    internal static string WrapWithBs(string commitMessage)
    {
        return WrapWithChar(commitMessage, AllChars.bs);
    }

    //    internal static Action<List<string>, int, string, string, bool> ReplaceInLine;
    //    internal static Func<string, string> WrapWithBs;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWith(string value, string h)
    {
        return h + value + h;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWithChar(string value, char v, bool _trimWrapping = false, bool alsoIfIsWhitespaceOrEmpty = true)
    {
        if (string.IsNullOrWhiteSpace(value) && !alsoIfIsWhitespaceOrEmpty)
        {
            return string.Empty;
        }

        // TODO: Make with StringBuilder, because of WordAfter and so
        return WrapWith(_trimWrapping ? value.Trim() : value, v.ToString());
    }
}
