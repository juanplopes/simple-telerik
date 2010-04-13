using Telerik.Web.Mvc.Infrastructure;
using Telerik.Web.Mvc.UI;
namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Xunit;
    using Moq;

    using System.IO;
    using System.Web.UI;
    using System.Web.Mvc;
    using System;
    using Telerik.Web.Mvc.Infrastructure;
   
    public class DatePickerHtmlBuilderTests
    {
        private IDatePickerHtmlBuilder renderer;
        private DatePicker datePicker;
        private DateTime date;

        public DatePickerHtmlBuilderTests()
        {
            date = new DateTime(2009, 12, 3);

            datePicker = DatePickerTestHelper.CreateDatePicker(null);
            datePicker.Name = "DatePicker";
            renderer = new DatePickerHtmlBuilder(datePicker);
        }

        [Fact]
        public void DatePickerStart_should_render_Div_tag() 
        {
            IHtmlNode tag = renderer.Build();

            Assert.Equal(tag.TagName, "div");
        }

        [Fact]
        public void DatePickerStart_should_render_classes()
        {
            IHtmlNode tag = renderer.Build();

            Assert.Equal(UIPrimitives.Widget.ToString() + " " + "t-datepicker", tag.Attribute("class"));
        }

        [Fact]
        public void DatePickerStart_should_render_id()
        {
            IHtmlNode tag = renderer.Build();

            Assert.Equal(datePicker.Id, tag.Attribute("id"));
        }

        [Fact]
        public void Input_should_render_input_control()
        {
            IHtmlNode tag = renderer.InputTag();

            Assert.Contains(UIPrimitives.Input, tag.Attribute("class"));
            Assert.Equal("input", tag.TagName);
        }

        [Fact]
        public void Input_should_render_id_and_name()
        {
            IHtmlNode tag = renderer.InputTag();

            Assert.Equal(datePicker.Id + "-input", tag.Attribute("id"));
            Assert.Equal(datePicker.Id, tag.Attribute("name"));
        }

        [Fact]
        public void Input_should_render_html_attributes()
        {
            datePicker.InputHtmlAttributes.Add("class", "t-test");

            IHtmlNode tag = renderer.InputTag();

            Assert.Equal("t-test", tag.Attribute("class"));
        }

        [Fact]
        public void Input_should_render_value_if_ViewData_value_exists() 
        {
            DateTime now = DateTime.Now;
            datePicker.ViewContext.ViewData["DatePicker"] = now.ToShortDateString();

            IHtmlNode tag = renderer.InputTag();

            Assert.Equal(Convert.ToDateTime(now.ToShortDateString(), Telerik.Web.Mvc.Infrastructure.Culture.Current).ToShortDateString(),
                tag.Attribute("value"));
        }

        [Fact]
        public void Input_should_render_selectedDate_if_set() 
        {
            DateTime now = DateTime.Now;
            datePicker.Value = now;

            IHtmlNode tag = renderer.InputTag();

            Assert.Equal(now.ToString(datePicker.Format), tag.Attribute("value"));
        }

        [Fact]
        public void Input_should_render_empty_string_if_no_viewdata_or_selectedDate()
        {
            IHtmlNode tag = renderer.InputTag();

            Assert.Equal("", tag.Attribute("value"));
        }

        [Fact]
        public void Input_should_render_viewdata_value_even_when_selectedDate_is_set()
        {
            DateTime now = DateTime.Now;
            datePicker.Value = now;
            datePicker.ViewContext.ViewData["DatePicker"] = now.ToShortDateString();

            IHtmlNode tag = renderer.InputTag();

            Assert.Equal(Convert.ToDateTime(now.ToShortDateString(), Telerik.Web.Mvc.Infrastructure.Culture.Current).ToShortDateString(),
                tag.Attribute("value"));
        }

        //should mock ModelState and check whether there is error.

        [Fact]
        public void Button_should_render_link_with_class_with_default_title()
        {
            datePicker.ButtonTitle = string.Empty;

            IHtmlNode tag = renderer.ButtonTag();

            Assert.Equal("#", tag.Attribute("href"));
            Assert.Equal("Open the calendar", tag.Attribute("title"));
            Assert.Equal(UIPrimitives.Link + " " + UIPrimitives.Icon + " t-icon-calendar", tag.Attribute("class"));
            Assert.Equal("a", tag.TagName);
       }

        [Fact]
        public void Button_should_render_link_with_class_with_set_title()
        {
            datePicker.ButtonTitle = "test";

            IHtmlNode tag = renderer.ButtonTag();
            
            Assert.Equal("test", tag.Attribute("title"));
        }
    }
}
