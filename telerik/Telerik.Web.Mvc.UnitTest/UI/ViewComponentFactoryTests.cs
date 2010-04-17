// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Infrastructure;
    using Fluent;

    using Moq;
    using Xunit;

    public class ViewComponentFactoryTests
    {
        private readonly ViewComponentFactory _factory;
        private readonly HtmlHelper htmlHelper;

        public ViewComponentFactoryTests()
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

            StyleSheetRegistrar styleSheetRegistrar = new StyleSheetRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.StyleSheetFilesPath), viewContext, new Mock<IWebAssetItemMerger>().Object);
            StyleSheetRegistrarBuilder styleSheetRegistrarBuilder = new StyleSheetRegistrarBuilder(styleSheetRegistrar);

            ScriptRegistrar scriptRegistrar = new ScriptRegistrar(new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath), new List<IScriptableComponent>(), viewContext, new Mock<IWebAssetItemMerger>().Object, new Mock<ScriptWrapperBase>().Object);
            ScriptRegistrarBuilder scriptRegistrarBuilder = new ScriptRegistrarBuilder(scriptRegistrar);
            htmlHelper = TestHelper.CreateHtmlHelper();
            _factory = new ViewComponentFactory(htmlHelper, new Mock<IClientSideObjectWriterFactory>().Object, styleSheetRegistrarBuilder, scriptRegistrarBuilder);
        }

        [Fact]
        public void StyleSheetManager_should_return_the_same_instace()
        {
            StyleSheetRegistrarBuilder sm1 = _factory.StyleSheetRegistrar();
            StyleSheetRegistrarBuilder sm2 = _factory.StyleSheetRegistrar();

            Assert.Same(sm1, sm2);
        }

        [Fact]
        public void ScriptManager_should_return_the_same_instace()
        {
            ScriptRegistrarBuilder sm1 = _factory.ScriptRegistrar();
            ScriptRegistrarBuilder sm2 = _factory.ScriptRegistrar();

            Assert.Same(sm1, sm2);
        }

        [Fact]
        public void Menu_should_return_new_instance()
        {
            Menu m1 = _factory.Menu();
            Menu m2 = _factory.Menu();

            Assert.NotSame(m1, m2);
        }

        [Fact]
        public void TabStrip_should_return_new_instance()
        {
            Assert.NotNull(_factory.TabStrip());
        }

        [Fact]
        public void Grid_should_return_new_instance()
        {
            Assert.NotNull(_factory.Grid<Customer>());
        }

        [Fact]
        public void Grid_should_set_data_source()
        {
            IEnumerable<Customer> dataSource = new[] { new Customer() };
            GridBuilder<Customer> builder = _factory.Grid(dataSource);
            Assert.Same(dataSource, builder.Component.DataSource);
        }

        [Fact]
        public void Grid_should_set_data_source_from_view_data()
        {
            IEnumerable<Customer> dataSource = new[] { new Customer() };
            htmlHelper.ViewContext.ViewData["dataSource"] = dataSource;
            GridBuilder<Customer> builder = _factory.Grid<Customer>("dataSource");
            Assert.Same(dataSource, builder.Component.DataSource);
        }

        [Fact]
        public void PanelBar_should_return_new_instance()
        {
            Assert.NotNull(_factory.PanelBar());
        }

        [Fact]
        public void DatePicker_should_return_new_instance()
        {
            Assert.NotNull(_factory.DatePicker());
        }

        [Fact]
        public void Calendar_should_return_new_instance()
        {
            Assert.NotNull(_factory.Calendar());
        }

        [Fact]
        public void IntegerTextBox_should_return_new_instance()
        {
            Assert.NotNull(_factory.IntegerTextBox());
        }

        [Fact]
        public void NumericTextBox_should_return_new_instance()
        {
            Assert.NotNull(_factory.NumericTextBox<double>());
        }

        [Fact]
        public void NumericTextBox_should_return_new_instance_with_type_double()
        {
            var builder = _factory.NumericTextBox<double>();
            Assert.IsType<double>(builder.ToComponent().MinValue);
        }

        [Fact]
        public void NumericTextBox_should_return_new_instance_with_type_float()
        {
            var builder = _factory.NumericTextBox<float>();
            Assert.IsType<float>(builder.ToComponent().MinValue);
        }

        [Fact]
        public void CurrencyTextBox_should_return_new_instance()
        {
            Assert.NotNull(_factory.CurrencyTextBox());
        }

        [Fact]
        public void PercentTextBox_should_return_new_instance()
        {
            Assert.NotNull(_factory.PercentTextBox());
        }
    }
}