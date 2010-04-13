namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class MenuController : Controller
    {
        public ActionResult Orientation(string orientation)
        {
            ViewData["orientation"] = orientation ?? "Vertical";
            return View();
        }
    }
}