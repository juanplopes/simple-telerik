// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Infrastructure;
    
    public class GridEmptyRowHtmlBuilder : HtmlBuilderBase
    {
        private readonly int colspan;

        public GridEmptyRowHtmlBuilder(int colspan)
        {
            this.colspan = colspan;
        }

        protected override IHtmlNode BuildCore()
        {
            IHtmlNode tr = new HtmlTag("tr")
                .AddClass("t-no-data");

            new HtmlTag("td").Attribute("colspan", colspan.ToString())
                .AppendTo(tr);

            return tr;

        }
    }
}
