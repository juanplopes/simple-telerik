namespace Telerik.Web.Mvc.UI.Html
{
    public class GridTemplateCellHtmlBuilder<T> : GridDataCellHtmlBuilder<T>
        where T : class
    {
        public GridTemplateCellHtmlBuilder(GridCell<T> cell)
            : base(cell)
        {
        }
       
        protected override IHtmlNode BuildCore()
        {
            var td = CreateCell();
            
            Cell.Template.Apply(Cell.DataItem, td);
            
            return td;
        }
    }
}