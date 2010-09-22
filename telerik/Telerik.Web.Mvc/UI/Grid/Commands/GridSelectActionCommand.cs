// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Infrastructure;
    using Telerik.Web.Mvc.Extensions;

    public class GridSelectActionCommand : GridActionCommandBase
    {
        public override string Name
        {
            get { return "select"; }
        }

        public override void EditModeHtml<T>(IHtmlNode parent, IGridRenderingContext<T> context)
        {
        }

        public override void InsertModeHtml<T>(IHtmlNode parent, IGridRenderingContext<T> context)
        {
        }

        public override void BoundModeHtml<T>(IHtmlNode parent, IGridRenderingContext<T> context)
        {
            #if MVC2
            Grid<T> grid = context.Grid;
            var urlBuilder = new GridUrlBuilder(grid);

            new HtmlTag("a")
                .Attributes(HtmlAttributes)
                .AddClass(UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Select)
                .Attribute("href", urlBuilder.Url(grid.Server.Select, routeValues =>
                {
                    grid.DataKeys.Each(dataKey =>
                    {
                        routeValues[dataKey.RouteKey] = dataKey.GetValue(context.DataItem);
                    });
                    routeValues[grid.Prefix(GridUrlParameters.Mode)] = "select";
                }))
                .Html(this.ButtonContent(grid.Localization.Select, "t-select"))
                .AppendTo(parent);
            #endif
        }
    }
}