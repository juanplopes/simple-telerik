// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public class GridBoundColumnHtmlBuilder<TModel, TValue> : GridColumnHtmlBuilderBase<TModel, GridBoundColumn<TModel, TValue>> where TModel : class
    {
        private readonly IGridColumnHtmlBuilder<TModel> innerHtmlBuilder;

        public GridBoundColumnHtmlBuilder(GridBoundColumn<TModel, TValue> column, IGridColumnHtmlBuilder<TModel> innerHtmlBuilder) : base(column)
        {
            this.innerHtmlBuilder = innerHtmlBuilder;
        }

        public override void Html(GridCell<TModel> context, IHtmlNode td)
        {
            #if MVC2

            if ((context.InInsertMode || context.InEditMode) && !Column.ReadOnly)
            {
                IViewDataContainer container = new GridViewDataContainer
                {
                    ViewData = new ViewDataDictionary(Column.Grid.ViewContext.ViewData)
                    { 
                        Model = context.DataItem,
                        ModelMetadata = Column.Metadata
                    }
                };

                HtmlHelper<TModel> htmlhelper = new HtmlHelper<TModel>(Column.Grid.ViewContext, container);

                string html = htmlhelper.EditorFor(Column.Expression).ToString();

                MvcHtmlString validationMessage = htmlhelper.ValidationMessageFor(Column.Expression);

                if (validationMessage != null)
                {
                    html += validationMessage;
                }

                td.Html(html);
                return;
            }

            #endif

            innerHtmlBuilder.Html(context, td);
        }
    }
}
