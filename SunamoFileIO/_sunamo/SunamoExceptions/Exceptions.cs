namespace SunamoFileIO._sunamo.SunamoExceptions;

internal sealed partial class Exceptions
{
    #region Other
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

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

    internal static void TypeAndMethodName(string stackTraceLine, out string type, out string methodName)
    {
        var trimmedLine = stackTraceLine.Split("at ")[1].Trim();
        var fullMethodPath = trimmedLine.Split("(")[0];
        var parts = fullMethodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }

    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase is null)
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
    internal static string? Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }
    #endregion

    internal static string? DirectoryWasntFound(string before, string directory)
    {
        return !Directory.Exists(directory)
        ? CheckBefore(before) + " Directory" + " " + directory +
        " wasn't found."
        : null;
    }
}
