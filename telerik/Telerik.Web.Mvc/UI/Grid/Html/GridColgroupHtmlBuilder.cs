namespace Telerik.Web.Mvc.UI.Html
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    public class GridColgroupHtmlBuilder : HtmlBuilderBase
    {
        private readonly IGrid grid;

        public GridColgroupHtmlBuilder(IGrid grid)
        {
            Guard.IsNotNull(grid, "grid");

            ChildBuilderCreator = (column) => new GridColHtmlBuilder(column);

            this.grid = grid;

            if (grid.Grouping.Enabled)
            {
                Adorners.Add(new GridTagRepeatingAdorner(grid.DataProcessor.GroupDescriptors.Count)
                {
                    TagName = "col",
                    CssClasses = { UIPrimitives.Grid.GroupCol },
                    RenderMode = TagRenderMode.SelfClosing
                });
            }

            if (grid.HasDetailView)
            {
                Adorners.Add(new GridTagRepeatingAdorner(1)
                {
                    TagName = "col",
                    CssClasses = { UIPrimitives.Grid.HierarchyCol },
                    RenderMode = TagRenderMode.SelfClosing
                });
            }
        }

        protected override IHtmlNode BuildCore()
        {
            var colgroup = new HtmlTag("colgroup");

            grid.Columns
                .Where(column => column.Visible)
                .Each(column =>
                {
                    var colBuilder = ChildBuilderCreator(column);
                    if (column.Hidden)
                    {
                        colBuilder.Adorners.Add(new GridHiddenColumnAdorner());
                    }
                    var col = colBuilder.Build();
                    col.AppendTo(colgroup);
                });

            return colgroup;
        }

        public Func<IGridColumn, IHtmlBuilder> ChildBuilderCreator
        {
            get;
            set;
        }
    }
}
