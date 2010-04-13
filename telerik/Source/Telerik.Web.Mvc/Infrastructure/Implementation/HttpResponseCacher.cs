// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;
    using System.Web;

    /// <summary>
    /// Class used to cache the http response.
    /// </summary>
    public class HttpResponseCacher : IHttpResponseCacher
    {
        /// <summary>
        /// Set the caching policy.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="duration">The duration.</param>
        public void Cache(HttpContextBase context, TimeSpan duration)
        {
            Guard.IsNotNull(context, "context");

            if ((duration > TimeSpan.Zero) || !context.IsDebuggingEnabled) // Do not cache in debug mode.
            {
                HttpCachePolicyBase cache = context.Response.Cache;

                cache.SetCacheability(HttpCacheability.Public);
                cache.SetOmitVaryStar(true);
                cache.SetExpires(context.Timestamp.Add(duration));
                cache.SetMaxAge(duration);
                cache.SetValidUntilExpires(true);
                cache.SetLastModified(context.Timestamp);
                cache.SetLastModifiedFromFileDependencies();
                cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            }
        }
    }
}