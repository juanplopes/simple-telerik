using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Telerik.Web.Mvc;
using System.ComponentModel;
using Simple.Web.Mvc.Telerik.Tests.Samples;
using System.Linq.Expressions;

namespace Simple.Web.Mvc.Telerik.Tests
{
    public class SortVisitorFixture
    {
        [Test]
        public void CanGenerateExpressionWithNoSortDescription()
        {
            var sort = Helper.Sorts();

            var param = Expression.Parameter(typeof(IQueryable<ClassA>), "q");
            var expr = new SortVisitor<ClassA>().Visit(param, sort);

            Assert.AreEqual("q", expr.ToString());
        }


        [Test]
        public void CanGenerateExpressionWithNullSortDescription()
        {
            var param = Expression.Parameter(typeof(IQueryable<ClassA>), "q");
            var expr = new SortVisitor<ClassA>().Visit(param, null);

            Assert.AreEqual("q", expr.ToString());
        }


        [Test]
        public void CanSortBySingleAttributeAscending()
        {
            var sort = Helper.Sort("PropInt", ListSortDirection.Ascending);
            
            var param = Expression.Parameter(typeof(IQueryable<ClassA>), "q");
            var expr = new SortVisitor<ClassA>().Visit(param, sort);

            Assert.AreEqual("q.OrderBy(x => x.PropInt)", expr.ToString());
        }

        [Test]
        public void CanSortByTwoAttributesAllAscending()
        {
            var sort = Helper.Sorts(
                Helper.Sort("PropInt", ListSortDirection.Ascending),
                Helper.Sort("PropString", ListSortDirection.Ascending));

            var param = Expression.Parameter(typeof(IQueryable<ClassA>), "q");
            var expr = new SortVisitor<ClassA>().Visit(param, sort);

            Assert.AreEqual("q.OrderBy(x => x.PropInt).ThenBy(x => x.PropString)", expr.ToString());
        }

        [Test]
        public void CanSortByTwoAttributesOneAscendingOtherDescending()
        {
            var sort = Helper.Sorts(
                Helper.Sort("PropInt", ListSortDirection.Ascending),
                Helper.Sort("PropString", ListSortDirection.Descending));

            var param = Expression.Parameter(typeof(IQueryable<ClassA>), "q");
            var expr = new SortVisitor<ClassA>().Visit(param, sort);

            Assert.AreEqual("q.OrderBy(x => x.PropInt).ThenByDescending(x => x.PropString)", expr.ToString());
        }

        [Test]
        public void CanSortByTwoAttributesOneDescendingOtherAscending()
        {
            var sort = Helper.Sorts(
                Helper.Sort("PropInt", ListSortDirection.Descending),
                Helper.Sort("PropString", ListSortDirection.Ascending));

            var param = Expression.Parameter(typeof(IQueryable<ClassA>), "q");
            var expr = new SortVisitor<ClassA>().Visit(param, sort);

            Assert.AreEqual("q.OrderByDescending(x => x.PropInt).ThenBy(x => x.PropString)", expr.ToString());
        }

        [Test]
        public void CanSortByThreeAttributesOneDescendingOtherAscendingLastDescending()
        {
            var sort = Helper.Sorts(
                Helper.Sort("PropInt", ListSortDirection.Descending),
                Helper.Sort("PropString", ListSortDirection.Ascending),
                Helper.Sort("PropB.PropDecimalNullable", ListSortDirection.Descending)
                );

            var param = Expression.Parameter(typeof(IQueryable<ClassA>), "q");
            var expr = new SortVisitor<ClassA>().Visit(param, sort);

            Assert.AreEqual("q.OrderByDescending(x => x.PropInt).ThenBy(x => x.PropString).ThenByDescending(x => x.PropB.PropDecimalNullable)", expr.ToString());
        }

        [Test]
        public void CanSortByTwoAttributesAllDescending()
        {
            var sort = Helper.Sorts(
                Helper.Sort("PropInt", ListSortDirection.Descending),
                Helper.Sort("PropString", ListSortDirection.Descending));

            var param = Expression.Parameter(typeof(IQueryable<ClassA>), "q");
            var expr = new SortVisitor<ClassA>().Visit(param, sort);

            Assert.AreEqual("q.OrderByDescending(x => x.PropInt).ThenByDescending(x => x.PropString)", expr.ToString());
        }


        [Test]
        public void CanSortBySingleAttributeDescending()
        {
            var sort = Helper.Sort("PropB.PropDateTimeNullable", ListSortDirection.Descending);

            var param = Expression.Parameter(typeof(IQueryable<ClassA>), "q");
            var expr = new SortVisitor<ClassA>().Visit(param, sort);

            Assert.AreEqual("q.OrderByDescending(x => x.PropB.PropDateTimeNullable)", expr.ToString());
        }
    }
}
