namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using System.IO;
    using System.Web.UI;
	
    using UI;
    
    using Moq;
    using Xunit;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridHtmlBuilderTests
    {
        private readonly Grid<Customer> grid;
        private readonly Mock<HtmlTextWriter> writer;
        private readonly GridHtmlBuilder<Customer> renderer;

        public GridHtmlBuilderTests()
        {
            var virtualPathProvider = new Mock<IVirtualPathProvider>();
            virtualPathProvider.Setup(vpp => vpp.FileExists(It.IsAny<string>())).Returns(false);

            var serviceLocator = new Mock<IServiceLocator>();
            serviceLocator.Setup(sl => sl.Resolve<IVirtualPathProvider>()).Returns(virtualPathProvider.Object);

            ServiceLocator.SetCurrent(() => serviceLocator.Object);

            writer = new Mock<HtmlTextWriter>(TextWriter.Null);
            
            grid = GridTestHelper.CreateGrid<Customer>(writer.Object, null);
            grid.DataSource = new[] { new Customer { Id = 1, Name = "John Doe" } };
            
            // Setting format to avoid exceptions caused by DisplayFor
            grid.Columns.Add(new GridBoundColumn<Customer, int>(grid, c => c.Id)
                {
                    Format = "{0}"
                });

            grid.Columns.Add(new GridBoundColumn<Customer, string>(grid, c => c.Name)
                {
                    Format = "{0}"
                });

            renderer = new GridHtmlBuilder<Customer>(grid);
        }
        
        [Fact]
        public void Should_output_start_tag()
        {
            IHtmlNode tag = renderer.GridTag();

            Assert.Equal(grid.Id, tag.Attribute("id"));
            Assert.Equal("t-widget t-grid", tag.Attribute("class"));
            Assert.Equal("div", tag.TagName);
        }
        
        [Fact]
        public void TableStart_otputs_table()
        {
            IHtmlNode tag = renderer.TableTag();

            Assert.Equal("table", tag.TagName);
        }

        [Fact]
        public void HeaderStart_outputs_thead()
        {
            IHtmlNode tag = renderer.HeadTag(new HtmlTag("div"));
            Assert.Equal("thead", tag.TagName);
        }

        [Fact]
        public void HeaderRowStart_outputs_tr()
        {
            IHtmlNode tag = renderer.RowTag();
            Assert.Equal("tr", tag.TagName);
        }

        [Fact]
        public void HeaderCellStart_outputs_th()
        {
            IHtmlNode tag = renderer.HeadCellTag(grid.Columns[0]);
            Assert.Equal("col", tag.Attribute("scope"));
            Assert.Equal("th", tag.TagName);
        }

        [Fact]
        public void HeaderCellContent_should_output_header_text_from_column_value()
        {
            IHtmlNode tag = renderer.HeadCellTag(grid.Columns[0]);
            Assert.Equal(grid.Columns[0].Title, tag.Children[0].InnerHtml);
        }

        [Fact]
        public void Pager_should_output_paging_tags()
        {
            IHtmlNode tag = renderer.PagerTag();

            Assert.Equal("t-pager t-reset", tag.Attribute("class"));
            Assert.Equal("div", tag.TagName);
        }

        [Fact]
        public void FooterStart_outputs_tfoot()
        {
            IHtmlNode tag = renderer.FootTag(new HtmlTag("div"));

            Assert.Equal("td", tag.TagName);
        }

        [Fact]
        public void FooterCellSTart_renders_td()
        {
            IHtmlNode tag = renderer.FootCellTag();

            Assert.Equal("t-footer", tag.Attribute("class"));
            Assert.Equal("td", tag.TagName);
            Assert.Equal(grid.Columns.Count.ToString(), tag.Attribute("colspan"));
        }

        [Fact]
        public void BodyStart_renders_tbody()
        {
            IHtmlNode tag = renderer.BodyTag(new HtmlTag("div"));

            Assert.Equal("tbody", tag.TagName);
        }

        [Fact]
        public void RowStart_renders_tr()
        {
            IHtmlNode tag = renderer.RowTag();

            Assert.Equal("tr", tag.TagName);
        }

        [Fact]
        public void CellContent_outputs_the_supplied_text()
        {
            IHtmlNode tag = renderer.CellTag(new GridCell<Customer>(grid.Columns[1], new Customer { Name = "content" }));
            Assert.Equal("content", tag.InnerHtml);
            Assert.Equal("td", tag.TagName);
        }

        [Fact]
        public void CellContent_outputs_the_html_attributes()
        {
            grid.Columns[1].HtmlAttributes["class"] = "test";
            IHtmlNode tag = renderer.CellTag(new GridCell<Customer>(grid.Columns[1], new Customer { Name = "content" }));
            Assert.Equal("t-last test", tag.Attribute("class"));
        }

        [Fact]
        public void EditActionCommand_column_and_data_key()
        {
#if MVC2            
            grid.DataKeys.Add(new GridDataKey<Customer, int>(c => c.Id));

            GridDeleteActionCommand<Customer> deleteCommand = new GridDeleteActionCommand<Customer>();
            deleteCommand.BoundModeHtml(new HtmlTag("td"), new GridCell<Customer>(grid.Columns[1], new Customer { Id = 1, Name = "content" }));
#endif
        }

    }
}