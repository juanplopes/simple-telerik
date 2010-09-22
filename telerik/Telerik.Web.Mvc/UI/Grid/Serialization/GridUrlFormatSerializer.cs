// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Extensions;

    class GridUrlFormatSerializer<T>
        where T : class
    {
        private readonly Grid<T> grid;

        public GridUrlFormatSerializer(Grid<T> grid)
        {
            this.grid = grid;
        }

        public void SerializeTo(IClientSideObjectWriter writer)
        {
            if (!grid.IsClientBinding)
            {
                grid.Server.Select.RouteValues.Merge(grid.ViewContext.RequestContext.RouteData.Values);
                
                if (grid.Paging.Enabled)
                {
                    grid.Server.Select.RouteValues[grid.Prefix(GridUrlParameters.CurrentPage)] = "{0}";
                }
                
                if (grid.Sorting.Enabled)
                {
                    grid.Server.Select.RouteValues[grid.Prefix(GridUrlParameters.OrderBy)] = "{1}";
                }

                if (grid.Grouping.Enabled)
                {
                    grid.Server.Select.RouteValues[grid.Prefix(GridUrlParameters.GroupBy)] = "{2}";
                }
                
                if (grid.Filtering.Enabled)
                {
                    grid.Server.Select.RouteValues[grid.Prefix(GridUrlParameters.Filter)] = "{3}";
                }
                
                GridUrlBuilder urlBuilder = new GridUrlBuilder(grid);
                writer.Append("urlFormat", urlBuilder.Url(grid.Server.Select));
            }
        }
    }
}
