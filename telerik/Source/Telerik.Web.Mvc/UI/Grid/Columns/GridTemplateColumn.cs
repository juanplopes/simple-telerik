// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using Infrastructure;

    public class GridTemplateColumn<T> : GridColumnBase<T> where T : class
    {
        public GridTemplateColumn(Grid<T> grid, Action<T> template) : base(grid)
        {
            Guard.IsNotNull(template, "value");

            Template = template;

            HtmlBuilder = new GridTemplateColumnHtmlBuilder<T>(this);
            HeaderHtmlBuilder = new GridColumnHeaderHtmlBuilder<T, GridTemplateColumn<T>>(this);
        }
    }
}