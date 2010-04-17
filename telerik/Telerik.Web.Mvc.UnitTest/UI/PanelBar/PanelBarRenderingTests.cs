namespace Telerik.Web.Mvc.UI.UnitTest
{

    using System.IO;
    using System.Web.UI;
    using Moq;
    using UI;
    using Xunit;
    using System;
    using System.Web.Routing;
    using Telerik.Web.Mvc.Infrastructure;

    public class PanelBarRenderingTest
    {
        private readonly PanelBar panelBar;
        private readonly Mock<IPanelBarHtmlBuilder> builder;

        public PanelBarRenderingTest()
        {
            Mock<TextWriter> textWriter = new Mock<TextWriter>();
            Mock<HtmlTextWriter> writer = new Mock<HtmlTextWriter>(textWriter.Object);

            builder = new Mock<IPanelBarHtmlBuilder>();
            builder.Setup(r => r.PanelBarTag()).Returns(() => new HtmlTag("ul"));
            builder.Setup(r => r.ItemTag(It.IsAny<PanelBarItem>())).Returns(() => new HtmlTag("li"));
            builder.Setup(r => r.ItemContentTag(It.IsAny<PanelBarItem>())).Returns(() => new HtmlTag("div"));
            builder.Setup(r => r.ItemInnerTag(It.IsAny<PanelBarItem>())).Returns(() => new HtmlTag("a"));
            builder.Setup(r => r.ChildrenTag(It.IsAny<PanelBarItem>())).Returns(() => new HtmlTag("ul"));

            panelBar = PanelBarTestHelper.CreatePanelbar(writer.Object, builder.Object);
            panelBar.Name = "PanelBar1";

            panelBar.Items.Add(new PanelBarItem { Text = "PanelBarItem1", RouteName = "ProductList" });
            panelBar.Items.Add(new PanelBarItem { Text = "PanelBarItem2", RouteName = "ProductList" });
            panelBar.Items.Add(new PanelBarItem { Text = "PanelBarItem3", RouteName = "ProductList" });
        }

        [Fact]
        public void Render_should_not_output_anything_in_case_of_empty_data_source()
        {
            panelBar.Items.Clear();

            builder.Setup(r => r.PanelBarTag());

            panelBar.Render();

            builder.Verify(r => r.PanelBarTag(), Times.Never());
        }

        [Fact]
        public void Render_should_output_once_PanelBar_begin_tag_if_items_are_not_zero()
        {
            builder.Setup(r => r.PanelBarTag()).Returns(() => new HtmlTag("div"));

            panelBar.Render();

            builder.Verify(r => r.PanelBarTag(), Times.Exactly(1));
        }

        [Fact]
        public void Should_output_as_many_li_tags_as_items()
        {
            builder.Setup(r => r.ItemTag(It.IsAny<PanelBarItem>())).Returns(() => new HtmlTag("li"));

            panelBar.Render();

			builder.Verify(r =>  r.ItemTag(It.IsAny<PanelBarItem>()), Times.Exactly(panelBar.Items.Count));
        }

        [Fact]
        public void Render_should_output_as_many_link_tags_as_items()
        {
            builder.Setup(r => r.ItemInnerTag(It.IsAny<PanelBarItem>())).Returns(() => new HtmlTag("a"));

            panelBar.Render();

            builder.Verify(r => r.ItemInnerTag(It.IsAny<PanelBarItem>()), Times.Exactly(panelBar.Items.Count));
        }

        [Fact]
        public void Render_should_output_ul_tag()
        {
            PanelBarItem item = new PanelBarItem { Text = "Item1", RouteName = "ProductList" };
            item.Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList" });

            panelBar.Items.Clear();
            panelBar.Items.Add(item);

            builder.Setup(b => b.ChildrenTag(It.IsAny<PanelBarItem>())).Returns(() => new HtmlTag("ul"));

            panelBar.Render();

            builder.Verify(b => b.ChildrenTag(It.IsAny<PanelBarItem>()));
        }

        [Fact]
        public void Render_should_output_child_items()
        {
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", RouteName = "ProductList" });

            builder.Setup(b => b.ItemTag(It.IsAny<PanelBarItem>())).Returns(() => new HtmlTag("li"));

            panelBar.Render();

			builder.Verify(b => b.ItemTag(It.IsAny<PanelBarItem>()), Times.Exactly(panelBar.Items.Count + panelBar.Items[0].Items.Count));
        }

        [Fact]
        public void Render_should_output_content_instead_of_items()
        {
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", RouteName = "ProductList" });

            panelBar.Items[0].Content = () => { };

            builder.Setup(r => r.ChildrenTag(It.IsAny<PanelBarItem>()));
            builder.Setup(r => r.ItemContentTag(It.IsAny<PanelBarItem>())).Returns(() => new HtmlTag("div"));

            panelBar.Render();

            builder.Verify(r => r.ChildrenTag(It.IsAny<PanelBarItem>()), Times.Never());
            builder.Verify(r => r.ItemContentTag(It.IsAny<PanelBarItem>()), Times.Exactly(1));
        }

        [Fact]
        public void Render_should_not_output_childrens_if_contentUrl_is_set()
        {
            const string contentUrl = "testUrl";

            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", RouteName = "ProductList" });

            panelBar.Items[0].ContentUrl = contentUrl;

            builder.Setup(r => r.ChildrenTag(It.IsAny<PanelBarItem>()));

            panelBar.Render();

            builder.Verify(r => r.ChildrenTag(It.IsAny<PanelBarItem>()), Times.Never());
        }

        [Fact]
        public void If_ContentURL_is_set_should_call_WriteContent()
        {
            const string contentUrl = "testUrl";

            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", RouteName = "ProductList" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", RouteName = "ProductList" });

            panelBar.Items[0].ContentUrl = contentUrl;

            builder.Setup(r => r.ItemContentTag(It.IsAny<PanelBarItem>())).Returns(() => new HtmlTag("div"));

            panelBar.Render();

            builder.Verify(r => r.ItemContentTag(It.IsAny<PanelBarItem>()), Times.Exactly(1));
        }

        [Fact]
        public void If_header_item_visible_property_is_false_should_not_render_this_item()
        {
            panelBar.Items.Clear();
            panelBar.Items.Add(new PanelBarItem { Text = "PanelBarItem1", RouteName = "ProductList", Visible = false });

            builder.Setup(r => r.ItemTag(It.IsAny<PanelBarItem>()));

            panelBar.Render();

            builder.Verify(r => r.ItemTag(It.IsAny<PanelBarItem>()), Times.Never());
        }

        [Fact]
        public void Render_should_call_objectWriter_start_method()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            PanelBarTestHelper.clientSideObjectWriter.Setup(ow => ow.Start()).Verifiable();

            panelBar.WriteInitializationScript(writer.Object);

            PanelBarTestHelper.clientSideObjectWriter.Verify(ow => ow.Start());
        }

        [Fact]
        public void ObjectWriter_should_call_objectWriter_complete_method()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            PanelBarTestHelper.clientSideObjectWriter.Setup(w => w.Complete());

            panelBar.WriteInitializationScript(writer.Object);

            PanelBarTestHelper.clientSideObjectWriter.Verify(w => w.Complete());
        }

        [Fact]
        public void ObjectWriter_should_append_Expand_property_of_clientEvents()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            panelBar.ClientEvents.OnExpand = () => { };

            PanelBarTestHelper.clientSideObjectWriter.Setup(w => w.Append("onExpand", panelBar.ClientEvents.OnExpand)).Verifiable();

            panelBar.WriteInitializationScript(writer.Object);

            PanelBarTestHelper.clientSideObjectWriter.Verify(w => w.Append("onExpand", panelBar.ClientEvents.OnExpand));
        }

        [Fact]
        public void ObjectWriter_should_append_Collapse_property_of_clientEvents()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            panelBar.ClientEvents.OnCollapse = () => { };

            PanelBarTestHelper.clientSideObjectWriter.Setup(w => w.Append("onCollapse", panelBar.ClientEvents.OnCollapse)).Verifiable();

            panelBar.WriteInitializationScript(writer.Object);

            PanelBarTestHelper.clientSideObjectWriter.Verify(w => w.Append("onCollapse", panelBar.ClientEvents.OnCollapse));
        }

        [Fact]
        public void ObjectWriter_should_append_SelectedItem_property_of_clientEvents()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            panelBar.ClientEvents.OnSelect = () => { };

            PanelBarTestHelper.clientSideObjectWriter.Setup(w => w.Append("onSelect", panelBar.ClientEvents.OnSelect)).Verifiable();

            panelBar.WriteInitializationScript(writer.Object);

            PanelBarTestHelper.clientSideObjectWriter.Verify(w => w.Append("onSelect", panelBar.ClientEvents.OnSelect));
        }

        [Fact]
        public void ObjectWriter_should_append_Loaded_property_of_clientEvents()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            panelBar.ClientEvents.OnLoad = () => { };

            PanelBarTestHelper.clientSideObjectWriter.Setup(w => w.Append("onLoad", panelBar.ClientEvents.OnLoad)).Verifiable();

            panelBar.WriteInitializationScript(writer.Object);

            PanelBarTestHelper.clientSideObjectWriter.Verify(w => w.Append("onLoad", panelBar.ClientEvents.OnLoad));
        }

        [Fact]
        public void ItemAction_should_set_items_Css_sprite_images() 
        {
            const string value = "test";
            panelBar.ItemAction = (item) =>
            {
                item.SpriteCssClasses = value;
            };

            panelBar.Render();

            Assert.Equal(value, panelBar.Items[0].SpriteCssClasses);
        }

        [Fact]
        public void Render_should_select_only_first_child_item_because_of_diff_route_values()
        {
            panelBar.HighlightPath = true;

            panelBar.ViewContext.RouteData.Values["controller"] = "Grid";
            panelBar.ViewContext.RouteData.Values["action"] = "Basic";
            panelBar.ViewContext.RouteData.Values["id"] = "10";
            panelBar.Items[0].Text = "Grid";
            panelBar.Items[0].Items.Add(new PanelBarItem
            {
                Text = "SubItem1",
                ControllerName = "Grid",
                ActionName = "Basic",
                RouteValues = new RouteValueDictionary(new { id = 5 })
            });
            panelBar.Items[0].Items.Add(new PanelBarItem
            {
                Text = "SubItem2",
                ControllerName = "Grid",
                ActionName = "Basic",
                RouteValues = new RouteValueDictionary(new { id = 10 })
            });

            panelBar.Render();

            Assert.True(panelBar.Items[0].Items[1].Selected);
            Assert.False(panelBar.Items[0].Items[0].Selected);
        }

        [Fact]
        public void Render_should_expand_first_item_and_select_first_child_item() 
        {
            panelBar.HighlightPath = true;

            panelBar.ViewContext.RouteData.Values["controller"] = "Grid";
            panelBar.ViewContext.RouteData.Values["action"] = "Basic";
            panelBar.Items[0].Text = "Grid";
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", ControllerName = "Grid", ActionName = "Basic" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory" });

            panelBar.Render();

            Assert.True(panelBar.Items[0].Expanded);
            Assert.True(panelBar.Items[0].Items[0].Selected);
        }

        [Fact]
        public void Render_should_not_expand_item_if_it_is_disabled_even_highlightPath()
        {
            panelBar.HighlightPath = true;

            panelBar.ViewContext.RouteData.Values["controller"] = "Grid";
            panelBar.ViewContext.RouteData.Values["action"] = "Basic";

            panelBar.Items[0].Text = "Grid";
            panelBar.Items[0].Enabled = false;

            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", ControllerName = "Grid", ActionName = "Basic" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory" });

            panelBar.Render();

            Assert.False(panelBar.Items[0].Expanded);
        }

        [Fact]
        public void Render_should_expand_first_nestedItem_and_select_first_item_in_the_third_level()
        {
            panelBar.HighlightPath = true;

            panelBar.ViewContext.RouteData.Values["controller"] = "Grid";
            panelBar.ViewContext.RouteData.Values["action"] = "FirstBasic";
            panelBar.Items[0].Text = "Grid";
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", ControllerName = "Grid", ActionName = "Basic", Enabled = true });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory", Enabled = true });

            panelBar.Items[0].Items[0].Items.Add(new PanelBarItem { Text = "SubSubItem1", ControllerName = "Grid", ActionName = "FirstBasic", });

            panelBar.Render();

            Assert.True(panelBar.Items[0].Items[0].Expanded);
            Assert.True(panelBar.Items[0].Items[0].Items[0].Selected);
        }

        [Fact]
        public void Render_should_not_expand_first_item_if_HighlightPath_is_false()
        {
            panelBar.ViewContext.RouteData.Values["controller"] = "Grid";
            panelBar.ViewContext.RouteData.Values["action"] = "Basic";

            panelBar.HighlightPath = false;

            panelBar.Items[0].Text = "Grid";
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", ControllerName = "Grid", ActionName = "Basic" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory" });

            panelBar.Render();

            Assert.False(panelBar.Items[0].Expanded);
            Assert.False(panelBar.Items[0].Items[0].Selected);
        }

        [Fact]
        public void Render_should_expand_all_items_ExpandAll_is_set_to_true_and_MultiExpandMode() 
        {
            panelBar.Items.Clear();

            panelBar.Items.Add(new PanelBarItem { Text = "Item1", Enabled = true });
            panelBar.Items.Add(new PanelBarItem { Text = "Item2", Enabled = true });
            panelBar.Items.Add(new PanelBarItem { Text = "Item3", Enabled = true });

            panelBar.ExpandMode = PanelBarExpandMode.Multiple;

            panelBar.ExpandAll = true;

            panelBar.Render();

            Assert.True(panelBar.Items[0].Expanded);
            Assert.True(panelBar.Items[1].Expanded);
            Assert.True(panelBar.Items[2].Expanded);
        }

        [Fact]
        public void SelectedItem_should_expand_item_with_children()
        {
            panelBar.SelectedIndex = 1;
            panelBar.Items[1].Items.Add(new PanelBarItem { Text = "subITem1" });
            panelBar.Items[1].Items.Add(new PanelBarItem { Text = "subITem2" });

            panelBar.Render();

            Assert.True(panelBar.Items[1].Expanded);
        }

        [Fact]
        public void SelectedItem_should_expand_items_with_content()
        {
            panelBar.SelectedIndex = 1;
            panelBar.Items[1].Content = () => { };

            panelBar.Render();

            Assert.True(panelBar.Items[1].Expanded);
        }

        [Fact]
        public void SelectedItem_should_expand_items_with_contentUrl()
        {
            panelBar.SelectedIndex = 1;
            panelBar.Items[1].ContentUrl = "fakeLink";

            panelBar.Render();

            Assert.True(panelBar.Items[1].Expanded);
        }

        [Fact]
        public void SelectedItem_should_be_expanded()
        {
            panelBar.SelectedIndex = 1;
            panelBar.Items[1].Items.Add(new PanelBarItem { Text = "subITem1" });
            panelBar.Items[1].Items.Add(new PanelBarItem { Text = "subITem2" });

            panelBar.Render();

            Assert.True(panelBar.Items[1].Expanded);
        }


        [Fact]
        public void Render_on_initial_load_should_expand_first_Item_if_HighlightPath_is_true_but_no_correct_item_is_found()
        {
            panelBar.HighlightPath = true;

            panelBar.ViewContext.RouteData.Values["controller"] = "NoSuchController";
            panelBar.ViewContext.RouteData.Values["action"] = "FirstBasic";
            panelBar.Items[0].Text = "Grid";
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1", ControllerName = "Grid", ActionName = "Basic", Enabled = true });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory", Enabled = true });

            panelBar.Items[0].Items[0].Items.Add(new PanelBarItem { Text = "SubSubItem1", ControllerName = "Grid", ActionName = "FirstBasic", });

            panelBar.SelectedIndex = 0;

            panelBar.Render();

            Assert.True(panelBar.Items[0].Expanded);
        }

        [Fact]
        public void Render_should_throw_exception_if_selectedIndex_is_out_of_range()
        {
            panelBar.SelectedIndex = 20; //out of range.

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => panelBar.Render());
        }

        [Fact]
        public void Render_should_output_only_one_expanded_header_item_if_single_expand_mode() 
        {
            panelBar.Items[0].Expanded = true;
            panelBar.Items[1].Expanded = true;

            panelBar.ExpandMode = PanelBarExpandMode.Single;

            panelBar.Render();

            Assert.False(panelBar.Items[1].Expanded);
        }

        [Fact]
        public void Render_should_expand_second_level_if_highlightpath_is_true_and_expand_mode_is_single() 
        {
            panelBar.HighlightPath = true;
            panelBar.ExpandMode = PanelBarExpandMode.Single;

            panelBar.ViewContext.RouteData.Values["controller"] = "Grid";
            panelBar.ViewContext.RouteData.Values["action"] = "FirstBasic";
            panelBar.Items[0].Text = "Grid";
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem1" });
            panelBar.Items[0].Items.Add(new PanelBarItem { Text = "SubItem2", ControllerName = "Grid", ActionName = "InMemory", Enabled = true });

            panelBar.Items[0].Items[0].Items.Add(new PanelBarItem { Text = "SubSubItem1", ControllerName = "Grid", ActionName = "FirstBasic", });

            panelBar.Render();

            Assert.True(panelBar.Items[0].Expanded);
            Assert.True(panelBar.Items[0].Items[0].Expanded);
        }
    }
}
