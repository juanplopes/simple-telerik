// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Infrastructure;

    public class GridRefreshHtmlBuilder : HtmlBuilderBase
    {
        private readonly string url;
        
        public GridRefreshHtmlBuilder(string url)
        {
            this.url = url;
        }
        
        protected override IHtmlNode BuildCore()
        {
            var div = new HtmlTag("div")
                .AddClass("t-status");

            var a = new HtmlTag("a")
                .AddClass(UIPrimitives.Icon, "t-refresh")
                .Attribute("href", url);

            a.AppendTo(div);

            return div;
        }
    }
}
