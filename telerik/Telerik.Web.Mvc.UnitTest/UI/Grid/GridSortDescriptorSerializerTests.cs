namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Xunit;

    using Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    
    public class GridSortDescriptorSerializerTests
    {
        [Fact]
        public void Should_serialize_columns()
        {
            IEnumerable<SortDescriptor> descriptors = new[]{
                new  SortDescriptor {
                    Member = "Column1",
                    SortDirection = ListSortDirection.Ascending
                },
                new SortDescriptor {
                    Member = "Column2",
                    SortDirection = ListSortDirection.Descending
                }
            };

            Assert.Equal("Column1-asc~Column2-desc", GridDescriptorSerializer.Serialize(descriptors));
        }

        [Fact]
        public void Should_deserialize_columns()
        {
            IList<SortDescriptor> descriptors = GridDescriptorSerializer.Deserialize<SortDescriptor>("Column1-asc~Column2-desc");

            Assert.Equal(descriptors[0].Member, "Column1");
            Assert.Equal(descriptors[0].SortDirection, ListSortDirection.Ascending);
            Assert.Equal(descriptors[1].Member, "Column2");
            Assert.Equal(descriptors[1].SortDirection, ListSortDirection.Descending);
        }

        [Fact]
        public void Deserialize_empty_data_returns_empty_list()
        {
            Assert.True(GridDescriptorSerializer.Deserialize<SortDescriptor>("").IsEmpty());
        }

        [Fact]
        public void Deserialize_data_without_separators_returns_empty_list()
        {
            Assert.True(GridDescriptorSerializer.Deserialize<SortDescriptor>("Column 1 asc Column2 desc").IsEmpty());
        }
    }
}
