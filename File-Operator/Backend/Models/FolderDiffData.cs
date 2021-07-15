using System.Linq;
using System.Text;

namespace Backend.Models
{
    /// <summary>
    /// Contains information about the differences between two folders.
    /// </summary>
    public class FolderDiffData
    {
        /// <summary>
        /// String representation of the directories not found in the target folder.
        /// </summary>
        public string MissingDirectoriesText;

        /// <summary>
        /// String representation of the directories found only in the target folder.
        /// </summary>
        public string ExtraDirectoriesText;

        /// <summary>
        /// String representation of the files not found in the target folder.
        /// </summary>
        public string MissingFilesText;

        /// <summary>
        /// String representation of the files found only in the target folder.
        /// </summary>
        public string ExtraFilesText;

        /// <summary>
        /// Directories in "source" folder that do not exist in "target" folder.
        /// </summary>
        readonly string[] MissingDirectoriesArray;

        /// <summary>
        /// Directories in "target" folder that do not exist in "source" folder.
        /// </summary>
        readonly string[] ExtraDirectoriesArray;

        /// <summary>
        /// Files in "source" folder that do not exist in "target" folder.
        /// </summary>
        readonly string[] MissingFilesArray;

        /// <summary>
        /// Files in "target" folder that do not exist in "source" folder.
        /// </summary>
        readonly string[] ExtraFilesArray;

        /// <summary>
        /// Initialises a new instance with the provided data.
        /// </summary>
        /// <param name="missingDirectoriesArray">Directories in "target" folder that do not exist in "source" folder.</param>
        /// <param name="extraDirectoriesArray">Directories in "source" folder that do not exist in "target" folder.</param>
        /// <param name="missingFilesArray">Files in "source" folder that do not exist in "target" folder.</param>
        /// <param name="extraFilesArray">Files in "target" folder that do not exist in "source" folder.</param>
        public FolderDiffData(string[] missingDirectoriesArray, string[] extraDirectoriesArray, string[] missingFilesArray, string[] extraFilesArray)
        {
            MissingDirectoriesArray = missingDirectoriesArray;
            ExtraDirectoriesArray = extraDirectoriesArray;
            MissingFilesArray = missingFilesArray;
            ExtraFilesArray = extraFilesArray;

            MissingDirectoriesText = BuildMissingDirectoriesText(MissingDirectoriesArray);
            ExtraDirectoriesText = BuildExtraDirectoriesText(ExtraDirectoriesArray);
            MissingFilesText = BuildMissingFilesText(MissingFilesArray);
            ExtraFilesText = BuildExtraFilesText(ExtraFilesArray);
        }

        /// <summary>
        /// Tests whether the current instance and the given <paramref name="obj"/> are equal.
        /// </summary>
        /// <param name="obj">An instance of <see cref="FolderDiffData"/> to test equality against.</param>
        /// <returns><see cref="bool"/> indicating whether the instances are equal.</returns>
        public bool Equals(FolderDiffData obj)
        {
            if (obj is null)
                return false;

            if (MissingDirectoriesArray is null || ExtraDirectoriesArray is null
                || MissingFilesArray is null || ExtraFilesArray is null)
                return false;

            if (MissingDirectoriesArray.Length != obj.MissingDirectoriesArray.Length
                || ExtraDirectoriesArray.Length != obj.ExtraDirectoriesArray.Length
                || MissingFilesArray.Length != obj.MissingFilesArray.Length
                || ExtraFilesArray.Length != obj.ExtraFilesArray.Length)
                return false;

            if (!Enumerable.SequenceEqual(MissingDirectoriesArray, obj.MissingDirectoriesArray)
                || !Enumerable.SequenceEqual(ExtraDirectoriesArray, obj.ExtraDirectoriesArray)
                || !Enumerable.SequenceEqual(MissingFilesArray, obj.MissingFilesArray)
                || !Enumerable.SequenceEqual(ExtraFilesArray, obj.ExtraFilesArray))
                return false;

            return true;
        }

        /// <summary>
        /// Uses information from the given <paramref name="missingDirectoriesArray"/> to build a human-readable
        /// text, displaying the directories, missing in the "target" folder.
        /// </summary>
        /// <param name="missingDirectoriesArray">String of directory relative paths that are not found in the "target" folder.</param>
        /// <returns>String representation of each member of <paramref name="missingDirectoriesArray"/> separated by newlines.</returns>
        string BuildMissingDirectoriesText(string[] missingDirectoriesArray)
        {
            StringBuilder missingDirsStrB = new();

            if (missingDirectoriesArray.Length > 0)
            {
                missingDirsStrB.AppendLine("Directories not present in target folder:");
                foreach (var line in missingDirectoriesArray)
                    missingDirsStrB.AppendLine(line);
            }

            return missingDirsStrB.ToString();
        }

        /// <summary>
        /// Uses information from the given <paramref name="extraDirectoriesArray"/> to build a human-readable
        /// text, displaying the directories, only found in the "target" folder.
        /// </summary>
        /// <param name="extraDirectoriesArray">String of directory relative paths that are only found in the "target" folder.</param>
        /// <returns>String representation of each member of <paramref name="extraDirectoriesArray"/> separated by newlines.</returns>

        string BuildExtraDirectoriesText(string[] extraDirectoriesArray)
        {
            StringBuilder extraDirsStrB = new();

            if (extraDirectoriesArray.Length > 0)
            {
                extraDirsStrB.AppendLine("Directories present in target but not in source folder:");
                foreach (var line in extraDirectoriesArray)
                    extraDirsStrB.AppendLine(line);
            }

            return extraDirsStrB.ToString();
        }

        /// <summary>
        /// Uses information from the given <paramref name="missingFilesArray"/> to build a human-readable
        /// text, displaying the files, missing in the "target" folder.
        /// </summary>
        /// <param name="missingFilesArray">String of file relative paths that are not found in the "target" folder.</param>
        /// <returns>String representation of each member of <paramref name="missingFilesArray"/> separated by newlines.</returns>
        string BuildMissingFilesText(string[] missingFilesArray)
        {
            StringBuilder missingfilesStrB = new();

            if (missingFilesArray.Length > 0)
            {
                missingfilesStrB.AppendLine("Files not present in target folder:");
                foreach (var line in missingFilesArray)
                    missingfilesStrB.AppendLine(line);
            }

            return missingfilesStrB.ToString();
        }

        /// <summary>
        /// Uses information from the given <paramref name="extraFilesArray"/> to build a human-readable
        /// text, displaying the files, only found in the "target" folder.
        /// </summary>
        /// <param name="extraFilesArray">String of file relative paths that are only found in the "target" folder.</param>
        /// <returns>String representation of each member of <paramref name="extraFilesArray"/> separated by newlines.</returns>
        string BuildExtraFilesText(string[] extraFilesArray)
        {
            StringBuilder extraFilesStrB = new();

            if (extraFilesArray.Length > 0)
            {
                extraFilesStrB.AppendLine("Files present in target but not in source folder:");
                foreach (var line in extraFilesArray)
                    extraFilesStrB.AppendLine(line);
            }

            return extraFilesStrB.ToString();
        }
    }
}
