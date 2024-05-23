namespace SunamoFileIO;

public class PH
{
    //public static Func<string, bool> IsAlreadyRunning;

    public static bool IsAlreadyRunning(string name)
    {
        return Process.GetProcessesByName(name).Select(d => d.ProcessName).ToList().Count > 1;
    }

}
