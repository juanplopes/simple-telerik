// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
#if MVC2
namespace Telerik.Web.Mvc.UI.Html
{
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridEditorForCellHtmlBuilder<TModel, TValue> : GridDataCellHtmlBuilder<TModel>
        where TModel : class
    {
        public GridEditorForCellHtmlBuilder(GridCell<TModel> cell) : base (cell)
        {
        }

        protected override IHtmlNode BuildCore()
        {
            var td = new HtmlTag("td");

            var column = (GridBoundColumn<TModel, TValue>)Cell.Column;

            ViewContext viewContext = Cell.Grid.ViewContext;
            
            var html = new HtmlHelper<TModel>(viewContext, new GridViewDataContainer<TModel>(Cell.DataItem, viewContext.ViewData));

            var validation = html.ValidationMessageFor(column.Expression);
            
            td.Html(html.EditorFor(column.Expression).ToHtmlString() + validation ?? "");
            
            return td;
        }
    }
}
#endif