// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Extensions;

    public class GridUrlBuilder
    {
        private readonly IGrid grid;

        public GridUrlBuilder(IGrid grid)
        {
            this.grid = grid;
        }

        public string Url(INavigatable navigatable, string key, object value)
        {
            RouteValueDictionary routeValues = PrepareRouteValues(navigatable.RouteValues);
            
            routeValues[grid.Prefix(key)] = value;

            return Url(routeValues);
        }

        public string Url(RouteValueDictionary routeValues)
        {
            return new UrlHelper(grid.ViewContext.RequestContext).RouteUrl(routeValues);
        }

        public string Url(INavigatable navigatable, Action<RouteValueDictionary> configurator)
        {
            RouteValueDictionary routeValues = PrepareRouteValues(navigatable.RouteValues);
            configurator(routeValues);

            return navigatable.GenerateUrl(grid.ViewContext, grid.UrlGenerator, routeValues);
        }

        public string Url(INavigatable navigatable)
        {
            RouteValueDictionary routeValues = PrepareRouteValues(navigatable.RouteValues);

            return navigatable.GenerateUrl(grid.ViewContext, grid.UrlGenerator, routeValues);
        }

        public RouteValueDictionary PrepareRouteValues(RouteValueDictionary routeValues)
        {
            RouteValueDictionary result = new RouteValueDictionary(routeValues);

            result.Merge(grid.ViewContext.RouteData.Values, false);
            
            if (grid.EnableCustomBinding)
            {
                result[grid.Prefix(GridUrlParameters.PageSize)] = grid.PageSize;
            }

            foreach (string key in grid.ViewContext.HttpContext.Request.QueryString)
            {
                if (key != null && !result.ContainsKey(key))
                {
                    result[key] = grid.ViewContext.HttpContext.Request.QueryString[key];
                }
            }

            return result;
        }
    }
}