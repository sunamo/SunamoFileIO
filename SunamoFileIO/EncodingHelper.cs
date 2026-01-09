namespace SunamoFileIO;

/// <summary>
/// Helper class for encoding detection and conversion operations.
/// </summary>
public class EncodingHelper
{
    /// <summary>
    /// Prints all encoding names for a given encoding (EncodingName, BodyName, HeaderName, WebName).
    /// </summary>
    /// <param name="encoding">The encoding to print names for.</param>
    /// <returns>String containing all encoding names, one per line.</returns>
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
    /// Detects encoding from the first 4 bytes (BOM - Byte Order Mark).
    /// </summary>
    /// <param name="bom">First 4 bytes of the file.</param>
    /// <param name="defaultEncoding">Default encoding to return if no BOM detected (defaults to ASCII).</param>
    /// <returns>Detected encoding or default encoding.</returns>
    public static Encoding DetectEncoding(List<byte> bom, Encoding? defaultEncoding = null)
    {
        if (defaultEncoding == null) defaultEncoding = Encoding.ASCII;

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
    /// Checks if a file is binary by detecting control characters.
    /// </summary>
    /// <param name="path">Path to the file to check.</param>
    /// <returns>True if file contains control characters (binary), false otherwise.</returns>
    public static bool IsBinary(string path)
    {
        var length = new FileInfo(path).Length;
        if (length == 0)
            return false;
        using (var stream = new StreamReader(path))
        {
            int ch;
            while ((ch = stream.Read()) != -1)
                if (IsControlChar(ch))
                    return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if a character is a control character (non-printable).
    /// </summary>
    /// <param name="characterCode">Character code to check.</param>
    /// <returns>True if character is a control character, false otherwise.</returns>
    public static bool IsControlChar(int characterCode)
    {
        return (characterCode > Chars.NUL && characterCode < Chars.BS) || (characterCode > Chars.CR && characterCode < Chars.SUB);
    }

    /// <summary>
    /// Converts bytes from detected encoding to specified destination encoding, replacing bad characters.
    /// </summary>
    /// <param name="destinationEncodingCodepage">Destination encoding codepage.</param>
    /// <param name="input">Input bytes to convert.</param>
    /// <param name="badCharsReplaceFor">Replacement string for characters that cannot be converted.</param>
    /// <returns>Converted string in destination encoding.</returns>
    public static string ConvertTo(int destinationEncodingCodepage, List<byte> input, string badCharsReplaceFor)
    {
        var sourceEncoding = DetectEncoding(input);
        var destinationEncoding = Encoding.GetEncoding(destinationEncodingCodepage, new EncoderReplacementFallback(badCharsReplaceFor),
            new DecoderReplacementFallback(badCharsReplaceFor));
        return destinationEncoding.GetString(Encoding.Convert(sourceEncoding, destinationEncoding, input.ToArray()));
    }

    /// <summary>
    /// Converts bytes to all available encodings and returns a dictionary of codepage to converted string.
    /// </summary>
    /// <param name="buffer">Bytes to convert.</param>
    /// <returns>Dictionary mapping codepage to converted string.</returns>
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
    /// Character constants for control character detection.
    /// </summary>
    public static class Chars
    {
        /// <summary>
        /// Null character (ASCII 0).
        /// </summary>
        public static readonly char NUL = (char)0;

        /// <summary>
        /// Backspace character (ASCII 8).
        /// </summary>
        public static readonly char BS = (char)8;

        /// <summary>
        /// Carriage Return character (ASCII 13).
        /// </summary>
        public static readonly char CR = (char)13;

        /// <summary>
        /// Substitute character (ASCII 26).
        /// </summary>
        public static readonly char SUB = (char)26;
    }
}