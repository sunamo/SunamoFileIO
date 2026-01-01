namespace SunamoFileIO._sunamo.SunamoStringGetLines;

/// <summary>
/// EN: String helper for splitting text into lines (handles all newline formats: \r\n, \n\r, \r, \n)
/// CZ: String helper pro rozdělení textu na řádky (zpracovává všechny formáty nových řádků: \r\n, \n\r, \r, \n)
/// </summary>
internal class SHGetLines
{
    /// <summary>
    /// EN: Splits text into lines, handling all newline formats (\r\n, \n\r, \r, \n)
    /// CZ: Rozdělí text na řádky, zpracovává všechny formáty nových řádků (\r\n, \n\r, \r, \n)
    /// </summary>
    internal static List<string> GetLines(string text)
    {
        var parts = text.Split(new string[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(parts);
        return parts;
    }

    /// <summary>
    /// EN: Splits parts by Unix newline characters (\r and \n)
    /// CZ: Rozdělí části podle Unix znaků pro nový řádek (\r a \n)
    /// </summary>
    private static void SplitByUnixNewline(List<string> lines)
    {
        SplitBy(lines, "\r");
        SplitBy(lines, "\n");
    }

    /// <summary>
    /// EN: Splits each line by specified delimiter, ensuring no double splitting
    /// CZ: Rozdělí každý řádek podle zadaného oddělovače, zajišťuje že nedojde k dvojímu rozdělení
    /// </summary>
    private static void SplitBy(List<string> lines, string delimiter)
    {
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            if (delimiter == "\r")
            {
                var rnParts = lines[i].Split(new string[] { "\r\n" }, StringSplitOptions.None);
                var nrParts = lines[i].Split(new string[] { "\n\r" }, StringSplitOptions.None);

                if (rnParts.Length > 1)
                {
                    ThrowEx.Custom("cannot contain any \r\n, pass already split by this pattern");
                }
                else if (nrParts.Length > 1)
                {
                    ThrowEx.Custom("cannot contain any \n\r, pass already split by this pattern");
                }
            }

            var splitParts = lines[i].Split(new string[] { delimiter }, StringSplitOptions.None);

            if (splitParts.Length > 1)
            {
                InsertOnIndex(lines, splitParts.ToList(), i);
            }
        }
    }

    /// <summary>
    /// EN: Inserts new parts at specified index, removing original element
    /// CZ: Vloží nové části na zadaný index, odstraní původní element
    /// </summary>
    private static void InsertOnIndex(List<string> lines, List<string> newParts, int index)
    {
        newParts.Reverse();

        lines.RemoveAt(index);

        foreach (var item in newParts)
        {
            lines.Insert(index, item);
        }
    }
}