// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions
{
    using System.Web.Mvc;
    using System;
    using System.Web.Routing;
    
    using Infrastructure;
    using UI;

    public static class GridControllerExtensions
    {
        public static RouteValueDictionary GridRouteValues(this ControllerBase controller)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary();

            foreach (string key in controller.ControllerContext.HttpContext.Request.Params)
            {
                if (key.EndsWith(GridUrlParameters.CurrentPage, StringComparison.OrdinalIgnoreCase) ||
                    key.EndsWith(GridUrlParameters.Filter, StringComparison.OrdinalIgnoreCase) ||
                    key.EndsWith(GridUrlParameters.OrderBy, StringComparison.OrdinalIgnoreCase) ||
                    key.EndsWith(GridUrlParameters.GroupBy, StringComparison.OrdinalIgnoreCase))
                {
                    routeValues[key] = controller.ControllerContext.HttpContext.Request.Params[key];
                }
            }

            return routeValues;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static T ValueOf<T>(this ControllerBase controller, string key)
        {
            ValueProviderResult result;
            bool found = true;
#if MVC1
            found = controller.ValueProvider.TryGetValue(key, out result);
#endif
#if MVC2
            result = controller.ValueProvider.GetValue(key);
            found = result != null;
#endif
            if (found)
            {
                return (T)result.ConvertTo(typeof(T), Culture.Current);
            }

            return default(T);
        }
    }
}
