// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System.Web;

    public class UrlEncoder : IUrlEncoder
    {
        public string Encode(string value)
        {
            HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);

            return httpContext.Server.UrlEncode(value);
        }

        public string PathEncode(string value)
        {
            HttpContextBase httpContext = new HttpContextWrapper(HttpContext.Current);

            return httpContext.Server.UrlPathEncode(value);
        }
    }
}