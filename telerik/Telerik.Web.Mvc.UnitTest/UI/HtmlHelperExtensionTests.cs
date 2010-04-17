// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.Web.Mvc;

    using Infrastructure;

    using Moq;
    using Xunit;

    public class HtmlHelperExtensionTests
    {
        [Fact]
        public void Telerik_should_always_return_the_same_instance()
        {
            Mock<IServiceLocator> locator = new Mock<IServiceLocator>();

            locator.Setup(l => l.Resolve<IWebAssetItemMerger>()).Returns(new Mock<IWebAssetItemMerger>().Object);
            locator.Setup(l => l.Resolve<ScriptWrapperBase>()).Returns(new Mock<ScriptWrapperBase>().Object);
            locator.Setup(l => l.Resolve<IUrlGenerator>()).Returns(new Mock<IUrlGenerator>().Object);
            locator.Setup(l => l.Resolve<INavigationItemAuthorization>()).Returns(new Mock<INavigationItemAuthorization>().Object);
            locator.Setup(l => l.Resolve<IClientSideObjectWriterFactory>()).Returns(new Mock<IClientSideObjectWriterFactory>().Object);
            locator.Setup(l => l.Resolve<IConfigurationManager>()).Returns(new Mock<IConfigurationManager>().Object);

            ServiceLocator.SetCurrent(() => locator.Object);

            HtmlHelper helper = TestHelper.CreateHtmlHelper();

            ViewComponentFactory factory1 = helper.Telerik();
            ViewComponentFactory factory2 = helper.Telerik();

            Assert.Same(factory1, factory2);
        }
    }
}