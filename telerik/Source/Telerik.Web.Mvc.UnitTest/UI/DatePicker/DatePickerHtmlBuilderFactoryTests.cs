namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Xunit;
    using Moq;

    using System.IO;
    using System.Web.UI;
    using System.Web.Mvc;
    using System;

    public class DatePickerHtmlBuilderFactoryTests
    {
        [Fact]
        public void Should_be_able_to_create_renderer()
        {
            DatePickerHtmlBuilderFactory factory = new DatePickerHtmlBuilderFactory();

            IDatePickerHtmlBuilder renderer = factory.Create(DatePickerTestHelper.CreateDatePicker(null));

            Assert.IsType<DatePickerHtmlBuilder>(renderer);
        }
    }
}
