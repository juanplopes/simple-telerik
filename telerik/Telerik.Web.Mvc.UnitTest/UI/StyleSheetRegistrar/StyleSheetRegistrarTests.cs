// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System;
    using Telerik.Web.Mvc.Infrastructure;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    using Moq;
    using Xunit;

    public class StyleSheetRegistrarTests
    {
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly ViewContext _viewContext;
        private readonly WebAssetItemCollection _styleSheets;
        private readonly Mock<IWebAssetItemMerger> _assetMerger;

        private readonly StyleSheetRegistrar _styleSheetRegistrar;

        public StyleSheetRegistrarTests()
        {
            Mock<IServiceLocator> locator = new Mock<IServiceLocator>();

            locator.Setup(l => l.Resolve<IUrlGenerator>()).Returns(new Mock<IUrlGenerator>().Object);
            locator.Setup(l => l.Resolve<IConfigurationManager>()).Returns(new Mock<IConfigurationManager>().Object);
            locator.Setup(l => l.Resolve<INavigationItemAuthorization>()).Returns(new Mock<INavigationItemAuthorization>().Object);
            locator.Setup(l => l.Resolve<IGridHtmlBuilderFactory>()).Returns(new Mock<IGridHtmlBuilderFactory>().Object);
            locator.Setup(l => l.Resolve<IMenuHtmlBuilderFactory>()).Returns(new Mock<IMenuHtmlBuilderFactory>().Object);
            locator.Setup(l => l.Resolve<ITabStripHtmlBuilderFactory>()).Returns(new Mock<ITabStripHtmlBuilderFactory>().Object);

            ServiceLocator.SetCurrent(() => locator.Object);
            _httpContext = TestHelper.CreateMockedHttpContext();
            _styleSheets = new WebAssetItemCollection(WebAssetDefaultSettings.StyleSheetFilesPath);
            _assetMerger = new Mock<IWebAssetItemMerger>();

            _viewContext = new ViewContext
                               {
                                   HttpContext = _httpContext.Object,
                                   ViewData = new ViewDataDictionary()
                               };

            _styleSheetRegistrar = new StyleSheetRegistrar(_styleSheets, _viewContext, _assetMerger.Object);
        }

        [Fact]
        public void Should_throw_exception_when_new_instance_is_created_for_the_same_http_context()
        {
            Assert.Throws<InvalidOperationException>(() => new StyleSheetRegistrar(_styleSheets, _viewContext, _assetMerger.Object));
        }

        [Fact]
        public void AssetHandlerPath_should_be_set_to_default_asset_handler_path_when_new_instance_is_created()
        {
            Assert.Equal(WebAssetHttpHandler.DefaultPath, _styleSheetRegistrar.AssetHandlerPath);
        }


        [Fact]
        public void Render_should_write_response()
        {
            SetupForRender();

            _styleSheetRegistrar.Render();

            _httpContext.Verify();
        }

        [Fact]
        public void Render_should_throw_exception_when_called_more_than_once()
        {
            SetupForRender();
            _styleSheetRegistrar.Render(); // Call once

            Assert.Throws<InvalidOperationException>(() => _styleSheetRegistrar.Render()); // Call Twice
        }

        private void SetupForRender()
        {
            _styleSheetRegistrar.DefaultGroup.Items.Add(new WebAssetItem("~/Content/site.css"));

            _assetMerger.Setup(m => m.Merge(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<WebAssetItemCollection>())).Returns(new List<string> { "/Content/site.css", "/Content/component1.css" });
            _httpContext.Setup(context => context.Response.Output.WriteLine(It.IsAny<string>())).Verifiable();
        }
    }
}