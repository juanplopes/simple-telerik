// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UnitTest
{
    using System.Web.Caching;

    using Infrastructure;

    using Moq;
    using Xunit;

    public class XmlSiteMapTests
    {
        private readonly Mock<IPathResolver> _pathResolver;
        private readonly Mock<IFileSystem> _fileSystem;
        private readonly Mock<ICacheManager> _cacheManager;

        private readonly XmlSiteMap _siteMap;

        public XmlSiteMapTests()
        {
            _pathResolver = new Mock<IPathResolver>();
            _fileSystem = new Mock<IFileSystem>();
            _cacheManager = new Mock<ICacheManager>();

            _siteMap = new XmlSiteMap(_pathResolver.Object, _fileSystem.Object, _cacheManager.Object);
        }

        [Fact]
        public void Default_constructor_should_not_throw_exception()
        {
            Mock<IServiceLocator> locator = new Mock<IServiceLocator>();

            locator.Setup(l => l.Resolve<IPathResolver>()).Returns(_pathResolver.Object);
            locator.Setup(l => l.Resolve<IFileSystem>()).Returns(_fileSystem.Object);
            locator.Setup(l => l.Resolve<ICacheManager>()).Returns(_cacheManager.Object);

            ServiceLocator.SetCurrent(() => locator.Object);

            Assert.DoesNotThrow(() => new XmlSiteMap());
        }

        [Fact]
        public void Should_be_able_to_set_default_path()
        {
            XmlSiteMap.DefaultPath = "~/App_Data/Web.sitemap";

            Assert.Equal("~/App_Data/Web.sitemap", XmlSiteMap.DefaultPath);

            XmlSiteMap.DefaultPath = "~/Web.sitemap";
        }

        [Fact]
        public void Load_should_populate_site_map_from_default_xml()
        {
            SetupLoad();

            _siteMap.Load();

            Assert.Equal("Home", _siteMap.RootNode.Title);
            Assert.Equal("Home", _siteMap.RootNode.RouteName);
            Assert.Equal("bar", _siteMap.RootNode.Attributes["foo"]);
            Assert.Equal("Products", _siteMap.RootNode.ChildNodes[0].Title);
            Assert.Equal("ProductList", _siteMap.RootNode.ChildNodes[0].RouteName);
            Assert.Equal("Product 1", _siteMap.RootNode.ChildNodes[0].ChildNodes[0].Title);
            Assert.Equal("Product", _siteMap.RootNode.ChildNodes[0].ChildNodes[0].ControllerName);
            Assert.Equal("Detail", _siteMap.RootNode.ChildNodes[0].ChildNodes[0].ActionName);
            Assert.Equal("1", _siteMap.RootNode.ChildNodes[0].ChildNodes[0].RouteValues["id"]);
            Assert.Equal("Product 2", _siteMap.RootNode.ChildNodes[0].ChildNodes[1].Title);
            Assert.Equal("Product", _siteMap.RootNode.ChildNodes[0].ChildNodes[1].ControllerName);
            Assert.Equal("Detail", _siteMap.RootNode.ChildNodes[0].ChildNodes[1].ActionName);
            Assert.Equal("2", _siteMap.RootNode.ChildNodes[0].ChildNodes[1].RouteValues["id"]);
            Assert.Equal("Faq", _siteMap.RootNode.ChildNodes[1].Title);
            Assert.Equal("~/faq", _siteMap.RootNode.ChildNodes[1].Url);
        }

        [Fact]
        public void Load_should_use_path_resolver()
        {
            SetupLoad();
            _siteMap.Load();

            _pathResolver.Verify();
        }

        [Fact]
        public void Load_should_use_file_System()
        {
            SetupLoad();
            _siteMap.Load();

            _fileSystem.Verify();
        }

        [Fact]
        public void Load_should_cache_file_path()
        {
            SetupLoad();
            _siteMap.Load();

            _cacheManager.Verify();
        }

        [Fact]
        public void Should_reload_when_xml_file_is_changed()
        {
            Mock<XmlSiteMap> siteMap = new Mock<XmlSiteMap>(_pathResolver.Object, _fileSystem.Object, _cacheManager.Object);

            siteMap.Setup(sm => sm.InternalLoad(It.IsAny<string>())).Verifiable();

            siteMap.Object.OnCacheItemRemoved("foo", @"C:\Temp\Web.sitemap", CacheItemRemovedReason.DependencyChanged);
        }

        private void SetupLoad()
        {
            const string Xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>" + "\r\n" +
                               @"<siteMap compress=""false"" cacheDurationInMinutes=""120"" generateSearchEngineMap=""true"">" +
                               "\r\n" +
                               @"    <siteMapNode title=""Home"" route=""Home"" foo=""bar"">" + "\r\n" +
                               @"        <siteMapNode title=""Products"" route=""ProductList"" visible=""true"" " +
                               @"lastModifiedAt=""2009/1/3"" changeFrequency=""hourly"" updatePriority=""high"" >" +
                               "\r\n" +
                               @"            <siteMapNode title=""Product 1"" controller=""Product"" action=""Detail"">" +
                               "\r\n" +
                               @"                <routeValues>" + "\r\n" +
                               @"                    <id>1</id>" + "\r\n" +
                               @"                </routeValues>" + "\r\n" +
                               @"            </siteMapNode>" + "\r\n" +
                               @"            <siteMapNode title=""Product 2"" controller=""Product"" action=""Detail"" " +
                               @"includeInSearchEngineIndex=""true"">" + "\r\n" +
                               @"                <routeValues>" + "\r\n" +
                               @"                    <id>2</id>" + "\r\n" +
                               @"                </routeValues>" + "\r\n" +
                               @"            </siteMapNode>" + "\r\n" +
                               @"        </siteMapNode>" + "\r\n" +
                               @"        <siteMapNode title=""Faq"" url=""~/faq"" />" + "\r\n" +
                               @"    </siteMapNode>" + "\r\n" +
                               @"</siteMap>";

            _pathResolver.Setup(pathResolver => pathResolver.Resolve(It.IsAny<string>())).Returns("C:\\Web.sitemap").Verifiable();
            _fileSystem.Setup(fileSystem => fileSystem.ReadAllText(It.IsAny<string>())).Returns(Xml).Verifiable();
            _cacheManager.Setup(cache => cache.Insert(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CacheItemRemovedCallback>(), It.IsAny<string>())).Verifiable();
        }
    }
}