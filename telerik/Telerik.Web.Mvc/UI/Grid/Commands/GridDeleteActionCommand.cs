// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Extensions;
    using Infrastructure;

    public class GridDeleteActionCommand : GridActionCommandBase
    {
        public override string Name
        {
            get { return "delete"; }
        }

        public override void EditModeHtml<T>(IHtmlNode parent, IGridRenderingContext<T> context)
        {
            //Nothing in edit mode
        }

        public override void InsertModeHtml<T>(IHtmlNode parent, IGridRenderingContext<T> context)
        {
            //Nothing in insert mode
        }

        public override void BoundModeHtml<T>(IHtmlNode parent, IGridRenderingContext<T> context)
        {
            #if MVC2

            Grid<T> grid = context.Grid;
            GridUrlBuilder urlBuilder = new GridUrlBuilder(grid);

            IHtmlNode form = new HtmlTag("form")
                                 .AddClass(UIPrimitives.Grid.ActionForm)
                                 .Attribute("method", "post")
                                 .Attribute("action", urlBuilder.Url(grid.Server.Delete, routeValues => 
                                     grid.DataKeys.Each(dataKey => routeValues[dataKey.RouteKey] = dataKey.GetValue(context.DataItem))))
                                 .AppendTo(parent);

            IHtmlNode div = new HtmlTag("div").AppendTo(form);

            grid.WriteDataKeys(context.DataItem, div);

            new HtmlTag("button")
                .Attributes(HtmlAttributes)
                .AddClass(UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Delete)
                .Attribute("type", "submit")
                .Html(this.ButtonContent(grid.Localization.Delete, UIPrimitives.Icons.Delete))
                .AppendTo(div);

            #endif
        }
    }
}