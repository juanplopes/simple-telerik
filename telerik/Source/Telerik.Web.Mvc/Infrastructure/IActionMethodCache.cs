// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Routing;

    public interface IActionMethodCache
    {
        IList<MethodInfo> GetActionMethods(RequestContext requestContext, string controllerName, string actionName);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Just to avoid creating some extra classes to hold the data structure.")]
        IDictionary<string, IList<MethodInfo>> GetAllActionMethods(RequestContext requestContext, string controllerName);
    }
}