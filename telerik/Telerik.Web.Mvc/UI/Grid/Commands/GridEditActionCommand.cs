// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Extensions;
    using Infrastructure;

    public class GridEditActionCommand : GridActionCommandBase
    {
        public override string Name
        {
            get { return "edit"; }
        }

        public override void EditModeHtml<T>(IHtmlNode parent, IGridRenderingContext<T> context)
        {
            #if MVC2

            Grid<T> grid = context.Grid;

            grid.WriteDataKeys(context.DataItem, parent);

            new HtmlTag("button")
                .Attributes(HtmlAttributes)
                .AddClass(UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Update)
                .Attribute("type", "submit")
                .Html(this.ButtonContent(grid.Localization.Update, "t-update"))
                .AppendTo(parent);

            AppendCancelButton(grid, parent);

            #endif
        }

        public override void InsertModeHtml<T>(IHtmlNode parent, IGridRenderingContext<T> context)
        {
            #if MVC2

            Grid<T> grid = context.Grid;

            grid.WriteDataKeys(context.DataItem, parent);

            new HtmlTag("button")
                .Attributes(HtmlAttributes)
                .AddClass(UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Insert)
                .Attribute("type", "submit")
                .Html(this.ButtonContent(grid.Localization.Insert, "t-insert"))
                .AppendTo(parent);

            AppendCancelButton(grid, parent);

            #endif
        }

        public override void BoundModeHtml<T>(IHtmlNode parent, IGridRenderingContext<T> context)
        {
            #if MVC2
            Grid<T> grid = context.Grid;
            var urlBuilder = new GridUrlBuilder(grid);

            new HtmlTag("a")
                .Attributes(HtmlAttributes)
                .AddClass(UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Edit)
                .Attribute("href", urlBuilder.Url(grid.Server.Select, routeValues =>
                {
                     grid.DataKeys.Each(dataKey =>
                     {
                         routeValues[dataKey.RouteKey] = dataKey.GetValue(context.DataItem);
                     });

                    routeValues[grid.Prefix(GridUrlParameters.Mode)] = "edit";
                }))
                .Html(this.ButtonContent(grid.Localization.Edit, "t-edit"))
                .AppendTo(parent);
            #endif
        }

        #if MVC2
        private void AppendCancelButton<T>(Grid<T> grid, IHtmlNode parent) where T : class
        {
            var urlBuilder = new GridUrlBuilder(grid);

            new HtmlTag("a")
                .Attributes(HtmlAttributes)
                .AddClass(UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Cancel)
                .Attribute("href", urlBuilder.Url(grid.Server.Select, routeValues =>
                {
                    grid.DataKeys.Each(dataKey =>
                    {
                        if (routeValues.ContainsKey(dataKey.RouteKey))
                        {
                            routeValues[dataKey.RouteKey] = string.Empty;
                        }
                    });
                    routeValues.Merge(grid.Server.Select.RouteValues);
                    routeValues.Remove(grid.Prefix(GridUrlParameters.Mode));
                }))
                .Html(this.ButtonContent(grid.Localization.Cancel, "t-cancel"))
                .AppendTo(parent);
        }
#endif
    }
}