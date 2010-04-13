// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UnitTest
{
    using System.IO;

    using Infrastructure;

    using Moq;
    using Xunit;

    public class WebAssetLocatorTests
    {
        private readonly Mock<IVirtualPathProvider> _virtualPathProvider;

        private WebAssetLocator _locator;

        public WebAssetLocatorTests()
        {
            _virtualPathProvider = new Mock<IVirtualPathProvider>();
            _virtualPathProvider.Setup(vpp => vpp.GetFile(It.IsAny<string>())).Returns((string p) => Path.GetFileName(p));
            _virtualPathProvider.Setup(vpp => vpp.GetDirectory(It.IsAny<string>())).Returns((string p) => p.Replace("/" + Path.GetFileName(p), string.Empty));
            _virtualPathProvider.Setup(vpp => vpp.GetExtension(It.IsAny<string>())).Returns((string p) => Path.GetExtension(p));
            _virtualPathProvider.Setup(vpp => vpp.CombinePaths(It.IsAny<string>(), It.IsAny<string>())).Returns((string p1, string p2) => p1 + "/" + p2);
        }

        [Fact]
        public void Locate_should_return_correct_path_in_debug_mode()
        {
            _locator = new WebAssetLocator(true, _virtualPathProvider.Object);

            _virtualPathProvider.Setup(vpp => vpp.FileExists("~/scripts/1.0/jquery-1.3.2.debug.js")).Returns(true);

            string path = _locator.Locate("~/scripts/jquery-1.3.2.js", "1.0");

            Assert.Equal("~/scripts/1.0/jquery-1.3.2.debug.js", path);
        }

        [Fact]
        public void Locate_should_return_correct_path_in_release_mode()
        {
            _locator = new WebAssetLocator(false, _virtualPathProvider.Object);

            _virtualPathProvider.Setup(vpp => vpp.FileExists("~/content/1.0/site.min.css")).Returns(true);

            string path = _locator.Locate("~/content/site.css", "1.0");

            Assert.Equal("~/content/1.0/site.min.css", path);
        }

        [Fact]
        public void Locate_should_return_correct_path_when_version_folder_is_missing()
        {
            _locator = new WebAssetLocator(false, _virtualPathProvider.Object);

            _virtualPathProvider.Setup(vpp => vpp.FileExists("~/content/site.min.css")).Returns(true);

            string path = _locator.Locate("~/content/site.css", "1.0");

            Assert.Equal("~/content/site.min.css", path);
        }

        [Fact]
        public void Locate_should_return_same_path_when_file_exists()
        {
            _locator = new WebAssetLocator(false, _virtualPathProvider.Object);

            _virtualPathProvider.Setup(vpp => vpp.FileExists("~/content/site.css")).Returns(true);

            string path = _locator.Locate("~/content/site.css", null);

            Assert.Equal("~/content/site.css", path);
        }

        [Fact]
        public void Locate_should_throw_exception_when_file_does_not_exist()
        {
            _locator = new WebAssetLocator(false, _virtualPathProvider.Object);

            _virtualPathProvider.Setup(vpp => vpp.FileExists("~/content/site.css")).Returns(false);

            Assert.Throws<FileNotFoundException>(() => _locator.Locate("~/content/site.css", null));
        }
    }
}