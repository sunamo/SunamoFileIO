namespace SunamoFileIO._sunamo;

internal class SH
{
    internal static Func<List<string>, string> JoinNL;
    internal static Action<List<string>, int, string, string, bool> ReplaceInLine;
    //internal static Func<string, string, List<string>> Split;
    internal static Func<string, string> WrapWithBs;
    internal static Func<string, List<string>> GetLines;
}
