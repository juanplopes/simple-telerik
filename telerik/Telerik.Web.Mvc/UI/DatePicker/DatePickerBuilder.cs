// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;

    using Extensions;
    using Infrastructure;
    using Telerik.Web.Mvc.Resources;

    /// <summary>
    /// Defines the fluent interface for configuring the <see cref="DatePicker"/> component.
    /// </summary>
    public class DatePickerBuilder : ViewComponentBuilderBase<DatePicker, DatePickerBuilder>, IHideObjectMembers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PanelBarBuilder"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public DatePickerBuilder(DatePicker component)
            : base(component)
        {
        }

        /// <summary>
        /// Sets the theme of the datepicker
        /// </summary>
        public DatePickerBuilder Theme(string name)
        {
            Guard.IsNotNullOrEmpty(name, "name");

            Component.Theme = name;

            return this;
        }

        /// <summary>
        /// Configures the effects of the datepicker.
        /// </summary>
        /// <param name="effectsAction">The action which configures the effects.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;%= Html.Telerik().DatePicker()
        ///	           .Name("DatePicker")
        ///	           .Effects(fx =>
        ///	           {
        ///		            fx.Height()
        ///			          .Opacity()
        ///					  .OpenDuration(AnimationDuration.Normal)
        ///					  .CloseDuration(AnimationDuration.Normal);
        ///	           })
        /// </code>
        /// </example>
        public DatePickerBuilder Effects(Action<EffectsBuilder> addEffects)
        {
            Guard.IsNotNull(addEffects, "addAction");

            EffectsBuilderFactory factory = new EffectsBuilderFactory();

            addEffects(factory.Create(Component.Effects));

            return this;
        }

        /// <summary>
        /// Sets whether datepicker to be rendered with button, which shows calendar on click
        /// </summary>
        public DatePickerBuilder ShowButton(bool showButton) 
        {
            Guard.IsNotNull(showButton, "showButton");

            Component.EnableButton = showButton;

            return this;
        }

        /// <summary>
        /// Sets the title of the datepicker button.
        /// </summary>
        public DatePickerBuilder ButtonTitle(string title) 
        {
            Guard.IsNotNullOrEmpty(title, "title");

            Component.ButtonTitle = title;

            return this;
        }

        /// <summary>
        /// Sets the date format, which will be used to parse and format the machine date.
        /// </summary>
        public DatePickerBuilder Format(string format) 
        {
            Guard.IsNotNullOrEmpty(format, "format");

            Component.Format = format;

            return this;
        }

        /// <summary>
        /// Sets the value of the datepicker input
        /// </summary>
        public DatePickerBuilder Value(DateTime? date)
        {
            Component.Value = date;

            return this;
        }

        /// <summary>
        /// Sets the value of the datepicker input
        /// </summary>
        public DatePickerBuilder Value(string date)
        {
            DateTime parsedDate;

            if (DateTime.TryParse(date, out parsedDate))
            {
                Component.Value = parsedDate;
            }
            else 
            {
                Component.Value = null;
            }
            return this;
        }

        /// <summary>
        /// Sets the minimal date, which can be selected in DatePicker.
        /// </summary>
        public DatePickerBuilder MinDate(DateTime date)
        {
            Guard.IsNotNull(date, "date");

            Component.MinDate = date;

            return this;
        }

        /// <summary>
        /// Sets the minimal date, which can be selected in DatePicker.
        /// </summary>
        public DatePickerBuilder MinDate(string date)
        {
            Guard.IsNotNullOrEmpty(date, "date");

            DateTime parsedDate;

            if (DateTime.TryParse(date, out parsedDate))
            {
                Component.MinDate = parsedDate;
            }
            else
            {
                throw new ArgumentException(TextResource.StringNotCorrectDate);
            }
            return this;
        }

        /// <summary>
        /// Sets the maximal date, which can be selected in DatePicker.
        /// </summary>
        public DatePickerBuilder MaxDate(DateTime date)
        {
            Guard.IsNotNull(date, "date");

            Component.MaxDate = date;

            return this;
        }

        /// <summary>
        /// Sets the maximal date, which can be selected in DatePicker.
        /// </summary>
        public DatePickerBuilder MaxDate(string date)
        {
            Guard.IsNotNullOrEmpty(date, "date");

            DateTime parsedDate;

            if (DateTime.TryParse(date, out parsedDate))
            {
                Component.MaxDate = parsedDate;
            }
            else
            {
                throw new ArgumentException(TextResource.StringNotCorrectDate);
            }
            return this;
        }

        /// <summary>
        /// Configures the client-side events.
        /// </summary>
        /// <param name="clientEventsAction">The client events action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DatePicker()
        ///             .Name("DatePicker")
        ///             .ClientEvents(events =>
        ///                 events.OnLoad("onLoad").OnSelect("onSelect")
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public DatePickerBuilder ClientEvents(Action<DatePickerClientEventsBuilder> clientEventsAction)
        {
            Guard.IsNotNull(clientEventsAction, "clientEventsAction");

            clientEventsAction(new DatePickerClientEventsBuilder(Component.ClientEvents, Component.ViewContext));

            return this;
        }

        /// <summary>
        /// Sets the Input HTML attributes.
        /// </summary>
        /// <param name="attributes">The HTML attributes.</param>
        public DatePickerBuilder InputHtmlAttributes(object attributes)
        {
            Guard.IsNotNull(attributes, "attributes");
            
            Component.InputHtmlAttributes.Clear();
            Component.InputHtmlAttributes.Merge(attributes);

            return this;
        }
    }
}