using Telerik.Web.Mvc.Infrastructure;
using Telerik.Web.Mvc.UI;
namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Xunit;
    
    using Infrastructure;

    public class InputHtmlBuilderTests
    {
        private ITextBoxBaseHtmlBuilder renderer;
        private TextBoxBase<int> input;
        private string objectName;

        public InputHtmlBuilderTests()
        {
            input = TextBoxBaseTestHelper.CreateInput<int>(null);
            renderer = new TextboxBaseHtmlBuilder<int>(input);
            objectName = "t-integerinput";
            input.Name = "IntegerInput";
        }

        [Fact]
        public void Build_should_render_div_tag() 
        {
            IHtmlNode tag = renderer.Build(objectName);

            Assert.Equal(tag.TagName, "div");
        }

        [Fact]
        public void Build_should_render_classes()
        {
            IHtmlNode tag = renderer.Build(objectName);

            Assert.Equal(UIPrimitives.Widget.ToString() + " " + objectName, tag.Attribute("class"));
        }

        [Fact]
        public void Build_should_render_html_attributes()
        {
            input.HtmlAttributes.Add("title", "genericInput");

            IHtmlNode tag = renderer.Build(objectName);

            Assert.Equal("genericInput", tag.Attribute("title"));
        }

        [Fact]
        public void Build_should_render_id()
        {
            input.Name = "TestName";

            IHtmlNode tag = renderer.Build(objectName);

            Assert.Equal("TestName", tag.Attribute("id"));
        }

        [Fact]
        public void InputTag_should_render_input_control()
        {
            IHtmlNode tag = renderer.InputTag();

            Assert.Contains(UIPrimitives.Input, tag.Attribute("class"));
            Assert.Equal("input", tag.TagName);
        }

        [Fact]
        public void InputTag_should_render_id_and_name()
        {
            input.Name = "IntegerInput";

            IHtmlNode tag = renderer.InputTag();

            Assert.Equal(input.Id + "-input", tag.Attribute("id"));
            Assert.Equal(input.Id, tag.Attribute("name"));
        }

        [Fact]
        public void Input_should_render_html_attributes()
        {
            input.InputHtmlAttributes.Add("class", "t-test");

            IHtmlNode tag = renderer.InputTag();

            Assert.Equal("t-test", tag.Attribute("class"));
        }

        [Fact]
        public void InputTag_should_render_value_if_ViewData_value_exists()
        {
            input.Name = "IntegerInput";
            const int value = 10;
            input.ViewContext.ViewData["IntegerInput"] = value.ToString();

            IHtmlNode tag = renderer.InputTag();

            Assert.Equal(value.ToString(), tag.Attribute("value"));
        }

        [Fact]
        public void InputTag_should_render_selected_value_if_set()
        {
            const int value = 10;
            input.Value = value;

            IHtmlNode tag = renderer.InputTag();

            Assert.Equal(value.ToString(), tag.Attribute("value"));
        }

        [Fact]
        public void InputTag_should_render_viewdata_value_even_when_selected_value_is_set()
        {
            const string inputName = "IntegerInput";
            const int value = 19;

            input.Name = inputName;
            input.Value = 10;
            input.ViewContext.ViewData[inputName] = value;

            IHtmlNode tag = renderer.InputTag();

            Assert.Equal(value.ToString(), tag.Attribute("value"));
        }

        [Fact]
        public void Up_Button_should_render_link_with_classes()
        {
            IHtmlNode tag = renderer.UpButtonTag();

            Assert.Equal("#", tag.Attribute("href"));
            Assert.Equal(UIPrimitives.Link + " " + UIPrimitives.Icon + " t-arrow-up", tag.Attribute("class"));
            Assert.Equal("a", tag.TagName);
        }

        [Fact]
        public void Up_Button_should_render_link_with_class_with_set_title()
        {
            input.ButtonTitleUp = "test";

            IHtmlNode tag = renderer.UpButtonTag();

            Assert.Equal("test", tag.Attribute("title"));
        }

        [Fact]
        public void Down_Button_should_render_link_with_classes()
        {
            IHtmlNode tag = renderer.DownButtonTag();

            Assert.Equal("#", tag.Attribute("href"));
            Assert.Equal(UIPrimitives.Link + " " + UIPrimitives.Icon + " t-arrow-down", tag.Attribute("class"));
            Assert.Equal("a", tag.TagName);
        }

        [Fact]
        public void Down_Button_should_render_link_with_class_with_set_title()
        {
            input.ButtonTitleDown = "test";

            IHtmlNode tag = renderer.DownButtonTag();

            Assert.Equal("test", tag.Attribute("title"));
        }
    }
}
