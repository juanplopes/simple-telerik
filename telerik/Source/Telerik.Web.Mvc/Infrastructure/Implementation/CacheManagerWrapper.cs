// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System.Diagnostics;
    using System.Web;
    using System.Web.Caching;

    using Extensions;

    /// <summary>
    /// Encapsulates the System.Web.HttpRuntime.Cache object that contains methods for accessing System.Web.HttpRuntime.Cache object.
    /// </summary>
    public class CacheManagerWrapper : ICacheManager
    {
        /// <summary>
        /// Retrieves the specified item from the System.Web.HttpRuntime.Cache object.
        /// </summary>
        /// <param name="key">The object to be retrives from the cache.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public object GetItem(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        /// <summary>
        /// Inserts an object into the System.Web.Caching.Cache object with dependencies and a delegate you can use to notify your application when the inserted item is removed from the Cache.
        /// </summary>
        /// <param name="key">The object to be inserted in the cache.</param>
        /// <param name="value">The file or cache key dependencies for the item. When any dependency changes, the object becomes invalid and is removed from the cache. If there are no dependencies, this parameter contains null.</param>
        /// <param name="onRemoveCallback">A delegate that, if provided, will be called when an object is removed from the cache. You can use this to notify applications when their objects are deleted from the cache.</param>
        /// <param name="fileDependencies">List of files that the cache item depends upon, if any of the file is changed the cache item will become invalid.</param>
        [DebuggerStepThrough]
        public void Insert(string key, object value, CacheItemRemovedCallback onRemoveCallback, params string[] fileDependencies)
        {
            HttpRuntime.Cache.Insert(key, value, fileDependencies.IsNullOrEmpty() ? null : new CacheDependency(fileDependencies), Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, onRemoveCallback);
        }
    }
}