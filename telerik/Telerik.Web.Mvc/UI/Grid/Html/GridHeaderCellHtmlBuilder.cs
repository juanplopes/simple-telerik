// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridHeaderCellHtmlBuilder : HtmlBuilderBase
    {
        private readonly IGridColumn column;

        public GridHeaderCellHtmlBuilder(IGridColumn column)
        {
            Guard.IsNotNull(column, "column");

            this.column = column;
        }

        protected override IHtmlNode BuildCore()
        {
            var title = column.Title.HasValue() ? column.Title : "&nbsp;";

            return new HtmlTag("th")
                .Attributes(column.HeaderHtmlAttributes)
                .ToggleClass(UIPrimitives.LastHeader, column.IsLast)
                .AddClass(UIPrimitives.Header)
                .Attribute("scope", "col")
                .Html(title);
        }
    }
}
