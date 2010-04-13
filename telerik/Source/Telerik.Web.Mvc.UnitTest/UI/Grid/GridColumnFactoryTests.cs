namespace Telerik.Web.Mvc.UI.Fluent.Tests
{
    using System.Data;
    using Telerik.Web.Mvc.UI.UnitTest;
    using Telerik.Web.Mvc.UI.UnitTest.Grid;
    using Xunit;

    public class GridColumnFactoryTests
    {
        private static GridColumnFactory<T> Factory<T>()
            where T : class
        {
            return new GridColumnFactory<T>(GridTestHelper.CreateGrid<T>());
        }
        
        [Fact]
        public void Bound_column_by_name_and_type_data_row()
        {
            DataTable dataTable = new DataTable
            {
                Columns =
                {
                    new DataColumn("ID", typeof(int)),
                    new DataColumn("Name", typeof(string))
                }
            };

            GridColumnFactory<DataRow> factory = Factory<DataRow>();

            var builder = factory.Bound(typeof(int), "ID");

            Assert.Equal("ID", builder.Column.Title);

            dataTable.Rows.Add(1, "Test");

            Assert.Equal(1, ((GridBoundColumn<DataRow, int?>)builder.Column).Value(dataTable.Rows[0]));
        }

        [Fact]
        public void Bound_column_by_name()
        {
            Customer customer = new Customer();
            customer.Id = 1;

            GridColumnFactory<Customer> factory = Factory<Customer>();

            var builder = factory.Bound("Id");

            Assert.IsAssignableFrom<GridBoundColumn<Customer, int>>(builder.Column);

            Assert.Equal(1, ((GridBoundColumn<Customer, int>)builder.Column).Value(customer));
        }
    }
}
