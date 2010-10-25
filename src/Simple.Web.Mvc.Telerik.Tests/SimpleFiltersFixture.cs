using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Telerik.Web.Mvc;
using Simple.Web.Mvc.Telerik.Tests.Samples;
using System.Linq.Expressions;

namespace Simple.Web.Mvc.Telerik.Tests
{
    public class SimpleFiltersFixture
    {
        [Test]
        public void CanCreateSampleEqualsFilterToInt()
        {
            var filter = new FilterDescriptor("PropInt", FilterOperator.IsEqualTo, 42);

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => (x.PropInt = 42)", expr.ToString());
        }

        [Test]
        public void CanCreateSampleIsContainedInFilterWithIntList()
        {
            var list = new[] { 1, 2, 3 };
            var filter = new FilterDescriptor("PropInt", FilterOperator.IsContainedIn, list);

            Expression<Func<int, bool>> ex = x => list.Contains(x);

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => value(System.Int32[]).Contains(x.PropInt)", expr.ToString());
        }

        [Test]
        public void CanCreateSampleEqualsFilterToIntWithQueryableVersion()
        {
            var filter = new FilterDescriptor("PropInt", FilterOperator.IsEqualTo, 42);
            var param = Expression.Parameter(typeof(IQueryable<ClassA>), "q");
            var expr = new FilterVisitor<ClassA>().Visit(param, filter);

            Assert.AreEqual("q.Where(x => (x.PropInt = 42))", expr.ToString());
        }


        [Test]
        public void CanCreateSampleEqualsFilterToIntWithDecimalValue()
        {
            var filter = new FilterDescriptor("PropInt", FilterOperator.IsEqualTo, 42m);

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => (x.PropInt = 42)", expr.ToString());
        }

        [Test]
        public void CanCreateSampleEqualsFilterToIntNullable()
        {
            var filter = new FilterDescriptor("PropIntNullable", FilterOperator.IsEqualTo, 42);

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => (x.PropIntNullable = 42)", expr.ToString());
        }

        [Test]
        public void CanCreateSampleEqualsFilterToIntNullableWithNullValue()
        {
            var filter = new FilterDescriptor("PropIntNullable", FilterOperator.IsEqualTo, null);

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => (x.PropIntNullable = null)", expr.ToString());
        }

        [Test]
        public void CanCreateSampleEqualsFilterToIntNullableWithDecimalValue()
        {
            var filter = new FilterDescriptor("PropIntNullable", FilterOperator.IsEqualTo, 42m);

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => (x.PropIntNullable = 42)", expr.ToString());
        }

        [Test]
        public void CanCreateSampleGreaterFilterToClassBWithDoubleValue()
        {
            var filter = new FilterDescriptor("PropB.PropDecimalNullable", FilterOperator.IsGreaterThan, 42.0);

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => (x.PropB.PropDecimalNullable > 42)", expr.ToString());
        }

        [Test]
        public void CanCreateSampleLessFilterToClassBDateTimeNullable()
        {
            var now = DateTime.Now;
            var filter = new FilterDescriptor("PropB.PropDateTimeNullable", FilterOperator.IsLessThan, now);

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual(string.Format("x => (x.PropB.PropDateTimeNullable < {0})", now.ToString()), expr.ToString());
        }


        [Test]
        public void CanCreateSampleContainsFilter()
        {
            var filter = new FilterDescriptor("PropString", FilterOperator.Contains, "asd");

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => x.PropString.ToUpper().Contains(\"ASD\")", expr.ToString());
        }

        [Test]
        public void CanCreateSampleEqualsToFilter()
        {
            var filter = new FilterDescriptor("PropString", FilterOperator.IsEqualTo, "asd");

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => (x.PropString.ToUpper() = \"ASD\")", expr.ToString());
        }

        [Test]
        public void CanCreateSampleNotEqualsToFilter()
        {
            var filter = new FilterDescriptor("PropString", FilterOperator.IsNotEqualTo, "asd");

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => (x.PropString.ToUpper() != \"ASD\")", expr.ToString());
        }

        [Test]
        public void CanCreateSampleStartsWithFilter()
        {
            var filter = new FilterDescriptor("PropString", FilterOperator.StartsWith, "asd");

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => x.PropString.ToUpper().StartsWith(\"ASD\")", expr.ToString());
        }

        [Test]
        public void CanCreateSampleEndsWithFilter()
        {
            var filter = new FilterDescriptor("PropString", FilterOperator.EndsWith, "asd");

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => x.PropString.ToUpper().EndsWith(\"ASD\")", expr.ToString());
        }


        [Test]
        public void CanCreateSampleContainsFilterWithNullValue()
        {
            var filter = new FilterDescriptor("PropString", FilterOperator.Contains, null);

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => x.PropString.ToUpper().Contains(null)", expr.ToString());
        }

        [Test]
        public void CanCreateSampleEqualsToFilterWithNullValue()
        {
            var filter = new FilterDescriptor("PropString", FilterOperator.IsEqualTo, null);

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => (x.PropString.ToUpper() = null)", expr.ToString());
        }


        [Test]
        public void CanCreateNoFilterExpression()
        {
            var filter = Helper.Filters();

            var expr = new FilterVisitor<ClassA>().MakePredicate(filter);

            Assert.AreEqual("x => True", expr.ToString());
        }

        [Test]
        public void CanCreateNullFilterExpression()
        {
            var expr = new FilterVisitor<ClassA>().MakePredicate(null);

            Assert.AreEqual("x => True", expr.ToString());
        }

    }
}
