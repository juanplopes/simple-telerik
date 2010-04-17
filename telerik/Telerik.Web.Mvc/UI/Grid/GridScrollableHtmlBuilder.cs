// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.UI;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridScrollableHtmlBuilder<T> : GridHtmlBuilder<T> where T : class
    {
        public GridScrollableHtmlBuilder(Grid<T> grid)
            : base(grid)
        {
        }

        public override IHtmlNode HeadTag(IHtmlNode parent)
        {
            return base.TableTag().AppendTo(
                new HtmlTag("div")
                    .AddClass("t-grid-header-wrap")
                    .AppendTo(
                        new HtmlTag("div")
                            .AddClass("t-grid-header")
                            .AppendTo(parent)));
        }

        public override IHtmlNode BodyTag(IHtmlNode parent)
        {
            return new HtmlTag("tbody").AppendTo(
                TableTag().AppendTo(
                new HtmlTag("div")
                    .AddClass("t-grid-content")
                    .Css("height", Grid.Scrolling.Height)
                    .AppendTo(parent)));
        }

        public override IHtmlNode FootTag(IHtmlNode parent)
        {
            return new HtmlTag("div")
                        .AddClass("t-grid-footer", "t-footer")
                        .AppendTo(parent);
        }
    }
}