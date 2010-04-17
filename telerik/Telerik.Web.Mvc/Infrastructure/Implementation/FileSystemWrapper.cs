// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Provides properties and methods for working with drives, files, and directories.
    /// </summary>
    public class FileSystemWrapper : IFileSystem
    {
        /// <summary>
        /// Determines whether the given path refers to an existing directory on disk.
        /// </summary>
        /// <param name="path">The path to test.</param>
        /// <returns>true if path refers to an existing directory; otherwise, false.</returns>
        [DebuggerStepThrough]
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// Determines whether the specified file exists.
        /// </summary>
        /// <param name="path">The file to check.</param>
        /// <returns>true if the caller has the required permissions and path contains the name of an existing file; otherwise, false. This method also returns false if path is null, an invalid path, or a zero-length string. If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method returns false regardless of the existence of path.</returns>
        [DebuggerStepThrough]
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// Returns the names of files in the specified directory that match the specified search pattern, using a value to determine whether to search subdirectories.
        /// </summary>
        /// <param name="path">The directory to search.</param>
        /// <param name="searchPattern">The search string to match against the names of files in path. The parameter cannot end in two periods ("..") or contain two periods ("..") followed by System.IO.Path.DirectorySeparatorChar or System.IO.Path.AltDirectorySeparatorChar, nor can it contain any of the characters in System.IO.Path.InvalidPathChars.</param>
        /// <param name="recursive">if set to <c>true</c> the search operation should include all subdirectories or only the current directory.</param>
        /// <returns>A String array containing the names of files in the specified directory that match the specified search pattern. File names include the full path.</returns>
        [DebuggerStepThrough]
        public string[] GetFiles(string path, string searchPattern, bool recursive)
        {
            return DirectoryExists(path) ? Directory.GetFiles(path, searchPattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly) : new string[0];
        }

        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        /// <returns>A string containing all lines of the file.</returns>
        [DebuggerStepThrough]
        public string ReadAllText(string path)
        {
            if (!FileExists(path))
                return null;

            return File.ReadAllText(path);
        }
    }
}