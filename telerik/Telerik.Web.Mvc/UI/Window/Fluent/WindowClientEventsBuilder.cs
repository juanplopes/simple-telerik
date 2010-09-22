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
    /// Defines the fluent interface for configuring the <see cref="Window.ClientEvents"/>.
    /// </summary>
    public class WindowClientEventsBuilder
    {
        private readonly WindowClientEvents clientEvents;
        private readonly ViewContext viewContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowClientEventsBuilder"/> class.
        /// </summary>
        /// <param name="clientEvents">The client events.</param>
        /// <param name="viewContext">The view context.</param>
        public WindowClientEventsBuilder(WindowClientEvents clientEvents, ViewContext viewContext)
        {
            Guard.IsNotNull(clientEvents, "clientEvents");
            Guard.IsNotNull(viewContext, "viewContext");

            this.clientEvents = clientEvents;
            this.viewContext = viewContext;
        }

        /// <summary>
        /// Defines the inline handler of the OnLoad client-side event
        /// </summary>
        /// <param name="onLoadAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Window()
        ///            .Name("Window")
        ///            .ClientEvents(events => events.OnLoad(() =>
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
        public WindowClientEventsBuilder OnLoad(Action onLoadInlineCode)
        {
            Guard.IsNotNull(onLoadInlineCode, "onLoadInlineCode");

            clientEvents.OnLoad.InlineCode = onLoadInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnLoad client-side event.
        /// </summary>
        /// <param name="onLoadHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Window()
        ///             .Name("Window")
        ///             .ClientEvents(events => events.OnLoad("onLoad"))
        /// %&gt;
        /// </code>
        /// </example>
        public WindowClientEventsBuilder OnLoad(string onLoadHandlerName)
        {
            Guard.IsNotNullOrEmpty(onLoadHandlerName, "onLoadHandlerName");

            clientEvents.OnLoad.HandlerName = onLoadHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnOpen client-side event
        /// </summary>
        /// <param name="javaScript">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Window()
        ///            .Name("Window")
        ///            .ClientEvents(events => events.OnOpen(() =>
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
        public WindowClientEventsBuilder OnOpen(Action onOpenInlineCode)
        {
            Guard.IsNotNull(onOpenInlineCode, "onOpenInlineCode");

            clientEvents.OnOpen.InlineCode = onOpenInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnOpen client-side event.
        /// </summary>
        /// <param name="handlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Window()
        ///             .Name("Window")
        ///             .ClientEvents(events => events.OnOpen("onOpen"))
        /// %&gt;
        /// </code>
        /// </example>
        public WindowClientEventsBuilder OnOpen(string onOpenHandlerName)
        {
            Guard.IsNotNullOrEmpty(onOpenHandlerName, "onOpenHandlerName");

            clientEvents.OnOpen.HandlerName = onOpenHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnClose client-side event
        /// </summary>
        /// <param name="javaScript">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Window()
        ///            .Name("Window")
        ///            .ClientEvents(events => events.OnClose(() =>
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
        public WindowClientEventsBuilder OnClose(Action onCloseInlineCode)
        {
            Guard.IsNotNull(onCloseInlineCode, "onCloseInlineCode");

            clientEvents.OnClose.InlineCode = onCloseInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnClose client-side event.
        /// </summary>
        /// <param name="handlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Window()
        ///             .Name("Window")
        ///             .ClientEvents(events => events.OnClose("onClose"))
        /// %&gt;
        /// </code>
        /// </example>
        public WindowClientEventsBuilder OnClose(string onCloseHandlerName)
        {
            Guard.IsNotNullOrEmpty(onCloseHandlerName, "onCloseHandlerName");

            clientEvents.OnClose.HandlerName = onCloseHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnMove client-side event
        /// </summary>
        /// <param name="javaScript">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Window()
        ///            .Name("Window")
        ///            .ClientEvents(events => events.OnMove(() =>
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
        public WindowClientEventsBuilder OnMove(Action onMoveInlineCode)
        {
            Guard.IsNotNull(onMoveInlineCode, "onMoveInlineCode");

            clientEvents.OnMove.InlineCode = onMoveInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnMove client-side event.
        /// </summary>
        /// <param name="handlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Window()
        ///             .Name("Window")
        ///             .ClientEvents(events => events.OnMove("onMove"))
        /// %&gt;
        /// </code>
        /// </example>
        public WindowClientEventsBuilder OnMove(string onMoveHandlerName)
        {
            Guard.IsNotNullOrEmpty(onMoveHandlerName, "onMoveHandlerName");

            clientEvents.OnMove.HandlerName = onMoveHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnResize client-side event
        /// </summary>
        /// <param name="javaScript">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Window()
        ///            .Name("Window")
        ///            .ClientEvents(events => events.OnResize(() =>
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
        public WindowClientEventsBuilder OnResize(Action onResizeInlineCode)
        {
            Guard.IsNotNull(onResizeInlineCode, "onResizeInlineCode");

            clientEvents.OnResize.InlineCode = onResizeInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnResize client-side event.
        /// </summary>
        /// <param name="handlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Window()
        ///             .Name("Window")
        ///             .ClientEvents(events => events.OnResize("onResize"))
        /// %&gt;
        /// </code>
        /// </example>
        public WindowClientEventsBuilder OnResize(string onResizeHandlerName)
        {
            Guard.IsNotNullOrEmpty(onResizeHandlerName, "onResizeHandlerName");

            clientEvents.OnResize.HandlerName = onResizeHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnRefresh client-side event
        /// </summary>
        /// <param name="javaScript">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Window()
        ///            .Name("Window")
        ///            .ClientEvents(events => events.OnRefresh(() =>
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
        public WindowClientEventsBuilder OnRefresh(Action onRefreshInlineCode)
        {
            Guard.IsNotNull(onRefreshInlineCode, "onRefreshInlineCode");

            clientEvents.OnRefresh.InlineCode = onRefreshInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnRefresh client-side event.
        /// </summary>
        /// <param name="handlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Window()
        ///             .Name("Window")
        ///             .ClientEvents(events => events.OnRefresh("onRefresh"))
        /// %&gt;
        /// </code>
        /// </example>
        public WindowClientEventsBuilder OnRefresh(string onRefreshHandlerName)
        {
            Guard.IsNotNullOrEmpty(onRefreshHandlerName, "onRefreshHandlerName");

            clientEvents.OnRefresh.HandlerName = onRefreshHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnError client-side event
        /// </summary>
        /// <param name="javaScript">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Window()
        ///            .Name("Window")
        ///            .ClientEvents(events => events.OnError(() =>
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
        public WindowClientEventsBuilder OnError(Action onErrorInlineCode)
        {
            Guard.IsNotNull(onErrorInlineCode, "onErrorInlineCode");

            clientEvents.OnError.InlineCode = onErrorInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnError client-side event.
        /// </summary>
        /// <param name="handlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Window()
        ///             .Name("Window")
        ///             .ClientEvents(events => events.OnError("onError"))
        /// %&gt;
        /// </code>
        /// </example>
        public WindowClientEventsBuilder OnError(string onErrorHandlerName)
        {
            Guard.IsNotNullOrEmpty(onErrorHandlerName, "onErrorHandlerName");

            clientEvents.OnError.HandlerName = onErrorHandlerName;

            return this;
        }
    }
}