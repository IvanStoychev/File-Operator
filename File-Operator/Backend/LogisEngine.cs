using Backend.Models;
using Backend.Utility;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace Backend
{
    /// <summary>
    /// Main cogitator housing the processing logic of this machine spirit.
    /// </summary>
    public class LogisEngine
    {
        /// <summary>
        /// File system to operate on.
        /// </summary>
        readonly IFileSystem fileSystem;

        /// <summary>
        /// Initializes a new instance using the real file system.
        /// </summary>
        public LogisEngine()
        {
            fileSystem = new FileSystem();
        }

        /// <summary>
        /// Initilizes a new instance using the provided file system.
        /// </summary>
        /// <param name="fileSystem">Implementation of <see cref="IFileSystem"/> to use for file operations.</param>
        public LogisEngine(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Compares the files and folders in the two given folders and returns an object with data on the differences.
        /// A difference is a file or folder that exists only in one of the "source" or "target" folders.
        /// </summary>
        /// <param name="sourceFolderPath">Original folder to use for comparison.</param>
        /// <param name="targetFolderPath">Folder to compare the original to.</param>
        /// <returns>A <see cref="FolderDiffData"/> instance, containing data on the differences between the folders.</returns>
        public FolderDiffData CompareFilesInFolders(string sourceFolderPath, string targetFolderPath)
        {
            var sourceDirsEnum = fileSystem.Directory.EnumerateDirectories(sourceFolderPath, "*", SearchOption.AllDirectories);
            var targetDirsEnum = fileSystem.Directory.EnumerateDirectories(targetFolderPath, "*", SearchOption.AllDirectories);
            var sourceFilesEnum = fileSystem.Directory.EnumerateFiles(sourceFolderPath, "*", SearchOption.AllDirectories);
            var targetFilesEnum = fileSystem.Directory.EnumerateFiles(targetFolderPath, "*", SearchOption.AllDirectories);

            sourceDirsEnum = StringOperator.TrimStartStrings(sourceDirsEnum, sourceFolderPath);
            targetDirsEnum = StringOperator.TrimStartStrings(targetDirsEnum, targetFolderPath);
            sourceFilesEnum = StringOperator.TrimStartStrings(sourceFilesEnum, sourceFolderPath);
            targetFilesEnum = StringOperator.TrimStartStrings(targetFilesEnum, targetFolderPath);

            var missingDirsEnum = sourceDirsEnum.Except(targetDirsEnum);
            var extraDirsEnum = targetDirsEnum.Except(sourceDirsEnum);
            var missingFilesEnum = sourceFilesEnum.Except(targetFilesEnum);
            var extraFilesEnum = targetFilesEnum.Except(sourceFilesEnum);

            var missingDirsArray = missingDirsEnum.OrderBy(x => x).ToArray();
            var extraDirsArray = extraDirsEnum.OrderBy(x => x).ToArray();
            var missingFilesArray = missingFilesEnum.OrderBy(x => x).ToArray();
            var extraFilesArray = extraFilesEnum.OrderBy(x => x).ToArray();

            FolderDiffData folderDiffData = new(missingDirsArray, extraDirsArray, missingFilesArray, extraFilesArray);

            return folderDiffData;
        }
    }
}
