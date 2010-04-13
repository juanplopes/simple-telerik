// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.UnitTest
{
    using System;

    using Xunit;

    public class DisposableBaseTests
    {
        private DisposableObjectBaseTestDouble _disposable;

        public DisposableBaseTests()
        {
            _disposable = new DisposableObjectBaseTestDouble();
        }

        [Fact]
        public void Should_be_able_to_dispose()
        {
            Assert.DoesNotThrow(() => _disposable.Dispose());

            _disposable = null;
            GC.Collect();
        }

        private sealed class DisposableObjectBaseTestDouble : DisposableBase
        {
        }
    }
}