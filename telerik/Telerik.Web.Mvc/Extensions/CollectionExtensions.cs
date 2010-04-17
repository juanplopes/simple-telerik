// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using Infrastructure;

    /// <summary>
    /// Contains extension methods of ICollection&lt;T&gt;.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines whether the specified collection instance is null or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance to check.</param>
        /// <returns>
        /// <c>true</c> if the specified instance is null or empty; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this ICollection<T> instance)
        {
            return (instance == null) || (instance.Count == 0);
        }

        /// <summary>
        /// Determines whether the specified collection is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>
        /// <c>true</c> if the specified instance is empty; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        public static bool IsEmpty<T>(this ICollection<T> instance)
        {
            Guard.IsNotNull(instance, "instance");

            return instance.Count == 0;
        }

        /// <summary>
        /// Adds the specified elements to the end of the System.Collections.Generic.ICollection&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance to add.</param>
        /// <param name="collection"> The collection whose elements should be added to the end of the ICollection&lt;T&gt;. The collection itself cannot be null, but it can contain elements that are null, if type T is a reference type.</param>
        [DebuggerStepThrough]
        public static void AddRange<T>(this ICollection<T> instance, IEnumerable<T> collection)
        {
            Guard.IsNotNull(instance, "instance");
            Guard.IsNotNull(collection, "collection");

            foreach (T item in collection)
            {
                instance.Add(item);
            }
        }
    }
}