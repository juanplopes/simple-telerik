// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using Extensions;
    using Infrastructure;

    public class GridColumnGenerator<T> where T : class
    {
        private readonly IPropertyCache propertyCache;
        private readonly Grid<T> grid;

        public GridColumnGenerator(Grid<T> grid) : this(grid, ServiceLocator.Current.Resolve<IPropertyCache>())
        {
        }

        public GridColumnGenerator(Grid<T> grid, IPropertyCache propertyCache)
        {
            Guard.IsNotNull(propertyCache, "grid");
            Guard.IsNotNull(propertyCache, "propertyCache");

            this.grid = grid;
            this.propertyCache = propertyCache;
        }

        public GridColumnGenerator(IPropertyCache propertyCache)
        {
            Guard.IsNotNull(propertyCache, "propertyCache");

            this.propertyCache = propertyCache;
        }

        public IEnumerable<GridColumnBase<T>> GetColumns()
        {
            return propertyCache.GetReadOnlyProperties(typeof(T))
                                .Where(property => property.PropertyType.IsEnum || (property.PropertyType != typeof(object) && property.PropertyType.IsPredefinedType()))
                                .Select(property => CreateBoundColumn(property));
        }

        public GridColumnBase<T> CreateBoundColumn(PropertyInfo property)
        {
            Type columnType = typeof(GridBoundColumn<,>).MakeGenericType(new[] { typeof(T), property.PropertyType });
            Type funcType = typeof(Func<,>).MakeGenericType(new[] { typeof(T), property.PropertyType });
            Type expressionType = typeof(Expression<>).MakeGenericType(new[] { funcType });

            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");

            Expression propertyExpression = Expression.Property(parameterExpression, property);
            
            Expression expression = Expression.Lambda(funcType, propertyExpression, parameterExpression);

            return (GridColumnBase<T>)columnType.GetConstructor(new[] { grid.GetType(), expressionType }).Invoke(new object[] { grid, expression });
        }
    }
}