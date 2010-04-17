// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

using System.Collections.Generic;

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Moq;
    using Xunit;

    public class AuthorizeAttributeCacheTests
    {
        private readonly Mock<IControllerTypeCache> _controllerTypeCache;
        private readonly Mock<IActionMethodCache> _actionMethodCache;

        private readonly AuthorizeAttributeCache _authorizeAttributeCache;

        public AuthorizeAttributeCacheTests()
        {
            Func<string, Type> getType = name =>
                                         {
                                             Type t = Type.GetType("Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest." + name + "Controller", false, true);

                                             return t;
                                         };

            _controllerTypeCache = new Mock<IControllerTypeCache>();
            _controllerTypeCache.Setup(c => c.GetControllerType(It.IsAny<RequestContext>(), It.IsAny<string>())).Returns((RequestContext r, string t) => getType(t));

            _actionMethodCache = new Mock<IActionMethodCache>();
            _actionMethodCache.Setup(c => c.GetAllActionMethods(It.IsAny<RequestContext>(), It.IsAny<string>())).Returns((RequestContext r, string c) => getType(c).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).ToDictionary(m => m.Name, m => new List<MethodInfo>{ m } as IList<MethodInfo>));

            _authorizeAttributeCache = new AuthorizeAttributeCache(_controllerTypeCache.Object, _actionMethodCache.Object);
        }

        [Fact]
        public void GetAuthorizeAttributes_should_return_correct_attribute_from_action_method()
        {
            AuthorizeAttribute attribute = _authorizeAttributeCache.GetAuthorizeAttributes(TestHelper.CreateRequestContext(), "Product", "Detail").ElementAt(0);

            Assert.Equal("Mort, Elvis, Einstein", attribute.Users);
        }

        [Fact]
        public void GetAuthorizeAttributes_should_return_correct_users_from_controller()
        {
            AuthorizeAttribute attribute = _authorizeAttributeCache.GetAuthorizeAttributes(TestHelper.CreateRequestContext(), "Home", "Index").ElementAt(0);

            Assert.Equal("Mort, Elvis, Einstein", attribute.Users);
        }

        [Fact]
        public void GetAuthorizeAttributes_should_return_correct_roles_from_namespaced_controller()
        {
            _controllerTypeCache.Setup(c => c.GetControllerType(It.IsAny<RequestContext>(), It.IsAny<string>())).Returns((RequestContext r, string t) => Type.GetType("Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest.DummyNamespace." + t + "Controller", false, true));

            RequestContext context = TestHelper.CreateRequestContext();
            context.RouteData.DataTokens.Add("Namespaces", new[] { "Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest.DummyNamespace" });

            AuthorizeAttribute attribute = _authorizeAttributeCache.GetAuthorizeAttributes(context, "DuplicateName", "AMethod").ElementAt(0);

            Assert.Equal("User, Power User, Admin", attribute.Roles);
        }
    }
}