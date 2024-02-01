namespace SunamoFileIO._sunamo;

internal class PH
{
    //internal static Func<string, bool> IsAlreadyRunning;

    internal static bool IsAlreadyRunning(string name)
    {
        return Process.GetProcessesByName(name).Select(d => d.ProcessName).ToList().Count > 1;
    }

}
