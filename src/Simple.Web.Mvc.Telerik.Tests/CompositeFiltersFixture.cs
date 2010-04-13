using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Telerik.Web.Mvc;
using Simple.Web.Mvc.Telerik.Tests.Samples;

namespace Simple.Web.Mvc.Telerik.Tests
{
    public class CompositeFiltersFixture
    {
        [Test]
        public void CanParseSimpleFilterList()
        {
            var filters = new[] 
            {
                new FilterDescriptor("PropInt", FilterOperator.IsEqualTo, 42),
                new FilterDescriptor( "PropString", FilterOperator.IsEqualTo, "42")
            };

            var expr = new FilterVisitor<ClassA>().MakePredicate(filters);

            Assert.AreEqual("x => ((x.PropInt = 42) && (x.PropString.ToUpper() = \"42\"))", expr.ToString());
        }

        [Test]
        public void CanParseSimpleFilterListWithCompositeParts()
        {
            var filters = Helper.Filters(
                Helper.Filter("PropInt", FilterOperator.IsEqualTo, 42),
                Helper.Filters(FilterCompositionLogicalOperator.Or,
                    Helper.Filter("PropString", FilterOperator.IsEqualTo, "42"),
                    Helper.Filter("PropB.PropDecimalNullable", FilterOperator.IsEqualTo, 42.0)));

            var expr = new FilterVisitor<ClassA>().MakePredicate(filters);

            Assert.AreEqual("x => ((x.PropInt = 42) && ((x.PropString.ToUpper() = \"42\") || (x.PropB.PropDecimalNullable = 42)))", expr.ToString());
        }

        [Test]
        public void CanParseSimpleFilterListWithTwoCompositeParts()
        {
            var now = DateTime.Now;
            var filters = Helper.Filters(
                 Helper.Filters(FilterCompositionLogicalOperator.And,
                    Helper.Filter("PropInt", FilterOperator.IsEqualTo, 42),
                    Helper.Filter("PropB.PropDateTimeNullable", FilterOperator.IsEqualTo, now)),
                Helper.Filters(FilterCompositionLogicalOperator.Or,
                    Helper.Filter("PropString", FilterOperator.IsEqualTo, "42"),
                    Helper.Filter("PropB.PropDecimalNullable", FilterOperator.IsEqualTo, 42.0)));

            var expr = new FilterVisitor<ClassA>().MakePredicate(filters);

            var str = string.Format("x => (((x.PropInt = 42) && (x.PropB.PropDateTimeNullable = {0})) && ((x.PropString.ToUpper() = \"42\") || (x.PropB.PropDecimalNullable = 42)))"
                ,now);

            Assert.AreEqual(str, expr.ToString());
        }
    }
}
