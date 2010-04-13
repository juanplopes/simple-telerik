// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Moq;
    using Xunit;

    public class ControllerAuthorizationTests
    {
        private readonly Mock<IAuthorizeAttributeCache> _authorizeAttributeCache;
        private readonly Mock<IReflectedAuthorizeAttributeCache> _reflectedAuthorizeAttributeCache;
        private readonly Mock<IObjectCopier> _objectCopier;
        private readonly ControllerAuthorization _controllerAuthorization;

        public ControllerAuthorizationTests()
        {
            _authorizeAttributeCache = new Mock<IAuthorizeAttributeCache>();
            _reflectedAuthorizeAttributeCache = new Mock<IReflectedAuthorizeAttributeCache>();
            _objectCopier = new Mock<IObjectCopier>();

            RouteCollection routes = new RouteCollection();
            TestHelper.RegisterDummyRoutes(routes);

            _controllerAuthorization = new ControllerAuthorization(_authorizeAttributeCache.Object, _reflectedAuthorizeAttributeCache.Object, _objectCopier.Object, routes);
        }

        [Fact]
        public void IsAccessibleToUser_for_action_should_use_reflected_attribute_when_decorated_with_custom_attribute()
        {
            Mock<AuthorizeAttribute> decoratedActionAttribute = new Mock<AuthorizeAttribute>();
            Mock<AuthorizeAttribute> decoratedControllerAttribute = new Mock<AuthorizeAttribute>();

            IList<AuthorizeAttribute> attributes = new List<AuthorizeAttribute>
                                                       {
                                                           decoratedActionAttribute.Object,
                                                           decoratedControllerAttribute.Object
                                                       };

            _authorizeAttributeCache.Setup(c => c.GetAuthorizeAttributes(It.IsAny<RequestContext>(), It.IsAny<string>(), It.IsAny<string>())).Returns(attributes);

            Mock<IAuthorizeAttribute> reflectedAttribute = new Mock<IAuthorizeAttribute>();
            reflectedAttribute.Setup(a => a.IsAuthorized(It.IsAny<HttpContextBase>())).Returns(true);

            _reflectedAuthorizeAttributeCache.Setup(c => c.GetAttribute(It.IsAny<Type>())).Returns(reflectedAttribute.Object);

            Assert.True(_controllerAuthorization.IsAccessibleToUser(TestHelper.CreateRequestContext(), "Default"));
        }

        [Fact]
        public void IsAccessibleToUser_for_action_should_use_built_in_attribute_when_decorated_with_default_attribute()
        {
            IList<AuthorizeAttribute> attributes = new List<AuthorizeAttribute>
                                                       {
                                                           new AuthorizeAttribute { Users = "Einstein"} ,
                                                           new AuthorizeAttribute { Roles = "Admin"}
                                                       };

            _authorizeAttributeCache.Setup(c => c.GetAuthorizeAttributes(It.IsAny<RequestContext>(), It.IsAny<string>(), It.IsAny<string>())).Returns(attributes);

            Mock<IIdentity> identity = new Mock<IIdentity>();
            identity.SetupGet(i => i.IsAuthenticated).Returns(true);
            identity.SetupGet(i => i.Name).Returns("Einstein");

            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();
            httpContext.SetupGet(context => context.User.Identity).Returns(identity.Object);

            Assert.False(_controllerAuthorization.IsAccessibleToUser(new RequestContext(httpContext.Object, new RouteData()), "Product", "Detail"));
        }

        [Fact]
        public void IsAccessibleToUser_should_return_false_when_exception_occurs()
        {
            Mock<AuthorizeAttribute> decoratedAttribute = new Mock<AuthorizeAttribute>();

            _authorizeAttributeCache.Setup(c => c.GetAuthorizeAttributes(It.IsAny<RequestContext>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new[] {decoratedAttribute.Object});

            Mock<IAuthorizeAttribute> reflectedAttribute = new Mock<IAuthorizeAttribute>();
            reflectedAttribute.Setup(a => a.IsAuthorized(It.IsAny<HttpContextBase>())).Throws<Exception>();

            _reflectedAuthorizeAttributeCache.Setup(c => c.GetAttribute(It.IsAny<Type>())).Returns(reflectedAttribute.Object);

            Assert.False(_controllerAuthorization.IsAccessibleToUser(TestHelper.CreateRequestContext(), "Product", "Detail"));
        }
    }
}