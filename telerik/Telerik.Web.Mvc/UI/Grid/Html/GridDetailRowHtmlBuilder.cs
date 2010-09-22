namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    
    public class GridDetailRowHtmlBuilder<T> : HtmlBuilderBase
        where T : class
    {
        private readonly GridRow<T> row;

        public GridDetailRowHtmlBuilder(GridRow<T> row)
        {
            this.row = row;
        }

        public int Colspan
        {
            get;
            set;
        }

        protected override IHtmlNode BuildCore()
        {
            var tr = new HtmlTag("tr")
                            .Attributes(row.DetailRow.HtmlAttributes)
                            .AddClass("t-detail-row");
            
            if (!row.DetailRow.Expanded)
            {
                tr.Css("display", "none");
            }

            var td = new HtmlTag("td")
                        .AddClass("t-detail-cell")
                        .Attribute("colspan", Colspan.ToString());
                        
            if (row.DetailRow.Html.HasValue())
            {
                td.Html(row.DetailRow.Html);
            }
            else
            {
                if (row.Grid.DetailView.Template.HasValue())
                {
                    row.Grid.DetailView.Template.Apply(row.DataItem, td);
                }
            }

            td.AppendTo(tr);
            
            return tr;
        }
    }
}
