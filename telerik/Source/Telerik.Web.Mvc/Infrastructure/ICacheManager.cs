// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure
{
    using System.Web.Caching;

    /// <summary>
    /// Defines members that a class must implement in order to access System.Web.HttpRuntime.Cache object.
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object GetItem(string key);

        /// <summary>
        /// Inserts the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="onRemoveCallback">The on remove callback.</param>
        /// <param name="fileDependencies">The file dependencies.</param>
        void Insert(string key, object value, CacheItemRemovedCallback onRemoveCallback, params string[] fileDependencies);
    }
}