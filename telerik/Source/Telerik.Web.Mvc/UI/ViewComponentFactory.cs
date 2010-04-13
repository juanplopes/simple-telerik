// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Web.Mvc;

    using Extensions;
    using Infrastructure;

    using Fluent;
    /// <summary>
    /// Provides the factory methods for creating Telerik View Components.
    /// </summary>
    public class ViewComponentFactory : IHideObjectMembers
    {
        private readonly StyleSheetRegistrarBuilder styleSheetRegistrarBuilder;
        private readonly ScriptRegistrarBuilder scriptRegistrarBuilder;

        [DebuggerStepThrough]
        public ViewComponentFactory(HtmlHelper htmlHelper, IClientSideObjectWriterFactory clientSideObjectWriterFactory, StyleSheetRegistrarBuilder styleSheetRegistrar, ScriptRegistrarBuilder scriptRegistrar)
        {
            Guard.IsNotNull(htmlHelper, "htmlHelper");
            Guard.IsNotNull(clientSideObjectWriterFactory, "clientSideObjectWriterFactory");
            Guard.IsNotNull(styleSheetRegistrar, "styleSheetRegistrar");
            Guard.IsNotNull(scriptRegistrar, "scriptRegistrar");

            HtmlHelper = htmlHelper;
            ClientSideObjectWriterFactory = clientSideObjectWriterFactory;

            styleSheetRegistrarBuilder = styleSheetRegistrar;
            scriptRegistrarBuilder = scriptRegistrar;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public HtmlHelper HtmlHelper
        {
            get;
            private set;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IClientSideObjectWriterFactory ClientSideObjectWriterFactory
        {
            get;
            private set;
        }

        private ViewContext ViewContext
        {
            [DebuggerStepThrough]
            get
            {
                return HtmlHelper.ViewContext;
            }
        }

        /// <summary>
        /// Creates a <see cref="StyleSheetRegistrar"/>
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().StyleSheetRegistrar()
        ///             .DefaultGroup(group => group
        ///                   group.Add("Site.css")
        ///                        .Add("telerik.common.css")
        ///                        .Add("telerik.vista.css")
        ///                        .Compressed(true)
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public StyleSheetRegistrarBuilder StyleSheetRegistrar()
        {
            return styleSheetRegistrarBuilder;
        }

        /// <summary>
        /// Creates a <see cref="ScriptRegistrar"/>
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().ScriptRegistrar()
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public ScriptRegistrarBuilder ScriptRegistrar()
        {
            return scriptRegistrarBuilder;
        }

        /// <summary>
        /// Creates a <see cref="Menu"/>
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Menu()
        ///             .Name("Menu")
        ///             .Items(items => { /* add items here */ });
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual MenuBuilder Menu()
        {
            return new MenuBuilder(Create(() => new Menu(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<INavigationItemAuthorization>(), ServiceLocator.Current.Resolve<IMenuHtmlBuilderFactory>())));
        }

        /// <summary>
        /// Creates a new <see cref="Grid&lt;T&gt;"/> bound to the specified data item type.
        /// </summary>
        /// <example>
        /// <typeparam name="T">The type of the data item</typeparam>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid&lt;Order&gt;()
        ///             .Name("Grid")
        ///             .BindTo(Model)
        /// %&gt;
        /// </code>
        /// </example>
        /// <remarks>
        /// Do not forget to bind the grid using the <see cref="GridBuilder{T}.BindTo(System.String)" /> method when using this overload.
        /// </remarks>
        [DebuggerStepThrough]
        public virtual GridBuilder<T> Grid<T>() where T : class
        {
            return new GridBuilder<T>(Create(() => new Grid<T>(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<IGridHtmlBuilderFactory>())));
        }

        /// <summary>
        /// Creates a new <see cref="Telerik.Web.UI.Grid&lt;T&gt;"/> bound to the specified data source.
        /// </summary>
        /// <typeparam name="T">The type of the data item</typeparam>
        /// <param name="dataSource">The data source.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid(Model)
        ///             .Name("Grid")
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Grid<T>(IEnumerable<T> dataSource) where T : class
        {
            GridBuilder<T> builder = Grid<T>();

            builder.Component.DataSource = dataSource;

            return builder;
        }

        /// <summary>
        /// Creates a new <see cref="Telerik.Web.UI.Grid&lt;T&gt;"/> bound an item in ViewData.
        /// </summary>
        /// <typeparam name="T">Type of the data item</typeparam>
        /// <param name="dataSourceViewDataKey">The data source view data key.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Grid&lt;Order&gt;("orders")
        ///             .Name("Grid")
        /// %&gt;
        /// </code>
        /// </example>
        public virtual GridBuilder<T> Grid<T>(string dataSourceViewDataKey) where T : class
        {
            GridBuilder<T> builder = Grid<T>();

            builder.Component.DataSource = ViewContext.ViewData.Eval(dataSourceViewDataKey) as IEnumerable<T>;

            return builder;
        }

        /// <summary>
        /// Creates a new <see cref="TabStrip"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TabStrip()
        ///             .Name("TabStrip")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First");
        ///                 items.Add().Text("Second");
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual TabStripBuilder TabStrip()
        {
            return new TabStripBuilder(Create(() => new TabStrip(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<INavigationItemAuthorization>(), ServiceLocator.Current.Resolve<ITabStripHtmlBuilderFactory>())));
        }

        /// <summary>
        /// Creates a new <see cref="DatePicker"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DatePicker()
        ///             .Name("DatePicker")
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual DatePickerBuilder DatePicker()
        {
            return new DatePickerBuilder(Create(() => new DatePicker(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IDatePickerHtmlBuilderFactory>())));
        }

        /// <summary>
        /// Creates a new <see cref="Calendar"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Calendar()
        ///             .Name("Calendar")
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual CalendarBuilder Calendar()
        {
            return new CalendarBuilder(Create(() => new Calendar(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<ICalendarHtmlBuilderFactory>())));
        }
        
        /// <summary>
        /// Creates a new <see cref="PanelBar"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PanelBar()
        ///             .Name("PanelBar")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First");
        ///                 items.Add().Text("Second");
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual PanelBarBuilder PanelBar()
        {
            return new PanelBarBuilder(Create(() => new PanelBar(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<INavigationItemAuthorization>(), ServiceLocator.Current.Resolve<IPanelBarHtmlBuilderFactory>())));
        }

        /// <summary>
        /// Creates a <see cref="TreeView"/>
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().TreeView()
        ///             .Name("TreeView")
        ///             .Items(items => { /* add items here */ });
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual TreeViewBuilder TreeView()
        {
            return new TreeViewBuilder(Create(() => new TreeView(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<INavigationItemAuthorization>(), ServiceLocator.Current.Resolve<ITreeViewHtmlBuilderFactory>())));
        }

        /// <summary>
        /// Creates a new <see cref="NumericTextBox{T}"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().NumericTextBox()
        ///             .Name("NumericTextBox")
        /// %&gt;
        /// </code>
        /// </example>
        /// <returns>Returns <see cref="NumericTextBoxBuilder{double}"/>.</returns>
        [DebuggerStepThrough]
        public virtual NumericTextBoxBuilder<double> NumericTextBox()
        {
            return new NumericTextBoxBuilder<double>(Create(() => new NumericTextBox<double>(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<double>>())));
        }

        /// <summary>
        /// Creates a new <see cref="NumericTextBox{T}"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().NumericTextBox<double>()
        ///             .Name("NumericTextBox")
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual NumericTextBoxBuilder<T> NumericTextBox<T>() where T: struct
        {
            return new NumericTextBoxBuilder<T>(Create(() => new NumericTextBox<T>(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<T>>())));
        }

        /// <summary>
        /// Creates a new <see cref="CurrencyTextBox"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().CurrencyTextBox()
        ///             .Name("CurrencyTextBox")
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual CurrencyTextBoxBuilder CurrencyTextBox()
        {
            return new CurrencyTextBoxBuilder(Create(() => new CurrencyTextBox(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<decimal>>())));
        }

        /// <summary>
        /// Creates a new <see cref="PercentTextBox"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PercentTextBox()
        ///             .Name("PercentTextBox")
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual PercentTextBoxBuilder PercentTextBox()
        {
            return new PercentTextBoxBuilder(Create(() => new PercentTextBox(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<double>>())));
        }

        /// <summary>
        /// Creates a new <see cref="IntegerTextBox"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().IntegerTextBox()
        ///             .Name("IntegerTextBox")
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual IntegerTextBoxBuilder IntegerTextBox()
        {
            return new IntegerTextBoxBuilder(Create(() => new IntegerTextBox(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<int>>())));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public TViewComponent Create<TViewComponent>(Func<TViewComponent> factory) where TViewComponent : ViewComponentBase
        {
            TViewComponent component = factory();

            scriptRegistrarBuilder.ToRegistrar().Register(component);

            return component;
        }
    }
}