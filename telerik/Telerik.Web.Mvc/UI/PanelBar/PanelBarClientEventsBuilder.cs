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
    /// Defines the fluent interface for configuring the <see cref="PanelBar.ClientEvents"/>.
    /// </summary>
    public class PanelBarClientEventsBuilder
    {
        private readonly PanelBarClientEvents clientEvents;
        private readonly ViewContext viewContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelBarClientEventsBuilder"/> class.
        /// </summary>
        /// <param name="clientEvents">The client events.</param>
        /// <param name="viewContext">The view context.</param>
        public PanelBarClientEventsBuilder(PanelBarClientEvents clientEvents, ViewContext viewContext)
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
        ///  &lt;% Html.Telerik().PanelBar()
        ///            .Name("PanelBar")
        ///            .ClientEvents(events => events.OnExpand(() =>
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
        public PanelBarClientEventsBuilder OnExpand(Action javaScript)
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
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .ClientEvents(events => events.OnExpand("onExpand"))
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarClientEventsBuilder OnExpand(string handlerName)
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
        ///  &lt;% Html.Telerik().PanelBar()
        ///            .Name("PanelBar")
        ///            .ClientEvents(events => events.OnCollapse(() =>
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
        public PanelBarClientEventsBuilder OnCollapse(Action javaScript)
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
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .ClientEvents(events => events.OnCollapse("onCollapse"))
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarClientEventsBuilder OnCollapse(string handlerName)
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
        ///  &lt;% Html.Telerik().PanelBar()
        ///            .Name("PanelBar")
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
        public PanelBarClientEventsBuilder OnSelect(Action javaScript)
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
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .ClientEvents(events => events.OnSelect("onSelect"))
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarClientEventsBuilder OnSelect(string handlerName)
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
        ///  &lt;% Html.Telerik().PanelBar()
        ///            .Name("PanelBar")
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
        public PanelBarClientEventsBuilder OnLoad(Action javaScript)
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
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .ClientEvents(events => events.OnLoad("onLoad"))
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarClientEventsBuilder OnLoad(string handlerName)
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
        ///  &lt;% Html.Telerik().PanelBar()
        ///            .Name("PanelBar")
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
        public PanelBarClientEventsBuilder OnError(Action javaScript)
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
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .ClientEvents(events => events.OnError("onError"))
        /// %&gt;
        /// </code>
        /// </example>
        public PanelBarClientEventsBuilder OnError(string handlerName)
        {
            clientEvents.OnError = HandlerAction(handlerName);

            return this;
        }

        private Action HandlerAction(string handlerName)
        {
            return () => viewContext.HttpContext.Response.Write(handlerName);
        }
    }
}