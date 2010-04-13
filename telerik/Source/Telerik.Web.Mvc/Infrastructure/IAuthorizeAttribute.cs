// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure
{
    using System.Web;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "Matching signature of the original authorize attribute.")]
    public interface IAuthorizeAttribute
    {
        int Order
        {
            get;
            set;
        }

        string Roles
        {
            get;
            set;
        }

        string Users
        {
            get;
            set;
        }

        bool IsAuthorized(HttpContextBase httpContext);
    }
}