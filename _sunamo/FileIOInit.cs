namespace SunamoFileIO;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using FS2 = SunamoFileIO._sunamo.FS;
//using SF2 = SunamoFileIO._sunamo.SF;
//using SH2 = SunamoFileIO._sunamo.SH;

//namespace SunamoFileIO._sunamo
//{
//    internal class FileIOInit
//    {
//        internal static void Fs(Func<string, bool> ExistsFile, Func<string, long> GetFileSize, Func<string, string> GetDirectoryName, Action<string> CreateUpfoldersPsysicallyUnlessThere, Action<string> CreateFoldersPsysicallyUnlessThere, Func<string, string> GetFileNameWithoutExtension, Func<string, string, string> InsertBetweenFileNameAndExtension, Func<string, string, bool?, List<string>> GetFilesWithoutArgs)
//        {
//            FS2.ExistsFile = ExistsFile;
//            FS2.GetFileSize = GetFileSize;
//            FS2.GetDirectoryName = GetDirectoryName;
//            FS2.CreateUpfoldersPsysicallyUnlessThere = CreateUpfoldersPsysicallyUnlessThere;
//            FS2.CreateFoldersPsysicallyUnlessThere = CreateFoldersPsysicallyUnlessThere;
//            FS2.GetFileNameWithoutExtension = GetFileNameWithoutExtension;
//            FS2.InsertBetweenFileNameAndExtension = InsertBetweenFileNameAndExtension;
//            FS2.GetFilesWithoutArgs = GetFilesWithoutArgs;
//        }

//        internal static void Ph(Func<string, bool> IsAlreadyRunning)
//        {
//            SunamoFileIO._sunamo.PH.IsAlreadyRunning = IsAlreadyRunning;
//        }

//        internal static void Sf(Func<string, string, (List<string> header, List<List<string>> rows)> GetAllElementsFileAdvanced, Action<List<string>> RemoveComments)
//        {
//            SF2.GetAllElementsFileAdvanced = GetAllElementsFileAdvanced;
//            SF2.RemoveComments = RemoveComments;
//        }

//        internal static void Sh(Action<List<string>, int, string, string, bool> ReplaceInLine, Func<string, string> WrapWithBs)
//        {
//            SH2.ReplaceInLine = ReplaceInLine;
//            SH2.WrapWithBs = WrapWithBs;
//        }

//        internal static void ShReplace(Action<List<string>, int, string, string, bool> ReplaceInLine)
//        {
//            SunamoFileIO._sunamo.SHReplace.ReplaceInLine = ReplaceInLine;
//        }
//    }
//}
