// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Infrastructure;

    /// <summary>
    /// Defines an individual web asset.
    /// </summary>
    public class WebAssetItem : IWebAssetItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebAssetItem"/> class.
        /// </summary>
        /// <param name="source">The source. Source must be a virtual path.</param>
        public WebAssetItem(string source)
        {
            Guard.IsNotVirtualPath(source, "source");

            Source = source;
        }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>The source.</value>
        public string Source
        {
            get;
            private set;
        }
    }
}