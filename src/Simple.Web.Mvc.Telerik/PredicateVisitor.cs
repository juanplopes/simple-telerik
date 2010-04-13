using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Telerik.Web.Mvc;

namespace Simple.Web.Mvc.Telerik
{
    public class PredicateVisitor
    {
        public ParameterExpression Parameter { get; protected set; }

        public PredicateVisitor(ParameterExpression parameter)
        {
            this.Parameter = parameter;
        }

        public virtual Expression Visit(IList<IFilterDescriptor> list)
        {
            return this.VisitFilterList(list, FilterCompositionLogicalOperator.And);
        }

        public virtual Expression Visit(IFilterDescriptor filter)
        {

            if (filter is CompositeFilterDescriptor)
                return VisitComposite(filter as CompositeFilterDescriptor);
            else if (filter is FilterDescriptor)
                return VisitBinary(filter as FilterDescriptor);

            return Expression.Constant(true);
        }

        protected virtual Expression VisitBinary(FilterDescriptor filter)
        {
            Expression property =
                LinqHelpers.GetPropertyExpression(Parameter, filter.Member);

            switch (filter.Operator)
            {
                case FilterOperator.Contains:
                    return VisitContains(filter, property);
                case FilterOperator.StartsWith:
                    return VisitStartsWith(filter, property);
                case FilterOperator.EndsWith:
                    return VisitEndsWith(filter, property);
                case FilterOperator.IsEqualTo:
                    return VisitIsEqualTo(filter, property);
                case FilterOperator.IsNotEqualTo:
                    return VisitIsNotEqualTo(filter, property);
                case FilterOperator.IsContainedIn:
                    return VisitIsContainedIn(filter, property);
                case FilterOperator.IsGreaterThan:
                    return VisitIsGreaterThan(filter, property);
                case FilterOperator.IsGreaterThanOrEqualTo:
                    return VisitIsGreaterThanOrEqualTo(filter, property);
                case FilterOperator.IsLessThan:
                    return VisitIsLessThan(filter, property);
                case FilterOperator.IsLessThanOrEqualTo:
                    return VisitIsLessThanOrEqualTo(filter, property);
                default:
                    return Expression.Constant(true);
            }
        }

        protected virtual Expression NormalizeStringExpression(Expression property)
        {
            return Expression.Call(property, "ToUpper", new Type[0]);
        }

        protected virtual object NormalizeStringParameter(object value)
        {
            string realValue = value as string;
            if (value != null) return value.ToString().ToUpper();
            else return null;
        }

        protected virtual Expression VisitStringMethod(FilterDescriptor filter, Expression property, string methodName)
        {
            var value = NormalizeStringParameter(filter.Value);
            var expr = NormalizeStringExpression(property);

            var method = typeof(string).GetMethod(methodName, new Type[] { typeof(string) });
            var parameters = new[] { 
                Expression.Constant(value, typeof(string))
            };

            return Expression.Call(expr, method, parameters);
        }

        protected virtual Expression VisitContains(FilterDescriptor filter, Expression property)
        {
            return VisitStringMethod(filter, property, "Contains");
        }

        protected virtual Expression VisitStartsWith(FilterDescriptor filter, Expression property)
        {
            return VisitStringMethod(filter, property, "StartsWith");
        }

        protected virtual Expression VisitEndsWith(FilterDescriptor filter, Expression property)
        {
            return VisitStringMethod(filter, property, "EndsWith");
        }

        protected virtual Expression VisitConstantValue(object value, Expression property)
        {
            Type type = property.Type;

            if (value != null)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    type = type.GetGenericArguments().First();
                    value = property.Type.GetConstructor(new[] { type }).Invoke(new[] { Convert.ChangeType(value, type) });
                }
                else
                {
                    value = Convert.ChangeType(value, type);
                }
            }
            return Expression.Constant(value, property.Type);
        }

        protected virtual void CheckStringParameters(Expression property, ref Expression expr, ref object value)
        {
            if (property.Type == typeof(string))
            {
                expr = NormalizeStringExpression(expr);
                value = NormalizeStringParameter(value);
            }
        }

        protected virtual Expression VisitIsEqualTo(FilterDescriptor filter, Expression property)
        {
            Expression expr = property;
            object value = filter.Value;

            CheckStringParameters(property, ref expr, ref value);

            return Expression.Equal(expr, VisitConstantValue(value, property));
        }

        protected virtual Expression VisitIsNotEqualTo(FilterDescriptor filter, Expression property)
        {
            Expression expr = property;
            object value = filter.Value;

            CheckStringParameters(property, ref expr, ref value);

            return Expression.NotEqual(expr, VisitConstantValue(value, property));
        }

        protected virtual Expression VisitIsLessThan(FilterDescriptor filter, Expression property)
        {
            return Expression.LessThan(property, VisitConstantValue(filter.Value, property));
        }

        protected virtual Expression VisitIsLessThanOrEqualTo(FilterDescriptor filter, Expression property)
        {
            return Expression.LessThanOrEqual(property, VisitConstantValue(filter.Value, property));
        }

        protected virtual Expression VisitIsGreaterThan(FilterDescriptor filter, Expression property)
        {
            return Expression.GreaterThan(property, VisitConstantValue(filter.Value, property));
        }

        private Expression VisitIsGreaterThanOrEqualTo(FilterDescriptor filter, Expression property)
        {
            return Expression.GreaterThanOrEqual(property, VisitConstantValue(filter.Value, property));
        }

        protected virtual Expression VisitIsContainedIn(FilterDescriptor filter, Expression property)
        {
            throw new NotImplementedException();
        }

        protected virtual Expression VisitComposite(CompositeFilterDescriptor filter)
        {
            return VisitFilterList(filter.FilterDescriptors, filter.LogicalOperator);
        }

        protected virtual Expression VisitFilterList(IList<IFilterDescriptor> filters, FilterCompositionLogicalOperator op)
        {
            if (filters == null || filters.Count == 0) return Expression.Constant(true);

            var expr = Visit(filters.First());

            foreach (var filter in filters.Skip(1))
            {
                var filterExpr = Visit(filter);
                switch (op)
                {
                    case FilterCompositionLogicalOperator.And:
                        expr = Expression.AndAlso(expr, filterExpr); break;
                    case FilterCompositionLogicalOperator.Or:
                        expr = Expression.OrElse(expr, filterExpr); break;
                }
            }

            return expr;
        }


    }
}
