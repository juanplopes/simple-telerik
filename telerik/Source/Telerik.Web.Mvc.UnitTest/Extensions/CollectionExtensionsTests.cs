// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Extensions.UnitTest
{
    using System.Collections.Generic;

    using Xunit;

    public class CollectionExtensionsTests
    {
        [Fact]
        public void IsNullOrEmpty_should_return_true_when_null_collection_is_specified()
        {
            // ReSharper disable ConvertToConstant.Local
            List<int> x = null;
            // ReSharper restore ConvertToConstant.Local

            Assert.True(x.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_should_return_true_when_empty_collection_is_specified()
        {
            List<int> x = new List<int>();

            Assert.True(x.IsNullOrEmpty());
        }

        [Fact]
        public void IsEmpty_should_return_true_when_empty_collection_is_specified()
        {
            List<int> x = new List<int>();

            Assert.True(x.IsEmpty());
        }

        [Fact]
        public void AddRange_should_add_specified_items()
        {
            IList<int> collection = new List<int> { 1, 2, 3 };

            collection.AddRange(new[] { 4, 5 });

            Assert.Contains(4, collection);
            Assert.Contains(5, collection);
        }
    }
}