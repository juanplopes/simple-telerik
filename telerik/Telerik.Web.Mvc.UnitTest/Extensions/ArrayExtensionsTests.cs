// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions.UnitTest
{
    using Xunit;

    public class ArrayExtensionsTests
    {
        [Fact]
        public void IsNullOrEmpty_should_return_true_when_null_array_is_specified()
        {
            // ReSharper disable ConvertToConstant.Local
            int[] x = null;
            // ReSharper restore ConvertToConstant.Local

            Assert.True(x.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_should_return_true_when_empty_array_is_specified()
        {
            int[] x = new int[0];

            Assert.True(x.IsNullOrEmpty());
        }

        [Fact]
        public void IsEmpty_should_return_true_when_empty_array_is_specified()
        {
            int[] x = new int[0];

            Assert.True(x.IsEmpty());
        }
    }
}