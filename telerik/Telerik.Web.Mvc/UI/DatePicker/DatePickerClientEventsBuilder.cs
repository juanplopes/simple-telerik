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
    /// Defines the fluent interface for configuring datepicker client events.
    /// </summary>
    public class DatePickerClientEventsBuilder : IHideObjectMembers
    {
        private readonly DatePickerClientEvents clientEvents;
        private readonly ViewContext viewContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatePickerClientEventsBuilder"/> class.
        /// </summary>
        /// <param name="clientEvents">Datepicker client-side events.</param>
        /// <param name="viewContext">The context of the View.</param>
        public DatePickerClientEventsBuilder(DatePickerClientEvents clientEvents, ViewContext viewContext)
        {
            Guard.IsNotNull(clientEvents, "clientEvents");
            Guard.IsNotNull(viewContext, "viewContext");

            this.clientEvents = clientEvents;
            this.viewContext = viewContext;
        }

        /// <summary>
        /// Defines the inline handler of the OnSelect client-side event
        /// </summary>
        /// <param name="onSelectAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().DatePicker()
        ///            .Name("DatePicker")
        ///            .ClientEvents(events => events.OnChange(() =>
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
        public DatePickerClientEventsBuilder OnChange(Action javaScript)
        {
            clientEvents.OnChange = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnSelect client-side event.
        /// </summary>
        /// <param name="onSelectHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DatePicker()
        ///             .Name("DatePicker")
        ///             .ClientEvents(events => events.OnChange("onChange"))
        /// %&gt;
        /// </code>
        /// </example>
        public DatePickerClientEventsBuilder OnChange(string handlerName)
        {
            clientEvents.OnChange = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnLoad client-side event
        /// </summary>
        /// <param name="onLoadAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().DatePicker()
        ///            .Name("DatePicker")
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
        public DatePickerClientEventsBuilder OnLoad(Action javaScript)
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
        ///  &lt;%= Html.Telerik().DatePicker()
        ///             .Name("DatePicker")
        ///             .ClientEvents(events => events.OnLoad("onLoad"))
        /// %&gt;
        /// </code>
        /// </example>
        public DatePickerClientEventsBuilder OnLoad(string handlerName)
        {
            clientEvents.OnLoad = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnPopupOpen client-side event
        /// </summary>
        /// <param name="onLoadAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().DatePicker()
        ///            .Name("DatePicker")
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
        public DatePickerClientEventsBuilder OnOpen(Action javaScript)
        {
            clientEvents.OnOpen = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnPopupOpen client-side event.
        /// </summary>
        /// <param name="onLoadHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DatePicker()
        ///             .Name("DatePicker")
        ///             .ClientEvents(events => events.OnOpen("onOpen"))
        /// %&gt;
        /// </code>
        /// </example>
        public DatePickerClientEventsBuilder OnOpen(string handlerName)
        {
            clientEvents.OnOpen = HandlerAction(handlerName);

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnPopupClose client-side event
        /// </summary>
        /// <param name="onLoadAction">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().DatePicker()
        ///            .Name("DatePicker")
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
        public DatePickerClientEventsBuilder OnClose(Action javaScript)
        {
            clientEvents.OnClose = javaScript;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnPopupClose client-side event.
        /// </summary>
        /// <param name="onLoadHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DatePicker()
        ///             .Name("DatePicker")
        ///             .ClientEvents(events => events.OnClose("onClose"))
        /// %&gt;
        /// </code>
        /// </example>
        public DatePickerClientEventsBuilder OnClose(string handlerName)
        {
            clientEvents.OnClose = HandlerAction(handlerName);

            return this;
        }

        private Action HandlerAction(string handlerName)
        {
            return () => viewContext.HttpContext.Response.Write(handlerName);
        }
    }
}
