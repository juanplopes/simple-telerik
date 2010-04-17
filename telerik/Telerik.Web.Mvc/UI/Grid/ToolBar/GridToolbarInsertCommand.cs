// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using Infrastructure;

    public class GridToolBarInsertCommand<T> : GridToolBarCommandBase<T> where T : class
    {
        public override void Html(Grid<T> context, IHtmlNode parent)
        {
            GridUrlBuilder<T> urlBuilder = new GridUrlBuilder<T>(context);

            new HtmlTag("a")
                .Attributes(HtmlAttributes)
                .AddClass(UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Add)
                .Attribute("href", urlBuilder.Url(context.Server.Select, routeValues =>
                {
                    routeValues[context.Prefix(GridUrlParameters.Mode)] = "insert";
                }))
                .Text(context.Localization.AddNew)
                .AppendTo(parent);
        }
    }
}