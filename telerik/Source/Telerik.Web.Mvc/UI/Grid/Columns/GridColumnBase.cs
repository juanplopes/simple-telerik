
// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Web.Routing;

    /// <summary>
    /// Represents a column in the <see cref="Grid{T}"/> component
    /// </summary>
    /// <typeparam name="T">The type of the data item</typeparam>
    public abstract class GridColumnBase<T> : IGridColumn where T : class
    {
        internal string EditorHtml
        {
            get;
            set;
        }

        internal IGridColumnHtmlBuilder<T> HtmlBuilder
        {
            get;
            set;
        }

        internal virtual IGridColumnHeaderHtmlBuilder<T> HeaderHtmlBuilder
        {
            get;
            set;
        }

        protected GridColumnBase(Grid<T> grid)
        {
            Grid = grid;
            HeaderHtmlAttributes = new RouteValueDictionary();
            HtmlAttributes = new RouteValueDictionary();

            Visible = true;
        }

        /// <summary>
        /// Gets or sets the grid.
        /// </summary>
        /// <value>The grid.</value>
        public Grid<T> Grid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets type of the property to which the column is bound to.
        /// </summary>
        public Type MemberType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the member of the column.
        /// </summary>
        /// <value>The member.</value>
        public string Member
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name of the column
        /// </summary>
        [Obsolete("Use the Member property instead")]
        public string Name
        {
            get
            {
                return Member;
            }
            set
            {
                Member = value;
            }
        }

        /// <summary>
        /// Gets the template of the column.
        /// </summary>
        public virtual Action<T> Template
        {
            get;
            set;
        }

        public string ClientTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title of the column.
        /// </summary>
        /// <value>The title.</value>
        public virtual string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the column.
        /// </summary>
        /// <value>The width.</value>
        public string Width
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this column is groupable.
        /// </summary>
        /// <value><c>true</c> if groupable; otherwise, <c>false</c>.</value>
        public bool Groupable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this column is hidden.
        /// </summary>
        /// <value><c>true</c> if hidden; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// Hidden columns are output as HTML but are not visible by the end-user.
        /// </remarks>
        public bool Hidden
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the header HTML attributes.
        /// </summary>
        /// <value>The header HTML attributes.</value>
        public IDictionary<string, object> HeaderHtmlAttributes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the display format for the data.
        /// </summary>
        /// <value>The format.</value>
        public virtual string Format
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this column is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        /// <remarks>
        /// Invisible columns are not output in the HTML.
        /// </remarks>
        public bool Visible
        {
            get;
            set;
        }

#if MVC2        
        public bool ReadOnly
        {
            get;
            set;
        }
#endif
        /// <summary>
        /// Gets the HTML attributes of the cell rendered for the column
        /// </summary>
        /// <value>The HTML attributes.</value>
        public IDictionary<string, object> HtmlAttributes
        {
            get;
            private set;
        }
    }
}