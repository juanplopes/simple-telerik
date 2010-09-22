// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Infrastructure;
    
    public class GridPagerHtmlBuilder<T> : HtmlBuilderBase
        where T : class
    {
        private readonly Grid<T> grid;
        
        public GridPagerHtmlBuilder(Grid<T> grid)
        {
            this.grid = grid;
        }

        public virtual IHtmlBuilder CreateButton(string text, bool enabled, string url)
        {
            return new GridPagerButtonHtmlBuilder(text)
            {
                Enabled = enabled,
                Url = url
            };
        }

        private IHtmlNode NumericLink(string text, bool active, int page)
        {
            var urlBuilder = new GridUrlBuilder(grid);

            var buttonBuilder = new GridPagerNumericButtonHtmlBulder(text)
            {
                Active = active,
                Url = urlBuilder.Url(grid.Server.Select, GridUrlParameters.CurrentPage, page)
            };

            return buttonBuilder.Build();
        }

        private IHtmlNode PagerIcon(string text, bool enabled, int page)
        {
            var urlBuilder = new GridUrlBuilder(grid);
            
            var buttonBuilder = new GridPagerButtonHtmlBuilder(text)
            {
                Enabled = enabled,
                Url = urlBuilder.Url(grid.Server.Select, GridUrlParameters.CurrentPage, page)
            };

            return buttonBuilder.Build();
        }

        protected override IHtmlNode BuildCore()
        {
            var div = new HtmlTag("div")
                .AddClass("t-pager", UIPrimitives.ResetStyle);

            bool shouldRenderNextPrev = (grid.Paging.Style & GridPagerStyles.NextPrevious) != 0;

            bool shouldRenderNumeric = (grid.Paging.Style & GridPagerStyles.Numeric) != 0;

            bool shouldRenderPageInput = (grid.Paging.Style & GridPagerStyles.PageInput) != 0;

            if (shouldRenderNextPrev)
            {
                PagerIcon("first", grid.DataProcessor.CurrentPage > 1, 1)
                    .AppendTo(div);

                PagerIcon("prev", grid.DataProcessor.CurrentPage > 1, grid.DataProcessor.CurrentPage - 1)
                    .AppendTo(div);
            }

            if (shouldRenderNumeric)
            {
                IHtmlNode numericDiv = new HtmlTag("div")
                    .AddClass("t-numeric")
                    .AppendTo(div);

                const int NumericLinkSize = 10;

                int pageCount = grid.DataProcessor.PageCount;
                int currentPage = grid.DataProcessor.CurrentPage;

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
                    NumericLink("...", false, numericStart - 1)
                        .AppendTo(numericDiv);
                }

                for (int page = numericStart; page <= numericEnd; page++)
                {
                     NumericLink(page.ToString(), page == currentPage, page)
                         .AppendTo(numericDiv);
                }

                if (numericEnd < pageCount)
                {
                    NumericLink("...", false, numericEnd + 1).AppendTo(numericDiv);
                }
            }

            if (shouldRenderPageInput)
            {
                var inputBuilder = new GridPagerTextBoxHtmlBuilder(grid.Localization) 
                {
                    Value = (grid.DataProcessor.CurrentPage).ToString(),
                    TotalPages = grid.DataProcessor.PageCount
                };
                
                inputBuilder.Build().AppendTo(div);
            }

            if (shouldRenderNextPrev)
            {
                PagerIcon("next", grid.DataProcessor.CurrentPage < grid.DataProcessor.PageCount, grid.DataProcessor.CurrentPage + 1)
                    .AppendTo(div);

                PagerIcon("last", grid.DataProcessor.CurrentPage < grid.DataProcessor.PageCount, grid.DataProcessor.PageCount)
                    .AppendTo(div);
            }

            return div;
        }
    }
}
