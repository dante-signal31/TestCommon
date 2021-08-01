using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using NUnit.Framework;
using TestCommon.Fs;

namespace TestCommonTests
{
    public class OpsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDeleteFile()
        {
            string tempFilePath = Path.GetTempFileName();
            Assert.True(File.Exists(tempFilePath));
            ops.delete_file(tempFilePath);
            Assert.False(File.Exists(tempFilePath));
        }

        [Test]
        public void TestDeleteExistingFiles()
        {
            string[] tempFilePathnames = new string[]
            {
                Path.GetTempFileName(),
                Path.GetTempFileName(),
                Path.GetTempFileName()
            };
            foreach (string filePathName in tempFilePathnames)
            {
                Assert.True(File.Exists(filePathName));
            }
            ops.delete_files(tempFilePathnames, false);
            foreach (string filePathName in tempFilePathnames)
            {
                Assert.False(File.Exists(filePathName));
            }
        }

        [Test]
        public void TestDeletingNonExistingFiles()
        {
            string tempFilePathName = Path.GetTempFileName();
            Assert.True(File.Exists(tempFilePathName));
            string unexistingFile = Path.Combine(Path.GetTempPath(), "123456789.nex");
            string[] filesToDelete = new string[]
            {
                tempFilePathName,
                unexistingFile
            };
            // Check non-existing files are detected when ignore_missing is False.
            Assert.Throws<FileNotFoundException>(() => ops.delete_files(filesToDelete, false));
            //Check non-existing files don't raise any exception when ignore_missing is true.
            tempFilePathName = Path.GetTempFileName();
            Assert.True(File.Exists(tempFilePathName));
            filesToDelete = new string[]
            {
                tempFilePathName,
                unexistingFile
            };
            ops.delete_files(filesToDelete,true);
            Assert.False(File.Exists(tempFilePathName));
        }

        [Test]
        public void TestCopyFile()
        {
            string tempFilePathName = Path.GetTempFileName();
            string tempFileName = Path.GetFileName(tempFilePathName);
            using (Temp tempDir = new Temp(Temp.TempType.Folder))
            {
                string expectedFilePathName = Path.Combine(tempDir.TempPath, tempFileName);
                Assert.False(File.Exists(expectedFilePathName));
                ops.copy_file(tempFilePathName, expectedFilePathName);
                Assert.True(File.Exists(expectedFilePathName));
            }
        }

        [Test]
        public void TestCopyFiles()
        {
            // Create temporal files at default temp folder.
            var tempFileNames = (from _ in Enumerable.Range(0, 2) select Path.GetTempFileName()).ToArray();
            // Copy temporal files to our own temporal folder.
            using (Temp tempDir = new Temp(Temp.TempType.Folder))
            {
                ops.copy_files(tempFileNames.ToArray(), tempDir.TempPath);
                // Check all created and copied files are actually at our temporal folder.
                Assert.True(tempFileNames.All(
                    file => File.Exists(
                        Path.Combine(tempDir.TempPath,
                            Path.GetFileName(file))
                        )
                    ));
            }
        }
    }
}