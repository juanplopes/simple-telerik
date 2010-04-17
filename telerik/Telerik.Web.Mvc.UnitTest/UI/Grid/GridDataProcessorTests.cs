namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Xunit;
    using Moq;

    public class GridDataProcessorTests
    {
        private readonly GridDataProcessor dataProcessor;
        private readonly Mock<IGridBindingContext> context;
        private readonly IDictionary<string, ValueProviderResult> valueProvider;
        
        public GridDataProcessorTests()
        {
            valueProvider = new Dictionary<string, ValueProviderResult>();

            context = new Mock<IGridBindingContext>();
            context.Setup(c => c.Prefix(GridUrlParameters.OrderBy)).Returns(GridUrlParameters.OrderBy);
            context.Setup(c => c.Prefix(GridUrlParameters.GroupBy)).Returns(GridUrlParameters.GroupBy);
            context.Setup(c => c.Prefix(GridUrlParameters.CurrentPage)).Returns(GridUrlParameters.CurrentPage);
            context.Setup(c => c.Prefix(GridUrlParameters.Filter)).Returns(GridUrlParameters.Filter);
            context.Setup(c => c.GroupDescriptors).Returns(() => new GroupDescriptor[]{});
            context.Setup(c => c.SortDescriptors).Returns(() => new SortDescriptor[]{});
            context.Setup(c => c.FilterDescriptors).Returns(() => new CompositeFilterDescriptor[] { });

            context.SetupGet(c => c.Controller).Returns(new ControllerTestDouble(valueProvider, new ViewDataDictionary()));
            context.Setup(c => c.PageSize).Returns(10);
            dataProcessor = new GridDataProcessor(context.Object);
        }

        [Fact]
        public void Total_returns_zero_if_data_source_is_null()
        {
            Assert.Equal(0, dataProcessor.Total);
        }

        [Fact]
        public void Total_returns_zero_if_data_source_is_empty()
        {
            context.Setup(c => c.DataSource).Returns(new object[] { });

            Assert.Equal(0, dataProcessor.Total);
        }

        [Fact]
        public void PageCount_returns_one_if_data_source_is_null_or_empty()
        {
            Assert.Equal(1, dataProcessor.PageCount);
        }

        [Fact]
        public void PageCount_returns_one_if_page_size_is_zero()
        {
            context.SetupGet(c => c.PageSize).Returns(0);

            Assert.Equal(1, dataProcessor.PageCount);
        }

        [Fact]
        public void Should_return_page_count_when_data_source_items_are_less_than_page_size()
        {
            context.SetupGet(c => c.DataSource).Returns(DataSource(8));

            Assert.Equal(1, dataProcessor.PageCount);
        }

        [Fact]
        public void Should_return_page_count_when_data_source_items_are_more_than_page_size()
        {
            context.SetupGet(c => c.DataSource).Returns(DataSource(11));

            Assert.Equal(2, dataProcessor.PageCount);
        }

        [Fact]
        public void Should_return_page_count_when_data_source_items_are_same_as_page_size()
        {
            context.SetupGet(c => c.DataSource).Returns(DataSource(10));

            Assert.Equal(1, dataProcessor.PageCount);
        }

        [Fact]
        public void Should_sort_if_sort_descriptors_are_specified()
        {
            IList<Customer> dataSource = new[] {
                new Customer {Name = "B"},
                new Customer {Name = "A"}
            };

            valueProvider.Add(GridUrlParameters.OrderBy, "Name-asc");
            context.SetupGet(c => c.DataSource).Returns(dataSource);
            IEnumerable<Customer> processedDataSource = dataProcessor.ProcessedDataSource.Cast<Customer>();

            Assert.Equal("A", processedDataSource.First().Name);
        }

        [Fact]
        public void Should_filter_if_filter_descriptors_are_specified()
        {
            IList<Customer> dataSource = new[] {
                new Customer {Name = "A"},
                new Customer {Name = "B"}
            };

            valueProvider.Add(GridUrlParameters.Filter, "startswith(Name,'A')");
            context.SetupGet(c => c.DataSource).Returns(dataSource);

            IEnumerable<Customer> processedDataSource = dataProcessor.ProcessedDataSource.Cast<Customer>();
            Assert.Equal(1, processedDataSource.Count());
            Assert.Equal("A", processedDataSource.First().Name);
        }

        [Fact]
        public void Should_page_if_current_page_is_specified_and_paging_is_enabled()
        {
            valueProvider.Add(GridUrlParameters.CurrentPage, 2);
            context.SetupGet(c => c.DataSource).Returns(DataSource(20));

            IEnumerable<int> processedDataSource = dataProcessor.ProcessedDataSource.Cast<int>();

            Assert.Equal(10, processedDataSource.Count());
        }

        [Fact]
        public void Should_not_process_data_source_if_enable_custom_binding_is_true()
        {
            IEnumerable dataSource = DataSource(20);
            context.SetupGet(c => c.EnableCustomBinding).Returns(true);
            context.SetupGet(c => c.DataSource).Returns(dataSource);

            Assert.Same(dataSource, dataProcessor.ProcessedDataSource);
        }

        private static IEnumerable DataSource(int howMany)
        {
            for (int i = 0; i < howMany; i++)
            {
                yield return i;
            }
        }
    }
}