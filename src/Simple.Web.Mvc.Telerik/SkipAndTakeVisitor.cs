using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Simple.Web.Mvc.Telerik
{
    public class SkipAndTakeVisitor<T>
    {
        public virtual Expression Visit(Expression expr, int page, int pageSize)
        {
            if (page < 0 || pageSize < 0) return expr;

            int skip = page * pageSize;
            int take = pageSize;

            if (skip > 0)
            {
                var skipMethod = LinqHelpers.GetFor(LinqHelpers.Skip, typeof(T));
                expr = Expression.Call(null, skipMethod, expr, Expression.Constant(skip));
            }

            if (take > 0)
            {
                var takeMethod = LinqHelpers.GetFor(LinqHelpers.Take, typeof(T));
                expr = Expression.Call(null, takeMethod, expr, Expression.Constant(take));
            }

            return expr;
        }
    }
}

