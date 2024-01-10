namespace SunamoFileIO._sunamo;

internal class SF
{
    internal static Func<string, string, (List<string> header, List<List<string>> rows)> GetAllElementsFileAdvanced;
    internal static Action<List<string>> RemoveComments;
}
