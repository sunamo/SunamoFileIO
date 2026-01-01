namespace SunamoFileIO._sunamo.SunamoExtensions;

/// <summary>
/// EN: Extension methods for converting line endings to Unix format (LF)
/// CZ: Rozšiřující metody pro konverzi konců řádků do Unix formátu (LF)
/// </summary>
internal static class ToUnixLineEndingExtensions
{
    /// <summary>
    /// EN: Converts all line endings in list of strings to Unix format (LF)
    /// CZ: Převede všechny konce řádků v seznamu stringů do Unix formátu (LF)
    /// </summary>
    internal static IList<string> ToUnixLineEnding(this IList<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i] = lines[i].ToUnixLineEnding();
        }
        return lines;
    }
}

/// <summary>
/// EN: Extension methods for string manipulation
/// CZ: Rozšiřující metody pro manipulaci se stringy
/// </summary>
internal static class StringExtensions
{
    /// <summary>
    /// EN: Converts line endings in string to Unix format (LF)
    /// CZ: Převede konce řádků ve stringu do Unix formátu (LF)
    /// </summary>
    internal static string ToUnixLineEnding(this string text)
    {
        return text.ReplaceLineEndings("\n");
    }
}