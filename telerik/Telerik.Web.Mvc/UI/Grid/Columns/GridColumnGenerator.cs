// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Telerik.Web.Mvc.Extensions;

    public class GridColumnGenerator<T> where T : class
    {
        private readonly Grid<T> grid;

        public GridColumnGenerator(Grid<T> grid)
        {
            this.grid = grid;
        }

        public IEnumerable<GridColumnBase<T>> GetColumns()
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .Where(property => property.CanRead && property.GetGetMethod().GetParameters().Length == 0)
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
        
        public GridColumnBase<T> CreateColumn(GridColumnSettings settings)
        {
            GridCommandColumnSettings commandSettings = settings as GridCommandColumnSettings;
            if (commandSettings != null)
            {
                var column = new GridActionColumn<T>(grid);
                
                column.Settings = settings;
                
                foreach (var command in commandSettings.Commands)
                {
                    if (!(command is GridSelectActionCommand))
                    {
                        grid.Editing.Enabled = true;
                    }
                    column.Commands.Add(command);
                }

                return column;

            }
            return CreateBoundColumn(settings);
        }
        
        private GridColumnBase<T> CreateBoundColumn(GridColumnSettings settings)
        {
            var lambdaExpression = ExpressionBuilder.Lambda<T>(null, settings.Member, false);

            var columnType = typeof(GridBoundColumn<,>).MakeGenericType(new[] { typeof(T), lambdaExpression.Body.Type });

            var constructor = columnType.GetConstructor(new[] { grid.GetType(), lambdaExpression.GetType() });

            var column = (GridColumnBase<T>)constructor.Invoke(new object[] { grid, lambdaExpression });

            column.Settings = settings;
            if (settings is GridColumnSettings<T>)
            {
                column.Template = ((GridColumnSettings<T>)settings).Template;
            }
            return column;
        }
    }
}