namespace SunamoFileIO._sunamo.SunamoFileSystem;

/// <summary>
/// EN: File system helper methods for path manipulation and folder creation
/// CZ: Pomocné metody pro souborový systém - manipulace s cestami a vytváření složek
/// </summary>
internal class FS
{
    /// <summary>
    /// EN: Inserts a folder name between parent path and file name
    /// CZ: Vloží název složky mezi nadřazenou cestu a název souboru
    /// </summary>
    internal static string InsertBetweenFileNameAndPath(string folder, string parentFolder, string insert)
    {
        if (parentFolder == null) parentFolder = Path.GetDirectoryName(folder);
        var outputFolder = Path.Combine(parentFolder, insert);
        CreateFoldersPsysicallyUnlessThere(outputFolder);
        return Path.Combine(outputFolder, Path.GetFileName(folder));
    }

    /// <summary>
    /// EN: Creates all necessary parent folders if they don't exist
    /// CZ: Vytvoří všechny potřebné nadřazené složky pokud neexistují
    /// </summary>
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
            currentPath = Path.GetDirectoryName(currentPath);
            // EN: TODO: This doesn't work for UWP/UAP apps because they don't have access to the whole disk
            // CZ: TODO: Toto nefunguje pro UWP/UAP aplikace protože nemají přístup k celému disku
            if (Directory.Exists(currentPath)) break;
            foldersToCreate.Add(currentPath);
        }

        foldersToCreate.Reverse();

        foreach (var folder in foldersToCreate)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }
    }

    /// <summary>
    /// EN: Inserts text between file name and extension (e.g., "file.txt" + "_backup" = "file_backup.txt")
    /// CZ: Vloží text mezi název souboru a příponu (např. "soubor.txt" + "_zaloha" = "soubor_zaloha.txt")
    /// </summary>
    internal static string InsertBetweenFileNameAndExtension(string originalPath, string whatInsert)
    {
        var pathString = originalPath.ToString();

        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathString);
        string extension = Path.GetExtension(pathString);

        if (pathString.Contains('/') || pathString.Contains('\\'))
        {
            string directoryPath = Path.GetDirectoryName(pathString);
            return Path.Combine(directoryPath, fileNameWithoutExtension + whatInsert + extension);
        }

        return fileNameWithoutExtension + whatInsert + extension;
    }
}