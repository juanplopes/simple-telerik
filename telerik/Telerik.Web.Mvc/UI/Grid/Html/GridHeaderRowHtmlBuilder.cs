// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html
{
    using System;
    using System.Linq;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    public class GridHeaderRowHtmlBuilder : HtmlBuilderBase
    {
        private readonly IGrid grid;
        private readonly Func<IGridColumn, IHtmlBuilder> cellBuilderFactoryMethod;

        public GridHeaderRowHtmlBuilder(IGrid grid)
        {
            this.grid = grid;
            
            ChildBuilderCreator = (column) => new GridHeaderCellHtmlBuilder(column);

            if (grid.Grouping.Enabled)
            {
                Adorners.Add(new GridTagRepeatingAdorner(grid.DataProcessor.GroupDescriptors.Count)
                    {
                        TagName = "th",
                        CssClasses = 
                        { 
                            UIPrimitives.Header, 
                            UIPrimitives.Grid.GroupCell 
                        }
                    });
            }

            if (grid.HasDetailView)
            {
                Adorners.Add(new GridTagRepeatingAdorner(1)
                {
                    TagName = "th",
                    CssClasses = 
                    { 
                        UIPrimitives.Header, 
                        UIPrimitives.Grid.HierarchyCell 
                    }
                });
            }

            this.cellBuilderFactoryMethod = column =>
            {
                var cellBuilder = ChildBuilderCreator(column);

                if (column.Hidden)
                {
                    cellBuilder.Adorners.Add(new GridHiddenColumnAdorner());
                }

                IGridBoundColumn boundColumn = column as IGridBoundColumn;
                if (boundColumn != null)
                {
                    if (grid.Sorting.Enabled && boundColumn.Sortable)
                    {
                        cellBuilder.Adorners.Add(new GridSortAdorner(boundColumn));
                    }

                    if (grid.Filtering.Enabled && boundColumn.Filterable)
                    {
                        cellBuilder.Adorners.Add(new GridFilterAdorner(boundColumn));
                    }
                }

                return cellBuilder;
            };
        }

        public Func<IGridColumn, IHtmlBuilder> ChildBuilderCreator
        {
            get;
            set;
        }

        protected override IHtmlNode BuildCore()
        {
            var row = new HtmlTag("tr");

            grid.Columns
                .Where(column => column.Visible)
                .Each(column => cellBuilderFactoryMethod(column)
                                    .Build()
                                    .AppendTo(row));

            return row;
        }
    }
}
