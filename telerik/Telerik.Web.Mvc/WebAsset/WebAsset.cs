// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc
{
    using Infrastructure;

    /// <summary>
    /// Reprenets an web asset.
    /// </summary>
    public class WebAsset
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebAsset"/> class.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="version">The version.</param>
        /// <param name="compress">if set to <c>true</c> [compress].</param>
        /// <param name="cacheDurationInDays">The cache duration in days.</param>
        /// <param name="content">The content.</param>
        public WebAsset(string contentType, string version, bool compress, float cacheDurationInDays, string content)
        {
            Guard.IsNotNullOrEmpty(contentType, "contentType");
            Guard.IsNotNullOrEmpty(version, "version");
            Guard.IsNotNegative(cacheDurationInDays, "cacheDurationInDays");
            Guard.IsNotNullOrEmpty(content, "content");

            ContentType = contentType;
            Version = version;
            Compress = compress;
            CacheDurationInDays = cacheDurationInDays;
            Content = content;
        }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        public string ContentType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="WebAsset"/> is compressed.
        /// </summary>
        /// <value><c>true</c> if compress; otherwise, <c>false</c>.</value>
        public bool Compress
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the cache duration in days.
        /// </summary>
        /// <value>The cache duration in days.</value>
        public float CacheDurationInDays
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content
        {
            get;
            private set;
        }
    }
}