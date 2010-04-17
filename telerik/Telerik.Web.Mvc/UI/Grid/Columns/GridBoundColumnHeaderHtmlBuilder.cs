// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using Extensions;
    using Infrastructure;

    public class GridBoundColumnHeaderHtmlBuilder<TModel, TValue> : GridColumnHeaderHtmlBuilder<TModel, GridBoundColumn<TModel, TValue>> where TModel : class
    {
        public GridBoundColumnHeaderHtmlBuilder(GridBoundColumn<TModel, TValue> column) : base(column)
        {
        }

        public override void Html(IHtmlNode parent)
        {
            GridUrlBuilder<TModel> urlGenerator = new GridUrlBuilder<TModel>(Column.Grid);

            if (Column.Sortable && Column.Grid.Sorting.Enabled)
            {
                IList<SortDescriptor> orderBy = new List<SortDescriptor>(Column.Grid.DataProcessor.SortDescriptors);
                SortDescriptor descriptor = orderBy.SingleOrDefault(c => c.Member.IsCaseInsensitiveEqual(Column.Member));

                ListSortDirection? oldDirection = null;

                if (descriptor != null)
                {
                    oldDirection = descriptor.SortDirection;

                    ListSortDirection? newDirection = NextSortDirection(oldDirection);

                    if (newDirection == null)
                    {
                        orderBy.Remove(descriptor);
                    }
                    else
                    {
                        if (Column.Grid.Sorting.SortMode == GridSortMode.SingleColumn)
                        {
                            orderBy.Clear();
                            orderBy.Add(new SortDescriptor { SortDirection = newDirection.Value, Member = descriptor.Member });
                        }
                        else
                        {
                            orderBy[orderBy.IndexOf(descriptor)] = new SortDescriptor { SortDirection = newDirection.Value, Member = descriptor.Member };
                        }
                    }
                }
                else
                {
                    if (Column.Grid.Sorting.SortMode == GridSortMode.SingleColumn)
                    {
                        orderBy.Clear();
                    }

                    orderBy.Add(new SortDescriptor { Member = Column.Member, SortDirection = ListSortDirection.Ascending });
                }

                string url = urlGenerator.Url(Column.Grid.Server.Select, GridUrlParameters.OrderBy, GridDescriptorSerializer.Serialize(orderBy));

                IHtmlNode sortLink = new HtmlTag("a")
                                        .AddClass(UIPrimitives.Link)
                                        .Attribute("href", url)
                                        .AppendTo(parent);

                new TextNode(Column.Title).AppendTo(sortLink);

                if (oldDirection.HasValue)
                {
                    new HtmlTag("span")
                        .ToggleClass("t-arrow-up", oldDirection == ListSortDirection.Ascending)
                        .ToggleClass("t-arrow-down", oldDirection != ListSortDirection.Ascending)
                        .PrependClass(UIPrimitives.Icon)
                        .AppendTo(sortLink);
                }
            }
            else if (!string.IsNullOrEmpty(Column.Title))
            {
                base.Html(parent);
            }

            if (Column.Filterable && Column.Grid.Filtering.Enabled)
            {
                IList<FilterDescriptor> filters = Column.Grid.DataProcessor.FilterDescriptors.SelectRecursive(filter =>
                {
                    CompositeFilterDescriptor compositeDescriptor = filter as CompositeFilterDescriptor;

                    if (compositeDescriptor != null)
                    {
                        return compositeDescriptor.FilterDescriptors;
                    }

                    return null;
                })
                .Where(filter => filter is FilterDescriptor)
                .OfType<FilterDescriptor>()
                .ToList();

                FilterDescriptor descriptor = filters.FirstOrDefault(filter => filter.Member.IsCaseInsensitiveEqual(Column.Member));

                IHtmlNode filterTag = new HtmlTag("div")
                                          .AddClass("t-grid-filter", UIPrimitives.DefaultState)
                                          .ToggleClass("t-active-filter", descriptor != null)
                                          .AppendTo(parent);

                new HtmlTag("span").AddClass(UIPrimitives.Icon, "t-filter").AppendTo(filterTag);
            }
        }

        private static ListSortDirection? NextSortDirection(ListSortDirection? direction)
        {
            if (direction == ListSortDirection.Ascending)
            {
                return ListSortDirection.Descending;
            }

            if (direction == ListSortDirection.Descending)
            {
                return null;
            }

            return ListSortDirection.Ascending;
        }
    }
}