namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using Mvc.UI;
    using Fluent;

    using Xunit;
    using System;

    public class GridPagerSettingsBuilderTests
    {
        private readonly GridPagingSettings pager;
        private readonly GridPagerSettingsBuilder builder;
        
        public GridPagerSettingsBuilderTests()
        {
            pager = new GridPagingSettings();
            builder = new GridPagerSettingsBuilder(pager);
        }

        [Fact]
        public void Position_sets_pager_position()
        {
            builder.Position(GridPagerPosition.Top);

            Assert.Equal(GridPagerPosition.Top, pager.Position);
        }

        [Fact]
        public void PageSize_sets_page_size()
        {
            builder.PageSize(1);

            Assert.Equal(1, pager.PageSize);
        }

        [Fact]
        public void Total_should_fail_on_negative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => builder.Total(-1));
        }

        [Fact]
        public void Total_should_accept_zero()
        {
            builder.Total(0);

            Assert.Equal(0, pager.Total);
        }
    }
}