// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;
    
    public class GridGroupRowHtmlBuilder<T> : HtmlBuilderBase
        where T : class
    {
        private readonly IGroup group;
        private readonly Grid<T> grid;

        public GridGroupRowHtmlBuilder(Grid<T> grid,IGroup group)
        {
            this.grid = grid;
            this.group = group;
        }

        public int Level
        {
            get;
            set;
        }

        protected override IHtmlNode BuildCore()
        {
            var tr = new HtmlTag("tr").AddClass("t-grouping-row");

            IHtmlNode td = new HtmlTag("td").Attribute("colspan", (grid.Colspan - Level).ToString())
                .AppendTo(tr);

            IHtmlNode p = new HtmlTag("p").AddClass(UIPrimitives.ResetStyle).AppendTo(td);

            GroupDescriptor groupDescriptor = grid.DataProcessor.GroupDescriptors[Level];

            new HtmlTag("a").AddClass(UIPrimitives.Icon, "t-collapse").Attribute("href", "#").AppendTo(p);

            new TextNode("{0}: {1}".FormatWith(grid.GroupTitle(groupDescriptor), group.Key)).AppendTo(p);

            return tr;
        }
    }
}
