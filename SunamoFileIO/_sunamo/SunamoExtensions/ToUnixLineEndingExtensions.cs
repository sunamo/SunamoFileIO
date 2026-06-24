namespace SunamoFileIO._sunamo.SunamoExtensions;

internal static class ToUnixLineEndingExtensions
{
    internal static IList<string> ToUnixLineEnding(this IList<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i] = lines[i].ToUnixLineEnding();
        }
        return lines;
    }
}

internal static class StringExtensions
{
    internal static string ToUnixLineEnding(this string text)
    {
        return text.ReplaceLineEndings("\n");
    }
}
