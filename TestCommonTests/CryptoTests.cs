using System.IO;
using System.Text;
using NUnit.Framework;
using TestCommon.fs;

namespace TestCommonTests
{
    public class TestCrypto
    {
        [Test]
        public void TestHashFile()
        {
            string EXPECTED_HASH = "c3ab8ff13720e8ad9047dd39466b3c8974e592c2fa383d4a3960714caef0c4f2";
            using (Temp tempFile = new Temp(Temp.TempType.File))
            {
                byte[] contentBytes = Encoding.ASCII.GetBytes("foobar");
                FileStream writeStream = File.OpenWrite(tempFile.TempPath);
                writeStream.Write(contentBytes);
                writeStream.Flush();
                writeStream.Close();
                string recoveredFileHash = crypto.hash_file(tempFile.TempPath);
                Assert.True(EXPECTED_HASH.Equals(recoveredFileHash));
            }
        }
    }
}