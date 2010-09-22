// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.Routing;

    using Infrastructure;

    public class GridToolBarCustomCommand<T> : GridToolBarCommandBase<T>, INavigatable where T : class
    {
        public GridToolBarCustomCommand()
        {
            RouteValues = new RouteValueDictionary();
        }

        public string RouteName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public RouteValueDictionary RouteValues { get; private set; }

        public string Url { get; set; }

        public string Text { get; set; }

        public override void Html(Grid<T> context, IHtmlNode parent)
        {
            #if MVC2
            GridUrlBuilder urlBuilder = new GridUrlBuilder(context);

            new HtmlTag("a")
                .Attributes(HtmlAttributes)
                .AddClass(UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState)
                .Attribute("href", urlBuilder.Url(this))
                .Html(this.ButtonContent<T>(Text, null))
                .AppendTo(parent);
            #endif
        }
    }
}