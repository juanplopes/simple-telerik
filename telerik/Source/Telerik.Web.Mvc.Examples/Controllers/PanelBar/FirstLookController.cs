namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class PanelBarController : Controller
    {
        public ActionResult FirstLook(string expandMode)
        {
            ViewData["expandMode"] = expandMode ?? "Multiple";
            return View();
        }
    }
}