// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.Mvc;

    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="Menu.ClientEvents"/>.
    /// </summary>
    public class MenuClientEventsBuilder : IHideObjectMembers
    {
        private readonly MenuClientEvents clientEvents;
        private readonly ViewContext viewContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuClientEventsBuilder"/> class.
        /// </summary>
        /// <param name="clientEvents">The client events.</param>
        /// <param name="viewContext">The view context.</param>
        public MenuClientEventsBuilder(MenuClientEvents clientEvents, ViewContext viewContext)
        {
            Guard.IsNotNull(clientEvents, "clientEvents");
            Guard.IsNotNull(viewContext, "viewContext");

            this.clientEvents = clientEvents;
            this.viewContext = viewContext;
        }

        /// <summary>
        /// Defines the inline handler of the OnOpen client-side event
        /// </summary>
        /// <param name="onOpenAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Menu()
        ///            .Name("Menu")
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
        public virtual MenuClientEventsBuilder OnOpen(Action onOpenAction)
        {
            Guard.IsNotNull(onOpenAction, "onOpenAction");

            clientEvents.OnOpen = onOpenAction;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnOpen client-side event.
        /// </summary>
        /// <param name="onOpenHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .ClientEvents(events => events.OnOpen("onOpen"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual MenuClientEventsBuilder OnOpen(string onOpenHandlerName)
        {
            Guard.IsNotNullOrEmpty(onOpenHandlerName, "onOpenHandlerName");

            clientEvents.OnOpen = HandlerAction(onOpenHandlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnClose client-side event
        /// </summary>
        /// <param name="onCloseAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Menu()
        ///            .Name("Menu")
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
        public virtual MenuClientEventsBuilder OnClose(Action onCloseAction)
        {
            Guard.IsNotNull(onCloseAction, "onCloseAction");

            clientEvents.OnClose = onCloseAction;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnClose client-side event.
        /// </summary>
        /// <param name="onCloseHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .ClientEvents(events => events.OnClose("onClose"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual MenuClientEventsBuilder OnClose(string onCloseHandlerName)
        {
            Guard.IsNotNullOrEmpty(onCloseHandlerName, "onCloseHandlerName");

            clientEvents.OnClose = HandlerAction(onCloseHandlerName);

            return this;
        }
        
        /// <summary>
        /// Defines the inline handler of the OnSelect client-side event
        /// </summary>
        /// <param name="onSelectAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Menu()
        ///            .Name("Menu")
        ///            .ClientEvents(events => events.OnSelect(() =>
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
        public virtual MenuClientEventsBuilder OnSelect(Action onSelectAction)
        {
            Guard.IsNotNull(onSelectAction, "onSelectAction");

            clientEvents.OnSelect = onSelectAction;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnSelect client-side event.
        /// </summary>
        /// <param name="onSelectHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .ClientEvents(events => events.OnSelect("onSelect"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual MenuClientEventsBuilder OnSelect(string onSelectHandlerName)
        {
            Guard.IsNotNullOrEmpty(onSelectHandlerName, "onSelectHandlerName");

            clientEvents.OnSelect = HandlerAction(onSelectHandlerName);

            return this;
        }
        
        /// <summary>
        /// Defines the inline handler of the OnLoad client-side event
        /// </summary>
        /// <param name="onSelectAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().Menu()
        ///            .Name("Menu")
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
        public virtual MenuClientEventsBuilder OnLoad(Action onLoadAction)
        {
            Guard.IsNotNull(onLoadAction, "onLoadAction");

            clientEvents.OnLoad = onLoadAction;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnSelect client-side event.
        /// </summary>
        /// <param name="onLoadHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .ClientEvents(events => events.OnLoad("onLoad"))
        /// %&gt;
        /// </code>
        /// </example>
        public virtual MenuClientEventsBuilder OnLoad(string onLoadHandlerName)
        {
            Guard.IsNotNullOrEmpty(onLoadHandlerName, "onLoadHandlerName");

            clientEvents.OnLoad = HandlerAction(onLoadHandlerName);

            return this;
        }

        private Action HandlerAction(string handlerName)
        {
            return () => viewContext.HttpContext.Response.Write(handlerName);
        }

    }
}