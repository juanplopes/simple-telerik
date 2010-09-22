#if MVC2
namespace Telerik.Web.Mvc.UI.Html
{
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    public class GridFormEditRowHtmlBuilder<T> : GridEditRowHtmlBuilder<T>
        where T : class
    {
        public GridFormEditRowHtmlBuilder(GridRow<T> row) : base(row)
        {
        }

        protected override IHtmlNode BuildCore()
        {
            var tr = CreateRow();

            var td = CreateCell();

            td.AppendTo(tr);

            var form = BuildForm();
            
            form.AppendTo(td);
            
            return tr;
        }
        
        public string CreateEditorHtml()
        {
            ViewContext viewContext = Row.Grid.ViewContext;
            var html = new HtmlHelper<T>(viewContext, new GridViewDataContainer<T>(Row.DataItem, viewContext.ViewData));
            return html.EditorForModel().ToHtmlString();
        }

        public IHtmlNode BuildForm()
        {
            var form = CreateForm();

            var editor = new HtmlTag("div")
                .AddClass("t-edit-form-container");

            editor.AppendTo(form);

            editor.Html(CreateEditorHtml());

            Row.Grid
               .VisibleColumns
               .OfType<GridActionColumn<T>>()
               .Each(column => column
                   .Commands
                   .Each(command =>
                   {
                       if (Row.InInsertMode)
                       {
                           command.InsertModeHtml<T>(editor, Row);
                       }
                       else
                       {
                           command.EditModeHtml<T>(editor, Row);
                       }
                   })
               );
            return form;
        }
    }
}
#endif