// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    public class GridTemplateColumnHtmlBuilder<T> : GridColumnHtmlBuilderBase<T, GridColumnBase<T>> where T : class
    {
        public GridTemplateColumnHtmlBuilder(GridColumnBase<T> column) : base(column)
        {
        }

        public override void Html(GridCell<T> context, IHtmlNode td)
        {
            td.Template(() => Column.Template(context.DataItem));
        }
    }
}