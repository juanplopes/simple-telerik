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
        public TreeViewClientEventsBuilder OnExpand(Action onExpandInlineCode)
        {
            Guard.IsNotNull(onExpandInlineCode, "onExpandInlineCode");

            clientEvents.OnExpand.InlineCode = onExpandInlineCode;

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
        public TreeViewClientEventsBuilder OnExpand(string onExpandHandlerName)
        {
            Guard.IsNotNullOrEmpty(onExpandHandlerName, "onExpandHandlerName");

            clientEvents.OnExpand.HandlerName = onExpandHandlerName;

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
        public TreeViewClientEventsBuilder OnCollapse(Action onCollapseInlineCode)
        {
            Guard.IsNotNull(onCollapseInlineCode, "onCollapseInlineCode");

            clientEvents.OnCollapse.InlineCode = onCollapseInlineCode;

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
        public TreeViewClientEventsBuilder OnCollapse(string onCollapseHandlerName)
        {
            Guard.IsNotNullOrEmpty(onCollapseHandlerName, "onCollapseHandlerName");

            clientEvents.OnCollapse.HandlerName = onCollapseHandlerName;

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
        public TreeViewClientEventsBuilder OnSelect(Action onSelectInlineCode)
        {
            Guard.IsNotNull(onSelectInlineCode, "onSelectInlineCode");

            clientEvents.OnSelect.InlineCode = onSelectInlineCode;

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
        public TreeViewClientEventsBuilder OnSelect(string onSelectHandlerName)
        {
            Guard.IsNotNullOrEmpty(onSelectHandlerName, "onSelectHandlerName");

            clientEvents.OnSelect.HandlerName = onSelectHandlerName;

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
        public TreeViewClientEventsBuilder OnLoad(Action onLoadInlineCode)
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
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnLoad("onLoad"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnLoad(string onLoadHandlerName)
        {
            Guard.IsNotNullOrEmpty(onLoadHandlerName, "onLoadHandlerName");

            clientEvents.OnLoad.HandlerName = onLoadHandlerName;

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
        public TreeViewClientEventsBuilder OnError(Action onErrorInlineCode)
        {
            Guard.IsNotNull(onErrorInlineCode, "onErrorInlineCode");

            clientEvents.OnError.InlineCode = onErrorInlineCode;

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
        public TreeViewClientEventsBuilder OnError(string onErrorHandlerName)
        {
            Guard.IsNotNullOrEmpty(onErrorHandlerName, "onErrorHandlerName");

            clientEvents.OnError.HandlerName = onErrorHandlerName;

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
        public TreeViewClientEventsBuilder OnNodeDragStart(Action onNodeDragStartInlineCode)
        {
            Guard.IsNotNull(onNodeDragStartInlineCode, "onNodeDragStartInlineCode");

            clientEvents.OnNodeDragStart.InlineCode = onNodeDragStartInlineCode;

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
        public TreeViewClientEventsBuilder OnNodeDragStart(string onNodeDragStartHandlerName)
        {
            Guard.IsNotNullOrEmpty(onNodeDragStartHandlerName, "onNodeDragStartHandlerName");

            clientEvents.OnNodeDragStart.HandlerName = onNodeDragStartHandlerName;

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
        public TreeViewClientEventsBuilder OnNodeDrop(Action onNodeDropInlineCode)
        {
            Guard.IsNotNull(onNodeDropInlineCode, "onNodeDropInlineCode");

            clientEvents.OnNodeDrop.InlineCode = onNodeDropInlineCode;

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
        public TreeViewClientEventsBuilder OnNodeDrop(string onNodeDropHandlerName)
        {
            Guard.IsNotNullOrEmpty(onNodeDropHandlerName, "onNodeDropHandlerName");

            clientEvents.OnNodeDrop.HandlerName = onNodeDropHandlerName;

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
        public TreeViewClientEventsBuilder OnNodeDropped(Action onNodeDroppedInlineCode)
        {
            Guard.IsNotNull(onNodeDroppedInlineCode, "onNodeDroppedInlineCode");

            clientEvents.OnNodeDropped.InlineCode = onNodeDroppedInlineCode;

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
        public TreeViewClientEventsBuilder OnNodeDropped(string onNodeDroppedHandlerName)
        {
            Guard.IsNotNullOrEmpty(onNodeDroppedHandlerName, "onNodeDroppedHandlerName");

            clientEvents.OnNodeDropped.HandlerName = onNodeDroppedHandlerName;

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
        public TreeViewClientEventsBuilder OnNodeDragCancelled(Action onNodeDragCancelledInlineCode)
        {
            Guard.IsNotNull(onNodeDragCancelledInlineCode, "onNodeDragCancelledInlineCode");

            clientEvents.OnNodeDragCancelled.InlineCode = onNodeDragCancelledInlineCode;

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
        public TreeViewClientEventsBuilder OnNodeDragCancelled(string onNodeDragCancelledHandlerName)
        {
            Guard.IsNotNullOrEmpty(onNodeDragCancelledHandlerName, "onNodeDragCancelledHandlerName");

            clientEvents.OnNodeDragCancelled.HandlerName = onNodeDragCancelledHandlerName;

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
        public TreeViewClientEventsBuilder OnNodeDragging(Action onNodeDraggingInlineCode)
        {
            Guard.IsNotNull(onNodeDraggingInlineCode, "onNodeDraggingInlineCode");

            clientEvents.OnNodeDragging.InlineCode = onNodeDraggingInlineCode;

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
        public TreeViewClientEventsBuilder OnNodeDragging(string onNodeDraggingHandlerName)
        {
            Guard.IsNotNullOrEmpty(onNodeDraggingHandlerName, "onNodeDraggingHandlerName");

            clientEvents.OnNodeDragging.HandlerName = onNodeDraggingHandlerName;

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
        public TreeViewClientEventsBuilder OnDataBinding(Action onDataBindingInlineCode)
        {
            Guard.IsNotNull(onDataBindingInlineCode, "onDataBindingInlineCode");

            clientEvents.OnDataBinding.InlineCode = onDataBindingInlineCode;

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
        public TreeViewClientEventsBuilder OnDataBinding(string OnDataBindingHandlerName)
        {
            Guard.IsNotNullOrEmpty(OnDataBindingHandlerName, "OnDataBindingHandlerName");

            clientEvents.OnDataBinding.HandlerName = OnDataBindingHandlerName;

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
        public TreeViewClientEventsBuilder OnDataBound(Action onDataBoundInlineCode)
        {
            Guard.IsNotNull(onDataBoundInlineCode, "onDataBoundInlineCode");

            clientEvents.OnDataBound.InlineCode = onDataBoundInlineCode;

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
        public TreeViewClientEventsBuilder OnDataBound(string onDataBoundHandlerName)
        {
            Guard.IsNotNullOrEmpty(onDataBoundHandlerName, "onDataBoundHandlerName");

            clientEvents.OnDataBound.HandlerName = onDataBoundHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnChecked client-side event. Requires ShowCheckBox to be true.
        /// </summary>
        /// <param name="onDataBoundAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().TreeView()
        ///            .Name("TreeView")
        ///            .ClientEvents(events => events.OnChecked(() =>
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
        public TreeViewClientEventsBuilder OnChecked(Action onCheckedInlineCode)
        {
            Guard.IsNotNull(onCheckedInlineCode, "onCheckedInlineCode");

            clientEvents.OnChecked.InlineCode = onCheckedInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnChecked client-side event. Requires ShowCheckBox to be true.
        /// </summary>
        /// <param name="onCheckedHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .ClientEvents(events => events.OnChecked("onChecked"))
        /// %&gt;
        /// </code>
        /// </example>
        public TreeViewClientEventsBuilder OnChecked(string onCheckedHandlerName)
        {
            Guard.IsNotNullOrEmpty(onCheckedHandlerName, "onCheckedHandlerName");

            clientEvents.OnChecked.HandlerName = onCheckedHandlerName;

            return this;
        }
    }
}