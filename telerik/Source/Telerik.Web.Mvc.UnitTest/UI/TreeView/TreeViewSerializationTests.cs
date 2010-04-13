namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.IO;
    using System.Web.UI;
    using Moq;
    using Telerik.Web.Mvc.UI;
    using Xunit;

    public class TreeViewSerializationTests
    {
        private readonly TreeView treeview;
        private Mock<TextWriter> textWriter;
        private string output;

        public TreeViewSerializationTests()
        {
            Mock<HtmlTextWriter> htmlWriter = new Mock<HtmlTextWriter>(TextWriter.Null);

            textWriter = new Mock<TextWriter>();
            textWriter.Setup(tw => tw.Write(It.IsAny<string>())).Callback<string>(s => output += s);

            treeview = TreeViewTestHelper.CreateTreeView(htmlWriter.Object, null);
            treeview.Name = "myTreeView";
        }

        [Fact]
        public void Treeview_serializes_cleanly_when_used_with_default_settings()
        {
            treeview.WriteInitializationScript(textWriter.Object);

            Assert.Equal("jQuery('#myTreeView').tTreeView();", output);
        }
    }
}
