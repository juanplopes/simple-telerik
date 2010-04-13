namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using UI;
    using Xunit;
    using System;
    
    public class GridBoundColumnTests
    {
        public class Product
        {
            public string Name
            {
                get;
                set;
            }

            public Category Category
            {
                get;
                set;
            }
        }

        public class Category
        {
            public string Name
            {
                get;
                set;
            }

            public User Owner
            {
                get;
                set;
            }
        }

        public class User
        {
            [DisplayName("UserName")]
            [DisplayFormat(DataFormatString="{0}")]
            [ReadOnly(true)]
            public string Name
            {
                get;
                set;
            }
        }

        [Fact]
        public void HeaderText_should_be_extracted_from_expression()
        {
            GridBoundColumn<Customer, int> column = new GridBoundColumn<Customer, int>(GridTestHelper.CreateGrid<Customer>(), c => c.Id);

            Assert.Equal("Id", column.Title);
        }

        [Fact]
        public void Name_should_be_extracted_from_expression()
        {
            GridBoundColumn<Customer, int> column = new GridBoundColumn<Customer, int>(GridTestHelper.CreateGrid<Customer>(), c => c.Id);
            Assert.Equal("Id", column.Member);
        }

        [Fact]
        public void Name_should_be_equal_to_member_when_complex_member_expression_is_supplied()
        {
            GridBoundColumn<Customer, int> column = new GridBoundColumn<Customer, int>(GridTestHelper.CreateGrid<Customer>(), c => c.RegisterAt.Day);

            Assert.Equal("RegisterAt.Day", column.Member);
        }

        [Fact]
        public void Name_should_be_extracted_correctly_from_nested_expression()
        {
            GridBoundColumn<Product, string> column = new GridBoundColumn<Product, string>(GridTestHelper.CreateGrid<Product>(), p => p.Category.Owner.Name);

            Assert.Equal("Category.Owner.Name", column.Member);
        }

        [Fact]
        public void Type_should_be_set()
        {
            GridBoundColumn<Customer, int> column = new GridBoundColumn<Customer, int>(GridTestHelper.CreateGrid<Customer>(), c => c.Id);
            Assert.Equal(typeof(int), column.MemberType);
        }

        [Fact]
        public void Throws_on_invalid_expression()
        {
            Assert.Throws<InvalidOperationException>(() => new GridBoundColumn<Customer, string>(GridTestHelper.CreateGrid<Customer>(), c => c.Id.ToString()));
        }

#if MVC2
        public void Title_format_and_visible_should_be_taken_from_metadata()
        {
            GridBoundColumn<User, string> column = new GridBoundColumn<User, string>(GridTestHelper.CreateGrid<User>(), u => u.Name);

            Assert.Equal("UserName", column.Title);
            Assert.Equal("{0}", column.Format);
            Assert.False(column.Visible);
        }

        public void Readonly_is_populated_from_metadata()
        {
            GridBoundColumn<User, string> column = new GridBoundColumn<User, string>(GridTestHelper.CreateGrid<User>(), u => u.Name);

            Assert.Equal(true, column.ReadOnly);
        }
#endif
    }
}