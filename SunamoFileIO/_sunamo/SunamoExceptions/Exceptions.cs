namespace SunamoFileIO._sunamo.SunamoExceptions;

// © www.sunamo.cz. All Rights Reserved.

/// <summary>
/// EN: Helper class for formatting exception messages
/// CZ: Pomocná třída pro formátování chybových zpráv
/// </summary>
internal sealed partial class Exceptions
{
    #region Other
    /// <summary>
    /// EN: Adds colon and space after prefix if not empty
    /// CZ: Přidá dvojtečku a mezeru za prefix pokud není prázdný
    /// </summary>
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    /// <summary>
    /// EN: Gets the place where exception occurred from stack trace
    /// CZ: Získá místo kde došlo k výjimce ze stack trace
    /// </summary>
    internal static Tuple<string, string, string> PlaceOfException(bool fillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var currentIndex = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; currentIndex < lines.Count; currentIndex++)
        {
            var line = lines[currentIndex];
            if (fillAlsoFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out type, out methodName);
                    fillAlsoFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }

    /// <summary>
    /// EN: Extracts type name and method name from stack trace line
    /// CZ: Extrahuje název typu a metody z řádku stack trace
    /// </summary>
    internal static void TypeAndMethodName(string stackTraceLine, out string type, out string methodName)
    {
        var trimmedLine = stackTraceLine.Split("at ")[1].Trim();
        var fullMethodPath = trimmedLine.Split("(")[0];
        var parts = fullMethodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }

    /// <summary>
    /// EN: Gets the name of calling method from stack trace
    /// CZ: Získá název volající metody ze stack trace
    /// </summary>
    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region IsNullOrWhitespace
    internal readonly static StringBuilder AdditionalInfoInnerStringBuilder = new();
    internal readonly static StringBuilder AdditionalInfoStringBuilder = new();
    #endregion

    #region OnlyReturnString
    /// <summary>
    /// EN: Creates custom exception message with optional prefix
    /// CZ: Vytvoří vlastní chybovou zprávu s volitelným prefixem
    /// </summary>
    internal static string? Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }
    #endregion

    /// <summary>
    /// EN: Returns error message if directory doesn't exist, null otherwise
    /// CZ: Vrátí chybovou zprávu pokud složka neexistuje, jinak null
    /// </summary>
    internal static string? DirectoryWasntFound(string before, string directory)
    {
        return !Directory.Exists(directory)
        ? CheckBefore(before) + " Directory" + " " + directory +
        " wasn't found."
        : null;
    }
}