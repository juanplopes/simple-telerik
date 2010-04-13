namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using System;
    using Xunit;

    using UI;
    using Fluent;
    
    public class GridBoundColumnBuilderTests
    {
        private readonly GridBoundColumn<Customer, int> column;
        private readonly GridBoundColumnBuilder<Customer> builder;
        
        public GridBoundColumnBuilderTests()
        {
            column = new GridBoundColumn<Customer, int>(GridTestHelper.CreateGrid<Customer>(), c=>c.Id);
            builder = new GridBoundColumnBuilder<Customer>(column);
        }

        [Fact]
        public void Header_text_sets_the_header_text_of_the_column()
        {
            builder.Title("Header");

            Assert.Equal("Header", column.Title);
        }

        [Fact]
        public void HeaderHtmlAttributes_sets_the_header_attributes_of_the_column()
        {
            builder.HeaderHtmlAttributes(new { @class="test" });

            Assert.Equal("test", column.HeaderHtmlAttributes["class"]);
        }

        [Fact]
        public void HeaderHtmlAttributes_throws_if_null_passed_as_argument()
        {
            Assert.Throws<ArgumentNullException>(() => builder.HeaderHtmlAttributes(null));
        }

        [Fact]
        public void Format_sets_the_format_string_of_the_column()
        {
            builder.Format("{0}");
            Assert.Equal("{0}", column.Format);
        }

        [Fact]
        public void Sortable_sets_the_sortable_of_the_column()
        {
            builder.Sortable(false);

            Assert.False(column.Sortable);
        }

        [Fact]
        public void Encoded_sets_the_encoded_property()
        {
            builder.Encoded(false);
            Assert.False(column.Encoded);
        }

        [Fact]
        public void HtmlAttributes_sets_the_html_attributes_of_the_column()
        {
            builder.HtmlAttributes(new { @class = "test" });

            Assert.Equal("test", column.HtmlAttributes["class"]);
        }

        [Fact]
        public void HtmlAttributes_throws_if_null_passed_as_argument()
        {
            Assert.Throws<ArgumentNullException>(() => builder.HtmlAttributes(null));
        }

        [Fact]
        public void Template_sets_the_template_of_the_column()
        {
            Action<Customer> template = c => { };
            builder.Template(template);

            Assert.Same(template, column.Template);
        }

        [Fact]
        public void Filterable_sets_the_filterable_property()
        {
            builder.Filterable(false);
            Assert.False(column.Filterable);
        }
    }
}
