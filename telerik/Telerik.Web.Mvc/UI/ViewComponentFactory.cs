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
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Extensions;
    using Fluent;
    using Infrastructure;

    /// <summary>
    /// Provides the factory methods for creating Telerik View Components.
    /// </summary>
    public class ViewComponentFactory : IHideObjectMembers
    {
        private readonly StyleSheetRegistrarBuilder styleSheetRegistrarBuilder;
        private readonly ScriptRegistrarBuilder scriptRegistrarBuilder;

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
            set;
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
            return MenuBuilder.Create(Create(() => new Menu(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<INavigationItemAuthorization>(), ServiceLocator.Current.Resolve<INavigationComponentHtmlBuilderFactory<Menu, MenuItem>>())));
        }

        /// <summary>
        /// Creates a <see cref="Editor"/>
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Editor()
        ///             .Name("Editor");
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual EditorBuilder Editor()
        {
            return EditorBuilder.Create(Create(() => new Editor(ViewContext, ClientSideObjectWriterFactory)));
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
            return GridBuilder<T>.Create(Create(() => new Grid<T>(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>())));
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
            return TabStripBuilder.Create(Create(() => new TabStrip(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<INavigationItemAuthorization>(), ServiceLocator.Current.Resolve<ITabStripHtmlBuilderFactory>())));
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
            return DatePickerBuilder.Create(Create(() => new DatePicker(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IDatePickerHtmlBuilderFactory>())));
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
            return CalendarBuilder.Create(Create(() => new Calendar(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<ICalendarHtmlBuilderFactory>())));
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
            return PanelBarBuilder.Create(Create(() => new PanelBar(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<INavigationItemAuthorization>(), ServiceLocator.Current.Resolve<INavigationComponentHtmlBuilderFactory<PanelBar, PanelBarItem>>())));
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
            return TreeViewBuilder.Create(Create(() => new TreeView(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>(), ServiceLocator.Current.Resolve<INavigationItemAuthorization>(), ServiceLocator.Current.Resolve<ITreeViewHtmlBuilderFactory>())));
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
            return NumericTextBoxBuilder<double>.Create(Create(() => new NumericTextBox<double>(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<double>>())));
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
            return NumericTextBoxBuilder<T>.Create(Create(() => new NumericTextBox<T>(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<T>>())));
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
            return CurrencyTextBoxBuilder.Create(Create(() => new CurrencyTextBox(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<decimal>>())));
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
            return PercentTextBoxBuilder.Create(Create(() => new PercentTextBox(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<double>>())));
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
            return IntegerTextBoxBuilder.Create(Create(() => new IntegerTextBox(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<int>>())));
        }

        /// <summary>
        /// Creates a new <see cref="Window"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Window()
        ///             .Name("Window")
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual WindowBuilder Window()
        {
            return WindowBuilder.Create(Create(() => new Window(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IWindowHtmlBuilderFactory>())));
        }

        /// <summary>
        /// Creates a new <see cref="DropDownList"/>.
        /// </summary>
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
        [DebuggerStepThrough]
        public virtual DropDownListBuilder DropDownList()
        {
            return DropDownListBuilder.Create(Create(() => new DropDownList(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>())));
        }

        /// <summary>
        /// Creates a new <see cref="ComboBox"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().ComboBox()
        ///             .Name("ComboBox")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First Item");
        ///                 items.Add().Text("Second Item");
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual ComboBoxBuilder ComboBox()
        {
            return ComboBoxBuilder.Create(Create(() => new ComboBox(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>())));
        }

        /// <summary>
        /// Creates a new <see cref="AutoComplete"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().AutoComplete()
        ///             .Name("AutoComplete")
        ///             .Items(items =>
        ///             {
        ///                 items.Add().Text("First Item");
        ///                 items.Add().Text("Second Item");
        ///             })
        /// %&gt;
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public virtual AutoCompleteBuilder AutoComplete()
        {
            return AutoCompleteBuilder.Create(Create(() => new AutoComplete(ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>())));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public TViewComponent Create<TViewComponent>(Func<TViewComponent> factory) where TViewComponent : ViewComponentBase
        {
            TViewComponent component = factory();

            if (!component.IsSelfInitialized)
            {
                scriptRegistrarBuilder.ToRegistrar().Register(component);
            }

            return component;
        }
    }
#if MVC2
    public class ViewComponentFactory<TModel> : ViewComponentFactory
    {
        public ViewComponentFactory(HtmlHelper<TModel> htmlHelper, IClientSideObjectWriterFactory clientSideObjectWriterFactory, StyleSheetRegistrarBuilder styleSheetRegistrar, ScriptRegistrarBuilder scriptRegistrar)
            : base(htmlHelper, clientSideObjectWriterFactory, styleSheetRegistrar, scriptRegistrar)
        {
            this.HtmlHelper = htmlHelper;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new HtmlHelper<TModel> HtmlHelper
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new <see cref="Editor" /> UI component.
        /// </summary>
        public virtual EditorBuilder EditorFor(Expression<Func<TModel, string>> expression)
        {
            Guard.IsNotNull(expression, "expression");
            return Editor()
                .Name(GetName(expression))
                .Value(expression.Compile()(HtmlHelper.ViewData.Model));
        }
        /// <summary>
        /// Creates a new <see cref="NumericTextBox{TValue}"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().NumericTextBoxFor(m=>m.Property) %&gt;
        /// </code>
        /// </example>
        public virtual NumericTextBoxBuilder<TValue> NumericTextBoxFor<TValue>(Expression<Func<TModel, TValue>> expression)
            where TValue : struct
        {
            Guard.IsNotNull(expression, "expression");

            var validators = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData).GetValidators(HtmlHelper.ViewContext.Controller.ControllerContext);

            NumericTextBoxBuilder<TValue> builder = new NumericTextBoxBuilder<TValue>(Create(() => new NumericTextBox<TValue>(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<TValue>>())));
            return builder.Name(GetName(expression))
                          .Value(expression.Compile()(HtmlHelper.ViewData.Model))
                          .MinValue(GetRangeValidationParameter<TValue>(validators, "minimum"))
                          .MaxValue(GetRangeValidationParameter<TValue>(validators, "maximum"));
        }

        /// <summary>
        /// Creates a new <see cref="NumericTextBox{Nullable{TValue}}"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().NumericTextBoxFor(m=>m.NullableProperty) %&gt;
        /// </code>
        /// </example>
        public virtual NumericTextBoxBuilder<TValue> NumericTextBoxFor<TValue>(Expression<Func<TModel, Nullable<TValue>>> expression)
            where TValue : struct
        {
            Guard.IsNotNull(expression, "expression");

            IEnumerable<ModelValidator> validators = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData).GetValidators(HtmlHelper.ViewContext.Controller.ControllerContext);
                        
            NumericTextBoxBuilder<TValue> builder = new NumericTextBoxBuilder<TValue>(Create(() => new NumericTextBox<TValue>(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<TValue>>())));
            return builder.Name(GetName(expression))
                          .Value(expression.Compile()(HtmlHelper.ViewData.Model))
                          .MinValue(GetRangeValidationParameter<TValue>(validators, "minimum"))
                          .MaxValue(GetRangeValidationParameter<TValue>(validators, "maximum"));
        }

        /// <summary>
        /// Creates a new <see cref="IntegerTextBox{Nullable{int}}"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().IntegerTextBoxFor(m=>m.Property) %&gt;
        /// </code>
        /// </example>
        public virtual IntegerTextBoxBuilder IntegerTextBoxFor(Expression<Func<TModel, Nullable<int>>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            IEnumerable<ModelValidator> validators = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData).GetValidators(HtmlHelper.ViewContext.Controller.ControllerContext);

            IntegerTextBoxBuilder builder = new IntegerTextBoxBuilder(Create(() => new IntegerTextBox(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<int>>())));
            return builder.Name(GetName(expression))
                          .Value(expression.Compile()(HtmlHelper.ViewData.Model))
                          .MinValue(GetRangeValidationParameter<int>(validators, "minimum"))
                          .MaxValue(GetRangeValidationParameter<int>(validators, "maximum"));
        }

        /// <summary>
        /// Creates a new <see cref="IntegerTextBox{int}"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().IntegerTextBoxFor(m=>m.Property) %&gt;
        /// </code>
        /// </example>
        public virtual IntegerTextBoxBuilder IntegerTextBoxFor(Expression<Func<TModel, int>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            IEnumerable<ModelValidator> validators = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData).GetValidators(HtmlHelper.ViewContext.Controller.ControllerContext);

            IntegerTextBoxBuilder builder = new IntegerTextBoxBuilder(Create(() => new IntegerTextBox(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<int>>())));
            return builder.Name(GetName(expression))
                          .Value(expression.Compile()(HtmlHelper.ViewData.Model))
                          .MinValue(GetRangeValidationParameter<int>(validators, "minimum"))
                          .MaxValue(GetRangeValidationParameter<int>(validators, "maximum"));
        }

        /// <summary>
        /// Creates a new <see cref="CurrencyTextBox{Nullable{decimal}}"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().CurrencyTextBoxFor(m=>m.Property) %&gt;
        /// </code>
        /// </example>
        public virtual CurrencyTextBoxBuilder CurrencyTextBoxFor(Expression<Func<TModel, Nullable<decimal>>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            IEnumerable<ModelValidator> validators = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData).GetValidators(HtmlHelper.ViewContext.Controller.ControllerContext);

            CurrencyTextBoxBuilder builder = new CurrencyTextBoxBuilder(Create(() => new CurrencyTextBox(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<decimal>>())));
            return builder.Name(GetName(expression))
                          .Value(expression.Compile()(HtmlHelper.ViewData.Model))
                          .MinValue(GetRangeValidationParameter<decimal>(validators, "minimum"))
                          .MaxValue(GetRangeValidationParameter<decimal>(validators, "maximum"));
        }

        /// <summary>
        /// Creates a new <see cref="CurrencyTextBox{decimal}"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().CurrencyTextBoxFor(m=>m.Property) %&gt;
        /// </code>
        /// </example>
        public virtual CurrencyTextBoxBuilder CurrencyTextBoxFor(Expression<Func<TModel, decimal>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            IEnumerable<ModelValidator> validators = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData).GetValidators(HtmlHelper.ViewContext.Controller.ControllerContext);

            CurrencyTextBoxBuilder builder = new CurrencyTextBoxBuilder(Create(() => new CurrencyTextBox(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<decimal>>())));
            return builder.Name(GetName(expression))
                          .Value(expression.Compile()(HtmlHelper.ViewData.Model))
                          .MinValue(GetRangeValidationParameter<decimal>(validators, "minimum"))
                          .MaxValue(GetRangeValidationParameter<decimal>(validators, "maximum"));
        }

        /// <summary>
        /// Creates a new <see cref="PercentTextBox{Nullable{double}}"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PercentTextBoxFor(m=>m.Property) %&gt;
        /// </code>
        /// </example>
        public virtual PercentTextBoxBuilder PercentTextBoxFor(Expression<Func<TModel, Nullable<double>>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            IEnumerable<ModelValidator> validators = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData).GetValidators(HtmlHelper.ViewContext.Controller.ControllerContext);

            PercentTextBoxBuilder builder = new PercentTextBoxBuilder(Create(() => new PercentTextBox(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<double>>())));
            return builder.Name(GetName(expression))
                          .Value(expression.Compile()(HtmlHelper.ViewData.Model))
                          .MinValue(GetRangeValidationParameter<double>(validators, "minimum"))
                          .MaxValue(GetRangeValidationParameter<double>(validators, "maximum"));
        }

        /// <summary>
        /// Creates a new <see cref="PercentTextBox{double}"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().PercentTextBoxFor(m=>m.Property) %&gt;
        /// </code>
        /// </example>
        public virtual PercentTextBoxBuilder PercentTextBoxFor(Expression<Func<TModel, double>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            IEnumerable<ModelValidator> validators = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData).GetValidators(HtmlHelper.ViewContext.Controller.ControllerContext);

            PercentTextBoxBuilder builder = new PercentTextBoxBuilder(Create(() => new PercentTextBox(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<ITextboxBaseHtmlBuilderFactory<double>>())));
            return builder.Name(GetName(expression))
                          .Value(expression.Compile()(HtmlHelper.ViewData.Model))
                          .MinValue(GetRangeValidationParameter<double>(validators, "minimum"))
                          .MaxValue(GetRangeValidationParameter<double>(validators, "maximum"));
        }

        /// <summary>
        /// Creates a new <see cref="DatePicker{Nullable{DateTime}}"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DatePickerFor(m=>m.Property) %&gt;
        /// </code>
        /// </example>
        public virtual DatePickerBuilder DatePickerFor(Expression<Func<TModel, Nullable<DateTime>>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            IEnumerable<ModelValidator> validators = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData).GetValidators(HtmlHelper.ViewContext.Controller.ControllerContext);

            DatePickerBuilder builder = new DatePickerBuilder(Create(() => new DatePicker(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IDatePickerHtmlBuilderFactory>())));
            return builder.Name(GetName(expression))
                          .Value(expression.Compile()(HtmlHelper.ViewData.Model))
                          .MinDate(GetRangeValidationParameter<DateTime>(validators, "minimum") ?? builder.ToComponent().defaultMinDate)
                          .MaxDate(GetRangeValidationParameter<DateTime>(validators, "maximum") ?? builder.ToComponent().defaultMaxDate);
        }

        /// <summary>
        /// Creates a new <see cref="DatePicker{DateTime}"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DatePickerFor(m=>m.Property) %&gt;
        /// </code>
        /// </example>
        public virtual DatePickerBuilder DatePickerFor(Expression<Func<TModel, DateTime>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            IEnumerable<ModelValidator> validators = ModelMetadata.FromLambdaExpression(expression, HtmlHelper.ViewData).GetValidators(HtmlHelper.ViewContext.Controller.ControllerContext);

            DatePickerBuilder builder = new DatePickerBuilder(Create(() => new DatePicker(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IDatePickerHtmlBuilderFactory>())));
            return builder.Name(GetName(expression))
                          .Value(expression.Compile()(HtmlHelper.ViewData.Model))
                          .MinDate(GetRangeValidationParameter<DateTime>(validators, "minimum") ?? builder.ToComponent().defaultMinDate)
                          .MaxDate(GetRangeValidationParameter<DateTime>(validators, "maximum") ?? builder.ToComponent().defaultMaxDate);
        }

        /// <summary>
        /// Creates a new <see cref="DropDownList"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().DropDownListFor(m=>m.Property) %&gt;
        /// </code>
        /// </example>
        public virtual DropDownListBuilder DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            return DropDownListBuilder.Create(Create(() => new DropDownList(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>())))
                                      .Name(GetName(expression));
        }

        /// <summary>
        /// Creates a new <see cref="ComboBox"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().ComboBoxFor(m=>m.Property) %&gt;
        /// </code>
        /// </example>
        public virtual ComboBoxBuilder ComboBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            return ComboBoxBuilder.Create(Create(() => new ComboBox(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>())))
                                  .Name(GetName(expression));
        }

        /// <summary>
        /// Creates a new <see cref="AutoComplete"/>.
        /// </summary>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().AutoCompleteFor(m=>m.Property) %&gt;
        /// </code>
        /// </example>
        public virtual AutoCompleteBuilder AutoCompleteFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            return AutoCompleteBuilder.Create(Create(() => new AutoComplete(HtmlHelper.ViewContext, ClientSideObjectWriterFactory, ServiceLocator.Current.Resolve<IUrlGenerator>())))
                                      .Name(GetName(expression));
        }

        private string GetName(LambdaExpression expression)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            return HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
        }

        private Nullable<TValue> GetRangeValidationParameter<TValue>(IEnumerable<ModelValidator> validators, string parameter) where TValue : struct
        {
            var rangeValidators = validators.OfType<RangeAttributeAdapter>().Cast<RangeAttributeAdapter>();

            object value = null;

            if (rangeValidators.Any()) 
            {
                var clientValidationsRules = rangeValidators.First()
                                                            .GetClientValidationRules()
                                                            .OfType<ModelClientValidationRangeRule>()
                                                            .Cast<ModelClientValidationRangeRule>();

                if(clientValidationsRules.Any() && clientValidationsRules.First().ValidationParameters.TryGetValue(parameter, out value))
                    return (TValue)Convert.ChangeType(value, typeof(TValue));
            }
            return null;
        }
    }
#endif
}
