namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Moq;
    using System.Web.UI;
    using System.Web;
    using System.Web.Mvc;
    using Infrastructure;
    using System.Web.Routing;
    using System.IO;
    using System.Web.Caching;
    using Telerik.Web.Mvc.UI.Fluent;

    public static class TreeViewTestHelper
    {
        public static Mock<IClientSideObjectWriter> clientSideObjectWriter;
        public static Mock<INavigationItemAuthorization> authorization;
        public static UrlGenerator urlGenerator;

        public static ViewContext viewContext;

        public static TreeView CreateTreeView(HtmlTextWriter writer, ITreeViewHtmlBuilder renderer)
        {
            Mock<HttpContextBase> httpContext = TestHelper.CreateMockedHttpContext();

            if (writer != null)
            {
                httpContext.Setup(c => c.Request.Browser.CreateHtmlTextWriter(It.IsAny<TextWriter>())).Returns(writer);
            }

            urlGenerator = new UrlGenerator();
            authorization = new Mock<INavigationItemAuthorization>();

            Mock<ITreeViewHtmlBuilderFactory> TreeViewRendererFactory = new Mock<ITreeViewHtmlBuilderFactory>();

            Mock<IViewDataContainer> viewDataContainer = new Mock<IViewDataContainer>();

            var viewDataDinctionary = new ViewDataDictionary();
            viewDataDinctionary.Add("sample", TestHelper.CreateXmlSiteMap());

            viewDataContainer.SetupGet(container => container.ViewData).Returns(viewDataDinctionary);

            // needed for testing serialization
            Mock<ClientSideObjectWriterFactory> clientSideObjectWriterFactory = new Mock<ClientSideObjectWriterFactory>();

            viewContext = TestHelper.CreateViewContext();
            viewContext.ViewData = viewDataDinctionary;

            authorization.Setup(a => a.IsAccessibleToUser(viewContext.RequestContext, It.IsAny<INavigatable>())).Returns(true);

            TreeView TreeView = new TreeView(viewContext, clientSideObjectWriterFactory.Object, urlGenerator, authorization.Object, TreeViewRendererFactory.Object);

            renderer = renderer ?? new TreeViewHtmlBuilder(TreeView, new Mock<IActionMethodCache>().Object);
            TreeViewRendererFactory.Setup(f => f.Create(It.IsAny<TreeView>())).Returns(renderer);

            return TreeView;
        }
    }
}
