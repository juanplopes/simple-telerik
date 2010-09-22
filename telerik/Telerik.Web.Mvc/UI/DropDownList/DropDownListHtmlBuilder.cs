namespace Telerik.Web.Mvc.UI
{
    using System.Linq;

    using Infrastructure;
    using Extensions;

    public class DropDownListHtmlBuilder : IDropDownHtmlBuilder
    {

        public DropDownListHtmlBuilder(IDropDownRenderable component)
        {
            this.Component = component;
        }

        public IDropDownRenderable Component
        {
            get;
            private set;
        }

        public IHtmlNode Build()
        {
            return new HtmlTag("div")
                    .Attribute("id", Component.Id)
                    .Attributes(Component.HtmlAttributes)
                    .PrependClass(UIPrimitives.Widget, "t-dropdown", UIPrimitives.Header);
        }

        public IHtmlNode InnerContentTag()
        {
            IHtmlNode root = new HtmlTag("div").AddClass("t-dropdown-wrap", UIPrimitives.DefaultState);

            new HtmlTag("span")
                .AddClass("t-input")
                .Html(Component.Items.Any() ? Component.Items[Component.SelectedIndex].Text : "&nbsp;")
                .AppendTo(root);

            IHtmlNode link = new HtmlTag("span").AddClass("t-select");

            new HtmlTag("span")
                .AddClass(UIPrimitives.Icon, "t-arrow-down")
                .Html("select")
                .AppendTo(link);

            link.AppendTo(root);
            
            return root;
        }

        public IHtmlNode HiddenInputTag()
        {
            IHtmlNode input = new HtmlTag("input")
                    .Attributes(new
                    {
                        type = "text",
                        style = "display:none"
                    });

            if (Component.Name.HasValue())
                input.Attributes(new
                {
                    name = Component.Name,
                    id = Component.Id + "-value"
                });

            if(Component.Items.Any())
                input.Attribute("value", Component.Items[Component.SelectedIndex].Value);

            return input;
        }
    }
}
