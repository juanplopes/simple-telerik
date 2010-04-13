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

            events.OnLoad = onLoadInlineCode;

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

            events.OnLoad = HandlerAction(onLoadHandlerName);

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnRowSelected client-side event.
        /// </summary>
        /// <param name="onLoadHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        ///             .ClientEvents(events => events.OnRowSelected("onRowSelected"))
        /// %&gt;
        /// </code>
        /// </example>
        public GridClientEventsBuilder OnRowSelected(string onRowSelectedHandlerName)
        {
            Guard.IsNotNullOrEmpty(onRowSelectedHandlerName, "onRowSelectedHandlerName");

            events.OnRowSelected = HandlerAction(onRowSelectedHandlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnRowSelected client-side event.
        /// </summary>
        /// <param name="onLoadAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Grid(Model)
        ///            .Name("Grid")
        ///            .ClientEvents(events => events.OnRowSelected(() =>
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
        public GridClientEventsBuilder OnRowSelected(Action onRowSelectedAction)
        {
            Guard.IsNotNull(onRowSelectedAction, "onRowSelectedAction");

            events.OnRowSelected = onRowSelectedAction;

            return this;
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

            events.OnError = onErrorInlineCode;

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

            events.OnError = HandlerAction(onErrorHandlerName);

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

            events.OnDataBound = onDataBoundInlineCode;

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

            events.OnDataBound = HandlerAction(onDataBoundHandlerName);

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

            events.OnDataBinding = onDataBindingInlineCode;

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

            events.OnDataBinding = HandlerAction(onDataBindingHandlerName);

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

            events.OnRowDataBound = onRowDataBoundInlineCode;

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

            events.OnRowDataBound = HandlerAction(onRowDataBoundHandlerName);

            return this;
        }

        private Action HandlerAction(string handlerName)
        {
            return () => viewContext.HttpContext.Response.Write(handlerName);
        }
    }
}