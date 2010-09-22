namespace Telerik.Web.Mvc.UI.Html
{
    public class GridMasterRowAdorner : IHtmlAdorner
    {
        public void ApplyTo(IHtmlNode target)
        {
            target.AddClass("t-master-row");
        }
    }
}
