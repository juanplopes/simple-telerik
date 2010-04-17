// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System;

    using Xunit;
    using Moq;
    using Telerik.Web.Mvc.Infrastructure;

    public class WebAssetItemGroupTests
    {
        private readonly WebAssetItemGroup _assetItemGroup;

        public WebAssetItemGroupTests()
        {
            var servicLocator = new Mock
                <IServiceLocator>();
            servicLocator.Setup(l => l.Resolve<IConfigurationManager>()).Returns(new Mock<IConfigurationManager>().Object);

            ServiceLocator.SetCurrent(() => servicLocator.Object);
            _assetItemGroup = new WebAssetItemGroup("Dummy", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath };
        }

        [Fact]
        public void Name_should_be_same_which_is_passed_in_constructor()
        {
            Assert.Equal("Dummy", _assetItemGroup.Name);
        }

        [Fact]
        public void DefaultPath_should_be_same_which_is_passed_in_constructor()
        {
            Assert.Equal(WebAssetDefaultSettings.ScriptFilesPath, _assetItemGroup.DefaultPath);
        }

        [Fact]
        public void Version_should_be_same_as_default_asset_version_when_new_instance_is_created()
        {
            Assert.Equal(WebAssetDefaultSettings.Version, _assetItemGroup.Version);
        }

        [Fact]
        public void Compress_should_be_same_as_default_asset_compress_when_new_instance_is_created()
        {
            Assert.Equal(WebAssetDefaultSettings.Compress, _assetItemGroup.Compress);
        }

        [Fact]
        public void CacheDurationInDays_should_be_same_as_default_asset_cache_duration_in_days_when_new_instance_is_created()
        {
            Assert.Equal(WebAssetDefaultSettings.CacheDurationInDays, _assetItemGroup.CacheDurationInDays);
        }

        [Fact]
        public void Combine_should_be_same_as_default_asset_combine_when_new_instance_is_created()
        {
            Assert.Equal(WebAssetDefaultSettings.Combined, _assetItemGroup.Combined);
        }

        [Fact]
        public void Items_should_be_empty_when_new_instance_is_created()
        {
            Assert.Empty(_assetItemGroup.Items);
        }

        [Fact]
        public void Should_be_able_to_set_version()
        {
            _assetItemGroup.Version = "1.0";

            Assert.Equal("1.0", _assetItemGroup.Version);
        }

        [Fact]
        public void Should_be_able_to_set_cache_duation_in_days()
        {
            _assetItemGroup.CacheDurationInDays = 7;

            Assert.Equal(7, _assetItemGroup.CacheDurationInDays);
        }

        [Fact]
        public void Should_be_able_to_insert_item()
        {
            _assetItemGroup.Items.Insert(0, new WebAssetItem("~/scripts/script.js"));

            Assert.NotEmpty(_assetItemGroup.Items);
        }

        [Fact]
        public void Should_be_able_to_set_item()
        {
            _assetItemGroup.Items.Add(new WebAssetItem("~/scripts/script.js"));
            _assetItemGroup.Items[0] = new WebAssetItem("~/scripts/script1.js");

            Assert.Equal("~/scripts/script1.js", _assetItemGroup.Items[0].Source);
        }

        [Fact]
        public void Should_not_be_able_to_add_duplicate_item()
        {
            _assetItemGroup.Items.Add(new WebAssetItem("~/scripts/script.js"));
            _assetItemGroup.Items.Add(new WebAssetItem("~/scripts/script.js"));

            Assert.Equal(1, _assetItemGroup.Items.Count);
        }

        [Fact]
        public void Setting_duplicate_item_should_throw_exception()
        {
            _assetItemGroup.Items.Add(new WebAssetItem("~/scripts/script.js"));
            _assetItemGroup.Items.Add(new WebAssetItem("~/scripts/script2.js"));

            Assert.Throws<ArgumentException>(() => _assetItemGroup.Items[1] = new WebAssetItem("~/scripts/script.js"));
        }
    }
}