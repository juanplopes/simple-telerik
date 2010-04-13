using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Telerik.Web.Mvc;

namespace Simple.Web.Mvc.Telerik
{
    public class FilterVisitor<T>
    {
        public virtual Expression Visit(Expression expr, params IFilterDescriptor[] filters)
        {
            return Visit(expr, (IList<IFilterDescriptor>)filters);
        }

        public virtual Expression Visit(Expression expr, IList<IFilterDescriptor> filters)
        {
            if (filters == null || filters.Count == 0) return expr;

            return Expression.Call(null, LinqHelpers.GetFor(LinqHelpers.Where, typeof(T)), expr, MakePredicate(filters));
        }

        public virtual Expression<Func<T, bool>> MakePredicate(params IFilterDescriptor[] filters)
        {
            return MakePredicate((IList<IFilterDescriptor>)filters);
        }

        public virtual Expression<Func<T, bool>> MakePredicate(IList<IFilterDescriptor> filters)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var visitor = new PredicateVisitor(param);
            var expr = visitor.Visit(filters);

            return Expression.Lambda<Func<T, bool>>(expr, param);
        }
    }


    
}
