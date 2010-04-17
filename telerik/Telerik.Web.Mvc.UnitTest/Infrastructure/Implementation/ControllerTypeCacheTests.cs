// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Routing;

    using Xunit;

    public class ControllerTypeCacheTests
    {
        private readonly ControllerTypeCache _controllerTypeCache;

        public ControllerTypeCacheTests()
        {
            _controllerTypeCache = new ControllerTypeCache
                                      {
                                          ReferencedAssemblies = (() => new List<Assembly>{ GetType().Assembly })
                                      };
        }

        [Fact]
        public void GetControllerType_should_return_correct_type()
        {
            Type type = _controllerTypeCache.GetControllerType(TestHelper.CreateRequestContext(), "Home");

            Assert.Same(typeof(HomeController), type);
        }

        [Fact]
        public void GetControllerType_should_throw_exception_when_same_controller_exists_in_another_namespace()
        {
            Assert.Throws<InvalidOperationException>(() => _controllerTypeCache.GetControllerType(TestHelper.CreateRequestContext(), "DuplicateName"));
        }

        [Fact]
        public void GetController_with_namespace_should_return_correct_type()
        {
            RequestContext requestContext = TestHelper.CreateRequestContext();

            requestContext.RouteData.DataTokens.Add("Namespaces", new[] { "Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest.DummyNamespace" });

            Type type = _controllerTypeCache.GetControllerType(requestContext, "DuplicateName");

            Assert.Same(typeof(DummyNamespace.DuplicateNameController), type);
        }
    }
}