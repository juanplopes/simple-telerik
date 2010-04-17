// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using Xunit;

    public class FieldCacheTests
    {
        [Fact]
        public void Should_be_able_get_fields()
        {
            Assert.NotEmpty(new FieldCache().GetFields(typeof(DummyObjectWithPublicField)));
        }

        public class DummyObjectWithPublicField
        {
            public string Field1;
        }
    }
}