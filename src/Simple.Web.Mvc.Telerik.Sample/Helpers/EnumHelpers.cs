using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple.Reflection;
using System.ComponentModel;

namespace Simple.Web.Mvc.Telerik.Sample.Helpers
{
    public static class EnumHelpers
    {
        public static string GetDescription(this Enum value)
        {
            var attr = AttributeCache.Do.First<DescriptionAttribute>(
                value.GetType().GetField(Enum.GetName(value.GetType(), value)));
            if (attr != null) return attr.Description;
            else return null;
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
