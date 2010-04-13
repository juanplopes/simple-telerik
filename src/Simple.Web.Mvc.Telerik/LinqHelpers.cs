using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Simple.Web.Mvc.Telerik
{
    public static class LinqHelpers
    {
        public static MethodInfo OrderBy {get; private set;}
        public static MethodInfo OrderByDesc {get; private set;}
        public static MethodInfo ThenBy {get; private set;}
        public static MethodInfo ThenByDesc {get; private set;}
        public static MethodInfo Where { get; private set; }

        public static MethodInfo Skip { get; private set; }
        public static MethodInfo Take { get; private set; }

        static LinqHelpers()
        {
            OrderBy = GetMethod("OrderBy");
            OrderByDesc = GetMethod("OrderByDescending");
            ThenBy = GetMethod("ThenBy");
            ThenByDesc = GetMethod("ThenByDescending");
            Where = GetMethod("Where");

            Skip = GetMethod("Skip");
            Take = GetMethod("Take");
        }

        private static MethodInfo GetMethod(string name)
        {
            var methods = typeof(Queryable).GetMember(name);
            return methods.OfType<MethodInfo>().Where(x => x.GetParameters().Length == 2).First();
        }

        public static MethodInfo GetFor(MethodInfo method, Type entityType)
        {
            return method.MakeGenericMethod(entityType);
        }

        public static MethodInfo GetFor(MethodInfo method, Type entityType, Type returnType)
        {
            return method.MakeGenericMethod(entityType, returnType);
        }

        public static Expression GetPropertyExpression(Expression expr, string propertyPath)
        {
            return GetPropertyExpression(expr, propertyPath.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries));
        }


        public static Expression GetPropertyExpression(Expression expr, IList<string> propertyPath)
        {
            Expression ret = expr;

            foreach (var prop in propertyPath)
                ret = Expression.Property(ret, prop);

            return ret;
        }
    }
}
