// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.UnitTest
{
    using Xunit;

    public class CacheBaseTests
    {
        private readonly CacheBaseTestDouble _cache;

        public CacheBaseTests()
        {
            _cache = new CacheBaseTestDouble();
        }

        [Fact]
        public void GetOrCreate_should_return_new_when_does_not_exist()
        {
            Assert.NotNull(_cache.Get("foo"));
        }

        [Fact]
        public void GetOrCreate_should_return_existing_when_exist()
        {
            Dummy d1 = _cache.Get("foo");
            Dummy d2 = _cache.Get("foo");

            Assert.Same(d1, d2);
        }

        [Fact]
        public void Should_be_able_to_dispose()
        {
            _cache.Dispose();
        }

        private class Dummy { }

        private sealed class CacheBaseTestDouble : CacheBase<string, Dummy>
        {
            public Dummy Get(string key)
            {
                return GetOrCreate(key, () => new Dummy());
            }
        }
    }
}