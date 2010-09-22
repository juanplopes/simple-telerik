namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.Web.Mvc;
    using Infrastructure;

    public class DropDownClientEventsBuilder
    {
        private readonly DropDownClientEvents clientEvents;
        private readonly ViewContext viewContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownClientEventsBuilder"/> class.
        /// </summary>
        /// <param name="clientEvents">The client events.</param>
        /// <param name="viewContext">The view context.</param>
        public DropDownClientEventsBuilder(DropDownClientEvents clientEvents, ViewContext viewContext)
        {
            Guard.IsNotNull(clientEvents, "clientEvents");
            Guard.IsNotNull(viewContext, "viewContext");

            this.clientEvents = clientEvents;
            this.viewContext = viewContext;
        }

        /// <summary>
        /// Defines the inline handler of the OnLoad client-side event
        /// </summary>
        /// <param name="onLoadInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().DropDownList()
        ///            .Name("DropDownList")
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
        public DropDownClientEventsBuilder OnLoad(Action onLoadInlineCode)
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
        ///  &lt;%= Html.Telerik().DropDownList()
        ///             .Name("DropDownList")
        ///             .ClientEvents(events => events.OnLoad("onLoad"))
        /// %&gt;
        /// </code>
        /// </example>
        public DropDownClientEventsBuilder OnLoad(string onLoadHandlerName)
        {
            Guard.IsNotNullOrEmpty(onLoadHandlerName, "onLoadHandlerName");

            clientEvents.OnLoad.HandlerName = onLoadHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnChange client-side event
        /// </summary>
        /// <param name="onChangeInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().DropDownList()
        ///            .Name("DropDownList")
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
        public DropDownClientEventsBuilder OnChange(Action onChangeInlineCode)
        {
            Guard.IsNotNull(onChangeInlineCode, "onChangeInlineCode");

            clientEvents.OnChange.InlineCode = onChangeInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnChange client-side event.
        /// </summary>
        /// <param name="onChangeHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DropDownList()
        ///             .Name("DropDownList")
        ///             .ClientEvents(events => events.OnChange("onChange"))
        /// %&gt;
        /// </code>
        /// </example>
        public DropDownClientEventsBuilder OnChange(string onChangeHandlerName)
        {
            Guard.IsNotNullOrEmpty(onChangeHandlerName, "onChangeHandlerName");

            clientEvents.OnChange.HandlerName = onChangeHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnOpen client-side event
        /// </summary>
        /// <param name="onOpenInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().DropDownList()
        ///            .Name("DropDownList")
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
        public DropDownClientEventsBuilder OnOpen(Action onOpenInlineCode)
        {
            Guard.IsNotNull(onOpenInlineCode, "onOpenInlineCode");

            clientEvents.OnOpen.InlineCode = onOpenInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnOpen client-side event.
        /// </summary>
        /// <param name="onOpenHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DropDownList()
        ///             .Name("DropDownList")
        ///             .ClientEvents(events => events.OnOpen("onOpen"))
        /// %&gt;
        /// </code>
        /// </example>
        public DropDownClientEventsBuilder OnOpen(string onOpenHandlerName)
        {
            Guard.IsNotNullOrEmpty(onOpenHandlerName, "onOpenHandlerName");

            clientEvents.OnOpen.HandlerName = onOpenHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnClose client-side event
        /// </summary>
        /// <param name="onCloseInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().DropDownList()
        ///            .Name("DropDownList")
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
        public DropDownClientEventsBuilder OnClose(Action onCloseInlineCode)
        {
            Guard.IsNotNull(onCloseInlineCode, "onCloseInlineCode");

            clientEvents.OnClose.InlineCode = onCloseInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnClose client-side event.
        /// </summary>
        /// <param name="onCloseHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DropDownList()
        ///             .Name("DropDownList")
        ///             .ClientEvents(events => events.OnClose("onClose"))
        /// %&gt;
        /// </code>
        /// </example>
        public DropDownClientEventsBuilder OnClose(string onCloseHandlerName)
        {
            Guard.IsNotNullOrEmpty(onCloseHandlerName, "onCloseHandlerName");

            clientEvents.OnClose.HandlerName = onCloseHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnDataBinding client-side event
        /// </summary>
        /// <param name="onDataBindingInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().DropDownList()
        ///            .Name("DropDownList")
        ///            .ClientEvents(events => events.OnDataBinding(() =>
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
        public DropDownClientEventsBuilder OnDataBinding(Action onDataBindingInlineCode)
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
        ///  &lt;%= Html.Telerik().DropDownList()
        ///             .Name("DropDownList")
        ///             .ClientEvents(events => events.OnDataBinding("OnDataBinding"))
        /// %&gt;
        /// </code>
        /// </example>
        public DropDownClientEventsBuilder OnDataBinding(string onDataBindingHandlerName)
        {
            Guard.IsNotNullOrEmpty(onDataBindingHandlerName, "onDataBindingHandlerName");

            clientEvents.OnDataBinding.HandlerName = onDataBindingHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnDataBound client-side event
        /// </summary>
        /// <param name="OnDataBoundInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().DropDownList()
        ///            .Name("DropDownList")
        ///            .ClientEvents(events => events.OnDataBound(() =>
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
        public DropDownClientEventsBuilder OnDataBound(Action onDataBoundInlineCode)
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
        ///  &lt;%= Html.Telerik().DropDownList()
        ///             .Name("DropDownList")
        ///             .ClientEvents(events => events.OnDataBound("onDataBound"))
        /// %&gt;
        /// </code>
        /// </example>
        public DropDownClientEventsBuilder OnDataBound(string onDataBoundHandlerName)
        {
            Guard.IsNotNullOrEmpty(onDataBoundHandlerName, "onDataBoundHandlerName");

            clientEvents.OnDataBound.HandlerName = onDataBoundHandlerName;

            return this;
        }

        /// <summary>
        /// Defines the inline handler of the OnError client-side event
        /// </summary>
        /// <param name="onErrorInlineCode">The action defining the inline handler.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;% Html.Telerik().DropDownList()
        ///            .Name("DropDownList")
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
        public DropDownClientEventsBuilder OnError(Action onErrorInlineCode)
        {
            Guard.IsNotNull(onErrorInlineCode, "onErrorInlineCode");

            clientEvents.OnError.InlineCode = onErrorInlineCode;

            return this;
        }

        /// <summary>
        ///  Defines the name of the JavaScript function that will handle the the OnError client-side event.
        /// </summary>
        /// <param name="onDataBoundHandlerName">The name of the JavaScript function that will handle the event.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DropDownList()
        ///             .Name("DropDownList")
        ///             .ClientEvents(events => events.OnError("onError"))
        /// %&gt;
        /// </code>
        /// </example>
        public DropDownClientEventsBuilder OnError(string onErrorHandlerName)
        {
            Guard.IsNotNullOrEmpty(onErrorHandlerName, "onErrorHandlerName");

            clientEvents.OnError.HandlerName = onErrorHandlerName;

            return this;
        }
    }
}
