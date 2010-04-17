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
    /// Defines the fluent interface for configuring the <see cref="TreeView.ClientEvents"/>.
    /// </summary>
    public class TreeViewClientEventsBuilder : IHideObjectMembers
    {
        private readonly TreeViewClientEvents clientEvents;
        private readonly ViewContext viewContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewClientEventsBuilder"/> class.
        /// </summary>
        /// <param name="clientEvents">The client events.</param>
        /// <param name="viewContext">The view context.</param>
        public TreeViewClientEventsBuilder(TreeViewClientEvents clientEvents, ViewContext viewContext)
        {
            Guard.IsNotNull(clientEvents, "clientEvents");
            Guard.IsNotNull(viewContext, "viewContext");

            this.clientEvents = clientEvents;
            this.viewContext = viewContext;
        }

        /// <summary>
        /// Defines the inline handler of the OnExpand client-side event
        /// </summary>
        /// <param name="onExpandAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnExpand(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     // event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnExpand(Action javaScript)
        {
            clientEvents.OnExpand = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnExpand client-side event.
        /// </summary>
        /// <param name="onExpandHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnExpand("onExpand"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnExpand(string handlerName)
        {
            clientEvents.OnExpand = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnCollapse client-side event
        /// </summary>
        /// <param name="onCollapseAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnCollapse(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     // event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnCollapse(Action javaScript)
        {
            clientEvents.OnCollapse = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnCollapse client-side event.
        /// </summary>
        /// <param name="onCollapseHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnCollapse("onCollapse"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnCollapse(string handlerName)
        {
            clientEvents.OnCollapse = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnSelect client-side event
        /// </summary>
        /// <param name="onSelectAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnSelect(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     // event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnSelect(Action javaScript)
        {
            clientEvents.OnSelect = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnSelect client-side event.
        /// </summary>
        /// <param name="onSelectHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnSelect("onSelect"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnSelect(string handlerName)
        {
            clientEvents.OnSelect = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnLoad client-side event
        /// </summary>
        /// <param name="onLoadAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnLoad(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     // event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnLoad(Action javaScript)
        {
            clientEvents.OnLoad = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnLoad client-side event.
        /// </summary>
        /// <param name="onLoadHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnLoad("onLoad"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnLoad(string handlerName)
        {
            clientEvents.OnLoad = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnError client-side event
        /// </summary>
        /// <param name="onErrorAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnError(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     // event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnError(Action javaScript)
        {
            clientEvents.OnError = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnError client-side event.
        /// </summary>
        /// <param name="onErrorHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnError("onError"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnError(string handlerName)
        {
            clientEvents.OnError = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnNodeDragStart client-side event
        /// </summary>
        /// <param name="onNodeDragAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnNodeDragStart(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     // event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnNodeDragStart(Action javaScript)
        {
            clientEvents.OnNodeDragStart = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnNodeDragStart client-side event.
        /// </summary>
        /// <param name="onNodeDragHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnNodeDragStart("onNodeDragStrat"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnNodeDragStart(string handlerName)
        {
            clientEvents.OnNodeDragStart = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnNodeDrop client-side event
        /// </summary>
        /// <param name="onNodeDropAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnNodeDrop(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     // event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnNodeDrop(Action javaScript)
        {
            clientEvents.OnNodeDrop = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnNodeDrop client-side event.
        /// </summary>
        /// <param name="onNodeDropHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnNodeDrop("OnNodeDrop"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnNodeDrop(string handlerName)
        {
            clientEvents.OnNodeDrop = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnNodeDropped client-side event
        /// </summary>
        /// <param name="onNodeDroppedAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnNodeDropped(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     // event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnNodeDropped(Action javaScript)
        {
            clientEvents.OnNodeDropped = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnNodeDropped client-side event.
        /// </summary>
        /// <param name="onNodeDroppedHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnNodeDropped("OnNodeDropped"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnNodeDropped(string handlerName)
        {
            clientEvents.OnNodeDropped = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnNodeDragCancelled client-side event
        /// </summary>
        /// <param name="onNodeDragCancelledAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnNodeDragCancelled(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     // event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnNodeDragCancelled(Action javaScript)
        {
            clientEvents.OnNodeDragCancelled = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnNodeDragCancelled client-side event.
        /// </summary>
        /// <param name="onNodeDragCancelledHandlerAction">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnNodeDragCancelled("OnNodeDragCancelled"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnNodeDragCancelled(string handlerName)
        {
            clientEvents.OnNodeDragCancelled = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnNodeDragging client-side event
        /// </summary>
        /// <param name="onNodeDragging">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnNodeDragging(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     // event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnNodeDragging(Action javaScript)
        {
            clientEvents.OnNodeDragging = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnNodeDragging client-side event.
        /// </summary>
        /// <param name="onNodeDragging">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnNodeDragging("OnNodeDragging"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnNodeDragging(string handlerName)
        {
            clientEvents.OnNodeDragging = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnDataBinding client-side event
        /// </summary>
        /// <param name="onDataBindingAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnDataBinding(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     // event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnDataBinding(Action javaScript)
        {
            clientEvents.OnDataBinding = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnDataBinding client-side event.
        /// </summary>
        /// <param name="onDataBindingHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnDataBinding("OnDataBinding"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnDataBinding(string handlerName)
        {
            clientEvents.OnDataBinding = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnDataBound client-side event
        /// </summary>
        /// <param name="onDataBoundAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnDataBound(() =>
        ///            {
        ///                 %&gt;
        ///                 function(e) {
        ///                     // event handling code
        ///                 }
        ///                 &lt;%
        ///            }))
        ///            .Render();
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnDataBound(Action javaScript)
        {
            clientEvents.OnDataBound = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnDataBound client-side event.
        /// </summary>
        /// <param name="onDataBoundHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnDataBound("OnDataBound"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnDataBound(string handlerName)
        {
            clientEvents.OnDataBound = HandlerAction(handlerName);

            return this;
        }

        private Action HandlerAction(string handlerName)
        {
            return () => viewContext.HttpContext.Response.Write(handlerName);
        }
    }
}