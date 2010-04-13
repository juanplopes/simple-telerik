// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Extensions;
    using Infrastructure;

    public class GridFilterDescriptorFactory<TModel> : IHideObjectMembers where TModel : class
    {
        public GridFilterDescriptorFactory(IList<CompositeFilterDescriptor> filters)
        {
            Guard.IsNotNull(filters, "filters");

            Filters = filters;
        }

        protected IList<CompositeFilterDescriptor> Filters { get; private set; }

        public virtual GridFilterEqualityDescriptorBuilder<bool> Add(Expression<Func<TModel, bool>> expression)
        {
            CompositeFilterDescriptor filter = CreateFilter(expression);

            return new GridFilterEqualityDescriptorBuilder<bool>(filter);
        }

        public virtual GridFilterEqualityDescriptorBuilder<bool?> Add(Expression<Func<TModel, bool?>> expression)
        {
            CompositeFilterDescriptor filter = CreateFilter(expression);

            return new GridFilterEqualityDescriptorBuilder<bool?>(filter);
        }

        public virtual GridFilterComparisonDescriptorBuilder<TValue> Add<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            CompositeFilterDescriptor filter = CreateFilter(expression);

            return new GridFilterComparisonDescriptorBuilder<TValue>(filter);
        }

        public virtual GridFilterStringDescriptorBuilder Add(Expression<Func<TModel, string>> expression)
        {
            CompositeFilterDescriptor filter = CreateFilter(expression);

            return new GridFilterStringDescriptorBuilder(filter);
        }

        protected virtual CompositeFilterDescriptor CreateFilter<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            CompositeFilterDescriptor composite = new CompositeFilterDescriptor
                                                            {
                                                                LogicalOperator = FilterCompositionLogicalOperator.And
                                                            };

            FilterDescriptor descriptor = new FilterDescriptor { Member = expression.MemberWithoutInstance() };

            composite.FilterDescriptors.Add(descriptor);

            Filters.Add(composite);

            return composite;
        }
    }
}