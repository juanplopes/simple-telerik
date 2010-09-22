// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Html
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    public class GridGroupIndicatorHtmlBuilder<T> : HtmlBuilderBase
        where T : class
    {
        private readonly Grid<T> grid;
        private readonly GroupDescriptor groupDescriptor;
        
        public GridGroupIndicatorHtmlBuilder(Grid<T> grid, GroupDescriptor groupDescriptor)
        {
            this.grid = grid;
            this.groupDescriptor = groupDescriptor;
        }

        protected override IHtmlNode BuildCore()
        {
            var urlBuilder = new GridUrlBuilder(grid);

            IHtmlNode indicator = new HtmlTag("div").AddClass(UIPrimitives.Grid.GroupIndicator);

            IList<GroupDescriptor> groups = new List<GroupDescriptor>(grid.DataProcessor.GroupDescriptors);

            groupDescriptor.CycleSortDirection();

            IHtmlNode a = new HtmlTag("a")
                .AddClass(UIPrimitives.Link)
                .Attribute("href", urlBuilder.Url(grid.Server.Select,
                        GridUrlParameters.GroupBy,
                        GridDescriptorSerializer.Serialize(groups)))
                .AppendTo(indicator);

            groupDescriptor.CycleSortDirection();

            new HtmlTag("span")
                .AddClass(UIPrimitives.Icon)
                .ToggleClass("t-arrow-up-small", groupDescriptor.SortDirection == ListSortDirection.Ascending)
                .ToggleClass("t-arrow-down-small", groupDescriptor.SortDirection == ListSortDirection.Descending)
                .AppendTo(a);

            new TextNode(grid.GroupTitle(groupDescriptor))
                .AppendTo(a);

            groups.Remove(groupDescriptor);

            IHtmlNode button = new HtmlTag("a")
                .Attribute("href", urlBuilder.Url(grid.Server.Select,
                        GridUrlParameters.GroupBy,
                        GridDescriptorSerializer.Serialize(groups)))
                .AddClass(UIPrimitives.Button, UIPrimitives.DefaultState)
                .AppendTo(indicator);

            new HtmlTag("span")
                .AddClass(UIPrimitives.Icon, UIPrimitives.Icons.GroupDelete)
                .AppendTo(button);

            return indicator;
        }
    }
}
