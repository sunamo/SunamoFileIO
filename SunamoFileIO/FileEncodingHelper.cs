namespace SunamoFileIO;

/// <summary>
/// Helper class for converting file encodings.
/// </summary>
internal class FileEncodingHelper
{
    /// <summary>
    /// Converts files from input encoding to output encoding with optional filename insert.
    /// </summary>
    /// <param name="files">List of file paths to convert.</param>
    /// <param name="inputEncoding">Source encoding (can be null to auto-detect).</param>
    /// <param name="outputEncoding">Target encoding.</param>
    /// <param name="filenameInsert">Optional string to insert into filename (null to overwrite original).</param>
    internal static
#if ASYNC
async Task
#else
void
#endif
ConvertToEncodingWorker(List<string> files, Encoding inputEncoding, Encoding outputEncoding, string filenameInsert = null)
    {
        foreach (var item in files)
        {
            string content = null;
            if (inputEncoding == null)
            {
                content =
#if ASYNC
await
#endif
TF.ReadAllText(item);
            }
            else
            {
                content =
#if ASYNC
await
#endif
            TF.ReadAllText(item, inputEncoding);
            }
            var newFile = item;
            if (filenameInsert != null)
            {
                newFile = FS.InsertBetweenFileNameAndPath(item, null, filenameInsert);
            }
            await TF.WriteAllText(newFile, content, outputEncoding);
        }
    }

    /// <summary>
    /// Converts files from input encoding to output encoding, creating new files with "Converted" inserted in filename.
    /// </summary>
    /// <param name="files">List of file paths to convert.</param>
    /// <param name="inputEncoding">Source encoding (can be null to auto-detect).</param>
    /// <param name="outputEncoding">Target encoding.</param>
    internal static
#if ASYNC
async Task
#else
    void
#endif
ConvertToEncoding(List<string> files, Encoding inputEncoding, Encoding outputEncoding)
    {
        var insert = "Converted";
        await ConvertToEncodingWorker(files, inputEncoding, outputEncoding, insert);
    }
}