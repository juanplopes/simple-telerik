// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation.UnitTest
{
    using Xunit;

    public class FileSystemWrapperTests
    {
        private readonly FileSystemWrapper _fileSystemWrapper;

        public FileSystemWrapperTests()
        {
            _fileSystemWrapper = new FileSystemWrapper();
        }

        [Fact]
        public void DirectoryExists_should_return_false_when_directory_is_missing()
        {
            Assert.False(_fileSystemWrapper.DirectoryExists("foo"));
        }

        [Fact]
        public void FileExists_should_return_false_when_file_is_missing()
        {
            Assert.False(_fileSystemWrapper.FileExists("foo.bar"));
        }

        [Fact]
        public void GetFiles_should_return_empty_array_when_path_is_invalid()
        {
            Assert.Empty(_fileSystemWrapper.GetFiles("foo", "*.*", true));
        }

        [Fact]
        public void ReadAllText_should_return_null_when_file_is_missing()
        {
            Assert.Null(_fileSystemWrapper.ReadAllText("foo.bar"));
        }
    }
}