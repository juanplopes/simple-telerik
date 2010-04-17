// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions.UnitTest
{
    using Xunit;

    public class StringExtensionsTests
    {
        [Fact]
        public void FormatWith_should_replace_place_holder_tokens_with_provided_value()
        {
            Assert.Equal("A-B-C-D", "{0}-{1}-{2}-{3}".FormatWith("A", "B", "C", "D"));
        }

        [Fact]
        public void IsCaseSensitiveEqual_should_return_true_when_both_string_are_same()
        {
            Assert.True("foo".IsCaseSensitiveEqual("foo"));
        }

        [Fact]
        public void IsCaseSensitiveEqual_should_return_false_when_casing_differs()
        {
            Assert.False("foO".IsCaseSensitiveEqual("fOo"));
        }

        [Fact]
        public void IsCaseInsensitiveEqual_should_return_true_when_casing_differs()
        {
            Assert.True("foO".IsCaseInsensitiveEqual("fOo"));
        }

        [Fact]
        public void Should_be_able_to_compress_and_decompress()
        {
            string compressed = new string('x', 10 * 1024).Compress();
            string plain = compressed.Decompress();

            Assert.True(plain.Length > compressed.Length);
        }
    }
}