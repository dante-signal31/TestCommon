using System;
using System.Collections.Generic;
using System.IO;

namespace TestCommon.Fs
{
    /// <summary>
    /// Class to walk through folder tree.
    ///
    /// It offers a <c>FileWalk</c> iterator that returns an Entry type at a time. Each Entry is whether a file or folder
    /// under root folder. Once iterator ends then all entries have been returned.
    /// </summary>
    public class FileWalker
    {
        /// <summary>
        /// FileWalker returns two kind of objects: File and Folder.
        /// </summary>
        public enum EntryType
        {
            /// <summary>
            /// This entry is a file.
            /// </summary>
            File,
            
            /// <summary>
            /// This entry is a folder.
            /// </summary>
            Folder
        }

        /// <summary>
        /// Type returned by FileWalk iterator.
        /// </summary>
        public struct Entry: IEqualityComparer<Entry>
        {
            private string _name;
            private EntryType _type;

            /// <summary>
            /// Create a new Entry object.
            /// </summary>
            /// <param name="Name">Entry pathname.</param>
            /// <param name="Type">Type pathname.</param>
            public Entry(string Name, EntryType Type)
            {
                this._name = Name;
                this._type = Type;
            }

            /// <summary>
            /// Read-write property to entry pathname.
            /// </summary>
            public string Name
            {
                get => _name;
                set => _name = value;
            }

            /// <summary>
            /// Read-write property to entry type.
            /// </summary>
            public EntryType Type
            {
                get => _type;
                set => _type = value;
            }

            /// <summary>
            /// Return true if this entry is a file.
            /// </summary>
            /// <returns></returns>
            public bool IsFile() => this._type == EntryType.File ? true : false;
            
            /// <summary>
            /// Return tru if this entry is a folder.
            /// </summary>
            /// <returns></returns>
            public bool IsFolder() => this._type == EntryType.Folder ? true : false;
            
            /// <summary>
            /// Compare to another entry.
            /// </summary>
            /// <param name="x">Entry 1</param>
            /// <param name="y">Entry 2</param>
            /// <returns>Returns true it their Names and Type properties are the same.</returns>
            public bool Equals(Entry x, Entry y)
            {
                return ((x.Name.Equals(y.Name)) && (x.Type.Equals(y.Type)));
            }

            /// <summary>
            /// Needed to compare Entry types.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int GetHashCode(Entry obj)
            {
                string hashString = obj.Name;
                switch (obj.Type)
                {
                    case FileWalker.EntryType.File:
                        hashString += 1;
                        break;
                    case FileWalker.EntryType.Folder:
                        hashString += 2;
                        break;
                }
                return hashString.GetHashCode();
            }
        }
        
        /// <summary>
        /// Iterate through a folder tree starting at given root folder yielding every entry found.
        ///
        /// Yielded entries can be of both types, files and folder identified by Type attribute.
        /// </summary>
        /// <param name="root">Base path where start to iterate.</param>
        /// <returns>Entry</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<Entry> FileWalk(string root)
        {
            if (!Directory.Exists(root))
            {
                throw new ArgumentException("Given folder root does not exist.");
            }
            
            Stack<Entry> entries = new Stack<Entry>();
            Entry initial_entry = new Entry(root, EntryType.Folder);
            entries.Push(initial_entry);
            
            while (entries.Count > 0)
            {
                Entry entry = entries.Pop();
                yield return entry;
                if (entry.IsFolder())
                {
                    foreach (string subFile in Directory.GetFiles(entry.Name))
                    {
                        yield return new Entry(subFile, EntryType.File);
                    }
                    foreach (string subFolder in Directory.GetDirectories(entry.Name))
                    {
                        entries.Push(new Entry(subFolder, EntryType.Folder));
                    }
                } 
                else if (entry.IsFile())
                {
                    yield return entry;
                }
            }
        }
    }
}