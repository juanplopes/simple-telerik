// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public class GridBoundColumnDisplayForHtmlBuilder<TModel, TValue> : GridColumnHtmlBuilderBase<TModel, GridBoundColumn<TModel, TValue>> where TModel : class
    {
        public GridBoundColumnDisplayForHtmlBuilder(GridBoundColumn<TModel, TValue> column) : base(column)
        {

        }

        public override void Html(GridCell<TModel> context, IHtmlNode td)
        {
            #if MVC2

            IViewDataContainer container = new GridViewDataContainer
            {
                ViewData = new ViewDataDictionary(Column.Grid.ViewContext.ViewData)
                {
                    Model = context.DataItem,
                    ModelMetadata = Column.Metadata
                }
            };

            HtmlHelper<TModel> htmlhelper = new HtmlHelper<TModel>(Column.Grid.ViewContext, container);

            td.Html(htmlhelper.DisplayFor(Column.Expression).ToString());

            #endif
        }
    }
}
