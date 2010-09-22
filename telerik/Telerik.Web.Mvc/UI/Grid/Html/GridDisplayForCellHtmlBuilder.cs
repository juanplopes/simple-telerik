// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

#if MVC2
namespace Telerik.Web.Mvc.UI.Html
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Extensions;

    public class GridDisplayForCellHtmlBuilder<TModel, TValue> : GridDataCellHtmlBuilder<TModel>
        where TModel : class
    {
        private readonly ViewContext viewContext;
        private readonly TModel model;
        private readonly Expression<Func<TModel, TValue>> expression;
        
        public GridDisplayForCellHtmlBuilder(GridCell<TModel> cell, Expression<Func<TModel, TValue>> expression)
        : base(cell)
        {
            this.viewContext = cell.Grid.ViewContext;
            this.model = cell.DataItem;
            this.expression = expression;
        }
        
        protected override IHtmlNode BuildCore()
        {
            var td = CreateCell();

            var html = new HtmlHelper<TModel>(viewContext, new GridViewDataContainer<TModel>(model, viewContext.ViewData));

            string content = html.DisplayFor(expression).ToHtmlString();
            td.Html(content.IsNullOrEmpty() ? "&nbsp;" : content);

            return td;
        }
    }
}
#endif