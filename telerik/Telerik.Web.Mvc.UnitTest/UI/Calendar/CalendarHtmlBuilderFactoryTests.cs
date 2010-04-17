namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Xunit;
    using Moq;

    using System.IO;
    using System.Web.UI;
    using System.Web.Mvc;
    using System;

    public class CalendarHtmlBuilderFactoryTests
    {
        [Fact]
        public void Should_be_able_to_create_renderer()
        {
            CalendarHtmlBuilderFactory factory = new CalendarHtmlBuilderFactory();

            ICalendarHtmlBuilder renderer = factory.Create(CalendarTestHelper.CreateCalendar(null));

            Assert.IsType<CalendarHtmlBuilder>(renderer);
        }
    }
}
