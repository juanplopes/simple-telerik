using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Infrastructure.Implementation;
using Simple.Web.Mvc.Telerik.Tests.Samples;
using System.ComponentModel;

namespace Simple.Web.Mvc.Telerik.Tests
{
    public static class Helper
    {
        public static Expression<Func<T, bool>> Expr<T>(Expression<Func<T, bool>> expr)
        {
            return expr;
        }

        public static FilterDescriptor Filter(string prop, FilterOperator op, object value)
        {
            return new FilterDescriptor(prop, op, value);
        }

        public static IList<IFilterDescriptor> Filters(params IFilterDescriptor[] filters)
        {
            return filters;
        }

        public static CompositeFilterDescriptor Filters(FilterCompositionLogicalOperator op, params IFilterDescriptor[] filters)
        {
            var collection = new FilterDescriptorCollection();

            foreach (var filter in filters)
                collection.Add(filter);

            return new CompositeFilterDescriptor()
            {
                FilterDescriptors = collection,
                LogicalOperator = op
            };
        }

        public static SortDescriptor Sort(string member, ListSortDirection direction)
        {
            return new SortDescriptor() { Member = member, SortDirection = direction };
        }

        public static IList<SortDescriptor> Sorts(params SortDescriptor[] sorts)
        {
            return sorts;
        }
    }
}
