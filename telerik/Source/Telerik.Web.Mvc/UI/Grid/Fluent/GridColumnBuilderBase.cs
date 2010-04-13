// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System.Collections.Generic;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring columns.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TColumnBuilder">The type of the column builder.</typeparam>
    public abstract class GridColumnBuilderBase<TColumn, TColumnBuilder> : IHideObjectMembers
        where TColumnBuilder : GridColumnBuilderBase<TColumn, TColumnBuilder>
        where TColumn : IGridColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridColumnBuilderBase&lt;T, TColumnBuilder&gt;"/> class.
        /// </summary>
        /// <param name="column">The column.</param>
        protected GridColumnBuilderBase(TColumn column)
        {
            Guard.IsNotNull(column, "column");

            Column = column;
        }

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>The column.</value>
        public TColumn Column
        {
            get;
            private set;
        }

        /// <summary>
        /// Sets the title displayed in the header of the column.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderID).Title("ID"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TColumnBuilder Title(string text)
        {
            Column.Title = text;

            return this as TColumnBuilder;
        }

        /// <summary>
        /// Sets the HTML attributes applied to the header cell of the column.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderID).HeaderHtmlAttributes(new {@class="order-header"}))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TColumnBuilder HeaderHtmlAttributes(object attributes)
        {
            MergeAttributes(Column.HeaderHtmlAttributes, attributes);

            return this as TColumnBuilder;
        }

        /// <summary>
        /// Sets the HTML attributes applied to the content cell of the column.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderID).HtmlAttributes(new {@class="order-cell"}))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TColumnBuilder HtmlAttributes(object attributes)
        {
            MergeAttributes(Column.HtmlAttributes, attributes);

            return this as TColumnBuilder;
        }

        /// <summary>
        /// Sets the width of the column in pixels.
        /// </summary>
        /// <param name="pixelWidth">The width in pixels.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderID).Width(100))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TColumnBuilder Width(int pixelWidth)
        {
            Guard.IsNotNegative(pixelWidth, "pixelWidth");

            Column.Width = pixelWidth + "px";

            return this as TColumnBuilder;
        }

        /// <summary>
        /// Sets the width of the column.
        /// </summary>
        /// <param name="value">The width to set.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .Columns(columns => columns.Bound(o => 
        ///            {
        ///                %&gt;
        ///                     &lt;%= Html.ActionLink("Edit", "Home", new { id = o.OrderID}) %&gt;
        ///                &lt;%
        ///            })
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TColumnBuilder Width(string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            Column.Width = value;

            return this as TColumnBuilder;
        }

        /// <summary>
        /// Makes the column visible or not. By default all columns are visible. Invisible columns are not rendered in the output HTML.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderID).Visible((bool)ViewData["visible"]))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TColumnBuilder Visible(bool value)
        {
            Column.Visible = value;

            return this as TColumnBuilder;
        }

        /// <summary>
        /// Makes the column hidden or not. By default all columns are not hidden. Hidden columns are rendered in the output HTML but are hidden.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderID).Hidden((bool)ViewData["hidden"]))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TColumnBuilder Hidden(bool value)
        {
            Column.Hidden = value;

            Column.HtmlAttributes["style"] += "display:none;width:0;";

            return this as TColumnBuilder;
        }

        /// <summary>
        /// Hides a column. By default all columns are not hidden. Hidden columns are rendered in the output HTML but are hidden.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderID).Hidden())
        /// %&gt;
        /// </code>
        /// </example>
        public virtual TColumnBuilder Hidden()
        {
            Column.Hidden = true;

            return this as TColumnBuilder;
        }
        private static void MergeAttributes(IDictionary<string, object> target, object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");

            target.Clear();
            target.Merge(attributes);
        }
    }
}