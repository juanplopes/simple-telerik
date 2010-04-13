namespace Telerik.Web.Mvc.UI.Fluent.UnitTest
{
    using System;
    using Telerik.Web.Mvc.UI.UnitTest;
    using Telerik.Web.Mvc.UI.UnitTest.Grid;
    using System.IO;
    using System.Web.UI;
    using System.Collections.Generic;

    using Xunit;
    using Moq;

    using UI;
    using System.Linq.Expressions;

    public class GridBuilderTests
    {
        private readonly Grid<Customer> grid;
        private readonly GridBuilder<Customer> builder;

        public GridBuilderTests()
        {
            Mock<HtmlTextWriter> writer = new Mock<HtmlTextWriter>(TextWriter.Null);
            grid = GridTestHelper.CreateGrid<Customer>(writer.Object, null);
            builder = new GridBuilder<Customer>(grid);
        }

        [Fact]
        public void Pager_enables_paging()
        {
            builder.Pageable();

            Assert.True(grid.Paging.Enabled);
        }
        
        [Fact]
        public void Pager_throws_when_action_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => builder.Pageable(null));
        }

        [Fact]
        public void BindTo_sets_the_data_source()
        {
            IEnumerable<Customer> customers = new [] { new Customer()};
            builder.BindTo(customers);

            Assert.Same(customers, grid.DataSource);
        }

        [Fact]
        public void Columns_builds_the_columns_of_the_grid()
        {
            builder.Columns(columns => columns.Bound(c => c.Id));

            Assert.Equal(1, grid.Columns.Count);
        }

        [Fact]
        public void Columns_throws_if_null_supplied_as_argument()
        {
            Assert.Throws<ArgumentNullException>(() => builder.Columns(null));
        }

        [Fact]
        public void ProcessDataSource_sets_the_corresponding_property()
        {
            builder.EnableCustomBinding(false);

            Assert.Equal(false, grid.EnableCustomBinding);
        }

        [Fact]
        public void PrefixUrlParameters_sets_the_corresponding_property()
        {
            builder.PrefixUrlParameters(false);
            Assert.Equal(false, grid.PrefixUrlParameters);
        }

        [Fact]
        public void Sortable_sets_the_corresponding_property()
        {
            builder.Sortable();
            Assert.Equal(GridSortMode.SingleColumn, grid.Sorting.SortMode);
        }

        [Fact]
        public void Should_add_data_keys()
        {
            Expression<Func<Customer, int>> expression = c => c.Id;

            builder.DataKeys(keys =>
            {
                keys.Add(expression).RouteKey("customerId");
            });

            Assert.Same(expression, ((GridDataKey<Customer, int>)grid.DataKeys[0]).Expression);
            Assert.Same("customerId", grid.DataKeys[0].RouteKey);
        }
    }
}