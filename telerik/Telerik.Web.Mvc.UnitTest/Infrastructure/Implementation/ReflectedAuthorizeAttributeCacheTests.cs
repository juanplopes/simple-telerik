// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using Moq;
    using Xunit;

    public class ReflectedAuthorizeAttributeCacheTests
    {
        private readonly Mock<IAuthorizeAttributeBuilder> _builder;
        private readonly ReflectedAuthorizeAttributeCache _cache;

        public ReflectedAuthorizeAttributeCacheTests()
        {
            _builder = new Mock<IAuthorizeAttributeBuilder>();
            _cache = new ReflectedAuthorizeAttributeCache(_builder.Object);
        }

        [Fact]
        public void Should_be_able_to_get_attribute()
        {
            _builder.Setup(b => b.Build(It.IsAny<Type>())).Returns(typeof(DummyAuthorizeAttribute).GetConstructor(Type.EmptyTypes));

            IAuthorizeAttribute attribute = _cache.GetAttribute(typeof(AuthorizeAttribute));

            Assert.NotNull(attribute);
        }

        private sealed class DummyAuthorizeAttribute : IAuthorizeAttribute
        {
            public int Order
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string Roles
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string Users
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public bool IsAuthorized(HttpContextBase httpContext)
            {
                throw new NotImplementedException();
            }
        }
    }
}