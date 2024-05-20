using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoFileIO;
internal class FileText
{
    public async Task<string> ReadAllText(string path)
    {
        return await File.ReadAllTextAsync(path);
    }

    public async Task WriteAllText(string path, string c)
    {
        await File.WriteAllTextAsync(path, c);
    }
}
