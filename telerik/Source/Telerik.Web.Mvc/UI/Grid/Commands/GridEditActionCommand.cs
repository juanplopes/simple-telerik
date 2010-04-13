// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Extensions;
    using Infrastructure;

    public class GridEditActionCommand<T> : GridActionCommandBase<T> where T : class
    {
        public override string Name
        {
            get { return "edit"; }
        }

        public override void EditModeHtml(IHtmlNode parent, GridCell<T> context)
        {
            #if MVC2

            Grid<T> grid = context.Column.Grid;

            grid.WriteDataKeys(context.DataItem, parent);

            new HtmlTag("button")
                .Attributes(HtmlAttributes)
                .AddClass(UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Update)
                .Attribute("type", "submit")
                .Text(grid.Localization.Update)
                .AppendTo(parent);

            AppendCancelButton(grid, parent);

            #endif
        }

        public override void InsertModeHtml(IHtmlNode parent, GridCell<T> context)
        {
            #if MVC2

            Grid<T> grid = context.Column.Grid;

            grid.WriteDataKeys(context.DataItem, parent);

            new HtmlTag("button")
                .Attributes(HtmlAttributes)
                .AddClass(UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Insert)
                .Attribute("type", "submit")
                .Text(grid.Localization.Insert)
                .AppendTo(parent);

            AppendCancelButton(grid, parent);

            #endif
        }

        public override void BoundModeHtml(IHtmlNode parent, GridCell<T> context)
        {
            Grid<T> grid = context.Column.Grid;
            var urlBuilder = new GridUrlBuilder<T>(grid);

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
                .Text(grid.Localization.Edit)
                .AppendTo(parent);
        }

        private void AppendCancelButton(Grid<T> grid, IHtmlNode parent)
        {
            var urlBuilder = new GridUrlBuilder<T>(grid);

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

                    routeValues.Remove(grid.Prefix(GridUrlParameters.Mode));
                }))
                .Text(grid.Localization.Cancel)
                .AppendTo(parent);
        }
    }
}