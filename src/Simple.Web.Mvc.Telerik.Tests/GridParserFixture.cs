using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.Mvc;
using Simple.Web.Mvc.Telerik.Tests.Samples;
using NUnit.Framework;

namespace Simple.Web.Mvc.Telerik.Tests
{
    public class GridParserFixture
    {
        [Test]
        public void CanGenerateExpresionWithFilterOnly()
        {
            var filter = "startswith(PropString,'a')~and~PropB.PropDateTimeNullable~gt~datetime'1984-12-06T12-00-00'";
            var cmd = GridCommand.Parse(0, 0, null, null, filter);
            var expr = GridParser.Parse<ClassA>(cmd, 50);

            Assert.AreEqual("q => q.Where(x => (x.PropString.ToUpper().StartsWith(\"A\") && (x.PropB.PropDateTimeNullable > 12/6/1984 12:00:00 PM)))", expr.Map.ToString());
            Assert.AreEqual("q => q.Take(50)", expr.Reduce.ToString());
        }

        [Test]
        public void CanGenerateExpresionWithEmptyFilter()
        {
            var cmd = GridCommand.Parse(0, 0, null, null, string.Empty);
            var expr = GridParser.Parse<ClassA>(cmd, 50);

            Assert.AreEqual("q => q", expr.Map.ToString());
            Assert.AreEqual("q => q.Take(50)", expr.Reduce.ToString());
        }

        [Test]
        public void CanGenerateExpresionWithSortOnly()
        {
            var cmd = GridCommand.Parse(0, 0, "PropInt-asc~PropB.PropDateTimeNullable-desc", null, null);
            var expr = GridParser.Parse<ClassA>(cmd, 50);

            Assert.AreEqual("q => q.OrderBy(x => x.PropInt).ThenByDescending(x => x.PropB.PropDateTimeNullable)", expr.Map.ToString());
            Assert.AreEqual("q => q.Take(50)", expr.Reduce.ToString());
        }

        [Test]
        public void CanGenerateExpresionWithSortAndFilter()
        {
            var sort = "PropInt-asc~PropB.PropDateTimeNullable-desc";
            var filter = "startswith(PropString,'a')~and~PropB.PropDateTimeNullable~gt~datetime'1984-12-06T11-00-00'";

            var cmd = GridCommand.Parse(0, 0, sort, null, filter);
            var expr = GridParser.Parse<ClassA>(cmd, 50);

            Assert.AreEqual("q => q.Where(x => (x.PropString.ToUpper().StartsWith(\"A\") && (x.PropB.PropDateTimeNullable > 12/6/1984 11:00:00 AM))).OrderBy(x => x.PropInt).ThenByDescending(x => x.PropB.PropDateTimeNullable)", expr.Map.ToString());
            Assert.AreEqual("q => q.Take(50)", expr.Reduce.ToString());
        }

        [Test]
        public void CanGenerateExpresionWithSortAndGroupOnTheSameField()
        {
            var sort = "PropInt-asc~PropB.PropDateTimeNullable-desc";
            var grouping = "PropB.PropDateTimeNullable-desc";

            var cmd = GridCommand.Parse(0, 0, sort, grouping, null);
            var expr = GridParser.Parse<ClassA>(cmd, 50);

            Assert.AreEqual("q => q.OrderByDescending(x => x.PropB.PropDateTimeNullable).ThenBy(x => x.PropInt)", expr.Map.ToString());
            Assert.AreEqual("q => q.Take(50)", expr.Reduce.ToString());
        }
    }
}
