// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Extensions;
    using Infrastructure;
    using Infrastructure.Implementation.Expressions;
    
    /// <summary>
    /// Creates columns for the <see cref="Grid{T}" />.
    /// </summary>
    /// <typeparam name="TModel">The type of the data item to which the grid is bound to</typeparam>
    public class GridColumnFactory<TModel> : IHideObjectMembers where TModel : class
    {
        private readonly Grid<TModel> container;

        public GridColumnFactory(Grid<TModel> container)
        {
            Guard.IsNotNull(container, "container");

            this.container = container;
        }
        
        /// <summary>
        /// Defines a bound column.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        [Obsolete("Use Bound(Expression<Func<TModel, TValue>>) instead")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual GridBoundColumnBuilder<TModel> Add<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            return Bound(expression);
        }

        /// <summary>
        /// Defines a bound column.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual GridBoundColumnBuilder<TModel> Bound<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            GridBoundColumn<TModel, TValue> column = new GridBoundColumn<TModel, TValue>(container, expression);

            container.Columns.Add(column);

            return new GridBoundColumnBuilder<TModel>(column);
        }

        /// <summary>
        /// Defines a bound column.
        /// </summary>
        public virtual GridBoundColumnBuilder<TModel> Bound(string memberName)
        {
            return Bound(null, memberName);
        }

        /// <summary>
        /// Defines a bound column.
        /// </summary>
        public virtual GridBoundColumnBuilder<TModel> Bound(Type memberType, string memberName)
        {
            bool liftMemberAccess = false;

            if (container.DataSource != null)
            {
                liftMemberAccess = container.DataSource.AsQueryable().Provider.IsLinqToObjectsProvider();
            }

            LambdaExpression lambdaExpression = ExpressionBuilder.Lambda<TModel>(memberType, memberName, liftMemberAccess);

            Type columnType = typeof(GridBoundColumn<,>).MakeGenericType(new[] { typeof(TModel), lambdaExpression.Body.Type });

            ConstructorInfo constructor = columnType.GetConstructor(new[] { container.GetType(), lambdaExpression.GetType() });

            GridColumnBase<TModel> column = (GridColumnBase<TModel>)constructor.Invoke(new object[] { container, lambdaExpression });
            column.Member = memberName;
            
            if (memberType != null)
            {
                column.MemberType = memberType;
            }

            container.Columns.Add(column);

            return new GridBoundColumnBuilder<TModel>((IGridBoundColumn)column);
        }

        /// <summary>
        /// Defines a template column.
        /// </summary>
        /// <param name="templateAction"></param>
        /// <returns></returns>
        [Obsolete("Use Template(Action<TModel>) instead")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual GridTemplateColumnBuilder Add(Action<TModel> templateAction)
        {
            return Template(templateAction);
        }
        
        /// <summary>
        /// Defines a template column.
        /// </summary>
        /// <param name="templateAction"></param>
        /// <returns></returns>
        public virtual GridTemplateColumnBuilder Template(Action<TModel> templateAction)
        {
            Guard.IsNotNull(templateAction, "templateAction");

            GridTemplateColumn<TModel> column = new GridTemplateColumn<TModel>(container, templateAction);
            container.Columns.Add(column);

            return new GridTemplateColumnBuilder(column);
        }

        /// <summary>
        /// Defines a command column.
        /// </summary>
        /// <param name="commandAction"></param>
        /// <returns></returns>
        public virtual GridActionColumnBuilder Command(Action<GridActionCommandFactory<TModel>> commandAction)
        {
            Guard.IsNotNull(commandAction, "commandAction");

            GridActionColumn<TModel> column = new GridActionColumn<TModel>(container);
            
            commandAction(new GridActionCommandFactory<TModel>(column));

            container.Columns.Add(column);

            return new GridActionColumnBuilder(column);
        }
    }
}