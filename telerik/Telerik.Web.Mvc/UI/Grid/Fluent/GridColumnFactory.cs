// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Creates columns for the <see cref="Grid{TModel}" />.
    /// </summary>
    /// <typeparam name="TModel">The type of the data item to which the grid is bound to</typeparam>
    public class GridColumnFactory<TModel> : IHideObjectMembers 
        where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridColumnFactory{TModel}"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public GridColumnFactory(Grid<TModel> container)
        {
            Guard.IsNotNull(container, "container");

            Container = container;
        }

        public Grid<TModel> Container
        {
            get;
            private set;
        }

        public void LoadSettings(IEnumerable<GridColumnSettings> settings)
        {
            var generator = new GridColumnGenerator<TModel>(Container);

            foreach (var setting in settings)
            {
                Container.Columns.Add(generator.CreateColumn(setting));
            }
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

            GridBoundColumn<TModel, TValue> column = new GridBoundColumn<TModel, TValue>(Container, expression);

            Container.Columns.Add(column);

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

            if (Container.DataSource != null)
            {
                liftMemberAccess = Container.DataSource.AsQueryable().Provider.IsLinqToObjectsProvider();
            }

            LambdaExpression lambdaExpression = ExpressionBuilder.Lambda<TModel>(memberType, memberName, liftMemberAccess);

            Type columnType = typeof(GridBoundColumn<,>).MakeGenericType(new[] { typeof(TModel), lambdaExpression.Body.Type });

            ConstructorInfo constructor = columnType.GetConstructor(new[] { Container.GetType(), lambdaExpression.GetType() });

            IGridBoundColumn column = (IGridBoundColumn)constructor.Invoke(new object[] { Container, lambdaExpression });
            
            column.Member = memberName;
            column.Title = memberName.AsTitle();
            
            if (memberType != null)
            {
                column.MemberType = memberType;
            }

            Container.Columns.Add((GridColumnBase<TModel>)column);

            return new GridBoundColumnBuilder<TModel>(column);
        }

        /// <summary>
        /// Defines a template column.
        /// </summary>
        /// <param name="templateAction"></param>
        /// <returns></returns>
        [Obsolete("Use Template(Action<TModel>) instead")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual GridTemplateColumnBuilder<TModel> Add(Action<TModel> templateAction)
        {
            return Template(templateAction);
        }
        
        /// <summary>
        /// Defines a template column.
        /// </summary>
        /// <param name="templateAction"></param>
        /// <returns></returns>
        public virtual GridTemplateColumnBuilder<TModel> Template(Action<TModel> templateAction)
        {
            Guard.IsNotNull(templateAction, "templateAction");

            GridTemplateColumn<TModel> column = new GridTemplateColumn<TModel>(Container, templateAction);
            Container.Columns.Add(column);

            return new GridTemplateColumnBuilder<TModel>(column);
        }

        public virtual GridTemplateColumnBuilder<TModel> Template(Func<TModel, object> template)
        {
            Guard.IsNotNull(template, "templateAction");

            GridTemplateColumn<TModel> column = new GridTemplateColumn<TModel>(Container, template);
            Container.Columns.Add(column);

            return new GridTemplateColumnBuilder<TModel>(column);
        }

        /// <summary>
        /// Defines a command column.
        /// </summary>
        /// <param name="commandAction"></param>
        /// <returns></returns>
        public virtual GridActionColumnBuilder Command(Action<GridActionCommandFactory<TModel>> commandAction)
        {
            Guard.IsNotNull(commandAction, "commandAction");

            GridActionColumn<TModel> column = new GridActionColumn<TModel>(Container);
            
            commandAction(new GridActionCommandFactory<TModel>(column));

            Container.Columns.Add(column);

            return new GridActionColumnBuilder(column);
        }
    }
}