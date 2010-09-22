namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Infrastructure;

    public class GridPagerNumericButtonHtmlBulder : HtmlBuilderBase
    {
        public GridPagerNumericButtonHtmlBulder(string text)
        {
            Text = text;
        }

        public string Text
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        protected override IHtmlNode BuildCore()
        {
            var result = new HtmlTag(Active ? "span" : "a")
                .Text(Text)
                .ToggleClass(UIPrimitives.Link, !Active)
                .ToggleAttribute("href", Url, !Active)
                .ToggleClass(UIPrimitives.ActiveState, Active);
            
            return result;
        }
    }
}
