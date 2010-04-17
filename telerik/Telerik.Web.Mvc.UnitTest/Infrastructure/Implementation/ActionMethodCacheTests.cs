// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Routing;

    using Moq;
    using Xunit;

    public class ActionMethodCacheTests
    {
        private readonly Mock<IControllerTypeCache> controllerTypeCache;
        private readonly ActionMethodCache actionMethodCache;

        public ActionMethodCacheTests()
        {
            controllerTypeCache = new Mock<IControllerTypeCache>();
            actionMethodCache = new ActionMethodCache(controllerTypeCache.Object);
        }

        [Fact]
        public void GetActionMethods_should_return_correct_action_methods()
        {
            controllerTypeCache.Setup(c => c.GetControllerType(It.IsAny<RequestContext>(), It.IsAny<string>())).Returns(typeof (HomeController));

            IDictionary<string, IList<MethodInfo>> actionMethods = actionMethodCache.GetAllActionMethods(TestHelper.CreateRequestContext(), "Home");

            Assert.True(actionMethods.ContainsKey("Index"));
        }
    }
}