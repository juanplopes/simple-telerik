// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.Web;

    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring bound columns
    /// </summary>
    /// <typeparam name="T">The type of the data item</typeparam>
    public class GridBoundColumnBuilder<T> : GridColumnBuilderBase<IGridBoundColumn, GridBoundColumnBuilder<T>>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridBoundColumnBuilder&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="column">The column.</param>
        public GridBoundColumnBuilder(IGridBoundColumn column)
            : base(column)
        {
        }

        /// <summary>
        /// Gets or sets the format for displaying the data.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderDate).Format("{0:dd/MM/yyyy}"))
        /// %&gt;
        /// </code>
        /// </example>        
        public virtual GridBoundColumnBuilder<T> Format(string value)
        {
            // Doing the UrlDecode to allow {0} in ActionLink e.g. Html.ActionLink("Index", "Home", new { id = "{0}" })
            Column.Format = HttpUtility.UrlDecode(value);

            return this;
        }

#if MVC2
        /// <summary>
        /// Makes the column read-only or not. By default bound columns are not read-only.
        /// </summary>
        /// <remarks>
        /// If a column is read-only it cannot be modified during editing.
        /// </remarks>
        /// <param name="value"><c>true</c> if the column should be read-only;otherwise <c>false</c></param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderDate).ReadOnly(true))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBoundColumnBuilder<T> ReadOnly(bool value)
        {
            Column.ReadOnly = value;
            return this;
        }

        /// <summary>
        /// Makes the column read-only.
        /// </summary>
        /// <remarks>
        /// If a column is read-only it cannot be modified during editing.
        /// </remarks>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderDate).ReadOnly())
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBoundColumnBuilder<T> ReadOnly()
        {
            return ReadOnly(true);
        }
#endif
        /// <summary>
        /// Enables or disables sorting the column. All bound columns are sortable by default.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderDate).Sortable(false))
        /// %&gt;
        /// </code>
        /// </example>        
        public virtual GridBoundColumnBuilder<T> Sortable(bool value)
        {
            Column.Sortable = value;

            return this;
        }

        /// <summary>
        /// Enables or disables grouping by that column. All bound columns are groupable by default.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderDate).Groupable(false))
        /// %&gt;
        /// </code>
        /// </example>        
        public virtual GridBoundColumnBuilder<T> Groupable(bool value)
        {
            Column.Groupable = value;

            return this;
        }

        /// <summary>
        /// Enables or disables filtering the column. All bound columns are filterable by default.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderDate).Filterable(false))
        /// %&gt;
        /// </code>
        /// </example>        
        public virtual GridBoundColumnBuilder<T> Filterable(bool value)
        {
            Column.Filterable = value;

            return this;
        }

        /// <summary>
        /// Enables or disables HTML encoding the data of the column. All bound columns are encoded by default.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .Columns(columns => columns.Bound(o => o.OrderDate).Encoded(false))
        /// %&gt;
        /// </code>
        /// </example>        
        public virtual GridBoundColumnBuilder<T> Encoded(bool value)
        {
            Column.Encoded = value;

            return this;
        }

        /// <summary>
        /// Sets the template for the column.
        /// </summary>
        /// <param name="templateAction">The action defining the template.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .Columns(columns => columns
        ///                     .Add(c => c.CustomerID)
        ///                     .Template(() => 
        ///                     { 
        ///                         %&gt;
        ///                          &gt;img 
        ///                             alt="&lt;%= c.CustomerID %&gt;" 
        ///                             src="&lt;%= Url.Content("~/Content/Grid/Customers/" + c.CustomerID + ".jpg") %&gt;" 
        ///                          /&gt;
        ///                         &lt;% 
        ///                     }).Title("Picture");)
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example
        public virtual GridBoundColumnBuilder<T> Template(Action<T> templateAction)
        {
            Guard.IsNotNull(templateAction, "templateAction");

            (Column as GridColumnBase<T>).Template = templateAction;

            return this;
        }

        public virtual GridBoundColumnBuilder<T> ClientTemplate(string value)
        {
            Column.ClientTemplate = value;

            return this;
        }
    }
}