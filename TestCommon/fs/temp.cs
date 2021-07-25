using System;
using System.IO;

namespace TestCommon.fs
{
    /// <summary>
    /// Creates a temporal object in OS current temporal storage.
    ///
    /// You can create these types of objects:
    /// <list type="bullet">
    ///     <item>
    ///         <term>File: </term>
    ///         <description>Create a temporal file in temporal storage.</description>
    ///     </item>
    ///     <item>
    ///         <term>Folder: </term>
    ///         <description>Create a temporal folder in temporal storage.</description>
    ///     </item>
    /// </list>
    ///
    /// This class is supposed to be used in an <c>using</c> clause:
    /// <example>
    ///     <code> 
    ///         using (var tempFolder = new Temp(Temp.TempType.Folder)
    ///         {
    ///             // Operate with tempFolder.
    ///         } // tempFolder and its contents is removed when using statement ends.
    ///     </code>
    /// </example>
    /// <exception cref="IOException">
    /// An I/O error occurs, such as no unique temporary name is available. -or-
    /// This method was unable to create a temporary file or folder.
    /// </exception>
    /// </summary>
    public class Temp: IDisposable
    {
        public enum TempType {File, Folder}

        private TempType _type;
        private string _tempPath;
    
        public Temp(TempType type)
        {
            _type = type;
            switch (_type)
            {
                case TempType.File:
                    _tempPath = Path.GetTempFileName();
                    break;
                case TempType.Folder:
                    _tempPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                    Directory.CreateDirectory(_tempPath);
                    break;
            }
        }

        public TempType Type => _type;

        public string TempPath => _tempPath;

        public void Dispose()
        {
            switch (_type)
            {
                case TempType.File:
                    File.Delete(_tempPath);
                    break;
                case TempType.Folder:
                    Directory.Delete(_tempPath, true);
                    break;
            }
        }
    }
}
