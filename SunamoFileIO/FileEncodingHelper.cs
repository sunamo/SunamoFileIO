namespace SunamoFileIO;

internal class FileEncodingHelper
{
    internal static
async Task
ConvertToEncodingWorker(List<string> files, Encoding? inputEncoding, Encoding outputEncoding, string? filenameInsert = null)
    {
        foreach (var item in files)
        {
            string? content = null;
            if (inputEncoding == null)
            {
                content =
                    await TF.ReadAllText(item);
            }
            else
            {
                content =
                    await TF.ReadAllText(item, inputEncoding);
            }
            var newFile = item;
            if (filenameInsert != null)
            {
                newFile = FS.InsertBetweenFileNameAndPath(item, null!, filenameInsert);
            }
            await TF.WriteAllText(newFile, content, outputEncoding);
        }
    }

    internal static
async Task
ConvertToEncoding(List<string> files, Encoding? inputEncoding, Encoding outputEncoding)
    {
        var insert = "Converted";
        await ConvertToEncodingWorker(files, inputEncoding, outputEncoding, insert);
    }
}
