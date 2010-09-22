namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridDataCellHtmlBuilder<T> : HtmlBuilderBase
        where T : class
    {
        public GridDataCellHtmlBuilder(GridCell<T> cell)
        {
            Cell = cell;
        }

        public GridCell<T> Cell
        {
            get;
            private set;
        }
        
        public object Value
        {
            get;
            set;
        }

        protected override IHtmlNode BuildCore()
        {
            var td = CreateCell();

            if (Value != null)
            {
                string format = Cell.Column.Format;
                string content = format.HasValue() ? format.FormatWith(Value) : Value.ToString();

                if (Cell.Column.Encoded)
                {
                    td.Text(content);
                }
                else
                {
                    td.Html(content);
                }
            }
            else
            {
                td.Html("&nbsp;");
            }

            return td;
        }
        
        protected IHtmlNode CreateCell()
        {
            return new HtmlTag("td").Attributes(Cell.HtmlAttributes);
        }
    }
}
