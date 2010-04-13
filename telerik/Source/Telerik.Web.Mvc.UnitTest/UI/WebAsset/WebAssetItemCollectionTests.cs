// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System;

    using Extensions;

    using Moq;
    using Xunit;

    public class WebAssetItemCollectionTests
    {
        private readonly WebAssetItemCollection _collection;

        public WebAssetItemCollectionTests()
        {
            _collection = new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath);
        }

        [Fact]
        public void DefaultPath_should_be_same_which_is_passed_in_constructor()
        {
            Assert.Equal(WebAssetDefaultSettings.ScriptFilesPath, _collection.DefaultPath);
        }

        [Fact]
        public void AssetGroups_should_be_empty_when_new_instance_is_created()
        {
            Assert.Empty(_collection.AssetGroups);
        }

        [Fact]
        public void AssetItems_should_be_empty_when_new_instance_is_created()
        {
            Assert.Empty(_collection.AssetItems);
        }

        [Fact]
        public void Should_be_able_to_add_item()
        {
            _collection.Add("~/scripts/bar.js");

            Assert.NotEmpty(_collection);
        }

        [Fact]
        public void Should_be_able_to_add_item_in_group()
        {
            _collection.Add("foo", "~/scripts/bar.js");

            Assert.NotEmpty(_collection);
            Assert.NotEmpty(((WebAssetItemGroup) _collection[0]).Items);
        }

        [Fact]
        public void Should_be_able_to_insert_item()
        {
            _collection.Insert(0, "~/scripts/script.js");

            Assert.NotEmpty(_collection);
        }

        [Fact]
        public void Should_be_able_to_insert_group()
        {
            _collection.Insert(0, "group", "~/scripts/script.js");

            Assert.NotEmpty(_collection);
        }

        [Fact]
        public void Should_be_to_find_group_by_name()
        {
            _collection.AddRange(new[] { new WebAssetItemGroup("group1", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath }, new WebAssetItemGroup("group2", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath }, new WebAssetItemGroup("group3", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath } });

            Assert.NotNull(_collection.FindGroupByName("group2"));
        }

        [Fact]
        public void Should_be_to_find_item_by_Source()
        {
            _collection.AddRange(new[] { new WebAssetItem("~/Scripts/1.js"), new WebAssetItem("~/Scripts/2.js"), new WebAssetItem("~/Scripts/3.js") });

            Assert.NotNull(_collection.FindItemBySource("~/Scripts/2.js"));
        }

        [Fact]
        public void Should_be_able_to_insert()
        {
            _collection.Insert(0, new WebAssetItem("~/scripts/script.js"));
            _collection.Insert(1, new WebAssetItemGroup("group", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath });

            Assert.NotEmpty(_collection);
        }

        [Fact]
        public void Should_be_able_to_set_item()
        {
            _collection.Add(new WebAssetItem("~/scripts/script.js"));
            _collection[0] = new WebAssetItem("~/scripts/script1.js");

            Assert.Equal("~/scripts/script1.js", ((WebAssetItem) _collection[0]).Source);
        }

        [Fact]
        public void Should_not_be_able_to_add_duplicate_item()
        {
            _collection.Add(new WebAssetItem("~/scripts/script.js"));
            _collection.Add(new WebAssetItem("~/scripts/script.js"));

            _collection.Add(new WebAssetItemGroup("group1", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath });
            _collection.Add(new WebAssetItemGroup("group1", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath });

            Assert.Equal(2, _collection.Count);
        }

        [Fact]
        public void Setting_duplicate_item_should_throw_exception()
        {
            _collection.Add(new WebAssetItem("~/scripts/script.js"));
            _collection.Add(new WebAssetItem("~/scripts/script2.js"));

            Assert.Throws<ArgumentException>(() => _collection[1] = new WebAssetItem("~/scripts/script.js"));
        }

        [Fact]
        public void Setting_duplicate_group_should_throw_exception()
        {
            _collection.Add(new WebAssetItemGroup("group1", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath });
            _collection.Add(new WebAssetItemGroup("group2", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath });

            Assert.Throws<ArgumentException>(() => _collection[1] = new WebAssetItemGroup("group1", false) { DefaultPath = WebAssetDefaultSettings.ScriptFilesPath });
        }

        [Fact]
        public void Should_do_nothing_when_adding_unlnown_items()
        {
            _collection.Add(new Mock<IWebAssetItem>().Object);

            Assert.NotEmpty(_collection);
        }
    }
}