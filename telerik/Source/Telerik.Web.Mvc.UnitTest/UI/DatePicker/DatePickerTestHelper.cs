namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.IO;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.UI;

    using Infrastructure;

    using Moq;
    using UI;

    public static class DatePickerTestHelper
    {
        public static Mock<IClientSideObjectWriter> clientSideObjectWriter;

        public static DatePicker CreateDatePicker(IDatePickerHtmlBuilder renderer)
        {
            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();

            httpContext.Setup(c => c.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns(new HtmlTextWriter(TextWriter.Null));

            Mock<IDatePickerHtmlBuilderFactory> datePickerRendererFactory = new Mock<IDatePickerHtmlBuilderFactory>();

            Mock<IClientSideObjectWriterFactory> clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();
            clientSideObjectWriter = new Mock<IClientSideObjectWriter>();

            ViewContext viewContext = TestHelper.CreateViewContext();

            clientSideObjectWriterFactory.Setup(c => c.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(clientSideObjectWriter.Object);

            DatePicker datepicker = new DatePicker(viewContext, clientSideObjectWriterFactory.Object, datePickerRendererFactory.Object);

            renderer = renderer ?? new DatePickerHtmlBuilder(datepicker);
            datePickerRendererFactory.Setup(f => f.Create(It.IsAny<DatePicker>())).Returns(renderer);

            return datepicker;
        }
    }
}