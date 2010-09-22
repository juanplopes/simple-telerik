// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Infrastructure;
    
    public class EditorSelectBoxHtmlBuilder : HtmlBuilderBase
    {
        private readonly EditorSelectBox selectBox;

        public EditorSelectBoxHtmlBuilder(EditorSelectBox selectBox)
        {
            this.selectBox = selectBox;
        }

        public new IHtmlNode Build()
        {
            return new HtmlTag("div")
                    .Attributes(selectBox.HtmlAttributes)
                    .PrependClass("t-selectbox", UIPrimitives.Header);
        }

        protected override IHtmlNode BuildCore()
        {
            var li = new HtmlTag("li")
                    .AddClass("t-editor-selectbox");


            IHtmlNode rootTag = Build();

            this.InnerContentTag().AppendTo(rootTag);

            rootTag.AppendTo(li);

            return li;
        }

        private IHtmlNode InnerContentTag() 
        {
            IHtmlNode root = new HtmlTag("div").AddClass("t-dropdown-wrap", UIPrimitives.DefaultState);

            new HtmlTag("span")
                .AddClass("t-input")
                .Html("&nbsp;")
                .AppendTo(root);

            IHtmlNode link = new HtmlTag("span").AddClass("t-select");

            new HtmlTag("span")
                .AddClass(UIPrimitives.Icon, "t-arrow-down")
                .Html("select")
                .AppendTo(link);

            link.AppendTo(root);

            return root;
        }
    }
}
