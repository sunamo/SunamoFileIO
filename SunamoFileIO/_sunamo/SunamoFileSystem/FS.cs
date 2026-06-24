namespace SunamoFileIO._sunamo.SunamoFileSystem;

internal class FS
{
    internal static string InsertBetweenFileNameAndPath(string folder, string? parentFolder, string insert)
    {
        parentFolder ??= Path.GetDirectoryName(folder)!;
        var outputFolder = Path.Combine(parentFolder, insert);
        CreateFoldersPsysicallyUnlessThere(outputFolder);
        return Path.Combine(outputFolder, Path.GetFileName(folder));
    }

    internal static void CreateFoldersPsysicallyUnlessThere(string path)
    {
        if (Directory.Exists(path)) return;

        var foldersToCreate = new List<string>
        {
            path
        };

        var currentPath = path;
        while (true)
        {
            currentPath = Path.GetDirectoryName(currentPath)!;
            // EN: TODO: This doesn't work for UWP/UAP apps because they don't have access to the whole disk
            // CZ: TODO: Toto nefunguje pro UWP/UAP aplikace protože nemají přístup k celému disku
            if (Directory.Exists(currentPath)) break;
            foldersToCreate.Add(currentPath!);
        }

        foldersToCreate.Reverse();

        foreach (var folder in foldersToCreate)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }
    }

    internal static string InsertBetweenFileNameAndExtension(string originalPath, string whatInsert)
    {
        var pathString = originalPath.ToString();

        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathString);
        string extension = Path.GetExtension(pathString);

        if (pathString.Contains('/') || pathString.Contains('\\'))
        {
            string? directoryPath = Path.GetDirectoryName(pathString);
            return Path.Combine(directoryPath!, fileNameWithoutExtension + whatInsert + extension);
        }

        return fileNameWithoutExtension + whatInsert + extension;
    }
}
