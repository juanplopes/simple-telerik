// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UnitTest
{
    using Infrastructure;
    using UI;

    using Moq;
    using Xunit;

    public class WebAssetRegistryTests
    {
        private readonly Mock<ICacheManager> _cacheManager;
        private readonly Mock<IWebAssetLocator> _assetLocator;
        private readonly Mock<IPathResolver> _pathResolver;
        private readonly Mock<IUrlResolver> _urlResolver;
        private readonly Mock<IVirtualPathProvider> _virtualPathProvider;

        private readonly WebAssetRegistry _registry;

        public WebAssetRegistryTests()
        {
            var servicLocator = new Mock<IServiceLocator>();
            servicLocator.Setup(l => l.Resolve<IConfigurationManager>()).Returns(new Mock<IConfigurationManager>().Object);

            ServiceLocator.SetCurrent(() => servicLocator.Object);

            _cacheManager = new Mock<ICacheManager>();
            _assetLocator = new Mock<IWebAssetLocator>();
            _urlResolver = new Mock<IUrlResolver>();
            _pathResolver = new Mock<IPathResolver>();
            _virtualPathProvider = new Mock<IVirtualPathProvider>();

            _registry = new WebAssetRegistry(false, _cacheManager.Object, _assetLocator.Object, _urlResolver.Object, _pathResolver.Object, _virtualPathProvider.Object);
        }

        [Fact]
        public void Should_be_able_to_store_and_retrieve()
        {
            _cacheManager.Setup(manager => manager.GetItem(It.IsAny<string>())).Returns(null);
            _assetLocator.Setup(locator => locator.Locate(It.IsAny<string>(), It.IsAny<string>())).Returns((string p, string v) => p);
            _pathResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Substring(1));
            _virtualPathProvider.Setup(provider => provider.ReadAllText(It.IsAny<string>())).Returns("function test{}");

            WebAssetItemGroup group = new WebAssetItemGroup("js", false)
                                          {
                                              CacheDurationInDays = 7,
                                              Version = "1.0",
                                          };

            group.Items.Add(new WebAssetItem("~/Scripts/file1.js"));
            group.Items.Add(new WebAssetItem("~/Scripts/file2.js"));

            string id = _registry.Store("application/x-javascript", group);

            WebAsset asset = _registry.Retrieve(id);

            Assert.Equal("application/x-javascript", asset.ContentType);
            Assert.Equal("1.0", asset.Version);
            Assert.True(asset.Compress);
            Assert.Equal(7, asset.CacheDurationInDays);
            Assert.Contains("function test{}", asset.Content);
        }

        [Fact]
        public void Should_be_able_to_locate()
        {
            _assetLocator.Setup(locator => locator.Locate(It.IsAny<string>(), It.IsAny<string>())).Returns((string p, string v) => p).Verifiable();

            _registry.Locate("~/Content/site.css", "2.0");

            _assetLocator.Verify();
        }

        [Fact]
        public void Should_be_able_to_store_and_retrieve_stylesheet_asset_with_no_prefix()
        {
            string id = SetupStylesheetAsset("");

            WebAsset asset = _registry.Retrieve(id);
            Assert.Contains(".ui-widget-content { border: 1px solid #a6c9e2; background: #fcfdfd url('/Content/images/ui-bg_inset-hard_100_fcfdfd_1x100.png') 50% bottom repeat-x; color: #222222; }", asset.Content);
        }

        [Fact]
        public void Should_be_able_to_store_and_retrieve_stylesheet_asset_with_no_prefix_and_version()
        {
            string id = SetupStylesheetAssetWithVersion("");

            WebAsset asset = _registry.Retrieve(id);
            Assert.Contains(".ui-widget-content { border: 1px solid #a6c9e2; background: #fcfdfd url('/Content/1.0/images/ui-bg_inset-hard_100_fcfdfd_1x100.png') 50% bottom repeat-x; color: #222222; }", asset.Content);
        }

        [Fact]
        public void Should_be_able_to_store_and_retrieve_stylesheet_asset_with_dot_dot_prefix()
        {
            string id = SetupStylesheetAsset("../");
            _urlResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Replace("../", "").Replace("~", ""));

            WebAsset asset = _registry.Retrieve(id);

            Assert.Contains(".ui-widget-content { border: 1px solid #a6c9e2; background: #fcfdfd url('/Content/images/ui-bg_inset-hard_100_fcfdfd_1x100.png') 50% bottom repeat-x; color: #222222; }", asset.Content);
        }

        [Fact]
        public void Should_be_able_to_store_and_retrieve_stylesheet_asset_with_dot_dot_prefix_and_version()
        {
            string id = SetupStylesheetAssetWithVersion("../");
            _urlResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Replace("../", "").Replace("~", ""));

            WebAsset asset = _registry.Retrieve(id);

            Assert.Contains(".ui-widget-content { border: 1px solid #a6c9e2; background: #fcfdfd url('/Content/1.0/images/ui-bg_inset-hard_100_fcfdfd_1x100.png') 50% bottom repeat-x; color: #222222; }", asset.Content);
        }

        [Fact]
        public void Should_be_able_to_store_and_retrieve_stylesheet_asset_with_http_prefix()
        {
            string id = SetupStylesheetAsset("http://www.telerik.mvc.com/");

            WebAsset asset = _registry.Retrieve(id);
            Assert.Contains(".ui-widget-content { border: 1px solid #a6c9e2; background: #fcfdfd url('http://www.telerik.mvc.com/images/ui-bg_inset-hard_100_fcfdfd_1x100.png') 50% bottom repeat-x; color: #222222; }", asset.Content);
        }


        private string SetupStylesheetAssetWithVersion(string urlPrefix)
        {
            _cacheManager.Setup(manager => manager.GetItem(It.IsAny<string>())).Returns(null);
            _assetLocator.Setup(locator => locator.Locate(It.IsAny<string>(), It.IsAny<string>())).Returns((string p, string v) => "~/Content/1.0/site.css");
            _urlResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Substring(1));
            _pathResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Substring(1));
            _virtualPathProvider.Setup(vpd => vpd.GetDirectory(It.IsAny<string>())).Returns((string p1) => p1.Substring(0, p1.LastIndexOf("/")));
            _virtualPathProvider.Setup(vpd => vpd.CombinePaths(It.IsAny<string>(), It.IsAny<string>())).Returns((string p1, string p2) => p1 + "/" + p2);
            _virtualPathProvider.Setup(vpd => vpd.ReadAllText(It.IsAny<string>())).Returns(string.Format(".ui-widget-content {{ border: 1px solid #a6c9e2; background: #fcfdfd url({0}images/ui-bg_inset-hard_100_fcfdfd_1x100.png) 50% bottom repeat-x; color: #222222; }}", urlPrefix));

            WebAssetItemGroup group = new WebAssetItemGroup("js", false)
            {
                CacheDurationInDays = 7,
                Combined = true,
                Version = "1.0"
            };

            group.Items.Add(new WebAssetItem("~/Content/Site.css"));

            return _registry.Store("text/css", group);
        }

        
        private string SetupStylesheetAsset(string urlPrefix)
        {
            _cacheManager.Setup(manager => manager.GetItem(It.IsAny<string>())).Returns(null);
            _assetLocator.Setup(locator => locator.Locate(It.IsAny<string>(), It.IsAny<string>())).Returns((string p, string v) => p);
            _urlResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Substring(1));
            _pathResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Substring(1));
            _virtualPathProvider.Setup(vpd => vpd.GetDirectory(It.IsAny<string>())).Returns((string p1) => p1.Substring(0, p1.LastIndexOf("/")));
            _virtualPathProvider.Setup(vpd => vpd.CombinePaths(It.IsAny<string>(), It.IsAny<string>())).Returns((string p1, string p2) => p1 + "/" + p2);
            _virtualPathProvider.Setup(vpd => vpd.ReadAllText(It.IsAny<string>())).Returns(string.Format(".ui-widget-content {{ border: 1px solid #a6c9e2; background: #fcfdfd url({0}images/ui-bg_inset-hard_100_fcfdfd_1x100.png) 50% bottom repeat-x; color: #222222; }}", urlPrefix));

            WebAssetItemGroup group = new WebAssetItemGroup("js", false)
                                          {
                                              CacheDurationInDays = 7,
                                              Combined = true
                                          };

            group.Items.Add(new WebAssetItem("~/Content/Site.css"));

            return _registry.Store("text/css", group);
        }
    }
}