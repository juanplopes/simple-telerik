// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html
{
    using System.Linq;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    public class EditorHtmlBuilder : HtmlBuilderBase
    {
        private readonly Editor editor;

        public EditorHtmlBuilder(Editor component)
        {
            this.editor = component;
        }

        public IHtmlNode CreateEditor()
        {
            return new HtmlTag("table")
                    .Attributes(new { id = editor.Id, cellspacing = "4", cellpadding = "0" })
                    .Attributes(editor.HtmlAttributes)
                    .PrependClass(UIPrimitives.Widget, "t-editor", UIPrimitives.Header);
        }

        public IHtmlNode CreateTextArea()
        {
            var content = new HtmlTag("textarea")
                            .Attributes(new
                            {
                                title = editor.Id,
                                @class = "t-content t-raw-content",
                                cols = "20",
                                rows = "5",
                                name = editor.Id,
                                id = editor.Id + "-value"
                            });

            var value = editor.ViewContext.ViewData.Eval(editor.Name) ?? editor.Value;

            if (value != null)
            {
                content.Text(value.ToString());
            }
            else if (editor.Content != null)
            {
                editor.Template.Apply(content);
            }
            else if (editor.Template.InlineTemplate != null)
            {
                value = editor.Template.InlineTemplate(null);
                content.Text(value.ToString());
            }

            return content;
        }

        private IHtmlNode CreateToolBar()
        {
            return new EditorToolGroupHtmlBuilder(editor.DefaultToolGroup)
                .Build();
        }
        
        protected override IHtmlNode BuildCore()
        {
            var root = CreateEditor();

            var toolbarRow = new HtmlTag("tr").AppendTo(root);

            if (editor.DefaultToolGroup.Tools.Any())
            {
                CreateToolBar().AppendTo(new HtmlTag("td").AddClass("t-editor-toolbar-wrap").AppendTo(toolbarRow));
            }

            var editableCell = new HtmlTag("td")
                .AddClass("t-editable-area")
                .AppendTo(new HtmlTag("tr").AppendTo(root));

            var textarea = CreateTextArea();
            textarea.AppendTo(editableCell);
            
            var script = new HtmlTag("script")
                            .Attribute("type", "text/javascript")
                            .Html("document.getElementById('" + textarea.Attribute("id") + "').style.display='none'");

            script.AppendTo(editableCell);

            return root;
        }
    }
}
