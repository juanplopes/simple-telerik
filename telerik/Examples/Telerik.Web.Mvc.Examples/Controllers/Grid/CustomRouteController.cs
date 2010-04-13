namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class GridController : Controller
    {
        [SourceCodeFile("Global.asax", "~/Global.asax.cs")]
        public ActionResult CustomRoute()
        {
            return View(GetOrders());
        }
    }
}