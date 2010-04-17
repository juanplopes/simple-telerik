// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Xunit;

    public class WebAssetItemTests
    {
        private readonly WebAssetItem _assetItem;

        public WebAssetItemTests()
        {
            _assetItem = new WebAssetItem("~/Script/content1.js");
        }

        [Fact]
        public void Source_should_be_same_which_is_passed_in_constructor()
        {
            Assert.Equal("~/Script/content1.js", _assetItem.Source);
        }
    }
}