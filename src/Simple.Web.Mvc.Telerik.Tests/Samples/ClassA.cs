using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Web.Mvc.Telerik.Tests.Samples
{
    public class ClassA
    {
        public enum TestEnum
        {
            Value1 = 1,
            Value2 = 3,
            Value3 = 2
        }

        public int PropInt { get; set; }
        public int? PropIntNullable { get; set; }
        public string PropString { get; set; }
        public ClassB PropB { get; set; }
        public TestEnum PropEnum { get; set; }
    }
}
