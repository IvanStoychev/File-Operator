using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Backend;
using Backend.Models;
using Xunit;

namespace Backend.Tests
{
    public class FolderDiffTests
    {
        [Fact]
        public void FolderDiff_Pass_NoDiff()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                                                    {
                                                        { @"C:\source\file0_1.txt", new MockFileData("") },
                                                        { @"C:\source\file0_2.txt", new MockFileData("") },
                                                        { @"C:\source\file0_3.txt", new MockFileData("") },
                                                        { @"C:\source\folder1\file1_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder1\file1_2.txt", new MockFileData("") },
                                                        { @"C:\source\folder1\folder1_1\file11_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\file2_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\file2_2.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\file2_3.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\file2_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\file2_2.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\file2_3.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\folder21_1\file21_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\folder21_1\folder211_1\file211_1.txt", new MockFileData("") },
                                                        { @"C:\target\file0_1.txt", new MockFileData("") },
                                                        { @"C:\target\file0_2.txt", new MockFileData("") },
                                                        { @"C:\target\file0_3.txt", new MockFileData("") },
                                                        { @"C:\target\folder1\file1_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder1\file1_2.txt", new MockFileData("") },
                                                        { @"C:\target\folder1\folder1_1\file11_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\file2_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\file2_2.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\file2_3.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\file2_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\file2_2.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\file2_3.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\folder21_1\file21_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\folder21_1\folder211_1\file211_1.txt", new MockFileData("") }
                                                    });

            string[] missingDirectoriesArray = Array.Empty<string>();
            string[] extraDirectoriesArray = Array.Empty<string>();
            string[] missingFilesArray = Array.Empty<string>();
            string[] extraFilesArray = Array.Empty<string>();

            var expected = new FolderDiffData(missingDirectoriesArray, extraDirectoriesArray, missingFilesArray, extraFilesArray);

            var logisEngine = new LogisEngine(fileSystem);
            var result = logisEngine.CompareFilesInFolders(@"C:\source", @"C:\target");

            Assert.True(expected.Equals(result));
        }

        [Fact]
        public void FolderDiff_Pass_Extra()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                                                    {
                                                        { @"C:\source\file0_1.txt", new MockFileData("") },
                                                        { @"C:\source\file0_2.txt", new MockFileData("") },
                                                        { @"C:\source\file0_3.txt", new MockFileData("") },
                                                        { @"C:\source\folder1\file1_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder1\file1_2.txt", new MockFileData("") },
                                                        //{ @"C:\source\folder1\folder1_1\file11_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\file2_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\file2_2.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\file2_3.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\file2_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\file2_2.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\file2_3.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\folder21_1\file21_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\folder21_1\folder211_1\file211_1.txt", new MockFileData("") },
                                                        { @"C:\target\file0_1.txt", new MockFileData("") },
                                                        { @"C:\target\file0_2.txt", new MockFileData("") },
                                                        { @"C:\target\file0_3.txt", new MockFileData("") },
                                                        { @"C:\target\folder1\file1_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder1\file1_2.txt", new MockFileData("") },
                                                        { @"C:\target\folder1\folder1_1\file11_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\file2_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\file2_2.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\file2_3.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\file2_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\file2_2.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\file2_3.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\folder21_1\file21_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\folder21_1\folder211_1\file211_1.txt", new MockFileData("") }
                                                    });

            string[] missingDirectoriesArray = Array.Empty<string>();
            string[] extraDirectoriesArray = new[] { @"\folder1\folder1_1" };
            string[] missingFilesArray = Array.Empty<string>();
            string[] extraFilesArray = new[] { @"\folder1\folder1_1\file11_1.txt" };

            var expected = new FolderDiffData(missingDirectoriesArray, extraDirectoriesArray, missingFilesArray, extraFilesArray);

            var logisEngine = new LogisEngine(fileSystem);
            var result = logisEngine.CompareFilesInFolders(@"C:\source", @"C:\target");

            Assert.True(expected.Equals(result));
        }

        [Fact]
        public void FolderDiff_Pass_Missing()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                                                    {
                                                        { @"C:\source\file0_1.txt", new MockFileData("") },
                                                        { @"C:\source\file0_2.txt", new MockFileData("") },
                                                        { @"C:\source\file0_3.txt", new MockFileData("") },
                                                        { @"C:\source\folder1\file1_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder1\file1_2.txt", new MockFileData("") },
                                                        { @"C:\source\folder1\folder1_1\file11_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\file2_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\file2_2.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\file2_3.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\file2_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\file2_2.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\file2_3.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\folder21_1\file21_1.txt", new MockFileData("") },
                                                        { @"C:\source\folder2\folder2_1\folder21_1\folder211_1\file211_1.txt", new MockFileData("") },
                                                        { @"C:\target\file0_1.txt", new MockFileData("") },
                                                        { @"C:\target\file0_2.txt", new MockFileData("") },
                                                        { @"C:\target\file0_3.txt", new MockFileData("") },
                                                        { @"C:\target\folder1\file1_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder1\file1_2.txt", new MockFileData("") },
                                                        //{ @"C:\target\folder1\folder1_1\file11_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\file2_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\file2_2.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\file2_3.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\file2_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\file2_2.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\file2_3.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\folder21_1\file21_1.txt", new MockFileData("") },
                                                        { @"C:\target\folder2\folder2_1\folder21_1\folder211_1\file211_1.txt", new MockFileData("") }
                                                    });

            string[] missingDirectoriesArray = new[] { @"\folder1\folder1_1" };
            string[] extraDirectoriesArray = Array.Empty<string>();
            string[] missingFilesArray = new[] { @"\folder1\folder1_1\file11_1.txt" };
            string[] extraFilesArray = Array.Empty<string>();

            var expected = new FolderDiffData(missingDirectoriesArray, extraDirectoriesArray, missingFilesArray, extraFilesArray);

            var logisEngine = new LogisEngine(fileSystem);
            var result = logisEngine.CompareFilesInFolders(@"C:\source", @"C:\target");

            Assert.True(expected.Equals(result));
        }
    }
}
