// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Html
{
    using System.Linq;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    
    public class GridGroupHeaderHtmlBuilder<T> : HtmlBuilderBase
        where T : class
    {
        private readonly Grid<T> grid;

        public GridGroupHeaderHtmlBuilder(Grid<T> grid)
        {
            this.grid = grid;
        }

        protected override IHtmlNode BuildCore()
        {
            IHtmlNode div = new HtmlTag("div")
                .AddClass("t-grouping-header");

            if (grid.DataProcessor.GroupDescriptors.Any())
            {
                grid.DataProcessor.GroupDescriptors.Each(group =>
                    {
                        var groupIndicatorBuilder = new GridGroupIndicatorHtmlBuilder<T>(grid, group);
                        groupIndicatorBuilder .Build().AppendTo(div);
                    });
            }
            else
            {
                div.Html(grid.Localization.GroupHint);
            }

            return div;
        }
    }
}
