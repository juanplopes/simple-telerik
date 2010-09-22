namespace Telerik.Web.Mvc.UI.Html
{
    using Telerik.Web.Mvc.Infrastructure;

    public class GridToggleDetailViewAdorner : IHtmlAdorner
    {
        public bool Expanded
        {
            get;
            set;
        }

        public void ApplyTo(IHtmlNode target)
        {
            var td = new HtmlTag("td")
                        .AddClass(UIPrimitives.Grid.HierarchyCell);

            var a = new HtmlTag("a")
                        .Attribute("href", "#")
                        .AddClass(UIPrimitives.Icon)
                        .ToggleClass("t-plus", !Expanded)
                        .ToggleClass("t-minus", Expanded);
            
            a.AppendTo(td);

            target.Children.Insert(0, td);
        }
    }
}
