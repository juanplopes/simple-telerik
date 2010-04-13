namespace Telerik.Web.Mvc.UnitTest.Menu
{
    using System.IO;
    using System.Web.UI;
    using Moq;
    using Telerik.Web.Mvc.UI;
    using Xunit;
    using System;
    using System.Web.Routing;
    using Telerik.Web.Mvc.Infrastructure;

    public class MenuRenderingTests
    {
        private readonly Menu menu;
        private readonly Mock<IMenuHtmlBuilder> builder;

        public MenuRenderingTests()
        {
            Mock<HtmlTextWriter> writer = new Mock<HtmlTextWriter>(TextWriter.Null);

            builder = new Mock<IMenuHtmlBuilder>();
            builder.Setup(b => b.MenuTag()).Returns(() => new HtmlTag("ul"));
            builder.Setup(r => r.ItemTag(It.IsAny<MenuItem>())).Returns(() => new HtmlTag("li"));
            builder.Setup(r => r.ItemInnerContentTag(It.IsAny<MenuItem>())).Returns(() => new HtmlTag("a"));
            builder.Setup(r => r.ChildrenTag()).Returns(() => new HtmlTag("ul"));

            menu = MenuTestHelper.CreateMenu(writer.Object, builder.Object);
            menu.Name = "Menu";

            menu.Items.Add(new MenuItem { Text = "MenuItem1" });
            menu.Items.Add(new MenuItem { Text = "MenuItem2" });
            menu.Items.Add(new MenuItem { Text = "MenuItem3" });
        }

        [Fact]
        public void Render_should_not_output_anything_when_there_are_no_items()
        {
            menu.Items.Clear();
            builder.Setup(r => r.MenuTag());

            menu.Render();

            builder.Verify(r => r.MenuTag(), Times.Never());
        }

        [Fact]
        public void Render_should_output_once_Menu_begin_tag_if_items_are_not_zero()
        {
            menu.Render();

            builder.Verify(r => r.MenuTag(), Times.Exactly(1));
        }

        [Fact]
        public void Render_should_output_the_same_amount_of_items_as_there_are()
        {
            menu.Render();

            builder.Verify(r => r.ItemTag(It.IsAny<MenuItem>()), Times.Exactly(menu.Items.Count));
            builder.Verify(r => r.ItemInnerContentTag(It.IsAny<MenuItem>()), Times.Exactly(menu.Items.Count));
        }

        [Fact]
        public void Render_should_output_content_if_there_is_any() 
        {
            menu.Items[0].Content = () => { };

            builder.Setup(r => r.ItemContentTag(It.IsAny<MenuItem>())).Returns(new HtmlTag("div"));

            menu.Render();

            builder.Verify(r => r.ItemContentTag(It.IsAny<MenuItem>()), Times.Exactly(1));
        }

        [Fact]
        public void ItemAction_should_be_invoked()
        {
            menu.ItemAction = (item) =>
            {
                item.SpriteCssClasses = "test";
            };

            menu.Render();

            Assert.Equal("test", menu.Items[0].SpriteCssClasses);
        }

        //[Fact]
        //public void Render_should_output_selected_item_if_selectedIndex_is_in_range()
        //{
        //    builder.Setup(r => r.ItemTag(It.IsAny<MenuItem>())).Returns(new HtmlTag("li"));
        //    builder.Setup(r => r.ItemLinkTag(It.IsAny<MenuItem>())).Returns(new HtmlTag("a"));

        //    menu.SelectedIndex = 1;

        //    menu.Render();

        //    Assert.True(menu.Items[1].Selected);
        //}

        [Fact]
        public void Render_should_throw_exception_if_selectedIndex_is_out_of_range()
        {
            menu.SelectedIndex = 20; //out of range.

            Assert.Throws(typeof(ArgumentOutOfRangeException),() => menu.Render());
        }

        [Fact]
        public void Render_should_select_only_first_child_item_because_of_diff_route_values()
        {
            menu.HighlightPath = true;

            menu.ViewContext.RouteData.Values["controller"] = "Grid";
            menu.ViewContext.RouteData.Values["action"] = "Basic";
            menu.ViewContext.RouteData.Values["id"] = "10";

            menu.Items[0].Text = "Grid";
            menu.Items[0].Items.Add(new MenuItem
            {
                Text = "SubItem1",
                ControllerName = "Grid",
                ActionName = "Basic",
                RouteValues = new RouteValueDictionary(new { id = 5 })
            });
            menu.Items[0].Items.Add(new MenuItem
            {
                Text = "SubItem2",
                ControllerName = "Grid",
                ActionName = "Basic",
                RouteValues = new RouteValueDictionary(new { id = 10 })
            });

            menu.Render();

            Assert.True(menu.Items[0].Items[1].Selected);
            Assert.False(menu.Items[0].Items[0].Selected);
        }

        [Fact]
        public void Render_should_expand_second_level_if_highlightpath_is_true()
        {
            menu.HighlightPath = true;

            menu.ViewContext.RouteData.Values["controller"] = "Grid";
            menu.ViewContext.RouteData.Values["action"] = "FirstBasic";
            menu.Items[0].Text = "Grid";
            menu.Items[0].Items.Add(new MenuItem { Text = "SubItem1" });
            menu.Items[0].Items.Add(new MenuItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory", Enabled = true });

            menu.Items[0].Items[0].Items.Add(new MenuItem { Text = "SubSubItem1", ControllerName = "Grid", ActionName = "FirstBasic", });

            menu.Render();

            Assert.True(menu.Items[0].Items[0].Items[0].Selected);
            Assert.True(menu.Items[0].Items[0].HtmlAttributes["class"].ToString() == "t-highlighted");
        }
    }
}
