// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.UnitTest
{
    using Xunit;

    public class CultureTests
    {
        [Fact]
        public void Current_should_not_be_null()
        {
            Assert.NotNull(Culture.Current);
        }

        [Fact]
        public void Invariant_should_not_be_null()
        {
            Assert.NotNull(Culture.Invariant);
        }
    }
}