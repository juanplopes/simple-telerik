namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Xunit;
    using Moq;

    using System.IO;
    using System.Web.UI;
    using System.Web.Mvc;
    using System;

    public class InputHtmlBuilderFactoryTests
    {
        [Fact]
        public void Should_be_able_to_create_renderer()
        {
            TextboxBaseHtmlBuilderFactory<int> factory = new TextboxBaseHtmlBuilderFactory<int>();

            ITextBoxBaseHtmlBuilder renderer = factory.Create(TextBoxBaseTestHelper.CreateInput<int>(null));

            Assert.IsType<TextboxBaseHtmlBuilder<int>>(renderer);
        }
    }
}
