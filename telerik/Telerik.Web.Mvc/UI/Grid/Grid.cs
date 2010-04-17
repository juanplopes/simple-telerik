// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;

    using Extensions;
    using Infrastructure;
    using Infrastructure.Implementation;
    using Resources;

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
    public class Grid<T> : ViewComponentBase, IGridBindingContext, IGridColumnContainer<T> where T : class
    {
        private readonly IGridHtmlBuilderFactory builderFactory;
        private int rowIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid{T}"/> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="clientSideObjectWriterFactory">The client side object writer factory.</param>
        /// <param name="urlGenerator">The URL generator.</param>
        /// <param name="builderFactory">The builder factory.</param>
        public Grid(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IUrlGenerator urlGenerator, IGridHtmlBuilderFactory builderFactory) : base(viewContext, clientSideObjectWriterFactory)
        {
            Guard.IsNotNull(urlGenerator, "urlGenerator");
            Guard.IsNotNull(builderFactory, "builderFactory");

            this.builderFactory = builderFactory;
            UrlGenerator = urlGenerator;

            PrefixUrlParameters = true;
            DataProcessor = new GridDataProcessor(this);
            Columns = new List<GridColumnBase<T>>();
            DataKeys = new List<IGridDataKey<T>>();

            Paging = new GridPagingSettings();
            Sorting = new GridSortSettings();
            Scrolling = new GridScrollingSettings();
            Filtering = new GridFilteringSettings();
            Editing = new GridEditingSettings();
            Grouping = new GridGroupingSettings();

            DataBinding = new GridDataBindingSettings();
            Server = DataBinding.Server;
            Ajax = DataBinding.Ajax;
            WebService = DataBinding.WebService;

            Footer = true;
            Empty = true;

            ClientEvents = new GridClientEvents();
            Selection = new GridSelectionSettings();
            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.grid.js" });

            ToolBar = new GridToolBarSettings<T>();
            Localization = new GridLocalizedStrings();
        }

        public bool Footer
        {
            get;
            set;
        }

        public GridLocalizedStrings Localization
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
            private set;
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
            get;
            private set;
        }

        /// <summary>
        /// Gets the server binding configuration.
        /// </summary>
        public GridBindingSettings Server
        {
            get;
            private set;
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
            get;
            private set;
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
            private set;
        }

        /// <summary>
        /// Gets the columns of the grid.
        /// </summary>
        public IList<GridColumnBase<T>> Columns
        {
            get;
            private set;
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
            private set;
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

            IList<IDictionary<string, object>> columnsAsJson = new List<IDictionary<string, object>>();

            VisibleColumns.Each(column =>
            {
                Dictionary<string, object> json = new Dictionary<string, object>
                                                      {
                                                          {"member", column.Member},
                                                          {"type", column.MemberType.ToJavaScriptType()},
                                                          {"title", column.Title}
                                                      };

                if (!string.IsNullOrEmpty(column.Format))
                {
                    json["format"] = column.Format;
                }
#if MVC2                
                if (column.ReadOnly)
                {
                    json["readonly"] = true;
                }
#endif
                if (!column.Groupable)
                {
                    json["groupable"] = false;
                }

                if (!column.HtmlAttributes.IsEmpty())
                {
                    json["attr"] = column.HtmlAttributes.ToAttributeString();
                }

                if (Filtering.Enabled)
                {
                    IList<FilterDescriptor> columnFilters = DataProcessor.FilterDescriptors.SelectRecursive(descriptor =>
                        {
                            CompositeFilterDescriptor compositeDescriptor = descriptor as CompositeFilterDescriptor;

                            if (compositeDescriptor != null)
                            {
                                return compositeDescriptor.FilterDescriptors;
                            }

                            return null;
                        })
                        .Where(descriptor => descriptor is FilterDescriptor)
                        .Cast<FilterDescriptor>()
                        .Where(descriptor => descriptor.Member == column.Member)
                        .ToList();

                    if (columnFilters.Count > 0)
                    {
                        ArrayList filters = new ArrayList();
                        columnFilters.Each(filter => filters.Add(new { @operator = filter.Operator.ToToken(), value = filter.Value }));
                        json["filters"] = filters;
                    }
                }

                if (Editing.Enabled && IsClientBinding)
                {
                    #if MVC2

                    if (column.EditorHtml != null)
                    {
                        json["editor"] = column.EditorHtml;
                    }

                    GridActionColumn<T> actionColumn = column as GridActionColumn<T>;

                    if (actionColumn != null && actionColumn.Commands.Any())
                    {
                        IList<IDictionary<string, object>> commands = new List<IDictionary<string, object>>();
                        actionColumn.Commands.Each(c =>
                        {
                            IDictionary<string, object> command = new Dictionary<string, object>();
                            command["name"] = c.Name;
                            if (c.HtmlAttributes.Any())
                            {
                                command["attr"] = c.HtmlAttributes.ToAttributeString();
                            }
                            commands.Add(command);
                        });

                        json["commands"] = commands;
                    }

                    #endif
                }

                if (IsClientBinding && column.ClientTemplate.HasValue())
                {
                    json["template"] = column.ClientTemplate;
                }

                if (column.MemberType != null && column.MemberType.IsEnum)
                {
                    IDictionary<string, object> values = new Dictionary<string, object>();
                    foreach (object value in Enum.GetValues(column.MemberType))
                    {
                        values[Enum.GetName(column.MemberType, value)] = value;
                    }

                    json["values"] = values;
                }

                if (Sorting.Enabled)
                {
                    SortDescriptor sortDescriptor = DataProcessor.SortDescriptors.FirstOrDefault(sort => sort.Member == column.Member);
                    if (sortDescriptor != null)
                    {
                        json["order"] = sortDescriptor.SortDirection == ListSortDirection.Ascending ? "asc" : "desc";
                    }
                }
                columnsAsJson.Add(json);
            });

            objectWriter.AppendCollection("columns", columnsAsJson);
            List<string> plugins = new List<string>();

            if (Filtering.Enabled)
            {
                plugins.Add("filtering");
            }

            if (Editing.Enabled)
            {
                plugins.Add("editing");

                Dictionary<string, object> editing = new Dictionary<string, object>();
                if (!Editing.DisplayDeleteConfirmation)
                {
                    editing["confirmDelete"] = false;
                }

                if (editing.Any())
                {
                    objectWriter.AppendObject("editing", editing);
                }

                if (IsClientBinding)
                {
                    if (DataKeys.Any())
                    {
                        Dictionary<string, string> dataKeys = new Dictionary<string, string>();
                        DataKeys.Each(dataKey =>
                        {
                            dataKeys[dataKey.Name] = dataKey.RouteKey;
                        });

                        objectWriter.AppendObject("dataKeys", dataKeys);
                    }
                }
            }

            if (Grouping.Enabled)
            {
                plugins.Add("grouping");

                if (DataProcessor.GroupDescriptors.Any())
                {
                    IList<IDictionary<string, object>> groups = new List<IDictionary<string, object>>();
                    DataProcessor.GroupDescriptors.Each(groupDescriptor =>
                    {
                        Dictionary<string, object> group = new Dictionary<string, object>();
                        group["member"] = groupDescriptor.Member;
                        group["order"] = groupDescriptor.SortDirection == ListSortDirection.Ascending ? "asc" : "desc";
                        group["title"] = this.GroupTitle(groupDescriptor);

                        groups.Add(group);
                    });

                    objectWriter.AppendCollection("groups", groups);

                    objectWriter.Append("groupBy", GridDescriptorSerializer.Serialize(DataProcessor.GroupDescriptors));
                }
            }

            if (plugins.Any())
            {
                objectWriter.AppendCollection("plugins", plugins);
            }

            if (!IsClientBinding && (Grouping.Enabled || Filtering.Enabled))
            {
                Server.Select.RouteValues.Merge(ViewContext.RequestContext.RouteData.Values);
                Server.Select.RouteValues[Prefix(GridUrlParameters.CurrentPage)] = "{0}";
                Server.Select.RouteValues[Prefix(GridUrlParameters.OrderBy)] = "{1}";
                Server.Select.RouteValues[Prefix(GridUrlParameters.GroupBy)] = "{2}";
                Server.Select.RouteValues[Prefix(GridUrlParameters.Filter)] = "{3}";
                GridUrlBuilder<T> urlBuilder = new GridUrlBuilder<T>(this);
                objectWriter.Append("urlFormat", urlBuilder.Url(Server.Select));
            }

            if (Paging.Enabled)
            {
                objectWriter.Append("pageSize", PageSize, 10);
                objectWriter.Append("total", DataProcessor.Total);
                objectWriter.Append("currentPage", DataProcessor.CurrentPage);
            }
            else
            {
                objectWriter.Append("pageSize", 0);
            }

            if (Sorting.Enabled)
            {
                objectWriter.Append("sortMode", Sorting.SortMode == GridSortMode.MultipleColumn ? "multi" : "single");

                if (DataProcessor.SortDescriptors.Any())
                {
                    objectWriter.Append("orderBy", GridDescriptorSerializer.Serialize(DataProcessor.SortDescriptors));
                }
            }

            objectWriter.Append("selectable", Selection.Enabled, false);

            if (Ajax.Enabled)
            {
                GridUrlBuilder<T> urlBuilder = new GridUrlBuilder<T>(this);

                Dictionary<string, string> ajax = new Dictionary<string, string>();

                ajax["selectUrl"] = urlBuilder.Url(Ajax.Select);

                if (Ajax.Insert.HasValue())
                {
                    ajax["insertUrl"] = urlBuilder.Url(Ajax.Insert);
                }

                if (Ajax.Update.HasValue())
                {
                    ajax["updateUrl"] = urlBuilder.Url(Ajax.Update);
                }

                if (Ajax.Delete.HasValue())
                {
                    ajax["deleteUrl"] = urlBuilder.Url(Ajax.Delete);
                }

                objectWriter.AppendObject("ajax", ajax);
            }

            if (WebService.Enabled)
            {
                Dictionary<string, string> webService = new Dictionary<string, string>();

                webService["selectUrl"] = UrlGenerator.Generate(ViewContext.RequestContext, WebService.Select.Url);

                if (WebService.Insert.HasValue())
                {
                    webService["insertUrl"] = UrlGenerator.Generate(ViewContext.RequestContext, WebService.Insert.Url);
                }

                if (WebService.Update.HasValue())
                {
                    webService["updateUrl"] = UrlGenerator.Generate(ViewContext.RequestContext, WebService.Update.Url);
                }

                if (WebService.Delete.HasValue())
                {
                    webService["deleteUrl"] = UrlGenerator.Generate(ViewContext.RequestContext, WebService.Delete.Url);
                }

                objectWriter.AppendObject("ws", webService);
            }

            if (Editing.Enabled && IsClientBinding && !Empty)
            {
                if (DataProcessor.ProcessedDataSource is IQueryable<AggregateFunctionsGroup>)
                {
                    IEnumerable<IGroup> grouppedDataSource = DataProcessor.ProcessedDataSource.Cast<IGroup>();
                    objectWriter.AppendCollection("data", grouppedDataSource.Leaves().Cast<T>());
                }
                else
                {
                    objectWriter.AppendCollection("data", DataProcessor.ProcessedDataSource.Cast<T>());
                }
            }

            objectWriter.Append("onLoad", ClientEvents.OnLoad);
            objectWriter.Append("onDataBinding", ClientEvents.OnDataBinding);
            objectWriter.Append("onRowDataBound", ClientEvents.OnRowDataBound);
            objectWriter.Append("onRowSelected", ClientEvents.OnRowSelected);
            objectWriter.Append("onDataBound", ClientEvents.OnDataBound);
            objectWriter.Append("onError", ClientEvents.OnError);

            if (!Localization.IsDefault)
            {
                objectWriter.AppendObject("localization", Localization.ToJson());
            }

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        internal int Colspan
        {
            get
            {
                return DataProcessor.GroupDescriptors.Count + VisibleColumns.Count;
            }
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            if (Filtering.Enabled)
            {
                ScriptFileNames.Add("telerik.grid.filtering.js");
            }

            if (Editing.Enabled)
            {
                ScriptFileNames.Add("jquery.validate.js");
                ScriptFileNames.Add("telerik.grid.editing.js");
            }

            if (Grouping.Enabled)
            {
                ScriptFileNames.Add("telerik.draganddrop.js");
                ScriptFileNames.Add("telerik.grid.grouping.js");
            }

            if (Columns.IsEmpty())
            {
                foreach (GridColumnBase<T> column in new GridColumnGenerator<T>(this).GetColumns())
                {
                    Columns.Add(column);
                }
            }

            var dateColumns = Columns.Where(c => c.MemberType == typeof(DateTime) || c.MemberType == typeof(DateTime?));
        
            if (dateColumns.Any())
            {
                ScriptFileNames.Insert(1, "telerik.calendar.js");
                ScriptFileNames.Insert(2, "telerik.datepicker.js");
            }

            var numericColumns = Columns.Where(c => c.MemberType.IsNumericType());

            if (numericColumns.Any())
            {
                ScriptFileNames.Insert(1, "telerik.textbox.js");
            }

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

                #endif

                IGridHtmlBuilder<T> builder = builderFactory.Create(this);

                IHtmlNode grid = builder.GridTag();

                if (Scrolling.Enabled)
                {
                    WriteToolbar(grid);
                    WriteGroupPanel(builder, grid);
                    WriteHeader(builder, grid);
                    WriteBody(builder, grid);
                    WriteFooter(builder, grid);
                }
                else
                {
                    WriteToolbar(grid);
                    WriteGroupPanel(builder, grid);

                    IHtmlNode table = builder.TableTag().AppendTo(grid);

                    WriteHeader(builder, table);
                    WriteFooter(builder, table);
                    WriteBody(builder, table);
                }

                grid.WriteTo(writer);
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

        private void WriteFooter(IGridHtmlBuilder<T> renderer, IHtmlNode parentTag)
        {
            if (Footer)
            {
                IHtmlNode footer = renderer.FootTag(parentTag);

                renderer.LoadingIndicatorTag().AppendTo(footer);

                WritePager(renderer, footer);
            }
        }

        private void WritePager(IGridHtmlBuilder<T> renderer, IHtmlNode parentTag)
        {
            if (Paging.Enabled && (Paging.Position == GridPagerPosition.Bottom || Paging.Position == GridPagerPosition.Both))
            {
                renderer.PagerTag().AppendTo(parentTag);
                renderer.PagerStatusTag().AppendTo(parentTag);
            }
        }

        public bool Empty
        {
            get;
            set;
        }

        private bool IsClientBinding
        {
            get
            {
                return Ajax.Enabled || WebService.Enabled;
            }
        }

        private void WriteBody(IGridHtmlBuilder<T> builder, IHtmlNode parentTag)
        {
            rowIndex = 0;
            IHtmlNode tbody = builder.BodyTag(parentTag);

#if MVC2
            if (this.IsInInsertMode())
            {
                T dataItem = Activator.CreateInstance<T>();

                GridRow<T> row = new GridRow<T>(dataItem, rowIndex) {InInsertMode = true};

                Row(row, builder).AppendTo(tbody);

                rowIndex++;

                Empty = false;
            }

            if (Editing.Enabled && IsClientBinding)
            {
                try
                {
                    ViewContext.HttpContext.Items["$SelfInitialize$"] = true;

                    T dataItem = Activator.CreateInstance<T>();
                    VisibleColumns.Each(column =>
                    {
                        var context = new GridCell<T>(column, dataItem)
                        {
                            InEditMode = true
                        };

                        IHtmlNode td = new HtmlTag("td");
                        column.HtmlBuilder.Html(context, td);
                        column.EditorHtml = td.InnerHtml;
                    });
                }
                finally
                {
                    ViewContext.HttpContext.Items.Remove("$SelfInitialize$");
                }
            }
#endif

            if (DataProcessor.ProcessedDataSource != null)
            {
                if (DataProcessor.ProcessedDataSource is IQueryable<AggregateFunctionsGroup>)
                {
                    IEnumerable<IGroup> grouppedDataSource = DataProcessor.ProcessedDataSource.Cast<IGroup>();

                    grouppedDataSource.Each(group => WriteGroup(builder, group, tbody, 0));
                }
                else
                {
                    IEnumerable<T> dataSource = DataProcessor.ProcessedDataSource.Cast<T>();

                    dataSource.Each(row =>
                    {
                        BoundRow(builder, row, rowIndex).AppendTo(tbody);
                        rowIndex++;
                        Empty = false;
                    });
                }
            }

            if (Empty)
            {
                builder.EmptyRowTag().AppendTo(tbody);
            }
        }

        private void WriteGroup(IGridHtmlBuilder<T> builder, IGroup group, IHtmlNode tbody, int level)
        {
            builder.GroupRowTag(group, level).AppendTo(tbody);

            if (group.HasSubgroups)
            {
                group.Subgroups.Each(subgroup => WriteGroup(builder, subgroup, tbody, level + 1));
            }
            else
            {
                IEnumerable<T> dataSource = group.Items.Cast<T>();
                dataSource.Each(row =>
                {
                    BoundRow(builder, row, rowIndex).AppendTo(tbody);
                    rowIndex++;
                    Empty = false;
                });
            }
        }

        private IHtmlNode BoundRow(IGridHtmlBuilder<T> renderer, T dataItem, int index)
        {
            GridRow<T> rowContext = new GridRow<T>(dataItem, index);

            #if MVC2
            if (Editing.Enabled)
            {
                rowContext.InEditMode = this.IsRecordInEditMode(dataItem);
            }
            #endif

            rowContext.Selected = this.IsRecordSelected(dataItem);

            return Row(rowContext, renderer);
        }

        private IHtmlNode Row(GridRow<T> row, IGridHtmlBuilder<T> builder)
        {
            if (RowAction != null)
            {
                RowAction(row);
            }

            IHtmlNode tr = builder.RowTag(row);
            IHtmlNode cellContainer = tr;

#if MVC2
            if (row.InInsertMode)
            {
                cellContainer = builder.EditFormTag(tr, Server.Insert);
            }
            else if (row.InEditMode)
            {
                cellContainer = builder.EditFormTag(tr, Server.Update);
            }
#endif
            DataProcessor.GroupDescriptors.Each(group => new HtmlTag("td").AddClass(UIPrimitives.Grid.GroupCell)
                    .Html("&nbsp;").AppendTo(cellContainer));

            VisibleColumns.Each(column =>
            {
                GridCell<T> cell = new GridCell<T>(column, row.DataItem)
                                       {
                                           Selected = row.Selected
                                       };

                #if MVC2
                cell.InEditMode = row.InEditMode;
                cell.InInsertMode = row.InInsertMode;
                #endif
                if (CellAction != null)
                {
                    CellAction(cell);
                }

                builder.CellTag(cell).AppendTo(cellContainer);
            });

            return tr;
        }
        
        private void WriteGroupPanel(IGridHtmlBuilder<T> builder, IHtmlNode parent)
        {
            if (Grouping.Enabled)
            {
                IHtmlNode div = new HtmlTag("div").AddClass("t-grouping-header")
                    .AppendTo(parent);

                if (DataProcessor.GroupDescriptors.Any())
                {
                    DataProcessor.GroupDescriptors.Each(group => builder.GroupIndicatorTag(group).AppendTo(div));
                }
                else
                {
                    div.Html(Localization.GroupHint);
                }
            }
        }

        private void WriteHeader(IGridHtmlBuilder<T> builder, IHtmlNode parent)
        {
            IHtmlNode thead = builder.HeadTag(parent);

            if (Paging.Enabled && (Paging.Position == GridPagerPosition.Top || Paging.Position == GridPagerPosition.Both))
            {
                WritePager(builder, thead);
            }

            IHtmlNode tr = builder.RowTag().AppendTo(thead);

            DataProcessor.GroupDescriptors.Each(group =>
                new HtmlTag("th")
                    .AddClass(UIPrimitives.Grid.GroupCell, UIPrimitives.Header)
                    .Html("&nbsp;")
                .AppendTo(tr));

            VisibleColumns.Each(column => builder.HeadCellTag(column).AppendTo(tr));
        }

        private void WriteToolbar(IHtmlNode parent)
        {
            if (ToolBar.Enabled)
            {
                IHtmlNode container = new HtmlTag("div")
                                          .AddClass(UIPrimitives.Grid.ToolBar)
                                          .AppendTo(parent);

                ToolBar.Commands.Each(command => command.Html(this, container));
            }
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
                if (Columns.Where(c => c.Template != null).Count() > 0)
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
                if (HasCommandOfType<GridEditActionCommand<T>>())
                {
                    if (!CurrrentBinding.Update.HasValue())
                    {
                        throw new NotSupportedException(TextResource.EditCommandRequiresUpdate);
                    }
                }

                if (HasCommandOfType<GridDeleteActionCommand<T>>())
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
    }
}
