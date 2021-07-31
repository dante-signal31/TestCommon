using System;
using System.IO;

namespace TestCommon.fs
{
    public class ops
    {
        /// <summary>
        /// Delete an specific file.
        /// </summary>
        /// <param name="filePath"> String with the absolute path to file.</param>
        public static void delete_file(string filePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete all files set in given list.
        /// </summary>
        /// <param name="files">List with absolute pathname for files to remove.</param>
        /// <param name="ignoreMissing">If true does not return an error if any of files actually does not exists.</param>
        /// <exception cref="NotImplementedException"></exception>
        public static void delete_files(string[] files, bool ignoreMissing)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copy an specific file.
        /// </summary>
        /// <param name="sourceFilePath">String with absolute pathname to original file.</param>
        /// <param name="destinationFilePath">String with absolute pathname to copied file.</param>
        /// <exception cref="NotImplementedException"></exception>
        public static void copy_file(string sourceFilePath, string destinationFilePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copy all files in a given list to a given destination folder.
        ///
        /// Original file names are kept untouched.
        /// </summary>
        /// <param name="files">List with absolute pathnames as strings.</param>
        /// <param name="destinationFolderPath">Absolute path name to folder where to copy files into.</param>
        /// <exception cref="NotImplementedException"></exception>
        public static void copy_files(string[] files, string destinationFolderPath)
        {
            throw new NotImplementedException();
        }
        
    }
}