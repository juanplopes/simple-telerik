// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions
{
    using System.Diagnostics;

    using Infrastructure;

    /// <summary>
    /// Contains extension methods of T[].
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Determines whether the specified array is empty or null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>
        /// <c>true</c> if [is null or empty] [the specified instance]; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this T[] instance)
        {
            return (instance == null) || (instance.Length == 0);
        }

        /// <summary>
        /// Determines whether the specified array is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>
        /// <c>true</c> if the specified instance is empty; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        public static bool IsEmpty<T>(this T[] instance)
        {
            Guard.IsNotNull(instance, "instance");

            return instance.Length == 0;
        }
    }
}