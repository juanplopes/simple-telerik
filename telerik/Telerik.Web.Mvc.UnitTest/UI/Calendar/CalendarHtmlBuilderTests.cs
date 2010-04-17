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
    using System.Collections.Generic;

    public class CalendarHtmlBuilderTests
    {
        private ICalendarHtmlBuilder renderer;
        private Calendar calendar;
        private DateTime date;

        public CalendarHtmlBuilderTests()
        {
            date = new DateTime(2009, 12, 3);

            calendar = CalendarTestHelper.CreateCalendar(null);
            calendar.Name = "Calendar";
            renderer = new CalendarHtmlBuilder(calendar);
        }

        [Fact]
        public void Build_should_render_Div_tag() 
        {
            IHtmlNode tag = renderer.Build();

            Assert.Equal(tag.TagName, "div");
        }

        [Fact]
        public void Build_should_render_classes()
        {
            IHtmlNode tag = renderer.Build();

            Assert.Equal(UIPrimitives.Widget.ToString() + " " + "t-calendar", tag.Attribute("class"));
        }

        [Fact]
        public void Build_should_render_id_attribute()
        {
            IHtmlNode tag = renderer.Build();

            Assert.Equal(calendar.Id, tag.Attribute("id"));
        }

        [Fact]
        public void Build_should_render_html_attribute()
        {
            calendar.HtmlAttributes.Add("title", "calendar");

            IHtmlNode tag = renderer.Build();

            Assert.Equal("calendar", tag.Attribute("title"));
        }

        [Fact]
        public void NavigationTag_should_render_Div_tag()
        {
            IHtmlNode tag = renderer.NavigationTag();

            Assert.Equal("div", tag.TagName);
        }

        [Fact]
        public void NavigationTag_should_render_class()
        {
            IHtmlNode tag = renderer.NavigationTag();

            Assert.Equal(UIPrimitives.Header, tag.Attribute("class"));
        }

        [Fact]
        public void NavigationTag_should_render_prev_link()
        {
            IHtmlNode tag = renderer.NavigationTag();

            Assert.Equal("a", tag.Children[0].TagName);
            Assert.Equal("#", tag.Children[0].Attribute("href"));
            Assert.Equal("t-link t-nav-prev", tag.Children[0].Attribute("class"));
        }

        [Fact]
        public void NavigationTag_should_render_disabled_prev_link_if_focused_date_is_not_in_range()
        {
            calendar.MinDate = DateTime.Today.AddMonths(1);

            IHtmlNode tag = renderer.NavigationTag();

            Assert.Equal("a", tag.Children[0].TagName);
            Assert.Equal("#", tag.Children[0].Attribute("href"));
            Assert.Equal("t-link t-nav-prev t-state-disabled", tag.Children[0].Attribute("class"));
        }

        [Fact]
        public void NavigationTag_should_render_prev_link_with_span()
        {
            IHtmlNode tag = renderer.NavigationTag();

            Assert.Equal("span", tag.Children[0].Children[0].TagName);
            Assert.Equal("t-icon t-arrow-prev", tag.Children[0].Children[0].Attribute("class"));
        }

        [Fact]
        public void NavigationTag_should_render_fast_link()
        {
            string focusedDate = calendar.DetermineFocusedDate().Value.ToString("MMMM yyyy");

            IHtmlNode tag = renderer.NavigationTag();

            Assert.Equal("a", tag.Children[1].TagName);
            Assert.Equal("#", tag.Children[1].Attribute("href"));
            Assert.Equal("t-link t-nav-fast", tag.Children[1].Attribute("class"));
            Assert.Equal(focusedDate, tag.Children[1].InnerHtml);
        }

        [Fact]
        public void NavigationTag_should_render_next_link()
        {
            IHtmlNode tag = renderer.NavigationTag();

            Assert.Equal("a", tag.Children[2].TagName);
            Assert.Equal("#", tag.Children[2].Attribute("href"));
            Assert.Equal("t-link t-nav-next", tag.Children[2].Attribute("class"));
        }

        [Fact]
        public void NavigationTag_should_render_disabled_next_link_if_focused_date_is_not_in_range()
        {
            calendar.MaxDate = calendar.DetermineFocusedDate().Value;

            IHtmlNode tag = renderer.NavigationTag();

            Assert.Equal("t-link t-nav-next t-state-disabled", tag.Children[2].Attribute("class"));
        }

        [Fact]
        public void NavigationTag_should_render_next_link_with_span()
        {
            IHtmlNode tag = renderer.NavigationTag();

            Assert.Equal("span", tag.Children[2].Children[0].TagName);
            Assert.Equal("t-icon t-arrow-next", tag.Children[2].Children[0].Attribute("class"));
        }

        [Fact]
        public void ContentTag_should_render_table_with_html_attributes()
        {
            IHtmlNode tag = renderer.ContentTag();

            Assert.Equal("table", tag.TagName);
            Assert.Equal(UIPrimitives.Content, tag.Attribute("class"));
            Assert.Equal("calendar widget", tag.Attribute("summary"));
            Assert.Equal("0", tag.Attribute("cellspacing"));
        }

        [Fact]
        public void HeaderTag_should_render_thead()
        {
            IHtmlNode tag = renderer.HeaderTag();

            Assert.Equal("thead", tag.TagName);
            Assert.Equal("t-week-header", tag.Attribute("class"));
        }

        [Fact]
        public void HeaderCellTag_should_render_th_with_htmlAttributes_and_day_name()
        {
            const string dayOfWeek = "Wednesday";

            IHtmlNode tag = renderer.HeaderCellTag(dayOfWeek);

            Assert.Equal("th", tag.TagName);
            Assert.Equal("col", tag.Attribute("scope"));
            Assert.Equal(dayOfWeek, tag.Attribute("title"));
            Assert.Equal(dayOfWeek.Substring(0, 3), tag.Attribute("abbr"));
            Assert.Equal(dayOfWeek.Substring(0, 1), tag.InnerHtml);
        }

        [Fact]
        public void MonthTag_should_render_tbody_tag()
        {
            IHtmlNode tag = renderer.MonthTag();

            Assert.Equal("tbody", tag.TagName);
        }

        [Fact]
        public void RowTag_should_render_tr_tag()
        {
            IHtmlNode tag = renderer.RowTag();

            Assert.Equal("tr", tag.TagName);
        }

        [Fact]
        public void CellTag_should_render_td_tag()
        {
            IHtmlNode tag = renderer.CellTag(DateTime.Today, string.Empty, false);

            Assert.Equal("td", tag.TagName);
        }

        [Fact]
        public void CellTag_should_render_other_month_class_if_isOtherMonth_is_true()
        {
            IHtmlNode tag = renderer.CellTag(DateTime.Today, string.Empty, true);

            Assert.Equal("t-other-month", tag.Attribute("class"));
        }

        [Fact]
        public void CellTag_should_render_selected_state_class_current_date_is_selected_and_it_not_from_other_month()
        {
            calendar.Value = DateTime.Today;

            IHtmlNode tag = renderer.CellTag(DateTime.Today, string.Empty, false);

            Assert.Equal(UIPrimitives.SelectedState, tag.Attribute("class"));
        }

        [Fact]
        public void CellTag_should_render_space_if_it_is_out_of_range() 
        {
            calendar.MaxDate = DateTime.Today.AddMonths(-1);

            IHtmlNode tag = renderer.CellTag(DateTime.Today, string.Empty, false);

            Assert.Equal("&nbsp;", tag.InnerHtml);
        }

        [Fact]
        public void CellTag_should_render_day_if_it_is_in_range_and_no_URL_format()
        {
            IHtmlNode tag = renderer.CellTag(DateTime.Today, string.Empty, false);

            Assert.Equal(DateTime.Today.Day.ToString(), tag.Children[0].InnerHtml);
        }

        [Fact]
        public void CellTag_should_render_day_with_ds_href_if_no_urlFormat()
        {
            IHtmlNode tag = renderer.CellTag(DateTime.Today, string.Empty, false);

            Assert.Equal("#", tag.Children[0].Attribute("href"));
            Assert.Equal(UIPrimitives.Link, tag.Children[0].Attribute("class"));
        }

        [Fact]
        public void CellTag_should_render_link_with_selection_url_and_formated_date_if_urlFormat_is_not_null()
        {
            DateTime day = DateTime.Today;
            const string urlFormat = "app/controller/action/{0}";
            calendar.SelectionSettings.Dates = new List<DateTime>();

            IHtmlNode tag = renderer.CellTag(day, urlFormat, false);

            Assert.Equal(string.Format(urlFormat, day.ToShortDateString()), tag.Children[0].Attribute("href"));
        }

        [Fact]
        public void CellTag_should_render_navigate_link_if_date_is_in_dates_and_urlFormat_is_passed()
        {
            DateTime day = DateTime.Today;
            const string urlFormat = "app/controller/action/{0}";
            calendar.SelectionSettings.Dates = new List<DateTime> { new DateTime(2005, 5, 10), day, new DateTime(2000, 10, 10) };

            IHtmlNode tag = renderer.CellTag(day, urlFormat, false);

            Assert.Equal(string.Format(urlFormat, day.ToShortDateString()), tag.Children[0].Attribute("href"));
        }

        [Fact]
        public void CellTag_should_render_navigate_link_with_ds_if_date_is_not_in_dates_and_urlFormat_is_passed()
        {
            DateTime day = DateTime.Today;
            const string urlFormat = "app/controller/action/{0}";
            calendar.SelectionSettings.Dates = new List<DateTime> { new DateTime(2005, 5, 10), day, new DateTime(2000, 10, 10) };

            IHtmlNode tag = renderer.CellTag(new DateTime(2000, 5, 10), urlFormat, false);

            Assert.Equal("#", tag.Children[0].Attribute("href"));
        }
    }
}
