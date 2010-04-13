using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Telerik.Web.Mvc;
using System.ComponentModel;

namespace Simple.Web.Mvc.Telerik
{
    public class SortVisitor<T> : SortVisitor<T, SortDescriptor>
    {

    }

    public class GroupVisitor<T> : SortVisitor<T, GroupDescriptor>
    {

    }

    public class SortVisitor<T, Q>
        where Q:IDescriptor
    {
        public virtual Expression Visit(Expression expr, params Q[] sorts)
        {
            return Visit(expr, (IList<Q>)sorts);
        }

        public virtual Expression Visit(Expression expr, IList<Q> sorts)
        {
            if (sorts == null || sorts.Count == 0) return expr;

            var firstSort = sorts.First();
            
            if (firstSort.SortDirection == ListSortDirection.Ascending)
                expr = VisitFirstAscending(expr, firstSort);
            else
                expr = VisitFirstDescending(expr, firstSort);

            foreach (var sort in sorts.Skip(1))
            {
                if (sort.SortDirection == ListSortDirection.Ascending)
                    expr = VisitAscending(expr, sort);
                else
                    expr = VisitDescending(expr, sort);
            }

            return expr;
        }

        protected virtual Expression VisitFirstAscending(Expression expr, Q sort)
        {
            return VisitByMethod(expr, sort, LinqHelpers.OrderBy);
        }

        protected virtual Expression VisitFirstDescending(Expression expr, Q sort)
        {
            return VisitByMethod(expr, sort, LinqHelpers.OrderByDesc);
        }

        protected virtual Expression VisitAscending(Expression expr, Q sort)
        {
            return VisitByMethod(expr, sort, LinqHelpers.ThenBy);
        }

        protected virtual Expression VisitDescending(Expression expr, Q sort)
        {
            return VisitByMethod(expr, sort, LinqHelpers.ThenByDesc);
        }

        protected virtual Expression VisitByMethod(Expression expr, Q sort, System.Reflection.MethodInfo methodInfo)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var member = LinqHelpers.GetPropertyExpression(parameter, sort.Member);
            var lambda = Expression.Lambda(member, parameter);
            var method = LinqHelpers.GetFor(methodInfo, typeof(T), member.Type);
            return Expression.Call(null, method, expr, lambda);
        }
    }
}
