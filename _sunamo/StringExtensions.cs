namespace SunamoFileIO;

public static class StringExtensions
{
    public static string ToUnixLineEnding(this string s)
    {
        return s.ReplaceLineEndings(Consts.nl);
    }
}
