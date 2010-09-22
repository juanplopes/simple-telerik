namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    
    public class GridEditRowHtmlBuilder<T> : GridRowHtmlBuilder<T>
        where T : class
    {
        public GridEditRowHtmlBuilder(GridRow<T> row)
            : base(row)
        {
        }

        public string ID
        {
            get;
            set;
        }

        public string ActionUrl
        {
            get;
            set;
        }

        public int Colspan
        {
            get;
            set;
        }

        protected override IHtmlNode BuildCore()
        {
            var tr = CreateRow();

            var td = CreateCell();

            td.AppendTo(tr);

            var form = CreateForm();

            form.AppendTo(td);

            var table = CreateTable();

            table.AppendTo(form);

            return tr;
        }

        private IHtmlNode CreateTable()
        {
            var table = new HtmlTag("table")
                .Attribute("cellspacing", "0");

            var colgroupBuilder = new GridColgroupHtmlBuilder(Row.Grid);
            
            var colgroup = colgroupBuilder.Build();

            colgroup.AppendTo(table);

            var tbody = new HtmlTag("tbody");

            tbody.AppendTo(table);

            var tr = new HtmlTag("tr");

            tr.AppendTo(tbody);

            if (Row.Grid.HasDetailView)
            {
                var detailViewAdorner = new GridToggleDetailViewAdorner
                {
                    Expanded = Row.DetailRow.Expanded
                };
            
                detailViewAdorner.ApplyTo(tr);
            }

            var repeatingAdorner = new GridTagRepeatingAdorner(Row.Grid.DataProcessor.GroupDescriptors.Count)
            {
                CssClasses = 
                    { 
                        UIPrimitives.Grid.GroupCell 
                    },
                Nbsp = true
            };

            repeatingAdorner.ApplyTo(tr);
            
            Row.Grid
               .VisibleColumns
               .Each(column => column
                    .CreateEditorHtmlBuilder(Row.CreateCellFor(column))
                    .Build()
                    .AppendTo(tr)
               );

            return table;
        }
        
        protected IHtmlNode CreateForm()
        {
            return new HtmlTag("form")
                .Attribute("method", "post")
                .Attribute("action", ActionUrl)
                .Attribute("id", ID)
                .AddClass(UIPrimitives.Grid.EditingForm);
        }
        
        protected IHtmlNode CreateCell()
        {
            return new HtmlTag("td")
                .Attribute("colspan", Colspan.ToString())
                .AddClass(UIPrimitives.Grid.EditingContainer);
        }
    }
}
