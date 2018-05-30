using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Operator
{
    public static class FileOperator
    {
        /// <summary>
        /// Calls a method on every item in a root directory.
        /// </summary>
        /// <param name="root">The root directory whose files and/or folders to call the method on.</param>
        /// <param name="target">Specify the target of the iteration - only files, only directories or both.</param>
        /// <param name="method">The method to call on each item.</param>
        public static void IterateFolder(DirectoryInfo root, IterateTarget target, Action<string> method)
        {
            if (target == IterateTarget.Files || target == IterateTarget.FilesAndFolders)
            {
                foreach (var file in root.GetFiles())
                {
                    method(file.FullName);
                }
            }

            if (target == IterateTarget.Folders || target == IterateTarget.FilesAndFolders)
            {
                foreach (var dir in root.GetDirectories())
                {
                    method(dir.FullName);
                }
            }
        }

        /// <summary>
        /// Calls a method on every item in every subdirectory of a root directory.
        /// </summary>
        /// <param name="root">The root directory whose subdirectories to iterate on.</param>
        /// <param name="target">Specify the target of the iteration - only files, only directories or both.</param>
        /// <param name="method">The method to call on each item.</param>
        public static void IterateSubfolders(DirectoryInfo root, IterateTarget target, Action<string> method)
        {
            foreach (var dir in root.GetDirectories())
            {
                IterateFolder(dir, target, method);
                IterateSubfolders(dir, target, method);
            }
        }
    }
}
