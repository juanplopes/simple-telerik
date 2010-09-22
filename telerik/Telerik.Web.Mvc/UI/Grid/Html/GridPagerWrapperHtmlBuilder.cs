// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Html
{
    using System;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridPagerWrapperHtmlBuilder<T> : HtmlBuilderBase
        where T : class
    {
        private readonly Grid<T> grid;
        
        public GridPagerWrapperHtmlBuilder(Grid<T> grid)
        {
            this.grid = grid;

            TagName = "div";
        }

        public int Colspan
        {
            get;
            set;
        }

        public string TagName
        {
            get;
            set;
        }
        
        public bool OutputPager
        {
            get;
            set;
        }
        
        protected override IHtmlNode BuildCore()
        {
            var wrapper = new HtmlTag(TagName)
                        .AddClass("t-pager-wrapper")
                        .ToggleAttribute("colspan", Colspan.ToString(), Colspan > 0);
            
            var urlBuilder = new GridUrlBuilder(grid);
            
            var refreshBuilder = new GridRefreshHtmlBuilder(urlBuilder.Url(grid.Server.Select));

            refreshBuilder.Build().AppendTo(wrapper);

            if (OutputPager)
            {
                var pagerBuilder = new GridPagerHtmlBuilder<T>(grid);
                
                pagerBuilder.Build().AppendTo(wrapper);
                
                var pagerStatusBuilder = new GridPagerStatusHtmlBuilder(grid.Localization)
                {
                    Total = grid.DataProcessor.Total,
                    FirstItemInPage = grid.DataProcessor.Total > 0 ? (grid.DataProcessor.CurrentPage - 1) * grid.Paging.PageSize + 1 : 0,
                    LastItemInPage = Math.Min(grid.Paging.PageSize * grid.DataProcessor.CurrentPage, grid.DataProcessor.Total)
                };

                pagerStatusBuilder.Build().AppendTo(wrapper);
            }

            return wrapper;
        }
    }
}
