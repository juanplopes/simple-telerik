// (c) Copyright 2002-2010 Telerik
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html.
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.UI;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.Resources;
    using Telerik.Web.Mvc.UI.Html;

    /// <summary>
    /// Telerik Grid for ASP.NET MVC is a view component for presenting tabular data.
    /// It supports the following features:
    /// <list type="bullet">
    ///     <item>Flexible databinding - server, ajax and web service</item>
    ///     <item>Paging, sorting and filtering</item>
    ///     <item>Light HTML and JavaScript footprint</item>
    /// </list>
    /// </summary>
    /// <typeparam name="T">The type of the data item which the grid is bound to.</typeparam>
    public class Grid<T> : ViewComponentBase, IGridColumnContainer<T>, IGrid where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Grid{T}"/> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="clientSideObjectWriterFactory">The client side object writer factory.</param>
        /// <param name="urlGenerator">The URL generator.</param>
        /// <param name="builderFactory">The builder factory.</param>
        public Grid(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IUrlGenerator urlGenerator)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            Guard.IsNotNull(urlGenerator, "urlGenerator");
            
            UrlGenerator = urlGenerator;

            PrefixUrlParameters = true;
            DataProcessor = new GridDataProcessor(this);
            Columns = new List<GridColumnBase<T>>();
            DataKeys = new List<IGridDataKey<T>>();

            Paging = new GridPagingSettings(this);
            Sorting = new GridSortSettings(this);
            Scrolling = new GridScrollingSettings();
            Filtering = new GridFilteringSettings();
            Editing = new GridEditingSettings(this)
            {
                PopUp = new Window(viewContext, clientSideObjectWriterFactory, new WindowHtmlBuilderFactory())
                {
                    Modal = true,
                    Draggable = true
                }
            };

            Grouping = new GridGroupingSettings(this);
            Resizing = new GridResizingSettings();

            TableHtmlAttributes = new RouteValueDictionary();

            DataBinding = new GridDataBindingSettings(this);

            Footer = true;
            IsEmpty = true;

            ClientEvents = new GridClientEvents();
            Selection = new GridSelectionSettings();
            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.grid.js" });

            ToolBar = new GridToolBarSettings<T>();
            Localization = new GridLocalization();
        }

        public IGridDetailView<T> DetailView
        {
            get;
            set;
        }

        public IDictionary<string, object> TableHtmlAttributes
        {
            get;
            private set;
        }

        public GridResizingSettings Resizing
        {
            get;
            private set;
        }

        public bool Footer
        {
            get;
            set;
        }

        public GridLocalization Localization
        {
            get;
            set;
        }

        public GridToolBarSettings<T> ToolBar
        {
            get;
            private set;
        }

        public GridGroupingSettings Grouping
        {
            get;
            private set;
        }

        public GridEditingSettings Editing
        {
            get;
            private set;
        }

        public GridDataBindingSettings DataBinding
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the selection configuration
        /// </summary>
        public GridSelectionSettings Selection
        {
            get;
            private set;
        }

        public IList<IGridDataKey<T>> DataKeys
        {
            get;
            private set;
        }

        IEnumerable<IGridDataKey> IGrid.DataKeys
        {
            get
            {
                return DataKeys.Cast<IGridDataKey>();
            }
        }

        /// <summary>
        /// Gets the client events of the grid.
        /// </summary>
        /// <value>The client events.</value>
        public GridClientEvents ClientEvents
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the filtering configuration.
        /// </summary>
        public GridFilteringSettings Filtering
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the web service configuration
        /// </summary>
        public GridBindingSettings WebService
        {
            get
            {
                return DataBinding.WebService;
            }
        }

        /// <summary>
        /// Gets the server binding configuration.
        /// </summary>
        public GridBindingSettings Server
        {
            get
            {
                return DataBinding.Server;
            }
        }

        /// <summary>
        /// Gets the scrolling configuration.
        /// </summary>
        public GridScrollingSettings Scrolling
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ajax configuration.
        /// </summary>
        public GridBindingSettings Ajax
        {
            get
            {
                return DataBinding.Ajax;
            }
        }

        public IUrlGenerator UrlGenerator
        {
            get;
            private set;
        }

        public GridDataProcessor DataProcessor
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether custom binding is enabled.
        /// </summary>
        /// <value><c>true</c> if custom binding is enabled; otherwise, <c>false</c>. The default value is <c>false</c></value>
        public bool EnableCustomBinding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the paging configuration.
        /// </summary>
        public GridPagingSettings Paging
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the columns of the grid.
        /// </summary>
        public IList<GridColumnBase<T>> Columns
        {
            get;
            private set;
        }

        IEnumerable<IGridColumn> IGrid.Columns
        {
            get
            {
                return Columns.Cast<IGridColumn>();
            }
        }

        public IList<GridColumnBase<T>> VisibleColumns
        {
            get
            {
                return Columns.Where(c => c.Visible).ToList();
            }
        }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public IEnumerable<T> DataSource
        {
            get;
            set;
        }

        int IGridBindingContext.Total
        {
            get
            {
                return Paging.Total;
            }
        }

        IEnumerable IGridBindingContext.DataSource
        {
            get
            {
                return DataSource;
            }
        }

        IList<SortDescriptor> IGridBindingContext.SortDescriptors
        {
            get
            {
                return Sorting.OrderBy;
            }
        }

        IList<GroupDescriptor> IGridBindingContext.GroupDescriptors
        {
            get
            {
                return Grouping.Groups;
            }
        }

        IList<CompositeFilterDescriptor> IGridBindingContext.FilterDescriptors
        {
            get
            {
                return Filtering.Filters;
            }
        }

        ControllerBase IGridBindingContext.Controller
        {
            get
            {
                return ViewContext.Controller;
            }
        }

        /// <summary>
        /// Gets the page size of the grid.
        /// </summary>
        public int PageSize
        {
            get
            {
                if (!Paging.Enabled)
                {
                    return 0;
                }

                return Paging.PageSize;
            }
        }

        /// <summary>
        /// Gets the sorting configuration.
        /// </summary>
        /// <value>The sorting.</value>
        public GridSortSettings Sorting
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to add the <see cref="Name"/> property of the grid as a prefix in url parameters.
        /// </summary>
        /// <value><c>true</c> if prefixing is enabled; otherwise, <c>false</c>. The default value is <c>true</c></value>
        public bool PrefixUrlParameters
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the action executed when rendering a row.
        /// </summary>
        public Action<GridRow<T>> RowAction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the action executed when rendering a cell.
        /// </summary>
        public Action<GridCell<T>> CellAction
        {
            get;
            set;
        }

        public string Prefix(string parameter)
        {
            return PrefixUrlParameters ? Id + "-" + parameter : parameter;
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tGrid", writer);

            objectWriter.Start();

            new GridClientObjectSerializer<T>(this).Serialize(objectWriter);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        internal int Colspan
        {
            get
            {
                int colspan = DataProcessor.GroupDescriptors.Count + VisibleColumns.Count;

                if (DetailView != null)
                {
                    colspan++;
                }

                return colspan;
            }
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
#if MVC2
            bool orignalClientValidationEnabled = ViewContext.ClientValidationEnabled;
            FormContext originalFormContext = ViewContext.FormContext;
            
            try
            {
                ViewContext.ClientValidationEnabled = true;
                ViewContext.FormContext = new FormContext
                {
                    FormId = Name + "form"
                };

                if (Editing.Enabled && IsClientBinding)
                {
                    InitializeEditors();
                }

#endif

                if (Columns.IsEmpty())
                {
                    foreach (GridColumnBase<T> column in new GridColumnGenerator<T>(this).GetColumns())
                    {
                        Columns.Add(column);
                    }
                }

                RegisterScriptFiles();
                
                var builder = new GridHtmlBuilder<T>(this);
                
                builder.Build()
                       .WriteTo(writer);
#if MVC2
                if (this.IsInInsertMode() || this.IsInEditMode() || (Editing.Enabled && IsClientBinding))
                {
                    ViewContext.OutputClientValidation();
                }
            }
            finally
            {
                ViewContext.FormContext = originalFormContext;
                ViewContext.ClientValidationEnabled = orignalClientValidationEnabled;
            }

#endif
            base.WriteHtml(writer);
        }

        public void RegisterScriptFiles()
        {
            if (Filtering.Enabled)
            {
                ScriptFileNames.Add("telerik.grid.filtering.js");
            }

            if (Editing.Enabled)
            {
                ScriptFileNames.Add("jquery.validate.js");
                if (Editing.Mode == GridEditMode.PopUp)
                {
                    ScriptFileNames.Add("telerik.draganddrop.js");
                    ScriptFileNames.Add("telerik.window.js");
                }
                ScriptFileNames.Add("telerik.grid.editing.js");

                if (Editing.Mode != GridEditMode.InLine)
                {
                    var properties = typeof(T).GetProperties();

                    if (properties.Where(p => p.PropertyType.IsDateTime()).Any())
                    {
                        ScriptFileNames.Insert(1, "telerik.calendar.js");
                        ScriptFileNames.Insert(2, "telerik.datepicker.js");
                    }

                    if (properties.Where(p => p.PropertyType.IsDateTime()).Any())
                    {
                        ScriptFileNames.Insert(1, "telerik.calendar.js");
                        ScriptFileNames.Insert(2, "telerik.datepicker.js");
                    }

                    if (properties.Where(p => p.PropertyType.IsNumericType()).Any())
                    {
                        ScriptFileNames.Insert(1, "telerik.textbox.js");
                    }
                }
            }

            if (Grouping.Enabled)
            {
                ScriptFileNames.Add("telerik.draganddrop.js");
                ScriptFileNames.Add("telerik.grid.grouping.js");
            }

            if (Resizing.Enabled)
            {
                ScriptFileNames.Add("telerik.draganddrop.js");
                ScriptFileNames.Add("telerik.grid.resizing.js");
            }

            var dateColumns = Columns.OfType<IGridBoundColumn>().Where(c => c.MemberType.IsDateTime());

            if (dateColumns.Any())
            {
                ScriptFileNames.Insert(1, "telerik.calendar.js");
                ScriptFileNames.Insert(2, "telerik.datepicker.js");
            }

            var numericColumns = Columns.OfType<IGridBoundColumn>().Where(c => c.MemberType.IsNumericType());

            if (numericColumns.Any())
            {
                ScriptFileNames.Insert(1, "telerik.textbox.js");
            }
        }

        public bool IsEmpty
        {
            get;
            set;
        }

        public bool IsClientBinding
        {
            get
            {
                return Ajax.Enabled || WebService.Enabled;
            }
        }

        public void VerifySettings()
        {
            EnsureRequired();
        }

        protected override void EnsureRequired()
        {
            base.EnsureRequired();

            if (Ajax.Enabled && WebService.Enabled)
            {
                throw new NotSupportedException(TextResource.CannotUseAjaxAndWebServiceAtTheSameTime);
            }

            if (IsClientBinding)
            {
                if (Columns.OfType<IGridTemplateColumn<T>>().Where(c => c.Template != null && string.IsNullOrEmpty(c.ClientTemplate)).Any())
                {
                    throw new NotSupportedException(TextResource.CannotUseTemplatesInAjaxOrWebService);
                }
            }

            if (WebService.Enabled && string.IsNullOrEmpty(WebService.Select.Url))
            {
                throw new ArgumentException(TextResource.WebServiceUrlRequired);
            }

            if (!DataKeys.Any() && (Editing.Enabled || (Selection.Enabled && !IsClientBinding)))
            {
                throw new NotSupportedException(TextResource.DataKeysEmpty);
            }

            if (Editing.Enabled)
            {
                if (HasCommandOfType<GridEditActionCommand>())
                {
                    if (!CurrrentBinding.Update.HasValue())
                    {
                        throw new NotSupportedException(TextResource.EditCommandRequiresUpdate);
                    }
                }

                if (HasCommandOfType<GridDeleteActionCommand>())
                {
                    if (!CurrrentBinding.Delete.HasValue())
                    {
                        throw new NotSupportedException(TextResource.DeleteCommandRequiresDelete);
                    }
                }

                if (HasCommandOfType<GridToolBarInsertCommand<T>>())
                {
                    if (!CurrrentBinding.Insert.HasValue())
                    {
                        throw new NotSupportedException(TextResource.InsertCommandRequiresInsert);
                    }
                }
            }
        }

        private bool HasCommandOfType<TCommand>()
        {
            return Columns.OfType<GridActionColumn<T>>().SelectMany(c => c.Commands).OfType<TCommand>().Any() ||
                ToolBar.Commands.OfType<TCommand>().Any();
        }

        private GridBindingSettings CurrrentBinding
        {
            get
            {
                if (Ajax.Enabled)
                {
                    return Ajax;
                }

                if (WebService.Enabled)
                {
                    return WebService;
                }

                return Server;
            }
        }

        public IHtmlBuilder CreateBuilderFor(GridRow<T> row)
        {
            IHtmlBuilder builder = new GridRowHtmlBuilder<T>(row);
#if MVC2
            if (row.InEditMode || row.InInsertMode)
            {
                var editorBuilder = Editing.Mode != GridEditMode.InLine ? new GridFormEditRowHtmlBuilder<T>(row) : new GridEditRowHtmlBuilder<T>(row);

                editorBuilder.Colspan = Colspan;
                editorBuilder.ID = Name + "form";
                editorBuilder.ActionUrl = new GridUrlBuilder(this).Url(row.InInsertMode ? Server.Insert : Server.Update);

                if (Editing.Mode != GridEditMode.PopUp)
                {
                    builder = editorBuilder;
                }
                else
                {
                    var popup = Editing.PopUp;

                    popup.Html = editorBuilder.Build().ToString();

                    if (!popup.Name.HasValue())
                    {
                        popup.Name = Name + "PopUp";
                    }

                    if (!popup.Title.HasValue())
                    {
                        popup.Title = row.InEditMode ? Localization.Edit : Localization.AddNew;
                    }

                    popup.HtmlAttributes["style"] = "top:10%;left:50%;margin-left: -" + (popup.Width == 0 ? 360 : popup.Width) / 4 + "px";

                    ScriptRegistrar.Current.Register(popup);
                }
            }
#endif
            if (HasDetailView)
            {
#if MVC2                
                if (!(row.InEditMode || row.InInsertMode))
#endif
                {
                    var detailViewAdorner = new GridToggleDetailViewAdorner
                    {
                        Expanded = row.DetailRow.Expanded
                    };

                    builder.Adorners.Add(detailViewAdorner);
                }

                var masterRowAdorner = new GridMasterRowAdorner();

                builder.Adorners.Add(masterRowAdorner);
            }

            if (DataProcessor.GroupDescriptors.Any())
            {
#if MVC2                
                if (!(row.InEditMode || row.InInsertMode))
#endif
                {
                    var repeatingAdorner = new GridTagRepeatingAdorner(DataProcessor.GroupDescriptors.Count)
                    {
                        CssClasses = 
                    { 
                        UIPrimitives.Grid.GroupCell 
                    },
                        Nbsp = true
                    };

                    builder.Adorners.Add(repeatingAdorner);
                }
            }

            return builder;
        }

#if MVC2
        private void InitializeEditors()
        {
            ViewContext.HttpContext.Items["$SelfInitialize$"] = true;

            T dataItem = Activator.CreateInstance<T>();
            var row = new GridRow<T>(this, dataItem, 0);

            if (Editing.Mode != GridEditMode.InLine)
            {
                var formBuilder = new GridFormEditRowHtmlBuilder<T>(row);

                EditorHtml = formBuilder.CreateEditorHtml();
            }
            else
            {
                VisibleColumns.Each(column =>
                {
                    var context = new GridCell<T>(column, dataItem)
                    {
                        InEditMode = true,
                    };
                    IHtmlNode td = column.CreateEditorHtmlBuilder(context).Build();

                    column.EditorHtml = td.InnerHtml;
                });
            }

            ViewContext.HttpContext.Items.Remove("$SelfInitialize$");
        }
#endif
        public string EditorHtml { get; set; }

        public bool HasDetailView
        {
            get
            {
                return DetailView != null;
            }
        }
    }
}
