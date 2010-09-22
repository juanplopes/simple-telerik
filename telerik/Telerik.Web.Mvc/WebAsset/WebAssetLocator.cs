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
        private static string[] DebugJavaScriptExtensions = new[] { ".debug.js", ".js", ".min.js" };
        private static string[] ReleaseJavaScriptExtensions = new[] { ".min.js", ".js", ".debug.js" };
        private static string[] DebugCssExtensions = new[] { ".css", ".min.css" };
        private static string[] ReleaseCssExtensions = new[] { ".min.css", ".css" };

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

            if (virtualPath.IndexOf("://") > -1)
            {
                return virtualPath;
            }
            
            if (isInDebugMode)
            {
                return Resolve(virtualPath, version);
            }

            return GetOrCreate("{0}:{1}".FormatWith(virtualPath, version), () => Resolve(virtualPath, version));
        }

        private string Resolve(string virtualPath, string version)
        {
            string result = virtualPath;

            string extension = virtualPathProvider.GetExtension(virtualPath);
            string[] extensions = null;

            if (extension.IsCaseInsensitiveEqual(".js"))
            {
                extensions = isInDebugMode ? DebugJavaScriptExtensions : ReleaseJavaScriptExtensions;
            }
            else if (extension.IsCaseInsensitiveEqual(".css"))
            {
                extensions = isInDebugMode ? DebugCssExtensions : ReleaseCssExtensions;
            }

            if (extensions != null)
            {
                result = ProbePath(virtualPath, version, extensions);
            }

            return result;
        }
        
        private bool TryPath(string path, string modifier, out string result)
        {
            var directory = virtualPathProvider.GetDirectory(path);
            var fileName = virtualPathProvider.GetFile(path);
            var pathToProbe = modifier.HasValue() ? virtualPathProvider.CombinePaths(directory, modifier) + Path.AltDirectorySeparatorChar + fileName : path;

            result = virtualPathProvider.FileExists(pathToProbe) ? pathToProbe : null;

            return result != null;
        }

        private string ProbePath(string virtualPath, string version, IEnumerable<string> extensions)
        {
            foreach (var modifier in new[] { version, "" })
            {
                foreach (var extension in extensions)
                {
                    var changedPath = Path.ChangeExtension(virtualPath, extension);

                    string result = null;

                    if (TryPath(changedPath, modifier, out result))
                    {
                        return result;
                    }
                }
            }

            throw new FileNotFoundException(Resources.TextResource.SpecifiedFileDoesNotExist.FormatWith(virtualPath));
        }
    }
}