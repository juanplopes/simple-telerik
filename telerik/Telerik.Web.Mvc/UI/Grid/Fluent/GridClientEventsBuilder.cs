// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.Web.Mvc;
    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="Grid{T}.ClientEvents"/>.
    /// </summary>
    public class GridClientEventsBuilder : IHideObjectMembers
    {
        private readonly GridClientEvents events;
        private readonly ViewContext viewContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridClientEventsBuilder"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        /// <param name="viewContext">The view context.</param>
        public GridClientEventsBuilder(GridClientEvents events, ViewContext viewContext)
        {
            this.events = events;
            this.viewContext = viewContext;
        }

        /// <summary>
        /// Defines the inline handler of the OnLoad client-side event.
        /// </summary>
        /// <param name="onLoadInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnLoad(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //Load handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnLoad(Action onLoadInlineCode)
        {
            Guard.IsNotNull(onLoadInlineCode, "onLoadInlineCode");

            events.OnLoad.InlineCode = onLoadInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnLoad client-side event.
        /// </summary>
        /// <param name="onLoadHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnLoad("onLoad"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnLoad(string onLoadHandlerName)
        {
            Guard.IsNotNullOrEmpty(onLoadHandlerName, "onLoadHandlerName");

            events.OnLoad.HandlerName = onLoadHandlerName;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnEdit client-side event.
        /// </summary>
        /// <param name="onEditHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnEdit("onEdit"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnEdit(string onEditHandlerName)
        {
            Guard.IsNotNullOrEmpty(onEditHandlerName, "onEditHandlerName");

            events.OnEdit.HandlerName = onEditHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnEdit client-side event.
        /// </summary>
        /// <param name="onEditInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnEdit(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //edit handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnEdit(Action onEditInlineCode)
        {
            Guard.IsNotNull(onEditInlineCode, "onEditInlineCode");

            events.OnEdit.InlineCode = onEditInlineCode;

            return this;
        }
        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnSave client-side event.
        /// </summary>
        /// <param name="onSaveHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnSave("onSave"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnSave(string onSaveHandlerName)
        {
            Guard.IsNotNullOrEmpty(onSaveHandlerName, "onSaveHandlerName");

            events.OnSave.HandlerName = onSaveHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnSave client-side event.
        /// </summary>
        /// <param name="onSaveInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnSave(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //edit handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnSave(Action onSaveInlineCode)
        {
            Guard.IsNotNull(onSaveInlineCode, "onSaveInlineCode");

            events.OnSave.InlineCode = onSaveInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnDetailViewExpand client-side event.
        /// </summary>
        /// <param name="onDetailViewExpandHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnDetailViewExpand("onDetailViewExpand"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnDetailViewExpand(string onDetailViewExpandHandlerName)
        {
            Guard.IsNotNullOrEmpty(onDetailViewExpandHandlerName, "onDetailViewExpandHandlerName");

            events.OnDetailViewExpand.HandlerName = onDetailViewExpandHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnDetailViewExpand client-side event.
        /// </summary>
        /// <param name="onDetailViewExpandInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnDetailViewExpand(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //edit handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnDetailViewExpand(Action onDetailViewExpandInlineCode)
        {
            Guard.IsNotNull(onDetailViewExpandInlineCode, "onDetailViewExpandInlineCode");

            events.OnSave.InlineCode = onDetailViewExpandInlineCode;

            return this;
        }
        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnDetailViewCollapse client-side event.
        /// </summary>
        /// <param name="onDetailViewCollapseHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnDetailViewCollapse("onDetailViewCollapse"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnDetailViewCollapse(string onDetailViewCollapseHandlerName)
        {
            Guard.IsNotNullOrEmpty(onDetailViewCollapseHandlerName, "onDetailViewCollapseHandlerName");

            events.OnDetailViewCollapse.HandlerName = onDetailViewCollapseHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnDetailViewCollapse client-side event.
        /// </summary>
        /// <param name="onDetailViewCollapseInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnDetailViewCollapse(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //edit handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnDetailViewCollapse(Action onDetailViewCollapseInlineCode)
        {
            Guard.IsNotNull(onDetailViewCollapseInlineCode, "onDetailViewCollapseInlineCode");

            events.OnSave.InlineCode = onDetailViewCollapseInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnDelete client-side event.
        /// </summary>
        /// <param name="onDeleteHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnSave("onDelete"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnDelete(string onDeleteHandlerName)
        {
            Guard.IsNotNullOrEmpty(onDeleteHandlerName, "onDeleteHandlerName");

            events.OnDelete.HandlerName = onDeleteHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnSave client-side event.
        /// </summary>
        /// <param name="onDeleteInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnSave(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //edit handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnDelete(Action onDeleteInlineCode)
        {
            Guard.IsNotNull(onDeleteInlineCode, "onDeleteInlineCode");

            events.OnDelete.InlineCode = onDeleteInlineCode;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnColumnResize client-side event.
        /// </summary>
        /// <param name="onLoadInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnColumnResize(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnColumnResize(Action onColumnResizeInlineCode)
        {
            Guard.IsNotNull(onColumnResizeInlineCode, "onColumnResizeInlineCode");

            events.OnColumnResize.InlineCode = onColumnResizeInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnColumnResize client-side event.
        /// </summary>
        /// <param name="onLoadHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnColumnResize("onColumnResize"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnColumnResize(string onColumnResizeHandlerName)
        {
            Guard.IsNotNullOrEmpty(onColumnResizeHandlerName, "onColumnResizeHandlerName");

            events.OnColumnResize.HandlerName = onColumnResizeHandlerName;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnRowSelect client-side event.
        /// </summary>
        /// <param name="onRowSelectHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnRowSelect("onRowSelect"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnRowSelect(string onRowSelectHandlerName)
        {
            Guard.IsNotNullOrEmpty(onRowSelectHandlerName, "onRowSelectHandlerName");

            events.OnRowSelect.HandlerName = onRowSelectHandlerName;

            return this;
        }
        
        [Obsolete("Use OnRowSelect instead")]
        public GridClientEventsBuilder OnRowSelected(string onRowSelectedHandlerName)
        {
            return OnRowSelect(onRowSelectedHandlerName);
        }

        /// <summary>
        /// Defines the inline handler of the OnRowSelect client-side event.
        /// </summary>
        /// <param name="onLoadAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnRowSelect(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //Error handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnRowSelect(Action onRowSelectInlineCode)
        {
            Guard.IsNotNull(onRowSelectInlineCode, "onRowSelectInlineCode");

            events.OnRowSelect.InlineCode = onRowSelectInlineCode;

            return this;
        }

        [Obsolete("Use OnRowSelect instead")]
        public GridClientEventsBuilder OnRowSelected(Action onRowSelectedInlineCode)
        {
            return OnRowSelected(onRowSelectedInlineCode);
        }

        /// <summary>
        /// Defines the inline handler of the OnError client-side event.
        /// </summary>
        /// <param name="onErrorInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnError(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //Error handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnError(Action onErrorInlineCode)
        {
            Guard.IsNotNull(onErrorInlineCode, "onErrorInlineCode");

            events.OnError.InlineCode = onErrorInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnError client-side event.
        /// </summary>
        /// <param name="onErrorHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnError("onError"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnError(string onErrorHandlerName)
        {
            Guard.IsNotNullOrEmpty(onErrorHandlerName, "onErrorHandlerName");

            events.OnError.HandlerName = onErrorHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline error handler of the OnDataBound client-side event.
        /// </summary>
        /// <param name="onDataBoundInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnDataBound(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //data bound handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnDataBound(Action onDataBoundInlineCode)
        {
            Guard.IsNotNull(onDataBoundInlineCode, "onDataBoundInlineCode");

            events.OnDataBound.InlineCode = onDataBoundInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnDataBound client-side event.
        /// </summary>
        /// <param name="onDataBoundHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnDataBound("onDataBound"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnDataBound(string onDataBoundHandlerName)
        {
            Guard.IsNotNullOrEmpty(onDataBoundHandlerName, "onDataBoundHandlerName");

            events.OnDataBound.HandlerName = onDataBoundHandlerName;

            return this;
        }
        /// <summary>
        /// Defines the inline error handler of the OnDataBinding client-side event.
        /// </summary>
        /// <param name="onDataBindingInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnDataBinding(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     //data binding handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnDataBinding(Action onDataBindingInlineCode)
        {
            Guard.IsNotNull(onDataBindingInlineCode, "onDataBindingInlineCode");

            events.OnDataBinding.InlineCode = onDataBindingInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnDataBinding client-side event.
        /// </summary>
        /// <param name="onDataBindingHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnDataBinding("onDataBinding"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnDataBinding(string onDataBindingHandlerName)
        {
            Guard.IsNotNullOrEmpty(onDataBindingHandlerName, "onDataBindingHandlerName");

            events.OnDataBinding.HandlerName = onDataBindingHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline error handler of the OnRowDataBound client-side event.
        /// </summary>
        /// <param name="onRowDataBoundInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnRowDataBound(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     var row = e.row;
        ///                     var dataItem = e.dataItem;
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnRowDataBound(Action onRowDataBoundInlineCode)
        {
            Guard.IsNotNull(onRowDataBoundInlineCode, "onRowDataBoundInlineCode");

            events.OnRowDataBound.InlineCode = onRowDataBoundInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnRowDataBound client-side event.
        /// </summary>
        /// <param name="onRowDataBoundHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnRowDataBound("onRowDataBound"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnRowDataBound(string onRowDataBoundHandlerName)
        {
            Guard.IsNotNullOrEmpty(onRowDataBoundHandlerName, "onRowDataBoundHandlerName");

            events.OnRowDataBound.HandlerName = onRowDataBoundHandlerName;

            return this;
        }
    }
}