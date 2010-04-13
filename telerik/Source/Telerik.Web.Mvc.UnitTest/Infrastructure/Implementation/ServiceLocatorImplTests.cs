// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using System;

    using Moq;
    using Xunit;

    public class ServiceLocatorImplTests
    {
        private readonly ServiceLocatorImpl locator;

        public ServiceLocatorImplTests()
        {
            locator = new ServiceLocatorImpl(true);
        }

        [Fact]
        public void Should_be_able_to_resolve_known_object()
        {
            Assert.NotNull(locator.Resolve<IVirtualPathProvider>());
        }

        [Fact]
        public void Resolve_should_return_null_for_unknown_object()
        {
            Assert.Null(locator.Resolve<IDisposable>());
        }

        [Fact]
        public void Should_be_able_to_register_factory()
        {
            Mock<IDisposable> service = new Mock<IDisposable>();

            locator.Register<IDisposable>(c => service.Object);

            Assert.Same(service.Object, locator.Resolve<IDisposable>());
        }

        [Fact]
        public void Should_be_able_to_register()
        {
            locator.Register(new Mock<IDisposable>().Object);

            Assert.NotNull(locator.Resolve<IDisposable>());
        }

        [Fact]
        public void Register_should_dispose_previous_instance_if_previous_instance_is_disposable()
        {
            Mock<IDisposable> old = new Mock<IDisposable>();
            old.Setup(o => o.Dispose()).Verifiable();

            locator.Register(old.Object);

            locator.Register(new Mock<IDisposable>().Object);

            old.Verify();
        }
    }

    public interface IDummyService
    {
    }
}