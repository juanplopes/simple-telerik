// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Mvc;
    using Extensions;
    using Infrastructure;
    using Resources;

    public class GridBoundColumn<TModel, TValue> : GridColumnBase<TModel>, IGridBoundColumn where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridBoundColumn{T}"/> class.
        /// </summary>
        /// <param name="value">The property to which the column is bound to.</param>
        public GridBoundColumn(Grid<TModel> grid, Expression<Func<TModel, TValue>> expression) : base(grid)
        {
            Guard.IsNotNull(expression, "expression");

            if (!typeof(TModel).IsDataRow() && expression.ToMemberExpression() == null)
            {
                throw new InvalidOperationException(TextResource.MemberExpressionRequired);
            }

            Sortable = true;
            Filterable = true;
            Groupable = true;
            Encoded = true;

            Expression = expression;
            Member = expression.MemberWithoutInstance();
            MemberType = TypeFromMemberExpression(expression.ToMemberExpression());
            Value = expression.Compile();

            HtmlBuilder = new GridBoundColumnHtmlBuilder<TModel, TValue>(this, new GridBoundColumnContentHtmlBuilder<TModel, TValue>(this));

#if MVC2
            if (!typeof(TModel).IsDataRow())
            {
                HtmlBuilder = new GridBoundColumnHtmlBuilder<TModel, TValue>(this, new GridBoundColumnDisplayForHtmlBuilder<TModel, TValue>(this));
                Metadata = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
                base.Title = Metadata.DisplayName;
                base.Format = Metadata.DisplayFormatString;
                Visible = Metadata.ShowForDisplay;
                ReadOnly = Metadata.IsReadOnly;
            }
#endif
            base.HeaderHtmlBuilder = new GridBoundColumnHeaderHtmlBuilder<TModel, TValue>(this);
        }

        public override string Title
        {
            get
            {
                if (!base.Title.HasValue())
                {
                    return Member.AsTitle();
                }

                return base.Title;
            }
            set
            {
                base.Title = value;
            }
        }

        public override string Format
        {
            get
            {
                return base.Format;
            }
            set
            {
                base.Format = value;

                #if MVC2
                HtmlBuilder = new GridBoundColumnHtmlBuilder<TModel, TValue>(this, new GridBoundColumnContentHtmlBuilder<TModel, TValue>(this));
                #endif
            }
        }

        public override Action<TModel> Template
        {
            get
            {
                return base.Template;
            }
            set
            {
                base.Template = value;

                HtmlBuilder = new GridBoundColumnHtmlBuilder<TModel, TValue>(this, new GridTemplateColumnHtmlBuilder<TModel>(this));
            }
        }

        #if MVC2

        public ModelMetadata Metadata
        {
            get;
            private set;
        }

        #endif

        /// <summary>
        /// Gets a function which returns the value of the property to which the column is bound to.
        /// </summary>
        public Func<TModel, TValue> Value
        {
            get;
            private set;
        }

        public Expression<Func<TModel, TValue>> Expression
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridColumnBase&lt;T&gt;"/> is encoded.
        /// </summary>
        /// <value><c>true</c> if encoded; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool Encoded
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridColumnBase&lt;T&gt;"/> is sortable.
        /// </summary>
        /// <value><c>true</c> if sortable; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool Sortable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridColumnBase&lt;T&gt;"/> is filterable.
        /// </summary>
        /// <value><c>true</c> if filterable; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool Filterable
        {
            get;
            set;
        }

        private static Type TypeFromMemberExpression(MemberExpression memberExpression)
        {
            if (memberExpression == null)
            {
                return null;
            }

            MemberInfo memberInfo = memberExpression.Member;

            if (memberInfo.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)memberInfo).PropertyType;
            }

            if (memberInfo.MemberType == MemberTypes.Field)
            {
                return ((FieldInfo)memberInfo).FieldType;
            }

            throw new NotSupportedException();
        }
    }
}