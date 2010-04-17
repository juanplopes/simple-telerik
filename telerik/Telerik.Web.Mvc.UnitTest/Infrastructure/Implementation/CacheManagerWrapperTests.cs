// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using Xunit;

    public class CacheManagerWrapperTests
    {
        private readonly CacheManagerWrapper _cacheManagerWrapper;

        public CacheManagerWrapperTests()
        {
            _cacheManagerWrapper = new CacheManagerWrapper();
        }

        [Fact]
        public void Should_be_able_to_cache()
        {
            _cacheManagerWrapper.Insert("foo", "bar", null, null);

            Assert.Equal("bar", _cacheManagerWrapper.GetItem("foo"));
        }
    }
}