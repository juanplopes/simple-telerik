// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;

    public class GridActionColumn<T> : GridColumnBase<T> where T : class
    {
        public GridActionColumn(Grid<T> grid) : base(grid)
        {
            Commands = new List<GridActionCommandBase<T>>();
            HtmlBuilder = new GridActionColumnHtmlBuilder<T>(this);
            HeaderHtmlBuilder = new GridColumnHeaderHtmlBuilder<T, GridActionColumn<T>>(this);
        }

        public IList<GridActionCommandBase<T>> Commands
        {
            get;
            private set;
        }
    }
}