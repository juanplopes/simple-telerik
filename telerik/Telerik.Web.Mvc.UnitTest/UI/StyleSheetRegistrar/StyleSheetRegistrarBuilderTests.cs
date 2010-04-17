// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;

    using Moq;
    using Xunit;

    public class StyleSheetRegistrarBuilderTests
    {
        private readonly StyleSheetRegistrar _styleSheetRegistrar;
        private readonly StyleSheetRegistrarBuilder _builder;

        public StyleSheetRegistrarBuilderTests()
        {
            Mock<IServiceLocator> locator = new Mock<IServiceLocator>();

            locator.Setup(l => l.Resolve<IUrlGenerator>()).Returns(new Mock<IUrlGenerator>().Object);
            locator.Setup(l => l.Resolve<IConfigurationManager>()).Returns(new Mock<IConfigurationManager>().Object);
            locator.Setup(l => l.Resolve<INavigationItemAuthorization>()).Returns(new Mock<INavigationItemAuthorization>().Object);
            locator.Setup(l => l.Resolve<IGridHtmlBuilderFactory>()).Returns(new Mock<IGridHtmlBuilderFactory>().Object);
            locator.Setup(l => l.Resolve<IMenuHtmlBuilderFactory>()).Returns(new Mock<IMenuHtmlBuilderFactory>().Object);
            locator.Setup(l => l.Resolve<ITabStripHtmlBuilderFactory>()).Returns(new Mock<ITabStripHtmlBuilderFactory>().Object);

            ServiceLocator.SetCurrent(() => locator.Object);

            ViewContext viewContext = new ViewContext
                                          {
                                              HttpContext = TestHelper.CreateMockedHttpContext().Object,
                                              ViewData = new ViewDataDictionary()
                                          };

            _styleSheetRegistrar = new StyleSheetRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.StyleSheetFilesPath), viewContext, new Mock<IWebAssetItemMerger>().Object);

            _builder = new StyleSheetRegistrarBuilder(_styleSheetRegistrar);
        }

        [Fact]
        public void ToRegistrar_should_return_internal_stylesheet_registrar()
        {
            Assert.Same(_styleSheetRegistrar, _builder.ToRegistrar());
        }

        [Fact]
        public void StyleSheetRegistrar_operator_should_return_internal_stylesheet_registrar()
        {
            StyleSheetRegistrar styleSheetRegistrar = _builder;

            Assert.Same(_styleSheetRegistrar, styleSheetRegistrar);
        }

        [Fact]
        public void AssetHandlerPath_should_set_stylesheet_manager_asset_handler_path()
        {
            const string HandlerPath = "~/assets/stylesheets/asset.axd";

            _builder.AssetHandlerPath(HandlerPath);

            Assert.Equal(HandlerPath, _styleSheetRegistrar.AssetHandlerPath);
        }

        [Fact]
        public void Should_be_able_to_configure_default_group()
        {
            int previousCount = _styleSheetRegistrar.DefaultGroup.Items.Count;

            _builder.DefaultGroup(group => group.Add("foo.css"));

            Assert.Equal((previousCount + 1), _styleSheetRegistrar.DefaultGroup.Items.Count);
        }

        [Fact]
        public void Should_be_able_to_configure_stylesheets()
        {
            _builder.StyleSheets(styleSheet => styleSheet.Add("~/Content/site.css"));

            Assert.Equal(1, _styleSheetRegistrar.StyleSheets.Count);
        }

        [Fact]
        public void Render_should_not_throw_exception()
        {
            Assert.DoesNotThrow(() => _builder.Render());
        }
    }
}