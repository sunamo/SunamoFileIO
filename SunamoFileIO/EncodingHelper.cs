namespace SunamoFileIO;

public class EncodingHelper
{
    public static string PrintNamesForEncodingAsIsInSheet(Encoding e)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(e.EncodingName);
        stringBuilder.AppendLine(e.BodyName);
        stringBuilder.AppendLine(e.HeaderName);
        stringBuilder.AppendLine(e.WebName);

        return stringBuilder.ToString();
    }

    /// <summary>
    ///     First 4 bytes
    /// </summary>
    /// <param name="bom"></param>
    public static Encoding DetectEncoding(List<byte> bom, Encoding def = null)
    {
        if (def == null) def = Encoding.ASCII;

        if (bom.Count > 3)
        {
            var first = bom[0];
            var second = bom[1];
            var third = bom[2];
            // Analyze the BOM
            if (first == 0x2b && second == 0x2f && third == 0x76)
                return Encoding.UTF7;
            if (first == 0xef && second == 0xbb && third == 0xbf)
                return Encoding.UTF8;
            if (first == 0xff && second == 0xfe)
                return Encoding.Unicode; //UTF-16LE
            if (first == 0xfe && second == 0xff)
                return Encoding.BigEndianUnicode; //UTF-16BE
            if (first == 0 && second == 0 && third == 0xfe && bom[3] == 0xff)
                return Encoding.UTF32;
        }

        return def;
    }

    private static Dictionary<string, bool> TestBinaryFile(string folderPath)
    {
        var output = new Dictionary<string, bool>();
        foreach (var filePath in Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories))
            output.Add(filePath, isBinary(filePath));

        return output;
    }

    public static bool isBinary(string path)
    {
        var length = new FileInfo(path).Length;
        if (length == 0)
            return false;
        using (var stream = new StreamReader(path))
        {
            int ch;
            while ((ch = stream.Read()) != -1)
                if (isControlChar(ch))
                    return true;
        }

        return false;
    }

    public static bool isControlChar(int ch)
    {
        return (ch > Chars.NUL && ch < Chars.BS) || (ch > Chars.CR && ch < Chars.SUB);
    }

    public static string ConvertTo(int destEncCodepage, List<byte> input, string badCharsReplaceFor)
    {
        var srcEnc = DetectEncoding(input);
        var destEnc = Encoding.GetEncoding(destEncCodepage, new EncoderReplacementFallback(badCharsReplaceFor),
            new DecoderReplacementFallback(badCharsReplaceFor));
        return destEnc.GetString(Encoding.Convert(srcEnc, destEnc, input.ToArray()));
    }

    public static Dictionary<int, string> ConvertToAllAvailableEncodings(byte[] buffer)
    {
        var value = new Dictionary<int, string>();
        Encoding e = null;
        var encs = Encoding.GetEncodings();
        foreach (var item in encs)
        {
            e = item.GetEncoding();
            value.Add(e.CodePage, Encoding.UTF8.GetString(Encoding.Convert(e, Encoding.UTF8, buffer)));
        }

        e = Encoding.GetEncoding("latin1");
        value.Remove(e.CodePage);
        value.Add(e.CodePage, Encoding.UTF8.GetString(Encoding.Convert(e, Encoding.UTF8, buffer)));
        //using (System.IO.FileStream output = new System.IO.FileStream(outFileName,
        //                                     System.IO.FileMode.Create,
        //                                     System.IO.FileAccess.Write))
        //{
        //    output.Write(converted, 0, converted.Length);
        //}
        return value;
    }

    public static class Chars
    {
        public static char NUL = (char)0; // Null char
        public static char BS = (char)8; // Back Space
        public static char CR = (char)13; // Carriage Return
        public static char SUB = (char)26; // Substitute
    }
    //public static string PureBytesOperation(Func<List<byte>, List<byte>> b, string text)
    //{
    //    var bytes = BTS.ConvertFromUtf8ToBytes(text);
    //    bytes = b.Invoke(bytes);
    //    return BTS.ConvertFromBytesToUtf8(bytes);
    //}
}