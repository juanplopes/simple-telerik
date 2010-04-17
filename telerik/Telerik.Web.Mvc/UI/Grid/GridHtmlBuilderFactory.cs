// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    public class GridHtmlBuilderFactory : IGridHtmlBuilderFactory
    {
        public IGridHtmlBuilder<T> Create<T>(Grid<T> grid) where T : class
        {
            if (grid.Scrolling.Enabled)
            {
                return new GridScrollableHtmlBuilder<T>(grid);
            }

            return new GridHtmlBuilder<T>(grid);
        }
    }
}