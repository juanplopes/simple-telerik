// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using System;
    using Xunit;
    

    public class UrlEncoderTests
    {
        private readonly UrlEncoder _encoder;

        public UrlEncoderTests()
        {
            _encoder = new UrlEncoder();
        }

        [Fact]
        public void Resolve_should_throw_excepton_when_not_running_in_web_server()
        {
            Assert.Throws<ArgumentNullException>(() => _encoder.Encode("<abc>"));
        }
    }
}