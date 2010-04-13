using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using NUnit.Framework;

namespace Simple.Web.Mvc.Telerik.Tests
{
    public class SkipAndTakeFixture
    {
        [Test]
        public void WhenSkipIsZeroDoesntCreateAMethodCall()
        {
            var param = Expression.Parameter(typeof(IQueryable<int>), "x");

            var expr = new SkipAndTakeVisitor<int>().Visit(param, 0, 30);

            Assert.AreEqual("x.Take(30)", expr.ToString());
        }

        [Test]
        public void WhenTakeIsZeroDoesntCreateAMethodCall()
        {
            var param = Expression.Parameter(typeof(IQueryable<int>), "x");

            var expr = new SkipAndTakeVisitor<int>().Visit(param, 30, 0);

            Assert.AreEqual("x", expr.ToString());
        }

        [Test]
        public void WhenBothArePositiveDoesntCare()
        {
            var param = Expression.Parameter(typeof(IQueryable<int>), "x");

            var expr = new SkipAndTakeVisitor<int>().Visit(param, 30, 50);

            Assert.AreEqual("x.Skip(1500).Take(50)", expr.ToString());
        }

        [Test]
        public void WhenBothAreNegativeDontCreateAnyMethodCall()
        {
            var param = Expression.Parameter(typeof(IQueryable<int>), "x");

            var expr = new SkipAndTakeVisitor<int>().Visit(param, -10, -10);

            Assert.AreEqual("x", expr.ToString());
        }
    }
}
