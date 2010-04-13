using Telerik.Web.Mvc.Infrastructure;
using Telerik.Web.Mvc.UI;
namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Moq;

    using System.IO;
    using System.Web.UI;
using Xunit;
    using System;
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    public class CalendarRenderingTests
    {
        private readonly Calendar calendar;
        private readonly Mock<ICalendarHtmlBuilder> tagBuilder;
        private readonly Mock<IHtmlNode> rootTag;
        Mock<TextWriter> textWriter;

        public CalendarRenderingTests()
        {
            textWriter = new Mock<TextWriter>();

            tagBuilder = new Mock<ICalendarHtmlBuilder>();
            rootTag = new Mock<IHtmlNode>();
            rootTag.SetupGet(t => t.Children).Returns(() => new List<IHtmlNode>());

            calendar = CalendarTestHelper.CreateCalendar(tagBuilder.Object);
            calendar.Name = "Calendar";
        }

        [Fact]
        public void Render_should_output_Calendar_start_only_once()
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);
            tagBuilder.Setup(t => t.ContentTag()).Returns(new HtmlTag("table"));
            tagBuilder.Setup(t => t.HeaderTag()).Returns(new HtmlTag("thead"));
            tagBuilder.Setup(t => t.MonthTag()).Returns(new HtmlTag("tbody"));
            tagBuilder.Setup(t => t.RowTag()).Returns(new HtmlTag("tr"));
            tagBuilder.Setup(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new HtmlTag("td"));

            calendar.Render();

            tagBuilder.Verify();
        }

        [Fact]
        public void Render_should_output_Navigation_header_if_date_is_range()
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);
            tagBuilder.Setup(t => t.ContentTag()).Returns(new HtmlTag("table"));
            tagBuilder.Setup(t => t.HeaderTag()).Returns(new HtmlTag("thead"));
            tagBuilder.Setup(t => t.MonthTag()).Returns(new HtmlTag("tbody"));
            tagBuilder.Setup(t => t.RowTag()).Returns(new HtmlTag("tr"));
            tagBuilder.Setup(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new HtmlTag("td"));

            tagBuilder.Setup(r => r.NavigationTag()).Verifiable();

            calendar.Render();

            tagBuilder.Verify();
        }

        [Fact]
        public void Render_should_output_Content_tag()
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);
            tagBuilder.Setup(t => t.HeaderTag()).Returns(new HtmlTag("thead"));
            tagBuilder.Setup(t => t.MonthTag()).Returns(new HtmlTag("tbody"));
            tagBuilder.Setup(t => t.RowTag()).Returns(new HtmlTag("tr"));
            tagBuilder.Setup(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new HtmlTag("td"));

            tagBuilder.Setup(r => r.ContentTag()).Returns(new HtmlTag("table")).Verifiable();

            calendar.Render();

            tagBuilder.Verify();
        }

        [Fact]
        public void Render_should_output_Header_Start_once()
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);
            tagBuilder.Setup(t => t.ContentTag()).Returns(new HtmlTag("table"));
            tagBuilder.Setup(t => t.HeaderTag()).Returns(new HtmlTag("thead"));
            tagBuilder.Setup(t => t.MonthTag()).Returns(new HtmlTag("tbody"));
            tagBuilder.Setup(t => t.RowTag()).Returns(new HtmlTag("tr"));
            tagBuilder.Setup(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new HtmlTag("td"));

            tagBuilder.Setup(r => r.HeaderTag()).Returns(new HtmlTag("thead")).Verifiable();

            calendar.Render();

            tagBuilder.Verify();
        }

        [Fact]
        public void Render_should_output_Header_day_seven_times()
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);
            tagBuilder.Setup(t => t.ContentTag()).Returns(new HtmlTag("table"));
            tagBuilder.Setup(t => t.HeaderTag()).Returns(new HtmlTag("thead"));
            tagBuilder.Setup(t => t.MonthTag()).Returns(new HtmlTag("tbody"));
            tagBuilder.Setup(t => t.RowTag()).Returns(new HtmlTag("tr"));
            tagBuilder.Setup(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new HtmlTag("td"));

            tagBuilder.Setup(r => r.HeaderCellTag(It.IsAny<string>())).Verifiable();

            calendar.Render();

            tagBuilder.Verify(r => r.HeaderCellTag(It.IsAny<string>()), Times.Exactly(7));
        }


        [Fact]
        public void Render_should_output_Month_Body_Start_once()
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);
            tagBuilder.Setup(t => t.ContentTag()).Returns(new HtmlTag("table"));
            tagBuilder.Setup(t => t.HeaderTag()).Returns(new HtmlTag("thead"));
            tagBuilder.Setup(t => t.RowTag()).Returns(new HtmlTag("tr"));
            tagBuilder.Setup(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new HtmlTag("td"));

            tagBuilder.Setup(t => t.MonthTag()).Returns(new HtmlTag("tbody")).Verifiable();

            calendar.Render();

            tagBuilder.Verify();
        }

        [Fact]
        public void Render_should_output_Month_Row_Start_once()
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);
            tagBuilder.Setup(t => t.ContentTag()).Returns(new HtmlTag("table"));
            tagBuilder.Setup(t => t.HeaderTag()).Returns(new HtmlTag("thead"));
            tagBuilder.Setup(t => t.MonthTag()).Returns(new HtmlTag("tbody"));
            tagBuilder.Setup(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new HtmlTag("td"));

            tagBuilder.Setup(t => t.RowTag()).Returns(new HtmlTag("tr")).Verifiable();

            calendar.Render();

            tagBuilder.Verify();
        }

        [Fact]
        public void Render_should_output_42_days()
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);
            tagBuilder.Setup(t => t.ContentTag()).Returns(new HtmlTag("table"));
            tagBuilder.Setup(t => t.HeaderTag()).Returns(new HtmlTag("thead"));
            tagBuilder.Setup(t => t.MonthTag()).Returns(new HtmlTag("tbody"));
            tagBuilder.Setup(t => t.RowTag()).Returns(new HtmlTag("tr"));

            tagBuilder.Setup(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new HtmlTag("td")).Verifiable();

            calendar.Render();

            tagBuilder.Verify(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Exactly(42));

        }

        [Fact]
        public void Render_should_call_objectWriter_start_method()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            CalendarTestHelper.clientSideObjectWriter.Setup(ow => ow.Start()).Verifiable();

            calendar.WriteInitializationScript(writer.Object);

            CalendarTestHelper.clientSideObjectWriter.Verify(ow => ow.Start());
        }

        [Fact]
        public void ObjectWriter_should_call_objectWriter_complete_method()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            CalendarTestHelper.clientSideObjectWriter.Setup(w => w.Complete());

            calendar.WriteInitializationScript(writer.Object);

            CalendarTestHelper.clientSideObjectWriter.Verify(w => w.Complete());
        }

        [Fact]
        public void ObjectWriter_should_append_Load_property_of_clientEvents()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            calendar.ClientEvents.OnLoad = () => { };

            CalendarTestHelper.clientSideObjectWriter.Setup(w => w.Append("onLoad", calendar.ClientEvents.OnLoad)).Verifiable();

            calendar.WriteInitializationScript(writer.Object);

            CalendarTestHelper.clientSideObjectWriter.Verify(w => w.Append("onLoad", calendar.ClientEvents.OnLoad));
        }

        [Fact]
        public void ObjectWriter_should_append_OnDateChanged_property_of_clientEvents()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            calendar.ClientEvents.OnChange = () => { };

            CalendarTestHelper.clientSideObjectWriter.Setup(w => w.Append("onChange", calendar.ClientEvents.OnChange)).Verifiable();

            calendar.WriteInitializationScript(writer.Object);

            CalendarTestHelper.clientSideObjectWriter.Verify(w => w.Append("onChange", calendar.ClientEvents.OnChange));
        }

        [Fact]
        public void ObjectWriter_should_append_SelectedDate_property()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            DateTime? date = new DateTime(2000, 12, 20);

            calendar.Value = date;

            CalendarTestHelper.clientSideObjectWriter.Setup(w => w.AppendDateOnly("selectedDate", date)).Verifiable();

            calendar.WriteInitializationScript(writer.Object);

            CalendarTestHelper.clientSideObjectWriter.Verify(w => w.AppendDateOnly("selectedDate", date));
        }

        [Fact]
        public void ObjectWriter_should_append_MinDate_property()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            DateTime date = new DateTime(1900, 12, 20);

            calendar.MinDate = date;

            CalendarTestHelper.clientSideObjectWriter.Setup(w => w.AppendDateOnly("minDate", date)).Verifiable();

            calendar.WriteInitializationScript(writer.Object);

            CalendarTestHelper.clientSideObjectWriter.Verify(w => w.AppendDateOnly("minDate", date));
        }

        [Fact]
        public void ObjectWriter_should_append_MaxDate_property()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            DateTime date = new DateTime(2100, 12, 20);

            calendar.MaxDate = date;

            CalendarTestHelper.clientSideObjectWriter.Setup(w => w.AppendDateOnly("maxDate", date)).Verifiable();

            calendar.WriteInitializationScript(writer.Object);

            CalendarTestHelper.clientSideObjectWriter.Verify(w => w.AppendDateOnly("maxDate", date));
        }

        [Fact]
        public void ObjectWriter_should_append_Selection_dates_property()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            IList<DateTime> dates = new List<DateTime> { new DateTime(2100, 12, 20) };

            calendar.SelectionSettings.Dates = dates;

            CalendarTestHelper.clientSideObjectWriter.Setup(w => w.AppendDatesOnly("dates", dates)).Verifiable();

            calendar.WriteInitializationScript(writer.Object);

            CalendarTestHelper.clientSideObjectWriter.Verify(w => w.AppendDatesOnly("dates", dates));
        }

        [Fact]
        public void ObjectWriter_should_append_Selection_URL_property()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            CalendarTestHelper.clientSideObjectWriter.Setup(w => w.Append("urlFormat", It.IsAny<string>())).Verifiable();

            calendar.WriteInitializationScript(writer.Object);

            CalendarTestHelper.clientSideObjectWriter.Verify(w => w.Append("urlFormat", It.IsAny<string>()));
        }

        [Fact]
        public void Render_should_throw_exception_if_selectedDate_is_out_of_limits()
        {
            calendar.MinDate = DateTime.Now.AddMonths(1);
            calendar.Value = DateTime.Now;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => calendar.Render());
        }

        [Fact]
        public void Render_should_not_throw_exception_if_selectedDate_is_null()
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);
            tagBuilder.Setup(t => t.ContentTag()).Returns(new HtmlTag("table"));
            tagBuilder.Setup(t => t.HeaderTag()).Returns(new HtmlTag("thead"));
            tagBuilder.Setup(t => t.MonthTag()).Returns(new HtmlTag("tbody"));
            tagBuilder.Setup(t => t.RowTag()).Returns(new HtmlTag("tr"));
            tagBuilder.Setup(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new HtmlTag("td"));

            calendar.MinDate = DateTime.Now.AddMonths(-1);
            calendar.Value = null;

            Assert.DoesNotThrow(() => calendar.Render());
        }

        [Fact]
        public void Render_should_throw_exception_if_minDate_is_bigger_than_maxDate()
        {
            DateTime date = DateTime.Now;
            calendar.MaxDate = date;
            calendar.MinDate = date.AddMonths(1);

            Assert.Throws(typeof(ArgumentException), () => calendar.Render());
        }

        [Fact]
        public void MaxDate_should_set_throw_exception_if_less_than_minDate()
        {
            DateTime date = DateTime.Now;
            calendar.MinDate = date;
            calendar.MaxDate = date.AddMonths(-1);

            Assert.Throws(typeof(ArgumentException), () => calendar.Render());
        }


        [Fact]
        public void Render_should_not_throw_exception_if_value_is_equal_to_maxDate()
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);
            tagBuilder.Setup(t => t.ContentTag()).Returns(new HtmlTag("table"));
            tagBuilder.Setup(t => t.HeaderTag()).Returns(new HtmlTag("thead"));
            tagBuilder.Setup(t => t.MonthTag()).Returns(new HtmlTag("tbody"));
            tagBuilder.Setup(t => t.RowTag()).Returns(new HtmlTag("tr"));
            tagBuilder.Setup(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new HtmlTag("td"));

            rootTag.Setup(tag => tag.WriteTo(It.IsAny<TextWriter>())).Verifiable();

            DateTime date = DateTime.Today;
            calendar.Value = date;
            calendar.MaxDate = date;

            Assert.DoesNotThrow(() => calendar.Render());
        }

        [Fact]
        public void Render_should_not_throw_exception_if_value_is_equal_to_minDate()
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);
            tagBuilder.Setup(t => t.ContentTag()).Returns(new HtmlTag("table"));
            tagBuilder.Setup(t => t.HeaderTag()).Returns(new HtmlTag("thead"));
            tagBuilder.Setup(t => t.MonthTag()).Returns(new HtmlTag("tbody"));
            tagBuilder.Setup(t => t.RowTag()).Returns(new HtmlTag("tr"));
            tagBuilder.Setup(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new HtmlTag("td"));

            rootTag.Setup(tag => tag.WriteTo(It.IsAny<TextWriter>())).Verifiable();

            DateTime date = DateTime.Today;
            calendar.Value = date;
            calendar.MinDate = date;

            Assert.DoesNotThrow(() => calendar.Render());
        }

        public void rootTag_should_call_writeTo_method() 
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);
            tagBuilder.Setup(t => t.ContentTag()).Returns(new HtmlTag("table"));
            tagBuilder.Setup(t => t.HeaderTag()).Returns(new HtmlTag("thead"));
            tagBuilder.Setup(t => t.MonthTag()).Returns(new HtmlTag("tbody"));
            tagBuilder.Setup(t => t.RowTag()).Returns(new HtmlTag("tr"));
            tagBuilder.Setup(t => t.CellTag(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(new HtmlTag("td"));

            rootTag.Setup(tag=>tag.WriteTo(It.IsAny<TextWriter>())).Verifiable();

            calendar.Render();

            rootTag.Verify();
        }
    }
}
