namespace SunamoFileIO;

/// <summary>
/// Represents the encoding helper type.
/// </summary>
public class EncodingHelper
{
    /// <summary>
    /// Print names for encoding as is in sheet.
    /// </summary>
    /// <param name="encoding">The encoding.</param>
    /// <returns>The print names for encoding as is in sheet result.</returns>
    public static string PrintNamesForEncodingAsIsInSheet(Encoding encoding)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(encoding.EncodingName);
        stringBuilder.AppendLine(encoding.BodyName);
        stringBuilder.AppendLine(encoding.HeaderName);
        stringBuilder.AppendLine(encoding.WebName);

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Detect encoding.
    /// </summary>
    /// <param name="bom">The bom.</param>
    /// <param name="defaultEncoding">The default encoding.</param>
    /// <returns>The detect encoding result.</returns>
    public static Encoding DetectEncoding(List<byte> bom, Encoding? defaultEncoding = null)
    {
        defaultEncoding ??= Encoding.ASCII;

        if (bom.Count > 3)
        {
            var first = bom[0];
            var second = bom[1];
            var third = bom[2];

#pragma warning disable SYSLIB0001
            if (first == 0x2b && second == 0x2f && third == 0x76)
                return Encoding.UTF7;
#pragma warning restore SYSLIB0001
            if (first == 0xef && second == 0xbb && third == 0xbf)
                return Encoding.UTF8;
            if (first == 0xff && second == 0xfe)
                return Encoding.Unicode;
            if (first == 0xfe && second == 0xff)
                return Encoding.BigEndianUnicode;
            if (first == 0 && second == 0 && third == 0xfe && bom[3] == 0xff)
                return Encoding.UTF32;
        }

        return defaultEncoding;
    }

    /// <summary>
    /// Is binary.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The is binary result.</returns>
    public static bool IsBinary(string path)
    {
        var length = new FileInfo(path).Length;
        if (length == 0)
            return false;
        using var stream = new StreamReader(path);
        int ch;
        while ((ch = stream.Read()) != -1)
            if (IsControlChar(ch))
                return true;

        return false;
    }

    /// <summary>
    /// Is control char.
    /// </summary>
    /// <param name="characterCode">The character code.</param>
    /// <returns>The is control char result.</returns>
    public static bool IsControlChar(int characterCode)
    {
        return (characterCode > Chars.NUL && characterCode < Chars.BS) || (characterCode > Chars.CR && characterCode < Chars.SUB);
    }

    /// <summary>
    /// Convert to.
    /// </summary>
    /// <param name="destinationEncodingCodepage">The destination encoding codepage.</param>
    /// <param name="input">The input.</param>
    /// <param name="badCharsReplaceFor">The bad chars replace for.</param>
    /// <returns>The convert to result.</returns>
    public static string ConvertTo(int destinationEncodingCodepage, List<byte> input, string badCharsReplaceFor)
    {
        var sourceEncoding = DetectEncoding(input);
        var destinationEncoding = Encoding.GetEncoding(destinationEncodingCodepage, new EncoderReplacementFallback(badCharsReplaceFor),
            new DecoderReplacementFallback(badCharsReplaceFor));
        return destinationEncoding.GetString(Encoding.Convert(sourceEncoding, destinationEncoding, input.ToArray()));
    }

    /// <summary>
    /// Convert to all available encodings.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <returns>The convert to all available encodings result.</returns>
    public static Dictionary<int, string> ConvertToAllAvailableEncodings(byte[] buffer)
    {
        var result = new Dictionary<int, string>();
        Encoding? encoding = null;
        var encodings = Encoding.GetEncodings();
        foreach (var item in encodings)
        {
            encoding = item.GetEncoding();
            result.Add(encoding.CodePage, Encoding.UTF8.GetString(Encoding.Convert(encoding, Encoding.UTF8, buffer)));
        }

        encoding = Encoding.GetEncoding("latin1");
        result.Remove(encoding.CodePage);
        result.Add(encoding.CodePage, Encoding.UTF8.GetString(Encoding.Convert(encoding, Encoding.UTF8, buffer)));

        return result;
    }

    /// <summary>
    /// Represents the chars type.
    /// </summary>
    public static class Chars
    {
        /// <summary>
        /// The NUL.
        /// </summary>
        public static readonly char NUL = (char)0;

        /// <summary>
        /// The BS.
        /// </summary>
        public static readonly char BS = (char)8;

        /// <summary>
        /// The CR.
        /// </summary>
        public static readonly char CR = (char)13;

        /// <summary>
        /// The SUB.
        /// </summary>
        public static readonly char SUB = (char)26;
    }
}
