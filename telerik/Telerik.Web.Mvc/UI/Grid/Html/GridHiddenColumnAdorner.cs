namespace Telerik.Web.Mvc.UI.Html
{
    public class GridHiddenColumnAdorner : IHtmlAdorner
    {
        public void ApplyTo(IHtmlNode target)
        {
            target.Css("display", "none")
                  .Css("width", "0");
        }
    }
}
