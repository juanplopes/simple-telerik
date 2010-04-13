// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web.Mvc;

    using Extensions;
    using Infrastructure;
    using Infrastructure.Implementation;

    public class GridHtmlBuilder<T> : IGridHtmlBuilder<T> where T : class
    {
        public GridHtmlBuilder(Grid<T> grid)
        {
            Guard.IsNotNull(grid, "grid");

            Grid = grid;
            UrlBuilder = new GridUrlBuilder<T>(grid);
        }

        protected GridUrlBuilder<T> UrlBuilder
        {
            get;
            private set;
        }

        protected Grid<T> Grid
        {
            get;
            private set;
        }

        public IHtmlNode GridTag()
        {
            return new HtmlTag("div")
                .Attribute("id", Grid.Id)
                .Attributes(Grid.HtmlAttributes)
                .PrependClass(UIPrimitives.Widget, "t-grid");
        }
        
        public IHtmlNode GroupIndicatorTag(GroupDescriptor groupDescriptor)
        {
            IHtmlNode indicator = new HtmlTag("div").AddClass(UIPrimitives.Grid.GroupIndicator);

            IList<GroupDescriptor> groups = new List<GroupDescriptor>(Grid.DataProcessor.GroupDescriptors);
            
            groupDescriptor.CycleSortDirection();

            IHtmlNode a = new HtmlTag("a")
                .AddClass(UIPrimitives.Link)
                .Attribute("href", UrlBuilder.Url(Grid.Server.Select,
                        GridUrlParameters.GroupBy,
                        GridDescriptorSerializer.Serialize(groups)))
                .AppendTo(indicator);

            groupDescriptor.CycleSortDirection();

            new HtmlTag("span")
                .AddClass(UIPrimitives.Icon)
                .ToggleClass("t-arrow-up-small", groupDescriptor.SortDirection == ListSortDirection.Ascending)
                .ToggleClass("t-arrow-down-small", groupDescriptor.SortDirection == ListSortDirection.Descending)
                .AppendTo(a);

            new TextNode(Grid.GroupTitle(groupDescriptor))
                .AppendTo(a);

            groups.Remove(groupDescriptor);
            
            IHtmlNode button = new HtmlTag("a")
                .Attribute("href", UrlBuilder.Url(Grid.Server.Select,
                        GridUrlParameters.GroupBy,
                        GridDescriptorSerializer.Serialize(groups)))
                .AddClass(UIPrimitives.Button, UIPrimitives.DefaultState)
                .AppendTo(indicator);

            new HtmlTag("span")
                .AddClass(UIPrimitives.Icon, "t-delete")
                .AppendTo(button);

            return indicator;
        }

        public IHtmlNode GroupRowTag(IGroup group, int level)
        {
            IHtmlNode tr = new HtmlTag("tr").AddClass("t-grouping-row");

            for (int i = 0; i < level; i++)
            {
                new HtmlTag("td").AddClass(UIPrimitives.Grid.GroupCell).Html("&nbsp;").AppendTo(tr);
            }
            
            IHtmlNode td = new HtmlTag("td").Attribute("colspan", (Grid.Colspan - level).ToString())
                .AppendTo(tr);

            IHtmlNode p = new HtmlTag("p").AddClass(UIPrimitives.ResetStyle).AppendTo(td);

            GroupDescriptor groupDescriptor = Grid.DataProcessor.GroupDescriptors[level];

            new HtmlTag("a").AddClass(UIPrimitives.Icon, "t-collapse").Attribute("href", "#").AppendTo(p);
            
            new TextNode("{0}: {1}".FormatWith(Grid.GroupTitle(groupDescriptor), group.Key)).AppendTo(p);

            return tr;
        }

        public IHtmlNode TableTag()
        {
            IHtmlNode table = new HtmlTag("table")
                .Attribute("cellspacing", "0");
            
            ColGroupTag().AppendTo(table);

            return table;
        }

        public IHtmlNode EmptyRowTag()
        {
            IHtmlNode tr = new HtmlTag("tr")
                .AddClass("t-no-data");

            new HtmlTag("td").Attribute("colspan", Grid.Colspan.ToString())
                .AppendTo(tr);
            
            return tr;
        }
        
        public virtual IHtmlNode HeadTag(IHtmlNode parent)
        {
            return new HtmlTag("thead")
                .AppendTo(parent);
        }

        public IHtmlNode RowTag()
        {
            return new HtmlTag("tr");
        }

        public virtual IHtmlNode BodyTag(IHtmlNode parent)
        {
            return new HtmlTag("tbody")
                .AppendTo(parent);
        }

        public IHtmlNode HeadCellTag(GridColumnBase<T> column)
        {
            IHtmlNode th = new HtmlTag("th")
                .Attribute("scope", "col")
                .Attributes(column.HeaderHtmlAttributes)
                .PrependClass(UIPrimitives.Header);

            if (column == Grid.VisibleColumns.Last())
            {
                th.PrependClass(UIPrimitives.LastHeader);
            }

            column.HeaderHtmlBuilder.Html(th);

            return th;
        }

        public IHtmlNode PagerTag()
        {
            IHtmlNode pagerDiv = new HtmlTag("div")
                .AddClass("t-pager", UIPrimitives.ResetStyle);

            bool shouldRenderNextPrev = (Grid.Paging.Style & GridPagerStyles.NextPrevious) != 0;

            bool shouldRenderNumeric = (Grid.Paging.Style & GridPagerStyles.Numeric) != 0;

            bool shouldRenderPageInput = (Grid.Paging.Style & GridPagerStyles.PageInput) != 0;

            if (shouldRenderNextPrev)
            {
                PagerIcon("first", Grid.DataProcessor.CurrentPage > 1, 1)
                    .AppendTo(pagerDiv);

                PagerIcon("prev", Grid.DataProcessor.CurrentPage > 1, Grid.DataProcessor.CurrentPage - 1)
                    .AppendTo(pagerDiv);
            }

            if (shouldRenderNumeric)
            {
                IHtmlNode numericDiv = new HtmlTag("div")
                    .AddClass("t-numeric")
                    .AppendTo(pagerDiv);

                const int NumericLinkSize = 10;
                
                int pageCount = Grid.DataProcessor.PageCount;
                int currentPage = Grid.DataProcessor.CurrentPage;

                int numericStart = 1;

                if (currentPage > NumericLinkSize)
                {
                    int reminder = (currentPage % NumericLinkSize);

                    numericStart = (reminder == 0) ?
                                   (currentPage - NumericLinkSize) + 1 :
                                   (currentPage - reminder) + 1;
                }

                int numericEnd = (numericStart + NumericLinkSize) - 1;

                if (numericEnd > pageCount)
                {
                    numericEnd = pageCount;
                }

                if (numericStart > 1)
                {
                    NumericLink("...", numericStart - 1)
                        .AppendTo(numericDiv);
                }

                for (int page = numericStart; page <= numericEnd; page++)
                {
                    if (page == currentPage)
                    {
                        new HtmlTag("span").AddClass(UIPrimitives.ActiveState)
                            .Text(page.ToString()).AppendTo(numericDiv);
                    }
                    else
                    {
                        NumericLink(page.ToString(), page)
                            .AppendTo(numericDiv);
                    }
                }

                if (numericEnd < pageCount)
                {
                    NumericLink("...", numericEnd + 1).AppendTo(numericDiv);
                }
            }

            if (shouldRenderPageInput)
            {
                IHtmlNode pageInputDiv = new HtmlTag("div")
                    .AddClass("t-page-i-of-n").AppendTo(pagerDiv);

                new TextNode(Grid.Localization.Page).AppendTo(pageInputDiv);
                new TextNode(" ").AppendTo(pageInputDiv);

                new HtmlTag("input", TagRenderMode.SelfClosing).Attribute("value", Grid.DataProcessor.CurrentPage.ToString())
                    .AppendTo(pageInputDiv);

                new TextNode(" ").AppendTo(pageInputDiv);
                new TextNode(Grid.Localization.PageOf.FormatWith(Grid.DataProcessor.PageCount)).AppendTo(pageInputDiv);
                new TextNode(" ").AppendTo(pageInputDiv);
            }

            if (shouldRenderNextPrev)
            {
                PagerIcon("next", Grid.DataProcessor.CurrentPage < Grid.DataProcessor.PageCount, Grid.DataProcessor.CurrentPage + 1)
                    .AppendTo(pagerDiv);

                PagerIcon("last", Grid.DataProcessor.CurrentPage < Grid.DataProcessor.PageCount, Grid.DataProcessor.PageCount)
                    .AppendTo(pagerDiv);
            }

            return pagerDiv;
        }

        public IHtmlNode PagerStatusTag()
        {
            int total = Grid.DataProcessor.Total;

            int start = total > 0 ? (Grid.DataProcessor.CurrentPage - 1) * Grid.Paging.PageSize + 1 : 0;
            int end = Math.Min(Grid.Paging.PageSize * Grid.DataProcessor.CurrentPage, total);

            IHtmlNode statusDiv = new HtmlTag("div").AddClass("t-status-text");

            new TextNode(Grid.Localization.DisplayingItems.FormatWith(start, end, total)).AppendTo(statusDiv);

            return statusDiv;
        }

        public virtual IHtmlNode FootTag(IHtmlNode parent)
        {
            IHtmlNode tfoot =  new HtmlTag("tfoot").AppendTo(parent);
            IHtmlNode tr = RowTag().AppendTo(tfoot);
            return FootCellTag().AppendTo(tr); 
        }

        public IHtmlNode FootCellTag()
        {
            return new HtmlTag("td").AddClass("t-footer")
                .Attribute("colspan", Grid.Colspan.ToString());
        }
        
        public IHtmlNode RowTag(GridRow<T> context)
        {
            IHtmlNode tr = RowTag()
                .Attributes(context.HtmlAttributes);

            if (context.IsAlternate)
            {
                tr.PrependClass("t-alt");
            }
            
            if (context.Selected)
            {
                tr.PrependClass(UIPrimitives.SelectedState);
            }

            return tr;
        }

        public IHtmlNode EditFormTag(IHtmlNode tr, INavigatable navigatable)
        {
            IHtmlNode td = new HtmlTag("td")
                .AddClass(UIPrimitives.Grid.EditingContainer)
                .Attribute("colspan", Grid.Colspan.ToString())
                .AppendTo(tr);
            
            IHtmlNode form = new HtmlTag("form")
                .AddClass(UIPrimitives.Grid.EditingForm)
                .Attribute("id", Grid.Name + "form")
                .Attribute("method", "post")
                .Attribute("action", UrlBuilder.Url(navigatable, routeValues =>
                {
                    
                }))
                .AppendTo(td);

            IHtmlNode table = TableTag()
                .AppendTo(form);

            return RowTag().AppendTo(table);
        }

        public IHtmlNode CellTag(GridCell<T> context)
        {
            IHtmlNode td = new HtmlTag("td")
                .Attributes(context.Column.HtmlAttributes)
                .Attributes(context.HtmlAttributes);

            if (context.Column == Grid.VisibleColumns.Last())
            {
                td.PrependClass(UIPrimitives.Last);
            }

            if (context.Content != null)
            {
                td.Template(() => context.Content(context.DataItem));
            }
            else if (!string.IsNullOrEmpty(context.Text))
            {
                td.Text(context.Text);
            }
            else
            {
                context.Column.HtmlBuilder.Html(context, td);
            }
            
            return td;
        }

        public IHtmlNode LoadingIndicatorTag()
        {
            IHtmlNode div = new HtmlTag("div").AddClass("t-status");

            new HtmlTag("a").AddClass(UIPrimitives.Icon, "t-refresh")
                .Attribute("href", "#").AppendTo(div);

            return div;
        }

        public IHtmlNode ColGroupTag()
        {
            IHtmlNode colGroup = new HtmlTag("colgroup");

            Grid.DataProcessor.GroupDescriptors.Each(group => new HtmlTag("col", TagRenderMode.SelfClosing)
                .AddClass(UIPrimitives.Grid.GroupCol)
                .AppendTo(colGroup));

            Grid.VisibleColumns.Each(column => 
                new HtmlTag("col", TagRenderMode.SelfClosing)
                   .ToggleAttribute("style", "width:" + column.Width, column.Width.HasValue())
                   .ToggleAttribute("style", "display:none;", column.Hidden)
                   .AppendTo(colGroup)
            );

            return colGroup;
        }

        private IHtmlNode PagerIcon(string text, bool enabled, int page)
        {
            IHtmlNode a = new HtmlTag("a")
                .AddClass(UIPrimitives.Link)
                .Attribute("href", "#")
                .ToggleClass(UIPrimitives.DisabledState, !enabled)
                .ToggleAttribute("href", UrlBuilder.Url(Grid.Server.Select, GridUrlParameters.CurrentPage, page), enabled);

            new HtmlTag("span").AddClass(UIPrimitives.Icon, "t-arrow-" + text).Text(text).AppendTo(a);
            
            return a;
        }

        private IHtmlNode NumericLink(string text, int page)
        {
            return new HtmlTag("a")
                .AddClass(UIPrimitives.Link)
                .Attribute("href", UrlBuilder.Url(Grid.Server.Select, GridUrlParameters.CurrentPage, page))
                .Text(text);
        }
    }
}