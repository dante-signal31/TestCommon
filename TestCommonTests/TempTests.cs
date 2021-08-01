using System.IO;
using NUnit.Framework;
using TestCommon;
using TestCommon.Fs;

namespace TestCommonTests
{
    public class TempTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestTempFileCreation()
        {
            string tempFilePath; 
            using (var tempFile = new Temp(Temp.TempType.File))
            {
                tempFilePath = tempFile.TempPath;
                // Check file is already created.
                Assert.True(File.Exists(tempFile.TempPath));
                string generalTempFolder = Path.GetTempPath();
                Assert.True(tempFilePath.Contains(generalTempFolder));
            }
            // Check file no longer exists.
            Assert.False(File.Exists(tempFilePath));
        }

        [Test]
        public void TestTempFolderCreation()
        {
            string tempFolderPath; 
            using (var tempFolder = new Temp(Temp.TempType.Folder))
            {
                tempFolderPath = tempFolder.TempPath;
                // Check file is already created.
                Assert.True(Directory.Exists(tempFolder.TempPath));
                string generalTempFolder = Path.GetTempPath();
                Assert.True(tempFolderPath.Contains(generalTempFolder));
            }
            // Check file no longer exists.
            Assert.False(Directory.Exists(tempFolderPath));
        }
    }
}