// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure
{
    /// <summary>
    /// Defines members that a class must implement in order to access file system.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Determines whether the specified directory exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        bool DirectoryExists(string path);

        /// <summary>
        /// Determines whether the specified file exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        bool FileExists(string path);

        /// <summary>
        /// Gets the files in the specified locations.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="recursive">if set to <c>true</c> [recursive].</param>
        /// <returns></returns>
        string[] GetFiles(string path, string searchPattern, bool recursive);

        /// <summary>
        /// Reads the content of the specified file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        string ReadAllText(string path);
    }
}