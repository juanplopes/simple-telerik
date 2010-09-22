// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI.Html;

    public class GridHtmlBuilder<T> : HtmlBuilderBase 
        where T : class
    {
        private readonly Grid<T> grid;

        public GridHtmlBuilder(Grid<T> grid)
        {
            Guard.IsNotNull(grid, "grid");

            this.grid = grid;
        }

        private IHtmlNode CreateGrid()
        {
            return new HtmlTag("div")
                .Attribute("id", grid.Id)
                .Attributes(grid.HtmlAttributes)
                .PrependClass(UIPrimitives.Widget, "t-grid");
        }
        
        private IHtmlNode CreateTable()
        {
            var table = new HtmlTag("table")
                .Attribute("cellspacing", "0")
                .Attributes(grid.TableHtmlAttributes);

            var colgroupBuilder = new GridColgroupHtmlBuilder(grid);
            var colgroup = colgroupBuilder.Build();
            colgroup.AppendTo(table);

            return table;
        }

        private IHtmlNode CreateToolbar()
        {
            var toolbar = new HtmlTag("div")
                      .AddClass(UIPrimitives.ToolBar, UIPrimitives.Grid.ToolBar);

            grid.ToolBar.Commands.Each(command => command.Html(grid, toolbar));

            return toolbar;
        }
        
        private IHtmlNode GroupHeader()
        {
            var groupHeader = new GridGroupHeaderHtmlBuilder<T>(grid).Build();
            return groupHeader;
        }

        protected override IHtmlNode BuildCore()
        {
            var div = CreateGrid();

            if (grid.ToolBar.Enabled)
            {
                var toolbar = CreateToolbar();

                toolbar.AppendTo(div);
            }

            if (grid.Grouping.Enabled)
            {
                var groupHeader = GroupHeader();
                groupHeader.AppendTo(div);
            }

            if (grid.Paging.Enabled)
            {
                if (grid.Paging.Position == GridPagerPosition.Top || grid.Paging.Position ==  GridPagerPosition.Both)
                {
                    var topPager = CreateTopPager();
                    topPager.AppendTo(div);
                }
            }

            var bottomPager = CreateBottomPagerBuilder();

            var headerRow = CreateHeaderRow();

            var body = CreateBody();

            if (grid.Scrolling.Enabled)
            {
                var header = new HtmlTag("div").AddClass("t-grid-header");
                header.AppendTo(div);

                var headerWrapper = new HtmlTag("div").AddClass("t-grid-header-wrap");
                headerWrapper.AppendTo(header);

                var headerTable = CreateTable();
                headerTable.AppendTo(headerWrapper);

                headerRow.AppendTo(headerTable);

                var content = new HtmlTag("div")
                        .AddClass("t-grid-content")
                        .Css("height", grid.Scrolling.Height);

                content.AppendTo(div);
                
                var contentTable = CreateTable();

                contentTable.AppendTo(content);

                body.AppendTo(contentTable);

                if (grid.Footer)
                {
                    var footer = new HtmlTag("div").AddClass("t-grid-footer");
                    footer.AppendTo(div);
                    bottomPager.Build().AppendTo(footer);
                }
            }
            else
            {
                var table = CreateTable();
                
                table.AppendTo(div);
                
                var header = new HtmlTag("thead").Attribute("class", "t-grid-header");
                
                header.AppendTo(table);
                
                headerRow.AppendTo(header);

                if (grid.Footer)
                {
                    var footer = new HtmlTag("tfoot");
                    footer.AppendTo(table);

                    var footerRow = new HtmlTag("tr");
                    footerRow.AppendTo(footer);

                    bottomPager.TagName = "td";
                    bottomPager.Colspan = grid.Colspan;
                    bottomPager.Build().AppendTo(footerRow);
                }
                
                body.AppendTo(table);
            }

            if (ScriptRegistrar.Current.IsRegistered(grid.Editing.PopUp))
            {
                var popup = new LiteralNode(grid.Editing.PopUp.ToHtmlString());
                popup.AppendTo(div);
            }

            return div;
        }
        
        private IHtmlNode CreateBody()
        {
            var body = new HtmlTag("tbody");

            if (grid.DataProcessor.ProcessedDataSource != null)
            {
                if (grid.DataProcessor.ProcessedDataSource is IQueryable<AggregateFunctionsGroup>)
                {
                    var grouppedDataSource = grid.DataProcessor.ProcessedDataSource.Cast<IGroup>();

                    grouppedDataSource.Each(group => 
                    {
                        AppendGroupRow(body, group, 0);
                    });
                }
                else
                {
                    var dataSource = grid.DataProcessor.ProcessedDataSource.Cast<T>();
                    AppendRows(body, dataSource);
                }
            }

            if (body.Children.Count == 0)
            {
                var emptyRowBuilder = new GridEmptyRowHtmlBuilder(grid.Colspan);
                emptyRowBuilder.Build().AppendTo(body);
            }

            return body;
        }

        private void AppendRows(IHtmlNode body, IEnumerable<T> dataSource)
        {
            int rowIndex = 0;

#if MVC2
            if (grid.IsInInsertMode())
            {
                T dataItem = Activator.CreateInstance<T>();

                GridRow<T> row = new GridRow<T>(grid, dataItem, rowIndex) { InInsertMode = true };

                var tr = CreateRow(row);

                if (grid.Editing.Mode != GridEditMode.PopUp)
                {
                    tr.AppendTo(body);
                }

                rowIndex++;

                grid.IsEmpty = false;
            }
#endif
            dataSource.Each(dataItem =>
            {
                GridRow<T> row = new GridRow<T>(grid, dataItem, rowIndex);
                if (grid.HasDetailView)
                {
                    row.DetailRow = new GridDetailRow<T>();
                }
#if MVC2
                if (grid.Editing.Enabled)
                {
                    row.InEditMode = grid.IsRecordInEditMode(row.DataItem);
                }
#endif
                row.Selected = grid.IsRecordSelected(row.DataItem);

                CreateRow(row).AppendTo(body);

                if (grid.HasDetailView)
                {
                    CreateDetailRow(row).AppendTo(body);
                }

                rowIndex++;
                grid.IsEmpty = false;
            });
        }

        private void AppendGroupRow(IHtmlNode body, IGroup group, int level)
        {
            var groupBuilder = new GridGroupRowHtmlBuilder<T>(grid, group)
            {
                Level = level,
                Adorners = 
                { 
                    new GridTagRepeatingAdorner(level)
                    {
                        CssClasses = {UIPrimitives.Grid.GroupCell},
                        Nbsp = true
                    }
                }
            };

            groupBuilder.Build().AppendTo(body);

            if (group.HasSubgroups)
            {
                group.Subgroups.Each(subgroup => AppendGroupRow(body, subgroup, level + 1));
            }
            else
            {
                IEnumerable<T> dataSource = group.Items.Cast<T>();
                AppendRows(body, dataSource);
            }
        }

        private IHtmlNode CreateDetailRow(GridRow<T> row)
        {
            var detailRowBuilder = new GridDetailRowHtmlBuilder<T>(row)
            {
                Colspan = grid.Colspan - 1
            };

            detailRowBuilder.Adorners.Add(new GridTagRepeatingAdorner(1)
            {
                CssClasses = { UIPrimitives.Grid.HierarchyCell },
                Nbsp = true
            });

            if (grid.DataProcessor.GroupDescriptors.Any())
            {
                var repeatingAdorner = new GridTagRepeatingAdorner(grid.DataProcessor.GroupDescriptors.Count)
                {
                    CssClasses = 
                        { 
                            UIPrimitives.Grid.GroupCell 
                        },
                    Nbsp = true
                };

                detailRowBuilder.Adorners.Add(repeatingAdorner);
            }
            
            return detailRowBuilder.Build();
        }

        private IHtmlNode CreateRow(GridRow<T> row)
        {
            if (grid.RowAction != null)
            {
                grid.RowAction(row);
            }
            var builder = grid.CreateBuilderFor(row);
            
            return builder.Build();
        }

        private IHtmlNode CreateHeaderRow()
        {
            var headRowBuilder = new GridHeaderRowHtmlBuilder(grid);
            
            return headRowBuilder.Build();
        }
        
        private IHtmlNode CreateTopPager()
        {
            var pagerWrapperBuilder = new GridPagerWrapperHtmlBuilder<T>(grid);
            
            pagerWrapperBuilder.OutputPager = true;
            
            return pagerWrapperBuilder.Build();
        }

        private GridPagerWrapperHtmlBuilder<T> CreateBottomPagerBuilder()
        {
            var pagerWrapperBuilder = new GridPagerWrapperHtmlBuilder<T>(grid);

            pagerWrapperBuilder.OutputPager = grid.Paging.Enabled && (grid.Paging.Position == GridPagerPosition.Bottom || grid.Paging.Position == GridPagerPosition.Both);

            return pagerWrapperBuilder;
        }
    }
}