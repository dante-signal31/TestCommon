using System;
using System.IO;
using System.Security.Cryptography;

namespace TestCommon.Fs
{
    /// <summary>
    ///  Cryptographic functions for your tests. Here you can find hashing functions to check file contents.
    /// </summary>
    public class crypto
    {
        /// <summary>
        /// Hash file content with SHA-256.
        ///
        ///  This way we can check two files have same content.
        /// </summary>
        /// <param name="filePath">Absolute path name.</param>
        /// <returns>File hash string.</returns>
        public static string hash_file(string filePath)
        {
            byte[] content = File.ReadAllBytes(filePath);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(content);
                string hashString = BitConverter.ToString(hashValue).Replace("-", "").ToLower();
                return hashString;
            }
        }
    }
}