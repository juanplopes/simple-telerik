// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Xunit;

    public class WebAssetItemGroupBuilderTests
    {
        private readonly WebAssetItemGroup _itemGroup;
        private readonly WebAssetItemGroupBuilder _builder;

        public WebAssetItemGroupBuilderTests()
        {
            _itemGroup = new WebAssetItemGroup("foo", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath };
            _builder = new WebAssetItemGroupBuilder(_itemGroup);
        }

        [Fact]
        public void ToGroup_should_return_internal_asset_item_group()
        {
            Assert.Same(_itemGroup, _builder.ToGroup());
        }

        [Fact]
        public void WebAssetItemGroup_operator_should_return_internal_asset_item_group()
        {
            WebAssetItemGroup assetItem = _builder;

            Assert.Same(_itemGroup, assetItem);
        }

        [Fact]
        public void Should_be_able_to_set_content_delivery_network_url()
        {
            _builder.ContentDeliveryNetworkUrl("http://cdn.com");

            Assert.Equal("http://cdn.com", _itemGroup.ContentDeliveryNetworkUrl);
        }

        [Fact]
        public void Should_be_able_to_set_disabled()
        {
            _builder.Enabled(false);

            Assert.False(_itemGroup.Enabled);
        }

        [Fact]
        public void Should_be_able_to_set_version()
        {
            _builder.Version("2.0");

            Assert.Equal("2.0", _itemGroup.Version);
        }

        [Fact]
        public void Should_be_able_to_set_compress()
        {
            _builder.Compress(false);

            Assert.False(_itemGroup.Compress);
        }

        [Fact]
        public void should_be_able_to_set_cache_duration_in_days()
        {
            _builder.CacheDurationInDays(7);

            Assert.Equal(7, _itemGroup.CacheDurationInDays);
        }

        [Fact]
        public void Should_be_able_to_set_combined()
        {
            _builder.Combined(true);

            Assert.True(_itemGroup.Combined);
        }

        [Fact]
        public void Should_be_able_to_set_default_path()
        {
            _builder.DefaultPath("~/assets/scripts");

            Assert.Equal("~/assets/scripts", _itemGroup.DefaultPath);
        }

        [Fact]
        public void Should_be_able_to_add()
        {
            _builder.Add("~/scripts/script1.js");
            _builder.Add("script2.js");

            Assert.Equal("~/scripts/script1.js", _itemGroup.Items[0].Source);
            Assert.Equal("~/Scripts/script2.js", _itemGroup.Items[1].Source);
        }
    }
}