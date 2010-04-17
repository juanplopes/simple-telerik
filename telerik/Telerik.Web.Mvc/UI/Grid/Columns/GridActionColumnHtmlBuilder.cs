// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Extensions;

    public class GridActionColumnHtmlBuilder<T> : GridColumnHtmlBuilderBase<T, GridActionColumn<T>> where T : class
    {
        public GridActionColumnHtmlBuilder(GridActionColumn<T> column) : base(column)
        {

        }

        public override void Html(GridCell<T> context, IHtmlNode td)
        {
            Column.Commands.Each(command => command.Html(context, td));
        }
    }
}