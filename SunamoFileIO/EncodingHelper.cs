namespace SunamoFileIO;

public class EncodingHelper
{
    public static string PrintNamesForEncodingAsIsInSheet(Encoding encoding)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(encoding.EncodingName);
        stringBuilder.AppendLine(encoding.BodyName);
        stringBuilder.AppendLine(encoding.HeaderName);
        stringBuilder.AppendLine(encoding.WebName);

        return stringBuilder.ToString();
    }

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

    public static bool IsControlChar(int characterCode)
    {
        return (characterCode > Chars.NUL && characterCode < Chars.BS) || (characterCode > Chars.CR && characterCode < Chars.SUB);
    }

    public static string ConvertTo(int destinationEncodingCodepage, List<byte> input, string badCharsReplaceFor)
    {
        var sourceEncoding = DetectEncoding(input);
        var destinationEncoding = Encoding.GetEncoding(destinationEncodingCodepage, new EncoderReplacementFallback(badCharsReplaceFor),
            new DecoderReplacementFallback(badCharsReplaceFor));
        return destinationEncoding.GetString(Encoding.Convert(sourceEncoding, destinationEncoding, input.ToArray()));
    }

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

    public static class Chars
    {
        public static readonly char NUL = (char)0;

        public static readonly char BS = (char)8;

        public static readonly char CR = (char)13;

        public static readonly char SUB = (char)26;
    }
}
