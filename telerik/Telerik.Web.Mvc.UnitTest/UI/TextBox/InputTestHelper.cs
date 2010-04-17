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

    public static class TextBoxBaseTestHelper
    {
        public static Mock<IClientSideObjectWriter> clientSideObjectWriter;

        public static TextBoxBase<T> CreateInput<T>(ITextBoxBaseHtmlBuilder renderer) where T : struct
        {
            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();

            httpContext.Setup(c => c.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns(new HtmlTextWriter(TextWriter.Null));

            Mock<ITextboxBaseHtmlBuilderFactory<T>> inputRendererFactory = new Mock<ITextboxBaseHtmlBuilderFactory<T>>();

            Mock<IClientSideObjectWriterFactory> clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();
            clientSideObjectWriter = new Mock<IClientSideObjectWriter>();

            ViewContext viewContext = TestHelper.CreateViewContext();

            clientSideObjectWriterFactory.Setup(c => c.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(clientSideObjectWriter.Object);

            TextBoxBase<T> input = new TextBoxBase<T>(viewContext, clientSideObjectWriterFactory.Object, inputRendererFactory.Object);

            renderer = renderer ?? new TextboxBaseHtmlBuilder<T>(input);
            inputRendererFactory.Setup(f => f.Create(It.IsAny<TextBoxBase<T>>())).Returns(renderer);

            return input;
        }

        public static IntegerTextBox CreateIntegerTextBox(ITextBoxBaseHtmlBuilder renderer)
        {
            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();

            httpContext.Setup(c => c.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns(new HtmlTextWriter(TextWriter.Null));

            Mock<ITextboxBaseHtmlBuilderFactory<int>> inputRendererFactory = new Mock<ITextboxBaseHtmlBuilderFactory<int>>();

            Mock<IClientSideObjectWriterFactory> clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();
            clientSideObjectWriter = new Mock<IClientSideObjectWriter>();

            ViewContext viewContext = TestHelper.CreateViewContext();

            clientSideObjectWriterFactory.Setup(c => c.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(clientSideObjectWriter.Object);

            IntegerTextBox input = new IntegerTextBox(viewContext, clientSideObjectWriterFactory.Object, inputRendererFactory.Object);

            renderer = renderer ?? new TextboxBaseHtmlBuilder<int>(input);
            inputRendererFactory.Setup(f => f.Create(It.IsAny<IntegerTextBox>())).Returns(renderer);

            return input;
        }

        public static NumericTextBox<T> CreateNumericTextBox<T>(ITextBoxBaseHtmlBuilder renderer) where T : struct
        {
            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();

            httpContext.Setup(c => c.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns(new HtmlTextWriter(TextWriter.Null));

            Mock<ITextboxBaseHtmlBuilderFactory<T>> inputRendererFactory = new Mock<ITextboxBaseHtmlBuilderFactory<T>>();

            Mock<IClientSideObjectWriterFactory> clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();
            clientSideObjectWriter = new Mock<IClientSideObjectWriter>();

            ViewContext viewContext = TestHelper.CreateViewContext();

            clientSideObjectWriterFactory.Setup(c => c.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(clientSideObjectWriter.Object);

            NumericTextBox<T> input = new NumericTextBox<T>(viewContext, clientSideObjectWriterFactory.Object, inputRendererFactory.Object);

            renderer = renderer ?? new TextboxBaseHtmlBuilder<T>(input);
            inputRendererFactory.Setup(f => f.Create(It.IsAny<NumericTextBox<T>>())).Returns(renderer);

            return input;
        }

        public static PercentTextBox CreatePercentTextBox(ITextBoxBaseHtmlBuilder renderer)
        {
            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();

            httpContext.Setup(c => c.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns(new HtmlTextWriter(TextWriter.Null));

            Mock<ITextboxBaseHtmlBuilderFactory<double>> inputRendererFactory = new Mock<ITextboxBaseHtmlBuilderFactory<double>>();

            Mock<IClientSideObjectWriterFactory> clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();
            clientSideObjectWriter = new Mock<IClientSideObjectWriter>();

            ViewContext viewContext = TestHelper.CreateViewContext();

            clientSideObjectWriterFactory.Setup(c => c.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(clientSideObjectWriter.Object);

            PercentTextBox input = new PercentTextBox(viewContext, clientSideObjectWriterFactory.Object, inputRendererFactory.Object);

            renderer = renderer ?? new TextboxBaseHtmlBuilder<double>(input);
            inputRendererFactory.Setup(f => f.Create(It.IsAny<PercentTextBox>())).Returns(renderer);

            return input;
        }

        public static CurrencyTextBox CreateCurrencyTextBox(ITextBoxBaseHtmlBuilder renderer)
        {
            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();

            httpContext.Setup(c => c.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns(new HtmlTextWriter(TextWriter.Null));

            Mock<ITextboxBaseHtmlBuilderFactory<decimal>> inputRendererFactory = new Mock<ITextboxBaseHtmlBuilderFactory<decimal>>();

            Mock<IClientSideObjectWriterFactory> clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();
            clientSideObjectWriter = new Mock<IClientSideObjectWriter>();

            ViewContext viewContext = TestHelper.CreateViewContext();

            clientSideObjectWriterFactory.Setup(c => c.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(clientSideObjectWriter.Object);

            CurrencyTextBox input = new CurrencyTextBox(viewContext, clientSideObjectWriterFactory.Object, inputRendererFactory.Object);

            renderer = renderer ?? new TextboxBaseHtmlBuilder<decimal>(input);
            inputRendererFactory.Setup(f => f.Create(It.IsAny<CurrencyTextBox>())).Returns(renderer);

            return input;
        }
    }
}