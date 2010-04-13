// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Basic building block to locate the correct virtual path.
    /// </summary>
    public interface IWebAssetLocator
    {
        /// <summary>
        /// Returns the correct virtual path based upon the debug mode and version.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        string Locate(string virtualPath, string version);
    }

    /// <summary>
    /// Default web asset locator.
    /// </summary>
    public class WebAssetLocator : CacheBase<string, string>, IWebAssetLocator
    {
        private readonly bool isInDebugMode;
        private readonly IVirtualPathProvider virtualPathProvider;

        public WebAssetLocator(bool isInDebugMode, IVirtualPathProvider virtualPathProvider) : base(StringComparer.OrdinalIgnoreCase)
        {
            Guard.IsNotNull(virtualPathProvider, "virtualPathProvider");

            this.isInDebugMode = isInDebugMode;
            this.virtualPathProvider = virtualPathProvider;
        }

        /// <summary>
        /// Returns the correct virtual path based upon the debug mode and version.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public string Locate(string virtualPath, string version)
        {
            Guard.IsNotNullOrEmpty(virtualPath, "virtualPath");

            return isInDebugMode ? InternalLocate(virtualPath, version) : GetOrCreate("{0}:{1}".FormatWith(virtualPath, version), () => InternalLocate(virtualPath, version));
        }

        private string InternalLocate(string virtualPath, string version)
        {
            string result = virtualPath;

            string extension = virtualPathProvider.GetExtension(virtualPath);

            if (extension.IsCaseInsensitiveEqual(".js"))
            {
                result = isInDebugMode ? ProbePath(virtualPath, version, new[] { ".debug.js", ".js", ".min.js" }) : ProbePath(virtualPath, version, new[] { ".min.js", ".js", ".debug.js" });
            }
            else if (extension.IsCaseInsensitiveEqual(".css"))
            {
                result = isInDebugMode ? ProbePath(virtualPath, version, new[] { ".css", ".min.css" }) : ProbePath(virtualPath, version, new[] { ".min.css", ".css" });
            }

            return result;
        }

        private string ProbePath(string virtualPath, string version, IEnumerable<string> extensions)
        {
            string result = null;

            Func<string ,string> fixPath = path =>
                                           {
                                               string directory = virtualPathProvider.GetDirectory(path);
                                               string fileName = virtualPathProvider.GetFile(path);

                                               if (!directory.EndsWith(version + Path.AltDirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                                               {
                                                   string newDirectory = virtualPathProvider.CombinePaths(directory, version);
                                                   string newPath = newDirectory + Path.AltDirectorySeparatorChar + fileName;

                                                   if (virtualPathProvider.FileExists(newPath))
                                                   {
                                                       return newPath;
                                                   }
                                               }

                                               return path;
                                           };

            foreach (string extension in extensions)
            {
                string changedPath = Path.ChangeExtension(virtualPath, extension);
                string newVirtualPath = string.IsNullOrEmpty(version) ? changedPath : fixPath(changedPath);

                if (virtualPathProvider.FileExists(newVirtualPath))
                {
                    result = newVirtualPath;
                    break;
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                result = virtualPath;

                if (!virtualPathProvider.FileExists(result))
                {
                    throw new FileNotFoundException(Resources.TextResource.SpecifiedFileDoesNotExist.FormatWith(result));
                }
            }

            return result;
        }
    }
}