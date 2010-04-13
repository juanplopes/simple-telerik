// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Extensions;
    using Infrastructure;

    public class GridSelectActionCommand<T> : GridActionCommandBase<T> where T : class
    {
        public override string Name
        {
            get { return "select"; }
        }

        public override void EditModeHtml(IHtmlNode parent, GridCell<T> context)
        {
        }

        public override void InsertModeHtml(IHtmlNode parent, GridCell<T> context)
        {
        }

        public override void BoundModeHtml(IHtmlNode parent, GridCell<T> context)
        {
            Grid<T> grid = context.Column.Grid;
            var urlBuilder = new GridUrlBuilder<T>(grid);

            new HtmlTag("a")
                .Attributes(HtmlAttributes)
                .AddClass(UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Select)
                .Attribute("href", urlBuilder.Url(grid.Server.Select, routeValues =>
                {
                    grid.DataKeys.Each(dataKey =>
                    {
                        routeValues[dataKey.RouteKey] = dataKey.GetValue(context.DataItem);
                    });
                    routeValues[grid.Prefix(GridUrlParameters.Mode)] ="select";
                }))
                .Text(grid.Localization.Select)
                .AppendTo(parent);
        }
    }
}