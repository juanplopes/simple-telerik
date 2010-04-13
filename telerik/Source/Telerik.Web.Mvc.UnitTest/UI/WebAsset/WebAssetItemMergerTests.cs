// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.Collections.Generic;

    using Extensions;
    using Infrastructure;

    using Moq;
    using Xunit;

    public class WebAssetItemMergerTests
    {
        private readonly Mock<IWebAssetRegistry> _assetRegistry;
        private readonly Mock<IUrlResolver> _urlResolver;
        private readonly Mock<IUrlEncoder> _urlEncoder;

        private readonly WebAssetItemMerger _assetItemMerger;

        public WebAssetItemMergerTests()
        {
            _assetRegistry = new Mock<IWebAssetRegistry>();
            _urlResolver = new Mock<IUrlResolver>();
            _urlEncoder = new Mock<IUrlEncoder>();

            _assetItemMerger = new WebAssetItemMerger(_assetRegistry.Object, _urlResolver.Object, _urlEncoder.Object);
        }

        [Fact]
        public void Should_be_able_to_merge()
        {
            WebAssetItemCollection assets = new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath)
                                             {
                                                 new WebAssetItem("~/Scripts/script1.js"),
                                                 new WebAssetItemGroup("group1", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath },
                                                 new WebAssetItemGroup("group2", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath },
                                                 new WebAssetItemGroup("group3", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath }
                                             };

            WebAssetItemGroup group1 = assets.FindGroupByName("group1");
            group1.ContentDeliveryNetworkUrl = "http://cdn.com";

            WebAssetItemGroup group2 = assets.FindGroupByName("group2");
            group2.Items.AddRange(new[] { new WebAssetItem("~/Scripts/script2.js"), new WebAssetItem("~/Scripts/script3.js") });

            WebAssetItemGroup group3 = assets.FindGroupByName("group3");
            group3.Items.AddRange(new[] { new WebAssetItem("~/Scripts/script4.js"), new WebAssetItem("~/Scripts/script5.js") });
            group3.Combined = true;

            _urlEncoder.Setup(s => s.Encode(It.IsAny<string>())).Returns((string u) => u);
            _assetRegistry.Setup(r => r.Locate(It.IsAny<string>(), It.IsAny<string>())).Returns((string p, string v) => p);
            _assetRegistry.Setup(r => r.Store(It.IsAny<string>(), It.IsAny<WebAssetItemGroup>())).Returns("123");

            _urlResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Substring(1));

            IList<string> items = _assetItemMerger.Merge("application/x-javascript", WebAssetHttpHandler.DefaultPath, false, true, assets);

            Assert.Equal("/Scripts/script1.js", items[0]);
            Assert.Equal("http://cdn.com", items[1]);
            Assert.Equal("/Scripts/script2.js", items[2]);
            Assert.Equal("/Scripts/script3.js", items[3]);
            Assert.Equal("/asset.axd?id=123", items[4]);
        }
    }
}