// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Routing;

    public interface IControllerTypeCache
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Needs a factory method for unit test, otherwise we have to come with the new interface and class")]
        Func<IEnumerable<Assembly>> ReferencedAssemblies
        {
            get;
            set;
        }

        Type GetControllerType(RequestContext requestContext, string controllerName);
    }
}