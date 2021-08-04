using System;
using System.IO;

namespace TestCommon.Fs
{
    /// <summary>
    /// File operation functions.
    ///
    /// Specifically some functions to ease copying and deleting files.
    /// </summary>
    public class Ops
    {
        /// <summary>
        /// Delete an specific file.
        /// </summary>
        /// <param name="filePath"> String with the absolute path to file.</param>
        /// <exception cref="FileNotFoundException">Exception raised if given file is not found for deletion.</exception>
        public static void delete_file(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException(filePath + " not found to be deleted."); 
            File.Delete(filePath);
        }

        /// <summary>
        /// Delete all files set in given list.
        /// </summary>
        /// <param name="files">List with absolute pathname for files to remove.</param>
        /// <param name="ignoreMissing">If true does not return an error if any of files actually does not exists.</param>
        /// <exception cref="FileNotFoundException">Exception raised if any of given files is not found for deletion.</exception>
        public static void delete_files(string[] files, bool ignoreMissing)
        {
            foreach (string file in files)
            {
                try
                {
                    delete_file(file);
                }
                catch (FileNotFoundException)
                {
                    if (ignoreMissing) continue;
                    throw;
                }
            }
        }

        /// <summary>
        /// Copy an specific file.
        ///
        /// If destination file already exists then it is not overwritten.
        /// </summary>
        /// <param name="sourceFilePath">String with absolute pathname to original file.</param>
        /// <param name="destinationFilePath">String with absolute pathname to copied file.</param>
        public static void copy_file(string sourceFilePath, string destinationFilePath)
        {
            File.Copy(sourceFilePath, destinationFilePath, false);
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
            foreach (string file in files)
            {
                string fileBaseName = Path.GetFileName(file);
                copy_file(file, Path.Combine(destinationFolderPath, fileBaseName));
            }
        }

        /// <summary>
        /// Copy an entire folder tree structure to a new root.
        ///
        /// Original files are untouched.
        /// </summary>
        /// <param name="originalRoot">Root folder to take beneath folder from.</param>
        /// <param name="copyRoot">Folder to copy tree under.</param>
        public static void recreate_folder_tree(string originalRoot, string copyRoot)
        {
            foreach (FileWalker.Entry entry in FileWalker.FileWalk(originalRoot))
            {
                if (entry.Type == FileWalker.EntryType.File)
                {
                    // If it es a file just copy it.
                    string relativeFileName = Path.GetRelativePath(originalRoot, entry.Name);
                    copy_file(entry.Name, Path.Combine(copyRoot, relativeFileName));
                }
                else
                {
                    // If it is a folder then we must create at destination.
                    string folderSubPath = Path.GetRelativePath(originalRoot, entry.Name);
                    Directory.CreateDirectory(Path.Combine(copyRoot, folderSubPath));
                }
            }
        }
        
    }
}