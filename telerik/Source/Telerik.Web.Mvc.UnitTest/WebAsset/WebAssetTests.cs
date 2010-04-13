// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UnitTest
{
    using Xunit;

    public class WebAssetTests
    {
        private readonly WebAsset _asset;

        public WebAssetTests()
        {
            _asset = new WebAsset("application/x-javascript", "1.0", true, 30, "function ready(){ test.init(); }");
        }

        [Fact]
        public void ContentType_should_be_same_which_is_passed_in_constructor()
        {
            Assert.Equal("application/x-javascript", _asset.ContentType);
        }

        [Fact]
        public void Version_should_be_same_which_is_passed_in_constructor()
        {
            Assert.Equal("1.0", _asset.Version);
        }

        [Fact]
        public void Compress_should_be_same_which_is_passed_in_constructor()
        {
            Assert.True(_asset.Compress);
        }

        [Fact]
        public void CacheDurationInDays_should_be_same_which_is_passed_in_constructor()
        {
            Assert.Equal(30, _asset.CacheDurationInDays);
        }

        [Fact]
        public void Content_should_be_same_which_is_passed_in_constructor()
        {
            Assert.Equal("function ready(){ test.init(); }", _asset.Content);
        }
    }
}