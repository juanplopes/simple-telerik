namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using Extensions;
    using Infrastructure;


    public class DropDownBuilderBase<TDropDown, TDropDownBuilder> : ViewComponentBuilderBase<TDropDown, TDropDownBuilder>, IHideObjectMembers
        where TDropDown : ViewComponentBase, IDropDown
        where TDropDownBuilder : ViewComponentBuilderBase<TDropDown, TDropDownBuilder>
    {
        public DropDownBuilderBase(TDropDown component)
            : base(component)
        {
        }

        /// <summary>
        /// Configures the client-side events.
        /// </summary>
        /// <param name="clientEventsAction">The client events action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DropDownList()
        ///             .Name("DropDownList")
        ///             .ClientEvents(events =>
        ///                 events.OnLoad("onLoad")
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public TDropDownBuilder ClientEvents(Action<DropDownClientEventsBuilder> clientEventsAction)
        {
            Guard.IsNotNull(clientEventsAction, "clientEventsAction");

            clientEventsAction(new DropDownClientEventsBuilder(Component.ClientEvents, Component.ViewContext));

            return this as TDropDownBuilder;
        }

        /// <summary>
        /// Configures the effects of the dropdownlist.
        /// </summary>
        /// <param name="effectsAction">The action which configures the effects.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().DropDownList()
        ///	           .Name("DropDownList")
        ///	           .Effects(fx =>
        ///	           {
        ///		            fx.Slide()
        ///					  .OpenDuration(AnimationDuration.Normal)
        ///					  .CloseDuration(AnimationDuration.Normal);
        ///	           })
        /// </code>
        /// </example>
        public TDropDownBuilder Effects(Action<EffectsBuilder> addEffects)
        {
            Guard.IsNotNull(addEffects, "addAction");

            EffectsBuilderFactory factory = new EffectsBuilderFactory();

            addEffects(factory.Create(Component.Effects));

            return this as TDropDownBuilder;
        }

        /// <summary>
        /// Defines the items in the DropDownList
        /// </summary>
        /// <param name="addAction">The add action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DropDownList()
        ///             .Name("DropDownList")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First Item");
        ///                 items.Add().Text("Second Item");
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public TDropDownBuilder Items(Action<DropDownItemFactory> addAction)
        {
            Guard.IsNotNull(addAction, "addAction");

            Component.Items.Clear();

            DropDownItemFactory factory = new DropDownItemFactory(Component.Items);

            addAction(factory);

            return this as TDropDownBuilder;
        }

        /// <summary>
        /// Binds the DropDownList to a list of DropDownItem.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DropDownList()
        ///             .Name("DropDownList")
        ///             .BindTo(new List<DropDownItem>
        ///             {
        ///                 new DropDownItem{
        ///                     Text = "Text1",
        ///                     Value = "Value1"
        ///                 },
        ///                 new DropDownItem{
        ///                     Text = "Text2",
        ///                     Value = "Value2"
        ///                 }
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public TDropDownBuilder BindTo(IEnumerable<DropDownItem> dataSource)
        {
            Guard.IsNotNull(dataSource, "dataSource");

            Component.Items.Clear();

            foreach (DropDownItem item in dataSource) 
            {
                Component.Items.Add(item);
            }

            return this as TDropDownBuilder;
        }

        /// <summary>
        /// Binds the DropDownList to a list of SelectListItem.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DropDownList()
        ///             .Name("DropDownList")
        ///             .BindTo(new List<SelectListItem>
        ///             {
        ///                 new SelectListItem{
        ///                     Text = "Text1",
        ///                     Value = "Value1"
        ///                 },
        ///                 new SelectListItem{
        ///                     Text = "Text2",
        ///                     Value = "Value2"
        ///                 }
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        public TDropDownBuilder BindTo(IEnumerable<SelectListItem> dataSource)
        {
            Guard.IsNotNull(dataSource, "dataSource");

            Component.Items.Clear();

            foreach (SelectListItem item in dataSource) 
            {
                Component.Items.Add(new DropDownItem { 
                    Text = item.Text,
                    Value = item.Value,
                    Selected = item.Selected
                });
            }

            return this as TDropDownBuilder;
        }

        public TDropDownBuilder DropDownHtmlAttributes(object attributes) 
        {
            Guard.IsNotNull(attributes, "attributes");

            Component.DropDownHtmlAttributes.Clear();
            Component.DropDownHtmlAttributes.Merge(attributes);

            return this as TDropDownBuilder;
        }
    }
}
